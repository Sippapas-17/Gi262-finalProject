using UnityEngine;
using TMPro; // ต้องมีถ้าใช้ TextMeshPro
using Solution; // <--- สำคัญ: เพื่อรู้จัก PlayerScore

namespace Solution // <--- ต้องใส่
{
    public class UIPlayerScore : MonoBehaviour
    {
        public TMP_Text Text;

        // เมธอดนี้ต้องเป็น public และรับ PlayerScore ที่อยู่ใน Solution namespace
        public void SetUpTextScore(PlayerScore txt)
        {
            Text.text = txt.playerName + " " + txt.score;
        }
    }
}