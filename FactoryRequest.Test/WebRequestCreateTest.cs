namespace FactoryRequest.Test {
    using System;
    using System.Net;
    using NUnit.Framework;

    [TestFixture]
    public class WebRequestCreateTest {
        [Test]
        public void CreateHttpWebRequestTestCase() {
            var creator = new WebRequestCreate();

            var http = creator.Create(new Uri("http://localhost/"));

            Assert.IsNotNull(http);
            Assert.IsInstanceOfType(typeof(HttpWebRequest), http);
            Assert.IsNotInstanceOfType(typeof(FakeRequest), http);
            TestHelper.IsValidRequestObject(http as HttpWebRequest);
        }

        [Test]
        public void CreateFakeRequestTestCase() {
            string url = "http://localhost.mocked/";
            Factory.Register(url, "c#");
            var creator = new WebRequestCreate();
            var http = creator.Create(new Uri(url));

            Assert.IsNotNull(http);
            Assert.IsInstanceOfType(typeof(FakeRequest), http);
            TestHelper.IsValidRequestObject(http as FakeRequest);
        }
    }
}