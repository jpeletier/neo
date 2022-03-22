// Decompiled with JetBrains decompiler
// Type: ZerroWare.ActivationInformation
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Management;
using System.Windows.Forms;

namespace ZerroWare
{
  internal class ActivationInformation
  {
    private static int versionLevel = 0;
    private static int oemPartnerId = 0;
    private static string licenseKey = string.Empty;
    private static string additionalInformation = string.Empty;
    private static bool allAccessActive = false;

    public static string LicensedHardwareID { set; get; }

    public static string LicensedHash { set; get; }

    public static string LicensedDate { set; get; }

    public static int VersionLevelProperty
    {
      set => ActivationInformation.versionLevel = value;
      get => ActivationInformation.versionLevel;
    }

    public static string AdditionalInformationProperty
    {
      set => ActivationInformation.additionalInformation = value;
      get => ActivationInformation.additionalInformation;
    }

    public static int OemPartnerIdProperty
    {
      set => ActivationInformation.oemPartnerId = value;
      get => ActivationInformation.oemPartnerId;
    }

    public static string UniqueHardwareID()
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      bool flag = false;
      try
      {
        using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * From Win32_processor"))
        {
          using (ManagementObjectCollection objectCollection = managementObjectSearcher.Get())
          {
            foreach (ManagementObject managementObject in objectCollection)
            {
              try
              {
                str2 = managementObject["ProcessorID"].ToString();
              }
              catch (NullReferenceException ex)
              {
                str2 = "DEADDEADBEEF8BAD";
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        str2 = HelperClass.RandomString(10);
        flag = true;
      }
      try
      {
        using (ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"c:\""))
        {
          managementObject.Get();
          str1 = managementObject["VolumeSerialNumber"].ToString();
        }
      }
      catch (Exception ex1)
      {
        try
        {
          using (ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + Environment.GetFolderPath(Environment.SpecialFolder.Windows).Split('\\')[0] + "\""))
          {
            managementObject.Get();
            str1 = managementObject["VolumeSerialNumber"].ToString();
          }
        }
        catch (Exception ex2)
        {
          str1 = HelperClass.RandomString(10);
        }
      }
      try
      {
        using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard"))
        {
          using (ManagementObjectCollection objectCollection = managementObjectSearcher.Get())
          {
            foreach (ManagementBaseObject managementBaseObject in objectCollection)
              str3 = (string) managementBaseObject["SerialNumber"];
          }
        }
      }
      catch (Exception ex)
      {
        str3 = HelperClass.RandomString(10);
      }
      if (flag)
      {
        GlobalLogger.Instance.Close();
        Application.Exit();
        int num = (int) MessageBox.Show(GlobalResource.GuestUser_ApplicationExit_Message, GlobalResource.GuestUser_ApplicationExit_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Environment.Exit(-1);
      }
      return str3 + "_" + str2 + "_" + str1;
    }

    public static void SetLicenseKey(string newLicenseKey) => ActivationInformation.licenseKey = newLicenseKey;

    public static string LicenseKey() => ActivationInformation.licenseKey;

    public static string VersionLevel() => ActivationInformation.versionLevel.ToString("00000");

    public static string OemPartnerId() => ActivationInformation.oemPartnerId.ToString("00000");

    public static string SpecialKey() => "tiSkFUAGmbHActivation";

    public static string ActivationDate() => DateTime.Now.ToString("yyyyMMddHHmmssfff");

    public static string AdditionalInformation() => ActivationInformation.additionalInformation;

    public static string ValidationKey(
      string versionLevel,
      string oemPartnerID,
      string validationTime)
    {
      string empty = string.Empty;
      return ActivationInformation.UniqueHardwareID() + "_" + validationTime + "_" + ActivationInformation.SpecialKey() + "_" + ActivationInformation.LicenseKey() + "_" + versionLevel + "_" + oemPartnerID;
    }

    public static bool ActivationWasSuccessful(string serverAnswer, string activationDate)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      string empty4 = string.Empty;
      string[] strArray1 = serverAnswer.Split('\n');
      string str1;
      try
      {
        str1 = strArray1[0];
      }
      catch (IndexOutOfRangeException ex)
      {
        GlobalLogger.Instance.WriteLine((Exception) ex);
        return false;
      }
      if (str1.StartsWith("NO VALID LICENSE"))
      {
        ActivationInformation.versionLevel = 0;
        ActivationInformation.oemPartnerId = 0;
        return false;
      }
      string[] strArray2 = str1.Split('%');
      string str2;
      string oemPartnerID;
      string versionLevel;
      try
      {
        str2 = strArray2[0];
        oemPartnerID = strArray2[1].Replace(" ", "");
        versionLevel = strArray2[2].Replace(" ", "");
        ActivationInformation.additionalInformation = strArray2[3];
      }
      catch (IndexOutOfRangeException ex)
      {
        GlobalLogger.Instance.WriteLine((Exception) ex);
        return false;
      }
      ActivationInformation.oemPartnerId = Convert.ToInt32(oemPartnerID);
      ActivationInformation.versionLevel = Convert.ToInt32(versionLevel);
      return HashGenerating.VerifyHash(ActivationInformation.ValidationKey(versionLevel, oemPartnerID, activationDate), str2.Replace(" ", "+"));
    }

    public static bool IsAllAccessActive
    {
      set => ActivationInformation.allAccessActive = value;
      get => ActivationInformation.allAccessActive;
    }
  }
}
