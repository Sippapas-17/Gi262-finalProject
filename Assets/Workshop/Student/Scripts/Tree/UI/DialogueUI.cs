using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Solution
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject dialoguePanel;
        public TextMeshProUGUI npcText;
        public Transform choiceContainer;
        public Button choiceButtonPrefab;

        // **********************************
        // ต้องมี GameObject นี้ประกาศไว้:
        // **********************************
        public GameObject closeButtonDialogue;

        private DialogueSequen InterractNpcSequen;

        // เก็บปุ่มที่ถูกสร้างขึ้น 
        private List<Button> activeButtons = new List<Button>();

        public void Setup(DialogueSequen sequen)
        {
            this.InterractNpcSequen = sequen;
            DialogueNode currentNode = InterractNpcSequen.tree.root;
            ShowDialogue(currentNode);

            // การเรียกใช้ closeButtonDialogue จะไม่ Error แล้ว
            closeButtonDialogue.SetActive(false);
            gameObject.SetActive(true);
        }

        public void ShowDialogue(DialogueNode node)
        {
            InterractNpcSequen.currentNode = node;
            npcText.text = node.text;
            ClearChoices();

            var choices = new List<string>(node.nexts.Keys);
            for (int i = 0; i < choices.Count; i++)
            {
                string choiceText = choices[i];
                CreateChoiceButton(choiceText, i);
            }
        }

        private void CreateChoiceButton(string text, int index)
        {
            Button newButton = Instantiate(choiceButtonPrefab, choiceContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = text;
            newButton.onClick.AddListener(() => OnChoiceSelected(index));
            activeButtons.Add(newButton);
        }

        private void ClearChoices()
        {
            foreach (Button button in activeButtons)
            {
                Destroy(button.gameObject);
            }
            activeButtons.Clear();
        }

        private void OnChoiceSelected(int index)
        {
            InterractNpcSequen.SelectChoice(index);
        }

        public void ShowCloseButtonDialog()
        {
            // การเรียกใช้ closeButtonDialogue จะไม่ Error แล้ว
            closeButtonDialogue.gameObject.SetActive(true);
        }

        public void HideDialogue()
        {
            dialoguePanel.SetActive(false);
            ClearChoices();
        }
    }
}