using System;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class KillManager : MonoBehaviour
    {
        public static int Kills;
        private Text _text;

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

        private void Validate()
        {
            if (_text == null)
                throw new ArgumentException("No text is found");
        }
    }
}