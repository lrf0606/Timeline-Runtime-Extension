using System.Collections.Generic;
using UnityEngine;
using TimelineRuntimeExtension;


public class Test : MonoBehaviour
{
    private List<TimelineBase> m_TimelineList = new List<TimelineBase>();

    // Start is called before the first frame update
    void Start()
    {
        // init
        TimelineExtensionCmdFactory.InitCmdRegister(); 
    }

    public void CreateTimeline()
    {
        var timeline = new TimelineBase();
        timeline.InitlizationByFile("Assets/Examples/TimelineFiles/Example.timeline");
        timeline.Start();
        m_TimelineList.Add(timeline);
    }


    // Update is called once per frame
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
