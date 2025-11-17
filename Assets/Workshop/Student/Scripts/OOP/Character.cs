using UnityEngine;

namespace Solution
{
    // คลาสแม่ของสิ่งมีชีวิตที่เคลื่อนที่ได้
    public class Character : Identity
    {
        // เก็บทิศทางล่าสุดที่เคลื่อนที่
        protected Vector2 lastMoveDirection = Vector2.down;

        // เมธอดสำหรับเคลื่อนที่
        // (ต้องเป็น virtual เพื่อให้ OOPPlayer override ได้)
        public virtual bool Move(Vector2 direction)
        {
            if (mapGenerator == null)
            {
                Debug.LogError("Character: MapGenerator reference is missing!");
                return false;
            }

            int toX = positionX + (int)direction.x;
            int toY = positionY + (int)direction.y;

            // <--- สำคัญ: อัปเดตทิศทางที่หันหน้าไป
            if (direction != Vector2.zero)
            {
                lastMoveDirection = direction;
            }

            // 1. ตรวจสอบว่าอยู่ในขอบเขตแผนที่หรือไม่
            if (!mapGenerator.HasPlacement(toX, toY))
            {
                Debug.Log($"Character: Cannot move to ({toX},{toY}), out of bounds.");
                return false; // ชนขอบแผนที่
            }

            // 2. ตรวจสอบว่ามีวัตถุอื่นขวางหรือไม่
            Identity targetObject = mapGenerator.GetMapData(toX, toY);
            if (targetObject != null)
            {
                // ถ้ามีวัตถุ ให้เรียก Hit() ของวัตถุนั้น
                if (!targetObject.Hit()) // ถ้า Hit() คืนค่า false (บล็อก)
                {
                    Debug.Log($"Character: Hit {targetObject.Name}, cannot move.");
                    return false; // เดินชน
                }
            }

            // 3. ถ้าเดินได้ (ไม่มีอะไรขวาง หรือ Hit() คืนค่า true)
            // ล้างตำแหน่งเก่าใน mapdata
            mapGenerator.mapdata[positionX, positionY] = null;

            // อัปเดตตำแหน่งใหม่
            positionX = toX;
            positionY = toY;
            transform.position = new Vector3(positionX, positionY, 0);

            // อัปเดตตำแหน่งใหม่ใน mapdata
            mapGenerator.mapdata[positionX, positionY] = this;

            Debug.Log($"Character: Moved to ({positionX},{positionY})");
            return true;
        }

        // เมธอดสำหรับส่งคืนทิศทางล่าสุด (ให้ TryInteract() ใช้)
        public Vector2 GetLastMoveDirection()
        {
            return lastMoveDirection;
        }

        // เมธอดสำหรับอัปเดตตำแหน่ง (ใช้ใน Undo/Redo)
        public void UpdatePosition(int newX, int newY)
        {
            if (mapGenerator == null) return;

            if (mapGenerator.mapdata[positionX, positionY] == this)
            {
                mapGenerator.mapdata[positionX, positionY] = null;
            }

            positionX = newX;
            positionY = newY;
            transform.position = new Vector3(positionX, positionY, 0);
            mapGenerator.mapdata[positionX, positionY] = this;
        }
    }
}