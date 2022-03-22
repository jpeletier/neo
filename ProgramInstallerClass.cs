// Decompiled with JetBrains decompiler
// Type: ZerroWare.ProgramInstallerClass
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Security.Permissions;

namespace ZerroWare
{
  [RunInstaller(true)]
  public class ProgramInstallerClass : Installer
  {
    private IContainer components;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent() => this.components = (IContainer) new Container();

    public ProgramInstallerClass() => this.InitializeComponent();

    [SecurityPermission(SecurityAction.Demand)]
    public override void Install(IDictionary stateSaver) => base.Install(stateSaver);

    [SecurityPermission(SecurityAction.Demand)]
    public override void Commit(IDictionary savedState)
    {
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      stopwatch.Stop();
      TimeSpan elapsed = stopwatch.Elapsed;
      try
      {
        DirectoryPermissions directoryPermissions = new DirectoryPermissions();
        while (true)
        {
          if (directoryPermissions.CreatingDefaultDirectory())
            goto label_4;
label_2:
          stopwatch.Start();
          directoryPermissions.UpdateDirectorySecurity();
          stopwatch.Stop();
          elapsed = stopwatch.Elapsed;
          continue;
label_4:
          if (elapsed.TotalSeconds > 30.0)
            goto label_2;
          else
            break;
        }
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.PROGRAM_INSTALLER, ex);
      }
      base.Commit(savedState);
    }

    [SecurityPermission(SecurityAction.Demand)]
    public override void Rollback(IDictionary savedState) => base.Rollback(savedState);

    [SecurityPermission(SecurityAction.Demand)]
    public override void Uninstall(IDictionary savedState) => base.Uninstall(savedState);
  }
}
