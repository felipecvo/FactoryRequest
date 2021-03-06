namespace FactoryRequest.Test {
    using System;
    using System.IO;
    using System.Net;
    using NUnit.Framework;

    [TestFixture()]
    public class Test {
        [Test()]
        public void TestCase() {
            var url = "http://www.facebook.com/";
            
            Factory.Register(url, "oi");
            
            var request = WebRequest.Create(url);
            
            var response = request.GetResponse();
            
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<FakeResponse>(response);
        }

        [Test()]
        public void TestCase2() {
            Factory.Register("https://www.facebook.com", "oi");
            var request = WebRequest.Create("https://www.facebook.com");

            var response = request.GetResponse();

            string responseText;
            using(var stream = new StreamReader(response.GetResponseStream())) {
                responseText = stream.ReadToEnd();
            }

            Assert.IsNotNull(response);
            Assert.IsInstanceOf<FakeResponse>(response);
            Assert.AreEqual("oi", responseText);
        }

        [Test()]
        public void ShouldBeAbleToDoRealRequests() {
            var request = WebRequest.Create("http://www.google.com");
            Assert.IsInstanceOf<HttpWebRequest>(request);
            var response = request.GetResponse();
            Assert.IsNotNull(response);
            Assert.IsInstanceOf<HttpWebResponse>(response);
        }
    }
}

