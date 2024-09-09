using UnityEngine;
using UnityEngine.Playables;

namespace TimelineRuntimeExtension
{
    [CmdCodeGenerate]
    public class ExampleClip2 : PlayableAsset
    {
        public int A;
        public string B;
        public float C;
        public double D;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<ExampleBehaviour2>.Create(graph);
            return playable;
        }
    }
}