using Itibsoft.PanelManager;
using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "DialogueWindow")]
    public class DialogueWindowController : PanelControllerBase<DialogueWindow>
    {
        public NpcTypes CurrentNpcType;
        
        [Inject] private DialogueSystemManager _dialogueSystemManager;
        
        protected override void Initialize()
        {
            
        }

        public void ShowDialogueText()
        {
            var text = _dialogueSystemManager.GetCurrentDialogue(CurrentNpcType);
            
            Debug.Log(text);   
        }

        public void ShowSpeakerImage()
        {
            
        }
    }
}