// Decompiled with JetBrains decompiler
// Type: ZerroWare.Activation
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UIElements;

namespace ZerroWare
{
  internal class Activation
  {
    private string contentToSend = string.Empty;
    private string contentReceived = string.Empty;
    private bool successfullyActivated;
    private string uniqueHardware = "";
    private DirectoryPermissions dirPer;
    private Security sec;
    private Point firstStartLocation;
    private Size firstStartSize;

    public Activation()
    {
      this.dirPer = new DirectoryPermissions();
      this.sec = new Security();
      this.firstStartLocation = new Point();
      this.firstStartSize = new Size();
    }

    public bool WasActivated()
    {
      if (!this.sec.DefaultLicenseFileExists())
        return false;
      string[] strArray = this.sec.DecryptLicenseFile().Split(';');
      ActivationInformation.SetLicenseKey(strArray[3]);
      try
      {
        ActivationInformation.AdditionalInformationProperty = strArray[4];
      }
      catch (IndexOutOfRangeException ex)
      {
      }
      return ActivationInformation.ActivationWasSuccessful(strArray[0] + "\n", strArray[1]);
    }

    public bool StartActivation()
    {
      Program.IncrementSplashPercent(1);
      this.uniqueHardware = ActivationInformation.UniqueHardwareID();
      this.successfullyActivated = false;
      Program.IncrementSplashPercent(1);
      try
      {
        if (this.sec.DefaultLicenseFileExists())
        {
          string[] strArray = this.sec.DecryptLicenseFile().Split(';');
          ActivationInformation.SetLicenseKey(strArray[3]);
          try
          {
            ActivationInformation.AdditionalInformationProperty = strArray[4];
          }
          catch (IndexOutOfRangeException ex)
          {
          }
          Program.IncrementSplashPercent(1);
          if (ActivationInformation.ActivationWasSuccessful(strArray[0] + "\n", strArray[1]) && this.uniqueHardware == strArray[2] && ActivationInformation.LicenseKey().Length == 29)
          {
            ActivationInformation.LicensedHash = HashGenerating.ComputeHash(ActivationInformation.LicenseKey(), HashGenerating.DefaultSaltByte());
            ActivationInformation.LicensedDate = strArray[1];
            ActivationInformation.LicensedHardwareID = strArray[2];
            Program.IncrementSplashPercent(1);
            if ((DateTime.Now - DateTime.ParseExact(ActivationInformation.LicensedDate, "yyyyMMddHHmmssfff", (IFormatProvider) CultureInfo.InvariantCulture)).Days < Program.MAX_DAYS_BETWEEN_REACTIVATION + Program.FINAL_TERMINATION_DAYS_BUFFER)
            {
              Program.IncrementSplashPercent(1);
              return true;
            }
            int num = (int) MessageBox.Show(GlobalResource.License_Expired_Message, GlobalResource.License_Expired_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            ActivationInformation.VersionLevelProperty = 0;
            FileOperation.DeleteFile(Directories.Instance.LicenseKeyFileName);
          }
        }
        ActivationInformation.SetLicenseKey("");
        FirstStartDialog firstStartDialog = new FirstStartDialog();
        int num1 = (int) firstStartDialog.ShowDialog();
        this.firstStartSize = firstStartDialog.Size;
        this.firstStartLocation = firstStartDialog.Location;
        Thread thread1 = new Thread(new ThreadStart(this.showPleaseWait));
        thread1.CurrentCulture = firstStartDialog.ActualCultureInfo;
        thread1.CurrentUICulture = firstStartDialog.ActualCultureInfo;
        thread1.Start();
        this.successfullyActivated = this.DoActivation();
        Thread thread2;
        if (!this.successfullyActivated)
        {
          try
          {
            if (this.WaitingDialog != null)
            {
              if (this.WaitingDialog.InvokeRequired)
              {
                this.WaitingDialog.Invoke((Delegate) new ThreadStart(((Action) (() => this.WaitingDialog.Dispose())).Invoke));
                this.WaitingDialog = (PleaseWait) null;
              }
              else
              {
                this.WaitingDialog.Dispose();
                this.WaitingDialog = (PleaseWait) null;
              }
            }
            thread1.Abort();
            thread2 = (Thread) null;
          }
          catch (Exception ex)
          {
          }
          UniqueError.Message(UniqueError.Number.ACTIVATION_FAILED);
        }
        else
        {
          RegInfoDialog regInfoDialog = new RegInfoDialog();
          try
          {
            if (this.WaitingDialog != null)
            {
              if (this.WaitingDialog.InvokeRequired)
              {
                this.WaitingDialog.Invoke((Delegate) new ThreadStart(((Action) (() => this.WaitingDialog.Dispose())).Invoke));
                this.WaitingDialog = (PleaseWait) null;
              }
              else
              {
                this.WaitingDialog.Dispose();
                this.WaitingDialog = (PleaseWait) null;
              }
            }
            thread1.Abort();
            thread2 = (Thread) null;
          }
          catch (Exception ex)
          {
          }
          if (regInfoDialog.ShowDialog() == DialogResult.OK)
            RegistrationInformation.WriteFile(new RegistrationInformation()
            {
              ContactPerson = regInfoDialog.ContactPerson,
              ComanyName = regInfoDialog.Company,
              Street = regInfoDialog.Street,
              StreetNumber = regInfoDialog.StreetNumber,
              Zip = regInfoDialog.Zip,
              City = regInfoDialog.City,
              Country = regInfoDialog.Country,
              LCID = regInfoDialog.LCID,
              EMail = regInfoDialog.EMail,
              PhoneNumber = regInfoDialog.PhoneNumber
            });
        }
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.START_ACTIVATION, ex, true);
        return false;
      }
      return this.successfullyActivated;
    }

