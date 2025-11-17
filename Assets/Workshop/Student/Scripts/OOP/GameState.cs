using UnityEngine;

namespace Solution // <--- 1. ต้องครอบด้วย Namespace
{
    // คลาสนี้ไม่ต้องติดกับ GameObject
    // "static" หมายถึงเป็นของส่วนกลาง
    public static class GameState
    {
        // 2. ตัวแปรนี้จะเก็บ "ชื่อ" ของที่ซ่อนกุญแจ
        public static string Key1_LocationName = "";

        // 3. ตัวแปรนี้จะเช็คว่าผู้เล่นเจอกุญแจแล้ว
        public static bool Key1_Found = false;
    }
}