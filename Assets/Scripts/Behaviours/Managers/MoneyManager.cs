using System;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class MoneyManager : Singleton<MoneyManager>
    {
        private int _money;
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
            _money = 0;
            Validate();
        }

        private void Update()
        {
            _text.text = _money.ToString();
        }

        private void Validate()
        {
            if (_text == null)
                throw new ArgumentException("No text is found");
        }


        public void AddMoney(int score)
        {
            if (GameManager.Instance.DoublePointsEnable)
                score *= 2;
            _money += score;
        }

        public void SubMoney(int score)
        {
            if (CanSubtractMoney(score))
                _money -= score;
            else
                throw new ArgumentException("it is not possible to subtract the score ");
        }

        private bool CanSubtractMoney(int score)
        {
            return _money - score >= 0;
        }
    }
}