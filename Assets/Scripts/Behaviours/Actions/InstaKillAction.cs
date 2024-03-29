using System.Collections;
using Behaviours.Managers;
using Enums;
using UnityEngine;

namespace Behaviours.Actions
{
    public class InstaKillAction : ActionBehaviour
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
            StartCoroutine(EnableInstaKill());
        }

        private IEnumerator EnableInstaKill()
        {
            PowerUpPanel = GameObject.FindGameObjectWithTag(Tag.PowerUpPanel.ToString());
            GameManager.Instance.InstaKillEnable = true;
            var powerUp = Instantiate(powerUpImage, PowerUpPanel.transform);

            yield return new WaitForSeconds(time);

            GameManager.Instance.InstaKillEnable = false;
            Destroy(gameObject);
            Destroy(powerUp);
        }
    }
}