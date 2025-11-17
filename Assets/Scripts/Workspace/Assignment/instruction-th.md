# Assignment 09: การเรียนรู้ Stack และ Queue สำหรับ Game Development

## 🎯 จุดประสงค์การเรียนรู้

- เรียนรู้การใช้งาน Stack และ Queue พื้นฐาน
- เข้าใจหลักการ LIFO (Last In First Out) และ FIFO (First In First Out)
- นำ Stack และ Queue มาใช้ในการแก้ปัญหาในเกม เช่น การจัดการ Action, Undo/Redo, Event Queue
- วิเคราะห์ประสิทธิภาพและการใช้งานที่เหมาะสมของ Stack และ Queue
- เขียน code ที่มีประสิทธิภาพในการจัดการข้อมูลแบบ linear ด้วย Stack และ Queue

## 📚 โครงสร้างของ Assignment

- **Lecture Methods (4 methods)** - การ implement ฝึกหัด Stack และ Queue พื้นฐาน พร้อมกันในห้องเรียน
- **Assignment Methods (2 methods)** - การประยุกต์ใช้ Stack และ Queue ในสถานการณ์เกม
- **Extra Methods (1 method)** - โจทย์เสริมสำหรับฝึกฝนเพิ่มเติม

---

## 🧠 ความรู้พื้นฐานที่ควรทราบ

### Stack คืออะไร?
Stack คือโครงสร้างข้อมูลแบบ LIFO (Last In First Out) โดยข้อมูลที่ใส่เข้าไปล่าสุดจะถูกนำออกมาก่อน

### Queue คืออะไร?
Queue คือโครงสร้างข้อมูลแบบ FIFO (First In First Out) โดยข้อมูลที่ใส่เข้าไปก่อนจะถูกนำออกมาก่อน

### ใช้ Stack และ Queue ทำอะไรบ้างในเกม?
- **Stack**: Undo/Redo system, Function call stack, Backtracking algorithms, Action history
- **Queue**: Event handling, Turn-based systems, BFS algorithms, Message queues, Loading sequences

### Operations พื้นฐาน
- **Stack**: Push (ใส่), Pop (นำออก), Peek (ดูบนสุด), Count (จำนวน)
- **Queue**: Enqueue (ใส่), Dequeue (นำออก), Peek (ดูหน้า), Count (จำนวน)

---

## Lecture Methods

### 1. LCT01_StackSyntax

**วัตถุประสงค์:** เรียนรู้การใช้งาน Stack พื้นฐาน

**คำอธิบายปัญหา:**
สาธิตการใช้งาน Stack โดยการ Push ข้อมูลเข้า Stack, ตรวจสอบจำนวน, Pop ข้อมูลออก, และ Peek ข้อมูลบนสุด

ในเกม Stack มักใช้ในการ:
- จัดการ Action history สำหรับ Undo/Redo
- จัดการ Function call ใน scripting systems
- จัดการ State ใน game flow

**Method Signature:**
```csharp
void LCT01_StackSyntax()
```

**Logic ที่ต้อง implement:**
1. สร้าง Stack<int>
2. Push 1, 2, 3 ตามลำดับ
3. แสดง Count
4. Pop และแสดงค่าที่ Pop ออกมา
5. Peek และแสดงค่าบนสุด พร้อม Count หลัง Peek

**Test Cases:**
1. **Expected Output:**
   ```
   Count: 3
   Popped: 3
   Peek: 2
   Count after peek: 2
   ```

### 2. LCT02_QueueSyntax

**วัตถุประสงค์:** เรียนรู้การใช้งาน Queue พื้นฐาน

**คำอธิบายปัญหา:**
สาธิตการใช้งาน Queue โดยการ Enqueue ข้อมูลเข้า Queue, ตรวจสอบจำนวน, Dequeue ข้อมูลออก, และ Peek ข้อมูลหน้า

ในเกม Queue มักใช้ในการ:
- จัดการ Event queue ใน game loop
- จัดการ Turn-based combat systems
- จัดการ Loading sequences
- จัดการ Message passing ระหว่าง systems

