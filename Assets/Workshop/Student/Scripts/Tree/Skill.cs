using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    // **********************************************
    // 1. คลาส Skill (Data Node)
    // **********************************************
    public class Skill
    {
        public string name;
        public bool isUnlocked;
        public bool isAvailable;
        public List<Skill> nextSkills;

        public Skill(string name)
        {
            this.name = name;
            isUnlocked = false;
            nextSkills = new List<Skill>();
        }

        public void Unlock()
        {
            if (!isAvailable)
            {
                throw new System.Exception("Skill is not available to unlock.");
            }

            if (isUnlocked)
            {
                Debug.Log($"Skill {name} is already unlocked.");
                return;
            }

            isUnlocked = true;

            for (int i = 0; i < nextSkills.Count; i++)
            {
                nextSkills[i].isAvailable = true;
            }
        }

        // ... (เมธอด PrintSkillTreeHierarchy อื่นๆ) ...
    }

    // **********************************************
    // 2. คลาส SkillTree (ถูกสร้างและรวมไว้ในไฟล์เดียวกัน)
    // **********************************************
    public class SkillTree
    {
        public Skill rootSkill;

        public SkillTree(Skill rootSkill)
        {
            this.rootSkill = rootSkill;
        }
    }
}