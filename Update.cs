// Decompiled with JetBrains decompiler
// Type: ZerroWare.Update
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ZerroWare
{
  internal class Update
  {
    private UpdateInformation[] updateInfo;
    private string licenseInformation = string.Empty;
    private HttpAccess httpAccess;

    public event Update.DataPackageReceivedHandler DataPackageReceive;

    public event Update.ConnectionErrorInformationHandler ConnectionError;

    public Update()
    {
      this.licenseInformation = "&hw=" + ActivationInformation.LicensedHardwareID + "&time=" + ActivationInformation.LicensedDate + "&lic=" + ActivationInformation.LicensedHash + "&version=" + (object) Assembly.GetExecutingAssembly().GetName().Version;
      this.updateInfo = new UpdateInformation[0];
    }

    public bool ServerReachable(bool silent = false)
    {
      this.httpAccess = new HttpAccess(Program.URL_FOR_REQUEST, WebSettings.ReadFile());
      this.httpAccess.ConnectionError += new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      string str = this.httpAccess.Request(HttpAccess.RequestCommand.PING, this.licenseInformation, silent);
      this.httpAccess.ConnectionError -= new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      this.httpAccess = (HttpAccess) null;
      if (str == "PING")
        return true;
      this.ServerErrorResponse = str;
      return false;
    }

    public int CheckForUpdates(
      LatestVersionsFile.Element updateElement,
      int updateVersion,
      bool silent = false)
    {
      this.httpAccess = new HttpAccess(Program.URL_FOR_REQUEST, WebSettings.ReadFile());
      this.httpAccess.ConnectionError += new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      int checkUpdateResponse = this.ParseCheckUpdateResponse(this.httpAccess.Request(HttpAccess.RequestCommand.CHECK_UPDATE, "upd_cat=" + LatestVersionsFile.ElementAsString(updateElement) + "&upd_version=" + (object) updateVersion + this.licenseInformation, silent));
      this.httpAccess.ConnectionError -= new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      this.httpAccess = (HttpAccess) null;
      return checkUpdateResponse;
    }

    public bool GetUpdate(
      int elementOfUpdateInfo,
      string destinationPath,
      ref string fileName,
      bool silent = false)
    {
      this.httpAccess = new HttpAccess(Program.URL_FOR_REQUEST, WebSettings.ReadFile());
      this.httpAccess.ConnectionError += new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      this.httpAccess.DataPackageReceive += new HttpAccess.DataPackageReceivedHanlder(this.DataPercentage);
      byte[] buffer = this.httpAccess.DataRequest("cmd=get_upd&upd_id=" + (object) this.updateInfo[elementOfUpdateInfo].Id + this.licenseInformation, ref fileName, silent);
      if (buffer == null)
        return false;
      try
      {
        BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream(destinationPath + fileName, FileMode.Create, FileAccess.ReadWrite));
        binaryWriter.Write(buffer);
        binaryWriter.Close();
      }
      catch (IOException ex)
      {
        return false;
      }
      this.httpAccess.DataPackageReceive -= new HttpAccess.DataPackageReceivedHanlder(this.DataPercentage);
      this.httpAccess.ConnectionError -= new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      this.httpAccess = (HttpAccess) null;
      return true;
    }

    public bool TransmitETData(string filename, bool silent = false)
    {
      if (!File.Exists(filename))
        return true;
      this.httpAccess = new HttpAccess(Program.URL_FOR_REQUEST, WebSettings.ReadFile());
      this.httpAccess.ConnectionError += new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      string softwareVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      bool flag = this.httpAccess.TransmitData(ActivationInformation.LicensedHardwareID, ActivationInformation.LicensedDate, ActivationInformation.LicensedHash.Replace('+', ' '), softwareVersion, filename, silent);
      this.httpAccess.ConnectionError -= new HttpAccess.ConnectionErrorInformationHandler(this.HandleConnectionError);
      this.httpAccess = (HttpAccess) null;
      return flag;
    }

    private int ParseCheckUpdateResponse(string response)
    {
      this.updateInfo = new UpdateInformation[0];
      if (response == "NO UPDATES")
        return 0;
      string[] strArray1 = response.Split('!');
      int num = strArray1.Length - 1;
      List<UpdateInformation> updateInformationList = new List<UpdateInformation>();
      for (int index = 0; index < num; ++index)
      {
        string[] strArray2 = strArray1[index].Split('/');
        try
        {
          bool testingOnly;
          try
          {
            testingOnly = Convert.ToInt32(strArray2[6]) != 0;
          }
          catch (FormatException ex)
          {
            testingOnly = false;
          }
          if (testingOnly)
          {
            if (MainWindow.Instance.Testmode)
              updateInformationList.Add(new UpdateInformation(strArray2[0], strArray2[1], strArray2[2], strArray2[3], strArray2[4], strArray2[5], testingOnly));
          }
          else
            updateInformationList.Add(new UpdateInformation(strArray2[0], strArray2[1], strArray2[2], strArray2[3], strArray2[4], strArray2[5], testingOnly));
        }
        catch (IndexOutOfRangeException ex)
        {
        }
      }
      this.updateInfo = updateInformationList.ToArray();
      int length = this.updateInfo.Length;
      Array.Reverse((Array) this.updateInfo);
      return length < 0 ? 0 : length;
    }

    private void DataPercentage(HttpAccess http, int percentage)
    {
      if (this.DataPackageReceive == null)
        return;
      this.DataPackageReceive(this, percentage);
    }

    private void HandleConnectionError(HttpAccess sender, string information)
    {
      if (this.ConnectionError == null)
        return;
      this.ConnectionError(this, information);
    }

    public string ServerErrorResponse { set; get; }

    public UpdateInformation[] GetUpdateInformation() => this.updateInfo;

    public delegate void DataPackageReceivedHandler(Update sender, int percentage);

    public delegate void ConnectionErrorInformationHandler(Update sender, string information);
  }
}
