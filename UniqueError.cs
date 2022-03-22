// Decompiled with JetBrains decompiler
// Type: ZerroWare.UniqueError
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using MMI;
using System;
using System.Windows.Forms;

namespace ZerroWare
{
  internal class UniqueError
  {
    private static Exception errorException = new Exception();
    private static ParameterValueException errorParameterException = new ParameterValueException();
    private static string additionalInformation = string.Empty;
    private static string parameterIdAsString = string.Empty;
    private static string firmwareTypeAsString = string.Empty;
    private static bool waitingForClick = false;

    public static void Message(UniqueError.Number errno, bool dumpToFile = false)
    {
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, 0, 0, 0), dumpToFile);
    }

    public static void Message(UniqueError.Number errno, string additionalInfo, bool dumpToFile = false)
    {
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, 0, 0, 0), dumpToFile);
    }

    public static void Message(UniqueError.Number errno, Exception e, bool dumpToFile = false)
    {
      UniqueError.errorException = e;
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, 0, 0, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      Exception e,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.errorException = e;
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, 0, 0, 0), dumpToFile);
    }

    public static void Message(UniqueError.Number errno, ParseErrorCodes code, bool dumpToFile = false)
    {
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, 0, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      ParseErrorCodes code,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, 0, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      Command_FirmwareTyps type,
      ParseErrorCodes code,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = string.Empty;
      UniqueError.FirmwareTypeAsString(type);
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) type, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      Command_FirmwareTyps type,
      ParseErrorCodes code,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.FirmwareTypeAsString(type);
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) type, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      ParameterIds id,
      ParseErrorCodes code,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ParameterIdAsString(id);
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) id, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      ParameterIds id,
      ParseErrorCodes code,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ParameterIdAsString(id);
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) id, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      PictureIds id,
      ParseErrorCodes code,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) id, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      PictureIds id,
      ParseErrorCodes code,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) id, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      RingBufferIds id,
      ParseErrorCodes code,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) id, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      RingBufferIds id,
      ParseErrorCodes code,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) id, (int) code, 0), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      ParameterValueException ex,
      bool dumpToFile = false)
    {
      UniqueError.errorParameterException = ex;
      UniqueError.ParameterIdAsString(UniqueError.errorParameterException.ParameterId);
      UniqueError.additionalInformation = string.Empty;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) ex.ParameterId, 0, ex.ParameterPosition), dumpToFile);
    }

    public static void Message(
      UniqueError.Number errno,
      ParameterValueException ex,
      string additionalInfo,
      bool dumpToFile = false)
    {
      UniqueError.errorParameterException = ex;
      UniqueError.ParameterIdAsString(UniqueError.errorParameterException.ParameterId);
      UniqueError.additionalInformation = additionalInfo;
      UniqueError.ShowDialog(errno, UniqueError.UniqueId((int) errno, (int) ex.ParameterId, 0, ex.ParameterPosition), dumpToFile);
    }

    public static string UniqueId(int errorNumber, int id, int parseCode, int paramPosition) => string.Format("{0}.{1:D3}.{2:D2}.{3:D4}", (object) errorNumber, (object) id, (object) parseCode, (object) paramPosition);

    private static void FirmwareTypeAsString(Command_FirmwareTyps type)
    {
      switch (type)
      {
        case Command_FirmwareTyps.BOOTLOADER_MOTOR:
          UniqueError.firmwareTypeAsString = GlobalResource.BOOTLOADER_MOTOR;
          break;
        case Command_FirmwareTyps.FIRMWARE_MOTOR:
          UniqueError.firmwareTypeAsString = GlobalResource.FIRMWARE_MOTOR;
          break;
        case Command_FirmwareTyps.FIRMWARE_ACCU:
          UniqueError.firmwareTypeAsString = GlobalResource.FIRMWARE_ACCU;
          break;
        case Command_FirmwareTyps.FIRMWARE_MMI:
          UniqueError.firmwareTypeAsString = GlobalResource.FIRMWARE_MMI;
          break;
        case Command_FirmwareTyps.ACCU_DFI_FILE:
          UniqueError.firmwareTypeAsString = GlobalResource.FIRMWARE_ACCU_DFI;
          break;
        default:
          UniqueError.firmwareTypeAsString = GlobalResource.Unknown;
          break;
      }
    }

    private static void ParameterIdAsString(ParameterIds id)
    {
      switch (id)
      {
        case ParameterIds.OEMID:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_OEMID;
          break;
        case ParameterIds.MMI_DRIVE_SETTINGS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_DRIVE_SETTINGS;
          break;
        case ParameterIds.MMI_DEFAULT_SETTINGS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_DEFAULT_SETTINGS;
          break;
        case ParameterIds.CONTROL_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_CONTROL_INFORMATION;
          break;
        case ParameterIds.SERVICE_INTERVAL:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_SERVICE_INTERVAL;
          break;
        case ParameterIds.CUMULATIVE_SPEED_AND_TIME:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_CUMULATIVE_SPEED_AND_TIME;
          break;
        case ParameterIds.ACTUAL_LIGHT_SENSOR_VALUE:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_ACTUAL_LIGHT_SENSOR_VALUE;
          break;
        case ParameterIds.BUTTONS_STATE:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_BUTTONS_STATE;
          break;
        case ParameterIds.LIGHT_SENSOR_LEVELS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_LIGTH_SENSOR_LEVELS;
          break;
        case ParameterIds.BACKGROUND_BRIGHTNESS_LEVELS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_BACKGROUND_BRIGHTNESS_LEVELS;
          break;
        case ParameterIds.DATE_TIME:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_DATE_TIME;
          break;
        case ParameterIds.MMI_VERSION_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_VERSION_INFORMATION;
          break;
        case ParameterIds.REST_OF_RANGE:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_REST_OF_RANGE;
          break;
        case ParameterIds.MOTOR_SETTINGS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MOTOR_SETTINGS;
          break;
        case ParameterIds.MOTOR_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MOTOR_INFORMATION;
          break;
        case ParameterIds.MOTOR_BATTERY_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MOTOR_BATTERY_INFORMATION;
          break;
        case ParameterIds.MOTOR_VERSION_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MOTOR_VERSION_INFORMATION;
          break;
        case ParameterIds.MOTOR_CLIENT_STATE:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MOTOR_CLIENT_STATE;
          break;
        case ParameterIds.ACCU_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_ACCU_INFORMATION;
          break;
        case ParameterIds.ACCU_VERSION_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_ACCU_VERSION_INFORMATION;
          break;
        case ParameterIds.ACCU_CLIENT_STATE:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_ACCU_CLIENT_STATE;
          break;
        case ParameterIds.MMI_PRODUCTION_INFORMATION:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_PRODUCTION_INFORMATION;
          break;
        case ParameterIds.MMI_DISPLAY_COLORS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_DISPLAY_COLORS;
          break;
        case ParameterIds.MMI_SHUTDOWN_OVERTRAVEL:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_SHUTDOWN_OVERTRAVEL;
          break;
        case ParameterIds.MMI_SPECIAL_DISPLAY_COLORS:
          UniqueError.parameterIdAsString = GlobalResource.PARAMETER_ID_MMI_SPECIAL_DISPLAY_COLORS;
          break;
        default:
          UniqueError.parameterIdAsString = GlobalResource.Unknown;
          break;
      }
    }

    public static bool WaitingForClick => UniqueError.waitingForClick;

    private static void ShowDialog(UniqueError.Number errno, string errorNumber, bool dumpToFile)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string caption = GlobalResource.Error;
      UniqueError.waitingForClick = true;
      string str1;
      string str2;
      switch (errno)
      {
        case UniqueError.Number.ACTIVATION_CREATING_DEFAULT_DIR:
          str1 = GlobalResource.Activation_FileAccessException;
          str2 = GlobalResource.Activation_FileAccessExceptionMessage;
          break;
        case UniqueError.Number.ACTIVATION_FAILED:
          str1 = GlobalResource.Activation_Failed_Caption;
          str2 = GlobalResource.Activation_Failed_Message;
          break;
        case UniqueError.Number.ACTIVATION_WEBEXCEPTION:
          str1 = GlobalResource.Activation_WebException;
          str2 = UniqueError.errorException.Message;
          break;
        case UniqueError.Number.PROFILELISTFILE_WRITE_ERROR:
          str1 = GlobalResource.ProfileListFile_Write_Error_Caption;
          str2 = GlobalResource.ProfileListFile_Write_Error_Message;
          break;
        case UniqueError.Number.PARAMETERLISTFILE_WRITE_ERROR:
          str1 = GlobalResource.ParameterListFile_Write_Error_Caption;
          str2 = GlobalResource.ParameterListFile_Write_Error_Message;
          break;
        case UniqueError.Number.MMI_IMAGE_WRITE_ERROR:
          str1 = GlobalResource.MMI_Image_Write_Error_Caption;
          str2 = GlobalResource.MMI_Image_Write_Error_Message;
          break;
        case UniqueError.Number.IMAGELISTFILE_WRITE_ERROR:
          str1 = GlobalResource.ImageListFile_Write_Error_Caption;
          str2 = GlobalResource.ImageListFile_Write_Error_Message;
          break;
        case UniqueError.Number.MMI_EDIT_IMAGE_WRITE_ERROR:
          str1 = GlobalResource.MMI_Image_Write_Error_Caption;
          str2 = GlobalResource.MMI_Image_Write_Error_Message;
          break;
        case UniqueError.Number.IMAGELISTFILE_EDIT_WRITE_ERROR:
          str1 = GlobalResource.ImageListFile_Write_Error_Caption;
          str2 = GlobalResource.ImageListFile_Write_Error_Message;
          break;
        case UniqueError.Number.DEFAULT_PROFILELISTFILE_REMOVE_WRITE_ERROR:
          str1 = GlobalResource.ProfileListFile_Write_Error_Caption;
          str2 = GlobalResource.ProfileListFile_Write_Error_Message;
          break;
        case UniqueError.Number.PROFILELISTFILE_REMOVE_WRITE_ERROR:
          str1 = GlobalResource.ProfileListFile_Write_Error_Caption;
          str2 = GlobalResource.ProfileListFile_Write_Error_Message;
          break;
        case UniqueError.Number.DEFAULT_PARAMETERLISTFILE_REMOVE_WRITE_ERROR:
          str1 = GlobalResource.ParameterListFile_Write_Error_Caption;
          str2 = GlobalResource.ParameterListFile_Write_Error_Message;
          break;
        case UniqueError.Number.PARAMETERLISTFILE_REMOVE_WRITE_ERROR:
          str1 = GlobalResource.ParameterListFile_Write_Error_Caption;
          str2 = GlobalResource.ParameterListFile_Write_Error_Message;
          break;
        case UniqueError.Number.IMAGELISTFILE_DELETE_WRITE_ERROR:
          str1 = GlobalResource.ImageListFile_Write_Error_Caption;
          str2 = GlobalResource.ImageListFile_Write_Error_Message;
          break;
        case UniqueError.Number.MMI_IMAGE_DELETE_ERROR:
          str1 = GlobalResource.MMI_Image_Delete_Error_Caption;
          str2 = string.Format(GlobalResource.MMI_Image_Delete_Error_Message, (object) UniqueError.additionalInformation);
          break;
        case UniqueError.Number.PARAMETERLISTFILE_DELETE_WRITE_ERROR:
          str1 = GlobalResource.ParameterListFile_Write_Error_Caption;
          str2 = GlobalResource.ParameterListFile_Write_Error_Message;
          break;
        case UniqueError.Number.PARAMETERSETTINGS_DELETE_ERROR:
          str1 = GlobalResource.ParameterSettings_Delete_Error_Caption;
          str2 = string.Format(GlobalResource.ParameterSettings_Delete_Error_Message, (object) UniqueError.additionalInformation);
          break;
        case UniqueError.Number.PROFILELISTFILE_DELETE_WRITE_ERROR:
          str1 = GlobalResource.ProfileListFile_Write_Error_Caption;
          str2 = GlobalResource.ProfileListFile_Write_Error_Message;
          break;
        case UniqueError.Number.PROFILESETTINGS_DELETE_ERROR:
          str1 = GlobalResource.ProfileSettings_Delete_Error_Caption;
          str2 = string.Format(GlobalResource.ProfileSettings_Delete_Error_Message, (object) UniqueError.additionalInformation);
          break;
        case UniqueError.Number.PRODUCTIONNUMBER_INVALID:
          str1 = GlobalResource.ProductionNumber_Invalid_Caption;
          str2 = GlobalResource.ProductionNumber_Invalid_Message;
          break;
        case UniqueError.Number.PROFILESETTINGS_WRITE_ERROR:
          str1 = GlobalResource.ProfileSettings_Write_Error_Caption;
          str2 = GlobalResource.ProfileSettings_Write_Error_Message;
          break;
        case UniqueError.Number.PROFILELISTFILE_UPDATE_WRITE_ERROR:
          str1 = GlobalResource.ProfileListFile_Write_Error_Caption;
          str2 = GlobalResource.ProfileListFile_Write_Error_Message;
          break;
        case UniqueError.Number.PARAMETERSETTINGS_WRITE_ERROR:
          str1 = GlobalResource.ParameterSettings_Write_Error_Caption;
          str2 = GlobalResource.ParameterSettings_Write_Error_Message;
          break;
        case UniqueError.Number.PARAMETERLISTFILE_UPDATE_WRITE_ERROR:
          str1 = GlobalResource.ParameterListFile_Write_Error_Caption;
          str2 = GlobalResource.ParameterListFile_Write_Error_Message;
          break;
        case UniqueError.Number.HTTP_REQUEST:
          str1 = GlobalResource.Activation_WebException;
          str2 = UniqueError.errorException.Message + "\n" + UniqueError.additionalInformation;
          break;
        case UniqueError.Number.HTTP_DATAREQUEST:
          str1 = GlobalResource.Activation_WebException;
          str2 = UniqueError.errorException.Message + "\n" + UniqueError.additionalInformation;
          break;
        case UniqueError.Number.HTTP_TRANSMITDATA:
          str1 = GlobalResource.Activation_WebException;
          str2 = UniqueError.errorException.Message + "\n" + UniqueError.additionalInformation;
          break;
        case UniqueError.Number.HTTP_OUTOFMEMORYEXCEPTION:
          str1 = GlobalResource.Activation_WebException;
          str2 = UniqueError.errorException.Message;
          break;
        case UniqueError.Number.HTTP_READ_OUTOFMEMORYEXCEPTION:
          str1 = GlobalResource.Activation_WebException;
          str2 = UniqueError.errorException.Message;
          break;
        case UniqueError.Number.MASS_PROFILELISTFILE_WRITE_ERROR:
          str1 = GlobalResource.ProfileListFile_Write_Error_Caption;
          str2 = GlobalResource.ProfileListFile_Write_Error_Message;
          break;
        case UniqueError.Number.MASS_PARAMETERLISTFILE_WRITE_ERROR:
          str1 = GlobalResource.ParameterListFile_Write_Error_Caption;
          str2 = GlobalResource.ParameterListFile_Write_Error_Message;
          break;
        case UniqueError.Number.MASS_PRODUCTIONNUMBER_INVALID:
          str1 = GlobalResource.ProductionNumber_Invalid_Caption;
          str2 = GlobalResource.ProductionNumber_Invalid_Message;
          break;
        case UniqueError.Number.APPLICATION_ALREADY_RUNNING:
          str1 = GlobalResource.ApplicationAlreadyRunning_Caption;
          str2 = GlobalResource.ApplicationAlreadyRunning_Message;
          break;
        case UniqueError.Number.CREATING_DEFAULT_DIRECTORY:
          str1 = GlobalResource.WrongFilePermission_Caption;
          str2 = string.Format(GlobalResource.WrongFilePermission_Message, (object) UniqueError.additionalInformation);
          break;
        case UniqueError.Number.CREATE_NON_GLOBAL_USER_DATA_DIRECTORY:
          str1 = GlobalResource.WrongFilePermission_Caption;
          str2 = string.Format(GlobalResource.WrongFilePermission_Message, (object) UniqueError.additionalInformation);
          break;
        case UniqueError.Number.PROXYSETTINGS_WRITE_ERROR:
          str1 = GlobalResource.ProxySettings_Write_Error_Caption;
          str2 = GlobalResource.ProxySettings_Write_Error_Message;
          break;
        case UniqueError.Number.UPDATE_LATESTVERSIONFILE_WRITE_ERROR:
          str1 = GlobalResource.LatestVersionFile_Write_Error_Caption;
          str2 = GlobalResource.LatestVersionFile_Write_Error_Message;
          break;
        case UniqueError.Number.MOTOR_DELETE_LIST_WRITE_ERROR:
          str1 = GlobalResource.List_Write_Error_Caption;
          str2 = GlobalResource.List_Write_Error_Message;
          break;
        case UniqueError.Number.ACCU_DELETE_LIST_WRITE_ERROR:
          str1 = GlobalResource.List_Write_Error_Caption;
          str2 = GlobalResource.List_Write_Error_Message;
          break;
        case UniqueError.Number.MMI_DELETE_LIST_WRITE_ERROR:
          str1 = GlobalResource.List_Write_Error_Caption;
          str2 = GlobalResource.List_Write_Error_Message;
          break;
        case UniqueError.Number.FIRMWAREFILE_DELETE_ERROR:
          str1 = GlobalResource.FirmwareFile_Delete_Error_Caption;
          str2 = string.Format(GlobalResource.FirmwareFile_Delete_Error_Message, (object) UniqueError.additionalInformation);
          break;
        case UniqueError.Number.WORKER_UPDATE_LATESTVERSIONFILE_WRITE_ERROR:
          str1 = GlobalResource.LatestVersionFile_Write_Error_Caption;
          str2 = GlobalResource.LatestVersionFile_Write_Error_Message;
          break;
        case UniqueError.Number.LATESTVERSIONFILE_WRITE_ERROR:
          str1 = GlobalResource.LatestVersionFile_Write_Error_Caption;
          str2 = GlobalResource.LatestVersionFile_Write_Error_Message;
          break;
        case UniqueError.Number.LIST_WRITE_ERROR:
          str1 = GlobalResource.List_Write_Error_Caption;
          str2 = GlobalResource.List_Write_Error_Message;
          break;
        case UniqueError.Number.UPDATE_LIST_WRITE_ERROR:
          str1 = GlobalResource.List_Write_Error_Caption;
          str2 = GlobalResource.List_Write_Error_Message;
          break;
        case UniqueError.Number.PROGRAM_INSTALLER:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_NEO_SMART_DIAGNOSTIC_UPDATE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_NEO_SMART_DIAGNOSTIC_UPDATE_DELETE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_NEWS_UPDATE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_NEWS_UPDATE_DELETE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_FIRMWARE_UPDATE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_FIRMWARE_UPDATE_DELETE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.START_ACTIVATION:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.TRANSEIVER_READ_FROM_FILE_DIRECTORY_NOT_FOUND:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.TRANSEIVER_READ_FROM_FILE_LOAD:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.TRANSEIVER_READ_FROM_FILE_NOT_FOUND:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.TRANSEIVER_READ_FROM_FILE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.TRANSFER_FIRMWARE_START:
          str1 = GlobalResource.FirmwareStartTranmission_Error_Caption;
          str2 = string.Format(GlobalResource.FirmwareStartTranmission_Error_Message, (object) UniqueError.firmwareTypeAsString);
          break;
        case UniqueError.Number.TRANSFER_FIRMWARE_TRANSMISSION:
          str1 = GlobalResource.FirmwareTranmission_Error_Caption;
          str2 = string.Format(GlobalResource.FirmwareTranmission_Error_Message, (object) UniqueError.firmwareTypeAsString);
          break;
        case UniqueError.Number.TRANSFER_FIRMWARE_END:
          str1 = GlobalResource.FirmwareEndTranmission_Error_Caption;
          str2 = string.Format(GlobalResource.FirmwareEndTranmission_Error_Message, (object) UniqueError.firmwareTypeAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ERROR:
          str1 = GlobalResource.Parameter_Receive_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Receive_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ERROR_PARSE:
          str1 = UniqueError.errorParameterException.Message;
          str2 = string.Format(GlobalResource.Parameter_Protocol_Value_Exception_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ERROR_WITH_BAR:
          str1 = GlobalResource.Parameter_Receive_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Receive_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ERROR_PARSE_WITH_BAR:
          str1 = UniqueError.errorParameterException.Message;
          str2 = string.Format(GlobalResource.Parameter_Protocol_Value_Exception_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ALL_ERROR:
          str1 = GlobalResource.Parameter_Receive_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Receive_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ALL_ERROR_PARSE:
          str1 = UniqueError.errorParameterException.Message;
          str2 = string.Format(GlobalResource.Parameter_Protocol_Value_Exception_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ALL_ACCESSIBLE_ERROR:
          str1 = GlobalResource.Parameter_Receive_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Receive_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_RECEIVE_ALL_ERROR_ACCESSIBLE_PARSE:
          str1 = UniqueError.errorParameterException.Message;
          str2 = string.Format(GlobalResource.Parameter_Protocol_Value_Exception_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_TRANSMISSION_ERROR_WITH_BAR:
          str1 = GlobalResource.Parameter_Transmission_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Transmission_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_TRANSMISSION_ERROR:
          str1 = GlobalResource.Parameter_Transmission_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Transmission_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_TRANSMISSION_ALL_ACCESSIBLE_ERROR_WITH_BAR:
          str1 = GlobalResource.Parameter_Transmission_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Transmission_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PARAMETER_TRANSMISSION_ALL_ERROR_WITH_BAR:
          str1 = GlobalResource.Parameter_Transmission_Error_Caption;
          str2 = string.Format(GlobalResource.Parameter_Transmission_Error_Message, (object) UniqueError.parameterIdAsString);
          break;
        case UniqueError.Number.PICTURE_START_TRANMISSION:
          str1 = GlobalResource.PictureStartTranmission_Error_Caption;
          str2 = GlobalResource.PictureStartTranmission_Error_Message;
          break;
        case UniqueError.Number.PICTURE_TRANMISSION:
          str1 = GlobalResource.PictureTranmission_Error_Caption;
          str2 = GlobalResource.PictureTranmission_Error_Message;
          break;
        case UniqueError.Number.PICTURE_END_TRANMISSION:
          str1 = GlobalResource.PictureEndTranmission_Error_Caption;
          str2 = GlobalResource.PictureEndTranmission_Error_Message;
          break;
        case UniqueError.Number.WAITING_FIRMWAREFLAG_RECEIVE_ERROR:
          str1 = GlobalResource.FirmwareFlag_Receive_Error_Caption;
          str2 = GlobalResource.FirmwareFlag_Receive_Error_Message;
          break;
        case UniqueError.Number.WAITING_FIRMWAREFLAG_TRANSMIT_ERROR:
          str1 = GlobalResource.FirmwareFlag_Transmit_Error_Caption;
          str2 = GlobalResource.FirmwareFlag_Transmit_Error_Message;
          break;
        case UniqueError.Number.FIRMWAREFLAG_RECEIVE_ERROR:
          str1 = GlobalResource.FirmwareFlag_Receive_Error_Caption;
          str2 = GlobalResource.FirmwareFlag_Receive_Error_Message;
          break;
        case UniqueError.Number.FIRMWAREFLAG_TRANSMIT_ERROR:
          str1 = GlobalResource.FirmwareFlag_Transmit_Error_Caption;
          str2 = GlobalResource.FirmwareFlag_Transmit_Error_Message;
          break;
        case UniqueError.Number.UPDATEFLAG_RECEIVE_ERROR:
          str1 = GlobalResource.UpdateFlag_Receive_Error_Caption;
          str2 = GlobalResource.UpdateFlag_Receive_Error_Message;
          break;
        case UniqueError.Number.UPDATEFLAG_TRANSMIT_ERROR:
          str1 = GlobalResource.UpdateFlag_Transmit_Error_Caption;
          str2 = GlobalResource.UpdateFlag_Transmit_Error_Message;
          break;
        case UniqueError.Number.RINGBUFFER_START_RECEIVE_ERROR:
          str1 = GlobalResource.RingBuffer_Receive_Error_Caption;
          str2 = GlobalResource.RingBuffer_Receive_Error_Message;
          break;
        case UniqueError.Number.RINGBUFFER_RECEIVE_ERROR:
          str1 = GlobalResource.RingBuffer_Receive_Error_Caption;
          str2 = GlobalResource.RingBuffer_Receive_Error_Message;
          break;
        case UniqueError.Number.SOUND_FILE_INVALID:
          str1 = UniqueError.errorException.Message;
          str2 = GlobalResource.SOUND_FILE_INVALID_Message;
          break;
        case UniqueError.Number.SYSTEM_OUT_OF_MEMORY:
          str1 = UniqueError.errorException.Message;
          str2 = GlobalResource.SystemOutOfMemory_Message;
          break;
        case UniqueError.Number.RINGBUFFER_DELETE:
          str1 = GlobalResource.RingBuffer_Delete_Error_Caption;
          str2 = GlobalResource.RingBuffer_Delete_Error_Message;
          break;
        case UniqueError.Number.TRANSFER_FIRMWARE_VERSION:
          str1 = GlobalResource.FirmwareVersionTranmission_Error_Caption;
          str2 = string.Format(GlobalResource.FirmwareVersionTranmission_Error_Message, (object) UniqueError.firmwareTypeAsString);
          break;
        case UniqueError.Number.RECEIVE_FIRMWARE_VERSION:
          str1 = GlobalResource.FirmwareVersionReceive_Error_Caption;
          str2 = string.Format(GlobalResource.FirmwareVersionReceive_Error_Message, (object) UniqueError.firmwareTypeAsString);
          break;
        case UniqueError.Number.HANDLE_PROFILE_UPDATE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        case UniqueError.Number.HANDLE_PROFILE_UPDATE_DELETE:
          str1 = UniqueError.errorException.Message;
          str2 = UniqueError.errorException.StackTrace;
          break;
        default:
          dumpToFile = true;
          str1 = GlobalResource.Unknown;
          str2 = GlobalResource.Unknown;
          caption = GlobalResource.Unknown;
          break;
      }
      errorNumber = GlobalResource.Error + " #" + errorNumber;
      if (dumpToFile)
        GlobalLogger.Instance.WriteLine(errorNumber + "\n" + str1 + "\n" + str2, GlobalLogger.Level.Fatal);
      if (MainWindow.Instance.ConnectionEstablishForm != null)
        MainWindow.Instance.ConnectionEstablishForm.ThreadSaveDispose();
      if (MessageBox.Show(errorNumber + "\n" + str1 + "\n\n" + str2 + "\n", caption, MessageBoxButtons.OK, MessageBoxIcon.Hand) != DialogResult.OK)
        return;
      UniqueError.waitingForClick = false;
    }

    public enum Number
    {
      ACTIVATION_CREATING_DEFAULT_DIR,
      ACTIVATION_FAILED,
      ACTIVATION_WEBEXCEPTION,
      PROFILELISTFILE_WRITE_ERROR,
      PARAMETERLISTFILE_WRITE_ERROR,
      MMI_IMAGE_WRITE_ERROR,
      IMAGELISTFILE_WRITE_ERROR,
      MMI_EDIT_IMAGE_WRITE_ERROR,
      IMAGELISTFILE_EDIT_WRITE_ERROR,
      DEFAULT_PROFILELISTFILE_REMOVE_WRITE_ERROR,
      PROFILELISTFILE_REMOVE_WRITE_ERROR,
      DEFAULT_PARAMETERLISTFILE_REMOVE_WRITE_ERROR,
      PARAMETERLISTFILE_REMOVE_WRITE_ERROR,
      IMAGELISTFILE_DELETE_WRITE_ERROR,
      MMI_IMAGE_DELETE_ERROR,
      PARAMETERLISTFILE_DELETE_WRITE_ERROR,
      PARAMETERSETTINGS_DELETE_ERROR,
      PROFILELISTFILE_DELETE_WRITE_ERROR,
      PROFILESETTINGS_DELETE_ERROR,
      PRODUCTIONNUMBER_INVALID,
      PROFILESETTINGS_WRITE_ERROR,
      PROFILELISTFILE_UPDATE_WRITE_ERROR,
      PARAMETERSETTINGS_WRITE_ERROR,
      PARAMETERLISTFILE_UPDATE_WRITE_ERROR,
      HTTP_REQUEST,
      HTTP_DATAREQUEST,
      HTTP_TRANSMITDATA,
      HTTP_OUTOFMEMORYEXCEPTION,
      HTTP_READ_OUTOFMEMORYEXCEPTION,
      MASS_PROFILELISTFILE_WRITE_ERROR,
      MASS_PARAMETERLISTFILE_WRITE_ERROR,
      MASS_PRODUCTIONNUMBER_INVALID,
      APPLICATION_ALREADY_RUNNING,
      CREATING_DEFAULT_DIRECTORY,
      CREATE_NON_GLOBAL_USER_DATA_DIRECTORY,
      PROXYSETTINGS_WRITE_ERROR,
      UPDATE_LATESTVERSIONFILE_WRITE_ERROR,
      MOTOR_DELETE_LIST_WRITE_ERROR,
      ACCU_DELETE_LIST_WRITE_ERROR,
      MMI_DELETE_LIST_WRITE_ERROR,
      FIRMWAREFILE_DELETE_ERROR,
      WORKER_UPDATE_LATESTVERSIONFILE_WRITE_ERROR,
      LATESTVERSIONFILE_WRITE_ERROR,
      LIST_WRITE_ERROR,
      UPDATE_LIST_WRITE_ERROR,
      PROGRAM_INSTALLER,
      HANDLE_NEO_SMART_DIAGNOSTIC_UPDATE,
      HANDLE_NEO_SMART_DIAGNOSTIC_UPDATE_DELETE,
      HANDLE_NEWS_UPDATE,
      HANDLE_NEWS_UPDATE_DELETE,
      HANDLE_FIRMWARE_UPDATE,
      HANDLE_FIRMWARE_UPDATE_DELETE,
      START_ACTIVATION,
      TRANSEIVER_READ_FROM_FILE_DIRECTORY_NOT_FOUND,
      TRANSEIVER_READ_FROM_FILE_LOAD,
      TRANSEIVER_READ_FROM_FILE_NOT_FOUND,
      TRANSEIVER_READ_FROM_FILE,
      TRANSFER_FIRMWARE_START,
      TRANSFER_FIRMWARE_TRANSMISSION,
      TRANSFER_FIRMWARE_END,
      PARAMETER_RECEIVE_ERROR,
      PARAMETER_RECEIVE_ERROR_PARSE,
      PARAMETER_RECEIVE_ERROR_WITH_BAR,
      PARAMETER_RECEIVE_ERROR_PARSE_WITH_BAR,
      PARAMETER_RECEIVE_ALL_ERROR,
      PARAMETER_RECEIVE_ALL_ERROR_PARSE,
      PARAMETER_RECEIVE_ALL_ACCESSIBLE_ERROR,
      PARAMETER_RECEIVE_ALL_ERROR_ACCESSIBLE_PARSE,
      PARAMETER_TRANSMISSION_ERROR_WITH_BAR,
      PARAMETER_TRANSMISSION_ERROR,
      PARAMETER_TRANSMISSION_ALL_ACCESSIBLE_ERROR_WITH_BAR,
      PARAMETER_TRANSMISSION_ALL_ERROR_WITH_BAR,
      PICTURE_START_TRANMISSION,
      PICTURE_TRANMISSION,
      PICTURE_END_TRANMISSION,
      WAITING_FIRMWAREFLAG_RECEIVE_ERROR,
      WAITING_FIRMWAREFLAG_TRANSMIT_ERROR,
      FIRMWAREFLAG_RECEIVE_ERROR,
      FIRMWAREFLAG_TRANSMIT_ERROR,
      UPDATEFLAG_RECEIVE_ERROR,
      UPDATEFLAG_TRANSMIT_ERROR,
      RINGBUFFER_START_RECEIVE_ERROR,
      RINGBUFFER_RECEIVE_ERROR,
      SOUND_FILE_INVALID,
      SYSTEM_OUT_OF_MEMORY,
      RINGBUFFER_DELETE,
      TRANSFER_FIRMWARE_VERSION,
      RECEIVE_FIRMWARE_VERSION,
      HANDLE_PROFILE_UPDATE,
      HANDLE_PROFILE_UPDATE_DELETE,
      DFI_DELETE_LIST_WRITE_ERROR,
    }
  }
}
