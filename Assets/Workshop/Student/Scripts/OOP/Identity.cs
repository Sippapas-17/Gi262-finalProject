using UnityEngine;
using Solution; 

namespace Solution
{
    public class Identity : MonoBehaviour
    {
        [Header("Identity")]
        public string Name;
        public int positionX;
        public int positionY;

        public OOPMapGenerator mapGenerator;

        public void Start()
        {
            SetUP();
        }

        public virtual void SetUP()
        {
        }

        public void PrintInfo()
        {
            Debug.Log("created " + Name + " at " + positionX + ":" + positionY);
        }

        public virtual bool Hit()
        {
            return false; 
        }

        public virtual bool Interact(OOPPlayer player)
        {
            Debug.Log(Name + ": Cannot interact with this object.");
            return false;
        }
    }
}