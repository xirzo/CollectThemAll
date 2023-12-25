using System;
using Collect.Entities.Coin;
using UnityEngine;

namespace Collect.Domain.Score
{
    public class CoinReceiver : MonoBehaviour
    {
        public event Action OnCoinReceived;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Coin coin))
            {
                OnCoinReceived?.Invoke();
            }
        }
    }
}
