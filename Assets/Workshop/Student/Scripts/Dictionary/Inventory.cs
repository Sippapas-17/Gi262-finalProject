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

            CheckForCrafting(); 
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

        private void CheckForCrafting()
        {
            if (HasItem("KeyPart1", 1) && HasItem("KeyPart2", 1))
            {
                UseItem("KeyPart1", 1);
                UseItem("KeyPart2", 1);

                AddItem("MasterKey", 1);

                Debug.LogWarning("CRAFTING SUCCESS: Key parts combined into MasterKey!");
            }
        }
    }
}