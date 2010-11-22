namespace FactoryRequest {
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization;

    public class FakeRequest : HttpWebRequest {
        private Uri requestUri;

        public FakeRequest(SerializationInfo info, StreamingContext ctx) : base(info, ctx) { }

        public override Uri RequestUri {
            get {
                return requestUri;
            }
        }

        internal void Init(Uri uri) {
            requestUri = uri;
        }

        public override WebResponse GetResponse() {
            var response = (FakeResponse)FormatterServices.GetUninitializedObject(typeof(FakeResponse));
            response.Init(Factory.GetStream(RequestUri));
            return response;
        }
    }
}

