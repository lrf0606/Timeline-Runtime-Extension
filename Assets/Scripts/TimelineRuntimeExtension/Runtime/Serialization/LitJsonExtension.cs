using System;
using System.Collections.Generic;

namespace LitJson
{
    public delegate object String2ObjectDelegate(string str);
    public delegate T String2ObjectDelegate<T>(string str);

    public static class ValueParserUtil
    {
        private static Dictionary<string, String2ObjectDelegate> m_ParseString2ObjectDict;
        private static Dictionary<string, string> m_FieldTypeTransform;

        static ValueParserUtil()
        {
            m_ParseString2ObjectDict = new Dictionary<string, String2ObjectDelegate>();
            m_FieldTypeTransform = new Dictionary<string, string>();

            RegisterToObject();
        }

        private static void RegisterToObject()
        {
            // int
            RegisterString2Object("Int32", (string str) => { return int.Parse(str); });
            m_FieldTypeTransform["Int32"] = "int";
            // float
            RegisterString2Object("Single", (string str) => { return float.Parse(str); });
            m_FieldTypeTransform["Single"] = "float";
            // double
            RegisterString2Object("Double", (string str) => { return double.Parse(str); });
            m_FieldTypeTransform["Double"] = "double";
            // string
            RegisterString2Object("String", (string str) => { return str; });
            m_FieldTypeTransform["String"] = "string";
            // bool
            RegisterString2Object("Boolean", (string str) => { return bool.Parse(str); });
            m_FieldTypeTransform["Boolean"] = "bool";
            // vector2
            RegisterString2Object("Vector2", (string str) => { return Vector2.Parse(str); });
            m_FieldTypeTransform["Vector2"] = "LitJson.Vector2";
            // vector3
            RegisterString2Object("Vector3", (string str) => { return Vector3.Parse(str); });
            m_FieldTypeTransform["Vector3"] = "LitJson.Vector3";
        }

        public static string GetFieldTypeTransform(string fieldType)
        {
            return m_FieldTypeTransform[fieldType];
        }

        public static void RegisterString2Object<T>(String2ObjectDelegate<T> func)
        {
            m_ParseString2ObjectDict[typeof(T).Name] = (string str) => { return func(str); };
        }

        public static void RegisterString2Object(string type, String2ObjectDelegate func)
        {
            m_ParseString2ObjectDict[type] = func;
        }

        public static object ToObject(string type, string str)
        {
            if (m_ParseString2ObjectDict.TryGetValue(type, out var func))
            {
                return func(str);
            }
            else
            {
                throw new Exception($"ParseUtil.ToObject: {type} is not register");
            }
        }
    }

    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        public static Vector2 Parse(string str)
        {
            str = str[1..^1];
            string[] parts = str.Split(',');
            if (parts.Length != 2)
            {
                throw new ArgumentException($"Vector2½âÎö´íÎó:{str}");
            }
            return new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));
        }
    }

    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public static Vector3 Parse(string str)
        {
            str = str[1..^1];
            string[] parts = str.Split(',');
            if (parts.Length != 3)
            {
                throw new ArgumentException($"Vector3½âÎö´íÎó:{str}");
            }
            return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
        }
    }

}
