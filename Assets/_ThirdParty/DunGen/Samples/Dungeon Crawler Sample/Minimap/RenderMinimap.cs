using UnityEngine;
using UnityEngine.UI;

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
            UnityEngine.Experimental.Rendering.GraphicsFormat.D24_UNorm_S8_UInt;

        private void OnEnable()
        {
            minimapCamera = GetComponent<Camera>();

            const int inputRes = 512;
            const int outputRes = 512;

            // Depth required for cameras
            cameraBuffer = CreateCameraRT(inputRes, inputRes);
            iconsBuffer = CreateCameraRT(outputRes, outputRes);

            // No depth needed for Blit-only textures
            distanceFieldBuffer = new RenderTexture(inputRes, inputRes, 0);
            outputBuffer = new RenderTexture(outputRes, outputRes, 0);

            createDistanceFieldMaterialInstance = new Material(createDistanceFieldMaterial);
            createDistanceFieldMaterialInstance.SetFloat("_TextureSize", inputRes);

            minimapCamera.targetTexture = cameraBuffer;
            minimapIconsCamera.targetTexture = iconsBuffer;

            var minimapObject = GameObject.Find("Minimap");
            minimapImage = minimapObject.GetComponent<RawImage>();

            var minimapIconsObject = GameObject.Find("Minimap Icons");
            minimapIconsImage = minimapIconsObject.GetComponent<RawImage>();

            minimapImage.texture = outputBuffer;
            minimapIconsImage.texture = iconsBuffer;
        }

        private RenderTexture CreateCameraRT(int width, int height)
        {
            var rt = new RenderTexture(width, height, 0)
            {
                depthStencilFormat = DepthFormat
            };
            rt.Create();
            return rt;
        }

        private void OnDisable()
        {
            minimapCamera.targetTexture = null;
            minimapIconsCamera.targetTexture = null;

            DestroySafe(cameraBuffer);
            DestroySafe(distanceFieldBuffer);
            DestroySafe(outputBuffer);
            DestroySafe(iconsBuffer);
            DestroySafe(createDistanceFieldMaterialInstance);
        }

        private void DestroySafe(Object obj)
        {
            if (obj != null)
                Destroy(obj);
        }

        private void OnPostRender()
        {
            Graphics.Blit(cameraBuffer, distanceFieldBuffer, createDistanceFieldMaterialInstance);
            Graphics.Blit(distanceFieldBuffer, outputBuffer, drawMinimapMaterial);
        }
    }
}
