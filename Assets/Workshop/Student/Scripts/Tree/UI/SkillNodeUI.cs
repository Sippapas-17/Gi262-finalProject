using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Solution;

namespace Solution
{
    public class SkillNodeUI : MonoBehaviour
    {
        [Header("UI References")]
        public Button button;
        public Image background;
        public TextMeshProUGUI skillNameText;

        [HideInInspector] public Skill skillData;

        [Header("Colors")]
        public Color colorLearned = Color.yellow;
        public Color colorAvailable = Color.green;
        public Color colorLocked = Color.gray;

        public void Initialize(Skill skill)
        {
            this.skillData = skill;
            skillNameText.text = skill.name;

            button.onClick.AddListener(OnNodeClicked);
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (skillData.isUnlocked)
            {
                background.color = colorLearned;
                button.interactable = false;
            }
            else if (skillData.isAvailable)
            {
                background.color = colorAvailable;
                button.interactable = true;
            }
            else
            {
                background.color = colorLocked;
                button.interactable = false;
            }
        }

        private void OnNodeClicked()
        {
            if (skillData.isAvailable && !skillData.isUnlocked)
            {
                skillData.Unlock();

                SkillTreeUI.Instance.RefreshAllUI();
            }
        }
    }
}