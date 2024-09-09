using System;
// === Code CmdRuntime Generate start ===
namespace TimelineRuntimeExtension
{
    public class CmdExampleClip1 : TimelineCmdBase
    {
        public LitJson.Vector2 FaceDirection;
        public LitJson.Vector3 Postion;
        public override void ParseField(string fieldName, string fieldType, string fieldValue)
        {
            switch(fieldName)
            {
                case "FaceDirection":
                    FaceDirection = (LitJson.Vector2)LitJson.ValueParserUtil.ToObject(fieldType, fieldValue); break;
                case "Postion":
                    Postion = (LitJson.Vector3)LitJson.ValueParserUtil.ToObject(fieldType, fieldValue); break;
            }
        }

        // === Cmd Override Start ===
        public override void OnStart()
        {
            // logic when cmd start
            UnityEngine.Debug.Log($"CmdExampleClip1 OnStart FaceDirection={FaceDirection} Postion={Postion}");
        }
        public override void OnUpdate(double deltaTime)
        {
            // logic when cmd update
        }
        public override void OnEnd()
        {
            // logic when cmd end
            UnityEngine.Debug.Log($"CmdExampleClip1 OnEnd FaceDirection={FaceDirection} Postion={Postion}");
        }
        // === Cmd Override End ===

    }
}
// === Code CmdRuntime Generate end ===
