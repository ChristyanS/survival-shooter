using System;
using UnityEngine;
using UnityEngine.UI;

namespace Behaviours.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public static int Score;
        private Text _text;


        private void Start()
        {
            _text = GetComponent<Text>();
            Score = 0;
            Validate();
        }

        private void Update()
        {
            _text.text = "Score: " + Score;
        }

        private void Validate()
        {
            if (_text == null)
                throw new ArgumentException("No text is found");
        }
    }
}