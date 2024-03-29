using System.Collections;
using Behaviours.Managers;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class DoublePointsAction : ActionBehaviour
    {
        [SerializeField] [Range(0.1f, 100)] private float time = 10f;

        [SerializeField] private GameObject powerUpImage;

        public float Time
        {
            get => time;
            set => time = value;
        }

        public GameObject PowerUpImage
        {
            get => powerUpImage;
            set => powerUpImage = value;
        }

        public GameObject PowerUpPanel { get; private set; }


        public override void Execute()
        {
            StartCoroutine(EnableDoublePoints());
        }

        private IEnumerator EnableDoublePoints()
        {
            PowerUpPanel = GameObject.FindGameObjectWithTag(Tag.PowerUpPanel.ToString());
            GameManager.Instance.DoublePointsEnable = true;
            var powerUp = Instantiate(powerUpImage, PowerUpPanel.transform);

            yield return new WaitForSeconds(time);

            GameManager.Instance.DoublePointsEnable = false;
            Destroy(gameObject);
            Destroy(powerUp);
        }
    }
}