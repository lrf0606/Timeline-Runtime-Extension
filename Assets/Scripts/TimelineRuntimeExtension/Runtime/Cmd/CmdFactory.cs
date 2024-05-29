using System.Collections.Generic;


namespace TimelineRuntimeExtension
{
    public delegate TimelineCmdBase CreateCmdDelegate();
    public static partial class CmdFactory
    {
        private static Dictionary<string, CreateCmdDelegate> m_CreateFuncDict;

        static CmdFactory()
        {
            m_CreateFuncDict = new Dictionary<string, CreateCmdDelegate>();
        }

        public static void RegisterCreateFunc(string cmdName, CreateCmdDelegate createFunc) 
        {
            m_CreateFuncDict[cmdName] = createFunc;
        }

        public static TimelineCmdBase CreateCmdInstance(string cmdName) 
        {
            if (m_CreateFuncDict.TryGetValue(cmdName, out var func))
            {
                return func();
            }
            else
            {
                throw new System.Exception($"NodeFactory create node failed, cmdType={cmdName} doesn't registered");
            }
        }
    }

}
