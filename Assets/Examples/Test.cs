using System.Collections.Generic;
using UnityEngine;
using TimelineRuntimeExtension;


public class Test : MonoBehaviour
{
    private List<TimelineBase> m_TimelineList;
     
    // Start is called before the first frame update
    void Start()
    {
        // init
        m_TimelineList = new List<TimelineBase>();
        CmdFactory.InitCmdRegister();

    }

    public void CreateTimeline()
    {
        var timeline1 = new TimelineBase();
        timeline1.InitlizationByFile("Assets/Examples/TimelineFiles/New Timeline.timeline");

        timeline1.Start();
        m_TimelineList.Add(timeline1);

        var timeline2 = new TimelineBase();
        timeline2.InitlizationByFile("Assets/Examples/TimelineFiles/New Timeline 1.timeline");
        timeline2.Start();
        m_TimelineList.Add(timeline2);
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
