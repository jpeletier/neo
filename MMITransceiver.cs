// Decompiled with JetBrains decompiler
// Type: ZerroWare.MMITransceiver
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using MMI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UIElements;

namespace ZerroWare
{
  internal class MMITransceiver
  {
    private Communication MMICom;
    private string readAccuVersion;
    private string readMMIVersion;
    private string readMotorVersion;

    public MMITransceiver(Communication MMICom)
    {
      this.MMICom = MMICom;
      this.readAccuVersion = string.Empty;
      this.readMMIVersion = string.Empty;
      this.readMotorVersion = string.Empty;
    }

    public string ReceiveMMISerialNumber()
    {
      this.ReceiveSettingsFromMMI(ParameterIds.MMI_PRODUCTION_INFORMATION);
      return Parameters.Instance.MMISerialNumber;
    }

    public string ReceiveFirmwareVersion(Command_FirmwareTyps type)
    {
      if (string.IsNullOrEmpty(this.readAccuVersion) && string.IsNullOrEmpty(this.readMMIVersion) && string.IsNullOrEmpty(this.readMotorVersion))
      {
        this.ReceiveSettingsFromMMI(ParameterIds.MMI_VERSION_INFORMATION);
        this.ReceiveSettingsFromMMI(ParameterIds.MOTOR_VERSION_INFORMATION);
        this.ReceiveSettingsFromMMI(ParameterIds.ACCU_VERSION_INFORMATION);
        this.readAccuVersion = Parameters.Instance.AccuFirmwareVersion;
        this.readMotorVersion = Parameters.Instance.MotorFirmwareVersion;
        this.readMMIVersion = Parameters.Instance.MMIFirmwareVersion;
      }
      switch (type)
      {
        case Command_FirmwareTyps.BOOTLOADER_MOTOR:
        case Command_FirmwareTyps.FIRMWARE_MOTOR:
          return this.readMotorVersion;
        case Command_FirmwareTyps.FIRMWARE_ACCU:
          return this.readAccuVersion;
        case Command_FirmwareTyps.FIRMWARE_MMI:
          return this.readMMIVersion;
        default:
          return string.Empty;
      }
    }

    public void ResetFirmwareVersionInformation()
    {
      this.readAccuVersion = string.Empty;
      this.readMMIVersion = string.Empty;
      this.readMotorVersion = string.Empty;
    }

    private string[] FirmwareContent { set; get; }

    private byte[] BinaryContent { set; get; }

