using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class DialogueSystemManager
    {
        public string DialogueID { get; set; }

        public int UndertakerCurrentText;
        public int BlacksmithCurrentText;
        public int TraderCurrentText;
        
        private string DialoguePath = "Dialogues/EN_Dialogue";
        private string DialogueFile;

        private Dictionary<string, string> _undertakerDialogue = new();
        private Dictionary<string, string> _blacksmithDialogue = new();
        private Dictionary<string, string> _traderDialogue = new();
        
        public DialogueSystemManager()
        {
            DialogueFile = GetJsonDialogueFile();
        }

        public void UpgradeDialogueState(NpcTypes npcType)
        {
            switch (npcType)
            {
                case NpcTypes.Undertaker:
                    UndertakerCurrentText++;
                    break;
                
                case NpcTypes.Blacksmith:
                    BlacksmithCurrentText++;
                    break;
                
                case NpcTypes.Trader:
                    TraderCurrentText++;
                    break;
            }
        }
        
        private void SearchDialogueText(NpcTypes npcType)
        {
            switch (npcType)
            {
                case NpcTypes.Undertaker:
                    
                    break;
                
                case NpcTypes.Blacksmith:

                    break;
                
                case NpcTypes.Trader:

                    break;
            }
        }
        
        private string GetJsonDialogueFile()
        {
            var dialogueFile = Resources.LoadAll<TextAsset>(DialoguePath);
             _undertakerDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(dialogueFile[0].text);

            foreach (var entry in _undertakerDialogue)
            {
                Debug.Log($"Key: {entry.Key}, Value: {entry.Value}");
            }

            return "123";
        }
    }
}