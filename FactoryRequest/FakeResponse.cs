namespace FactoryRequest {
    using System;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class FakeResponse : HttpWebResponse {
        private Stream responseStream;

        public FakeResponse(SerializationInfo info, StreamingContext ctx) : base(info, ctx) {
        }

        internal void Init(Stream stream) {
            responseStream = stream;
            typeof(HttpWebResponse).GetField("webHeaders", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, new WebHeaderCollection());

        }

        public static FakeResponse New(Stream stream) {
            var response = (FakeResponse)FormatterServices.GetUninitializedObject(typeof(FakeResponse));
            response.Init(stream);
            return response;
        }

        public override Stream GetResponseStream() {
            return responseStream;
        }
    }
}