    public bool ReadBytesFromFile(string filename)
    {
      try
      {
        byte[] numArray = File.ReadAllBytes(filename);
        this.FirmwareContentElements = numArray.Length;
        this.BinaryContent = numArray;
      }
      catch (DirectoryNotFoundException ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE_DIRECTORY_NOT_FOUND, (Exception) ex);
        return false;
      }
      catch (FileLoadException ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE_LOAD, (Exception) ex);
        return false;
      }
      catch (FileNotFoundException ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE_NOT_FOUND, (Exception) ex);
        return false;
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE, ex);
        return false;
      }
      return true;
    }

    public bool ReadLinesFromFirmwareFile(string filename)
    {
      try
      {
        this.FirmwareContent = new string[File.ReadAllLines(filename).Length];
        int index = 0;
        this.FirmwareContentElements = 0;
        using (StreamReader streamReader = File.OpenText(filename))
        {
          string str;
          while ((str = streamReader.ReadLine()) != null)
          {
            this.FirmwareContent[index] = str;
            this.FirmwareContentElements += str.Length;
            ++index;
          }
        }
      }
      catch (DirectoryNotFoundException ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE_DIRECTORY_NOT_FOUND, (Exception) ex);
        return false;
      }
      catch (FileLoadException ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE_LOAD, (Exception) ex);
        return false;
      }
      catch (FileNotFoundException ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE_NOT_FOUND, (Exception) ex);
        return false;
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.TRANSEIVER_READ_FROM_FILE, ex);
        return false;
      }
      return true;
    }

    public int FirmwareContentElements { set; get; }

    public bool RemoveFirmwareFromMMI(Command_FirmwareTyps type)
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareStartTransmission(type));
      if (this.ResponseWasSuccess())
        return true;
      UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_START, type, this.MMICom.Response);
      return false;
    }

    public string ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps type)
    {
      switch (type)
      {
        case Command_FirmwareTyps.FIRMWARE_MOTOR:
        case Command_FirmwareTyps.FIRMWARE_ACCU:
          try
          {
            this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareVersionTransmission(type, SubCommands.GET));
            if (!this.ResponseWasSuccess())
              return "0";
            string str = Convert.ToString((double) Convert.ToInt32(CommandBuilder.Instance.FirmwareVersion) / 10.0).Replace(",", ".");
            if (str.IndexOf('.') < 0 && !str.Equals("0"))
              str += ".0";
            return str;
          }
          catch (Exception ex)
          {
            return "0";
          }
        case Command_FirmwareTyps.ACCU_DFI_FILE:
          try
          {
            this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareVersionTransmission(type, SubCommands.GET));
            if (!this.ResponseWasSuccess())
              return "0x00 0x00";
            byte[] dfiVersion = CommandBuilder.Instance.DFIVersion;
            StringBuilder stringBuilder = new StringBuilder(dfiVersion.Length * 2);
            stringBuilder.AppendFormat("0x{0:X2}", (object) dfiVersion[0]);
            stringBuilder.Append(" ");
            stringBuilder.AppendFormat("0x{0:X2}", (object) dfiVersion[1]);
            return stringBuilder.ToString();
          }
          catch (Exception ex)
          {
            return "0x00 0x00";
          }
        default:
          return "0";
      }
    }

    public bool TransferFirmwareToMMI(
      Command_FirmwareTyps type,
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar,
      bool transferVersion = false,
      string version = null)
    {
      try
      {
        MMI.BinaryContent firmware = type == Command_FirmwareTyps.ACCU_DFI_FILE ? new MMI.BinaryContent(this.BinaryContent) : new MMI.BinaryContent(this.FirmwareContent);
        this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareStartTransmission(type));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_START, type, this.MMICom.Response);
          return false;
        }
        if (transferVersion)
        {
          if (type != Command_FirmwareTyps.FIRMWARE_ACCU)
          {
            if (type != Command_FirmwareTyps.FIRMWARE_MOTOR)
              goto label_8;
          }
          try
          {
            version = version.Replace(".", "");
            this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareVersionTransmission(type, SubCommands.SET, Convert.ToByte(version)));
            if (!this.ResponseWasSuccess())
            {
              UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_VERSION, type, this.MMICom.Response);
              return false;
            }
            goto label_18;
          }
          catch (Exception ex)
          {
            UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_VERSION, type, this.MMICom.Response);
            return false;
          }
        }
label_8:
        if (transferVersion)
        {
          if (type == Command_FirmwareTyps.ACCU_DFI_FILE)
          {
            try
            {
              byte[] numArray = new byte[2]
              {
                Convert.ToByte(version.Substring(2, 2), 16),
                Convert.ToByte(version.Substring(7, 2), 16)
              };
              this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareVersionTransmission(type, SubCommands.SET, numArray[0], numArray[1]));
              if (!this.ResponseWasSuccess())
              {
                UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_VERSION, type, this.MMICom.Response);
                return false;
              }
            }
            catch (Exception ex)
            {
              UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_VERSION, type, this.MMICom.Response);
              return false;
            }
          }
        }
