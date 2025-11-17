using UnityEngine;

namespace Solution
{
    public enum InteractableType
    {
        Statue,
        Box,
        Fermenter,
        Chest,
        Coffin
    }

    public class InteractableObject : Identity
    {
        [Header("Object Type")]
        public InteractableType objectType;

        // **********************************
        // 1. เพิ่ม 3 บรรทัดนี้
        // **********************************
        [Header("Visuals")]
        public Sprite openSprite; // <--- ช่องสำหรับใส่ "รูปตอนเปิด"

        private SpriteRenderer spriteRenderer;
        private bool isAlreadyOpen = false; // <--- ตัวแปรเช็คว่าเปิดไปรึยัง
        // **********************************

        public override void SetUP()
        {
            // 2. ดึง SpriteRenderer มาเก็บไว้ตอนเริ่ม
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override bool Interact(OOPPlayer player)
        {
            // 3. เช็คว่าถ้าเปิดไปแล้ว ให้หยุดทำงาน
            if (isAlreadyOpen)
            {
                Debug.Log("This object has already been interacted with.");
                return false;
            }

            string myType = objectType.ToString();
            bool foundKey = false;

            // ... (Logic การเช็ค KeyPart1 และ KeyPart2 เหมือนเดิม) ...
            if (myType == GameState.KeyPart1_Location && !GameState.KeyPart1_Found)
            {
                player.inventory.AddItem("KeyPart1", 1);
                GameState.KeyPart1_Found = true;
                Debug.Log($"You interacted with the {Name} and found KeyPart1!");
                foundKey = true;
            }
            else if (myType == GameState.KeyPart2_Location && !GameState.KeyPart2_Found)
            {
                player.inventory.AddItem("KeyPart2", 1);
                GameState.KeyPart2_Found = true;
                Debug.Log($"You interacted with the {Name} and found KeyPart2!");
                foundKey = true;
            }

            // **********************************
            // 4. แก้ไข Logic การแสดงผล
            // **********************************
            if (objectType == InteractableType.Box || objectType == InteractableType.Fermenter)
            {
                // ถ้าเป็น กล่อง หรือ ถังหมัก -> ให้ทำลายทิ้ง (เหมือนเดิม)
                if (!foundKey) Debug.Log($"You broke the {Name}. It was empty.");
                mapGenerator.mapdata[positionX, positionY] = null;
                Destroy(gameObject);
            }
            else
            {
                // ถ้าเป็น รูปปั้น, หีบ, โลงศพ -> ให้เปลี่ยน Sprite (ไม่ทำลาย)
                if (!foundKey) Debug.Log($"You examine the {Name}, but find nothing.");

                // สั่งเปลี่ยน Sprite
                if (openSprite != null)
                {
                    spriteRenderer.sprite = openSprite;
                }
            }

            // 5. ตั้งค่าว่า "เปิดแล้ว" (กันการกดซ้ำ)
            isAlreadyOpen = true;
            return true;
        }

        public override bool Hit()
        {
            return false;
        }
    }
}