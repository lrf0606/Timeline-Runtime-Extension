using UnityEngine;
using UnityEngine.Playables;

namespace TimelineRuntimeExtension
{
    [CmdCodeGenerate]
    public class Test2Clip : PlayableAsset
    {
        public string Str;
        public float Flt;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<Test2Behaviour>.Create(graph);
            return playable;
        }
    }
}