label_18:
        MMI.BinaryContent binaryContent;
        while (firmware.hasMoreBlocks)
        {
          this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareTransmission(type, firmware));
          if (!this.ResponseWasSuccess())
          {
            UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_TRANSMISSION, type, this.MMICom.Response);
            binaryContent = (MMI.BinaryContent) null;
            return false;
          }
          int num = actualPosition * 100 / maximum;
          if (num >= 100)
            num = 98;
          progressBar.Value = num;
          progressBar.Refresh();
          HelperClass.DoEvents();
          ++actualPosition;
        }
        this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareEndTransmission(type));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.TRANSFER_FIRMWARE_END, type, this.MMICom.Response);
          binaryContent = (MMI.BinaryContent) null;
          return false;
        }
        binaryContent = (MMI.BinaryContent) null;
        return true;
      }
      catch (OutOfMemoryException ex)
      {
        UniqueError.Message(UniqueError.Number.SYSTEM_OUT_OF_MEMORY, (Exception) ex, true);
        return false;
      }
    }

    public bool ResponseWasSuccess()
    {
      bool flag = false;
      Thread thread = new Thread(new ThreadStart(this.MMICom.WaitForTimeoutOrResponse));
      thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread.Start();
      while (thread.IsAlive)
        Thread.Sleep(1);
      if (this.MMICom.Response == ParseErrorCodes.SUCCESS)
        flag = true;
      return flag;
    }

    public void ReceiveSettingsFromMMI(ParameterIds paramId)
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.GET, paramId));
      if (!this.ResponseWasSuccess())
      {
        UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ERROR, paramId, this.MMICom.Response);
      }
      else
      {
        try
        {
          // ISSUE: reference to a compiler-generated method
          Parameters.Instance.SetSetOfParameters(paramId, CommandBuilder.Instance.ParameterValues.GetParameterPackage());
        }
        catch (ParameterValueException ex)
        {
          UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ERROR_PARSE, ex, false);
        }
        catch (NullReferenceException ex)
        {
        }
      }
    }

    public bool ReceiveSettingsFromMMI(
      ParameterIds paramId,
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.GET, paramId));
      progressBar.Value = actualPosition * 100 / maximum;
      progressBar.Refresh();
      HelperClass.DoEvents();
      if (!this.ResponseWasSuccess())
      {
        UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ERROR_WITH_BAR, paramId, this.MMICom.Response);
        return false;
      }
      try
      {
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetSetOfParameters(paramId, CommandBuilder.Instance.ParameterValues.GetParameterPackage());
      }
      catch (ParameterValueException ex)
      {
        UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ERROR_PARSE_WITH_BAR, ex, false);
      }
      catch (NullReferenceException ex)
      {
      }
      ++actualPosition;
      return true;
    }

    public bool ReceiveAllSettingsFromMMI(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      foreach (byte num in Enum.GetValues(typeof (ParameterIds)))
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.GET, (ParameterIds) num));
        if (progressBar != null)
        {
          progressBar.Value = actualPosition * 100 / maximum;
          if (!progressBar.InvokeRequired)
            progressBar.Refresh();
          HelperClass.DoEvents();
        }
        if (!this.ResponseWasSuccess())
        {
          if (this.MMICom.Response == ParseErrorCodes.NOTSUPPORTED)
          {
            // ISSUE: reference to a compiler-generated method
            CommandBuilder.Instance.ParameterValues.SetParameterPackage(Parameters.Instance.GetDefaultSetOfParameters((ParameterIds) num));
          }
          else
          {
            UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ALL_ERROR, (ParameterIds) num, this.MMICom.Response);
            return false;
          }
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          Parameters.Instance.SetSetOfParameters((ParameterIds) num, CommandBuilder.Instance.ParameterValues.GetParameterPackage());
        }
        catch (ParameterValueException ex)
        {
          UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ALL_ERROR_PARSE, ex, false);
        }
        ++actualPosition;
      }
      return true;
    }

    public bool ReceiveAllAccessibleSettingsFromMMI(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      foreach (byte paramId in Enum.GetValues(typeof (ParameterIds)))
      {
        if (!this.IgnoreParameterId(paramId))
          this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.GET, (ParameterIds) paramId));
        progressBar.Value = actualPosition * 100 / maximum;
        progressBar.Refresh();
        HelperClass.DoEvents();
        if (!this.IgnoreParameterId(paramId) && !this.ResponseWasSuccess())
        {
          if (this.MMICom.Response == ParseErrorCodes.NOTSUPPORTED)
          {
            // ISSUE: reference to a compiler-generated method
            CommandBuilder.Instance.ParameterValues.SetParameterPackage(Parameters.Instance.GetDefaultSetOfParameters((ParameterIds) paramId));
          }
          else
          {
            UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ALL_ACCESSIBLE_ERROR, (ParameterIds) paramId, this.MMICom.Response);
            return false;
          }
        }
        if (!this.IgnoreParameterId(paramId))
        {
          try
          {
            // ISSUE: reference to a compiler-generated method
            Parameters.Instance.SetSetOfParameters((ParameterIds) paramId, CommandBuilder.Instance.ParameterValues.GetParameterPackage());
          }
          catch (ParameterValueException ex)
          {
            UniqueError.Message(UniqueError.Number.PARAMETER_RECEIVE_ALL_ERROR_ACCESSIBLE_PARSE, ex, false);
          }
        }
        ++actualPosition;
      }
      return true;
    }

    public bool TransferSettingToMMI(
      ParameterIds paramId,
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      // ISSUE: reference to a compiler-generated method
      ParamterTranmissionPackage parameter = new ParamterTranmissionPackage(Parameters.Instance.GetSetOfParameters(paramId));
      this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.SET, paramId, parameter));
      progressBar.Value = actualPosition * 100 / maximum;
      progressBar.Refresh();
      HelperClass.DoEvents();
      if (!this.ResponseWasSuccess() && this.MMICom.Response != ParseErrorCodes.NOTSUPPORTED)
      {
        UniqueError.Message(UniqueError.Number.PARAMETER_TRANSMISSION_ERROR_WITH_BAR, paramId, this.MMICom.Response);
        return false;
      }
      ++actualPosition;
      return true;
    }

    public void TransferSettingToMMI(ParameterIds paramId)
    {
      // ISSUE: reference to a compiler-generated method
      ParamterTranmissionPackage parameter = new ParamterTranmissionPackage(Parameters.Instance.GetSetOfParameters(paramId));
      this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.SET, paramId, parameter));
      if (this.ResponseWasSuccess() || this.MMICom.Response == ParseErrorCodes.NOTSUPPORTED)
        return;
      UniqueError.Message(UniqueError.Number.PARAMETER_TRANSMISSION_ERROR, paramId, this.MMICom.Response);
    }

    public bool TransferAllAccessibleSettingsToMMI(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      foreach (byte paramId in Enum.GetValues(typeof (ParameterIds)))
      {
        if (!this.IgnoreParameterId(paramId))
        {
          // ISSUE: reference to a compiler-generated method
          ParamterTranmissionPackage parameter = new ParamterTranmissionPackage(Parameters.Instance.GetSetOfParameters((ParameterIds) paramId));
          this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.SET, (ParameterIds) paramId, parameter));
          if (!this.ResponseWasSuccess() && this.MMICom.Response != ParseErrorCodes.NOTSUPPORTED)
          {
            UniqueError.Message(UniqueError.Number.PARAMETER_TRANSMISSION_ALL_ACCESSIBLE_ERROR_WITH_BAR, (ParameterIds) paramId, this.MMICom.Response);
            return false;
          }
        }
        progressBar.Value = actualPosition * 100 / maximum;
        progressBar.Refresh();
        HelperClass.DoEvents();
        ++actualPosition;
      }
      return true;
    }

    public bool TransferAllSettingsToMMI(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      foreach (byte num in Enum.GetValues(typeof (ParameterIds)))
      {
        // ISSUE: reference to a compiler-generated method
        ParamterTranmissionPackage parameter = new ParamterTranmissionPackage(Parameters.Instance.GetSetOfParameters((ParameterIds) num));
        this.MMICom.SendMessage(CommandBuilder.Instance.Parameter(SubCommands.SET, (ParameterIds) num, parameter));
        if (!this.ResponseWasSuccess() && this.MMICom.Response != ParseErrorCodes.NOTSUPPORTED)
        {
          UniqueError.Message(UniqueError.Number.PARAMETER_TRANSMISSION_ALL_ERROR_WITH_BAR, (ParameterIds) num, this.MMICom.Response);
          return false;
        }
        progressBar.Value = actualPosition * 100 / maximum;
        progressBar.Refresh();
        HelperClass.DoEvents();
        ++actualPosition;
      }
      return true;
    }

    private bool IgnoreParameterId(byte paramId) => !ActivationInformation.IsAllAccessActive && ((paramId == (byte) 1 || paramId == (byte) 9 || paramId == (byte) 10) && ActivationInformation.VersionLevelProperty < 4 || (paramId == (byte) 12 || paramId == (byte) 17 || (paramId == (byte) 20 || paramId == (byte) 22) || (paramId == (byte) 4 || paramId == (byte) 7 || (paramId == (byte) 8 || paramId == (byte) 13))) || (paramId == (byte) 16 || paramId == (byte) 18 || (paramId == (byte) 21 || paramId == (byte) 6)));

    public bool DeleteStartScreenFromMMI(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      PictureIds pictureIds = PictureIds.STARTSCREEN;
      try
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.PictureStartTransmission(pictureIds));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.PICTURE_START_TRANMISSION, pictureIds, this.MMICom.Response);
          return false;
        }
        progressBar.Value = actualPosition * 100 / maximum;
        progressBar.Refresh();
        HelperClass.DoEvents();
        ++actualPosition;
        return true;
      }
      catch (OutOfMemoryException ex)
      {
        UniqueError.Message(UniqueError.Number.SYSTEM_OUT_OF_MEMORY, (Exception) ex, true);
        return false;
      }
    }

    public bool TransferStartScreenToMMI(
      byte[] pictureContent,
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar)
    {
      PictureIds pictureIds = PictureIds.STARTSCREEN;
      try
      {
        MMI.BinaryContent picture = new MMI.BinaryContent(pictureContent);
        this.MMICom.SendMessage(CommandBuilder.Instance.PictureStartTransmission(pictureIds));
        MMI.BinaryContent binaryContent;
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.PICTURE_START_TRANMISSION, pictureIds, this.MMICom.Response);
          binaryContent = (MMI.BinaryContent) null;
          return false;
        }
        while (picture.hasMoreBlocks)
        {
          this.MMICom.SendMessage(CommandBuilder.Instance.PictureTransmission(pictureIds, picture));
          if (!this.ResponseWasSuccess())
          {
            UniqueError.Message(UniqueError.Number.PICTURE_TRANMISSION, pictureIds, this.MMICom.Response);
            binaryContent = (MMI.BinaryContent) null;
            return false;
          }
          progressBar.Value = actualPosition * 100 / maximum;
          progressBar.Refresh();
          HelperClass.DoEvents();
          ++actualPosition;
        }
        this.MMICom.SendMessage(CommandBuilder.Instance.PictureEndTransmission(pictureIds));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.PICTURE_END_TRANMISSION, pictureIds, this.MMICom.Response);
          binaryContent = (MMI.BinaryContent) null;
          return false;
        }
        binaryContent = (MMI.BinaryContent) null;
        return true;
      }
      catch (OutOfMemoryException ex)
      {
        UniqueError.Message(UniqueError.Number.SYSTEM_OUT_OF_MEMORY, (Exception) ex, true);
        return false;
      }
    }

    public bool WaitingForMMISucceeded { set; get; }

    public void WaitingForWritingDataToMemoryOfMMI()
    {
      this.WaitingForMMISucceeded = false;
      this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareFlag(SubCommands.GET));
      if (!this.ResponseWasSuccess())
      {
        UniqueError.Message(UniqueError.Number.WAITING_FIRMWAREFLAG_RECEIVE_ERROR, this.MMICom.Response);
      }
      else
      {
        FirmwareFlags firmwareFlags = CommandBuilder.Instance.FirmwareFlags;
        firmwareFlags.SetFirmwareFlags(FirmwareFlagIds.PARAMETER);
        this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareFlag(SubCommands.SET, firmwareFlags));
        if (!this.ResponseWasSuccess())
          UniqueError.Message(UniqueError.Number.WAITING_FIRMWAREFLAG_TRANSMIT_ERROR, this.MMICom.Response);
        else
          this.WaitingForMMISucceeded = true;
      }
    }

    public bool SetFirmwareFlag(FirmwareFlagIds typeOfFirmware)
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareFlag(SubCommands.GET));
      if (!this.ResponseWasSuccess())
      {
        UniqueError.Message(UniqueError.Number.FIRMWAREFLAG_RECEIVE_ERROR, this.MMICom.Response);
        return false;
      }
      FirmwareFlags firmwareFlags = CommandBuilder.Instance.FirmwareFlags;
      firmwareFlags.SetFirmwareFlags(typeOfFirmware);
      this.MMICom.SendMessage(CommandBuilder.Instance.FirmwareFlag(SubCommands.SET, firmwareFlags));
      if (this.ResponseWasSuccess())
        return true;
      UniqueError.Message(UniqueError.Number.FIRMWAREFLAG_TRANSMIT_ERROR, this.MMICom.Response);
      return false;
    }

    public bool SetUpdateFlag(UpdateFlags doUpdate)
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.UpdateFlag(SubCommands.GET, UpdateFlags.DONTUPDATE));
      if (!this.ResponseWasSuccess())
      {
        UniqueError.Message(UniqueError.Number.UPDATEFLAG_RECEIVE_ERROR, this.MMICom.Response);
        return false;
      }
      if (CommandBuilder.Instance.OperationMode != OperationModes.UPDATE)
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.UpdateFlag(SubCommands.SET, doUpdate));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.UPDATEFLAG_TRANSMIT_ERROR, this.MMICom.Response);
          return false;
        }
      }
      return true;
    }

    public bool DeleteMotorErrorLog()
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferDelete(RingBufferIds.MOTOR_ERROR_LOG));
      if (this.ResponseWasSuccess())
        return true;
      UniqueError.Message(UniqueError.Number.RINGBUFFER_DELETE, RingBufferIds.MOTOR_ERROR_LOG, this.MMICom.Response);
      return false;
    }

    public bool ReadMotorErrorLogRingBuffer(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar,
      ref byte[][] ringBufferContent)
    {
      int num = actualPosition;
      this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferStartTransmission(RingBufferIds.MOTOR_ERROR_LOG));
      if (!this.ResponseWasSuccess())
      {
        UniqueError.Message(UniqueError.Number.RINGBUFFER_START_RECEIVE_ERROR, this.MMICom.Response);
        ringBufferContent = new byte[0][];
        return false;
      }
      while (CommandBuilder.Instance.RingBufferHasMoreContent)
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferTransmission(RingBufferIds.MOTOR_ERROR_LOG));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.RINGBUFFER_RECEIVE_ERROR, this.MMICom.Response);
          ringBufferContent = new byte[0][];
          return false;
        }
        if (progressBar != null)
        {
          progressBar.Value = actualPosition * 100 / maximum;
          if (!progressBar.InvokeRequired)
            progressBar.Refresh();
          HelperClass.DoEvents();
        }
        ++actualPosition;
      }
      actualPosition = num + CommandBuilder.Instance.MotorErrorRingBufferMaximumElements;
      if (progressBar != null)
      {
        progressBar.Value = actualPosition * 100 / maximum;
        if (!progressBar.InvokeRequired)
          progressBar.Refresh();
        HelperClass.DoEvents();
      }
      byte[] ringBufferContent1 = CommandBuilder.Instance.RingBufferContent;
      if (ringBufferContent1.Length == 24)
      {
        ringBufferContent = new byte[0][];
        return true;
      }
      if (ringBufferContent1.Length != CommandBuilder.Instance.MotorErrorRingBufferMaximumElements * 24)
      {
        ringBufferContent = new byte[0][];
        return false;
      }
      byte[][] numArray = new byte[CommandBuilder.Instance.MotorErrorRingBufferMaximumElements][];
      for (int index = 0; index < numArray.Length; ++index)
      {
        try
        {
          numArray[index] = ((IEnumerable<byte>) ringBufferContent1).Skip<byte>(index * 24).Take<byte>(24).ToArray<byte>();
        }
        catch (ArgumentNullException ex)
        {
          ringBufferContent = new byte[0][];
          return false;
        }
      }
      ringBufferContent = numArray;
      return true;
    }

    public bool DeleteAccuErrorLog()
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferDelete(RingBufferIds.ACCU_ERROR_LOG));
      if (this.ResponseWasSuccess())
        return true;
      UniqueError.Message(UniqueError.Number.RINGBUFFER_DELETE, RingBufferIds.ACCU_ERROR_LOG, this.MMICom.Response);
      return false;
    }

    public bool ReadAccuErrorLogRingBuffer(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar,
      ref byte[] ringBufferContent)
    {
      int num = actualPosition;
      this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferStartTransmission(RingBufferIds.ACCU_ERROR_LOG));
      if (!this.ResponseWasSuccess())
      {
        if (this.MMICom.Response == ParseErrorCodes.NOTSUPPORTED)
        {
          ringBufferContent = new byte[0];
          return true;
        }
        UniqueError.Message(UniqueError.Number.RINGBUFFER_START_RECEIVE_ERROR, this.MMICom.Response);
        ringBufferContent = new byte[0];
        return false;
      }
      while (CommandBuilder.Instance.RingBufferHasMoreContent)
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferTransmission(RingBufferIds.ACCU_ERROR_LOG));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.RINGBUFFER_RECEIVE_ERROR, this.MMICom.Response);
          ringBufferContent = new byte[0];
          return false;
        }
        if (progressBar != null)
        {
          progressBar.Value = actualPosition * 100 / maximum;
          if (!progressBar.InvokeRequired)
            progressBar.Refresh();
          HelperClass.DoEvents();
        }
        ++actualPosition;
      }
      actualPosition = num + CommandBuilder.Instance.AccuErrorRingBufferMaximumElements;
      if (progressBar != null)
      {
        progressBar.Value = actualPosition * 100 / maximum;
        if (!progressBar.InvokeRequired)
          progressBar.Refresh();
        HelperClass.DoEvents();
      }
      byte[] ringBufferContent1 = CommandBuilder.Instance.RingBufferContent;
      if (ringBufferContent1.Length == 24)
      {
        ringBufferContent = new byte[0];
        return true;
      }
      if (ringBufferContent1.Length != CommandBuilder.Instance.AccuErrorRingBufferMaximumElements * 24)
      {
        ringBufferContent = new byte[0];
        return false;
      }
      byte[] numArray = new byte[49];
      Buffer.BlockCopy((Array) ringBufferContent1, 0, (Array) numArray, 0, numArray.Length);
      ringBufferContent = numArray;
      return true;
    }

    public bool ReadTourDataRingBuffer(
      ref int actualPosition,
      int maximum,
      ZerroProgressBar progressBar,
      ref byte[] tourDataBufferContent)
    {
      this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferStartTransmission(RingBufferIds.TOUR_DATA));
      if (!this.ResponseWasSuccess())
      {
        if (this.MMICom.Response == ParseErrorCodes.NOTSUPPORTED)
        {
          tourDataBufferContent = new byte[0];
          return true;
        }
        UniqueError.Message(UniqueError.Number.RINGBUFFER_START_RECEIVE_ERROR, this.MMICom.Response);
        tourDataBufferContent = new byte[0];
        return false;
      }
      while (CommandBuilder.Instance.RingBufferHasMoreContent)
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.RingBufferTransmission(RingBufferIds.TOUR_DATA));
        if (!this.ResponseWasSuccess())
        {
          UniqueError.Message(UniqueError.Number.RINGBUFFER_RECEIVE_ERROR, this.MMICom.Response);
          tourDataBufferContent = new byte[0];
          return false;
        }
        if (progressBar != null)
        {
          progressBar.Value = actualPosition * 100 / maximum;
          if (!progressBar.InvokeRequired)
            progressBar.Refresh();
          HelperClass.DoEvents();
        }
        ++actualPosition;
      }
      if (progressBar != null)
      {
        progressBar.Value = actualPosition * 100 / maximum;
        if (!progressBar.InvokeRequired)
          progressBar.Refresh();
        HelperClass.DoEvents();
      }
      byte[] ringBufferContent = CommandBuilder.Instance.RingBufferContent;
      tourDataBufferContent = ringBufferContent;
      return true;
    }
  }
}
