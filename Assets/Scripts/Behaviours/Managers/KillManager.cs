using System;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class KillManager : Singleton<KillManager>
    {
        private int _kills;
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
            _kills = 0;
            Validate();
        }

        private void Update()
        {
            _text.text = _kills.ToString();
        }

        public void AddKill()
        {
            _kills++;
        }

        private void Validate()
        {
            if (_text == null)
                throw new ArgumentException("No text is found");
        }
    }
}