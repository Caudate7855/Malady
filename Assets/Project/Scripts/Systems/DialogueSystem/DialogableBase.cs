using System;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Core.Abstracts;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public abstract class DialogableBase : InteractableZoneBase , IDialogable
    {
        public NpcTypes NpcType;
        
        [Inject] private IPanelManager PanelManager;
        private DialogueWindowController _dialogueWindowController;
        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }

        public void ShowDialogue()
        {
            _dialogueWindowController = PanelManager.LoadPanel<DialogueWindowController>();
            _dialogueWindowController.CurrentNpcType = NpcType;
            
            _dialogueWindowController.Open();
            _dialogueWindowController.ShowDialogueWindow();
        }
    }
}