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
            Debug.Log("NPC: Blocked player movement.");
            return false; // ไม่ให้เดินทะลุ
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

            // NPC จะตรวจสอบ Inventory และให้คำใบ้ที่ถูกต้อง
            if (player.inventory != null && player.inventory.HasItem("MasterKey", 1))
            {
                nextNode = dialogueSeauen.GetNode("farewell");
            }
            else if (player.inventory != null && player.inventory.HasItem("KeyPart1", 1))
            {
                nextNode = dialogueSeauen.GetNode("clue2");
            }
            else
            {
                nextNode = dialogueSeauen.GetNode("clue1");
            }

            if (nextNode != null)
            {
                dialogueSeauen.currentNode = nextNode;
                dialogueUI.ShowDialogue(nextNode);

                dialogueUI.ShowCloseButtonDialog(); // <--- สั่งให้ปุ่ม "ปิด" แสดงขึ้นมา
            }
            else
            {
                Debug.LogError("NPC Error: DialogueNode not found.");
                dialogueUI.HideDialogue();
            }
        }
    }
}