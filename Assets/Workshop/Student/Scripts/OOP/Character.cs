using UnityEngine;

namespace Solution
{
    public class Character : Identity
    {
        protected Vector2 lastMoveDirection = Vector2.down;

        public virtual bool Move(Vector2 direction)
        {
            if (mapGenerator == null)
            {
                Debug.LogError("Character: MapGenerator reference is missing!");
                return false;
            }

            int toX = positionX + (int)direction.x;
            int toY = positionY + (int)direction.y;

            if (direction != Vector2.zero)
            {
                lastMoveDirection = direction;
            }

            if (!mapGenerator.HasPlacement(toX, toY))
            {
                Debug.Log($"Character: Cannot move to ({toX},{toY}), out of bounds.");
                return false; 
            }

            Identity targetObject = mapGenerator.GetMapData(toX, toY);
            if (targetObject != null)
            {
                if (!targetObject.Hit()) 
                {
                    Debug.Log($"Character: Hit {targetObject.Name}, cannot move.");
                    return false; 
                }
            }

            mapGenerator.mapdata[positionX, positionY] = null;

            positionX = toX;
            positionY = toY;
            transform.position = new Vector3(positionX, positionY, 0);

            mapGenerator.mapdata[positionX, positionY] = this;

            Debug.Log($"Character: Moved to ({positionX},{positionY})");
            return true;
        }

        public Vector2 GetLastMoveDirection()
        {
            return lastMoveDirection;
        }

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