**Method Signature:**
```csharp
void LCT02_QueueSyntax()
```

**Logic ที่ต้อง implement:**
1. สร้าง Queue<int>
2. Enqueue 1, 2, 3 ตามลำดับ
3. แสดง Count
4. Dequeue และแสดงค่าที่ Dequeue ออกมา
5. Peek และแสดงค่าหน้า พร้อม Count หลัง Dequeue

**Test Cases:**
1. **Expected Output:**
   ```
   Count: 3
   Dequeue: 1
   Peek: 2
   Count after dequeue: 2
   ```

### 3. LCT03_ActionStack

**วัตถุประสงค์:** เรียนรู้การใช้งาน Stack กับ Object

**คำอธิบายปัญหา:**
สาธิตการใช้งาน Stack ในการจัดการ Action objects โดย Push actions เข้า Stack แล้ว Pop ออกมา execute ตามลำดับ LIFO

ในเกมสามารถใช้ในการ:
- จัดการ Action stack สำหรับ command patterns
- Implement Undo system โดย pop actions ออกมาคืนค่า
- จัดการ State transitions ใน game flow

**Method Signature:**
```csharp
void LCT03_ActionStack()
```

**Logic ที่ต้อง implement:**
1. สร้าง Action class ที่มี Name property
2. สร้าง Action objects 3 ตัว
3. Push เข้า Stack ตามลำดับ
4. Pop และ execute (แสดงชื่อ) จน Stack ว่าง

**Test Cases:**
1. **Expected Output:**
   ```
   Executing Action 3
   Executing Action 2
   Executing Action 1
   ```

### 4. LCT04_ActionQueue

**วัตถุประสงค์:** เรียนรู้การใช้งาน Queue กับ Object

**คำอธิบายปัญหา:**
สาธิตการใช้งาน Queue ในการจัดการ Action objects โดย Enqueue actions เข้า Queue แล้ว Dequeue ออกมา execute ตามลำดับ FIFO

ในเกมสามารถใช้ในการ:
- จัดการ Event queue ใน real-time systems
- Implement Turn-based action systems
- จัดการ Animation sequences
- จัดการ Network message queues

**Method Signature:**
```csharp
void LCT04_ActionQueue()
```

**Logic ที่ต้อง implement:**
1. ใช้ Action class เดียวกับ LCT03
2. สร้าง Action objects 3 ตัว
3. Enqueue เข้า Queue ตามลำดับ
4. Dequeue และ execute (แสดงชื่อ) จน Queue ว่าง

**Test Cases:**
1. **Expected Output:**
   ```
   Executing Action 1
   Executing Action 2
   Executing Action 3
   ```

---

## Assignment Methods

### ASN01_ReverseString

**วัตถุประสงค์:** ใช้ Stack ในการ Reverse String

**คำอธิบายปัญหา:**
เขียนโปรแกรมที่รับ String และแสดงผลเป็น String ที่ถูก Reverse โดยใช้ Stack

ในเกมสามารถใช้ในการ:
- สร้าง Effect การพลิกคำหรือข้อความ
- Implement Text animation ที่ reverse
- จัดการ String manipulation ใน puzzle games
- สร้าง Password หรือ code ที่ต้อง reverse เพื่อใช้งาน

**Method Signature:**
```csharp
void ASN01_ReverseString(string str)
```

**Logic ที่ต้อง implement:**
1. สร้าง Stack<string>
2. Push แต่ละ character ของ str เข้า Stack
3. Pop ออกมาทีละตัวและ concat เป็น string ใหม่
4. แสดงผล string ที่ reverse แล้ว

**Test Cases:**
1. **Input:** str = "hello"
   **Expected Output:**
   ```
   olleh
   ```

2. **Input:** str = "world"
   **Expected Output:**
   ```
   dlrow
   ```

3. **Input:** str = "a"
   **Expected Output:**
   ```
   a
   ```

4. **Input:** str = ""
   **Expected Output:**
   ```
   
   ```

5. **Input:** str = "12345"
   **Expected Output:**
   ```
   54321
   ```

