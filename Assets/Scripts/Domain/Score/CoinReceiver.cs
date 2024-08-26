using System;
using Collect.Domain.Entities;
using Collect.Domain.Pooling;
using UnityEngine;

namespace Collect.Domain.Score
{
	public class CoinReceiver : MonoBehaviour
	{
		private ObjectPool<Entity> _pool;

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out Coin coin))
			{
				_pool.Pool(coin);
				OnCoinReceived?.Invoke(coin);
			}
		}

		public void Construct(ObjectPool<Entity> pool)
		{
			_pool = pool;
		}

		public event Action<Coin> OnCoinReceived;
	}
}