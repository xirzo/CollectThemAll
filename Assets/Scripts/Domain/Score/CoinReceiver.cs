using System;
using Collect.Domain.Entities;
using UnityEngine;

namespace Collect.Domain.Score
{
	public class CoinReceiver : MonoBehaviour
	{
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out Coin coin)) OnCoinReceived?.Invoke();
		}

		public event Action OnCoinReceived;
	}
}