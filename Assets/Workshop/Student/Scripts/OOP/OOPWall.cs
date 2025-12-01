using UnityEngine;

namespace Solution
{
    public class OOPWall : Identity
    {
        public bool IsIceWall;

        public override void SetUP()
        {
            base.SetUP();

            IsIceWall = Random.Range(0, 100) < 20 ? true : false;
            if (IsIceWall)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.color = Color.blue;
                }
            }
        }

        public override bool Hit()
        {
            mapGenerator.mapdata[positionX, positionY] = null;
            Destroy(gameObject);

            if (IsIceWall)
            {
                if (mapGenerator.player != null && mapGenerator.player.inventory != null)
                {
                    mapGenerator.player.inventory.AddItem("KeyPart1", 1);
                    Debug.Log("The Ice Wall shattered, revealing a small part of the key!");
                }
            }

            return false;
        }
    }
}