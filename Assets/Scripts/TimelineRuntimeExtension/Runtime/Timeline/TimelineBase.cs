using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


namespace TimelineRuntimeExtension
{
    public class TimelineBase
    {
        public List<TimelineCmdBase> CmdList;
        public double DurationTime;
        public double CurrentTime;
     
        public bool IsStart;
        public bool IsEnd;

        public void InitlizationByFile(string assetPath)
        {
            string fileData = File.ReadAllText(assetPath);
            var jsonData = JsonMapper.ToObject(fileData);
            var duratoinTime = (double)jsonData["DurationTime"];
            var cmdList = new List<TimelineCmdBase>();
            if (jsonData.ContainsKey("ClipList"))
            {
                foreach (JsonData cmdJsonData in jsonData["ClipList"])
                {
                    var cmd = CmdFactory.CreateCmdInstance((string)cmdJsonData["CmdName"]);
                    cmd.LoadJsonData(cmdJsonData);
                    cmd.Owner = this;
                    cmdList.Add(cmd);
                }
            }
            Initlization(cmdList, duratoinTime);
        }

        public void Initlization(List<TimelineCmdBase> cmdList, double durationTime)
        {
            CmdList = cmdList;
            DurationTime = durationTime;
            CurrentTime = 0f;
            IsStart = false;
            IsEnd = false;
        }

        public void Start()
        {
            IsStart = true;
        }

        public void Update(double deltaTime)
        {
            if (!IsStart || IsEnd)
            {
                return;
            }
            var lastTime = CurrentTime;
            var nextTime = CurrentTime + deltaTime;
            CurrentTime = nextTime;
            foreach(var cmd in CmdList)
            {
                if (!cmd.IsStart)
                {
                    if (lastTime <= cmd.StartTime && nextTime >=cmd.StartTime)
                    {
                        cmd.Start();
                    }
                }
                if (!cmd.IsStart)
                {
                    continue;
                }
                if (nextTime > cmd.StartTime)
                {
                    cmd.Update(Math.Min(nextTime, cmd.EndTime) - Math.Max(lastTime, cmd.StartTime));
                }

                if (lastTime < cmd.EndTime && nextTime >= cmd.EndTime)
                {
                    cmd.End();
                }
            }
            if (nextTime >= DurationTime)
            {
                End();
            }
        }

        public void End()
        {
            IsEnd = true;
        }
    }
}
