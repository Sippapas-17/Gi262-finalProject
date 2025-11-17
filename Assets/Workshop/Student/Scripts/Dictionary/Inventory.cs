using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    public class Inventory : MonoBehaviour
    {
        public Dictionary<string, int> inventory = new Dictionary<string, int>();

        public void AddItem(string item, int amount)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] += amount;
            }
            else
            {
                inventory.Add(item, amount);
            }
            Debug.Log("Added " + amount + " " + item + ". Total: " + inventory[item]);

            // **********************************
            // ******* เพิ่มการเรียกเมธอดนี้ *******
            // **********************************
            CheckForCrafting(); // ตรวจสอบการคราฟต์ทุกครั้งที่เพิ่มของ
        }

        public void UseItem(string item, int amount)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] -= amount;
                if (inventory[item] <= 0)
                {
                    inventory.Remove(item);
                }
            }
        }

        public bool HasItem(string item, int amount)
        {
            return inventory.ContainsKey(item) && inventory[item] >= amount;
        }

        public int GetItemCount(string item)
        {
            if (inventory.ContainsKey(item))
            {
                return inventory[item];
            }
            return 0;
        }

        // **********************************
        // ******* เมธอดใหม่สำหรับรวมกุญแจ *******
        // **********************************
        private void CheckForCrafting()
        {
            // ตรวจสอบสูตร MasterKey
            if (HasItem("KeyPart1", 1) && HasItem("KeyPart2", 1))
            {
                // 1. ลบชิ้นส่วนออกจากกระเป๋า
                UseItem("KeyPart1", 1);
                UseItem("KeyPart2", 1);

                // 2. เพิ่มกุญแจดอกสมบูรณ์
                AddItem("MasterKey", 1);

                // (แสดงข้อความพิเศษใน Console)
                Debug.LogWarning("CRAFTING SUCCESS: Key parts combined into MasterKey!");
            }

            // (คุณสามารถเพิ่มสูตรคราฟต์อื่น ๆ ที่นี่ได้ในอนาคต)
        }
    }
}