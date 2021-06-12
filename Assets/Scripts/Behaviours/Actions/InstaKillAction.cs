using System.Collections;
using Behaviours.Managers;
using UnityEngine;

namespace Behaviours.Actions
{
    public class InstaKillAction : ActionBehaviour
    {
        [SerializeField] [Range(0.1f, 100)] private float time = 10f;

        public override void Execute()
        {
            StartCoroutine(EnableInstaKill());
        }

        private IEnumerator EnableInstaKill()
        {
            GameManager.InstaKillEnable = true;

            yield return new WaitForSeconds(time);

            GameManager.InstaKillEnable = false;
            Destroy(gameObject);
        }
    }
}