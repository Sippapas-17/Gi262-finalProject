using Solution;
using UnityEngine;

namespace Solution
{
    public class OOPExit : Identity
    {
        public Leaderboard leaderboard;
        public string ItemToOpen = "MasterKey"; 
        public int ItemAmountToOpen = 1;       

        public override bool Hit()
        {
            bool IsHasItemAmount = mapGenerator.player.inventory.HasItem(ItemToOpen, ItemAmountToOpen);

            if (IsHasItemAmount)
            {
                mapGenerator.player.inventory.UseItem(ItemToOpen, ItemAmountToOpen);

                leaderboard.gameObject.SetActive(true);
                Debug.Log("You win! The Exit is open.");

                int scorereceived = CalculateScore(); 
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

        int CalculateScore()
        {
            int score = (int)(100000 / Time.time);
            return score;
        }
    }
}