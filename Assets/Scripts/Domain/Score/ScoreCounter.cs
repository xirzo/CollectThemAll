using System;
using UnityEngine;

namespace Collect.Domain.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        public event Action<int> OnScoreIncreased;
        
        private int _score;

        public void Add(int value = 1)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value should be at least 1");
            }

            _score += value;
            OnScoreIncreased?.Invoke(_score);
        }

        public void Reset()
        {
            _score = 0;
        }
    }
}
