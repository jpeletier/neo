// Decompiled with JetBrains decompiler
// Type: ZerroWare.UpdateWorker
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZerroWare
{
  public class UpdateWorker
  {
    private Update myUpdate;
    private int totalProgressMaxStep;
    private int totalProgressActStep;
    private LatestVersionsFile latestVersions;
    private LatestVersionsFile realLatestVersions;
    private UpdateInformation[] myUpdateInfo;
    private bool connectionLost;

    public event UpdateWorker.InformationStringHandler InformationAvailable;

    public event UpdateWorker.DataPackageReceivedHandler DataPackageReceived;

    public event UpdateWorker.TotolProgressPercentageHandler TotalProgressPercentage;

    public event UpdateWorker.UpdateDonedHandler UpdateDone;

    public event UpdateWorker.AccessLevelUpdateHanlder AccessLevelUpdate;

    public event UpdateWorker.NewsAvailableHandler NewsAvailable;

    public event UpdateWorker.ConnectionErrorHandler ConnectionError;

    public UpdateWorker()
    {
      this.myUpdate = new Update();
      this.myUpdate.DataPackageReceive += new Update.DataPackageReceivedHandler(this.DataPercentage);
      this.latestVersions = LatestVersionsFile.ReadFile();
      this.realLatestVersions = LatestVersionsFile.ReadFile();
      this.myUpdateInfo = new UpdateInformation[0];
      this.totalProgressMaxStep = Enum.GetNames(typeof (LatestVersionsFile.Element)).Length + 1;
      this.totalProgressActStep = 0;
      this.connectionLost = false;
    }

    public void DoUpdateInStealthMode() => this.DoUpdate(true);

    public void DoUpdate(bool stealthMode = false)
    {
      this.DoSelfUpdate = false;
      UpdateWorkerEventArgs e = new UpdateWorkerEventArgs();
      if (this.UpdateDone != null)
      {
        e.UpdateDone = false;
        this.UpdateDone((object) this, e);
      }
      if (this.DataPackageReceived != null)
      {
        e.DataPercentage = 0;
        this.DataPackageReceived((object) this, e);
      }
      if (this.TotalProgressPercentage != null)
      {
        e.TotalPercentage = this.GetTotalProgressPercentage;
        this.TotalProgressPercentage((object) this, e);
      }
      bool flag = false;
      if (!this.connectionLost)
        flag = this.IsServerReachable(stealthMode);
      this.myUpdate.ConnectionError += new Update.ConnectionErrorInformationHandler(this.HandleConnectionError);
      if (flag)
      {
        if (!this.connectionLost && new Activation().DoActivation() && this.AccessLevelUpdate != null)
        {
          e.AccessLevelUpdate = true;
          this.AccessLevelUpdate((object) this, e);
        }
        if (!this.connectionLost)
        {
          this.DoET(stealthMode);
          this.DoParameterTransmission(stealthMode);
        }
        if (!this.connectionLost)
          this.DoNormalUpdate(stealthMode);
      }
      this.latestVersions.LastUpdate = DateTime.Now;
      try
      {
        LatestVersionsFile.WriteFile(this.latestVersions);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.WORKER_UPDATE_LATESTVERSIONFILE_WRITE_ERROR);
      }
      if (this.UpdateDone != null)
      {
        e.UpdateDone = true;
        if (this.connectionLost || !flag)
          e.ConnectionError = true;
        this.UpdateDone((object) this, e);
      }
      if (this.DataPackageReceived != null)
      {
        e.DataPercentage = 100;
        this.DataPackageReceived((object) this, e);
      }
      if (this.TotalProgressPercentage != null)
      {
        e.TotalPercentage = 100;
        this.TotalProgressPercentage((object) this, e);
      }
      if (!this.connectionLost && this.ConnectionError != null)
      {
        e.ConnectionError = false;
        this.ConnectionError((object) this, e);
      }
      if (flag || this.ConnectionError == null)
        return;
      e.ConnectionError = true;
      this.ConnectionError((object) this, e);
    }

    public bool DoSelfUpdate { set; get; }

    private bool IsServerReachable(bool stealthMode = false)
    {
      UpdateWorkerEventArgs e = new UpdateWorkerEventArgs();
      if (this.InformationAvailable != null)
      {
        e.Information = GlobalResource.UpdateWorker_ConnectingServer + "\n";
        this.InformationAvailable((object) this, e);
      }
      if (this.myUpdate.ServerReachable(stealthMode))
        return true;
      if (this.myUpdate.ServerErrorResponse.StartsWith("NO VALID LICENSE"))
      {
        if (this.InformationAvailable != null)
        {
          e.Information = GlobalResource.UpdateWorker_InvalidLicense + "\n";
          this.InformationAvailable((object) this, e);
        }
      }
      else if (this.InformationAvailable != null)
      {
        e.Information = GlobalResource.UpdateWorker_ServerNotReachable + "\n";
        this.InformationAvailable((object) this, e);
      }
      return false;
    }

    private int GetTotalProgressPercentage => (int) ((double) this.totalProgressActStep / (double) this.totalProgressMaxStep * 100.0);

    public void DoETInStealthMode()
    {
      if (!this.myUpdate.ServerReachable(true) || !new Activation().DoActivation(true))
        return;
      if (this.AccessLevelUpdate != null)
        this.AccessLevelUpdate((object) this, new UpdateWorkerEventArgs()
        {
          AccessLevelUpdate = true
        });
      this.DoET(true);
      this.DoParameterTransmission(true);
    }

    public void DoParameterTransmission(bool stealthMode = false)
    {
      if (!this.myUpdate.ServerReachable(stealthMode))
        return;
      bool flag = false;
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      if (!this.connectionLost)
      {
        foreach (string str in FileOperation.GetFilesWithSearchPattern(string.Format(Directories.Instance.ParameterSettingsLogFileNameWithoutPath, (object) "*", (object) "*"), Directories.Instance.DataPath))
          stringList1.Add(str);
        if (stringList1.Count > 0)
        {
          new FileCompression(Directories.Instance.ParameterBackupLogFileName).UpateZip(stringList1.ToArray());
          new FileCompression(Directories.Instance.ParameterBackupLogFileName + ".zip").UpateZip(Directories.Instance.ParameterBackupLogFileName);
          flag = this.myUpdate.TransmitETData(Directories.Instance.ParameterBackupLogFileName + ".zip");
        }
      }
      if (!flag)
        return;
      stringList2.Add(Directories.Instance.ParameterBackupLogFileName + ".zip");
      stringList2.Add(Directories.Instance.ParameterBackupLogFileName);
      foreach (string filename in stringList2)
        FileOperation.DeleteFile(filename);
      foreach (string source in stringList1)
        FileOperation.MoveFile(source, Directories.Instance.DumpPath);
    }

    private void DoET(bool stealthMode = false)
    {
      UpdateWorkerEventArgs e = new UpdateWorkerEventArgs();
      if (this.InformationAvailable != null)
      {
        e.Information = GlobalResource.UpdateWorker_StartET;
        this.InformationAvailable((object) this, e);
      }
      bool flag = false;
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      if (FileOperation.FileSize(Directories.Instance.NeoBackupLogFileName) > 8)
        FileOperation.RenameFile(Directories.Instance.NeoBackupLogFileName, Directories.Instance.NeoBackupLogFileName + ".bak");
      if (!this.connectionLost)
      {
        if (File.Exists(Directories.Instance.SystemInfoLogFileName))
          stringList1.Add(Directories.Instance.SystemInfoLogFileName);
        if (File.Exists(Directories.Instance.ZerrowareInfoLogFileName))
          stringList1.Add(Directories.Instance.ZerrowareInfoLogFileName);
        if (File.Exists(Directories.Instance.RegistrationInformationFileName))
          stringList1.Add(Directories.Instance.RegistrationInformationFileName);
        if (File.Exists(Directories.Instance.NeoBackupLogFileName))
        {
          string backupLogFileName = Directories.Instance.NeoBackupLogFileName;
          new NeoLicInfo(ActivationInformation.LicenseKey(), ActivationInformation.LicensedHardwareID, ActivationInformation.ActivationDate()).WriteFile();
          new FileCompression(Directories.Instance.NeoBackupLogFileName).UpateZip(Directories.Instance.NeoLicInfoFileName);
          FileOperation.DeleteFile(Directories.Instance.NeoLicInfoFileName);
          stringList1.Add(backupLogFileName);
          stringList2.Add(backupLogFileName);
        }
        if (File.Exists(Directories.Instance.MMILogFileName))
        {
          string mmiLogFileName = Directories.Instance.MMILogFileName;
          string str = mmiLogFileName + ".bak";
          if (File.Exists(str))
          {
            if (MMIErrorLog.AppendDefaultToSpecialFile(str))
            {
              FileOperation.DeleteFile(mmiLogFileName);
              stringList1.Add(str);
              stringList2.Add(str);
            }
          }
          else if (FileOperation.RenameFile(mmiLogFileName, str))
          {
            stringList1.Add(str);
            stringList2.Add(str);
          }
        }
        if (File.Exists(Directories.Instance.FirmwareTranmissionLogFileName))
        {
          string tranmissionLogFileName = Directories.Instance.FirmwareTranmissionLogFileName;
          string str = tranmissionLogFileName + ".bak";
          if (File.Exists(str))
          {
            if (FirmwareTransmission.AppendDefaultToSpecialFile(str))
            {
              FileOperation.DeleteFile(tranmissionLogFileName);
              stringList1.Add(str);
              stringList2.Add(str);
            }
          }
          else if (FileOperation.RenameFile(tranmissionLogFileName, str))
          {
            stringList1.Add(str);
            stringList2.Add(str);
          }
        }
        new FileCompression(Directories.Instance.ETBackupLogFileName).UpateZip(stringList1.ToArray());
        flag = this.myUpdate.TransmitETData(Directories.Instance.ETBackupLogFileName, stealthMode);
      }
      if (flag)
      {
        stringList2.Add(Directories.Instance.ETBackupLogFileName);
        foreach (string filename in stringList2)
          FileOperation.DeleteFile(filename);
        if (this.InformationAvailable != null)
        {
          e.Information = " " + GlobalResource.UpdateWorker_ProcessDone + "\n";
          this.InformationAvailable((object) this, e);
        }
      }
      else if (this.InformationAvailable != null)
      {
        e.Information = " " + GlobalResource.UpdateWorker_ProcessFailed + "\n";
        this.InformationAvailable((object) this, e);
      }
      ++this.totalProgressActStep;
      if (this.TotalProgressPercentage == null)
        return;
      e.TotalPercentage = this.GetTotalProgressPercentage;
      this.TotalProgressPercentage((object) this, e);
    }

    private void DoNormalUpdate(bool stealthMode = false)
    {
      UpdateWorkerEventArgs e = new UpdateWorkerEventArgs();
      foreach (LatestVersionsFile.Element element in Enum.GetValues(typeof (LatestVersionsFile.Element)))
      {
        if (element == LatestVersionsFile.Element.NEOSDIAG)
          this.latestVersions.ServerVersionID((int) element, Program.NEO_SDIAG_SERVER_VERSION_ID);
        if (!this.connectionLost)
          this.Updating(this.myUpdate, element, this.latestVersions.ServerVersionID((int) element), stealthMode);
        ++this.totalProgressActStep;
        if (this.TotalProgressPercentage != null)
        {
          e.TotalPercentage = this.GetTotalProgressPercentage;
          this.TotalProgressPercentage((object) this, e);
        }
      }
      if (this.InformationAvailable == null)
        return;
      e.Information = "\n" + GlobalResource.UpdateWorker_UpdateDone;
      this.InformationAvailable((object) this, e);
    }

    private void DataPercentage(Update update, int percentage)
    {
      if (this.DataPackageReceived == null)
        return;
      this.DataPackageReceived((object) this, new UpdateWorkerEventArgs()
      {
        DataPercentage = percentage
      });
    }

    private void HandleConnectionError(Update sender, string information)
    {
      UpdateWorkerEventArgs e = new UpdateWorkerEventArgs();
      this.connectionLost = true;
      if (this.InformationAvailable != null)
      {
        e.Information = "\n" + GlobalResource.UpdateWorker_LostConnection + "\n";
        this.InformationAvailable((object) this, e);
      }
      if (this.ConnectionError == null)
        return;
      e.ConnectionError = true;
      this.ConnectionError((object) this, e);
    }

    private void Updating(
      Update myUpdate,
      LatestVersionsFile.Element element,
      int version,
      bool stealthMode = false)
    {
      UpdateWorkerEventArgs e = new UpdateWorkerEventArgs();
      if (this.DataPackageReceived != null)
      {
        e.DataPercentage = 0;
        this.DataPackageReceived((object) this, e);
      }
      FileCompression fileCompression = new FileCompression();
      string empty = string.Empty;
      string str;
      switch (element)
      {
        case LatestVersionsFile.Element.NEWS:
          str = GlobalResource.UpdateWorker_Element_News;
          break;
        case LatestVersionsFile.Element.NEOSDIAG:
          str = GlobalResource.UpdateWorker_Element_neosDiag;
          break;
        case LatestVersionsFile.Element.MOTOR:
          version = 0;
          str = GlobalResource.UpdateWorker_Element_Motor;
          break;
        case LatestVersionsFile.Element.ACCU:
          version = 0;
          str = GlobalResource.UpdateWorker_Element_Accu;
          break;
        case LatestVersionsFile.Element.MMI:
          version = 0;
          str = GlobalResource.UpdateWorker_Element_MMI;
          break;
        case LatestVersionsFile.Element.PROFILE:
          str = GlobalResource.UpdateWorker_Element_Profil;
          break;
        case LatestVersionsFile.Element.DFI:
          str = GlobalResource.UpdateWorker_Element_DFI;
          break;
        case LatestVersionsFile.Element.CONNECT_MMI:
          version = 0;
          str = GlobalResource.UpdateWorker_Element_ConnectMMI;
          break;
        default:
          return;
      }
      if (this.InformationAvailable != null)
      {
        e.Information = string.Format(GlobalResource.UpdateWorker_StartUpdate, (object) str);
        this.InformationAvailable((object) this, e);
      }
      int num1 = 0;
      int num2 = 0;
      this.myUpdateInfo = new UpdateInformation[0];
      if (!this.connectionLost)
      {
        num1 = myUpdate.CheckForUpdates(element, version, stealthMode);
        this.myUpdateInfo = myUpdate.GetUpdateInformation();
        foreach (UpdateInformation updateInformation in this.myUpdateInfo)
        {
          if (updateInformation.Version > this.realLatestVersions.ServerVersionID((int) element) && !updateInformation.Critical)
            ++num2;
        }
      }
      if (this.InformationAvailable != null)
      {
        e.Information = string.Format(" " + GlobalResource.UpdateWorker_NumOfUpdates + "\n", (object) num2);
        this.InformationAvailable((object) this, e);
      }
      string fileName = "test.zip";
      string updatePath = Directories.Instance.UpdatePath;
      for (int index = 0; index < num1; ++index)
      {
        if (!this.myUpdateInfo[index].Critical && this.myUpdateInfo[index].Version > this.realLatestVersions.ServerVersionID((int) element))
        {
          if (this.InformationAvailable != null)
          {
            e.Information = string.Format(GlobalResource.UpdateWorker_StartDownload + "\n", (object) str);
            this.InformationAvailable((object) this, e);
          }
          bool flag = false;
          if (!this.connectionLost)
            flag = myUpdate.GetUpdate(index, updatePath, ref fileName, stealthMode);
          if (flag)
          {
            if (this.InformationAvailable != null)
            {
              e.Information = string.Format(GlobalResource.UpdateWorker_DownloadSuccess + "\n", (object) fileName);
              this.InformationAvailable((object) this, e);
            }
            if (this.InformationAvailable != null)
            {
              e.Information = string.Format(GlobalResource.UpdateWorker_StartExtraction, (object) fileName);
              this.InformationAvailable((object) this, e);
            }
            if (fileCompression.ExtractZip(updatePath + fileName, updatePath + (object) element))
            {
              if (this.InformationAvailable != null)
              {
                e.Information = " " + GlobalResource.UpdateWorker_ProcessDone + "\n";
                this.InformationAvailable((object) this, e);
              }
              if (this.DataPackageReceived != null)
              {
                e.DataPercentage = 100;
                this.DataPackageReceived((object) this, e);
              }
              this.ParseAndMoveUpdateFile(element, updatePath + (object) element, this.myUpdateInfo[index].Version, this.myUpdateInfo[index].VersionText);
              try
              {
                LatestVersionsFile.WriteFile(this.latestVersions);
              }
              catch (Exception ex)
              {
                UniqueError.Message(UniqueError.Number.LATESTVERSIONFILE_WRITE_ERROR);
              }
            }
            else if (this.InformationAvailable != null)
            {
              e.Information = " " + GlobalResource.UpdateWorker_ProcessFailed + "\n";
              this.InformationAvailable((object) this, e);
            }
            try
            {
              File.Delete(updatePath + fileName);
            }
            catch (IOException ex)
            {
            }
          }
          else if (this.InformationAvailable != null)
          {
            e.Information = " " + GlobalResource.UpdateWorker_ProcessFailed + "\n";
            this.InformationAvailable((object) this, e);
          }
        }
        this.FlagCriticalFirmwareVersion(element, index);
      }
    }

    private void ParseAndMoveUpdateFile(
      LatestVersionsFile.Element element,
      string contentPath,
      int serverId,
      string serverReadableVersion)
    {
      string[] strArray1;
      string[] strArray2;
      try
      {
        strArray1 = File.ReadAllLines(contentPath + "\\" + Directories.Instance.UpdateDescriptionFileName);
        strArray2 = strArray1[0].Split(';');
      }
      catch (Exception ex)
      {
        return;
      }
      try
      {
        if (element != LatestVersionsFile.Element.NEOSDIAG)
        {
          this.latestVersions.ServerVersionID((int) element, serverId);
          this.latestVersions.ReadableVersionID((int) element, serverReadableVersion);
        }
      }
      catch (IndexOutOfRangeException ex)
      {
        this.latestVersions.ServerVersionID((int) element, 0);
        this.latestVersions.ReadableVersionID((int) element, "0");
      }
      switch (element)
      {
        case LatestVersionsFile.Element.NEWS:
          string empty1 = string.Empty;
          string text = string.Empty;
          try
          {
            empty1 = strArray1[1];
            for (int index = 2; index < strArray1.Length; ++index)
              text = text + strArray1[index] + "\n";
          }
          catch (IndexOutOfRangeException ex)
          {
            string empty2 = string.Empty;
          }
          this.HandelNewsUpdate(element, contentPath, this.latestVersions.ServerVersionID((int) element), this.latestVersions.ReadableVersionID((int) element), empty1, text);
          if (this.NewsAvailable == null)
            break;
          this.NewsAvailable((object) this, new UpdateWorkerEventArgs()
          {
            NewsAvailable = true
          });
          break;
        case LatestVersionsFile.Element.NEOSDIAG:
          this.HandleNeoSmartDiagnosticUpdate(contentPath, Convert.ToInt32(strArray2[0]));
          break;
        case LatestVersionsFile.Element.MOTOR:
        case LatestVersionsFile.Element.ACCU:
        case LatestVersionsFile.Element.MMI:
        case LatestVersionsFile.Element.DFI:
        case LatestVersionsFile.Element.CONNECT_MMI:
          string empty3 = string.Empty;
          string description;
          try
          {
            description = empty3 + strArray2[2];
            for (int index = 1; index < strArray1.Length; ++index)
              description = description + "\n" + strArray1[index];
          }
          catch (IndexOutOfRangeException ex)
          {
            description = string.Empty;
          }
          this.HandelFirmwareUpdate(element, contentPath, this.latestVersions.ServerVersionID((int) element), this.latestVersions.ReadableVersionID((int) element), description);
          break;
        case LatestVersionsFile.Element.PROFILE:
          this.HandelProfileUpdate(element, contentPath, this.latestVersions.ServerVersionID((int) element), this.latestVersions.ReadableVersionID((int) element));
          break;
      }
    }

    private void HandleNeoSmartDiagnosticUpdate(string contentPath, int serverVersion)
    {
      FileCompression fileCompression = new FileCompression();
      string str = LatestVersionsFile.ElementAsString(LatestVersionsFile.Element.NEOSDIAG) + ".zip";
      try
      {
        FileOperation.CreateDirectory(contentPath + "\\" + serverVersion.ToString("00000000000"));
        fileCompression.ExtractZip(contentPath + "\\" + str, contentPath + "\\" + serverVersion.ToString("00000000000"));
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_NEO_SMART_DIAGNOSTIC_UPDATE, ex);
      }
      try
      {
        File.Delete(contentPath + "\\" + str);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_NEO_SMART_DIAGNOSTIC_UPDATE_DELETE, ex);
      }
      this.DoSelfUpdate = true;
    }

    private void HandelNewsUpdate(
      LatestVersionsFile.Element element,
      string contentPath,
      int serverVersion,
      string readableVersion,
      string caption,
      string text)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (element != LatestVersionsFile.Element.NEWS)
        return;
      string newsPath = Directories.Instance.NewsPath;
      NewsListFile content = NewsListFile.ReadFile();
      string str1 = serverVersion.ToString("00000000000");
      FileCompression fileCompression = new FileCompression();
      string str2 = LatestVersionsFile.ElementAsString(element) + ".zip";
      try
      {
        foreach (string zipWithFile in fileCompression.ExtractZipWithFileList(contentPath + "\\" + str2, newsPath + str1 + "\\"))
        {
          if (zipWithFile.Contains(".pdf"))
            content.AddListElement(serverVersion, readableVersion, caption, text, str1 + "\\" + zipWithFile);
        }
        try
        {
          NewsListFile.WriteFile(content);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.LIST_WRITE_ERROR);
        }
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_NEWS_UPDATE, ex);
      }
      try
      {
        Directory.Delete(contentPath, true);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_NEWS_UPDATE_DELETE, ex);
      }
    }

    private void HandelProfileUpdate(
      LatestVersionsFile.Element element,
      string contentPath,
      int serverVersion,
      string readableVersion)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (element != LatestVersionsFile.Element.PROFILE)
        return;
      string defaultsPath = Directories.Instance.DefaultsPath;
      FileCompression fileCompression = new FileCompression();
      string str = LatestVersionsFile.ElementAsString(element) + ".zip";
      try
      {
        fileCompression.ExtractZipWithFileList(contentPath + "\\" + str, defaultsPath + "\\");
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_PROFILE_UPDATE, ex);
      }
      try
      {
        Directory.Delete(contentPath, true);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_PROFILE_UPDATE_DELETE, ex);
      }
    }

    private void HandelFirmwareUpdate(
      LatestVersionsFile.Element element,
      string contentPath,
      int serverVersion,
      string readableVersion,
      string description)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string str1;
      switch (element)
      {
        case LatestVersionsFile.Element.MOTOR:
          str1 = Directories.Instance.MotorFirmwarePath;
          break;
        case LatestVersionsFile.Element.ACCU:
          str1 = Directories.Instance.BatteryFirmwarePath;
          break;
        case LatestVersionsFile.Element.MMI:
          str1 = Directories.Instance.MMIFirmwarePath;
          break;
        case LatestVersionsFile.Element.PROFILE:
          return;
        case LatestVersionsFile.Element.DFI:
          str1 = Directories.Instance.DFIFirmwarePath;
          break;
        case LatestVersionsFile.Element.CONNECT_MMI:
          str1 = Directories.Instance.ConnectMMIFirmwarePath;
          break;
        default:
          return;
      }
      string filename = str1 + Directories.Instance.FirmwareListFileName;
      FirmwareListFile content = FirmwareListFile.ReadFile(filename);
      if (element == LatestVersionsFile.Element.DFI)
      {
        for (int position = content.Length - 1; position >= 0; --position)
        {
          if (content.FirmwareInformations[position].ServerID < serverVersion && content.FirmwareInformations[position].ServerID > 0)
          {
            FileOperation.RecursiveDeleteDirectory(str1 + content.FirmwareInformations[position].ServerID.ToString("00000000000"));
            content.RemoveListElement(position);
          }
        }
      }
      string str2 = serverVersion.ToString("00000000000");
      FileCompression fileCompression = new FileCompression();
      string str3 = LatestVersionsFile.ElementAsString(element) + ".zip";
      try
      {
        FileOperation.CreateDirectory(str1 + str2 + "\\");
        foreach (string zipWithFile in fileCompression.ExtractZipWithFileList(contentPath + "\\" + str3, str1 + str2 + "\\"))
        {
          if (zipWithFile.Contains(".hex"))
            content.AddListElement(description, str2 + "\\" + zipWithFile, serverVersion, readableVersion);
          else if (zipWithFile.Contains(".dfi"))
          {
            readableVersion = zipWithFile.Substring(0, 4);
            readableVersion = "0x" + readableVersion;
            readableVersion = readableVersion.Insert(4, " 0x");
            content.AddListElement(description, str2 + "\\" + zipWithFile, serverVersion, readableVersion);
          }
        }
        try
        {
          FirmwareListFile.WriteFile(content, filename);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.UPDATE_LIST_WRITE_ERROR);
        }
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_FIRMWARE_UPDATE, ex);
      }
      try
      {
        Directory.Delete(contentPath, true);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.HANDLE_FIRMWARE_UPDATE_DELETE, ex);
      }
    }

    private void FlagCriticalFirmwareVersion(
      LatestVersionsFile.Element element,
      int positionInUpdateInfo)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string str;
      switch (element)
      {
        case LatestVersionsFile.Element.MOTOR:
          str = Directories.Instance.MotorFirmwarePath;
          break;
        case LatestVersionsFile.Element.ACCU:
          str = Directories.Instance.BatteryFirmwarePath;
          break;
        case LatestVersionsFile.Element.MMI:
          str = Directories.Instance.MMIFirmwarePath;
          break;
        case LatestVersionsFile.Element.PROFILE:
          return;
        case LatestVersionsFile.Element.DFI:
          str = Directories.Instance.DFIFirmwarePath;
          break;
        default:
          return;
      }
      string filename = str + Directories.Instance.FirmwareListFileName;
      FirmwareListFile content = FirmwareListFile.ReadFile(filename);
      bool flag = false;
      for (int index = 0; index < content.FirmwareInformations.Length; ++index)
      {
        if (this.myUpdateInfo[positionInUpdateInfo].Version == content.FirmwareInformations[index].ServerID)
        {
          content.FirmwareInformations[index].Critical = this.myUpdateInfo[positionInUpdateInfo].Critical;
          content.FirmwareInformations[index].Warning = this.myUpdateInfo[positionInUpdateInfo].Warning;
          flag = true;
        }
      }
      if (!flag)
        return;
      FirmwareListFile.WriteFile(content, filename);
    }

    public delegate void InformationStringHandler(object sender, UpdateWorkerEventArgs e);

    public delegate void DataPackageReceivedHandler(object sender, UpdateWorkerEventArgs e);

    public delegate void TotolProgressPercentageHandler(object sender, UpdateWorkerEventArgs e);

    public delegate void UpdateDonedHandler(object sender, UpdateWorkerEventArgs e);

    public delegate void AccessLevelUpdateHanlder(object sender, UpdateWorkerEventArgs e);

    public delegate void NewsAvailableHandler(object sender, UpdateWorkerEventArgs e);

    public delegate void ConnectionErrorHandler(object sender, UpdateWorkerEventArgs e);
  }
}
