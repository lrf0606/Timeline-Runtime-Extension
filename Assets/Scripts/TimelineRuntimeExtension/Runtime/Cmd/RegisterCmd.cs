using System;
// === Code CmdRegister Generate start ===
namespace TimelineRuntimeExtension
{
    static partial class TimelineExtensionCmdFactory
    {
        public static void InitCmdRegister()
        {
            RegisterCreateFunc("CmdExampleClip1", () => { return new CmdExampleClip1(); });
            RegisterCreateFunc("CmdExampleClip2", () => { return new CmdExampleClip2(); });
        }
    }
}
// === Code CmdRegister Generate end ===

