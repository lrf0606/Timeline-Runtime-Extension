using System.IO;
using UnityEditor;


namespace TimelineRuntimeExtension
{
    class TimelineExtensionProcessor : AssetModificationProcessor
    {
        public static void OnWillSaveAssets(string[] paths)
        {
            foreach(var assetPath in paths)
            {
                if (Path.GetExtension(assetPath) == ".playable")
                {
                    TimelineExtensionEditorUtil.CreateTimelineFileByPlayable(assetPath);
                }
            }
        }
    }
}