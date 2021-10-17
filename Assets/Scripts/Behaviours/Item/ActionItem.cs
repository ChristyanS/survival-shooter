using System;
using Behaviours.Actions;
using Enums;
using UnityEngine;

namespace Behaviours.Item
{
    public class ActionItem : Item
    {
        public override ItemType ItemType => ItemType.Action;

        public override void Execute(GameObject other = null)
        {
            var instance =
                Instantiate(Loot, new Vector3(),
                    new Quaternion()); //todo deve ter alguma forma de fazer isso aqui sem ter que instanciar o objecto, para eu conseguir dar start em uma corrotina

            GetAction(instance).Execute();
        }

        private static ActionBehaviour GetAction(GameObject other)
        {
            var action = other.gameObject.GetComponent<ActionBehaviour>();

            if (action == null)
                throw new ArgumentException($"No {nameof(action)} object found");

            return action;
        }
    }
}