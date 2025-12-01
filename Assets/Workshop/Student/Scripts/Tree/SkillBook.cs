using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solution; 

namespace Solution 
{
    public class SkillBook : MonoBehaviour
    {
        public SkillTree attackSkillTree;

        public OOPPlayer player;

        void Start()
        {
            Skill root = new Skill("Explorer's Eye");
            root.isAvailable = true;

            Skill level1 = new Skill("Clue Whisperer");
            Skill level2 = new Skill("Master Key Crafter");

            root.nextSkills.Add(level1);
            level1.nextSkills.Add(level2);

            attackSkillTree = new SkillTree(root);
        }

        public void LearnSkill(Skill skill, OOPPlayer player)
        {
            if (skill.isAvailable && !skill.isUnlocked)
            {
                if (player.inventory.HasItem("SkillPoint", 1))
                {
                    player.inventory.UseItem("SkillPoint", 1);

                    skill.Unlock();

                    Debug.Log($"Successfully learned {skill.name}");
                }
                else
                {
                    Debug.Log("Not enough SkillPoint to learn this skill.");
                }
            }
        }
    }
}