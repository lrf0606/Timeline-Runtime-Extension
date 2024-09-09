# Timeline-Runtime-Extension
## 项目介绍

Unity Timeline运行时扩展框架，解析Timeline数据为json，运行时不依赖Unity环境。

## 使用步骤

1.下载代码，把TimelineRuntimeExtension文件夹放置在脚本任意位置，修改TimelineRuntimeExtension/Editor/Config.cs里的"CMD_CODE_GENERATE_PATH"变量值，设置代码生成Cmd指令的存放位置。

2.正常编写Timeline的Behaviour、Track、Clip拓展代码，可参考TimelineRuntimeExtension/Editor/Custom文件夹里代码。

3.创建任意timeline文件，创建轨道和片段，保存后会自动生成同名的json格式文件，可参考Assets/Examples/TimelineFiles文件夹下的Example.playable和Example.timeline文件。

4.点击工具栏的Tools/Timeline Runtime Extension/Code Generate，会在第1步指定路径生成Clip对应的Cmd实现代码，在OnStart、OnUpdate、OnEnd中实现对应逻辑，可参考TimelineRuntimeExtension/Runtime/Cmd中的文件。

5.运行时，首先通过代码进行初始化，之后创建timeline运行时对象并开始执行，可参考Assets/Examples/Example.cs。

```c#
using System.Collections.Generic;
using UnityEngine;
using TimelineRuntimeExtension;

public class Test : MonoBehaviour
{
    private List<TimelineBase> m_TimelineList = new List<TimelineBase>();

    void Start()
    {
        TimelineExtensionCmdFactory.InitCmdRegister();  // 初始化，一个项目只执行一次
    }

    public void CreateTimeline()
    {
        // 创建timeline运行时对象并保存
        var timeline = new TimelineBase();
        timeline.InitlizationByFile("Assets/Examples/TimelineFiles/Example.timeline");
        timeline.Start();
        m_TimelineList.Add(timeline);
    }

    void Update()
    {
        // 空格键抬起
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            CreateTimeline();
        }
        // Update中驱动timeline运行时更新，使其中cmd工作
        foreach (var timeline in m_TimelineList)
        {
            timeline.Update(Time.deltaTime);
        }
    }
}

```

6.把上述脚本挂在任何GameObject上启动游戏，检查第4步实现的逻辑是否生效。

## 源码介绍

1.核心功能在于解析Unity Timeline的playable文件，生成一份双端通用的json格式文件，运行时读取、解析json文件，以Cmd的形式执行自定义逻辑，playable转json在TimelineRuntimeExtension/Editor/Util/TimelineExtensionUtil.cs，json运行时解析在TimelineRuntimeExtension/Runtime/Data中。

2.序列化和反序列化使用LitJson库中基础的JsonData和string相互转化的接口。

3.使用代码生成，来避免运行时反射创建类实例，避免使用反射给字段赋值，具体可见TimelineRuntimeExtension/Editor/CodeGenerate，以及代码生成的RegisterCmd.cs和Cmd的ParseField方法。
