using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solution; 

namespace Solution
{
    public class DialogueNode
    {
        public string text;
        public Dictionary<string, DialogueNode> nexts = new Dictionary<string, DialogueNode>();

        public DialogueNode(string text)
        {
            this.text = text;
            nexts = new Dictionary<string, DialogueNode>();
        }

        public void AddNext(DialogueNode next, string choiceText)
        {
            nexts.Add(choiceText, next);
        }
    }

    public class DialogueTree
    {
        public DialogueNode root;

        public DialogueTree(DialogueNode root)
        {
            this.root = root;
        }
    }


    public class DialogueSequen : MonoBehaviour
    {
        public DialogueTree tree;
        public DialogueNode currentNode;
        public DialogueUI dialogueUI;

        private Dictionary<string, DialogueNode> allNodes = new Dictionary<string, DialogueNode>();

        public void Start()
        {
            LoadConversations();
        }

        private void LoadConversations()
        {
            DialogueNode greeting = new DialogueNode("Ah, traveler! I know you seek the way out. Find the Master Key.");
            allNodes.Add("greeting", greeting);

            DialogueNode clue1 = new DialogueNode("The first key part is hidden where 'Time stands still' but its eye is cold blue.");
            allNodes.Add("clue1", clue1);

            DialogueNode clue2 = new DialogueNode("The final part of the key is guarded by the 'Three-headed beast'. A Firestorm might weaken its bond.");
            allNodes.Add("clue2", clue2);

            DialogueNode farewell = new DialogueNode("You have proven yourself. The Master Key will open the Final Gate.");
            allNodes.Add("farewell", farewell);

            DialogueNode chestNode = new DialogueNode("Perhaps the key is in what treasure hunters seek most?");
            allNodes.Add("Chest", chestNode); 

            DialogueNode boxNode = new DialogueNode("In many games, people love to break these.");
            allNodes.Add("Box", boxNode);

            DialogueNode fermenterNode = new DialogueNode("This is often used for fermenting or storing goods.");
            allNodes.Add("Fermenter", fermenterNode);

            DialogueNode coffinNode = new DialogueNode("A final resting place.");
            allNodes.Add("Coffin", coffinNode);

            DialogueNode statueNode = new DialogueNode("People build these to remember someone or something.");
            allNodes.Add("Statue", statueNode);

            tree = new DialogueTree(greeting);
        }

        public DialogueNode GetNode(string key)
        {
            if (allNodes.ContainsKey(key))
            {
                return allNodes[key];
            }
            Debug.LogError($"Dialogue Node '{key}' not found! Returning root node.");
            return tree.root;
        }

        public void SelectChoice(int index)
        {
            if (currentNode == null || currentNode.nexts.Count == 0 || index >= currentNode.nexts.Count)
            {
                dialogueUI.HideDialogue();
                return;
            }

            var choiceKeys = new List<string>(currentNode.nexts.Keys);
            string selectedKey = choiceKeys[index];

            currentNode = currentNode.nexts[selectedKey];

            dialogueUI.ShowDialogue(currentNode);

            if (currentNode.nexts.Count == 0)
            {
                dialogueUI.ShowCloseButtonDialog();
            }
        }
    }
}