using System;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class KillManager : Singleton<KillManager>
    {
        private Text _text;
        private int Kills;

        private void Start()
        {
            _text = GetComponent<Text>();
            Kills = 0;
            Validate();
        }

        private void Update()
        {
            _text.text = Kills.ToString();
        }

        public void AddKill()
        {
            Kills++;
        }

        private void Validate()
        {
            if (_text == null)
                throw new ArgumentException("No text is found");
        }
    }
}