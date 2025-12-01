using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Solution;

namespace Solution 
{
    public class Leaderboard : MonoBehaviour
    {
        private List<PlayerScore> scores = new List<PlayerScore>();
        public GameObject UIScore;
        public Transform UiParent;

        void Awake()
        {
            RecordScore(new PlayerScore("Alice", 100));
            RecordScore(new PlayerScore("Bob", 50));
            RecordScore(new PlayerScore("Charlie", 75));
            RecordScore(new PlayerScore("David", 25));
            RecordScore(new PlayerScore("Eve", 125));
            RecordScore(new PlayerScore("Frank", 150));
            RecordScore(new PlayerScore("Lily", 300));
            RecordScore(new PlayerScore("Grace", 175));
            RecordScore(new PlayerScore("Oscar", 375));
            RecordScore(new PlayerScore("Ivan", 225));
            RecordScore(new PlayerScore("Heidi", 200));
            RecordScore(new PlayerScore("Judy", 250));
            RecordScore(new PlayerScore("Kevin", 275));
            RecordScore(new PlayerScore("Nina", 350));
            RecordScore(new PlayerScore("Mona", 325));
        }

        public void RecordScore(PlayerScore score)
        {
            int index = -1;
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].playerName == score.playerName)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                scores.RemoveAt(index);
            }

            index = -1;
            int left = 0;
            int right = scores.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (scores[mid].score == score.score)
                {
                    index = mid;
                    break;
                }
                else if (scores[mid].score < score.score) 
                {
                    right = mid - 1; 
                }
                else
                {
                    left = mid + 1;
                }
            }

            if (index == -1)
            {
                index = left;
            }
            scores.Insert(index, score);
        }

        public void PrintScores()
        {
            string allScores = scores.Aggregate("", (acc, score) => acc + score.score.ToString() + ",");
            Debug.Log(allScores);
        }

        public void ShowleaderBoard()
        {
            foreach (var score in scores)
            {
                UIPlayerScore uIScore = Instantiate(UIScore, UiParent).GetComponent<UIPlayerScore>();
                uIScore.SetUpTextScore(score);
            }
        }
    }
}