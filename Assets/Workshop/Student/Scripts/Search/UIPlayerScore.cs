using UnityEngine;
using TMPro; 
using Solution; 

namespace Solution 
{
    public class UIPlayerScore : MonoBehaviour
    {
        public TMP_Text Text;

        public void SetUpTextScore(PlayerScore txt)
        {
            Text.text = txt.playerName + " " + txt.score;
        }
    }
}