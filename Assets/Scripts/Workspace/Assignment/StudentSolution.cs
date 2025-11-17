using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = AssignmentSystem.Services.AssignmentDebugConsole;

namespace Assignment
{
    public class StudentSolution : IAssignment
    {
        class Action
        {
            public string Name;
            public int Value;
        }

        #region Lecture

        public void LCT01_StackSyntax()
        {
            throw new NotImplementedException();
        }

        public void LCT02_QueueSyntax()
        {
            throw new NotImplementedException();
        }

        public void LCT03_ActionStack()
        {
            Action action1 = new Action { Name = "Action 1" };
            Action action2 = new Action { Name = "Action 2" };
            Action action3 = new Action { Name = "Action 3" };
            throw new NotImplementedException();
        }

        public void LCT04_ActionQueue()
        {
            Action action1 = new Action { Name = "Action 1" };
            Action action2 = new Action { Name = "Action 2" };
            Action action3 = new Action { Name = "Action 3" };
            throw new NotImplementedException();
        }

        #endregion

        #region Assignment

        public void ASN01_ReverseString(string str)
        {
            throw new NotImplementedException();
        }

        public void ASN02_StackPalindrome(string str)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Extra

        public void EX01_ParenthesesChecker(string str)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
