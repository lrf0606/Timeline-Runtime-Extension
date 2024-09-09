using System;
using System.IO;
using System.Text;

namespace TimelineRuntimeExtension
{
    static class CmdRegisterGenerate
    {
        const string FileName = "RegisterCmd.cs";
        const string GenerateStart = "// === Code CmdRegister Generate start ===";
        const string GenerateEnd = "// === Code CmdRegister Generate end ===";
        public static void Generate()
        {
            string directory = CodeGenerateUtil.MakeSureGenenrateDirectory();
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GenerateStart);
            stringBuilder.AppendLine($"namespace {typeof(CmdRegisterGenerate).Namespace}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"    static partial class {typeof(TimelineExtensionCmdFactory).Name}");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        public static void InitCmdRegister()");
            stringBuilder.AppendLine("        {");
            foreach(var type in CodeGenerateUtil.GetAllCmdCodeGenerateTypes()) 
            {
                string className = CodeGenerateUtil.GetCmdName(type);
                stringBuilder.AppendLine($"            RegisterCreateFunc(\"{className}\", () => {{ return new {className}(); }});");
            }
            stringBuilder.AppendLine("        }");
            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine(GenerateEnd);
            
            var code = stringBuilder.ToString();
            string filePath = Path.Combine(directory, FileName);
            if (File.Exists(filePath))
            {
                var source = File.ReadAllText(filePath);
                code = CodeGenerateUtil.ReplaceStringByStartAndEnd(source, code, GenerateStart, GenerateEnd);
            }
            else
            {
                code = $"using System;{Environment.NewLine}{code}"; // 加个using防止后续编辑器自动不全using到错误位置
            }
            File.WriteAllText(filePath, code);
        }
    }
}
