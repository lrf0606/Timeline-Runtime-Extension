using System.IO;
using UnityEditor;
using UnityEngine.Timeline;
using LitJson;
using System.Collections.Generic;
using System;


namespace TimelineRuntimeExtension
{
    static class TimelineExtensionEditorUtil
    {
        private static string GetTimelineAssetJsonData(TimelineAsset timelineAsset)
        {
            JsonData timelineJsonData = new JsonData();
            JsonData clipListJsonData = new JsonData();
            var clipList = new List<TimelineClip>();
            foreach (var trackAsset in timelineAsset.GetOutputTracks())
            {
                var muted = trackAsset.muted;
                if (muted)
                {
                    continue;
                }
                foreach (var clip in trackAsset.GetClips())
                {
                    clipList.Add(clip);
                }
            }
            clipList.Sort((TimelineClip a, TimelineClip b) => { return a.start.CompareTo(b.start); }); // 按开始时间排序
            int id = 0;
            double allEndTime = 0;
            foreach (var clip in clipList)
            {
                var assetType = clip.asset.GetType();
                JsonData clipJsonData = new JsonData();
                clipJsonData["Guid"] = id++;
                clipJsonData["StartTime"] = clip.start;
                clipJsonData["EndTime"] = clip.end;
                clipJsonData["DurationTime"] = clip.duration;
                clipJsonData["CmdName"] = CodeGenerateConfig.GetCmdName(assetType);
                allEndTime = Math.Max(allEndTime, clip.end);

                JsonData fieldListJsonData = new JsonData();
                foreach (var field in assetType.GetFields())
                {
                    if (!field.IsPublic)
                    {
                        continue;
                    }
                    var fieldJsonData = new JsonData();
                    string fieldType = field.FieldType.Name;
                    fieldJsonData["FieldName"] = field.Name;
                    fieldJsonData["FieldType"] = fieldType;
                    var fieldValue = field.GetValue(clip.asset);
                    fieldJsonData["FieldValue"] = fieldValue != null ? ValueParserUtil.ToString(fieldType, fieldValue) : "";
                    fieldListJsonData.Add(fieldJsonData);
                }
                clipJsonData["Fields"] = fieldListJsonData;
                clipListJsonData.Add(clipJsonData);
            }
            timelineJsonData["DurationTime"] = allEndTime;
            if (clipList.Count > 0) 
            {
                timelineJsonData["ClipList"] = clipListJsonData;
            }
            return timelineJsonData.ToJson();
        }

        public static void CreateTimelineFileByPlayable(string playableAssetPath)
        {
            var timelineAsset = AssetDatabase.LoadAssetAtPath<TimelineAsset>(playableAssetPath);
            string timelineData = GetTimelineAssetJsonData(timelineAsset);
            string assetDcirectory = Path.GetDirectoryName(playableAssetPath);
            string timelinePath = Path.Combine(assetDcirectory, $"{Path.GetFileNameWithoutExtension(playableAssetPath)}.{TimelineExtensionImporter.Extension}");
            File.WriteAllText(timelinePath, timelineData);
            AssetDatabase.Refresh();
        }

        [MenuItem("Tools/Timeline Runtime Extension/Code Generate")]
        public static void CodeGenerate()
        {
            CmdRuntimeGenerate.Generate();
            CmdRegisterGenerate.Generate();
            AssetDatabase.Refresh();
        }

    }
}
