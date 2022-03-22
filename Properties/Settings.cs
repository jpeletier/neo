// Decompiled with JetBrains decompiler
// Type: ZerroWare.Properties.Settings
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace ZerroWare.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    [UserScopedSetting]
    public string Language
    {
      get => (string) this[nameof (Language)];
      set => this[nameof (Language)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("614")]
    [DebuggerNonUserCode]
    public int FormHeight
    {
      get => (int) this[nameof (FormHeight)];
      set => this[nameof (FormHeight)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("1026")]
    [DebuggerNonUserCode]
    public int FormWidth
    {
      get => (int) this[nameof (FormWidth)];
      set => this[nameof (FormWidth)] = (object) value;
    }

    [DefaultSettingValue("62")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public int PreviewHeight
    {
      get => (int) this[nameof (PreviewHeight)];
      set => this[nameof (PreviewHeight)] = (object) value;
    }

    [DefaultSettingValue("48")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public int PreviewWidth
    {
      get => (int) this[nameof (PreviewWidth)];
      set => this[nameof (PreviewWidth)] = (object) value;
    }
  }
}
