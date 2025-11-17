using Solution;
using UnityEngine;

namespace Solution
{
    public class OOPExit : Identity
    {
        public Leaderboard leaderboard;
        public string ItemToOpen = "MasterKey"; // เปลี่ยนเป็น MasterKey
        public int ItemAmountToOpen = 1;        // ต้องการ 1 ดอก

        public override bool Hit()
        {
            // ตรวจสอบว่าผู้เล่นมีไอเท็มที่ต้องการหรือไม่
            bool IsHasItemAmount = mapGenerator.player.inventory.HasItem(ItemToOpen, ItemAmountToOpen);

            if (IsHasItemAmount)
            {
                mapGenerator.player.inventory.UseItem(ItemToOpen, ItemAmountToOpen);

                // *** Logic การจบเกม ***
                leaderboard.gameObject.SetActive(true);
                Debug.Log("You win! The Exit is open.");

                int scorereceived = CalculateScore(); // เรียกใช้เมธอดคำนวณคะแนนใหม่
                string PlayerName = mapGenerator.player.Name;

                leaderboard.RecordScore(new PlayerScore(PlayerName, scorereceived));
                leaderboard.PrintScores();
                leaderboard.ShowleaderBoard();

                return true;
            }
            else
            {
                Debug.Log($"Need Item {ItemToOpen} x {ItemAmountToOpen} to Open the exit.");
                return false;
            }
        }

        // เมธอดคำนวณคะแนน (ปรับปรุงใหม่ให้คิดจากเวลาที่ใช้น้อยลง)
        int CalculateScore()
        {
            // คะแนนคำนวณจาก (100,000 / เวลาที่ใช้ (วินาที)) เพื่อให้เล่นเร็วได้คะแนนเยอะ
            // ป้องกันการหารด้วยศูนย์: Time.time จะมากกว่า 0 เสมอ
            int score = (int)(100000 / Time.time);
            return score;
        }
    }
}