// Decompiled with JetBrains decompiler
// Type: ZerroWare.UpdateWorkerEventArgs
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System;

namespace ZerroWare
{
  public class UpdateWorkerEventArgs : EventArgs
  {
    public bool UpdateDone { get; set; }

    public bool ConnectionError { get; set; }

    public bool AccessLevelUpdate { get; set; }

    public bool NewsAvailable { get; set; }

    public int DataPercentage { get; set; }

    public int TotalPercentage { get; set; }

    public string Information { get; set; }
  }
}
