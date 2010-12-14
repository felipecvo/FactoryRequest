namespace FactoryRequest {
    using System;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class FakeRequest : HttpWebRequest {
        private Uri requestUri;
        private long contentLength;

        public FakeRequest(SerializationInfo info, StreamingContext ctx) : base(info, ctx) {
        }

        public override Uri RequestUri {
            get { return requestUri; }
        }

        public static FakeRequest New(Uri uri) {
            var request = (FakeRequest)FormatterServices.GetUninitializedObject(typeof(FakeRequest));
            request.Init(uri);
            return request;
        }

        internal void Init(Uri uri) {
            requestUri = uri;
            Headers = new WebHeaderCollection();
            AllowAutoRedirect = true;
            AllowWriteStreamBuffering = true;
            contentLength = -1;
            KeepAlive = true;
            MaximumAutomaticRedirections = 50;
            MediaType = string.Empty;
            Method = "GET";
            Pipelined = true;
            ProtocolVersion = HttpVersion.Version11;
            Timeout = 100000;
            ReadWriteTimeout = 300000;
            var actualUriField = typeof(HttpWebRequest).GetField("actualUri", BindingFlags.NonPublic | BindingFlags.Instance);
            actualUriField.SetValue(this, uri);
            var lockerField = typeof(HttpWebRequest).GetField("locker", BindingFlags.NonPublic | BindingFlags.Instance);
            lockerField.SetValue(this, new object());
        }

        public override Stream GetRequestStream() {
            if(Method.ToUpper() != "POST")
                throw new ProtocolViolationException("Cannot send data when method is: " + Method);

            return new MemoryStream();
        }

        public override long ContentLength {
            get { return contentLength; }
            set {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value", "Content-Length must be >= 0");
                
                contentLength = value;
            }
        }

        public override WebResponse GetResponse() {
            return FakeResponse.New(Factory.GetStream(RequestUri));
        }
    }
}

