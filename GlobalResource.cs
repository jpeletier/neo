// Decompiled with JetBrains decompiler
// Type: ZerroWare.GlobalResource
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ZerroWare
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  internal class GlobalResource
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal GlobalResource()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) GlobalResource.resourceMan, (object) null))
          GlobalResource.resourceMan = new ResourceManager("ZerroWare.GlobalResource", typeof (GlobalResource).Assembly);
        return GlobalResource.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => GlobalResource.resourceCulture;
      set => GlobalResource.resourceCulture = value;
    }

    internal static string AboutBox_MessageBox_InvalidLicense_Caption => GlobalResource.ResourceManager.GetString(nameof (AboutBox_MessageBox_InvalidLicense_Caption), GlobalResource.resourceCulture);

    internal static string AboutBox_MessageBox_InvalidLicense_Message => GlobalResource.ResourceManager.GetString(nameof (AboutBox_MessageBox_InvalidLicense_Message), GlobalResource.resourceCulture);

    internal static string AccuFirmwareTransmissionSucceeded_Caption_911 => GlobalResource.ResourceManager.GetString(nameof (AccuFirmwareTransmissionSucceeded_Caption_911), GlobalResource.resourceCulture);

    internal static string AccuFirmwareTransmissionSucceeded_Message_911 => GlobalResource.ResourceManager.GetString(nameof (AccuFirmwareTransmissionSucceeded_Message_911), GlobalResource.resourceCulture);

    internal static string Activation_Failed_Caption => GlobalResource.ResourceManager.GetString(nameof (Activation_Failed_Caption), GlobalResource.resourceCulture);

    internal static string Activation_Failed_Message => GlobalResource.ResourceManager.GetString(nameof (Activation_Failed_Message), GlobalResource.resourceCulture);

    internal static string Activation_FileAccessException => GlobalResource.ResourceManager.GetString(nameof (Activation_FileAccessException), GlobalResource.resourceCulture);

    internal static string Activation_FileAccessExceptionMessage => GlobalResource.ResourceManager.GetString(nameof (Activation_FileAccessExceptionMessage), GlobalResource.resourceCulture);

    internal static string Activation_WebException => GlobalResource.ResourceManager.GetString(nameof (Activation_WebException), GlobalResource.resourceCulture);

    internal static string Actual_ProductionNumber_Label => GlobalResource.ResourceManager.GetString(nameof (Actual_ProductionNumber_Label), GlobalResource.resourceCulture);

    internal static string ApplicationAlreadyRunning_Caption => GlobalResource.ResourceManager.GetString(nameof (ApplicationAlreadyRunning_Caption), GlobalResource.resourceCulture);

    internal static string ApplicationAlreadyRunning_Message => GlobalResource.ResourceManager.GetString(nameof (ApplicationAlreadyRunning_Message), GlobalResource.resourceCulture);

    internal static string BOOTLOADER_MOTOR => GlobalResource.ResourceManager.GetString(nameof (BOOTLOADER_MOTOR), GlobalResource.resourceCulture);

    internal static string cancel => GlobalResource.ResourceManager.GetString(nameof (cancel), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Delete_DefaultParameter_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Delete_DefaultParameter_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Delete_DefaultParameter_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Delete_DefaultParameter_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Delete_DefaultProfile_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Delete_DefaultProfile_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Delete_DefaultProfile_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Delete_DefaultProfile_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Delete_DefaultScreen_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Delete_DefaultScreen_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Delete_DefaultScreen_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Delete_DefaultScreen_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_FastConfig_MaxNumbers_Exceeded_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_FastConfig_MaxNumbers_Exceeded_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_FastConfig_MaxNumbers_Exceeded_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_FastConfig_MaxNumbers_Exceeded_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_newParameter_Description => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_newParameter_Description), GlobalResource.resourceCulture);

    internal static string ConfigPanel_newParameter_Label => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_newParameter_Label), GlobalResource.resourceCulture);

    internal static string ConfigPanel_newProfile_Description => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_newProfile_Description), GlobalResource.resourceCulture);

    internal static string ConfigPanel_newProfile_Label => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_newProfile_Label), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Parameter_Name_Missing_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Parameter_Name_Missing_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Parameter_Name_Missing_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Parameter_Name_Missing_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_parameterDeleteButton_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_parameterDeleteButton_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_parameterDeleteButton_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_parameterDeleteButton_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_parameterDescriptionRichTextBox_MMI_Text => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_parameterDescriptionRichTextBox_MMI_Text), GlobalResource.resourceCulture);

    internal static string ConfigPanel_parameterNameTextBox_MMI_Text => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_parameterNameTextBox_MMI_Text), GlobalResource.resourceCulture);

    internal static string ConfigPanel_parameterTransferPictureBoxToolTip => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_parameterTransferPictureBoxToolTip), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Profile_Name_Missing_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Profile_Name_Missing_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Profile_Name_Missing_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Profile_Name_Missing_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_profileDeleteButton_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_profileDeleteButton_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_profileDeleteButton_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_profileDeleteButton_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_profileDescriptionRichTextBox_MMI_Text => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_profileDescriptionRichTextBox_MMI_Text), GlobalResource.resourceCulture);

    internal static string ConfigPanel_profileNameTextBox_MMI_Text => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_profileNameTextBox_MMI_Text), GlobalResource.resourceCulture);

    internal static string ConfigPanel_profileTransferPictureBoxToolTip => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_profileTransferPictureBoxToolTip), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ScreenDelete_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ScreenDelete_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ScreenDelete_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ScreenDelete_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_DeleteParameter => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_DeleteParameter), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_DeleteProfile => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_DeleteProfile), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_DeleteStartScreen => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_DeleteStartScreen), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_EditParameter => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_EditParameter), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_EditProfile => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_EditProfile), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_EditStartScreen => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_EditStartScreen), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_MassConfigParameter => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_MassConfigParameter), GlobalResource.resourceCulture);

    internal static string ConfigPanel_ToolTip_MassConfigProfile => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_ToolTip_MassConfigProfile), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Update_DefaultParameter_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Update_DefaultParameter_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Update_DefaultParameter_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Update_DefaultParameter_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Update_DefaultProfile_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Update_DefaultProfile_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Update_DefaultProfile_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Update_DefaultProfile_Message), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Update_DefaultScreen_Caption => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Update_DefaultScreen_Caption), GlobalResource.resourceCulture);

    internal static string ConfigPanel_Update_DefaultScreen_Message => GlobalResource.ResourceManager.GetString(nameof (ConfigPanel_Update_DefaultScreen_Message), GlobalResource.resourceCulture);

    internal static string Configuration_Parameters => GlobalResource.ResourceManager.GetString(nameof (Configuration_Parameters), GlobalResource.resourceCulture);

    internal static string Configuration_Profile => GlobalResource.ResourceManager.GetString(nameof (Configuration_Profile), GlobalResource.resourceCulture);

    internal static string Configuration_StartScreen => GlobalResource.ResourceManager.GetString(nameof (Configuration_StartScreen), GlobalResource.resourceCulture);

    internal static string ConfigurationPanel_MassTrans_NoElement => GlobalResource.ResourceManager.GetString(nameof (ConfigurationPanel_MassTrans_NoElement), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_AccuVeryOld_913_Caption => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_AccuVeryOld_913_Caption), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_AccuVeryOld_913_Message => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_AccuVeryOld_913_Message), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_DeleteErrorLog_Caption => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_DeleteErrorLog_Caption), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_DeleteErrorLog_Question => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_DeleteErrorLog_Question), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_DeleteErrorLog_SuccessMessage => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_DeleteErrorLog_SuccessMessage), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_DFI_Safety_Caption => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_DFI_Safety_Caption), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_DFI_Safety_Message => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_DFI_Safety_Message), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_FirmwareButton_nok => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_FirmwareButton_nok), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_FirmwareButton_ok => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_FirmwareButton_ok), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_FirmwareToolTipText => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_FirmwareToolTipText), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_FirmwareToolTipTitle => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_FirmwareToolTipTitle), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_ImmobilizerActive => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_ImmobilizerActive), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_ImmobilizerInactive => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_ImmobilizerInactive), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_LockBreakToolTip => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_LockBreakToolTip), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_LockOpenToolTip => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_LockOpenToolTip), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_MessageBox_Redock_Needed_Caption => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_MessageBox_Redock_Needed_Caption), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_MessageBox_Redock_Needed_Message => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_MessageBox_Redock_Needed_Message), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_MotorSerialsNotMatch_Caption => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_MotorSerialsNotMatch_Caption), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_MotorSerialsNotMatch_Message => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_MotorSerialsNotMatch_Message), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_sDiagButton_nok => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_sDiagButton_nok), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_sDiagButton_ok => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_sDiagButton_ok), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_ToolTip_Firmware_NotUpToDate => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_ToolTip_Firmware_NotUpToDate), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_ToolTip_Firmware_UpToDate => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_ToolTip_Firmware_UpToDate), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_ToolTip_Healthy => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_ToolTip_Healthy), GlobalResource.resourceCulture);

    internal static string DiagnosePanel_ToolTip_NotHealthy => GlobalResource.ResourceManager.GetString(nameof (DiagnosePanel_ToolTip_NotHealthy), GlobalResource.resourceCulture);

    internal static string EditActualMMIData => GlobalResource.ResourceManager.GetString(nameof (EditActualMMIData), GlobalResource.resourceCulture);

    internal static string Error => GlobalResource.ResourceManager.GetString(nameof (Error), GlobalResource.resourceCulture);

    internal static string ErrorOverviewPanel_Report_SerialNumber_Text => GlobalResource.ResourceManager.GetString(nameof (ErrorOverviewPanel_Report_SerialNumber_Text), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailBatteryStateCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailBatteryStateCaption), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailDocumentSubject => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailDocumentSubject), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailDocumentTitle => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailDocumentTitle), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailErrorOverviewCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailErrorOverviewCaption), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailMMIStateCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailMMIStateCaption), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailMotorStateCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailMotorStateCaption), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailPageCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailPageCaption), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailSensorDataCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailSensorDataCaption), GlobalResource.resourceCulture);

    internal static string ErrorReport_DetailSystemErrorCaption => GlobalResource.ResourceManager.GetString(nameof (ErrorReport_DetailSystemErrorCaption), GlobalResource.resourceCulture);

    internal static string FailedToDeleteSettingsFile_Caption => GlobalResource.ResourceManager.GetString(nameof (FailedToDeleteSettingsFile_Caption), GlobalResource.resourceCulture);

    internal static string FailedToDeleteSettingsFile_Message => GlobalResource.ResourceManager.GetString(nameof (FailedToDeleteSettingsFile_Message), GlobalResource.resourceCulture);

    internal static string FatalError_Caption => GlobalResource.ResourceManager.GetString(nameof (FatalError_Caption), GlobalResource.resourceCulture);

    internal static string FatalError_Message => GlobalResource.ResourceManager.GetString(nameof (FatalError_Message), GlobalResource.resourceCulture);

    internal static string FIRMWARE_ACCU => GlobalResource.ResourceManager.GetString(nameof (FIRMWARE_ACCU), GlobalResource.resourceCulture);

    internal static string FIRMWARE_ACCU_DFI => GlobalResource.ResourceManager.GetString(nameof (FIRMWARE_ACCU_DFI), GlobalResource.resourceCulture);

    internal static string FIRMWARE_MMI => GlobalResource.ResourceManager.GetString(nameof (FIRMWARE_MMI), GlobalResource.resourceCulture);

    internal static string FIRMWARE_MOTOR => GlobalResource.ResourceManager.GetString(nameof (FIRMWARE_MOTOR), GlobalResource.resourceCulture);

    internal static string FirmwareEndTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareEndTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareEndTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareEndTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareFile_Delete_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareFile_Delete_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareFile_Delete_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareFile_Delete_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareFlag_Receive_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareFlag_Receive_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareFlag_Receive_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareFlag_Receive_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareFlag_Transmit_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareFlag_Transmit_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareFlag_Transmit_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareFlag_Transmit_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareList_Delete_ToolTip => GlobalResource.ResourceManager.GetString(nameof (FirmwareList_Delete_ToolTip), GlobalResource.resourceCulture);

    internal static string FirmwareList_Info_ToolTip => GlobalResource.ResourceManager.GetString(nameof (FirmwareList_Info_ToolTip), GlobalResource.resourceCulture);

    internal static string FirmwareList_Transmit_ToolTip => GlobalResource.ResourceManager.GetString(nameof (FirmwareList_Transmit_ToolTip), GlobalResource.resourceCulture);

    internal static string FirmwareStartTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareStartTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareStartTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareStartTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareTransmissionSucceeded_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareTransmissionSucceeded_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareTransmissionSucceeded_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareTransmissionSucceeded_Message), GlobalResource.resourceCulture);

    internal static string FirmwareVersionReceive_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareVersionReceive_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareVersionReceive_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareVersionReceive_Error_Message), GlobalResource.resourceCulture);

    internal static string FirmwareVersionTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (FirmwareVersionTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string FirmwareVersionTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (FirmwareVersionTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string GuestUser_ApplicationExit_Caption => GlobalResource.ResourceManager.GetString(nameof (GuestUser_ApplicationExit_Caption), GlobalResource.resourceCulture);

    internal static string GuestUser_ApplicationExit_Message => GlobalResource.ResourceManager.GetString(nameof (GuestUser_ApplicationExit_Message), GlobalResource.resourceCulture);

    internal static string HelpPanel_UpdateCaption => GlobalResource.ResourceManager.GetString(nameof (HelpPanel_UpdateCaption), GlobalResource.resourceCulture);

    internal static string HelpPanel_UpdateFailedMessage => GlobalResource.ResourceManager.GetString(nameof (HelpPanel_UpdateFailedMessage), GlobalResource.resourceCulture);

    internal static string HelpPanel_UpdateSuccessMessage => GlobalResource.ResourceManager.GetString(nameof (HelpPanel_UpdateSuccessMessage), GlobalResource.resourceCulture);

    internal static string ImageListFile_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ImageListFile_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string ImageListFile_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ImageListFile_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string ImageLoad_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ImageLoad_Error_Caption), GlobalResource.resourceCulture);

    internal static string ImageLoad_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ImageLoad_Error_Message), GlobalResource.resourceCulture);

    internal static string ImageWarningNoImage => GlobalResource.ResourceManager.GetString(nameof (ImageWarningNoImage), GlobalResource.resourceCulture);

    internal static string ImageWarningNoImageCaption => GlobalResource.ResourceManager.GetString(nameof (ImageWarningNoImageCaption), GlobalResource.resourceCulture);

    internal static string ImageWarningNoName => GlobalResource.ResourceManager.GetString(nameof (ImageWarningNoName), GlobalResource.resourceCulture);

    internal static string ImageWarningNoNameCaption => GlobalResource.ResourceManager.GetString(nameof (ImageWarningNoNameCaption), GlobalResource.resourceCulture);

    internal static string ImageWarningNoPicture => GlobalResource.ResourceManager.GetString(nameof (ImageWarningNoPicture), GlobalResource.resourceCulture);

    internal static string ImageWarningNoPictureCaption => GlobalResource.ResourceManager.GetString(nameof (ImageWarningNoPictureCaption), GlobalResource.resourceCulture);

    internal static string LanguageInvalid_ParameterPanel_Caption => GlobalResource.ResourceManager.GetString(nameof (LanguageInvalid_ParameterPanel_Caption), GlobalResource.resourceCulture);

    internal static string LanguageInvalid_ParameterPanel_Message => GlobalResource.ResourceManager.GetString(nameof (LanguageInvalid_ParameterPanel_Message), GlobalResource.resourceCulture);

    internal static string LatestVersionFile_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (LatestVersionFile_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string LatestVersionFile_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (LatestVersionFile_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string License_Expired_Caption => GlobalResource.ResourceManager.GetString(nameof (License_Expired_Caption), GlobalResource.resourceCulture);

    internal static string License_Expired_Message => GlobalResource.ResourceManager.GetString(nameof (License_Expired_Message), GlobalResource.resourceCulture);

    internal static string List_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (List_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string List_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (List_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string MainWindow_englischToolStripMenuItem_Click_Caption => GlobalResource.ResourceManager.GetString(nameof (MainWindow_englischToolStripMenuItem_Click_Caption), GlobalResource.resourceCulture);

    internal static string MainWindow_englischToolStripMenuItem_Click_Message => GlobalResource.ResourceManager.GetString(nameof (MainWindow_englischToolStripMenuItem_Click_Message), GlobalResource.resourceCulture);

    internal static string MainWindow_germanToolStripMenuItem_Click_Caption => GlobalResource.ResourceManager.GetString(nameof (MainWindow_germanToolStripMenuItem_Click_Caption), GlobalResource.resourceCulture);

    internal static string MainWindow_germanToolStripMenuItem_Click_Message => GlobalResource.ResourceManager.GetString(nameof (MainWindow_germanToolStripMenuItem_Click_Message), GlobalResource.resourceCulture);

    internal static string MainWindow_MaxSpeedAttention_Caption => GlobalResource.ResourceManager.GetString(nameof (MainWindow_MaxSpeedAttention_Caption), GlobalResource.resourceCulture);

    internal static string MainWindow_MaxSpeedAttention_Message => GlobalResource.ResourceManager.GetString(nameof (MainWindow_MaxSpeedAttention_Message), GlobalResource.resourceCulture);

    internal static string MainWindow_MMIConnected => GlobalResource.ResourceManager.GetString(nameof (MainWindow_MMIConnected), GlobalResource.resourceCulture);

    internal static string MainWindow_MMIConnectionFailed_Caption => GlobalResource.ResourceManager.GetString(nameof (MainWindow_MMIConnectionFailed_Caption), GlobalResource.resourceCulture);

    internal static string MainWindow_MMIConnectionFailed_Message => GlobalResource.ResourceManager.GetString(nameof (MainWindow_MMIConnectionFailed_Message), GlobalResource.resourceCulture);

    internal static string MainWindow_MMINotConnected => GlobalResource.ResourceManager.GetString(nameof (MainWindow_MMINotConnected), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_MassReceive => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_MassReceive), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_MassReceive_Done => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_MassReceive_Done), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_MassReceive_Failed => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_MassReceive_Failed), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_MassReceive_Failed_Caption => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_MassReceive_Failed_Caption), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_MassTransmit_Failed => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_MassTransmit_Failed), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_MassTransmit_Failed_Caption => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_MassTransmit_Failed_Caption), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_ToolTip_Parameter_FastTransmission => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_ToolTip_Parameter_FastTransmission), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_ToolTip_Parameter_StartScreen => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_ToolTip_Parameter_StartScreen), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_ToolTip_Parameter_Transmission => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_ToolTip_Parameter_Transmission), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_ToolTip_Profile_StartScreen => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_ToolTip_Profile_StartScreen), GlobalResource.resourceCulture);

    internal static string MassConfigPanel_ToolTip_Profile_Transmission => GlobalResource.ResourceManager.GetString(nameof (MassConfigPanel_ToolTip_Profile_Transmission), GlobalResource.resourceCulture);

    internal static string MMI_Image_Delete_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (MMI_Image_Delete_Error_Caption), GlobalResource.resourceCulture);

    internal static string MMI_Image_Delete_Error_Message => GlobalResource.ResourceManager.GetString(nameof (MMI_Image_Delete_Error_Message), GlobalResource.resourceCulture);

    internal static string MMI_Image_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (MMI_Image_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string MMI_Image_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (MMI_Image_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string Mmi_Lock_Information => GlobalResource.ResourceManager.GetString(nameof (Mmi_Lock_Information), GlobalResource.resourceCulture);

    internal static string MmiSettingReport_DocumentSubject => GlobalResource.ResourceManager.GetString(nameof (MmiSettingReport_DocumentSubject), GlobalResource.resourceCulture);

    internal static string neoSDiag_Update_Available_Caption => GlobalResource.ResourceManager.GetString(nameof (neoSDiag_Update_Available_Caption), GlobalResource.resourceCulture);

    internal static string neoSDiag_Update_Available_Install_Soon_Caption => GlobalResource.ResourceManager.GetString(nameof (neoSDiag_Update_Available_Install_Soon_Caption), GlobalResource.resourceCulture);

    internal static string neoSDiag_Update_Available_Install_Soon_Message => GlobalResource.ResourceManager.GetString(nameof (neoSDiag_Update_Available_Install_Soon_Message), GlobalResource.resourceCulture);

    internal static string neoSDiag_Update_Available_Message => GlobalResource.ResourceManager.GetString(nameof (neoSDiag_Update_Available_Message), GlobalResource.resourceCulture);

    internal static string no => GlobalResource.ResourceManager.GetString(nameof (no), GlobalResource.resourceCulture);

    internal static string NoImage_RadioButton_Text => GlobalResource.ResourceManager.GetString(nameof (NoImage_RadioButton_Text), GlobalResource.resourceCulture);

    internal static string ok => GlobalResource.ResourceManager.GetString(nameof (ok), GlobalResource.resourceCulture);

    internal static string Parameter_Checked => GlobalResource.ResourceManager.GetString(nameof (Parameter_Checked), GlobalResource.resourceCulture);

    internal static string Parameter_Checking => GlobalResource.ResourceManager.GetString(nameof (Parameter_Checking), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_ACCU_CLIENT_STATE => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_ACCU_CLIENT_STATE), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_ACCU_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_ACCU_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_ACCU_VERSION_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_ACCU_VERSION_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_ACTUAL_LIGHT_SENSOR_VALUE => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_ACTUAL_LIGHT_SENSOR_VALUE), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_BACKGROUND_BRIGHTNESS_LEVELS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_BACKGROUND_BRIGHTNESS_LEVELS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_BUTTONS_STATE => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_BUTTONS_STATE), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_CONTROL_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_CONTROL_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_CUMULATIVE_SPEED_AND_TIME => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_CUMULATIVE_SPEED_AND_TIME), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_DATE_TIME => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_DATE_TIME), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_LIGTH_SENSOR_LEVELS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_LIGTH_SENSOR_LEVELS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_DEFAULT_SETTINGS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_DEFAULT_SETTINGS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_DISPLAY_COLORS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_DISPLAY_COLORS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_DRIVE_SETTINGS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_DRIVE_SETTINGS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_PRODUCTION_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_PRODUCTION_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_SHUTDOWN_OVERTRAVEL => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_SHUTDOWN_OVERTRAVEL), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_SPECIAL_DISPLAY_COLORS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_SPECIAL_DISPLAY_COLORS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MMI_VERSION_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MMI_VERSION_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MOTOR_BATTERY_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MOTOR_BATTERY_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MOTOR_CLIENT_STATE => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MOTOR_CLIENT_STATE), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MOTOR_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MOTOR_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MOTOR_SETTINGS => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MOTOR_SETTINGS), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_MOTOR_VERSION_INFORMATION => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_MOTOR_VERSION_INFORMATION), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_OEMID => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_OEMID), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_REST_OF_RANGE => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_REST_OF_RANGE), GlobalResource.resourceCulture);

    internal static string PARAMETER_ID_SERVICE_INTERVAL => GlobalResource.ResourceManager.GetString(nameof (PARAMETER_ID_SERVICE_INTERVAL), GlobalResource.resourceCulture);

    internal static string Parameter_Not_Checked => GlobalResource.ResourceManager.GetString(nameof (Parameter_Not_Checked), GlobalResource.resourceCulture);

    internal static string Parameter_Protocol_Value_Exception_Message => GlobalResource.ResourceManager.GetString(nameof (Parameter_Protocol_Value_Exception_Message), GlobalResource.resourceCulture);

    internal static string Parameter_Receive_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (Parameter_Receive_Error_Caption), GlobalResource.resourceCulture);

    internal static string Parameter_Receive_Error_Message => GlobalResource.ResourceManager.GetString(nameof (Parameter_Receive_Error_Message), GlobalResource.resourceCulture);

    internal static string Parameter_Transmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (Parameter_Transmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string Parameter_Transmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (Parameter_Transmission_Error_Message), GlobalResource.resourceCulture);

    internal static string ParameterListFile_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ParameterListFile_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string ParameterListFile_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ParameterListFile_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string ParameterMaxAssistanceSpeed_Caption => GlobalResource.ResourceManager.GetString(nameof (ParameterMaxAssistanceSpeed_Caption), GlobalResource.resourceCulture);

    internal static string ParameterMaxAssistanceSpeed_Message => GlobalResource.ResourceManager.GetString(nameof (ParameterMaxAssistanceSpeed_Message), GlobalResource.resourceCulture);

    internal static string ParameterPanel_IdenticalColor_Caption => GlobalResource.ResourceManager.GetString(nameof (ParameterPanel_IdenticalColor_Caption), GlobalResource.resourceCulture);

    internal static string ParameterPanel_IdenticalColor_Message => GlobalResource.ResourceManager.GetString(nameof (ParameterPanel_IdenticalColor_Message), GlobalResource.resourceCulture);

    internal static string ParameterPanel_State_Details => GlobalResource.ResourceManager.GetString(nameof (ParameterPanel_State_Details), GlobalResource.resourceCulture);

    internal static string ParameterSettings_Delete_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ParameterSettings_Delete_Error_Caption), GlobalResource.resourceCulture);

    internal static string ParameterSettings_Delete_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ParameterSettings_Delete_Error_Message), GlobalResource.resourceCulture);

    internal static string ParameterSettings_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ParameterSettings_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string ParameterSettings_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ParameterSettings_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string ParameterWheelCircumference_Caption => GlobalResource.ResourceManager.GetString(nameof (ParameterWheelCircumference_Caption), GlobalResource.resourceCulture);

    internal static string ParameterWheelCircumference_Message => GlobalResource.ResourceManager.GetString(nameof (ParameterWheelCircumference_Message), GlobalResource.resourceCulture);

    internal static string PdfReader_Not_Available_Caption => GlobalResource.ResourceManager.GetString(nameof (PdfReader_Not_Available_Caption), GlobalResource.resourceCulture);

    internal static string PdfReader_Not_Available_Message => GlobalResource.ResourceManager.GetString(nameof (PdfReader_Not_Available_Message), GlobalResource.resourceCulture);

    internal static string PictureEndTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (PictureEndTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string PictureEndTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (PictureEndTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string PictureStartTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (PictureStartTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string PictureStartTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (PictureStartTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string PictureTranmission_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (PictureTranmission_Error_Caption), GlobalResource.resourceCulture);

    internal static string PictureTranmission_Error_Message => GlobalResource.ResourceManager.GetString(nameof (PictureTranmission_Error_Message), GlobalResource.resourceCulture);

    internal static string PleaseConnect_Title => GlobalResource.ResourceManager.GetString(nameof (PleaseConnect_Title), GlobalResource.resourceCulture);

    internal static string ProductionNumber_InputBox_Caption => GlobalResource.ResourceManager.GetString(nameof (ProductionNumber_InputBox_Caption), GlobalResource.resourceCulture);

    internal static string ProductionNumber_InputBox_Message => GlobalResource.ResourceManager.GetString(nameof (ProductionNumber_InputBox_Message), GlobalResource.resourceCulture);

    internal static string ProductionNumber_Invalid_Caption => GlobalResource.ResourceManager.GetString(nameof (ProductionNumber_Invalid_Caption), GlobalResource.resourceCulture);

    internal static string ProductionNumber_Invalid_Message => GlobalResource.ResourceManager.GetString(nameof (ProductionNumber_Invalid_Message), GlobalResource.resourceCulture);

    internal static string ProductionNumberIsMissing_Caption => GlobalResource.ResourceManager.GetString(nameof (ProductionNumberIsMissing_Caption), GlobalResource.resourceCulture);

    internal static string ProductionNumberIsMissing_Message => GlobalResource.ResourceManager.GetString(nameof (ProductionNumberIsMissing_Message), GlobalResource.resourceCulture);

    internal static string ProfileListFile_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ProfileListFile_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string ProfileListFile_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ProfileListFile_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string ProfileSettings_Delete_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ProfileSettings_Delete_Error_Caption), GlobalResource.resourceCulture);

    internal static string ProfileSettings_Delete_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ProfileSettings_Delete_Error_Message), GlobalResource.resourceCulture);

    internal static string ProfileSettings_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ProfileSettings_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string ProfileSettings_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ProfileSettings_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string ProxySettings_Information_Caption => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Information_Caption), GlobalResource.resourceCulture);

    internal static string ProxySettings_Information_Message => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Information_Message), GlobalResource.resourceCulture);

    internal static string ProxySettings_Invalid_Credential_Caption => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Invalid_Credential_Caption), GlobalResource.resourceCulture);

    internal static string ProxySettings_Invalid_Credential_Message => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Invalid_Credential_Message), GlobalResource.resourceCulture);

    internal static string ProxySettings_Invalid_Uri_Caption => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Invalid_Uri_Caption), GlobalResource.resourceCulture);

    internal static string ProxySettings_Invalid_Uri_Message => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Invalid_Uri_Message), GlobalResource.resourceCulture);

    internal static string ProxySettings_Write_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Write_Error_Caption), GlobalResource.resourceCulture);

    internal static string ProxySettings_Write_Error_Message => GlobalResource.ResourceManager.GetString(nameof (ProxySettings_Write_Error_Message), GlobalResource.resourceCulture);

    internal static string PushAssistant_Warning_Caption => GlobalResource.ResourceManager.GetString(nameof (PushAssistant_Warning_Caption), GlobalResource.resourceCulture);

    internal static string PushAssistant_Warning_Message => GlobalResource.ResourceManager.GetString(nameof (PushAssistant_Warning_Message), GlobalResource.resourceCulture);

    internal static string Reactivation_Warning_MessageBox_Caption => GlobalResource.ResourceManager.GetString(nameof (Reactivation_Warning_MessageBox_Caption), GlobalResource.resourceCulture);

    internal static string Reactivation_Warning_MessageBox_Message => GlobalResource.ResourceManager.GetString(nameof (Reactivation_Warning_MessageBox_Message), GlobalResource.resourceCulture);

    internal static string RegInfoDialog_AcceptEProcessing_Caption => GlobalResource.ResourceManager.GetString(nameof (RegInfoDialog_AcceptEProcessing_Caption), GlobalResource.resourceCulture);

    internal static string RegInfoDialog_AcceptEProcessing_Message => GlobalResource.ResourceManager.GetString(nameof (RegInfoDialog_AcceptEProcessing_Message), GlobalResource.resourceCulture);

    internal static string RegInfoDialog_FillRequired_Caption => GlobalResource.ResourceManager.GetString(nameof (RegInfoDialog_FillRequired_Caption), GlobalResource.resourceCulture);

    internal static string RegInfoDialog_FillRequired_Message => GlobalResource.ResourceManager.GetString(nameof (RegInfoDialog_FillRequired_Message), GlobalResource.resourceCulture);

    internal static string RegInfoDialog_InvalidEMail_Caption => GlobalResource.ResourceManager.GetString(nameof (RegInfoDialog_InvalidEMail_Caption), GlobalResource.resourceCulture);

    internal static string RegInfoDialog_InvalidEMail_Message => GlobalResource.ResourceManager.GetString(nameof (RegInfoDialog_InvalidEMail_Message), GlobalResource.resourceCulture);

    internal static string Report_NoErrorMessage_Text => GlobalResource.ResourceManager.GetString(nameof (Report_NoErrorMessage_Text), GlobalResource.resourceCulture);

    internal static string RingBuffer_Delete_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (RingBuffer_Delete_Error_Caption), GlobalResource.resourceCulture);

    internal static string RingBuffer_Delete_Error_Message => GlobalResource.ResourceManager.GetString(nameof (RingBuffer_Delete_Error_Message), GlobalResource.resourceCulture);

    internal static string RingBuffer_Receive_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (RingBuffer_Receive_Error_Caption), GlobalResource.resourceCulture);

    internal static string RingBuffer_Receive_Error_Message => GlobalResource.ResourceManager.GetString(nameof (RingBuffer_Receive_Error_Message), GlobalResource.resourceCulture);

    internal static string SaveSettingsQuestion_Caption => GlobalResource.ResourceManager.GetString(nameof (SaveSettingsQuestion_Caption), GlobalResource.resourceCulture);

    internal static string SaveSettingsQuestion_Message => GlobalResource.ResourceManager.GetString(nameof (SaveSettingsQuestion_Message), GlobalResource.resourceCulture);

    internal static string SettingFile_NotFound_Caption => GlobalResource.ResourceManager.GetString(nameof (SettingFile_NotFound_Caption), GlobalResource.resourceCulture);

    internal static string SettingFile_NotFound_Message => GlobalResource.ResourceManager.GetString(nameof (SettingFile_NotFound_Message), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Actual_Kilometers => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Actual_Kilometers), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Battery => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Battery), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Checklist => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Checklist), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Comment => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Comment), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_ComponentInformation => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_ComponentInformation), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_DiagnoseReportCaption => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_DiagnoseReportCaption), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_DiagnoseTableCaption => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_DiagnoseTableCaption), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Hardware_Version => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Hardware_Version), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_MMI => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_MMI), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Modelyear => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Modelyear), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Motor => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Motor), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Neo_Version => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Neo_Version), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_RecommendedProcedureCaption => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_RecommendedProcedureCaption), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_ReportCheckedCaption => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_ReportCheckedCaption), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Serial_Number => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Serial_Number), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_ServiceOrder => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_ServiceOrder), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_ServiceReport => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_ServiceReport), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_SettingCaption => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_SettingCaption), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Software_Version => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Software_Version), GlobalResource.resourceCulture);

    internal static string ShortErrorReport_Update_Recommended => GlobalResource.ResourceManager.GetString(nameof (ShortErrorReport_Update_Recommended), GlobalResource.resourceCulture);

    internal static string ShortReport_SOH_NOK => GlobalResource.ResourceManager.GetString(nameof (ShortReport_SOH_NOK), GlobalResource.resourceCulture);

    internal static string ShortReport_SOH_OK => GlobalResource.ResourceManager.GetString(nameof (ShortReport_SOH_OK), GlobalResource.resourceCulture);

    internal static string SOUND_FILE_INVALID_Message => GlobalResource.ResourceManager.GetString(nameof (SOUND_FILE_INVALID_Message), GlobalResource.resourceCulture);

    internal static string SystemOutOfMemory_Caption => GlobalResource.ResourceManager.GetString(nameof (SystemOutOfMemory_Caption), GlobalResource.resourceCulture);

    internal static string SystemOutOfMemory_Message => GlobalResource.ResourceManager.GetString(nameof (SystemOutOfMemory_Message), GlobalResource.resourceCulture);

    internal static string TestMode_Firmware_ManualAdded => GlobalResource.ResourceManager.GetString(nameof (TestMode_Firmware_ManualAdded), GlobalResource.resourceCulture);

    internal static string TestMode_Firmware_ManualAdded_AlreadyExits => GlobalResource.ResourceManager.GetString(nameof (TestMode_Firmware_ManualAdded_AlreadyExits), GlobalResource.resourceCulture);

    internal static string TestMode_Firmware_ManualAdded_CopyFailed => GlobalResource.ResourceManager.GetString(nameof (TestMode_Firmware_ManualAdded_CopyFailed), GlobalResource.resourceCulture);

    internal static string TestMode_Firmware_ManualAdded_DirCreateFailed => GlobalResource.ResourceManager.GetString(nameof (TestMode_Firmware_ManualAdded_DirCreateFailed), GlobalResource.resourceCulture);

    internal static string TestMode_Firmware_ManualAdded_Done => GlobalResource.ResourceManager.GetString(nameof (TestMode_Firmware_ManualAdded_Done), GlobalResource.resourceCulture);

    internal static string TestMode_Firmware_ManualAdded_SameFiles => GlobalResource.ResourceManager.GetString(nameof (TestMode_Firmware_ManualAdded_SameFiles), GlobalResource.resourceCulture);

    internal static string ToolTip_Title_Information => GlobalResource.ResourceManager.GetString(nameof (ToolTip_Title_Information), GlobalResource.resourceCulture);

    internal static string ToolTip_Title_Warning => GlobalResource.ResourceManager.GetString(nameof (ToolTip_Title_Warning), GlobalResource.resourceCulture);

    internal static string Unknown => GlobalResource.ResourceManager.GetString(nameof (Unknown), GlobalResource.resourceCulture);

    internal static string Update_Firmware_Delete_MessageBox_Caption => GlobalResource.ResourceManager.GetString(nameof (Update_Firmware_Delete_MessageBox_Caption), GlobalResource.resourceCulture);

    internal static string Update_Firmware_Delete_MessageBox_Message => GlobalResource.ResourceManager.GetString(nameof (Update_Firmware_Delete_MessageBox_Message), GlobalResource.resourceCulture);

    internal static string Update_Firmware_Transmit_MessageBox_Caption => GlobalResource.ResourceManager.GetString(nameof (Update_Firmware_Transmit_MessageBox_Caption), GlobalResource.resourceCulture);

    internal static string Update_Firmware_Transmit_MessageBox_Message => GlobalResource.ResourceManager.GetString(nameof (Update_Firmware_Transmit_MessageBox_Message), GlobalResource.resourceCulture);

    internal static string UpdateFlag_Receive_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (UpdateFlag_Receive_Error_Caption), GlobalResource.resourceCulture);

    internal static string UpdateFlag_Receive_Error_Message => GlobalResource.ResourceManager.GetString(nameof (UpdateFlag_Receive_Error_Message), GlobalResource.resourceCulture);

    internal static string UpdateFlag_Transmit_Error_Caption => GlobalResource.ResourceManager.GetString(nameof (UpdateFlag_Transmit_Error_Caption), GlobalResource.resourceCulture);

    internal static string UpdateFlag_Transmit_Error_Message => GlobalResource.ResourceManager.GetString(nameof (UpdateFlag_Transmit_Error_Message), GlobalResource.resourceCulture);

    internal static string UpdatePanel_Accu_Firmware_Delete => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_Accu_Firmware_Delete), GlobalResource.resourceCulture);

    internal static string UpdatePanel_DownloadProgressBar_AdditionalText => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_DownloadProgressBar_AdditionalText), GlobalResource.resourceCulture);

    internal static string UpdatePanel_FirmwareDeactived => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_FirmwareDeactived), GlobalResource.resourceCulture);

    internal static string UpdatePanel_FirmwareInstalled => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_FirmwareInstalled), GlobalResource.resourceCulture);

    internal static string UpdatePanel_Motor_Bootloader_Delete => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_Motor_Bootloader_Delete), GlobalResource.resourceCulture);

    internal static string UpdatePanel_Motor_Firmware_Delete => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_Motor_Firmware_Delete), GlobalResource.resourceCulture);

    internal static string UpdatePanel_ProgressBar_ComponentsUpToDate => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_ProgressBar_ComponentsUpToDate), GlobalResource.resourceCulture);

    internal static string UpdatePanel_ProgressBar_MotorBootloader => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_ProgressBar_MotorBootloader), GlobalResource.resourceCulture);

    internal static string UpdatePanel_RemoveButtons_Text => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_RemoveButtons_Text), GlobalResource.resourceCulture);

    internal static string UpdatePanel_RemoveFirmware_Caption => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_RemoveFirmware_Caption), GlobalResource.resourceCulture);

    internal static string UpdatePanel_RemoveFirmware_Message => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_RemoveFirmware_Message), GlobalResource.resourceCulture);

    internal static string UpdatePanel_TotalProgressBar_AdditionalText => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_TotalProgressBar_AdditionalText), GlobalResource.resourceCulture);

    internal static string UpdatePanel_Update_Components_MessageBox_Caption => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_Update_Components_MessageBox_Caption), GlobalResource.resourceCulture);

    internal static string UpdatePanel_Update_Components_MessageBox_Message => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_Update_Components_MessageBox_Message), GlobalResource.resourceCulture);

    internal static string UpdatePanel_WrongMmiFirmwareVersion_Caption => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_WrongMmiFirmwareVersion_Caption), GlobalResource.resourceCulture);

    internal static string UpdatePanel_WrongMmiFirmwareVersion_Message => GlobalResource.ResourceManager.GetString(nameof (UpdatePanel_WrongMmiFirmwareVersion_Message), GlobalResource.resourceCulture);

    internal static string UpdateWorker_ConnectingServer => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_ConnectingServer), GlobalResource.resourceCulture);

    internal static string UpdateWorker_DownloadSuccess => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_DownloadSuccess), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_Accu => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_Accu), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_ConnectMMI => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_ConnectMMI), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_DFI => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_DFI), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_Help => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_Help), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_Knowledge => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_Knowledge), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_MMI => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_MMI), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_Motor => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_Motor), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_neosDiag => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_neosDiag), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_News => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_News), GlobalResource.resourceCulture);

    internal static string UpdateWorker_Element_Profil => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_Element_Profil), GlobalResource.resourceCulture);

    internal static string UpdateWorker_InvalidLicense => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_InvalidLicense), GlobalResource.resourceCulture);

    internal static string UpdateWorker_LostConnection => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_LostConnection), GlobalResource.resourceCulture);

    internal static string UpdateWorker_NumOfUpdates => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_NumOfUpdates), GlobalResource.resourceCulture);

    internal static string UpdateWorker_ProcessDone => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_ProcessDone), GlobalResource.resourceCulture);

    internal static string UpdateWorker_ProcessFailed => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_ProcessFailed), GlobalResource.resourceCulture);

    internal static string UpdateWorker_ReActivation => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_ReActivation), GlobalResource.resourceCulture);

    internal static string UpdateWorker_ServerNotReachable => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_ServerNotReachable), GlobalResource.resourceCulture);

    internal static string UpdateWorker_StartDownload => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_StartDownload), GlobalResource.resourceCulture);

    internal static string UpdateWorker_StartET => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_StartET), GlobalResource.resourceCulture);

    internal static string UpdateWorker_StartExtraction => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_StartExtraction), GlobalResource.resourceCulture);

    internal static string UpdateWorker_StartUpdate => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_StartUpdate), GlobalResource.resourceCulture);

    internal static string UpdateWorker_UpdateDone => GlobalResource.ResourceManager.GetString(nameof (UpdateWorker_UpdateDone), GlobalResource.resourceCulture);

    internal static string WebException_Check_Proxy_Settings => GlobalResource.ResourceManager.GetString(nameof (WebException_Check_Proxy_Settings), GlobalResource.resourceCulture);

    internal static string WrongFilePermission_Caption => GlobalResource.ResourceManager.GetString(nameof (WrongFilePermission_Caption), GlobalResource.resourceCulture);

    internal static string WrongFilePermission_Message => GlobalResource.ResourceManager.GetString(nameof (WrongFilePermission_Message), GlobalResource.resourceCulture);

    internal static string yes => GlobalResource.ResourceManager.GetString(nameof (yes), GlobalResource.resourceCulture);
  }
}
