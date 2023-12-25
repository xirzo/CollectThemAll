using System;
using UnityEngine;

namespace Collect.Domain.Score
{
    public class ScoreCounter : MonoBehaviour
    {
        public int Score { get; private set; }

        public void Add(int value = 1)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Value should be at least 1");
            }

            Score += value;
        }

        public void Reset()
        {
            Score = 0;
        }
    }
}
