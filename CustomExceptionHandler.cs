// Decompiled with JetBrains decompiler
// Type: ZerroWare.CustomExceptionHandler
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace ZerroWare
{
  internal class CustomExceptionHandler
  {
    public void OnThreadException(object sender, ThreadExceptionEventArgs e)
    {
      try
      {
        GlobalLogger.Instance.WriteLine(e.Exception);
        GlobalLogger.Instance.WriteLine(sender.ToString());
      }
      catch (Exception ex)
      {
      }
      int num1 = (int) MessageBox.Show(e.Exception.Message + "\n" + e.Exception.StackTrace, "Uuups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      int num2 = (int) MessageBox.Show(GlobalResource.FatalError_Message, GlobalResource.FatalError_Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Program.ResetAppMutex();
      Application.Exit();
      Process.Start(Application.ExecutablePath);
    }
  }
}
