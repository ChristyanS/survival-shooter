using System;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        private int _score;
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
            _score = 0;
            Validate();
        }

        private void Update()
        {
            _text.text = "Score: " + _score;
        }

        private void Validate()
        {
            if (_text == null)
                throw new ArgumentException("No text is found");
        }

        public void AddScore(int score)
        {
            if (GameManager.Instance.DoublePointsEnable)
                score *= 2;
            _score += score;
        }

        public void SubScore(int score)
        {
            if (CanSubtractScore(score))
                _score -= score;
            else
                throw new ArgumentException("it is not possible to subtract the score ");
        }

        public bool CanSubtractScore(int score)
        {
            return _score - score >= 0;
        }
    }
}