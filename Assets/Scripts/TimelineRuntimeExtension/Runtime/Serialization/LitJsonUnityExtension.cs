using System;
using System.Collections.Generic;

namespace LitJson
{
    public delegate string Object2StringDelegate(object obj);
    public delegate string Object2StringDelegate<T>(T obj);
    public delegate object String2ObjectDelegate(string str);
    public delegate T String2ObjectDelegate<T>(string str);

    public static class ValueParserUtil
    {
        private static Dictionary<string, Object2StringDelegate> ParseObject2StringDict;
        private static Dictionary<string, String2ObjectDelegate> ParseString2ObjectDict;

        static ValueParserUtil()
        {
            ParseObject2StringDict = new Dictionary<string, Object2StringDelegate>();
            ParseString2ObjectDict = new Dictionary<string, String2ObjectDelegate>();

            RegisterToObject();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#endif
        private static void RegisterToString()
        {
            // int
            RegisterObject2String((int val) => { return val.ToString(); });
            // float
            RegisterObject2String((float val) => { return val.ToString(); });
            // double
            RegisterObject2String((double val) => { return val.ToString(); });
            // string
            RegisterObject2String((string val) => { return val; });
            // bool
            RegisterObject2String((bool val) => { return val.ToString(); });
            // vector2
            RegisterObject2String((UnityEngine.Vector2 vec2) => { return $"{vec2.x},{vec2.y}"; });
            // vector3
            RegisterObject2String((UnityEngine.Vector3 vec3) => { return $"{vec3.x},{vec3.y},{vec3.z}"; });
        }

        private static void RegisterToObject()
        {
            // int
            RegisterString2Object("Int32", (string str) => { return int.Parse(str); });
            // float
            RegisterString2Object("Single", (string str) => { return float.Parse(str); });
            // double
            RegisterString2Object("Double", (string str) => { return double.Parse(str); });
            // string
            RegisterString2Object("String", (string str) => { return str; });
            // bool
            RegisterString2Object("Boolean", (string str) => { return bool.Parse(str); });
            // vector2
            RegisterString2Object("Vector2", (string str) => {
                string[] parts = str.Split(',');
                return new UnityEngine.Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
            });
            // vector3
            RegisterString2Object("Vector3", (string str) => {
                string[] parts = str.Split(',');
                return new UnityEngine.Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
            });
        }

        public static void RegisterObject2String<T>(Object2StringDelegate<T> func)
        {
            ParseObject2StringDict[typeof(T).Name] = (object obj) => { return func((T)obj); };
        }

        public static void RegisterString2Object<T>(String2ObjectDelegate<T> func)
        {
            ParseString2ObjectDict[typeof(T).Name] = (string str) => { return func(str); };
        }

        public static void RegisterObject2String(string type, Object2StringDelegate func)
        {
            ParseObject2StringDict[type] = func;
        }

        public static void RegisterString2Object(string type, String2ObjectDelegate func)
        {
            ParseString2ObjectDict[type] = func;
        }

        public static string ToString(string type, object obj)
        {
            if (ParseObject2StringDict.TryGetValue(type, out var func))
            {
                return func(obj);
            }
            else
            {
                throw new Exception($"ParseUtil.ToString: {type} is not register");
            }
        }

        public static object ToObject(string type, string str)
        {
            if (ParseString2ObjectDict.TryGetValue(type, out var func))
            {
                return func(str);
            }
            else
            {
                throw new Exception($"ParseUtil.ToObject: {type} is not register");
            }
        }

        public static T ToObject<T>(string type, string str)
        {
            return (T)ToObject(type, str);
        }

    }
}
