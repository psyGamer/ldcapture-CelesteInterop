using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.ldcapture;

public class LDCaptureModule : EverestModule
{
    public override void Load()
    { }

    public override void Unload()
    { }

    // Use "fmod" as library name since it should exist and if ldcapture is loaded, it should be intercepted by it anyway.
    // A bit hacky, but it works.
    [DllImport("libfmod.so.10")]
    private static extern void ldcapture_StartRecording();
    [DllImport("libfmod.so.10")]
    private static extern void ldcapture_StopRecording();
    [DllImport("libfmod.so.10")]
    private static extern void ldcapture_StopRecordingAfter(int extensionFrames);
    [DllImport("libfmod.so.10")]
    private static extern void ldcapture_ReloadConfig();

    [Command("start_recording", "Starts the ldcapture recording")]
    private static void CmdStartRecording()
    {
        try
        {
            ldcapture_StartRecording();
            Engine.Commands.Log("Successfully started recording.", Color.LightBlue);
        }
        catch (EntryPointNotFoundException)
        {
            Engine.Commands.Log("ldcapture is not loaded! Please start Celeste with ldcapture. More information is found on the README.", Color.Red);
        }
    }

    [Command("stop_recording", "Stops the ldcapture recording. If specified, extensionFrames frames after this call.")]
    private static void CmdStopRecording(int extensionFrames = 0)
    {
        try
        {
            if (extensionFrames == 0)
                ldcapture_StopRecording();
            else
                ldcapture_StopRecordingAfter(extensionFrames);
            Engine.Commands.Log("Successfully stopped recording.", Color.LightBlue);
        }
        catch (EntryPointNotFoundException)
        {
            Engine.Commands.Log("ldcapture is not loaded! Please start Celeste with ldcapture. More information is found on the README.", Color.Red);
        }
    }

    [Command("reload_config", "Reloads the ldcapture config.")]
    private static void CmdReloadConfig(int extensionFrames = 0)
    {
        try
        {
            ldcapture_ReloadConfig();
            Engine.Commands.Log("Successfully reloaded config.", Color.LightBlue);
        }
        catch (EntryPointNotFoundException)
        {
            Engine.Commands.Log("ldcapture is not loaded! Please start Celeste with ldcapture. More information is found on the README.", Color.Red);
        }
    }
}