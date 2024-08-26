using Collect.Domain.Score;
using TMPro;
using UnityEngine;

namespace Collect.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreCounterText : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _counter;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            TryGetComponent(out _text);
        }

        private void SetScoreText(int score)
        {
            _text.text = score.ToString();
        }

        private void Start()
        {
            _counter.OnScoreIncreased += SetScoreText;
        }

        private void OnDestroy()
        {
            _counter.OnScoreIncreased -= SetScoreText;
        }
    }
}
