using UnityEngine;

namespace Collect.Domain.Score
{
	[RequireComponent(typeof(ScoreCounter))]
	[RequireComponent(typeof(CoinReceiver))]
	public class CoinCounter : MonoBehaviour
	{
		private ScoreCounter _counter;
		private CoinReceiver _receiver;

		private void Awake()
		{
			TryGetComponent(out _counter);
			TryGetComponent(out _receiver);
		}

		private void Start()
		{
			_receiver.OnCoinReceived += coin => _counter.Add();
		}

		private void OnDestroy()
		{
			_receiver.OnCoinReceived -= coin => _counter.Add();
		}
	}
}