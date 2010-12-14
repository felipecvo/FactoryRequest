namespace FactoryRequest.Test {
    using System;
    using System.Net;
    using System.Reflection;
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class FakeRequestTest {
        [TestFixtureSetUp]
        public void SetUp() {
            Factory.Register("http://olutador.com", "fight");
        }

        [Test]
        public void TestConstructor() {
            var request = FakeRequest.New(new Uri("http://olutador.com"));

            TestHelper.IsValidRequestObject(request);
        }

        [Test]
        public void TestResponse() {
            var request = FakeRequest.New(new Uri("http://olutador.com"));

            var response = request.GetResponse();

            Assert.IsInstanceOf<FakeResponse>(response);
        }

        [Test]
        public void TestRequestStream() {
            var request = FakeRequest.New(new Uri("http://olutador.com"));
            request.Method = "POST";
            var buffer = Encoding.ASCII.GetBytes("should be able to write");
            using(var stream = request.GetRequestStream()) {
                stream.Write(buffer, 0, buffer.Length);
            }

            Assert.True(true);
        }

        [Test]
        public void ShouldThrowCannotSendDataThrowGet() {
            Assert.Throws<System.Net.ProtocolViolationException>(delegate() {
                var request = FakeRequest.New(new Uri("http://olutador.com"));

                var buffer = Encoding.ASCII.GetBytes("should not be able to write");
                using(var stream = request.GetRequestStream()) {
                    stream.Write(buffer, 0, buffer.Length);
                }
            });
        }
    }
}