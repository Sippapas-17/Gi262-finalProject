using UnityEngine;

namespace Solution
{
    // 1. Enum (ตัวเลือก) สำหรับประเภทของวัตถุ
    public enum InteractableType
    {
        Statue,     // รูปปั้น
        Box,        // กล่อง
        Fermenter,  // ถังหมัก
        Chest,      // หีบ
        Coffin      // โลงศพ
    }

    public class InteractableObject : Identity
    {
        [Header("Object Type")]
        // 2. นี่คือช่องที่ต้องมีใน Inspector
        public InteractableType objectType;

        // (เราลบ Item To Give ออก)

        // (เราลบ DialogueUI และ Sequen ออก)

        public override void SetUP()
        {
            // ไม่ต้องทำอะไร
        }

        public override bool Interact(OOPPlayer player)
        {
            // 1. เช็คว่าผู้เล่นเจอไอเท็มไปแล้วหรือยัง
            if (GameState.Key1_Found)
            {
                Debug.Log("The object is now empty.");
                return false;
            }

            // 2. เช็คว่า "ฉัน" (วัตถุนี้) คือที่ซ่อนกุญแจที่ถูกสุ่มเลือกหรือไม่
            string myType = objectType.ToString(); // (เช่น "Chest", "Fermenter")

            if (myType == GameState.Key1_LocationName)
            {
                // 3. ถ้าใช่ ให้ KeyPart1
                player.inventory.AddItem("KeyPart1", 1);
                GameState.Key1_Found = true;

                Debug.Log($"You found KeyPart1 hidden inside the {Name}!");
                return true;
            }

            // 4. ถ้าไม่ใช่ที่ซ่อนกุญแจ
            Debug.Log($"You examine the {Name}, but find nothing.");
            return true;
        }

        public override bool Hit() { return false; } // บล็อกไม่ให้เดินทะลุ
    }
}