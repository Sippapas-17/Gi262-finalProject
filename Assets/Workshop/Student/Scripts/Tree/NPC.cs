using Solution;
using UnityEngine;

namespace Solution
{
    public class NPC : Identity
    {
        public DialogueUI dialogueUI;
        public DialogueSequen dialogueSeauen;
        public bool canTalk = true;

        public override bool Hit()
        {
            return false; 
        }

        public override bool Interact(OOPPlayer player)
        {
            if (dialogueUI == null || dialogueSeauen == null)
            {
                Debug.LogError("NPC Error: DialogueUI หรือ DialogueSequen เป็น None ใน Inspector!");
                return false;
            }

            if (canTalk)
            {
                dialogueUI.gameObject.SetActive(true);

                Debug.Log("NPC: Starting dialogue...");
                dialogueUI.Setup(dialogueSeauen);
                StartDialogue(player);
                return true;
            }
            else
            {
                Debug.Log("NPC: Not ready to talk.");
                return false;
            }
        }

        public void StartDialogue(OOPPlayer player)
        {
            dialogueUI.gameObject.SetActive(true);
            dialogueSeauen.dialogueUI = dialogueUI;

            DialogueNode nextNode;

            bool hasMasterKey = (player.inventory != null && player.inventory.HasItem("MasterKey", 1));
            bool hasKey1 = (player.inventory != null && player.inventory.HasItem("KeyPart1", 1));

            if (hasMasterKey)
            {
                nextNode = dialogueSeauen.GetNode("farewell");
            }
            else if (hasKey1)
            {
                string hintKey = GameState.KeyPart2_Location;

                nextNode = dialogueSeauen.GetNode(hintKey);
            }
            else
            {
                string hintKey = GameState.KeyPart1_Location; 

                nextNode = dialogueSeauen.GetNode(hintKey);
            }

            if (nextNode != null)
            {
                dialogueSeauen.currentNode = nextNode;
                dialogueUI.ShowDialogue(nextNode);
                dialogueUI.ShowCloseButtonDialog(); 
            }
            else
            {
                Debug.LogError("NPC Error: DialogueNode not found.");
                dialogueUI.HideDialogue();
            }
        }
    }
}