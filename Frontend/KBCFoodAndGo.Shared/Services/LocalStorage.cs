using System;
using System.Collections.Generic;
using KBCFoodAndGo.Shared.Exceptions;

namespace KBCFoodAndGo.Shared.Services
{
    public class LocalStorage
    {
        private static Dictionary<string, Object> localStorage;

        protected LocalStorage()
        {

        }
        public static void InitializeStorage()
        {
            localStorage = new Dictionary<string, object>();
        }

        public static void Add(string key, Object value)
        {
            if (!localStorage.ContainsKey(key))
            {
                localStorage.Add(key, value);
            }
            else
            {
                localStorage[key] = value;
            }
        }

        public static void Remove(string key)
        {
            if (localStorage.ContainsKey(key))
            {
                localStorage.Remove(key);
            }
        }

        public static Object Get(string key)
        {
            if (localStorage.ContainsKey(key))
            {
                return localStorage[key];
            }

            throw new ItemNotFoundException("Item with key '" + key + "' is not found.");
        }
    }
}
