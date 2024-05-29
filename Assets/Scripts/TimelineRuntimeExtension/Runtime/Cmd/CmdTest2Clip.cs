using LitJson;
using UnityEngine;
namespace TimelineRuntimeExtension
{
    public class CmdTest2Clip : TimelineCmdBase
    {
        public System.String Str;
        public System.Single Flt;
        public override void ParseField(string fieldName, string fieldType, string fieldValue)
        {
            switch(fieldName)
            {
                case "Str":
                    Str = ValueParserUtil.ToObject<System.String>(fieldType, fieldValue); break;
                case "Flt":
                    Flt = ValueParserUtil.ToObject<System.Single>(fieldType, fieldValue); break;
            }
        }

        public override void OnStart()
        {
            Debug.Log($"CmdTest2Clip OnStart Str={Str} Flt={Flt} {Time.timeSinceLevelLoad}");
        }
        public override void OnUpdate(double deltaTime)
        {
            // Debug.Log($"CmdTest2Clip OnUpdate {deltaTime}");
        }
        public override void OnEnd()
        {
            Debug.Log($"CmdTest2Clip OnEnd {Time.timeSinceLevelLoad}");
        }

    }
}
