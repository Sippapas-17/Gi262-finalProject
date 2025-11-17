using System;
using UnityEngine;

namespace Solution
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;
        public ActionHistoryManager actionHistoryManager;

        public bool isAutoMoving = false;

        public override void SetUP()
        {
            base.SetUP();
            PrintInfo();

            if (inventory == null) inventory = GetComponent<Inventory>();
            if (actionHistoryManager == null) actionHistoryManager = GetComponent<ActionHistoryManager>();
        }

        // เมธอด Update() สำหรับจัดการ Input
        public void Update()
        {
            if (!isAutoMoving)
            {
                // Input การเคลื่อนที่: เรียก Move(Vector2 direction) ของคลาสแม่
                if (Input.GetKeyDown(KeyCode.W)) { Move(Vector2.up); }
                if (Input.GetKeyDown(KeyCode.S)) { Move(Vector2.down); }
                if (Input.GetKeyDown(KeyCode.A)) { Move(Vector2.left); }
                if (Input.GetKeyDown(KeyCode.D)) { Move(Vector2.right); }

                // Input สำหรับ Interact (E)
                if (Input.GetKeyDown(KeyCode.E))
                {
                    TryInteract();
                }

            }
        }

        // <--- สำคัญ: Override Move() เพื่อบันทึก Undo ก่อนเคลื่อนที่
        public override bool Move(Vector2 direction)
        {
            // บันทึกตำแหน่งปัจจุบันก่อนเคลื่อนที่ (สำหรับ Undo)
            if (actionHistoryManager != null)
            {
                actionHistoryManager.SaveStateForUndo(new Vector2(positionX, positionY));
            }

            // เรียกเมธอด Move() ของคลาสแม่ (Character)
            return base.Move(direction);
        }


        // เมธอดสำหรับพยายามโต้ตอบกับวัตถุข้างหน้า
        private void TryInteract()
        {
            Vector2 direction = GetLastMoveDirection(); // <--- เรียกจาก Character.cs

            int targetX = (int)(positionX + direction.x);
            int targetY = (int)(positionY + direction.y);

            if (mapGenerator == null)
            {
                Debug.LogError("Player Error: MapGenerator reference is missing!");
                return;
            }

            Identity targetObject = mapGenerator.GetMapData(targetX, targetY);

            if (targetObject != null)
            {
                // เรียกเมธอด Interact() ของวัตถุนั้น (NPC, หีบ, ฯลฯ)
                targetObject.Interact(this);
            }
            else
            {
                Debug.Log("Player: Nothing to interact with.");
            }
        }
    }
}