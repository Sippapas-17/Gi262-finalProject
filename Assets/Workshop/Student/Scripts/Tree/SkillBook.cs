using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solution; // <--- สำคัญ: ต้องเพิ่มเพื่อให้รู้จัก Skill และ SkillTree

namespace Solution // <--- ต้องครอบด้วย namespace Solution
{
    public class SkillBook : MonoBehaviour
    {
        // อ้างอิงถึง SkillTree ที่สร้างขึ้น
        public SkillTree attackSkillTree;

        // ควรมี reference ถึง Player 
        public OOPPlayer player;

        void Start()
        {
            // สร้าง Skill Tree โครงสร้างเดิม:
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
                // Logic: ตรวจสอบว่าผู้เล่นมีทรัพยากรที่ใช้ในการเรียนรู้หรือไม่
                // สมมติใช้ "SkillPoint" 1 หน่วย
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