using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FoodUp.Tests
{
    internal class MockRequestCookieCollection : IRequestCookieCollection
    {
        private Dictionary<string, string> cookieStore;

        public MockRequestCookieCollection(Dictionary<string, string> cookieStore)
        {
            this.cookieStore = cookieStore;
        }

        public string this[string key] => cookieStore[key];

        public int Count => cookieStore.Count;

        public ICollection<string> Keys => cookieStore.Keys;

        public bool ContainsKey(string key)
        {
            return cookieStore.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return cookieStore.GetEnumerator();
        }

        public bool TryGetValue(string key, out string value)
        {
            return cookieStore.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cookieStore.GetEnumerator();
        }
    }
}