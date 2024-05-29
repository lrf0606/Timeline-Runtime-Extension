using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace TimelineRuntimeExtension
{
    [AttributeUsage(AttributeTargets.Class)]
    class CmdCodeGenerateAttribute : Attribute
    {

    }

    static class CodeGenerateConfig
    {
        public const string FolderPath = "Assets/Scripts/TimelineRuntimeExtension/Runtime/Cmd/";

        public static void MakeSureGenenrateDirectory()
        {
            if (Directory.Exists(FolderPath))
            {
                return;
            }
            Directory.CreateDirectory(FolderPath);
        }

        public static List<Type> GetAllCmdCodeGenerateTypes()
        {
            var types = new List<Type>();
            foreach (var type in typeof(CodeGenerateConfig).Assembly.GetTypes())
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

    }
}
