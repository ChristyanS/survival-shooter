using NUnit.Framework;

namespace Test.EditMode
{
    public class HelloWorldTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void HelloWorldSimplePasses()
        {
            var helloWorld = new HelloWorld();
            Assert.IsTrue(helloWorld.Test());
        }
    }
}