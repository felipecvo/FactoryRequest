namespace FactoryRequest.Test {
    using System;
    using System.Net;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class FakeRequestTest {

        [Test]
        public void TestConstructor() {
            var request = FakeRequest.New(new Uri("http://olutador.com"));

            TestHelper.IsValidRequestObject(request);
        }
    }
}

