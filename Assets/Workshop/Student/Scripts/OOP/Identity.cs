using UnityEngine;
using Solution; // เพื่อให้รู้จัก OOPPlayer

namespace Solution
{
    // คลาสแม่ของทุกวัตถุในแผนที่
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

        // เมธอด Hit() (เมื่อเดินชน)
        // คืนค่า true = เดินผ่านได้
        // คืนค่า false = เดินผ่านไม่ได้ (บล็อก)
        public virtual bool Hit()
        {
            return false; // <--- Default คือเดินผ่านไม่ได้
        }

        // เมธอด Interact() (เมื่อกด E)
        public virtual bool Interact(OOPPlayer player)
        {
            Debug.Log(Name + ": Cannot interact with this object.");
            return false;
        }
    }
}