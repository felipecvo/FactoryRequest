namespace FactoryRequest.Test {
    using System;
    using System.Net;
    using System.Reflection;
    using NUnit.Framework;

    public static class TestHelper {
        public static void IsValidRequestObject(HttpWebRequest request) {
            Assert.IsNotNull(request.Headers);
            Assert.IsTrue(request.AllowAutoRedirect);
            Assert.IsTrue(request.AllowWriteStreamBuffering);
            Assert.AreEqual(-1, request.ContentLength);
            Assert.IsTrue(request.KeepAlive);
            Assert.AreEqual(50, request.MaximumAutomaticRedirections);
            Assert.IsEmpty(request.MediaType);
            Assert.AreEqual("GET", request.Method);
            Assert.IsTrue(request.Pipelined);
            Assert.AreEqual(HttpVersion.Version11, request.ProtocolVersion);
            Assert.AreEqual(100000, request.Timeout);
            Assert.AreEqual(300000, request.ReadWriteTimeout);
            Assert.IsNotNull(request.Address);

            var lockerField = typeof(HttpWebRequest).GetField("locker", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(lockerField);
            Assert.IsNotNull(lockerField.GetValue(request));
        }
    }
}

