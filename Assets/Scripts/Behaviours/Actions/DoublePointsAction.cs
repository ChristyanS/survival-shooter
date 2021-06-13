using System.Collections;
using System.Collections.Generic;
using Behaviours.Managers;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class DoublePointsAction : ActionBehaviour
    {
        [SerializeField] [Range(0.1f, 100)] private float time = 10f;
        [SerializeField] private GameObject powerUpImage;
        private GameObject _powerUpPanel;

        public override void Execute()
        {
            StartCoroutine(EnableDoublePoints());
        }

        private IEnumerator EnableDoublePoints()
        {
            _powerUpPanel = GameObject.FindGameObjectWithTag(Tag.PowerUpPanel.ToString());
            GameManager.DoublePointsEnable = true;
            var powerUp = Instantiate(powerUpImage, _powerUpPanel.transform);

            yield return new WaitForSeconds(time);

            GameManager.DoublePointsEnable = false;
            Destroy(gameObject);
            Destroy(powerUp);
        }
    }
}