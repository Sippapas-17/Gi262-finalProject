using System.Collections; // ต้องมีสำหรับ IEnumerator
using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    public class ActionHistoryManager : MonoBehaviour
    {
        // 1. Undo System using Stack
        private Stack<Vector2> undoStack = new Stack<Vector2>();
        private Stack<Vector2> redoStack = new Stack<Vector2>();
        // 2. Auto-Move System using Queue
        private Queue<Vector2> autoMoveQueue = new Queue<Vector2>();

        // เมธอดสำหรับบันทึกสถานะ (คุณต้องเรียกใช้ใน OOPPlayer.Move ก่อน base.Move)
        public void SaveStateForUndo(Vector2 currentPosition)
        {
            undoStack.Push(currentPosition);
            redoStack.Clear(); // เคลียร์ Redo เมื่อมีการเคลื่อนที่ใหม่
        }

        // เมธอดที่ OOPPlayer เรียกใช้: Undo
        public void UndoLastMove(OOPPlayer player)
        {
            if (undoStack.Count > 1) // ต้องมีอย่างน้อย 2 สถานะ (ปัจจุบันและก่อนหน้า)
            {
                // 1. Pop ตำแหน่งปัจจุบัน (ที่ไม่ต้องการ) ไปใส่ Redo
                Vector2 lastPos = undoStack.Pop(); // <--- แก้ไข Syntax ตรงนี้
                redoStack.Push(lastPos);

                // 2. Peek ตำแหน่งก่อนหน้าเพื่อย้อนกลับไป
                Vector2 previousPos = undoStack.Peek();
                player.UpdatePosition((int)previousPos.x, (int)previousPos.y);
            }
            else
            {
                Debug.Log("Cannot Undo: History is empty or only start position remains.");
            }
        }

        // เมธอดที่ OOPPlayer เรียกใช้: Redo
        public void RedoLastMove(OOPPlayer player)
        {
            if (redoStack.Count > 0)
            {
                Vector2 nextPos = redoStack.Pop();

                // บันทึกตำแหน่งปัจจุบันลงใน Undo Stack ก่อนเคลื่อนที่
                undoStack.Push(new Vector2(player.positionX, player.positionY));

                // เคลื่อนที่ไปยังตำแหน่ง Redo
                player.UpdatePosition((int)nextPos.x, (int)nextPos.y);
            }
            else
            {
                Debug.Log("Cannot Redo: Redo Stack is empty.");
            }
        }

        // เมธอดที่ OOPPlayer เรียกใช้: StartAutoMoveSequence
        public void StartAutoMoveSequence(OOPPlayer player)
        {
            // สร้าง Sequence ตัวอย่าง
            autoMoveQueue.Clear();
            autoMoveQueue.Enqueue(Vector2.right);
            autoMoveQueue.Enqueue(Vector2.up);
            // ... (เพิ่มทิศทางอื่น ๆ ตามต้องการ)

            StartCoroutine(ProcessAutoMoveSequence(player));
        }

        // Coroutine สำหรับประมวลผล Queue
        public IEnumerator ProcessAutoMoveSequence(OOPPlayer player)
        {
            player.isAutoMoving = true;

            while (autoMoveQueue.Count > 0)
            {
                Vector2 nextMove = autoMoveQueue.Dequeue();
                player.Move(nextMove); // เรียก Move เพื่อให้เกิด Side Effect (ถ้ามี)
                yield return new WaitForSeconds(0.3f); // รอ 0.3 วินาทีต่อการเคลื่อนไหว
            }

            player.isAutoMoving = false;
        }
    }
}