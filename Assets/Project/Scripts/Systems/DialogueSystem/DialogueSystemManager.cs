using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class DialogueSystemManager
    {
        public int UndertakerCurrentText = 1;
        public int BlacksmithCurrentText = 1;
        public int TraderCurrentText = 1;
        
        private const string UNDERTAKER_DIALOGUE_FILE_PATH = "Dialogues/EN_Undertaker_Dialogue";
        private const string BLACKSMITH_DIALOGUE_FILE_PATH = "Dialogues/EN_Blacksmith_Dialogue";
        private const string TRADER_DIALOGUE_FILE_PATH = "Dialogues/EN_Trader_Dialogue";

        private Dictionary<string, string> _undertakerDialogue = new();
        private Dictionary<string, string> _blacksmithDialogue = new();
        private Dictionary<string, string> _traderDialogue = new();
        
        public DialogueSystemManager()
        { 
            InitializeDialoguesFromFiles();
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

        public string GetCurrentDialogue(NpcTypes npcType)
        {
            var currentDialogueList = new Dictionary<string, string>();
            string dialogueCurrentTextId = "";

            switch (npcType)
            {
                case NpcTypes.Undertaker:
                    currentDialogueList = _undertakerDialogue;
                    dialogueCurrentTextId = UndertakerCurrentText.ToString();
                    break;
                
                case NpcTypes.Blacksmith:
                    currentDialogueList = _blacksmithDialogue;
                    dialogueCurrentTextId = BlacksmithCurrentText.ToString();
                    break;
                
                case NpcTypes.Trader:
                    currentDialogueList = _traderDialogue;
                    dialogueCurrentTextId = TraderCurrentText.ToString();
                    break;
            }

            var currentDialogue = currentDialogueList[dialogueCurrentTextId];
            return currentDialogue;
        }

        public string GetNpcName(NpcTypes npcType)
        {
            var currentDialogueList = new Dictionary<string, string>();
            
            switch (npcType)
            {
                case NpcTypes.Undertaker:
                    currentDialogueList = _undertakerDialogue;
                    break;
                
                case NpcTypes.Blacksmith:
                    currentDialogueList = _blacksmithDialogue;
                    break;
                
                case NpcTypes.Trader:
                    currentDialogueList = _traderDialogue;
                    break;
            }

            var currentDialogue = currentDialogueList[0.ToString()];
            return currentDialogue;
        }
        
        private void InitializeDialoguesFromFiles()
        {
            var undertakerDialogueFile = Resources.Load<TextAsset>(UNDERTAKER_DIALOGUE_FILE_PATH);
             _undertakerDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(undertakerDialogueFile.text);
             
             var blacksmithDialogueFile = Resources.Load<TextAsset>(BLACKSMITH_DIALOGUE_FILE_PATH);
             _blacksmithDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(blacksmithDialogueFile.text);
             
             var traderDialogueFile = Resources.Load<TextAsset>(TRADER_DIALOGUE_FILE_PATH);
             _traderDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(traderDialogueFile.text);
        }
    }
}