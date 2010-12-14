namespace FactoryRequest.Test {
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class FakeResponseTest {
        [Test]
        public void CreateNewInstance() {
            var ms = new MemoryStream(ASCIIEncoding.ASCII.GetBytes("teste"));
            var actual = FakeResponse.New(ms);

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Headers);
            Assert.IsNotNull(actual.StatusCode);
        }
    }
}

