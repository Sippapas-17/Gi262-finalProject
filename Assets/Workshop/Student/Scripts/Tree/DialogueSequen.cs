using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solution; // เพื่อให้รู้จัก OOPPlayer, Inventory

// NOTE: คลาส DialogueNode และ DialogueTree ถูกประกาศในไฟล์นี้ 
namespace Solution
{
    // *** 1. คลาส DialogueNode ***
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

    // *** 2. คลาส DialogueTree ***
    public class DialogueTree
    {
        public DialogueNode root;

        public DialogueTree(DialogueNode root)
        {
            this.root = root;
        }
    }


    // *** 3. คลาส DialogueSequen หลัก ***
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
            // --- 1. Node สำหรับ NPC ---
            DialogueNode greeting = new DialogueNode("Ah, traveler! I know you seek the way out. Find the Master Key.");
            allNodes.Add("greeting", greeting);

            DialogueNode clue1 = new DialogueNode("The first key part is hidden where 'Time stands still' but its eye is cold blue.");
            allNodes.Add("clue1", clue1);

            DialogueNode clue2 = new DialogueNode("The final part of the key is guarded by the 'Three-headed beast'. A Firestorm might weaken its bond.");
            allNodes.Add("clue2", clue2);

            DialogueNode farewell = new DialogueNode("You have proven yourself. The Master Key will open the Final Gate.");
            allNodes.Add("farewell", farewell);

            // ******************************************************
            // ******* 2. เพิ่ม Node สำหรับวัตถุโต้ตอบ (Interactable Objects) *******
            // ******************************************************

            // หีบ (Chest)
            DialogueNode chestNode = new DialogueNode("Perhaps the key is in what treasure hunters seek most?");
            allNodes.Add("Chest", chestNode); // <--- Key ต้องตรงกับที่ InteractableObject เรียกใช้

            // กล่อง (Box)
            DialogueNode boxNode = new DialogueNode("In many games, people love to break these.");
            allNodes.Add("Box", boxNode);

            // ถังหมัก (Fermenter)
            DialogueNode fermenterNode = new DialogueNode("This is often used for fermenting or storing goods.");
            allNodes.Add("Fermenter", fermenterNode);

            // โลงศพ/หลุมศพ (Coffin)
            DialogueNode coffinNode = new DialogueNode("A final resting place.");
            allNodes.Add("Coffin", coffinNode);

            // รูปปั้น (Statue)
            DialogueNode statueNode = new DialogueNode("People build these to remember someone or something.");
            allNodes.Add("Statue", statueNode);

            // ******************************************************

            // กำหนด Root Node (สำหรับ NPC)
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

        // เมธอดสำหรับจัดการเมื่อผู้เล่น "เลือก" ตัวเลือก
        public void SelectChoice(int index)
        {
            // ตรวจสอบว่ามีตัวเลือกให้เลือกหรือไม่
            if (currentNode == null || currentNode.nexts.Count == 0 || index >= currentNode.nexts.Count)
            {
                // ถ้าไม่มีตัวเลือก หรือ index ผิดพลาด ให้ปิด Dialogue
                dialogueUI.HideDialogue();
                return;
            }

            // 1. หา Key ของตัวเลือกที่ผู้เล่นกด
            var choiceKeys = new List<string>(currentNode.nexts.Keys);
            string selectedKey = choiceKeys[index];

            // 2. ไปยัง Node ถัดไป
            currentNode = currentNode.nexts[selectedKey];

            // 3. แสดง Node ถัดไป
            dialogueUI.ShowDialogue(currentNode);

            // 4. ตรวจสอบว่า Node ใหม่นี้มีตัวเลือกต่อไปหรือไม่
            if (currentNode.nexts.Count == 0)
            {
                // ถ้า Node ใหม่นี้ไม่มีตัวเลือก (เป็นจุดจบ) ให้แสดงปุ่มปิด
                dialogueUI.ShowCloseButtonDialog();
            }
        }
    }
}