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

        [Header("Visuals")]
        public Sprite openSprite;

        // **********************************
        // 1. เพิ่มช่องใส่เสียงใน Inspector
        // **********************************
        [Header("Audio")]
        public AudioClip interactSound; // ลากไฟล์เสียงมาใส่ในช่องนี้

        private SpriteRenderer spriteRenderer;
        private bool isAlreadyOpen = false;

        public override void SetUP()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override bool Interact(OOPPlayer player)
        {
            if (isAlreadyOpen)
            {
                // (Optional) อาจจะใส่เสียง "Lock" หรือเสียงไม่ได้ทำอะไรที่นี่
                return false;
            }

            // **********************************
            // 2. สั่งเล่นเสียง (ถ้ามีคลิปเสียงใส่ไว้)
            // **********************************
            if (interactSound != null)
            {
                // PlayClipAtPoint จะสร้างวัตถุชั่วคราวเพื่อเล่นเสียงจนจบ (เหมาะกับวัตถุที่กำลังจะถูกทำลาย)
                AudioSource.PlayClipAtPoint(interactSound, transform.position);
            }

            // --- ส่วนที่เหลือเหมือนเดิม ---
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            if (mapGenerator == null) { return false; }

            string myType = objectType.ToString();
            bool foundKey = false;

            if (myType == GameState.KeyPart1_Location && !GameState.KeyPart1_Found)
            {
                player.inventory.AddItem("KeyPart1", 1);
                GameState.KeyPart1_Found = true;
                foundKey = true;
            }
            else if (myType == GameState.KeyPart2_Location && !GameState.KeyPart2_Found)
            {
                player.inventory.AddItem("KeyPart2", 1);
                GameState.KeyPart2_Found = true;
                foundKey = true;
            }

            if (objectType == InteractableType.Box || objectType == InteractableType.Fermenter)
            {
                if (!foundKey) Debug.Log($"You broke the {Name}. It was empty.");

                mapGenerator.mapdata[positionX, positionY] = null;
                Destroy(gameObject); // เสียงจะยังเล่นต่อจนจบเพราะใช้ PlayClipAtPoint
            }
            else
            {
                if (!foundKey) Debug.Log($"You examine the {Name}, but find nothing.");

                if (openSprite != null && spriteRenderer != null)
                {
                    spriteRenderer.sprite = openSprite;
                }
            }

            isAlreadyOpen = true;
            return true;
        }

        public override bool Hit() { return false; }
    }
}