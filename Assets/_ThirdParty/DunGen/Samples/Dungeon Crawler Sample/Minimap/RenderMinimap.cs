using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace DunGen.DungeonCrawler
{
    [RequireComponent(typeof(Camera))]
    sealed class RenderMinimap : MonoBehaviour
    {
        [SerializeField] private Material drawMinimapMaterial = null;
        [SerializeField] private Camera minimapIconsCamera = null;
        [SerializeField] private Material createDistanceFieldMaterial = null;

        private RawImage minimapImage = null;
        private RawImage minimapIconsImage = null;
        private Camera minimapCamera;

        private RenderTexture cameraBuffer;
        private RenderTexture distanceFieldBuffer;
        private RenderTexture outputBuffer;
        private RenderTexture iconsBuffer;

        private Material createDistanceFieldMaterialInstance;

        private readonly UnityEngine.Experimental.Rendering.GraphicsFormat DepthFormat =
            UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm;

        private void OnEnable()
        {
            minimapCamera = GetComponent<Camera>();

            const int inputRes = 512;
            const int outputRes = 512;

            cameraBuffer = CreateCameraRT(inputRes, inputRes);
            iconsBuffer = CreateCameraRT(outputRes, outputRes);

            distanceFieldBuffer = new RenderTexture(inputRes, inputRes, 0);
            outputBuffer = new RenderTexture(outputRes, outputRes, 0);

            createDistanceFieldMaterialInstance = new Material(createDistanceFieldMaterial);
            createDistanceFieldMaterialInstance.SetFloat("_TextureSize", inputRes);

            minimapCamera.targetTexture = cameraBuffer;
            minimapIconsCamera.targetTexture = iconsBuffer;

            minimapImage = GameObject.Find("Minimap").GetComponent<RawImage>();
            minimapIconsImage = GameObject.Find("Minimap Icons").GetComponent<RawImage>();

            minimapImage.texture = outputBuffer;
            minimapIconsImage.texture = iconsBuffer;

            RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        }

        private void OnDisable()
        {
            RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;

            minimapCamera.targetTexture = null;
            minimapIconsCamera.targetTexture = null;

            Destroy(cameraBuffer);
            Destroy(distanceFieldBuffer);
            Destroy(outputBuffer);
            Destroy(iconsBuffer);
            Destroy(createDistanceFieldMaterialInstance);
        }

        private RenderTexture CreateCameraRT(int width, int height)
        {
            var rt = new RenderTexture(width, height, 0)
            {
                graphicsFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.R8G8B8A8_UNorm,
                depthStencilFormat = UnityEngine.Experimental.Rendering.GraphicsFormat.D16_UNorm
            };

            rt.Create();
            return rt;
        }

        private void OnEndCameraRendering(ScriptableRenderContext context, Camera cam)
        {
            if (cam != minimapCamera)
                return;

            Graphics.Blit(cameraBuffer, distanceFieldBuffer, createDistanceFieldMaterialInstance);
            Graphics.Blit(distanceFieldBuffer, outputBuffer, drawMinimapMaterial);
        }
    }
}
