using Solution;

namespace Solution // <-- CHANGED: ย้ายมาที่ Solution namespace
{
    public class PlayerScore
    {
        public string playerName;
        public int score;

        public PlayerScore(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }
}