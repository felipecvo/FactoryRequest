namespace FactoryRequest {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Reflection;
    using System.Text;

    public static class Factory {
        private static WebRequestCreate creator = new WebRequestCreate();
        private static bool initialized = false;
        private static Dictionary<string, string> mockRequest = new Dictionary<string, string>();

        public static void Init() {
            typeof(WebRequest).GetMethod("ClearPrefixes", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
            WebRequest.RegisterPrefix("http", creator);
            WebRequest.RegisterPrefix("https", creator);
            initialized = true;
        }

        public static void EnsureInit() {
            if(!initialized) {
                Init();
            }
        }

        public static void Register(string url, string response) {
            EnsureInit();
            mockRequest.Add(new Uri(url).ToString(), response);
        }

        public static bool IsMocked(Uri url) {
            return mockRequest.ContainsKey(url.ToString());
        }

        public static MemoryStream GetStream(Uri uri) {
            return new MemoryStream(Encoding.UTF8.GetBytes(mockRequest[uri.ToString()]));
        }
    }
}

