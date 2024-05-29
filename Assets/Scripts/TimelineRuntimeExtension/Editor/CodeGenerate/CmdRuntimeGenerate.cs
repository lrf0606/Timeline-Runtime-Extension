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
        public static void Generate()
        {
            CodeGenerateConfig.MakeSureGenenrateDirectory();
            foreach(var type in CodeGenerateConfig.GetAllCmdCodeGenerateTypes())
            {
                string filePath = Path.Combine(CodeGenerateConfig.FolderPath, $"{CodeGenerateConfig.GetCmdName(type)}.cs");
                if (File.Exists(filePath))
                {
                    continue;
                }
                string code = GetCmdCode(type);
                File.WriteAllText(filePath, code);
            }
        }

        private static string GetCmdCode(Type type)
        {
            var code = new StringBuilder();
            code.AppendLine($"using {typeof(ValueParserUtil).Namespace};");
            code.AppendLine($"namespace {type.Namespace}");
            code.AppendLine("{");
            code.AppendLine($"    public class {CodeGenerateConfig.GetCmdName(type)} : {typeof(TimelineCmdBase).Name}");
            code.AppendLine("    {");
            code.AppendLine(GetCmdFieldCode(type));
            code.AppendLine(GetCmdFuctionCode());
            code.AppendLine("    }");
            code.AppendLine("}");
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
                code.AppendLine($"        public {field.FieldType} {field.Name};");
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
                    code.AppendLine($"                    {field.Name} = {typeof(ValueParserUtil).Name}.ToObject<{field.FieldType}>(fieldType, fieldValue); break;");
                }
                code.AppendLine("            }");
                code.AppendLine("        }");
            }
            return code.ToString();
        }

        private static string GetCmdFuctionCode()
        {
            var code = new StringBuilder();
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
            return code.ToString();
        }
    }
}

