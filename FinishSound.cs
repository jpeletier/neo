// Decompiled with JetBrains decompiler
// Type: ZerroWare.FinishSound
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.IO;
using System.Media;
using ZerroWare.Properties;

namespace ZerroWare
{
  internal class FinishSound
  {
    private static volatile FinishSound instance = (FinishSound) null;
    private static object mutex = new object();
    private bool playFinishedSound;
    private SoundPlayer player;

    public static FinishSound Instance
    {
      get
      {
        if (FinishSound.instance == null)
        {
          lock (FinishSound.mutex)
          {
            if (FinishSound.instance == null)
              FinishSound.instance = new FinishSound();
          }
        }
        return FinishSound.instance;
      }
    }

    private FinishSound()
    {
      this.player = new SoundPlayer();
      this.UpdateSoundSettings();
    }

    public void Play()
    {
      if (!this.playFinishedSound)
        return;
      try
      {
        this.player.Play();
      }
      catch (Exception ex)
      {
        SystemSounds.Exclamation.Play();
      }
    }

    public void UpdateSoundSettings() => this.UpdateSoundSettings(LocalSettingsFile.ReadFile());

    public void UpdateSoundSettings(LocalSettingsFile lsf)
    {
      this.playFinishedSound = lsf.PlayFinishedSound;
      if (lsf.SoundFile != string.Empty && !lsf.PlayDefaultSound)
        this.player.SoundLocation = lsf.SoundFile;
      else
        this.player.Stream = (Stream) Resources.ding;
    }
  }
}
