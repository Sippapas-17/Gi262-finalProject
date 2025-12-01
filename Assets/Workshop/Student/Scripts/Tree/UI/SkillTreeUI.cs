using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solution;

namespace Solution
{
    public class SkillTreeUI : MonoBehaviour
    {
        public static SkillTreeUI Instance;

        [Header("Required References")]
        public SkillBook skillBook;
        public SkillNodeUI skillNodePrefab;
        public Transform skillNodeContainer;

        public RectTransform contentSkill;

        private float minX = 0f;
        private float maxX = 0f;
        private float minY = 0f;
        private float maxY = 0f;

        private readonly float NODE_WIDTH = 150f;
        private readonly float NODE_HEIGHT = 150f;
        private readonly float X_SPACING = 300f;
        private readonly float Y_SPACING = 200f;

        private Dictionary<Skill, SkillNodeUI> skillUIMap = new Dictionary<Skill, SkillNodeUI>();

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            if (skillBook == null || contentSkill == null)
            {
                Debug.LogError("SkillBook หรือ ContentSkill reference is missing!");
                return;
            }

            StartCoroutine(DelayShowTree());
        }

        IEnumerator DelayShowTree()
        {
            yield return new WaitForSeconds(0.1f);
            minX = 0f; maxX = 0f; minY = 0f; maxY = 0f;

            if (skillBook.attackSkillTree.rootSkill != null)
            {
                CreateAllSkillNodes(skillBook.attackSkillTree.rootSkill, Vector2.zero);
            }

            CalculateAndSetContentSize();
            RefreshAllUI();
        }

        private void CalculateAndSetContentSize()
        {
            if (skillUIMap.Count == 0) return;

            float contentWidth = (maxX - minX) + NODE_WIDTH + 50f;
            float contentHeight = Mathf.Abs(minY) + NODE_HEIGHT + 50f;

            contentSkill.sizeDelta = new Vector2(contentWidth, contentHeight);
        }

        private void CreateAllSkillNodes(Skill currentSkill, Vector2 position)
        {
            if (skillUIMap.ContainsKey(currentSkill)) return;

            SkillNodeUI newNode = Instantiate(skillNodePrefab, skillNodeContainer);
            newNode.Initialize(currentSkill);
            skillUIMap.Add(currentSkill, newNode);

            RectTransform rt = newNode.GetComponent<RectTransform>();
            rt.localPosition = position;

            int numChildren = currentSkill.nextSkills.Count;

            float totalWidth = (numChildren - 1) * X_SPACING;
            float startX = position.x - (totalWidth / 2f);

            for (int i = 0; i < numChildren; i++)
            {
                Skill nextSkill = currentSkill.nextSkills[i];

                Vector2 nextPos = new Vector2(
                    startX + (i * X_SPACING),
                    position.y - Y_SPACING
                );

                CreateAllSkillNodes(nextSkill, nextPos);
            }
        }

        public void RefreshAllUI()
        {
            foreach (var uiNode in skillUIMap.Values)
            {
                uiNode.UpdateUI();
            }
        }

        public void CloseUI()
        {
            gameObject.SetActive(false);
        }
    }
}