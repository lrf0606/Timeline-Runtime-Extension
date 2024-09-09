using LitJson;
using System;
using System.Collections.Generic;
using System.IO;


namespace TimelineRuntimeExtension
{
    public class TimelineBase
    {
        public List<TimelineCmdBase> m_CmdList;
        public double m_DurationTime;
        public double m_CurrentTime;
     
        private bool m_IsStart;
        private bool m_IsEnd;

        public TimelineBase()
        {
            m_CmdList = new List<TimelineCmdBase>();
        }

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
                    var cmd = TimelineExtensionCmdFactory.CreateCmdInstance((string)cmdJsonData["CmdName"]);
                    cmd.LoadJsonData(cmdJsonData);
                    cmd.m_Owner = this;
                    cmdList.Add(cmd);
                }
            }
            Initlization(cmdList, duratoinTime);
        }

        public void Initlization(List<TimelineCmdBase> cmdList, double durationTime)
        {
            m_CmdList = cmdList;
            m_DurationTime = durationTime;
            m_CurrentTime = 0f;
            m_IsStart = false;
            m_IsEnd = false;
        }

        public void Start()
        {
            m_IsStart = true;
        }

        public void Update(double deltaTime)
        {
            if (!m_IsStart || m_IsEnd)
            {
                return;
            }
            var lastTime = m_CurrentTime;
            var nextTime = m_CurrentTime + deltaTime;
            m_CurrentTime = nextTime;
            foreach(var cmd in m_CmdList)
            {
                if (!cmd.m_IsStart)
                {
                    if (lastTime <= cmd.m_StartTime && nextTime >=cmd.m_StartTime)
                    {
                        cmd.Start();
                    }
                }
                if (!cmd.m_IsStart)
                {
                    continue;
                }
                if (nextTime > cmd.m_StartTime)
                {
                    cmd.Update(Math.Min(nextTime, cmd.m_EndTime) - Math.Max(lastTime, cmd.m_StartTime));
                }

                if (lastTime < cmd.m_EndTime && nextTime >= cmd.m_EndTime)
                {
                    cmd.End();
                }
            }
            if (nextTime >= m_DurationTime)
            {
                End();
            }
        }

        public void End()
        {
            m_IsEnd = true;
        }
    }
}
