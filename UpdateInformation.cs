// Decompiled with JetBrains decompiler
// Type: ZerroWare.UpdateInformation
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System;

namespace ZerroWare
{
  internal class UpdateInformation
  {
    public UpdateInformation(
      string id,
      string version,
      string datetime,
      string warning,
      string critical,
      string versionText,
      bool testingOnly)
    {
      try
      {
        this.Id = Convert.ToInt32(id);
      }
      catch (FormatException ex)
      {
        this.Id = 0;
      }
      try
      {
        this.Version = Convert.ToInt32(version);
      }
      catch (FormatException ex)
      {
        this.Version = 0;
      }
      try
      {
        this.CreationDate = DateTime.Parse(datetime);
      }
      catch (FormatException ex)
      {
        this.CreationDate = DateTime.MinValue;
      }
      this.Warning = warning;
      try
      {
        this.Critical = Convert.ToInt32(critical) != 0;
      }
      catch (FormatException ex)
      {
        this.Critical = true;
      }
      this.VersionText = versionText;
      this.TestingOnly = testingOnly;
    }

    public int Id { set; get; }

    public int Version { set; get; }

    public DateTime CreationDate { set; get; }

    public string Warning { set; get; }

    public bool Critical { set; get; }

    public string VersionText { set; get; }

    public bool TestingOnly { set; get; }
  }
}
