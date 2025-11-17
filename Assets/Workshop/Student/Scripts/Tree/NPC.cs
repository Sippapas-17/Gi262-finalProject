using Solution;
using UnityEngine;

namespace Solution
{
    public class NPC : Identity
    {
        public DialogueUI dialogueUI;
        public DialogueSequen dialogueSeauen;
        public bool canTalk = true;

        // 1. ป้องกันการเดินทะลุ (Hit)
        public override bool Hit()
        {
            return false; // บล็อกผู้เล่น
        }

        // 2. เริ่มการสนทนา (Interact)
        public override bool Interact(OOPPlayer player)
        {
            if (dialogueUI == null || dialogueSeauen == null)
            {
                Debug.LogError("NPC Error: DialogueUI หรือ DialogueSequen เป็น None ใน Inspector!");
                return false;
            }

            if (canTalk)
            {
                // **********************************
                // ******* 1. ปลุก UI ขึ้นมาก่อน *******
                // **********************************
                dialogueUI.gameObject.SetActive(true);

                // 2. ค่อยเรียก Setup และ Start
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

        // 3. "สมอง" ของ NPC (เมธอดนี้คือส่วนที่คุณถามถึง)
        public void StartDialogue(OOPPlayer player)
        {
            dialogueUI.gameObject.SetActive(true);
            dialogueSeauen.dialogueUI = dialogueUI;

            DialogueNode nextNode;

            // ตรวจสอบสถานะกุญแจใน Inventory
            bool hasMasterKey = (player.inventory != null && player.inventory.HasItem("MasterKey", 1));
            bool hasKey1 = (player.inventory != null && player.inventory.HasItem("KeyPart1", 1));
            // (เราสมมติว่ากุญแจดอกที่ 2 ชื่อ KeyPart2)
            // bool hasKey2 = (player.inventory != null && player.inventory.HasItem("KeyPart2", 1));

            // --- Logic การเลือกคำใบ้ ---

            // 1. ถ้ามี MasterKey แล้ว (รวมร่าง Key1 และ Key2 แล้ว)
            if (hasMasterKey)
            {
                nextNode = dialogueSeauen.GetNode("farewell"); // พูด "ไปประตูทางออกได้"
            }
            // 2. ถ้ายังไม่มี MasterKey แต่มี KeyPart1 แล้ว
            else if (hasKey1)
            {
                // (หมายความว่าผู้เล่นกำลังหา KeyPart2)
                // NPC จะไปดู "คำตอบ" จาก GameState ว่า Key 2 ซ่อนอยู่ที่ไหน
                string hintKey = GameState.KeyPart2_Location; // (เช่น "Chest" หรือ "Box")

                // NPC ดึงคำใบ้ของ "Chest" หรือ "Box" จาก Dictionary
                nextNode = dialogueSeauen.GetNode(hintKey);
            }
            // 3. ถ้ายังไม่ได้กุญแจดอกที่ 1
            else
            {
                // (หมายความว่าผู้เล่นกำลังหา KeyPart1)
                // NPC จะไปดู "คำตอบ" จาก GameState ว่า Key 1 ซ่อนอยู่ที่ไหน
                string hintKey = GameState.KeyPart1_Location; // (เช่น "Statue" หรือ "Coffin")

                // NPC ดึงคำใบ้ของ "Statue" หรือ "Coffin" จาก Dictionary
                nextNode = dialogueSeauen.GetNode(hintKey);
            }

            // --- แสดงผลคำใบ้ ---
            if (nextNode != null)
            {
                dialogueSeauen.currentNode = nextNode;
                dialogueUI.ShowDialogue(nextNode);
                dialogueUI.ShowCloseButtonDialog(); // แสดงปุ่มปิด
            }
            else
            {
                Debug.LogError("NPC Error: DialogueNode not found.");
                dialogueUI.HideDialogue();
            }
        }
    }
}