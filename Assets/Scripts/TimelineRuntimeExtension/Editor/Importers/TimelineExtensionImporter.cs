using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace TimelineRuntimeExtension
{
    [ScriptedImporter(1, Extension)]
    class TimelineExtensionImporter : ScriptedImporter
    {
        public const string Extension = "timeline"; // 文件名后缀
        public override void OnImportAsset(AssetImportContext ctx)
        {
            string text = File.ReadAllText(ctx.assetPath);
            var textAsset = new TextAsset(text);
            ctx.AddObjectToAsset("main obj", textAsset);
            ctx.SetMainObject(textAsset);
        }
    }
}