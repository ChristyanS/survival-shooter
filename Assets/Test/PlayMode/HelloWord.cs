using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.PlayMode
{
    public class HelloWord
    {
        [UnityTest]
        public IEnumerator HelloWordWithEnumeratorPasses()
        {
            var gameObject = new GameObject();
            var helloWorld = gameObject.AddComponent<HelloWorld>();
            yield return null;
            Assert.AreEqual(new Vector3(0, 0, 0), helloWorld.transform.position);
        }
    }
}