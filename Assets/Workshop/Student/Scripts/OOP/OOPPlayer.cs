using System;
using UnityEngine;

namespace Solution
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;
        // ลบ ActionHistoryManager ออกแล้ว

        public bool isAutoMoving = false;

        public override void SetUP()
        {
            base.SetUP();
            PrintInfo();

            if (inventory == null) inventory = GetComponent<Inventory>();
        }

        public void Update()
        {
            if (!isAutoMoving)
            {
                if (Input.GetKeyDown(KeyCode.W)) { Move(Vector2.up); }
                if (Input.GetKeyDown(KeyCode.S)) { Move(Vector2.down); }
                if (Input.GetKeyDown(KeyCode.A)) { Move(Vector2.left); }
                if (Input.GetKeyDown(KeyCode.D)) { Move(Vector2.right); }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    TryInteract();
                }

            }
        }

        public override bool Move(Vector2 direction)
        {
            return base.Move(direction);
        }

        private void TryInteract()
        {
            Vector2 direction = GetLastMoveDirection();

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
                targetObject.Interact(this);
            }
            else
            {
                Debug.Log("Player: Nothing to interact with.");
            }
        }
    }
}