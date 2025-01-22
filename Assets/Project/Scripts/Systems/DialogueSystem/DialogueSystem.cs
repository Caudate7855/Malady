using Cysharp.Threading.Tasks;
using Project.Scripts.Core;
using Zenject;

namespace Project.Scripts.DialogueSystem
{
    public class DialogueSystem
    {
        public string DialogueID { get; set; }

        public int UndertakerCurrentText;
        public int BlacksmithCurrentText;
        public int TraderCurrentText;

        [Inject] private IAssetLoader _assetLoader;

        private string DialoguePath = "EN_Dialogue";
        private string DialogueFile;

        public DialogueSystem()
        {
            Initialize();
        }

        private async void Initialize()
        {
            DialogueFile = await GetJsonDialogueFile();
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
        
        private async UniTask<string> GetJsonDialogueFile()
        {
            var dialogueFile = await _assetLoader.Load<string>(DialoguePath);
            //DialogueFileJSON dialogueFileJson = 

            return "123";
        }
    }
}