### ASN02_StackPalindrome

**วัตถุประสงค์:** ใช้ Stack ในการตรวจสอบ Palindrome

**คำอธิบายปัญหา:**
เขียนโปรแกรมที่รับ String และตรวจสอบว่าเป็น Palindrome หรือไม่โดยใช้ Stack

ในเกมสามารถใช้ในการ:
- สร้าง Puzzle ที่ต้องหาคำที่เป็น palindrome
- ตรวจสอบ Magic words หรือ incantations
- Implement Word games ที่ให้ bonus สำหรับ palindrome
- ตรวจสอบ Symmetry ใน game patterns

**Method Signature:**
```csharp
void ASN02_StackPalindrome(string str)
```

**Logic ที่ต้อง implement:**
1. ใช้ logic เดียวกับ ASN01 เพื่อสร้าง reversed string
2. เปรียบเทียบ str กับ reversed string
3. แสดง "is a palindrome" หรือ "is not a palindrome"

**Test Cases:**
1. **Input:** str = "radar"
   **Expected Output:**
   ```
   is a palindrome
   ```

2. **Input:** str = "hello"
   **Expected Output:**
   ```
   is not a palindrome
   ```

3. **Input:** str = "a"
   **Expected Output:**
   ```
   is a palindrome
   ```

4. **Input:** str = ""
   **Expected Output:**
   ```
   is a palindrome
   ```

5. **Input:** str = "aba"
   **Expected Output:**
   ```
   is a palindrome
   ```

6. **Input:** str = "abcba"
   **Expected Output:**
   ```
   is a palindrome
   ```

7. **Input:** str = "ab"
   **Expected Output:**
   ```
   is not a palindrome
   ```

8. **Input:** str = "aa"
   **Expected Output:**
   ```
   is a palindrome
   ```

---

## Extra Methods

### EX01_ParenthesesChecker

**วัตถุประสงค์:** ใช้ Stack ในการตรวจสอบ Balanced Parentheses

**คำอธิบายปัญหา:**
เขียนโปรแกรมที่รับ String ที่ประกอบด้วย parentheses ต่างๆ และตรวจสอบว่ามีการเปิด-ปิด ที่สมดุลหรือไม่โดยใช้ Stack

ในเกมสามารถใช้ในการ:
- ตรวจสอบ Syntax ใน custom scripting languages
- Implement Code validation ใน programming games
- ตรวจสอบ Bracket matching ใน puzzle games
- Validate mathematical expressions หรือ formulas

**Method Signature:**
```csharp
void EX01_ParenthesesChecker(string str)
```

**Logic ที่ต้อง implement:**
1. สร้าง Stack<char>
2. วนลูปแต่ละ character ใน str
3. ถ้าเป็น opening bracket (, [, { ให้ Push เข้า Stack
4. ถ้าเป็น closing bracket ), ], } ให้ Pop จาก Stack และตรวจสอบ matching
5. ถ้า Stack ว่างหลังวนลูป แสดง "Balanced" มิฉะนั้น "Unbalanced"

**Test Cases:**
1. **Input:** str = "()"
   **Expected Output:**
   ```
   Balanced
   ```

2. **Input:** str = "([])"
   **Expected Output:**
   ```
   Balanced
   ```

3. **Input:** str = "{[()]}"
   **Expected Output:**
   ```
   Balanced
   ```

4. **Input:** str = ""
   **Expected Output:**
   ```
   Balanced
   ```

5. **Input:** str = "("
   **Expected Output:**
   ```
   Unbalanced
   ```

6. **Input:** str = ")"
   **Expected Output:**
   ```
   Unbalanced
   ```

7. **Input:** str = "(]"
   **Expected Output:**
   ```
   Unbalanced
   ```

8. **Input:** str = "([)]"
   **Expected Output:**
   ```
   Unbalanced
   ```

9. **Input:** str = "(("
   **Expected Output:**
   ```
   Unbalanced
   ```

10. **Input:** str = "))"
    **Expected Output:**
    ```
    Unbalanced
    ```

---
