using System;
// === Code CmdRuntime Generate start ===
namespace TimelineRuntimeExtension
{
    public class CmdExampleClip2 : TimelineCmdBase
    {
        public int A;
        public string B;
        public float C;
        public double D;
        public override void ParseField(string fieldName, string fieldType, string fieldValue)
        {
            switch(fieldName)
            {
                case "A":
                    A = (int)LitJson.ValueParserUtil.ToObject(fieldType, fieldValue); break;
                case "B":
                    B = (string)LitJson.ValueParserUtil.ToObject(fieldType, fieldValue); break;
                case "C":
                    C = (float)LitJson.ValueParserUtil.ToObject(fieldType, fieldValue); break;
                case "D":
                    D = (double)LitJson.ValueParserUtil.ToObject(fieldType, fieldValue); break;
            }
        }

        // === Cmd Override Start ===
        public override void OnStart()
        {
            UnityEngine.Debug.Log($"CmdExampleClip2 OnStart A={A} B={B} C={C} D={D}");
        }
        public override void OnUpdate(double deltaTime)
        {
            // logic when cmd update
        }
        public override void OnEnd()
        {
            UnityEngine.Debug.Log($"CmdExampleClip2 OnEnd A={A} B={B} C={C} D={D}");
        }
        // === Cmd Override End ===

    }
}
// === Code CmdRuntime Generate end ===
