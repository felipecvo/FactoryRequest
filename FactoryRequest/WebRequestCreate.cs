namespace FactoryRequest {
    using System;
    using System.Net;
    using System.Reflection;
    using System.Runtime.Serialization;

    public class WebRequestCreate : IWebRequestCreate {
        #region IWebRequestCreate implementation

        public WebRequest Create(Uri uri) {
            if(Factory.IsMocked(uri)) {
                var request = (FakeRequest)FormatterServices.GetUninitializedObject(typeof(FakeRequest));
                request.Init(uri);
                return request;
            } else {
                var ctor = typeof(HttpWebRequest).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                    null, new Type[] { typeof(Uri) }, null);

                return (HttpWebRequest)ctor.Invoke(new object[] { uri });
            }
        }

        #endregion
    }
}

