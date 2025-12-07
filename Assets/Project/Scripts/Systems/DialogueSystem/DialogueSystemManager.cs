using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class DialogueSystemManager
    {
        public readonly string EndDialogueKey = "EndDialogue";
        
        private const string UNDERTAKER_DIALOGUE_FILE_PATH = "EN_Undertaker_Dialogue";
        private const string BLACKSMITH_DIALOGUE_FILE_PATH = "EN_Blacksmith_Dialogue";
        private const string TRADER_DIALOGUE_FILE_PATH = "EN_Trader_Dialogue";

        private const string UNDERTAKER_SPRITE_FILE_PATH = "UndertakerDialogueSprite";
        private const string BLACKSMITH_SPRITE_FILE_PATH = "BlacksmithDialogueSprite";
        private const string TRADER_SPRITE_FILE_PATH = "TraderDialogueSprite";

        private IAssetLoader _assetLoader;
        
        private int _undertakerCurrentText = 1;
        private int _blacksmithCurrentText = 1;
        private int _traderCurrentText = 1;

        private Dictionary<NpcTypes, int> _npcDialogueText;
        private Dictionary<NpcTypes, Dictionary<string,string>> _npcCurrentDialogueList;
            
        private Dictionary<NpcTypes, string> _npcSpritesPaths;
        private Dictionary<NpcTypes, Sprite> _npcSprites;

        private Dictionary<string, string> _undertakerDialogue = new();
        private Dictionary<string, string> _blacksmithDialogue = new();
        private Dictionary<string, string> _traderDialogue = new();


        public DialogueSystemManager(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public void Initialize()
        {
            InitializeDialoguesFromFiles();
        }

        public string GetNpcName(NpcTypes npcType)
        {
            var currentDialogueList = _npcCurrentDialogueList[npcType];
            var currentDialogue = currentDialogueList[0.ToString()];
            return currentDialogue;
        }

        public string GetCurrentDialogue(NpcTypes npcType)
        {
            var dialogueCurrentTextId = 0;

            var currentDialogueList = _npcCurrentDialogueList[npcType];
            dialogueCurrentTextId =  _npcDialogueText[npcType];
            
            if (currentDialogueList.Count <= dialogueCurrentTextId)
            {
                RestartDialogue(npcType);
                return EndDialogueKey;
            }
            
            var currentDialogue = currentDialogueList[dialogueCurrentTextId.ToString()];
            return currentDialogue;
        }

        public Sprite GetCurrentSpeakerSprite(NpcTypes npcType)
        {
            return _npcSprites[npcType];
        }

        public void UpgradeDialogueState(NpcTypes npcType)
        {
            _npcDialogueText[npcType]++;
        }

        private void RestartDialogue(NpcTypes npcType)
        {
            _npcDialogueText[npcType] = 1;
        }

        private async void InitializeDialoguesFromFiles()
        { 
            var undertakerDialogueFile = await _assetLoader.LoadNotGameObjectAsync<TextAsset>(UNDERTAKER_DIALOGUE_FILE_PATH);
             _undertakerDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(undertakerDialogueFile.text);
             
             var blacksmithDialogueFile = await _assetLoader.LoadNotGameObjectAsync<TextAsset>(BLACKSMITH_DIALOGUE_FILE_PATH);
             _blacksmithDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(blacksmithDialogueFile.text);
             
             var traderDialogueFile = await _assetLoader.LoadNotGameObjectAsync<TextAsset>(TRADER_DIALOGUE_FILE_PATH);
             _traderDialogue = JsonConvert.DeserializeObject<Dictionary<string, string>>(traderDialogueFile.text);
             
             InitializeDialogues();
        }

        private async void InitializeDialogues()
        {
            _npcDialogueText = new()
            {
                {NpcTypes.Undertaker, _undertakerCurrentText},
                {NpcTypes.Blacksmith, _blacksmithCurrentText},
                {NpcTypes.Trader, _traderCurrentText}
            };

            _npcCurrentDialogueList = new()
            {
                {NpcTypes.Undertaker, _undertakerDialogue},
                {NpcTypes.Blacksmith, _blacksmithDialogue},
                {NpcTypes.Trader, _traderDialogue}
            };

            _npcSpritesPaths = new()
            {
                { NpcTypes.Undertaker, UNDERTAKER_SPRITE_FILE_PATH },
                { NpcTypes.Blacksmith, BLACKSMITH_SPRITE_FILE_PATH },
                { NpcTypes.Trader, TRADER_SPRITE_FILE_PATH }
            };

            _npcSprites = new()
            {
                {
                    NpcTypes.Undertaker,
                    await _assetLoader.LoadNotGameObjectAsync<Sprite>(_npcSpritesPaths[NpcTypes.Undertaker])
                },
                {
                    NpcTypes.Blacksmith,
                    await _assetLoader.LoadNotGameObjectAsync<Sprite>(_npcSpritesPaths[NpcTypes.Blacksmith])
                },
                {
                    NpcTypes.Trader,
                    await _assetLoader.LoadNotGameObjectAsync<Sprite>(_npcSpritesPaths[NpcTypes.Trader])
                }
            };
        }
    }
}