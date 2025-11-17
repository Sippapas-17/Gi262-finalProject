namespace Solution
{
    // คลาสนี้ไม่ต้องติดกับ GameObject
    // ใช้สำหรับเก็บ "คำตอบ" ของเกม
    public static class GameState
    {
        // Key 1
        public static string KeyPart1_Location = ""; // (จะเก็บชื่อเช่น "Chest" หรือ "Box")
        public static bool KeyPart1_Found = false;

        // Key 2
        public static string KeyPart2_Location = ""; // (จะเก็บชื่อเช่น "Statue" หรือ "Coffin")
        public static bool KeyPart2_Found = false;
    }
}