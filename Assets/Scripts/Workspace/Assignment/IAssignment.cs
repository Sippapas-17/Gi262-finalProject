using UnityEngine;

namespace Assignment
{
    public interface IAssignment
    {
        #region Lecture

        void LCT01_StackSyntax();
        void LCT02_QueueSyntax();
        void LCT03_ActionStack();
        void LCT04_ActionQueue();

        #endregion

        #region Assignment

        void ASN01_ReverseString(string str);
        void ASN02_StackPalindrome(string str);

        #endregion

        #region Extra
        void EX01_ParenthesesChecker(string str);

        #endregion

    }
}
