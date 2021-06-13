using UnityEngine;

namespace Behaviours.Utils
{
    public class BehaviourUtils : MonoBehaviour
    {
        public static void DestroyAllChilds(Transform transformLocal)
        {
            foreach (Transform child in transformLocal)
            {
                Destroy(child.gameObject);
            }
        }
        
    }
}