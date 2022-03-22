// Decompiled with JetBrains decompiler
// Type: ZerroWare.HelperClass
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ZerroWare
{
  internal static class HelperClass
  {
    public static readonly string InchAbbr = "in";
    public static readonly string MilesPerHourAbbr = "mph";
    public static readonly string MilesAbbr = "mi";
    public static readonly double CentimetersToInchFactor = 0.393700787;
    public static readonly double KilometersToMilesFactor = 0.621371192;
    public static readonly string ValidCharacters = "^([A-Za-z0-9äöüÄÖÜß_\\+\\-=.:,/\\ #*~{}\\[\\]\\(\\)]){0,}$";

    public static string ConvertSecondsToReadable(int seconds)
    {
      TimeSpan timeSpan = TimeSpan.FromSeconds((double) seconds);
      return string.Format("{0:D2}d {1:D2}h {2:D2}m {3:D2}s", (object) timeSpan.Days, (object) timeSpan.Hours, (object) timeSpan.Minutes, (object) timeSpan.Seconds);
    }

    public static string ConvertSecondsToReadable(string seconds)
    {
      try
      {
        return HelperClass.ConvertSecondsToReadable(Convert.ToInt32(seconds));
      }
      catch (Exception ex)
      {
        return HelperClass.ConvertSecondsToReadable(Convert.ToInt32(0));
      }
    }

    public static string AddSpacesToSerialNumber(string serial)
    {
      for (int length = serial.Length; length > 3; length -= 3)
        serial = serial.Insert(length - 3, " ");
      return serial;
    }

    public static string AddThousandsSeparatorToNumber(string number)
    {
      int length = number.Length;
      string str;
      switch (MainWindow.Instance.CurrentCulture().Name)
      {
        case "en-US":
          str = ",";
          break;
        case "de-DE":
          str = ".";
          break;
        default:
          str = ",";
          break;
      }
      for (; length > 3; length -= 3)
        number = number.Insert(length - 3, str);
      return number;
    }

    [STAThread]
    public static Image AddLeftSpaceToImage(Image resourceImage, int spacer)
    {
      int width = resourceImage != null ? resourceImage.Width : throw new ArgumentNullException("image");
      int height = resourceImage.Height;
      Bitmap bitmap = new Bitmap(width + spacer, height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.Clear(Color.Transparent);
        graphics.DrawImage(resourceImage, new Rectangle(spacer, 0, width, height));
      }
      return (Image) bitmap;
    }

    [STAThread]
    public static Image AddRightSpaceToImage(Image resourceImage, int spacer)
    {
      int width = resourceImage != null ? resourceImage.Width : throw new ArgumentNullException("image");
      int height = resourceImage.Height;
      Bitmap bitmap = new Bitmap(width + spacer, height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.Clear(Color.Transparent);
        graphics.DrawImage(resourceImage, new Rectangle(0, 0, width, height));
      }
      return (Image) bitmap;
    }

    [STAThread]
    public static Image ResizeImage(Image resourceImage, Size newSize, bool aspectRatio = false)
    {
      if (resourceImage == null)
        throw new ArgumentNullException("image");
      if (aspectRatio)
      {
        int width = resourceImage.Width;
        int height = resourceImage.Height;
        float num1 = (float) newSize.Width / (float) width;
        float num2 = (float) newSize.Height / (float) height;
        float num3 = (double) num2 < (double) num1 ? num2 : num1;
        newSize.Width = (int) ((double) width * (double) num3);
        newSize.Height = (int) ((double) height * (double) num3);
      }
      return (Image) new Bitmap(resourceImage, newSize);
    }

    [STAThread]
    public static Image RotateImage(Image image, PointF offset, float angle)
    {
      if (image == null)
        throw new ArgumentNullException(nameof (image));
      Bitmap bitmap = new Bitmap(image.Width, image.Height);
      bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      graphics.TranslateTransform(offset.X, offset.Y);
      graphics.RotateTransform(angle);
      graphics.TranslateTransform(-offset.X, -offset.Y);
      graphics.DrawImage(image, new PointF(0.0f, 0.0f));
      return (Image) bitmap;
    }

    public static byte ClearBitInByte(byte valueToChange, int positionToClear)
    {
      if (positionToClear < 0 || positionToClear > 7)
        throw new ArgumentOutOfRangeException("bit out of range");
      return (byte) ((uint) valueToChange & (uint) ~(1 << positionToClear));
    }

    public static byte SetBitInByte(byte valueToChange, int positionToSet)
    {
      if (positionToSet < 0 || positionToSet > 7)
        throw new ArgumentOutOfRangeException("bit out of range");
      return (byte) ((uint) valueToChange | (uint) (1 << positionToSet));
    }

    public static byte ToggleBitInByte(byte valueToChange, int positionToSet)
    {
      if (positionToSet < 0 || positionToSet > 7)
        throw new ArgumentOutOfRangeException("bit out of range");
      return (byte) ((uint) valueToChange ^ (uint) (1 << positionToSet));
    }

    public static byte ChangeBitInByte(byte valueToChange, int positionToSet, bool value)
    {
      if (positionToSet < 0 || positionToSet > 7)
        throw new ArgumentOutOfRangeException("bit out of range");
      return !value ? (byte) ((uint) valueToChange & (uint) ~(1 << positionToSet)) : (byte) ((uint) valueToChange | (uint) (1 << positionToSet));
    }

    public static bool IsBitSetInByte(byte toCheck, int position)
    {
      if (position < 0 || position > 7)
        throw new ArgumentOutOfRangeException("bit out of range");
      return ((int) toCheck & 1 << position) != 0;
    }

    public static bool IsUpToDate(string actualReadableVersion, string newReadableVersion)
    {
      actualReadableVersion = actualReadableVersion.Replace(',', '.');
      newReadableVersion = newReadableVersion.Replace(',', '.');
      string[] versionArray1 = actualReadableVersion.Split('.');
      string[] versionArray2 = newReadableVersion.Split('.');
      int[] intVersion1 = HelperClass.ConvertStringVersionToIntVersion(versionArray1);
      int[] intVersion2 = HelperClass.ConvertStringVersionToIntVersion(versionArray2);
      return intVersion1[0] >= intVersion2[0] && (intVersion1[0] != intVersion2[0] || intVersion1[1] >= intVersion2[1] && (intVersion1[1] != intVersion2[1] || intVersion1[2] >= intVersion2[2] && (intVersion1[2] != intVersion2[2] || intVersion1[3] >= intVersion2[3])));
    }

    private static int[] ConvertStringVersionToIntVersion(string[] versionArray)
    {
      int[] numArray = new int[4];
      try
      {
        numArray[0] = Convert.ToInt32(versionArray[0]);
      }
      catch (Exception ex)
      {
        numArray[0] = 0;
      }
      try
      {
        numArray[1] = Convert.ToInt32(versionArray[1]);
      }
      catch (Exception ex)
      {
        numArray[1] = 0;
      }
      try
      {
        numArray[2] = Convert.ToInt32(versionArray[2]);
      }
      catch (Exception ex)
      {
        numArray[2] = 0;
      }
      try
      {
        numArray[3] = Convert.ToInt32(versionArray[3]);
      }
      catch (Exception ex)
      {
        numArray[3] = 0;
      }
      return numArray;
    }

    public static void CheckIfUpdateIsAvailable()
    {
      string path1 = Directories.Instance.UpdatePath + (object) LatestVersionsFile.Element.NEOSDIAG;
      string empty = string.Empty;
      int sdiagServerVersionId = Program.NEO_SDIAG_SERVER_VERSION_ID;
      string actualReadableVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      string path2 = path1 + "\\" + Directories.Instance.UpdateDescriptionFileName;
      if (!File.Exists(path2))
        return;
      int num1;
      string newReadableVersion;
      try
      {
        string[] strArray = File.ReadAllLines(path2)[0].Split(';');
        num1 = Convert.ToInt32(strArray[0]);
        newReadableVersion = strArray[1];
      }
      catch (FileNotFoundException ex)
      {
        newReadableVersion = actualReadableVersion;
        num1 = sdiagServerVersionId;
      }
      catch (DirectoryNotFoundException ex)
      {
        newReadableVersion = actualReadableVersion;
        num1 = sdiagServerVersionId;
      }
      string str1 = path1 + "\\" + num1.ToString("00000000000");
      if (sdiagServerVersionId < num1 || !HelperClass.IsUpToDate(actualReadableVersion, newReadableVersion))
      {
        string str2 = str1 + "\\Setup.exe";
        if (File.Exists(str2))
        {
          if (MessageBox.Show(GlobalResource.neoSDiag_Update_Available_Message, GlobalResource.neoSDiag_Update_Available_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          {
            Process.Start(str2);
            Environment.Exit(0);
          }
          else
          {
            int num2 = (int) MessageBox.Show(GlobalResource.neoSDiag_Update_Available_Install_Soon_Message, GlobalResource.neoSDiag_Update_Available_Install_Soon_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
        }
      }
      string str3 = path1 + "\\" + sdiagServerVersionId.ToString("00000000000");
      if (!Directory.Exists(path1))
        return;
      foreach (string directory in Directory.GetDirectories(path1))
      {
        if (directory != str1 && directory != str3)
          FileOperation.RecursiveDeleteDirectory(directory);
      }
    }

    public static bool CheckIfOutdated()
    {
      string str1 = Directories.Instance.UpdatePath + (object) LatestVersionsFile.Element.NEOSDIAG;
      string empty = string.Empty;
      int sdiagServerVersionId = Program.NEO_SDIAG_SERVER_VERSION_ID;
      string actualReadableVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      int num;
      string newReadableVersion;
      try
      {
        string[] strArray = File.ReadAllLines(str1 + "\\" + Directories.Instance.UpdateDescriptionFileName)[0].Split(';');
        num = Convert.ToInt32(strArray[0]);
        newReadableVersion = strArray[1];
      }
      catch (FileNotFoundException ex)
      {
        newReadableVersion = actualReadableVersion;
        num = sdiagServerVersionId;
      }
      catch (DirectoryNotFoundException ex)
      {
        newReadableVersion = actualReadableVersion;
        num = sdiagServerVersionId;
      }
      string str2 = str1 + "\\" + num.ToString("00000000000");
      return (sdiagServerVersionId < num || !HelperClass.IsUpToDate(actualReadableVersion, newReadableVersion)) && File.Exists(str2 + "\\Setup.exe");
    }

    public static void UpdateVersionFile()
    {
      LatestVersionsFile content = LatestVersionsFile.ReadFile();
      HelperClass.CheckForDeprecatedFiles(content.ServerVersionID(1));
      content.ServerVersionID(1, Program.NEO_SDIAG_SERVER_VERSION_ID);
      content.ReadableVersionID(1, Assembly.GetExecutingAssembly().GetName().Version.ToString());
      if ((DateTime.Now - content.LastUpdate).Days > Program.MAX_DAYS_BETWEEN_UPDATES)
        Program.ShowUpdateHint = true;
      FirmwareListFile firmwareListFile1 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MOTOR);
      if (firmwareListFile1.Length > 0)
      {
        if (firmwareListFile1.ServerId(0) != -1)
        {
          content.ServerVersionID(2, firmwareListFile1.ServerId(0));
          content.ReadableVersionID(2, firmwareListFile1.Version(0));
        }
        else
        {
          content.ServerVersionID(2, 0);
          content.ReadableVersionID(2, "0.0");
        }
      }
      else
      {
        content.ServerVersionID(2, 0);
        content.ReadableVersionID(2, "0.0");
      }
      FirmwareListFile firmwareListFile2 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.ACCU);
      if (firmwareListFile2.Length > 0)
      {
        if (firmwareListFile2.ServerId(0) != -1)
        {
          content.ServerVersionID(3, firmwareListFile2.ServerId(0));
          content.ReadableVersionID(3, firmwareListFile2.Version(0));
        }
        else
        {
          content.ServerVersionID(3, 0);
          content.ReadableVersionID(3, "0.0");
        }
      }
      else
      {
        content.ServerVersionID(3, 0);
        content.ReadableVersionID(3, "0.0");
      }
      FirmwareListFile firmwareListFile3 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.DFI);
      if (firmwareListFile3.Length > 0)
      {
        if (firmwareListFile3.ServerId(0) != -1)
        {
          content.ServerVersionID(6, firmwareListFile3.ServerId(0));
          content.ReadableVersionID(6, firmwareListFile3.Version(0));
        }
        else
        {
          content.ServerVersionID(6, 0);
          content.ReadableVersionID(6, "0.0");
        }
      }
      else
      {
        content.ServerVersionID(6, 0);
        content.ReadableVersionID(6, "0.0");
      }
      FirmwareListFile firmwareListFile4 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MMI);
      if (firmwareListFile4.Length > 0)
      {
        if (firmwareListFile4.ServerId(0) != -1)
        {
          content.ServerVersionID(4, firmwareListFile4.ServerId(0));
          content.ReadableVersionID(4, firmwareListFile4.Version(0));
        }
        else
        {
          content.ServerVersionID(4, 0);
          content.ReadableVersionID(4, "0.0.0.0");
        }
      }
      else
      {
        content.ServerVersionID(4, 0);
        content.ReadableVersionID(4, "0.0.0.0");
      }
      FirmwareListFile firmwareListFile5 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.CONNECT_MMI);
      if (firmwareListFile5.Length > 0)
      {
        if (firmwareListFile5.ServerId(0) != -1)
        {
          content.ServerVersionID(7, firmwareListFile5.ServerId(0));
          content.ReadableVersionID(7, firmwareListFile5.Version(0));
        }
        else
        {
          content.ServerVersionID(7, 0);
          content.ReadableVersionID(7, "0.0.0.0");
        }
      }
      else
      {
        content.ServerVersionID(7, 0);
        content.ReadableVersionID(7, "0.0.0.0");
      }
      NewsListFile newsListFile = NewsListFile.ReadFile();
      if (newsListFile.Length > 0)
      {
        content.ServerVersionID(0, newsListFile.ServerId(0));
        content.ReadableVersionID(0, newsListFile.Version(0));
      }
      else
      {
        content.ServerVersionID(0, 0);
        content.ReadableVersionID(0, "0.0");
      }
      ListSettingFile listSettingFile = ListSettingFile.ReadFile(Directories.Instance.DefaultProfilesListFileName);
      if (listSettingFile.Length > 0)
      {
        content.ServerVersionID(5, listSettingFile.ServerId);
        content.ReadableVersionID(5, listSettingFile.Version);
      }
      else
      {
        content.ServerVersionID(5, 0);
        content.ReadableVersionID(5, "0.0");
      }
      try
      {
        LatestVersionsFile.WriteFile(content);
      }
      catch (Exception ex)
      {
      }
    }

    public static bool IsChristmasTime()
    {
      DateTime now = DateTime.Now;
      int month1 = 12;
      int day1 = 24;
      DateTime t2_1 = new DateTime(now.Year, month1, day1);
      int month2 = month1;
      int day2 = (int) (day1 - 21 - t2_1.DayOfWeek);
      if (day2 < 1)
      {
        --month2;
        day2 = 30 + day2;
      }
      DateTime t2_2 = new DateTime(now.Year, month2, day2);
      return DateTime.Compare(now, t2_2) > 0 && DateTime.Compare(now, t2_1) < 0 || (DateTime.Compare(now, t2_2) == 0 || DateTime.Compare(now, t2_1) == 0);
    }

    public static bool IsEasterTime()
    {
      DateTime now = DateTime.Now;
      DateTime dateTime = HelperClass.EasterSunday(now.Year);
      DateTime t2_1 = dateTime.AddDays(-7.0);
      DateTime t2_2 = dateTime.AddDays(7.0);
      return DateTime.Compare(now, t2_1) > 0 && DateTime.Compare(now, t2_2) < 0 || (DateTime.Compare(now, t2_1) == 0 || DateTime.Compare(now, t2_2) == 0);
    }

    public static DateTime EasterSunday(int year)
    {
      int num1 = year / 100;
      int num2 = year - 19 * (year / 19);
      int num3 = (num1 - 17) / 25;
      int num4 = num1 - num1 / 4 - (num1 - num3) / 3 + 19 * num2 + 15;
      int num5 = num4 - 30 * (num4 / 30);
      int num6 = num5 - num5 / 28 * ((1 - num5 / 28) * (29 / (num5 + 1)) * ((21 - num2) / 11));
      int num7 = year + year / 4 + num6 + 2 - num1 + num1 / 4;
      int num8 = num7 - 7 * (num7 / 7);
      int num9 = num6 - num8;
      int month = 3 + (num9 + 40) / 44;
      int day = num9 + 28 - 31 * (month / 4);
      return new DateTime(year, month, day);
    }

    public static string GetRelativePath(string p_fullDestinationPath, string p_startPath)
    {
      string[] strArray1 = Path.GetFullPath(p_startPath).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
      string[] strArray2 = p_fullDestinationPath.Split(Path.DirectorySeparatorChar);
      int index1 = 0;
      while (index1 < strArray1.Length && index1 < strArray2.Length && strArray1[index1].Equals(strArray2[index1], StringComparison.InvariantCultureIgnoreCase))
        ++index1;
      if (index1 == 0)
        return p_fullDestinationPath;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index2 = index1; index2 < strArray1.Length; ++index2)
        stringBuilder.Append(".." + (object) Path.DirectorySeparatorChar);
      for (int index2 = index1; index2 < strArray2.Length; ++index2)
        stringBuilder.Append(strArray2[index2] + (object) Path.DirectorySeparatorChar);
      --stringBuilder.Length;
      return stringBuilder.ToString();
    }

    public static void DoEvents()
    {
      Thread.Sleep(0);
      Application.DoEvents();
      Thread.Sleep(0);
    }

    public static string RandomString(int length)
    {
      StringBuilder stringBuilder = new StringBuilder();
      Random random = new Random((int) DateTime.Now.Ticks);
      for (int index = 0; index < length; ++index)
      {
        char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0)));
        stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }

    public static void CheckForDeprecatedFiles(int savedServerID)
    {
    }

    public static void SelfUpdateHint()
    {
      string str1 = Directories.Instance.UpdatePath + (object) LatestVersionsFile.Element.NEOSDIAG;
      string str2 = Convert.ToInt32(File.ReadAllLines(str1 + "\\" + Directories.Instance.UpdateDescriptionFileName)[0].Split(';')[0]).ToString("00000000000");
      string str3 = str1 + "\\" + str2;
      if (File.Exists(str3 + "\\Setup.exe"))
      {
        if (MessageBox.Show(GlobalResource.neoSDiag_Update_Available_Message, GlobalResource.neoSDiag_Update_Available_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          Process.Start(str3 + "\\Setup.exe");
          Environment.Exit(0);
        }
        else
        {
          int num = (int) MessageBox.Show(GlobalResource.neoSDiag_Update_Available_Install_Soon_Message, GlobalResource.neoSDiag_Update_Available_Install_Soon_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
      NewsListFile content = NewsListFile.ReadFile();
      if (content.WasRead(0))
        return;
      content.RemoveListElement(0);
      NewsListFile.WriteFile(content);
    }

    public static byte[] StringToByteArray(string hex) => Enumerable.Range(0, hex.Length).Where<int>((Func<int, bool>) (x => x % 2 == 0)).Select<int, byte>((Func<int, byte>) (x => Convert.ToByte(hex.Substring(x, 2), 16))).ToArray<byte>();

    public static string UpperCaseHexString(string convert) => convert.ToUpper().Replace('X', 'x');
  }
}
