using System;
using System.Collections;
using System.Security.Cryptography;
using Behaviours.Managers;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class InstaKillAction : ActionBehaviour
    {
        [SerializeField] [Range(0.1f, 100)] private float time = 10f;
        [SerializeField] private GameObject powerUpImage;
        private GameObject _instaKillPanel;

        private void Start()
        {
            Validate();
        }

        private void Validate()
        {
            if (_instaKillPanel == null)
                throw new ArgumentException($"No {nameof(_instaKillPanel)} object is found");
            if (powerUpImage == null)
                throw new ArgumentException($"No {nameof(powerUpImage)} object is found");
        }

        public override void Execute()
        {
            StartCoroutine(EnableInstaKill());
        }

        private IEnumerator EnableInstaKill()
        {
            _instaKillPanel = GameObject.FindGameObjectWithTag(Tag.PowerUpPanel.ToString());
            GameManager.InstaKillEnable = true;
            _instaKillPanel.SetActive(true);
            var powerUp = Instantiate(powerUpImage, _instaKillPanel.transform);

            yield return new WaitForSeconds(time);

            GameManager.InstaKillEnable = false;
            _instaKillPanel.SetActive(false);

            Destroy(gameObject);
            Destroy(powerUp);
        }
    }
}