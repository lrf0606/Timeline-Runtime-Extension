using UnityEngine.Playables;

namespace TimelineRuntimeExtension
{
    public class ExampleBehaviour2 : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
        }
    }
}