using System.Collections.Generic;
using UnityEngine;
using TimelineRuntimeExtension;


public class Test : MonoBehaviour
{
    private List<TimelineBase> m_TimelineList = new List<TimelineBase>();

    void Start()
    {
        TimelineExtensionCmdFactory.InitCmdRegister();  // ��ʼ����һ����Ŀִֻ��һ��
    }

    public void CreateTimeline()
    {
        var timeline = new TimelineBase();
        timeline.InitlizationByFile("Assets/Examples/TimelineFiles/Example.timeline");
        timeline.Start();
        m_TimelineList.Add(timeline);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            CreateTimeline();
        }
        foreach (var timeline in m_TimelineList)
        {
            timeline.Update(Time.deltaTime);
        }
    }
}
