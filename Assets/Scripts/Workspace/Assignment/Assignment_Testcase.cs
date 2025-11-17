using System.Linq;
using AssignmentSystem.Services;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;

namespace Assignment
{
    public class Assignment_Testcase
    {
        private IAssignment assignment;

        [SetUp]
        public void Setup()
        {
            // Reset static state before each test
            AssignmentDebugConsole.Clear();

            // Use StudentSolution as the test subject
            assignment = new StudentSolution();
        }

        [TearDown]
        public void Teardown()
        {
            if (assignment is MonoBehaviour)
            {
                MonoBehaviour.DestroyImmediate(assignment as MonoBehaviour);
            }
        }

        #region Lecture

        [Category("Lecture")]
        [Test]
        public void Test_LCT01_StackSyntax()
        {
            assignment.LCT01_StackSyntax();
            string expected = "Count: 3\nPopped: 3\nPeek: 2\nCount after peek: 2";
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        [Category("Lecture")]
        [Test]
        public void Test_LCT02_QueueSyntax()
        {
            assignment.LCT02_QueueSyntax();
            string expected = "Count: 3\nDequeue: 1\nPeek: 2\nCount after dequeue: 2";
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        [Category("Lecture")]
        [Test]
        public void Test_LCT03_ActionStack()
        {
            assignment.LCT03_ActionStack();
            string expected = "Executing Action 3\nExecuting Action 2\nExecuting Action 1";
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        [Category("Lecture")]
        [Test]
        public void Test_LCT04_ActionQueue()
        {
            assignment.LCT04_ActionQueue();
            string expected = "Executing Action 1\nExecuting Action 2\nExecuting Action 3";
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        #endregion

        #region Assignment

        [Category("Assignment")]
        [TestCase("hello", "olleh", TestName = "ASN01_ReverseString_Hello", Description = "Reverse hello")]
        [TestCase("world", "dlrow", TestName = "ASN01_ReverseString_World", Description = "Reverse world")]
        [TestCase("a", "a", TestName = "ASN01_ReverseString_Single", Description = "Reverse single char")]
        [TestCase("", "", TestName = "ASN01_ReverseString_Empty", Description = "Reverse empty string")]
        [TestCase("12345", "54321", TestName = "ASN01_ReverseString_Numbers", Description = "Reverse numbers")]
        public void Test_ASN01_ReverseString(string str, string expected)
        {
            assignment.ASN01_ReverseString(str);
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        [Category("Assignment")]
        [TestCase("radar", "is a palindrome", TestName = "ASN02_StackPalindrome_Radar", Description = "Radar is palindrome")]
        [TestCase("hello", "is not a palindrome", TestName = "ASN02_StackPalindrome_Hello", Description = "Hello is not palindrome")]
        [TestCase("a", "is a palindrome", TestName = "ASN02_StackPalindrome_Single", Description = "Single char is palindrome")]
        [TestCase("", "is a palindrome", TestName = "ASN02_StackPalindrome_Empty", Description = "Empty string is palindrome")]
        [TestCase("aba", "is a palindrome", TestName = "ASN02_StackPalindrome_Aba", Description = "Aba is palindrome")]
        [TestCase("abcba", "is a palindrome", TestName = "ASN02_StackPalindrome_Abcba", Description = "Abcba is palindrome")]
        [TestCase("ab", "is not a palindrome", TestName = "ASN02_StackPalindrome_Ab", Description = "Ab is not palindrome")]
        [TestCase("aa", "is a palindrome", TestName = "ASN02_StackPalindrome_Aa", Description = "Aa is palindrome")]
        public void Test_ASN02_StackPalindrome(string str, string expected)
        {
            assignment.ASN02_StackPalindrome(str);
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        #endregion

        #region Extra

        [Category("Extra")]
        [TestCase("()", "Balanced", TestName = "EX01_ParenthesesChecker_BalancedSimple", Description = "Simple balanced")]
        [TestCase("([])", "Balanced", TestName = "EX01_ParenthesesChecker_BalancedMixed", Description = "Mixed balanced")]
        [TestCase("{[()]}", "Balanced", TestName = "EX01_ParenthesesChecker_BalancedNested", Description = "Nested balanced")]
        [TestCase("", "Balanced", TestName = "EX01_ParenthesesChecker_Empty", Description = "Empty balanced")]
        [TestCase("(", "Unbalanced", TestName = "EX01_ParenthesesChecker_UnbalancedOpen", Description = "Unbalanced open")]
        [TestCase(")", "Unbalanced", TestName = "EX01_ParenthesesChecker_UnbalancedClose", Description = "Unbalanced close")]
        [TestCase("(]", "Unbalanced", TestName = "EX01_ParenthesesChecker_Mismatch", Description = "Mismatch")]
        [TestCase("([)]", "Unbalanced", TestName = "EX01_ParenthesesChecker_Crossed", Description = "Crossed")]
        [TestCase("((", "Unbalanced", TestName = "EX01_ParenthesesChecker_MultipleOpen", Description = "Multiple open")]
        [TestCase("))", "Unbalanced", TestName = "EX01_ParenthesesChecker_MultipleClose", Description = "Multiple close")]
        public void Test_EX01_ParenthesesChecker(string str, string expected)
        {
            assignment.EX01_ParenthesesChecker(str);
            string actual = AssignmentDebugConsole.GetOutput().Trim();
            TestUtils.AssertMultilineEqual(expected, actual);
        }

        #endregion
    }

    public class TestUtils
    {
        internal static void AssertMultilineEqual(string expected, string actual, string message = null)
        {
            string normExpected = expected.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            string normActual = actual.Replace("\r\n", "\n").Replace("\r", "\n").Trim();
            if (string.IsNullOrEmpty(message))
            {
                message = $"Expected output:\n{normExpected}\n----\nActual output:\n{normActual}";
            }
            Assert.AreEqual(normExpected, normActual, message);
        }
    }
}
