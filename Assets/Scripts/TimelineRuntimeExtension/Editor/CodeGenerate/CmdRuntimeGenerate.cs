using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LitJson;

namespace TimelineRuntimeExtension
{
    static class CmdRuntimeGenerate
    {
        const string GenerateStart = "// === Code CmdRuntime Generate start ===";
        const string GenerateEnd = "// === Code CmdRuntime Generate end ===";
        const string OverrideStart = "// === Cmd Override Start ===";
        const string OverrideEnd = "// === Cmd Override End ===";

        public static void Generate()
        {
            string directory = CodeGenerateUtil.MakeSureGenenrateDirectory();
            foreach(var type in CodeGenerateUtil.GetAllCmdCodeGenerateTypes())
            {
                string filePath = Path.Combine(directory, $"{CodeGenerateUtil.GetCmdName(type)}.cs");
                string code = GetCmdCode(type, filePath);
                if (File.Exists(filePath))
                {
                    string source = File.ReadAllText(filePath);
                    code = CodeGenerateUtil.ReplaceStringByStartAndEnd(source, code, GenerateStart, GenerateEnd);
                }
                else
                {
                    code = $"using System;{Environment.NewLine}{code}"; // 加个using防止后续编辑器自动不全using到错误位置
                }
                File.WriteAllText(filePath, code);
            }
        }

        private static string GetCmdCode(Type type, string filePath)
        {
            var code = new StringBuilder();
            code.AppendLine(GenerateStart);
            code.AppendLine($"namespace {type.Namespace}");
            code.AppendLine("{");
            code.AppendLine($"    public class {CodeGenerateUtil.GetCmdName(type)} : {typeof(TimelineCmdBase).Name}");
            code.AppendLine("    {");
            code.AppendLine(GetCmdFieldCode(type));
            code.AppendLine(GetCmdFuctionCode(filePath));
            code.AppendLine("    }");
            code.AppendLine("}");
            code.AppendLine(GenerateEnd);
            return code.ToString();
        }

        private static string GetCmdFieldCode(Type type)
        {
            var code = new StringBuilder();
            var fieldList = new List<FieldInfo>();
            foreach (var field in type.GetFields())
            {
                if (!field.IsPublic)
                {
                    continue;
                }
                fieldList.Add(field);
                code.AppendLine($"        public {ValueParserUtil.GetFieldTypeTransform(field.FieldType.Name)} {field.Name};");
            }
            if (fieldList.Count > 0)
            {
                code.AppendLine($"        public override void ParseField(string fieldName, string fieldType, string fieldValue)");
                code.AppendLine("        {");
                code.AppendLine("            switch(fieldName)");
                code.AppendLine("            {");
                foreach (var field in fieldList) 
                {
                    code.AppendLine($"                case \"{field.Name}\":");
                    code.AppendLine($"                    {field.Name} = ({ValueParserUtil.GetFieldTypeTransform(field.FieldType.Name)}){typeof(ValueParserUtil).Namespace}.{typeof(ValueParserUtil).Name}.ToObject(fieldType, fieldValue); break;");
                }
                code.AppendLine("            }");
                code.AppendLine("        }");
            }
            return code.ToString();
        }

        private static string GetCmdFuctionCode(string filePath)
        {
            string oldExecLogicCode = "";
            if (File.Exists(filePath))
            {
                string allCode = File.ReadAllText(filePath);
                oldExecLogicCode = CodeGenerateUtil.GetStringByStartAndEnd(allCode, OverrideStart, OverrideEnd);
            }
            if (string.IsNullOrEmpty(oldExecLogicCode))
            {
                var code = new StringBuilder();
                code.AppendLine($"        {OverrideStart}");
                code.AppendLine("        public override void OnStart()");
                code.AppendLine("        {");
                code.AppendLine("            // logic when cmd start");
                code.AppendLine("        }");
                code.AppendLine("        public override void OnUpdate(double deltaTime)");
                code.AppendLine("        {");
                code.AppendLine("            // logic when cmd update");
                code.AppendLine("        }");
                code.AppendLine("        public override void OnEnd()");
                code.AppendLine("        {");
                code.AppendLine("            // logic when cmd end");
                code.AppendLine("        }");
                code.ToString();
                code.AppendLine($"        {OverrideEnd}");
                return code.ToString();
            }
            else
            {
                return oldExecLogicCode;
            }
        }
    }
}

