using System;
using UnityEngine;

namespace Collect.Domain.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        public event Action<int> OnScoreIncreased;
        public int Score { get; private set; }

        public void Add(int value = 1)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value should be at least 1");
            }

            Score += value;
            OnScoreIncreased?.Invoke(Score);
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}
