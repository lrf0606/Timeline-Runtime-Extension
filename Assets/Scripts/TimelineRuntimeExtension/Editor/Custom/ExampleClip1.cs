using UnityEngine;
using UnityEngine.Playables;

namespace TimelineRuntimeExtension
{
    [CmdCodeGenerate]
    public class ExampleClip1 : PlayableAsset
    {
        public Vector2 FaceDirection;
        public Vector3 Postion;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<ExampleBehaviour1>.Create(graph);
            return playable;
        }
    }
}