using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    public class ActionHistoryManager : MonoBehaviour
    {
        private Stack<Vector2> undoStack = new Stack<Vector2>();
        private Stack<Vector2> redoStack = new Stack<Vector2>();
        private Queue<Vector2> autoMoveQueue = new Queue<Vector2>();

        public void SaveStateForUndo(Vector2 currentPosition)
        {
            undoStack.Push(currentPosition);
            redoStack.Clear(); 
        }

        public void UndoLastMove(OOPPlayer player)
        {
            if (undoStack.Count > 1) 
            {
                Vector2 lastPos = undoStack.Pop(); 
                redoStack.Push(lastPos);

                Vector2 previousPos = undoStack.Peek();
                player.UpdatePosition((int)previousPos.x, (int)previousPos.y);
            }
            else
            {
                Debug.Log("Cannot Undo: History is empty or only start position remains.");
            }
        }

        public void RedoLastMove(OOPPlayer player)
        {
            if (redoStack.Count > 0)
            {
                Vector2 nextPos = redoStack.Pop();

                undoStack.Push(new Vector2(player.positionX, player.positionY));

                player.UpdatePosition((int)nextPos.x, (int)nextPos.y);
            }
            else
            {
                Debug.Log("Cannot Redo: Redo Stack is empty.");
            }
        }

        public void StartAutoMoveSequence(OOPPlayer player)
        {
            autoMoveQueue.Clear();
            autoMoveQueue.Enqueue(Vector2.right);
            autoMoveQueue.Enqueue(Vector2.up);

            StartCoroutine(ProcessAutoMoveSequence(player));
        }

        public IEnumerator ProcessAutoMoveSequence(OOPPlayer player)
        {
            player.isAutoMoving = true;

            while (autoMoveQueue.Count > 0)
            {
                Vector2 nextMove = autoMoveQueue.Dequeue();
                player.Move(nextMove); 
                yield return new WaitForSeconds(0.3f); 
            }

            player.isAutoMoving = false;
        }
    }
}