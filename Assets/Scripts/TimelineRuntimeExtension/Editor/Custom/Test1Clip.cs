using UnityEngine;
using UnityEngine.Playables;

namespace TimelineRuntimeExtension
{
    [CmdCodeGenerate]
    public class Test1Clip : PlayableAsset
    {
        public int A;
        public Vector3 Postion;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<Test1Behaviour>.Create(graph);
            return playable;
        }
    }
}


