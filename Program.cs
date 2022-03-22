// Decompiled with JetBrains decompiler
// Type: ZerroWare.Program
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using ZerroWare.Properties;

namespace ZerroWare
{
  internal static class Program
  {
    public static readonly bool JUST_FOR_TESTING = false;
    public static readonly string URL_FOR_REQUEST = "http://neolic.alber.de/neolic/index.php";
    public static readonly int NEO_SDIAG_SERVER_VERSION_ID = 51;
    public static readonly int NEO_SDIAG_BUILD_NUMBER = 32;
    public static readonly int MAX_DAYS_BETWEEN_UPDATES = 90;
    public static readonly int MAX_DAYS_BETWEEN_REACTIVATION = 250;
    public static readonly int FINAL_TERMINATION_DAYS_BUFFER = 14;
    private static Mutex _AppMutex = new Mutex(false, Assembly.GetExecutingAssembly().GetName().Name);
    public static SplashScreen SPLASH;

    public static bool ShowUpdateHint { set; get; }

    public static void ResetAppMutex()
    {
      try
      {
        Program._AppMutex.ReleaseMutex();
      }
      catch (Exception ex)
      {
      }
      Program._AppMutex.Dispose();
    }

    public static void SetSplashPercent(int percent)
    {
      if (Program.SPLASH == null)
        return;
      Program.SPLASH.ProgressValue = percent;
      HelperClass.DoEvents();
    }

    public static void IncrementSplashPercent(int increment)
    {
      if (Program.SPLASH == null)
        return;
      Program.SPLASH.ProgressValue += increment;
      HelperClass.DoEvents();
    }

    [STAThread]
    private static void Main(string[] args)
    {
      try
      {
        if (!string.IsNullOrEmpty(Settings.Default.Language))
          Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
      }
      catch (Exception ex)
      {
      }
      if (!Program._AppMutex.WaitOne(0, false))
      {
        UniqueError.Message(UniqueError.Number.APPLICATION_ALREADY_RUNNING);
        Environment.Exit(0);
      }
      Application.ThreadException += new ThreadExceptionEventHandler(new CustomExceptionHandler().OnThreadException);
      AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Program.SPLASH = new SplashScreen();
      Program.SPLASH.Show();
      Program.SPLASH.BringToFront();
      Program.SetSplashPercent(0);
      Thread thread1 = new Thread(new ThreadStart(Program.StartLogging));
      Thread thread2 = new Thread(new ThreadStart(HelperClass.CheckIfUpdateIsAvailable));
      Thread thread3 = new Thread(new ThreadStart(Program.WriteInformation));
      Thread thread4 = new Thread(new ThreadStart(HelperClass.UpdateVersionFile));
      Program.SetSplashPercent(5);
      try
      {
        DirectoryPermissions directoryPermissions = new DirectoryPermissions();
        if (!directoryPermissions.CreatingDefaultDirectory())
        {
          UniqueError.Message(UniqueError.Number.CREATING_DEFAULT_DIRECTORY, directoryPermissions.LastDir);
          Environment.Exit(0);
        }
        Program.SetSplashPercent(10);
        Directories.Instance.GlobalListFile = LocalSettingsFile.ReadFile().GlobalListFiles;
        Program.SetSplashPercent(11);
        if (!directoryPermissions.CreateNonGlobalUserDataDirectory())
        {
          UniqueError.Message(UniqueError.Number.CREATE_NON_GLOBAL_USER_DATA_DIRECTORY, Directories.Instance.UserDataPath);
          Environment.Exit(0);
        }
      }
      catch (ConfigurationException ex1)
      {
        string filename = ((ConfigurationException) ex1.InnerException).Filename;
        try
        {
          File.Delete(filename);
        }
        catch (Exception ex2)
        {
          int num = (int) MessageBox.Show(string.Format(GlobalResource.FailedToDeleteSettingsFile_Message, (object) filename), GlobalResource.FirmwareEndTranmission_Error_Caption);
        }
        Program.ResetAppMutex();
        Process.Start(Application.ExecutablePath);
        Environment.Exit(0);
      }
      Program.SetSplashPercent(15);
      thread2.Start();
      Program.SetSplashPercent(20);
      thread1.Start();
      Program.SetSplashPercent(25);
      while (thread1.IsAlive || thread2.IsAlive)
        Thread.Sleep(20);
      thread1.Join();
      thread2.Join();
      Program.SetSplashPercent(40);
      bool flag = false;
      while (!flag)
        flag = new Activation().StartActivation();
      Program.SetSplashPercent(50);
      thread3.Start();
      Program.SetSplashPercent(55);
      thread4.Start();
      Program.SetSplashPercent(60);
      while (thread3.IsAlive || thread4.IsAlive)
        Thread.Sleep(20);
      thread3.Join();
      thread4.Join();
      Program.SetSplashPercent(70);
      Application.Run((Form) MainWindow.Instance);
      Program.ResetAppMutex();
    }

    public static void CurrentDomain_UnhandledException(
      object sender,
      UnhandledExceptionEventArgs e)
    {
      try
      {
        GlobalLogger.Instance.WriteLine(e.ExceptionObject as Exception);
        GlobalLogger.Instance.WriteLine(sender.ToString());
      }
      catch (Exception ex)
      {
      }
      int num1 = (int) MessageBox.Show((e.ExceptionObject as Exception).Message + "\n" + (e.ExceptionObject as Exception).StackTrace, "Uuups", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      int num2 = (int) MessageBox.Show(GlobalResource.FatalError_Message, GlobalResource.FatalError_Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }

    public static void StartLogging()
    {
      GlobalLogger.Instance.XmlLogFileName = Directories.Instance.ZerrowareLogFileName;
      GlobalLogger.Instance.XmlLogFileNameArchive = Directories.Instance.NeoBackupLogFileName;
      GlobalLogger.Instance.LogLevel = GlobalLogger.Level.Warning;
      GlobalLogger.Instance.StartLogging();
    }

    public static void WriteInformation()
    {
      ComputerInformation.WriteFile();
      NeoInformation.WriteFile(Assembly.GetExecutingAssembly().GetName().Version.ToString(), ActivationInformation.VersionLevel(), ActivationInformation.OemPartnerId(), ActivationInformation.LicensedDate);
    }
  }
}