    private PleaseWait WaitingDialog { set; get; }

    private void showPleaseWait()
    {
      try
      {
        this.WaitingDialog = new PleaseWait(this.firstStartLocation, this.firstStartSize);
        int num = (int) this.WaitingDialog.ShowDialog();
      }
      catch (ThreadAbortException ex)
      {
      }
    }

    public bool DoActivation(bool stealthMode = false)
    {
      this.contentToSend = string.Empty;
      this.contentReceived = string.Empty;
      this.successfullyActivated = false;
      if (ActivationInformation.LicenseKey().Length != 29)
        return false;
      try
      {
        this.uniqueHardware = ActivationInformation.UniqueHardwareID();
        string activationDate = ActivationInformation.ActivationDate();
        string hash = HashGenerating.ComputeHash(ActivationInformation.LicenseKey(), HashGenerating.DefaultSaltByte());
        this.contentToSend = "lic=" + hash;
        Activation activation1 = this;
        activation1.contentToSend = activation1.contentToSend + "&hw=" + this.uniqueHardware;
        Activation activation2 = this;
        activation2.contentToSend = activation2.contentToSend + "&time=" + activationDate;
        Activation activation3 = this;
        activation3.contentToSend = activation3.contentToSend + "&version=" + (object) Assembly.GetExecutingAssembly().GetName().Version;
        try
        {
          this.contentReceived = new HttpAccess(Program.URL_FOR_REQUEST, WebSettings.ReadFile()).Request(HttpAccess.RequestCommand.LICENSE, this.contentToSend);
          this.successfullyActivated = ActivationInformation.ActivationWasSuccessful(this.contentReceived, activationDate);
          if (this.successfullyActivated)
          {
            this.sec.EncryptLicenseFile(this.contentReceived + ";" + activationDate + ";" + this.uniqueHardware + ";" + ActivationInformation.LicenseKey() + ";" + ActivationInformation.AdditionalInformation());
            ActivationInformation.LicensedHash = hash;
            ActivationInformation.LicensedDate = activationDate;
            ActivationInformation.LicensedHardwareID = this.uniqueHardware;
            return true;
          }
        }
        catch (WebException ex)
        {
          if (!stealthMode)
            UniqueError.Message(UniqueError.Number.ACTIVATION_WEBEXCEPTION, (Exception) ex);
          GlobalLogger.Instance.WriteLine((Exception) ex);
          return false;
        }
        catch (Exception ex)
        {
          GlobalLogger.Instance.WriteLine(ex);
          return false;
        }
      }
      catch (Exception ex)
      {
        GlobalLogger.Instance.WriteLine(ex);
        return false;
      }
      return this.successfullyActivated;
    }
  }
}
