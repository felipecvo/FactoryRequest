namespace FactoryRequest {
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;

    public class FakeResponse : HttpWebResponse {
        private Stream responseStream;

        public FakeResponse(SerializationInfo info, StreamingContext ctx) : base(info, ctx) {
        }

        internal void Init(Stream stream) {
            responseStream = stream;
        }

        public override Stream GetResponseStream() {
            return responseStream;
        }
    }
}

