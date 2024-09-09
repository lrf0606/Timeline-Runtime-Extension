using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace TimelineRuntimeExtension
{
    // 需要代码生成的Cmd添加此属性
    [AttributeUsage(AttributeTargets.Class)]
    class CmdCodeGenerateAttribute : Attribute
    {

    }

    static class CodeGenerateUtil
    {
        public static string MakeSureGenenrateDirectory()
        {
            var directory = TimelineRuntimeExtensionConfig.CMD_CODE_GENERATE_PATH;
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory ;

        }

        public static List<Type> GetAllCmdCodeGenerateTypes()
        {
            var types = new List<Type>();
            foreach (var type in typeof(CodeGenerateUtil).Assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && type.GetCustomAttribute<CmdCodeGenerateAttribute>() != null)
                {
                    types.Add(type);
                }
            }
            return types;
        }

        public static string GetCmdName(Type type)
        {
            return $"Cmd{type.Name}";
        }

        // 获取以target替换source中以start开头end结尾的内容
        public static string ReplaceStringByStartAndEnd(string source, string target, string start, string end)
        {
            int startIndex = source.IndexOf(start);
            if (startIndex < 0)
            {
                return source;
            }
            int endIndex = source.LastIndexOf(end);
            if (endIndex < 0)
            {
                return source;
            }
            string head = source[..startIndex];
            string tail = source[(endIndex + end.Length)..];
            return head + target + tail;
        }

        // 获取source中以start开头end结尾的内容
        public static string GetStringByStartAndEnd(string source, string start, string end, bool delStartAndEnd = false)
        {
            int startIndex = source.IndexOf(start);
            if (startIndex < 0)
            {
                return "";
            }
            int endIndex = source.LastIndexOf(end);
            if (endIndex < 0)
            {
                return "";
            }
            if (delStartAndEnd)
            {
                return source[(startIndex + start.Length)..endIndex];
            }
            else
            {
                return source[startIndex..(endIndex + end.Length)];
            }
        }
    }
}
