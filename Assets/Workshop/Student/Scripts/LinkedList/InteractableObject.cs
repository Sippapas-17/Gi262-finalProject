using UnityEngine;

namespace Solution
{
    // 1. Enum (µÑÇàÅ×Í¡) ÊÓËÃÑº»ÃÐàÀ·¢Í§ÇÑµ¶Ø
    public enum InteractableType
    {
        Statue,     // ÃÙ»»Ñé¹
        Box,        // ¡ÅèÍ§
        Fermenter,  // ¶Ñ§ËÁÑ¡
        Chest,      // ËÕº
        Coffin      // âÅ§È¾
    }

    public class InteractableObject : Identity
    {
        [Header("Object Type")]
        // 2. ¹Õè¤×ÍªèÍ§·ÕèµéÍ§ÁÕã¹ Inspector
        public InteractableType objectType;

        // (àÃÒÅº Item To Give ÍÍ¡)

        // (àÃÒÅº DialogueUI áÅÐ Sequen ÍÍ¡)

        public override void SetUP()
        {
            // äÁèµéÍ§·ÓÍÐäÃ
        }

        public override bool Interact(OOPPlayer player)
        {
            // 1. àªç¤ÇèÒ¼ÙéàÅè¹à¨ÍäÍà·çÁä»áÅéÇËÃ×ÍÂÑ§
            if (GameState.Key1_Found)
            {
                Debug.Log("The object is now empty.");
                return false;
            }

            // 2. àªç¤ÇèÒ "©Ñ¹" (ÇÑµ¶Ø¹Õé) ¤×Í·Õè«èÍ¹¡Ø­á¨·Õè¶Ù¡ÊØèÁàÅ×Í¡ËÃ×ÍäÁè
            string myType = objectType.ToString(); // (àªè¹ "Chest", "Fermenter")

            if (myType == GameState.Key1_LocationName)
            {
                // 3. ¶éÒãªè ãËé KeyPart1
                player.inventory.AddItem("KeyPart1", 1);
                GameState.Key1_Found = true;

                Debug.Log($"You found KeyPart1 hidden inside the {Name}!");
                return true;
            }

            // 4. ¶éÒäÁèãªè·Õè«èÍ¹¡Ø­á¨
            Debug.Log($"You examine the {Name}, but find nothing.");
            return true;
        }

        public override bool Hit() { return false; } // ºÅçÍ¡äÁèãËéà´Ô¹·ÐÅØ
    }
}