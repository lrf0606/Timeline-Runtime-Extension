using LitJson;
using UnityEngine;
namespace TimelineRuntimeExtension
{
    public class CmdTest1Clip : TimelineCmdBase
    {
        public System.Int32 A;
        public UnityEngine.Vector3 Postion;
        public override void ParseField(string fieldName, string fieldType, string fieldValue)
        {
            switch(fieldName)
            {
                case "A":
                    A = ValueParserUtil.ToObject<System.Int32>(fieldType, fieldValue); break;
                case "Postion":
                    Postion = ValueParserUtil.ToObject<UnityEngine.Vector3>(fieldType, fieldValue); break;
            }
        }

        public override void OnStart()
        {
            Debug.Log($"CmdTest1Clip OnStart A={A} Postion={Postion} {Time.timeSinceLevelLoad}");
        }
        public override void OnUpdate(double deltaTime)
        {
            // Debug.Log($"CmdTest1Clip OnUpdate {deltaTime}");
        }
        public override void OnEnd()
        {
            Debug.Log($"CmdTest1Clip OnEnd {Time.timeSinceLevelLoad}");
        }

    }
}
