// Decompiled with JetBrains decompiler
// Type: ZerroWare.DiagnosePanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using MMI;
using Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class DiagnosePanel : UserControl
  {
    private Communication MMICom;
    private MMITransceiver transceiver;
    private WaitingForReboot rebootDialog;
    private readonly int[] headerPages = new int[5]
    {
      1,
      13,
      25,
      37,
      49
    };
    private readonly int motorHeaderPageBytePos = 16;
    private readonly int motorHeaderPageErrorBytePos = 17;
    private readonly int motorHeaderDetailPageBytePos0;
    private readonly int motorHeaderDetailPageBytePos1 = 1;
    private readonly int motorGreenFlag = 7;
    private readonly int motorOrangeFlag = 63;
    private readonly int motorShortCircuitInverter = 2;
    private readonly string minValidBatteryVersion = "1.2";
    private NumericUpDownWithSpecialValue nextServiceDistance;
    private DateControlWithActivation serviceInterval;
    private SmartToolTip firmwareVersionToolTip;
    private string custStreet;
    private string custName;
    private string custCity;
    private string custComment;
    private bool immobilizerIsAvailable;
    private bool motorHasErrors;
    private bool accuFirmwareHasBeenUpdated;
    private int firmwareUpdateCounter;
    private bool veryOldFirmwareHasBeeenUpdated;
    private int oldHeight;
    private int easterEggCounter;
    private bool easterEggFlipper;
    private IContainer components;
    private TabControl diagnoseTabControl;
    private TabPage diagnoseStatusTabPage;
    private PictureBox motorHealtyPictureBox;
    private PictureBox accuHealtyPictureBox;
    private Label label5;
    private Label label6;
    private TabPage diagnoseTabPage;
    private BikePictureBox bikePictureBox;
    private SmartToolTip immobilizerToolTip;
    private ErrorOverviewPanel errorOverviewPanel;
    private PictureBox mmiHealtyPictureBox;
    private Label label7;
    private Button createShortReportButton;
    private Label immobilizerCaptionLabel;
    private SmartToolTip healthyToolTip;
    private SmartToolTip upToDateToolTip;
    private Button deleteErrorLogButton;
    private Button serviceOrderButton;
    private Label serviceDateCaption;
    private Label serviceKilometerCaption;
    private Button diagnoseButton;
    private Button firmwareButton;
    private Button sDiagButton;
    private Label serviceCaption;
    private Button transferButton;
    private FlickerFreeTableLayoutPanel distanceTableLayoutPanel;
    private PictureBox distancePictureBox;
    private FlickerFreeTableLayoutPanel dateTableLayoutPanel;
    private PictureBox datePictureBox;
    private Label totalKilometersLabel;
    private PictureBox bikeFlippedPictureBox;
    private FlickerFreeTableLayoutPanel flickerFreeTableLayoutPanel4;
    private FlickerFreeTableLayoutPanel flickerFreeTableLayoutPanel3;
    private PictureBox sDiagPictureBox;
    private PictureBox firmwarePictureBox;
    private PictureBox diagnosePictureBox;
    private Panel kilometerPanel;
    private FlickerFreeTableLayoutPanel kilometersFlickerFreeTableLayout;
    private Panel servicePanel;
    private FlickerFreeTableLayoutPanel serviceFlickerFreeTableLayout;
    private Panel immobilizerPanel;
    private FlickerFreeTableLayoutPanel immobilizerFlickerFreeTableLayout;
    private PictureBox pictureBox1;
    private FlickerFreeTableLayoutPanel immobilizerFlickerFreeTableLayout2;
    private CheckBox lockCheckBox;
    private FlickerFreeTableLayoutPanel hackFlickerFreeTableLayout;
    private FlickerFreeTableLayoutPanel accuTableLayoutPanel;
    private Label accuByteLabel;
    private TextBox accuBytesTextBox;
    private Button readTourDataButton;

    public DiagnosePanel(Communication MMICom)
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.firmwareVersionToolTip = new SmartToolTip();
      this.SuspendLayout();
      this.DefaultUISettings();
      this.ResumeLayout(false);
      this.MMICom = MMICom;
      this.transceiver = new MMITransceiver(this.MMICom);
      this.rebootDialog = (WaitingForReboot) null;
      MainWindow.Instance.HelloLoopPassed += new MainWindow.HelloLoopPassedEventHandler(this.HelloLoopPassedHandler);
      ((Control) this.diagnoseTabPage).Enabled = false;
      if (ActivationInformation.VersionLevelProperty < 4)
        this.diagnoseTabControl.TabPages.Remove(this.diagnoseTabPage);
      this.createServiceElements();
      this.transferEnable = false;
      this.immobilizerIsAvailable = true;
      this.UpdateInformation();
      this.custStreet = string.Empty;
      this.custName = string.Empty;
      this.custCity = string.Empty;
      this.custComment = string.Empty;
    }

    private void DefaultUISettings()
    {
      this.SizeChanged += new EventHandler(this.DiagnosePanel_SizeChanged);
      this.VisibleChanged += new EventHandler(this.DiagnosePanel_VisibleChanged);
      this.Load += new EventHandler(this.DiagnosePanel_Load);
      this.diagnoseTabControl.Font = FontDefinition.MenubarFont;
      this.diagnoseTabPage.Font = FontDefinition.DefaultTextFont;
      this.diagnoseStatusTabPage.Font = FontDefinition.DefaultTextFont;
      this.diagnoseTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.diagnoseStatusTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.serviceCaption.Font = FontDefinition.BoldCaptionTextFont;
      this.label5.Font = FontDefinition.TableCaptionTextFont;
      this.label6.Font = FontDefinition.TableCaptionTextFont;
      this.label7.Font = FontDefinition.TableCaptionTextFont;
      this.serviceKilometerCaption.Font = FontDefinition.TableCaptionTextFont;
      this.serviceDateCaption.Font = FontDefinition.TableCaptionTextFont;
      this.immobilizerCaptionLabel.Font = FontDefinition.TableCaptionTextFont;
      this.totalKilometersLabel.Font = FontDefinition.DefaultTextFont;
      this.createShortReportButton.Font = FontDefinition.TableCaptionTextFont;
      this.createShortReportButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.createShortReportButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.deleteErrorLogButton.Font = FontDefinition.SmallDefaultTextFont;
      this.deleteErrorLogButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.deleteErrorLogButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.serviceOrderButton.Font = FontDefinition.TableCaptionTextFont;
      this.serviceOrderButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.serviceOrderButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.diagnoseButton.Font = FontDefinition.TableCaptionTextFont;
      this.diagnoseButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.diagnoseButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.firmwareButton.Font = FontDefinition.TableCaptionTextFont;
      this.firmwareButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.firmwareButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.sDiagButton.Font = FontDefinition.TableCaptionTextFont;
      this.sDiagButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.sDiagButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.transferButton.Font = FontDefinition.TableCaptionTextFont;
      this.transferButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.transferButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.healthyToolTip.SetToolTip((Control) this.mmiHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_Healthy);
      this.immobilizerToolTip.AutoPopDelay = 20000;
      this.immobilizerToolTip.ShowAlways = true;
    }

    private void DFISafetyMessage()
    {
      if (MainWindow.Instance.IsMMIUpdateInProgress || MMIData.Instance.AccuErrorBlocks.Length != 0 || MMIData.Instance.AccuFirmwareVersion.Equals("1.0"))
        return;
      int num = (int) MessageBox.Show(GlobalResource.DiagnosePanel_DFI_Safety_Message, GlobalResource.DiagnosePanel_DFI_Safety_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void MotorSerialsDoNotMatchMessage()
    {
      if (MainWindow.Instance.IsMMIUpdateInProgress)
        return;
      string str = this.errorOverviewPanel.MotorSerialErrorNumber();
      if (!this.motorHasErrors || !(str != HelperClass.AddSpacesToSerialNumber(Parameters.Instance.MotorSerialNumber)))
        return;
      int num = (int) MessageBox.Show(GlobalResource.DiagnosePanel_MotorSerialsNotMatch_Message, GlobalResource.DiagnosePanel_MotorSerialsNotMatch_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void DiagnosePanel_Load(object sender, EventArgs e)
    {
      this.DFISafetyMessage();
      this.MotorSerialsDoNotMatchMessage();
    }

    private void DiagnosePanel_VisibleChanged(object sender, EventArgs e)
    {
      if (!this.Visible)
        return;
      this.UpdateInformation();
      this.DFISafetyMessage();
      this.MotorSerialsDoNotMatchMessage();
    }

    private void DiagnosePanel_SizeChanged(object sender, EventArgs e)
    {
      int height = this.Size.Height;
      if (this.easterEggFlipper)
      {
        if (this.oldHeight > height)
        {
          ++this.easterEggCounter;
          this.easterEggFlipper = false;
        }
      }
      else if (this.oldHeight < height)
      {
        ++this.easterEggCounter;
        this.easterEggFlipper = true;
      }
      this.oldHeight = height;
      if (this.easterEggCounter != 50 || this.bikeFlippedPictureBox.Visible)
        return;
      this.bikePictureBox.Visible = false;
      this.bikeFlippedPictureBox.Visible = true;
      BikeAnimation bikeAnimation = new BikeAnimation(this.bikePictureBox.GetImage(), (Image) Resources.bike_flipped);
      bikeAnimation.AnimatedElement += new BikeAnimation.AnimatedSequenceElement(this.animate_AnimatedElement);
      new Thread(new ThreadStart(bikeAnimation.Start)).Start();
    }

    private void animate_AnimatedElement(object sender, BikeAnimationEventArgs e)
    {
      this.bikeFlippedPictureBox.Image = e.Animated;
      HelperClass.DoEvents();
    }

    private void servicePlausibilityCheck()
    {
      // ISSUE: reference to a compiler-generated method
      this.distancePictureBox.Image = (double) this.nextServiceDistance.Value <= MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESAMTKILOMETER)].ReadableValue ? (!(this.nextServiceDistance.SpecialValue == this.nextServiceDistance.Value) ? (Image) Resources.buttons_error : (Image) Resources.button_ok) : (Image) Resources.button_ok;
      if (this.serviceInterval.Value > DateTime.Now)
        this.datePictureBox.Image = (Image) Resources.button_ok;
      else if (this.serviceInterval.SpecialDate == this.serviceInterval.Value)
        this.datePictureBox.Image = (Image) Resources.button_ok;
      else
        this.datePictureBox.Image = (Image) Resources.buttons_error;
    }

    private bool transferEnable
    {
      set => this.transferButton.Enabled = value;
      get => this.transferButton.Enabled;
    }

    private void nextServiceDistance_ValueChanged(object sender, EventArgs e)
    {
      this.transferEnable = true;
      this.servicePlausibilityCheck();
    }

    private void serviceInterval_ValueChanged(object sender, EventArgs e)
    {
      this.transferEnable = true;
      this.servicePlausibilityCheck();
    }

    private void UpdateServiceValues()
    {
      this.nextServiceDistance.ValueChanged -= new NumericUpDownWithSpecialValue.ValueChangedEventHandler(this.nextServiceDistance_ValueChanged);
      this.serviceInterval.ValueChanged -= new DateControlWithActivation.ValueChangedEventHandler(this.serviceInterval_ValueChanged);
      // ISSUE: reference to a compiler-generated method
      this.nextServiceDistance.Value = (Decimal) MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_STRECKE)].ReadableValue;
      // ISSUE: reference to a compiler-generated method
      ParameterValue mmiParameter = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_JAHR)];
      this.serviceInterval.Reset = true;
      this.serviceInterval.ValueYear = (int) mmiParameter.ReadableValue;
      // ISSUE: reference to a compiler-generated method
      this.serviceInterval.ValueMonth = (int) MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_MONAT)].ReadableValue;
      // ISSUE: reference to a compiler-generated method
      this.serviceInterval.ValueDay = (int) MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_TAG)].ReadableValue;
      // ISSUE: reference to a compiler-generated method
      this.totalKilometersLabel.Text = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESAMTKILOMETER)].ReadableValue.ToString() + " /";
      this.serviceInterval.Reset = false;
      this.nextServiceDistance.ValueChanged += new NumericUpDownWithSpecialValue.ValueChangedEventHandler(this.nextServiceDistance_ValueChanged);
      this.serviceInterval.ValueChanged += new DateControlWithActivation.ValueChangedEventHandler(this.serviceInterval_ValueChanged);
      this.servicePlausibilityCheck();
    }

    private void createServiceElements()
    {
      // ISSUE: reference to a compiler-generated method
      ParameterValue mmiParameter1 = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_STRECKE)];
      this.nextServiceDistance = new NumericUpDownWithSpecialValue();
      this.nextServiceDistance.BeginInit();
      this.nextServiceDistance.ShowConversion = false;
      this.nextServiceDistance.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      this.nextServiceDistance.ValueChanged += new NumericUpDownWithSpecialValue.ValueChangedEventHandler(this.nextServiceDistance_ValueChanged);
      this.nextServiceDistance.SpecialValue = 0M;
      this.nextServiceDistance.DecimalPlaces = 0;
      this.nextServiceDistance.Font = FontDefinition.DefaultTextFont;
      this.nextServiceDistance.ForeColor = ColorDefinition.BlackTextColor;
      this.nextServiceDistance.Minimum = (Decimal) mmiParameter1.ReadableMinimumValue;
      this.nextServiceDistance.Maximum = (Decimal) mmiParameter1.ReadableMaximumValue;
      this.nextServiceDistance.Value = (Decimal) mmiParameter1.ReadableValue;
      this.nextServiceDistance.DefaultValue = (Decimal) mmiParameter1.ReadableDefaultValue;
      this.nextServiceDistance.Increment = (Decimal) mmiParameter1.StepSize;
      this.nextServiceDistance.EndInit();
      // ISSUE: reference to a compiler-generated method
      ParameterValue mmiParameter2 = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_JAHR)];
      this.serviceInterval = new DateControlWithActivation();
      this.serviceInterval.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      this.serviceInterval.MinYear = (int) mmiParameter2.ReadableMinimumValue;
      this.serviceInterval.VisibleMinYear = 2012;
      this.serviceInterval.MaxYear = (int) mmiParameter2.ReadableMaximumValue;
      this.serviceInterval.DefaultYear = (int) mmiParameter2.ReadableDefaultValue;
      this.serviceInterval.ValueYear = (int) mmiParameter2.ReadableValue;
      this.serviceInterval.SpecialYear = 2000;
      this.serviceInterval.ValueChanged += new DateControlWithActivation.ValueChangedEventHandler(this.serviceInterval_ValueChanged);
      // ISSUE: reference to a compiler-generated method
      ParameterValue mmiParameter3 = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_MONAT)];
      this.serviceInterval.MinMonth = (int) mmiParameter3.ReadableMinimumValue;
      this.serviceInterval.MaxMonth = (int) mmiParameter3.ReadableMaximumValue;
      this.serviceInterval.DefaultMonth = (int) mmiParameter3.ReadableDefaultValue;
      this.serviceInterval.ValueMonth = (int) mmiParameter3.ReadableValue;
      this.serviceInterval.SpecialMonth = 1;
      // ISSUE: reference to a compiler-generated method
      ParameterValue mmiParameter4 = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_TAG)];
      this.serviceInterval.MinDay = (int) mmiParameter4.ReadableMinimumValue;
      this.serviceInterval.MaxDay = (int) mmiParameter4.ReadableMaximumValue;
      this.serviceInterval.DefaultDay = (int) mmiParameter4.ReadableDefaultValue;
      this.serviceInterval.ValueDay = (int) mmiParameter4.ReadableValue;
      this.serviceInterval.SpecialDay = 1;
      // ISSUE: reference to a compiler-generated method
      this.totalKilometersLabel.Text = MMIData.Instance.MMIParameters()[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESAMTKILOMETER)].ReadableValue.ToString() + " /";
      this.nextServiceDistance.Size = new Size(this.serviceInterval.Width, this.nextServiceDistance.Height);
      this.distanceTableLayoutPanel.Controls.Add((Control) this.nextServiceDistance, 1, 0);
      this.dateTableLayoutPanel.Controls.Add((Control) this.serviceInterval, 0, 0);
      this.lockCheckBox.Size = new Size(this.serviceInterval.Width, this.lockCheckBox.Height);
    }

    private void changeButtonState(Button change, EventHandler method, bool stateOk)
    {
      change.Click -= method;
      if (stateOk)
      {
        if (this.sDiagButton.Name == change.Name)
          this.sDiagPictureBox.Image = (Image) Resources.button_ok;
        else if (this.firmwareButton.Name == change.Name)
          this.firmwarePictureBox.Image = (Image) Resources.button_ok;
        else if (this.diagnoseButton.Name == change.Name)
          this.diagnosePictureBox.Image = (Image) Resources.button_ok;
        change.FlatStyle = FlatStyle.Flat;
        change.BackgroundImage = (Image) null;
        change.BackColor = Color.White;
        change.FlatAppearance.MouseOverBackColor = Color.White;
        change.FlatAppearance.MouseDownBackColor = Color.White;
        change.FlatAppearance.BorderSize = 1;
        change.TabStop = false;
      }
      else
      {
        if (this.sDiagButton.Name == change.Name)
          this.sDiagPictureBox.Image = (Image) Resources.buttons_error;
        else if (this.firmwareButton.Name == change.Name)
          this.firmwarePictureBox.Image = (Image) Resources.buttons_error;
        else if (this.diagnoseButton.Name == change.Name)
          this.diagnosePictureBox.Image = (Image) Resources.buttons_error;
        change.FlatStyle = FlatStyle.Standard;
        change.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        change.BackgroundImageLayout = ImageLayout.Stretch;
        change.Click += method;
        change.TabStop = true;
      }
      HelperClass.DoEvents();
    }

    private void DiagnosePanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.Parent == null || !(this.Size != this.Parent.Size))
        return;
      this.Size = this.Parent.Size;
    }

    private void MMILockCheckbox_stateChanged(object sender, EventArgs e)
    {
      this.transferEnable = true;
      if (!this.lockCheckBox.Checked)
      {
        this.immobilizerToolTip.SetToolTip((Control) this.lockCheckBox, GlobalResource.DiagnosePanel_LockOpenToolTip + "\n\n" + GlobalResource.Mmi_Lock_Information);
        this.lockCheckBox.Text = GlobalResource.DiagnosePanel_ImmobilizerInactive;
      }
      else
      {
        this.immobilizerToolTip.SetToolTip((Control) this.lockCheckBox, GlobalResource.DiagnosePanel_LockBreakToolTip + "\n\n" + GlobalResource.Mmi_Lock_Information);
        this.lockCheckBox.Text = GlobalResource.DiagnosePanel_ImmobilizerActive;
      }
    }

    private int TrafficLightStateMotor(byte[][] motorBlocks)
    {
      byte[] numArray1 = new byte[24]
      {
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue
      };
      int num1 = (int) motorBlocks[0][0] - 1;
      int[] numArray2 = new int[5];
      int[][] numArray3 = new int[5][];
      int[] numArray4 = new int[5];
      for (int index = 0; index < numArray2.Length; ++index)
      {
        numArray2[index] = (int) motorBlocks[this.headerPages[0]][this.motorHeaderPageBytePos];
        numArray3[index] = new int[2];
        numArray3[index][0] = (int) motorBlocks[this.headerPages[index] + 1][this.motorHeaderDetailPageBytePos0];
        numArray3[index][1] = (int) motorBlocks[this.headerPages[index] + 1][this.motorHeaderDetailPageBytePos1];
        numArray4[index] = (int) motorBlocks[this.headerPages[0]][this.motorHeaderPageErrorBytePos];
        if (ActivationInformation.VersionLevelProperty >= 4 ? ((IEnumerable<byte>) numArray1).SequenceEqual<byte>((IEnumerable<byte>) motorBlocks[this.headerPages[index]]) || index > num1 : numArray4[index] == 40 || ((IEnumerable<byte>) numArray1).SequenceEqual<byte>((IEnumerable<byte>) motorBlocks[this.headerPages[index]]) || index > num1)
        {
          numArray2[index] = 0;
          numArray3[index][0] = 0;
          numArray3[index][1] = 0;
        }
      }
      int val1_1 = Math.Max(numArray2[0], numArray2[1]);
      if (HelperClass.IsBitSetInByte((byte) val1_1, this.motorShortCircuitInverter))
        val1_1 = this.motorOrangeFlag + 1;
      int val1_2 = Math.Max(val1_1, numArray2[2]);
      if (HelperClass.IsBitSetInByte((byte) val1_2, this.motorShortCircuitInverter))
        val1_2 = this.motorOrangeFlag + 1;
      int val1_3 = Math.Max(val1_2, numArray2[3]);
      if (HelperClass.IsBitSetInByte((byte) val1_3, this.motorShortCircuitInverter))
        val1_3 = this.motorOrangeFlag + 1;
      int val1_4 = Math.Max(val1_3, numArray2[4]);
      if (HelperClass.IsBitSetInByte((byte) val1_4, this.motorShortCircuitInverter))
        val1_4 = this.motorOrangeFlag + 1;
      int num2 = Math.Max(Math.Max(Math.Max(Math.Max(numArray3[0][0], numArray3[1][0]), numArray3[2][0]), numArray3[3][0]), numArray3[4][0]);
      int num3 = Math.Max(Math.Max(Math.Max(Math.Max(numArray3[0][1], numArray3[1][1]), numArray3[2][1]), numArray3[3][1]), numArray3[4][1]);
      int val2_1;
      switch (num2)
      {
        case 0:
          val2_1 = this.motorGreenFlag;
          break;
        case 64:
          val2_1 = this.motorOrangeFlag;
          break;
        default:
          val2_1 = this.motorOrangeFlag + 1;
          break;
      }
      int val2_2;
      switch (num3)
      {
        case 0:
          val2_2 = this.motorGreenFlag;
          break;
        case 2:
          val2_2 = this.motorOrangeFlag;
          break;
        default:
          val2_2 = this.motorOrangeFlag + 1;
          break;
      }
      return Math.Max(Math.Max(val1_4, val2_1), val2_2);
    }

    private bool IsErrorFree()
    {
      bool flag = false;
      byte[][] motorErrorBlocks = MMIData.Instance.MotorErrorBlocks;
      byte[] accuErrorBlocks = MMIData.Instance.AccuErrorBlocks;
      // ISSUE: reference to a compiler-generated method
      Parameters.Instance.SetDictionary(MMIData.Instance.MMIParameters());
      if (motorErrorBlocks.Length == 0)
      {
        this.motorHealtyPictureBox.Image = (Image) Resources.button_ok;
        this.motorHealtyPictureBox.Cursor = Cursors.Default;
        this.bikePictureBox.Motor = (Image) Resources.Bikestatus_complete_Motor;
        this.healthyToolTip.SetToolTip((Control) this.motorHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_Healthy);
        ((Control) this.diagnoseTabPage).Enabled = false;
        flag = true;
        this.deleteErrorLogButton.Enabled = false;
      }
      else
      {
        this.deleteErrorLogButton.Enabled = true;
        int num = this.TrafficLightStateMotor(motorErrorBlocks);
        if (num <= this.motorGreenFlag)
        {
          this.motorHealtyPictureBox.Image = (Image) Resources.button_ok;
          this.bikePictureBox.Motor = (Image) Resources.Bikestatus_complete_Motor;
          this.healthyToolTip.SetToolTip((Control) this.motorHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_Healthy);
          flag = true;
        }
        else if (num > this.motorGreenFlag && num <= this.motorOrangeFlag)
        {
          this.motorHealtyPictureBox.Image = (Image) Resources.button_orange_ok;
          this.bikePictureBox.Motor = (Image) Resources.Bikestatus_orange_error_Motor;
          this.healthyToolTip.SetToolTip((Control) this.motorHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_NotHealthy);
          flag = true;
        }
        else if (num > this.motorOrangeFlag)
        {
          this.motorHealtyPictureBox.Image = (Image) Resources.buttons_error;
          this.bikePictureBox.Motor = (Image) Resources.Bikestatus_error_Motor;
          this.healthyToolTip.SetToolTip((Control) this.motorHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_NotHealthy);
          flag = false;
        }
        ((Control) this.diagnoseTabPage).Enabled = true;
      }
      MMIErrorDetail detail = new MMIErrorDetail();
      detail.MotorSerial = Parameters.Instance.MotorSerialNumber;
      detail.BatterySerial = Parameters.Instance.AccuSerialNumber;
      detail.MMISerial = Parameters.Instance.MMISerialNumber;
      detail.ErrorBlocks = motorErrorBlocks;
      detail.AccuInformation = accuErrorBlocks;
      int length = Parameters.Instance.ParameterElements - 1;
      detail.ParameterContent = new byte[length][];
      foreach (ParameterIds id in Enum.GetValues(typeof (ParameterIds)))
      {
        int index = (int) (id - (byte) 1);
        // ISSUE: reference to a compiler-generated method
        detail.ParameterContent[index] = Parameters.Instance.GetSetOfParameters(id);
      }
      try
      {
        MMIErrorLog.WriteFile(detail);
      }
      catch (Exception ex)
      {
      }
      this.errorOverviewPanel.SetErrorInformation(motorErrorBlocks);
      return flag;
    }

    public void UpdateFirmwareInformation() => this.CheckForFirmwareUpdates();

    private void CheckForFirmwareUpdates()
    {
      string accuFirmwareVersion = MMIData.Instance.AccuFirmwareVersion;
      string motorFirmwareVersion = MMIData.Instance.MotorFirmwareVersion;
      string mmiFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      FirmwareListFile firmwareListFile1 = CommandBuilder.Instance.MCUVersion != MMIMCUVersion.CONNECT_MZ ? FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MMI) : FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.CONNECT_MMI);
      string newReadableVersion1 = firmwareListFile1.Length <= 0 ? "0" : (firmwareListFile1.Critical(0) || firmwareListFile1.ServerId(0) <= 0 ? "0" : firmwareListFile1.Version(0));
      FirmwareListFile firmwareListFile2 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.ACCU);
      string str1 = firmwareListFile2.Length <= 0 ? "0" : (firmwareListFile2.Critical(0) || firmwareListFile2.ServerId(0) <= 0 ? "0" : firmwareListFile2.Version(0));
      FirmwareListFile firmwareListFile3 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MOTOR);
      string newReadableVersion2 = firmwareListFile3.Length <= 0 ? "0" : (firmwareListFile3.Critical(0) || firmwareListFile3.ServerId(0) <= 0 ? "0" : firmwareListFile3.Version(0));
      bool flag1 = !HelperClass.IsUpToDate(accuFirmwareVersion, str1) && !HelperClass.IsUpToDate(MMIData.Instance.SavedAccuFirmwareVersion, str1) && HelperClass.IsUpToDate(str1, this.minValidBatteryVersion);
      bool flag2 = !HelperClass.IsUpToDate(motorFirmwareVersion, newReadableVersion2) && !HelperClass.IsUpToDate(MMIData.Instance.SavedMotorFirmwareVersion, newReadableVersion2);
      bool flag3 = !HelperClass.IsUpToDate(mmiFirmwareVersion, newReadableVersion1);
      bool flag4 = this.DFIUpdate() >= 0;
      bool flag5 = accuFirmwareVersion.Equals("1.0") && !HelperClass.IsUpToDate(MMIData.Instance.SavedAccuFirmwareVersion, "1.1");
      if (flag1 || flag2 || (flag3 || flag4) || flag5)
      {
        this.firmwareButton.Text = GlobalResource.DiagnosePanel_FirmwareButton_nok;
        this.changeButtonState(this.firmwareButton, new EventHandler(this.firmwareButton_Click), false);
      }
      else
      {
        this.firmwareButton.Text = GlobalResource.DiagnosePanel_FirmwareButton_ok;
        this.changeButtonState(this.firmwareButton, new EventHandler(this.firmwareButton_Click), true);
      }
      HelperClass.DoEvents();
      string str2 = MMIData.Instance.SavedMotorFirmwareVersion;
      string str3 = MMIData.Instance.SavedAccuFirmwareVersion;
      if (str2.Equals("0"))
        str2 = "0.0";
      if (str3.Equals("0"))
        str3 = "0.0";
      this.firmwareVersionToolTip.ToolTipTitle = GlobalResource.DiagnosePanel_FirmwareToolTipTitle;
      if (CommandBuilder.Instance.UseNewFirmwareFlag && MainWindow.Instance.Testmode)
      {
        string savedAccuDfiVersion = MMIData.Instance.SavedAccuDFIVersion;
        string accuDfiVersion = MMIData.Instance.AccuDFIVersion;
        this.firmwareVersionToolTip.SetToolTip((Control) this.bikePictureBox, string.Format(GlobalResource.DiagnosePanel_FirmwareToolTipText, (object) mmiFirmwareVersion, (object) str2, (object) (str3 + " - " + savedAccuDfiVersion), (object) motorFirmwareVersion, (object) (accuFirmwareVersion + " - " + accuDfiVersion)));
      }
      else
        this.firmwareVersionToolTip.SetToolTip((Control) this.bikePictureBox, string.Format(GlobalResource.DiagnosePanel_FirmwareToolTipText, (object) mmiFirmwareVersion, (object) str2, (object) str3, (object) motorFirmwareVersion, (object) accuFirmwareVersion));
      this.firmwareVersionToolTip.AutoPopDelay = 20000;
      this.firmwareVersionToolTip.ShowAlways = true;
    }

    private void CheckIfMarried()
    {
      if (MMIData.Instance.ImmobilizerActive)
      {
        this.immobilizerToolTip.SetToolTip((Control) this.lockCheckBox, GlobalResource.DiagnosePanel_LockBreakToolTip + "\n\n" + GlobalResource.Mmi_Lock_Information);
        this.lockCheckBox.Checked = true;
        this.lockCheckBox.Text = GlobalResource.DiagnosePanel_ImmobilizerActive;
      }
      else
      {
        this.immobilizerToolTip.SetToolTip((Control) this.lockCheckBox, GlobalResource.DiagnosePanel_LockOpenToolTip + "\n\n" + GlobalResource.Mmi_Lock_Information);
        this.lockCheckBox.Checked = false;
        this.lockCheckBox.Text = GlobalResource.DiagnosePanel_ImmobilizerInactive;
      }
    }

    private bool AccuIsHealthy()
    {
      // ISSUE: reference to a compiler-generated method
      MMIData.Instance.GetParameterValueObject(ParameterIds.ACCU_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.STATE_OF_HEALTH));
      // ISSUE: reference to a compiler-generated method
      int protocolValue = (int) MMIData.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.CLIENTSTATUS_ACCU)).ProtocolValue;
      if (protocolValue <= 7)
      {
        this.accuHealtyPictureBox.Image = (Image) Resources.button_ok;
        this.bikePictureBox.Accu = (Image) Resources.Bikestatus_complete_Accu;
        this.healthyToolTip.SetToolTip((Control) this.accuHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_Healthy);
        return true;
      }
      if (protocolValue > 7 && protocolValue < 128)
      {
        this.accuHealtyPictureBox.Image = (Image) Resources.button_orange_ok;
        this.bikePictureBox.Accu = (Image) Resources.Bikestatus_orange_error_Accu;
        this.healthyToolTip.SetToolTip((Control) this.accuHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_Healthy);
        return true;
      }
      if (protocolValue < 128)
        return false;
      this.accuHealtyPictureBox.Image = (Image) Resources.buttons_error;
      this.bikePictureBox.Accu = (Image) Resources.Bikestatus_error_Accu;
      this.healthyToolTip.SetToolTip((Control) this.accuHealtyPictureBox, GlobalResource.DiagnosePanel_ToolTip_NotHealthy);
      return false;
    }

    private bool EnablesUserControl(bool enable)
    {
      MainWindow.Instance.EnablesUserControl(enable);
      this.Enabled = enable;
      return enable;
    }

    private void UpdateInformation()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.bikePictureBox.MMI = (Image) Resources.Bikestatus_complete_sMMI;
      this.motorHasErrors = false;
      bool flag = !this.IsErrorFree();
      this.motorHasErrors = flag;
      if (!this.AccuIsHealthy() || flag)
        this.changeButtonState(this.diagnoseButton, new EventHandler(this.diagnoseButton_Click), false);
      else
        this.changeButtonState(this.diagnoseButton, new EventHandler(this.diagnoseButton_Click), true);
      if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ && this.immobilizerIsAvailable)
      {
        this.lockCheckBox.Visible = false;
        this.immobilizerCaptionLabel.Visible = false;
        this.immobilizerPanel.Visible = false;
        this.immobilizerIsAvailable = false;
        this.transferButton.Location = new Point(this.transferButton.Location.X, this.transferButton.Location.Y - this.immobilizerPanel.Height);
        HelperClass.DoEvents();
      }
      else
      {
        this.CheckIfMarried();
        HelperClass.DoEvents();
      }
      this.CheckForFirmwareUpdates();
      HelperClass.DoEvents();
      if (HelperClass.CheckIfOutdated())
      {
        this.changeButtonState(this.sDiagButton, new EventHandler(this.sDiagButton_Click), false);
        this.sDiagButton.Text = GlobalResource.DiagnosePanel_sDiagButton_nok;
      }
      else
      {
        this.changeButtonState(this.sDiagButton, new EventHandler(this.sDiagButton_Click), true);
        this.sDiagButton.Text = GlobalResource.DiagnosePanel_sDiagButton_ok;
      }
      this.UpdateServiceValues();
      if (MainWindow.Instance.Testmode)
      {
        this.accuTableLayoutPanel.Visible = true;
        this.accuBytesTextBox.Text = BitConverter.ToString(MMIData.Instance.AccuErrorBlocks).Replace('-', ' ');
        this.readTourDataButton.Visible = true;
      }
      else
      {
        this.accuTableLayoutPanel.Visible = false;
        this.readTourDataButton.Visible = false;
      }
      this.transferButton.Enabled = false;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void createShortReportButton_Click(object sender, EventArgs e)
    {
      CustomerInfoDialog customerInfoDialog = new CustomerInfoDialog();
      customerInfoDialog.Customer = this.custName;
      customerInfoDialog.Street = this.custStreet;
      customerInfoDialog.City = this.custCity;
      customerInfoDialog.Comment = this.custComment;
      if (customerInfoDialog.ShowDialog() == DialogResult.Abort)
        return;
      this.custCity = customerInfoDialog.City;
      this.custName = customerInfoDialog.Customer;
      this.custStreet = customerInfoDialog.Street;
      this.custComment = customerInfoDialog.Comment;
      string addressText = string.Empty + customerInfoDialog.Customer + "\n" + customerInfoDialog.Street + "\n" + customerInfoDialog.City;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ShortErrorReport shortErrorReport = new ShortErrorReport(GlobalResource.ShortErrorReport_ServiceReport, "short report of service information", Directories.Instance.ReportLogoImageFileName, false, addressText);
      this.createShortReportContent(ref shortErrorReport);
      if (customerInfoDialog.Comment != string.Empty)
        shortErrorReport.AddServiceOrderChecklist("", "", GlobalResource.ShortErrorReport_Comment, customerInfoDialog.Comment);
      PreViewDialog preViewDialog = new PreViewDialog(shortErrorReport.Document);
      MainWindow.Instance.Cursor = Cursors.Default;
      int num = (int) preViewDialog.ShowDialog();
    }

    private void deleteErrorLogButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.DiagnosePanel_DeleteErrorLog_Question, GlobalResource.DiagnosePanel_DeleteErrorLog_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      bool flag = new MMITransceiver(this.MMICom).DeleteMotorErrorLog();
      disconnectWarning.Dispose();
      FinishSound.Instance.Play();
      MainWindow.Instance.Cursor = Cursors.Default;
      this.errorOverviewPanel.EmptyAllInformation();
      MMIData.Instance.MotorErrorBlocks = new byte[0][];
      this.UpdateInformation();
      if (flag)
      {
        int num1 = (int) MessageBox.Show(GlobalResource.DiagnosePanel_DeleteErrorLog_SuccessMessage, GlobalResource.DiagnosePanel_DeleteErrorLog_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int num2 = (int) MessageBox.Show(GlobalResource.RingBuffer_Delete_Error_Message, GlobalResource.DiagnosePanel_DeleteErrorLog_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void serviceOrderButton_Click(object sender, EventArgs e)
    {
      ServiceOrderQuestionsDialog orderQuestionsDialog = new ServiceOrderQuestionsDialog();
      if (orderQuestionsDialog.ShowDialog() == DialogResult.Abort)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ShortErrorReport shortErrorReport = new ShortErrorReport(GlobalResource.ShortErrorReport_ServiceOrder, "short report of service information for service order", Directories.Instance.ReportLogoImageFileName, true, orderQuestionsDialog.ContactPerson());
      this.createShortReportContent(ref shortErrorReport);
      shortErrorReport.AddServiceOrderChecklist(GlobalResource.ShortErrorReport_Checklist, orderQuestionsDialog.Checkboxes(), GlobalResource.ShortErrorReport_Comment, orderQuestionsDialog.Comment());
      PreViewDialog preViewDialog = new PreViewDialog(shortErrorReport.Document);
      MainWindow.Instance.Cursor = Cursors.Default;
      int num = (int) preViewDialog.ShowDialog();
    }

    private void createShortReportContent(ref ShortErrorReport shortErrorReport)
    {
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      shortErrorReport.AddComponentInformation(GlobalResource.ShortErrorReport_ComponentInformation, GlobalResource.ShortErrorReport_Modelyear, string.Concat((object) Parameters.Instance.GetParameterValueObject(ParameterIds.MMI_PRODUCTION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.PRODUKTIONSDATUM_JAHR)).ReadableValue), GlobalResource.ShortErrorReport_Actual_Kilometers, string.Concat((object) Parameters.Instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.GESAMTKILOMETER)).ReadableValue), GlobalResource.ShortErrorReport_Neo_Version, Assembly.GetExecutingAssembly().GetName().Version.ToString());
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      FirmwareListFile firmwareListFile1 = CommandBuilder.Instance.MCUVersion != MMIMCUVersion.CONNECT_MZ ? FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MMI) : FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.CONNECT_MMI);
      string newReadableVersion1 = firmwareListFile1.Length <= 0 ? "0" : firmwareListFile1.Version(0);
      FirmwareListFile firmwareListFile2 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.ACCU);
      string newReadableVersion2 = firmwareListFile2.Length <= 0 ? "0" : firmwareListFile2.Version(0);
      FirmwareListFile firmwareListFile3 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MOTOR);
      string newReadableVersion3 = firmwareListFile3.Length <= 0 ? "0" : firmwareListFile3.Version(0);
      string str1 = GlobalResource.yes;
      string str2 = GlobalResource.yes;
      string str3 = GlobalResource.yes;
      if (HelperClass.IsUpToDate(Parameters.Instance.MotorFirmwareVersion, newReadableVersion3))
        str1 = GlobalResource.no;
      if (HelperClass.IsUpToDate(Parameters.Instance.AccuFirmwareVersion, newReadableVersion2) && this.DFIUpdate() == -1)
        str2 = GlobalResource.no;
      if (HelperClass.IsUpToDate(Parameters.Instance.MMIFirmwareVersion, newReadableVersion1))
        str3 = GlobalResource.no;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      shortErrorReport.AddComponentDetailInformation(GlobalResource.ShortErrorReport_Motor, GlobalResource.ShortErrorReport_Battery, GlobalResource.ShortErrorReport_MMI, new string[4]
      {
        GlobalResource.ShortErrorReport_Hardware_Version,
        GlobalResource.ShortErrorReport_Software_Version,
        GlobalResource.ShortErrorReport_Serial_Number,
        GlobalResource.ShortErrorReport_Update_Recommended
      }, new string[4]
      {
        string.Concat((object) Parameters.Instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.HARDWAREVERSION_ANTRIEB)).ReadableValue),
        Parameters.Instance.MotorFirmwareVersion,
        Parameters.Instance.MotorSerialNumber,
        str1
      }, new string[4]
      {
        string.Concat((object) Parameters.Instance.GetParameterValueObject(ParameterIds.ACCU_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.HARDWAREVERSION_ACCU)).ReadableValue),
        Parameters.Instance.AccuFirmwareVersion,
        Parameters.Instance.AccuSerialNumber,
        str2
      }, new string[4]
      {
        string.Concat((object) Parameters.Instance.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.HARDWAREVERSION_MMI)).ReadableValue),
        Parameters.Instance.MMIFirmwareVersion,
        Parameters.Instance.MMISerialNumber,
        str3
      });
      string[] errorMessages = this.errorOverviewPanel.GetErrorMessages();
      List<string> stringList1 = new List<string>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      string str4 = new ErrorNumberMotor().ErrorNumberMessage((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      for (int index = 0; index < errorMessages.Length; ++index)
      {
        if (ActivationInformation.VersionLevelProperty >= 4 ? !errorMessages[index].Equals("-") && !errorMessages[index].Equals(str4) : !errorMessages[index].Equals("-") && !errorMessages[index].Equals(str4) && errorMessages[index].IndexOf("#40") == -1)
          stringList1.Add(errorMessages[index]);
      }
      string[] array = stringList1.ToArray();
      bool accuHasError = false;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      if ((int) Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.CLIENTSTATUS_ACCU)).ProtocolValue >= 128)
      {
        accuHasError = true;
        string[] strArray = new StateDetailsDialog((byte) 128, new ComponentResourceManager(typeof (AccuClientStates)), (Form) MainWindow.Instance).ValueAsText().Split('\n');
        string str5 = strArray[strArray.Length - 2];
        if (array.Length >= 5)
        {
          array[array.Length - 1] = str5;
        }
        else
        {
          stringList1.Add(str5);
          array = stringList1.ToArray();
        }
      }
      if (array.Length > 0)
      {
        shortErrorReport.AddErrorDetailInformation(GlobalResource.ShortErrorReport_DiagnoseReportCaption, GlobalResource.ShortErrorReport_RecommendedProcedureCaption, GlobalResource.ShortErrorReport_DiagnoseTableCaption, array, accuHasError);
      }
      else
      {
        string[] errorDetails = new string[1]
        {
          GlobalResource.Report_NoErrorMessage_Text
        };
        shortErrorReport.AddErrorDetailInformation(GlobalResource.ShortErrorReport_DiagnoseReportCaption, GlobalResource.ShortErrorReport_RecommendedProcedureCaption, GlobalResource.ShortErrorReport_DiagnoseTableCaption, errorDetails, accuHasError);
      }
      ParameterPanel parameterPanel = new ParameterPanel(true);
      parameterPanel.updateUIWithDictionaryValues();
      string[] labels = parameterPanel.GetElementTextLabels();
      string[] values = parameterPanel.GetElementNumbericUpDownValues(3);
      string[] units = parameterPanel.GetElementUnitLabels();
      if (ActivationInformation.VersionLevelProperty < 4)
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string label1 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.NACHLAUFWEG)).Label;
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string label2 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.STATE_OF_HEALTH)).Label;
        List<string> stringList2 = new List<string>();
        List<string> stringList3 = new List<string>();
        List<string> stringList4 = new List<string>();
        for (int index = 0; index < labels.Length; ++index)
        {
          string str5 = labels[index].Substring(labels[index].IndexOf(':') + 2);
          if (str5 != label1 && str5 != label2)
          {
            stringList2.Add(labels[index]);
            stringList3.Add(values[index]);
            stringList4.Add(units[index]);
          }
          else if (str5 == label2)
          {
            stringList2.Add(labels[index]);
            stringList4.Add(string.Empty);
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            if (Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.STATE_OF_HEALTH)).ReadableValue >= 60.0)
              stringList3.Add(GlobalResource.ShortReport_SOH_OK);
            else
              stringList3.Add(GlobalResource.ShortReport_SOH_NOK);
          }
        }
        labels = stringList2.ToArray();
        values = stringList3.ToArray();
        units = stringList4.ToArray();
      }
      shortErrorReport.AddSettingInformation(GlobalResource.ShortErrorReport_SettingCaption, labels, values, units);
    }

    private void firmwareButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.UpdatePanel_Update_Components_MessageBox_Message, GlobalResource.UpdatePanel_Update_Components_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning dw = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      dw.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      bool flag = this.UpdateAllComponents(dw.ProgressBar(), dw);
      dw.Dispose();
      MainWindow.Instance.Cursor = Cursors.Default;
      HelperClass.DoEvents();
      FinishSound.Instance.Play();
      MainWindow.Instance.ReReadFirmwareLists();
      if (flag && this.firmwareUpdateCounter > 0)
      {
        if (this.veryOldFirmwareHasBeeenUpdated)
        {
          int num1 = (int) MessageBox.Show(GlobalResource.AccuFirmwareTransmissionSucceeded_Message_911, GlobalResource.AccuFirmwareTransmissionSucceeded_Caption_911, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
          int num2 = (int) MessageBox.Show(GlobalResource.FirmwareTransmissionSucceeded_Message, GlobalResource.FirmwareTransmissionSucceeded_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
      this.MotorSerialsDoNotMatchMessage();
      if (MMIData.Instance.AccuErrorBlocks.Length == 0)
        this.DFISafetyMessage();
      MainWindow.Instance.ShowMaxSpeedWaringMessage();
    }

    private void sDiagButton_Click(object sender, EventArgs e) => HelperClass.CheckIfUpdateIsAvailable();

    private void diagnoseButton_Click(object sender, EventArgs e)
    {
      string[] strArray = this.ErrorMessage();
      string text = string.Empty;
      foreach (string str in strArray)
        text = text + str + "\n";
      int num = (int) MessageBox.Show(text, GlobalResource.ShortErrorReport_DiagnoseReportCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void transferButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      disconnectWarning.ProgressBar().MaxValue = 100;
      this.UpdateProgessBarValue(disconnectWarning.ProgressBar(), 0);
      this.transceiver.ReceiveSettingsFromMMI(ParameterIds.MOTOR_VERSION_INFORMATION);
      this.UpdateProgessBarValue(disconnectWarning.ProgressBar(), 17);
      // ISSUE: reference to a compiler-generated method
      for (int parameterPosition = Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SERIENNUMMER_ANTRIEB_01); parameterPosition < 11; ++parameterPosition)
      {
        // ISSUE: reference to a compiler-generated method
        ParameterValue parameterValueObject = Parameters.Instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, parameterPosition);
        parameterValueObject.ReadableValue = 0.0;
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, parameterPosition, parameterValueObject);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        MMIData.Instance.SetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERIENNUMMER_ANTRIEB_01) + parameterPosition, Parameters.Instance.Dictionary[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERIENNUMMER_ANTRIEB_01) + parameterPosition]);
      }
      this.transceiver.TransferSettingToMMI(ParameterIds.MOTOR_VERSION_INFORMATION);
      this.UpdateProgessBarValue(disconnectWarning.ProgressBar(), 33);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      byte parameterValue = (byte) Parameters.Instance.GetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.MMI_DEFAULT_SETTINGS));
      this.UpdateProgessBarValue(disconnectWarning.ProgressBar(), 50);
      if (this.lockCheckBox.Checked)
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.MMI_DEFAULT_SETTINGS), (double) HelperClass.SetBitInByte(parameterValue, 4));
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.MMI_DEFAULT_SETTINGS), (double) HelperClass.ClearBitInByte(parameterValue, 4));
      }
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      MMIData.Instance.SetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.MMI_DEFAULT_SETTINGS), Parameters.Instance.Dictionary[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.MMI_DEFAULT_SETTINGS)]);
      this.transceiver.TransferSettingToMMI(ParameterIds.MMI_DEFAULT_SETTINGS);
      this.UpdateProgessBarValue(disconnectWarning.ProgressBar(), 84);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_STRECKE), (double) this.nextServiceDistance.Value);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_JAHR), (double) this.serviceInterval.ValueYear);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_MONAT), (double) this.serviceInterval.ValueMonth);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_TAG), (double) this.serviceInterval.ValueDay);
      this.transceiver.TransferSettingToMMI(ParameterIds.SERVICE_INTERVAL);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      MMIData.Instance.SetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_STRECKE), Parameters.Instance.Dictionary[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_STRECKE)]);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      MMIData.Instance.SetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_JAHR), Parameters.Instance.Dictionary[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_JAHR)]);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      MMIData.Instance.SetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_MONAT), Parameters.Instance.Dictionary[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_MONAT)]);
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      MMIData.Instance.SetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_TAG), Parameters.Instance.Dictionary[Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_TAG)]);
      this.UpdateProgessBarValue(disconnectWarning.ProgressBar(), disconnectWarning.ProgressBar().MaxValue);
      Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
      thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread.Start();
      while (thread.IsAlive)
        Thread.Sleep(50);
      disconnectWarning.Dispose();
      FinishSound.Instance.Play();
      MainWindow.Instance.Cursor = Cursors.Default;
      this.transferEnable = false;
      int num = (int) MessageBox.Show(GlobalResource.DiagnosePanel_MessageBox_Redock_Needed_Message, GlobalResource.DiagnosePanel_MessageBox_Redock_Needed_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private bool UpdateAllComponents(ZerroProgressBar bar, DisconnectWarning dw)
    {
      FirmwareListFile firmwareListFile1 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MOTOR);
      FirmwareListFile firmwareListFile2 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.ACCU);
      FirmwareListFile firmwareListFile3 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.DFI);
      this.firmwareUpdateCounter = 0;
      string mmiFirmwarePath = Directories.Instance.MMIFirmwarePath;
      FirmwareListFile firmwareListFile4;
      string str;
      if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
      {
        firmwareListFile4 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.CONNECT_MMI);
        str = Directories.Instance.ConnectMMIFirmwarePath;
      }
      else
      {
        firmwareListFile4 = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MMI);
        str = Directories.Instance.MMIFirmwarePath;
      }
      if (firmwareListFile4.Length > 0 && !HelperClass.IsUpToDate(MMIData.Instance.MMIFirmwareVersion, firmwareListFile4.Version(0)) && (!firmwareListFile4.Critical(0) && this.transceiver.ReadLinesFromFirmwareFile(str + firmwareListFile4.FirmwareFilename(0))))
      {
        if (!HelperClass.IsUpToDate(MMIData.Instance.MMIFirmwareVersion, "2.1.1.0"))
          this.veryOldFirmwareHasBeeenUpdated = true;
        if (!this.RemoveComponentFirmwareFromMMI(bar))
        {
          this.UpdateProgessBarValue(bar, 0);
          return false;
        }
        bar.AdditionalText = GlobalResource.UpdateWorker_Element_MMI;
        this.UpdateProgessBarValue(bar, 0);
        MainWindow.Instance.IsMMIUpdateInProgress = true;
        int actualPosition = 0;
        int num1 = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
        int num2 = Parameters.Instance.ParameterElements - 1;
        int maximum = num2 * 2 + num1;
        if (!this.transceiver.ReceiveAllSettingsFromMMI(ref actualPosition, maximum, bar))
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_MMI, ref actualPosition, maximum, bar))
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.MMI))
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        // ISSUE: reference to a compiler-generated method
        byte[] setOfParameters1 = Parameters.Instance.GetSetOfParameters(ParameterIds.MMI_PRODUCTION_INFORMATION);
        // ISSUE: reference to a compiler-generated method
        byte[] setOfParameters2 = Parameters.Instance.GetSetOfParameters(ParameterIds.ACCU_VERSION_INFORMATION);
        // ISSUE: reference to a compiler-generated method
        byte[] setOfParameters3 = Parameters.Instance.GetSetOfParameters(ParameterIds.MOTOR_VERSION_INFORMATION);
        MainWindow.Instance.ShowMaxSpeedWarning = false;
        if (!this.transceiver.SetUpdateFlag(UpdateFlags.DOUPDATE))
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        dw.Visible = false;
        if (!this.ShowRebootDialog())
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        dw.Visible = true;
        actualPosition = maximum - num2;
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetSetOfParameters(ParameterIds.MMI_PRODUCTION_INFORMATION, setOfParameters1);
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetSetOfParameters(ParameterIds.ACCU_VERSION_INFORMATION, setOfParameters2);
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetSetOfParameters(ParameterIds.MOTOR_VERSION_INFORMATION, setOfParameters3);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.DATUM_JAHR), (double) DateTime.Now.Year);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.DATUM_MONAT), (double) DateTime.Now.Month);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.DATUM_TAG), (double) DateTime.Now.Day);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.UHRZEIT_STUNDE), (double) DateTime.Now.Hour);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.UHRZEIT_MINUTE), (double) DateTime.Now.Minute);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.FIRMWARE_AUFSPIELDATUM_JAHR), (double) DateTime.Now.Year);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.FIRMWARE_AUFSPIELDATUM_MONAT), (double) DateTime.Now.Month);
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SetParameterValue(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.FIRMWARE_AUFSPIELDATUM_TAG), (double) DateTime.Now.Day);
        // ISSUE: reference to a compiler-generated method
        Parameters.Instance.SortLightAndBrigthnessLevels();
        if (!this.transceiver.TransferAllSettingsToMMI(ref actualPosition, maximum, bar))
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
        thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
        thread.Start();
        while (thread.IsAlive)
          Thread.Sleep(50);
        if (!this.transceiver.WaitingForMMISucceeded)
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        if (!this.transceiver.SetUpdateFlag(UpdateFlags.DOUPDATE))
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        dw.Visible = false;
        if (!this.ShowRebootDialog())
        {
          this.UpdateProgessBarValue(bar, 0);
          MainWindow.Instance.IsMMIUpdateInProgress = false;
          return false;
        }
        dw.Visible = true;
        try
        {
          if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
            FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(Parameters.Instance.MMISerialNumber, firmwareListFile4.Version(0), firmwareListFile4.ServerId(0), FirmwareListFile.FirmwareType.CONNECT_MMI));
          else
            FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(Parameters.Instance.MMISerialNumber, firmwareListFile4.Version(0), firmwareListFile4.ServerId(0), FirmwareListFile.FirmwareType.MMI));
        }
        catch (Exception ex)
        {
        }
        this.UpdateProgessBarValue(bar, 100);
        ++this.firmwareUpdateCounter;
        MainWindow.Instance.IsMMIUpdateInProgress = false;
      }
      if (firmwareListFile1.Length > 0 && !HelperClass.IsUpToDate(MMIData.Instance.MotorFirmwareVersion, firmwareListFile1.Version(0)) && (!firmwareListFile1.Critical(0) && !HelperClass.IsUpToDate(MMIData.Instance.SavedMotorFirmwareVersion, firmwareListFile1.Version(0))))
      {
        if (!this.hasMMITheRightVersion())
          return false;
        if (this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.MotorBootloaderFileName))
        {
          bar.AdditionalText = GlobalResource.UpdatePanel_ProgressBar_MotorBootloader;
          this.UpdateProgessBarValue(bar, 0);
          int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
          int actualPosition = 0;
          if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.BOOTLOADER_MOTOR, ref actualPosition, maximum, bar))
          {
            this.UpdateProgessBarValue(bar, 0);
            return false;
          }
          this.UpdateProgessBarValue(bar, 100);
        }
        if (this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.MotorFirmwarePath + firmwareListFile1.FirmwareFilename(0)))
        {
          bar.AdditionalText = GlobalResource.UpdateWorker_Element_Motor;
          this.UpdateProgessBarValue(bar, 0);
          int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
          int actualPosition = 0;
          if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_MOTOR, ref actualPosition, maximum, bar, true, firmwareListFile1.Version(0)))
          {
            this.UpdateProgessBarValue(bar, 0);
            return false;
          }
          if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.MOTOR))
          {
            this.UpdateProgessBarValue(bar, 0);
            return false;
          }
          try
          {
            FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(Parameters.Instance.MMISerialNumber, firmwareListFile1.Version(0), firmwareListFile1.ServerId(0), FirmwareListFile.FirmwareType.MOTOR));
          }
          catch (Exception ex)
          {
          }
          MMIData.Instance.SavedMotorFirmwareVersion = firmwareListFile1.Version(0);
          this.UpdateProgessBarValue(bar, 100);
          ++this.firmwareUpdateCounter;
        }
      }
      bool flag = MMIData.Instance.AccuFirmwareVersion.Equals("1.0") && firmwareListFile2.ServerId(0) > 0;
      if (MMIData.Instance.AccuErrorBlocks.Length > 0 || flag)
      {
        if (firmwareListFile2.Length > 0)
        {
          this.accuFirmwareHasBeenUpdated = false;
          if (flag)
          {
            int position1 = -1;
            for (int position2 = 0; position2 < firmwareListFile2.Length; ++position2)
            {
              if (firmwareListFile2.Version(position2) == "1.1" && firmwareListFile2.ServerId(position2) > 0)
                position1 = position2;
            }
            if (position1 < 0)
            {
              int num = (int) MessageBox.Show(GlobalResource.DiagnosePanel_AccuVeryOld_913_Message, GlobalResource.DiagnosePanel_AccuVeryOld_913_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
              if (!this.hasMMITheRightVersion())
                return false;
              if (this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.BatteryFirmwarePath + firmwareListFile2.FirmwareFilename(position1)))
              {
                bar.AdditionalText = GlobalResource.UpdateWorker_Element_Accu;
                this.UpdateProgessBarValue(bar, 0);
                int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
                int actualPosition = 0;
                if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_ACCU, ref actualPosition, maximum, bar, true, firmwareListFile2.Version(position1)))
                {
                  this.UpdateProgessBarValue(bar, 0);
                  return false;
                }
                if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.ACCU))
                {
                  this.UpdateProgessBarValue(bar, 0);
                  return false;
                }
                this.accuFirmwareHasBeenUpdated = true;
                this.veryOldFirmwareHasBeeenUpdated = true;
                try
                {
                  FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(Parameters.Instance.MMISerialNumber, firmwareListFile2.Version(position1), firmwareListFile2.ServerId(position1), FirmwareListFile.FirmwareType.ACCU));
                }
                catch (Exception ex)
                {
                }
                MMIData.Instance.SavedAccuFirmwareVersion = firmwareListFile2.Version(position1);
                this.UpdateProgessBarValue(bar, 100);
                ++this.firmwareUpdateCounter;
              }
            }
          }
          else if (!HelperClass.IsUpToDate(MMIData.Instance.AccuFirmwareVersion, firmwareListFile2.Version(0)) && !firmwareListFile2.Critical(0) && (!HelperClass.IsUpToDate(MMIData.Instance.SavedAccuFirmwareVersion, firmwareListFile2.Version(0)) && HelperClass.IsUpToDate(firmwareListFile2.Version(0), this.minValidBatteryVersion)))
          {
            if (!this.hasMMITheRightVersion())
              return false;
            if (this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.BatteryFirmwarePath + firmwareListFile2.FirmwareFilename(0)))
            {
              bar.AdditionalText = GlobalResource.UpdateWorker_Element_Accu;
              this.UpdateProgessBarValue(bar, 0);
              int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
              int actualPosition = 0;
              if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_ACCU, ref actualPosition, maximum, bar, true, firmwareListFile2.Version(0)))
              {
                this.UpdateProgessBarValue(bar, 0);
                return false;
              }
              if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.ACCU))
              {
                this.UpdateProgessBarValue(bar, 0);
                return false;
              }
              this.accuFirmwareHasBeenUpdated = true;
              try
              {
                FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(Parameters.Instance.MMISerialNumber, firmwareListFile2.Version(0), firmwareListFile2.ServerId(0), FirmwareListFile.FirmwareType.ACCU));
              }
              catch (Exception ex)
              {
              }
              MMIData.Instance.SavedAccuFirmwareVersion = firmwareListFile2.Version(0);
              this.UpdateProgessBarValue(bar, 100);
              ++this.firmwareUpdateCounter;
            }
          }
        }
        int position = this.DFIUpdate();
        if (position >= 0 && (HelperClass.IsUpToDate(MMIData.Instance.AccuFirmwareVersion, this.minValidBatteryVersion) || HelperClass.IsUpToDate(MMIData.Instance.SavedAccuFirmwareVersion, this.minValidBatteryVersion) || HelperClass.IsUpToDate(firmwareListFile2.Version(0), this.minValidBatteryVersion)))
        {
          if (!this.hasMMITheRightVersion())
            return false;
          if (this.transceiver.ReadBytesFromFile(Directories.Instance.DFIFirmwarePath + firmwareListFile3.FirmwareFilename(position)))
          {
            bar.AdditionalText = GlobalResource.UpdateWorker_Element_DFI;
            this.UpdateProgessBarValue(bar, 0);
            int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 1000.0) + 1;
            int actualPosition = 0;
            if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.ACCU_DFI_FILE, ref actualPosition, maximum, bar, true, firmwareListFile3.Version(position)))
            {
              this.UpdateProgessBarValue(bar, 0);
              return false;
            }
            if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.DFI))
            {
              this.UpdateProgessBarValue(bar, 0);
              return false;
            }
            try
            {
              FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(Parameters.Instance.MMISerialNumber, firmwareListFile3.Version(position), firmwareListFile3.ServerId(position), FirmwareListFile.FirmwareType.DFI));
            }
            catch (Exception ex)
            {
            }
            this.accuFirmwareHasBeenUpdated = false;
            MMIData.Instance.SavedAccuDFIVersion = firmwareListFile3.Version(position);
            this.UpdateProgessBarValue(bar, 100);
            ++this.firmwareUpdateCounter;
          }
        }
      }
      bar.AdditionalText = GlobalResource.UpdatePanel_ProgressBar_ComponentsUpToDate;
      this.UpdateProgessBarValue(bar, 100);
      bar.AdditionalText = "";
      return true;
    }

    private bool RemoveComponentFirmwareFromMMI(ZerroProgressBar bar)
    {
      bar.AdditionalText = GlobalResource.UpdatePanel_Motor_Bootloader_Delete;
      this.UpdateProgessBarValue(bar, 0);
      if (!this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.BOOTLOADER_MOTOR))
        return false;
      bar.AdditionalText = GlobalResource.UpdatePanel_Motor_Firmware_Delete;
      this.UpdateProgessBarValue(bar, 25);
      if (!this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.FIRMWARE_MOTOR))
        return false;
      MMIData.Instance.SavedMotorFirmwareVersion = "0.0";
      bar.AdditionalText = GlobalResource.UpdatePanel_Accu_Firmware_Delete;
      this.UpdateProgessBarValue(bar, 50);
      if (!this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.FIRMWARE_ACCU))
        return false;
      MMIData.Instance.SavedAccuFirmwareVersion = "0.0";
      bar.AdditionalText = GlobalResource.UpdatePanel_Accu_Firmware_Delete;
      this.UpdateProgessBarValue(bar, 75);
      if (CommandBuilder.Instance.UseNewFirmwareFlag)
      {
        if (!this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.ACCU_DFI_FILE))
          return false;
        MMIData.Instance.SavedAccuDFIVersion = "0x00 0x00";
      }
      this.UpdateProgessBarValue(bar, 100);
      return true;
    }

    private int DFIUpdate()
    {
      int index1 = -1;
      FirmwareListFile firmwareListFile = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.DFI);
      if (CommandBuilder.Instance.UseNewFirmwareFlag)
      {
        string[] strArray1 = MMIData.Instance.AccuDFIVersion.Split(' ');
        FirmwareInformation[] firmwareInformations = firmwareListFile.FirmwareInformations;
        string convert = strArray1[0];
        string str1 = strArray1[1];
        string str2 = HelperClass.UpperCaseHexString(convert);
        if (this.accuFirmwareHasBeenUpdated)
          str1 = "0x00";
        for (int index2 = 0; index2 < firmwareInformations.Length; ++index2)
        {
          if (firmwareInformations[index2].ServerID == firmwareListFile.ServerId(0) && firmwareInformations[index2].ServerID > 0 && (HelperClass.UpperCaseHexString(firmwareInformations[index2].Version).StartsWith(str2, true, (CultureInfo) null) && !firmwareInformations[index2].Critical))
          {
            byte[] byteArray1 = HelperClass.StringToByteArray(str1.Substring(2));
            byte[] byteArray2 = HelperClass.StringToByteArray(firmwareInformations[index2].Version.Split(' ')[1].Substring(2));
            if ((int) byteArray2[0] > (int) byteArray1[0])
            {
              string[] strArray2 = MMIData.Instance.SavedAccuDFIVersion.Split(' ');
              byte[] byteArray3 = HelperClass.StringToByteArray(strArray2[1].Substring(2));
              if (HelperClass.UpperCaseHexString(strArray2[0]).Equals(str2))
              {
                if ((int) byteArray2[0] > (int) byteArray3[0])
                  index1 = index2;
              }
              else
                index1 = index2;
            }
          }
        }
        if (index1 >= 0)
        {
          if (HelperClass.StringToByteArray(firmwareInformations[index1].Version.Split(' ')[0].Substring(2))[0] <= (byte) 16)
            index1 = -1;
        }
      }
      return index1;
    }

    private void UpdateProgessBarValue(ZerroProgressBar progressbar, int value)
    {
      progressbar.Value = value;
      progressbar.Refresh();
    }

    private bool ShowRebootDialog()
    {
      this.rebootDialog = new WaitingForReboot((Form) MainWindow.Instance);
      int num = (int) this.rebootDialog.ShowDialog((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      bool flag = this.rebootDialog.DialogResult != DialogResult.Abort;
      this.rebootDialog.Dispose();
      this.rebootDialog = (WaitingForReboot) null;
      return flag;
    }

    private bool hasMMITheRightVersion()
    {
      if (HelperClass.IsUpToDate(MMIData.Instance.MMIFirmwareVersion, "1.3.0.4") || CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
        return true;
      int num = (int) MessageBox.Show(GlobalResource.UpdatePanel_WrongMmiFirmwareVersion_Message, GlobalResource.UpdatePanel_WrongMmiFirmwareVersion_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return false;
    }

    private void HelloLoopPassedHandler(object mainWindow, MainWindowEventArgs args)
    {
      if (this.rebootDialog == null)
        return;
      this.rebootDialog.MMIIsBack();
    }

    private string[] ErrorMessage()
    {
      string[] errorMessages = this.errorOverviewPanel.GetErrorMessages();
      List<string> stringList = new List<string>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      string str1 = new ErrorNumberMotor().ErrorNumberMessage((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      for (int index = 0; index < errorMessages.Length; ++index)
      {
        if (ActivationInformation.VersionLevelProperty >= 4 ? !errorMessages[index].Equals("-") && !errorMessages[index].Equals(str1) : !errorMessages[index].Equals("-") && !errorMessages[index].Equals(str1) && errorMessages[index].IndexOf("#40") == -1)
          stringList.Add(errorMessages[index]);
      }
      string[] array = stringList.ToArray();
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      if ((int) Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.CLIENTSTATUS_ACCU)).ProtocolValue >= 128)
      {
        string[] strArray = new StateDetailsDialog((byte) 128, new ComponentResourceManager(typeof (AccuClientStates)), (Form) MainWindow.Instance).ValueAsText().Split('\n');
        string str2 = strArray[strArray.Length - 2];
        if (array.Length >= 5)
        {
          array[array.Length - 1] = str2;
        }
        else
        {
          stringList.Add(str2);
          array = stringList.ToArray();
        }
      }
      return array;
    }

    private void readTourDataButton_Click(object sender, EventArgs e)
    {
      ConnectionEstablished ce = new ConnectionEstablished((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      this.Invoke((MethodInvoker) (() => ce.Show((IWin32Window) MainWindow.Instance)));
      int actualPosition = 1;
      byte[] tourDataBufferContent = new byte[0];
      int maximum = 426;
      if (this.transceiver.ReadTourDataRingBuffer(ref actualPosition, maximum, ce.ProgressBar(), ref tourDataBufferContent))
      {
        byte[] bytes1 = new byte[tourDataBufferContent.Length / 2];
        byte[] bytes2 = new byte[tourDataBufferContent.Length / 2];
        Buffer.BlockCopy((Array) tourDataBufferContent, 0, (Array) bytes1, 0, bytes1.Length);
        Buffer.BlockCopy((Array) tourDataBufferContent, bytes1.Length, (Array) bytes2, 0, bytes2.Length);
        File.WriteAllBytes(Directories.Instance.DataPath + "flash_total.hex", tourDataBufferContent);
        File.WriteAllBytes(Directories.Instance.DataPath + "flash_split1.hex", bytes1);
        File.WriteAllBytes(Directories.Instance.DataPath + "flash_split2.hex", bytes2);
      }
      ce.Dispose();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DiagnosePanel));
      this.diagnoseTabControl = new TabControl();
      this.diagnoseStatusTabPage = new TabPage();
      this.accuTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.accuByteLabel = new Label();
      this.accuBytesTextBox = new TextBox();
      this.immobilizerPanel = new Panel();
      this.immobilizerFlickerFreeTableLayout = new FlickerFreeTableLayoutPanel();
      this.pictureBox1 = new PictureBox();
      this.immobilizerCaptionLabel = new Label();
      this.immobilizerFlickerFreeTableLayout2 = new FlickerFreeTableLayoutPanel();
      this.lockCheckBox = new CheckBox();
      this.transferButton = new Button();
      this.servicePanel = new Panel();
      this.serviceFlickerFreeTableLayout = new FlickerFreeTableLayoutPanel();
      this.datePictureBox = new PictureBox();
      this.dateTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.serviceDateCaption = new Label();
      this.kilometerPanel = new Panel();
      this.kilometersFlickerFreeTableLayout = new FlickerFreeTableLayoutPanel();
      this.distancePictureBox = new PictureBox();
      this.distanceTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.hackFlickerFreeTableLayout = new FlickerFreeTableLayoutPanel();
      this.totalKilometersLabel = new Label();
      this.serviceKilometerCaption = new Label();
      this.serviceOrderButton = new Button();
      this.createShortReportButton = new Button();
      this.flickerFreeTableLayoutPanel4 = new FlickerFreeTableLayoutPanel();
      this.deleteErrorLogButton = new Button();
      this.label7 = new Label();
      this.label6 = new Label();
      this.label5 = new Label();
      this.motorHealtyPictureBox = new PictureBox();
      this.accuHealtyPictureBox = new PictureBox();
      this.mmiHealtyPictureBox = new PictureBox();
      this.flickerFreeTableLayoutPanel3 = new FlickerFreeTableLayoutPanel();
      this.sDiagPictureBox = new PictureBox();
      this.firmwarePictureBox = new PictureBox();
      this.diagnosePictureBox = new PictureBox();
      this.diagnoseButton = new Button();
      this.sDiagButton = new Button();
      this.firmwareButton = new Button();
      this.bikeFlippedPictureBox = new PictureBox();
      this.serviceCaption = new Label();
      this.bikePictureBox = new BikePictureBox();
      this.diagnoseTabPage = new TabPage();
      this.errorOverviewPanel = new ErrorOverviewPanel();
      this.immobilizerToolTip = new SmartToolTip(this.components);
      this.healthyToolTip = new SmartToolTip(this.components);
      this.upToDateToolTip = new SmartToolTip(this.components);
      this.readTourDataButton = new Button();
      this.diagnoseTabControl.SuspendLayout();
      this.diagnoseStatusTabPage.SuspendLayout();
      this.accuTableLayoutPanel.SuspendLayout();
      this.immobilizerPanel.SuspendLayout();
      this.immobilizerFlickerFreeTableLayout.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.immobilizerFlickerFreeTableLayout2.SuspendLayout();
      this.servicePanel.SuspendLayout();
      this.serviceFlickerFreeTableLayout.SuspendLayout();
      ((ISupportInitialize) this.datePictureBox).BeginInit();
      this.kilometerPanel.SuspendLayout();
      this.kilometersFlickerFreeTableLayout.SuspendLayout();
      ((ISupportInitialize) this.distancePictureBox).BeginInit();
      this.distanceTableLayoutPanel.SuspendLayout();
      this.hackFlickerFreeTableLayout.SuspendLayout();
      this.flickerFreeTableLayoutPanel4.SuspendLayout();
      ((ISupportInitialize) this.motorHealtyPictureBox).BeginInit();
      ((ISupportInitialize) this.accuHealtyPictureBox).BeginInit();
      ((ISupportInitialize) this.mmiHealtyPictureBox).BeginInit();
      this.flickerFreeTableLayoutPanel3.SuspendLayout();
      ((ISupportInitialize) this.sDiagPictureBox).BeginInit();
      ((ISupportInitialize) this.firmwarePictureBox).BeginInit();
      ((ISupportInitialize) this.diagnosePictureBox).BeginInit();
      ((ISupportInitialize) this.bikeFlippedPictureBox).BeginInit();
      ((ISupportInitialize) this.bikePictureBox).BeginInit();
      this.diagnoseTabPage.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.diagnoseTabControl, "diagnoseTabControl");
      this.diagnoseTabControl.Controls.Add((Control) this.diagnoseStatusTabPage);
      this.diagnoseTabControl.Controls.Add((Control) this.diagnoseTabPage);
      this.diagnoseTabControl.Name = "diagnoseTabControl";
      this.diagnoseTabControl.SelectedIndex = 0;
      this.diagnoseStatusTabPage.BackColor = Color.White;
      this.diagnoseStatusTabPage.Controls.Add((Control) this.readTourDataButton);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.accuTableLayoutPanel);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.immobilizerPanel);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.transferButton);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.servicePanel);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.kilometerPanel);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.serviceOrderButton);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.createShortReportButton);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.flickerFreeTableLayoutPanel4);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.flickerFreeTableLayoutPanel3);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.bikeFlippedPictureBox);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.serviceCaption);
      this.diagnoseStatusTabPage.Controls.Add((Control) this.bikePictureBox);
      componentResourceManager.ApplyResources((object) this.diagnoseStatusTabPage, "diagnoseStatusTabPage");
      this.diagnoseStatusTabPage.Name = "diagnoseStatusTabPage";
      componentResourceManager.ApplyResources((object) this.accuTableLayoutPanel, "accuTableLayoutPanel");
      this.accuTableLayoutPanel.Controls.Add((Control) this.accuByteLabel, 0, 0);
      this.accuTableLayoutPanel.Controls.Add((Control) this.accuBytesTextBox, 0, 1);
      this.accuTableLayoutPanel.Name = "accuTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.accuByteLabel, "accuByteLabel");
      this.accuByteLabel.Name = "accuByteLabel";
      componentResourceManager.ApplyResources((object) this.accuBytesTextBox, "accuBytesTextBox");
      this.accuBytesTextBox.Name = "accuBytesTextBox";
      this.accuBytesTextBox.TabStop = false;
      this.immobilizerPanel.BackColor = Color.Transparent;
      this.immobilizerPanel.BorderStyle = BorderStyle.FixedSingle;
      this.immobilizerPanel.Controls.Add((Control) this.immobilizerFlickerFreeTableLayout);
      componentResourceManager.ApplyResources((object) this.immobilizerPanel, "immobilizerPanel");
      this.immobilizerPanel.Name = "immobilizerPanel";
      componentResourceManager.ApplyResources((object) this.immobilizerFlickerFreeTableLayout, "immobilizerFlickerFreeTableLayout");
      this.immobilizerFlickerFreeTableLayout.BackColor = Color.Transparent;
      this.immobilizerFlickerFreeTableLayout.Controls.Add((Control) this.pictureBox1, 0, 0);
      this.immobilizerFlickerFreeTableLayout.Controls.Add((Control) this.immobilizerCaptionLabel, 1, 0);
      this.immobilizerFlickerFreeTableLayout.Controls.Add((Control) this.immobilizerFlickerFreeTableLayout2, 1, 1);
      this.immobilizerFlickerFreeTableLayout.Name = "immobilizerFlickerFreeTableLayout";
      componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.immobilizerCaptionLabel, "immobilizerCaptionLabel");
      this.immobilizerCaptionLabel.BackColor = Color.Transparent;
      this.immobilizerCaptionLabel.Name = "immobilizerCaptionLabel";
      componentResourceManager.ApplyResources((object) this.immobilizerFlickerFreeTableLayout2, "immobilizerFlickerFreeTableLayout2");
      this.immobilizerFlickerFreeTableLayout2.Controls.Add((Control) this.lockCheckBox, 1, 0);
      this.immobilizerFlickerFreeTableLayout2.Name = "immobilizerFlickerFreeTableLayout2";
      componentResourceManager.ApplyResources((object) this.lockCheckBox, "lockCheckBox");
      this.lockCheckBox.Name = "lockCheckBox";
      this.lockCheckBox.UseVisualStyleBackColor = true;
      this.lockCheckBox.CheckedChanged += new EventHandler(this.MMILockCheckbox_stateChanged);
      componentResourceManager.ApplyResources((object) this.transferButton, "transferButton");
      this.transferButton.Name = "transferButton";
      this.transferButton.UseVisualStyleBackColor = true;
      this.transferButton.Click += new EventHandler(this.transferButton_Click);
      this.servicePanel.BackColor = Color.Transparent;
      this.servicePanel.BorderStyle = BorderStyle.FixedSingle;
      this.servicePanel.Controls.Add((Control) this.serviceFlickerFreeTableLayout);
      componentResourceManager.ApplyResources((object) this.servicePanel, "servicePanel");
      this.servicePanel.Name = "servicePanel";
      componentResourceManager.ApplyResources((object) this.serviceFlickerFreeTableLayout, "serviceFlickerFreeTableLayout");
      this.serviceFlickerFreeTableLayout.BackColor = Color.Transparent;
      this.serviceFlickerFreeTableLayout.Controls.Add((Control) this.datePictureBox, 0, 0);
      this.serviceFlickerFreeTableLayout.Controls.Add((Control) this.dateTableLayoutPanel, 1, 1);
      this.serviceFlickerFreeTableLayout.Controls.Add((Control) this.serviceDateCaption, 1, 0);
      this.serviceFlickerFreeTableLayout.Name = "serviceFlickerFreeTableLayout";
      componentResourceManager.ApplyResources((object) this.datePictureBox, "datePictureBox");
      this.datePictureBox.BackColor = Color.Transparent;
      this.datePictureBox.Name = "datePictureBox";
      this.datePictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.dateTableLayoutPanel, "dateTableLayoutPanel");
      this.dateTableLayoutPanel.Name = "dateTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.serviceDateCaption, "serviceDateCaption");
      this.serviceDateCaption.BackColor = Color.Transparent;
      this.serviceDateCaption.Name = "serviceDateCaption";
      this.kilometerPanel.BackColor = Color.Transparent;
      this.kilometerPanel.BorderStyle = BorderStyle.FixedSingle;
      this.kilometerPanel.Controls.Add((Control) this.kilometersFlickerFreeTableLayout);
      componentResourceManager.ApplyResources((object) this.kilometerPanel, "kilometerPanel");
      this.kilometerPanel.Name = "kilometerPanel";
      componentResourceManager.ApplyResources((object) this.kilometersFlickerFreeTableLayout, "kilometersFlickerFreeTableLayout");
      this.kilometersFlickerFreeTableLayout.BackColor = Color.Transparent;
      this.kilometersFlickerFreeTableLayout.Controls.Add((Control) this.distancePictureBox, 0, 0);
      this.kilometersFlickerFreeTableLayout.Controls.Add((Control) this.distanceTableLayoutPanel, 1, 1);
      this.kilometersFlickerFreeTableLayout.Controls.Add((Control) this.serviceKilometerCaption, 1, 0);
      this.kilometersFlickerFreeTableLayout.Name = "kilometersFlickerFreeTableLayout";
      componentResourceManager.ApplyResources((object) this.distancePictureBox, "distancePictureBox");
      this.distancePictureBox.BackColor = Color.Transparent;
      this.distancePictureBox.Name = "distancePictureBox";
      this.distancePictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.distanceTableLayoutPanel, "distanceTableLayoutPanel");
      this.distanceTableLayoutPanel.BackColor = Color.Transparent;
      this.distanceTableLayoutPanel.Controls.Add((Control) this.hackFlickerFreeTableLayout, 0, 0);
      this.distanceTableLayoutPanel.Name = "distanceTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.hackFlickerFreeTableLayout, "hackFlickerFreeTableLayout");
      this.hackFlickerFreeTableLayout.Controls.Add((Control) this.totalKilometersLabel, 0, 0);
      this.hackFlickerFreeTableLayout.Name = "hackFlickerFreeTableLayout";
      componentResourceManager.ApplyResources((object) this.totalKilometersLabel, "totalKilometersLabel");
      this.totalKilometersLabel.BackColor = Color.Transparent;
      this.totalKilometersLabel.Name = "totalKilometersLabel";
      componentResourceManager.ApplyResources((object) this.serviceKilometerCaption, "serviceKilometerCaption");
      this.serviceKilometerCaption.BackColor = Color.Transparent;
      this.serviceKilometerCaption.Name = "serviceKilometerCaption";
      componentResourceManager.ApplyResources((object) this.serviceOrderButton, "serviceOrderButton");
      this.serviceOrderButton.Name = "serviceOrderButton";
      this.serviceOrderButton.UseVisualStyleBackColor = true;
      this.serviceOrderButton.Click += new EventHandler(this.serviceOrderButton_Click);
      componentResourceManager.ApplyResources((object) this.createShortReportButton, "createShortReportButton");
      this.createShortReportButton.Name = "createShortReportButton";
      this.createShortReportButton.UseVisualStyleBackColor = true;
      this.createShortReportButton.Click += new EventHandler(this.createShortReportButton_Click);
      componentResourceManager.ApplyResources((object) this.flickerFreeTableLayoutPanel4, "flickerFreeTableLayoutPanel4");
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.deleteErrorLogButton, 0, 1);
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.label7, 5, 0);
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.label6, 3, 0);
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.label5, 1, 0);
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.motorHealtyPictureBox, 0, 0);
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.accuHealtyPictureBox, 2, 0);
      this.flickerFreeTableLayoutPanel4.Controls.Add((Control) this.mmiHealtyPictureBox, 4, 0);
      this.flickerFreeTableLayoutPanel4.Name = "flickerFreeTableLayoutPanel4";
      componentResourceManager.ApplyResources((object) this.deleteErrorLogButton, "deleteErrorLogButton");
      this.flickerFreeTableLayoutPanel4.SetColumnSpan((Control) this.deleteErrorLogButton, 3);
      this.deleteErrorLogButton.Name = "deleteErrorLogButton";
      this.deleteErrorLogButton.UseVisualStyleBackColor = true;
      this.deleteErrorLogButton.Click += new EventHandler(this.deleteErrorLogButton_Click);
      componentResourceManager.ApplyResources((object) this.label7, "label7");
      this.label7.BackColor = Color.Transparent;
      this.label7.Name = "label7";
      componentResourceManager.ApplyResources((object) this.label6, "label6");
      this.label6.BackColor = Color.Transparent;
      this.label6.Name = "label6";
      componentResourceManager.ApplyResources((object) this.label5, "label5");
      this.label5.BackColor = Color.Transparent;
      this.label5.Name = "label5";
      componentResourceManager.ApplyResources((object) this.motorHealtyPictureBox, "motorHealtyPictureBox");
      this.motorHealtyPictureBox.BackColor = Color.Transparent;
      this.motorHealtyPictureBox.Cursor = Cursors.Hand;
      this.motorHealtyPictureBox.Image = (Image) Resources.Sync;
      this.motorHealtyPictureBox.Name = "motorHealtyPictureBox";
      this.motorHealtyPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.accuHealtyPictureBox, "accuHealtyPictureBox");
      this.accuHealtyPictureBox.BackColor = Color.Transparent;
      this.accuHealtyPictureBox.Image = (Image) Resources.Sync;
      this.accuHealtyPictureBox.Name = "accuHealtyPictureBox";
      this.accuHealtyPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.mmiHealtyPictureBox, "mmiHealtyPictureBox");
      this.mmiHealtyPictureBox.BackColor = Color.Transparent;
      this.mmiHealtyPictureBox.Name = "mmiHealtyPictureBox";
      this.mmiHealtyPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.flickerFreeTableLayoutPanel3, "flickerFreeTableLayoutPanel3");
      this.flickerFreeTableLayoutPanel3.Controls.Add((Control) this.sDiagPictureBox, 0, 0);
      this.flickerFreeTableLayoutPanel3.Controls.Add((Control) this.firmwarePictureBox, 0, 1);
      this.flickerFreeTableLayoutPanel3.Controls.Add((Control) this.diagnosePictureBox, 0, 2);
      this.flickerFreeTableLayoutPanel3.Controls.Add((Control) this.diagnoseButton, 1, 2);
      this.flickerFreeTableLayoutPanel3.Controls.Add((Control) this.sDiagButton, 1, 0);
      this.flickerFreeTableLayoutPanel3.Controls.Add((Control) this.firmwareButton, 1, 1);
      this.flickerFreeTableLayoutPanel3.Name = "flickerFreeTableLayoutPanel3";
      componentResourceManager.ApplyResources((object) this.sDiagPictureBox, "sDiagPictureBox");
      this.sDiagPictureBox.Image = (Image) Resources.button_ok;
      this.sDiagPictureBox.Name = "sDiagPictureBox";
      this.sDiagPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.firmwarePictureBox, "firmwarePictureBox");
      this.firmwarePictureBox.Image = (Image) Resources.button_ok;
      this.firmwarePictureBox.Name = "firmwarePictureBox";
      this.firmwarePictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.diagnosePictureBox, "diagnosePictureBox");
      this.diagnosePictureBox.Image = (Image) Resources.button_ok;
      this.diagnosePictureBox.Name = "diagnosePictureBox";
      this.diagnosePictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.diagnoseButton, "diagnoseButton");
      this.diagnoseButton.Name = "diagnoseButton";
      this.diagnoseButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.sDiagButton, "sDiagButton");
      this.sDiagButton.Name = "sDiagButton";
      this.sDiagButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.firmwareButton, "firmwareButton");
      this.firmwareButton.Name = "firmwareButton";
      this.firmwareButton.UseVisualStyleBackColor = true;
      this.bikeFlippedPictureBox.Image = (Image) Resources.bike_flipped;
      componentResourceManager.ApplyResources((object) this.bikeFlippedPictureBox, "bikeFlippedPictureBox");
      this.bikeFlippedPictureBox.Name = "bikeFlippedPictureBox";
      this.bikeFlippedPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.serviceCaption, "serviceCaption");
      this.serviceCaption.BackColor = Color.Transparent;
      this.serviceCaption.Name = "serviceCaption";
      this.bikePictureBox.Accu = (Image) Resources.Bikestatus_sync_Accu;
      this.bikePictureBox.BackColor = Color.Transparent;
      this.bikePictureBox.Bike = (Image) Resources.Bikestatus_only;
      componentResourceManager.ApplyResources((object) this.bikePictureBox, "bikePictureBox");
      this.bikePictureBox.MMI = (Image) Resources.Bikestatus_sync_sMMI;
      this.bikePictureBox.Motor = (Image) Resources.Bikestatus_sync_Motor;
      this.bikePictureBox.Name = "bikePictureBox";
      this.bikePictureBox.TabStop = false;
      this.diagnoseTabPage.BackColor = Color.White;
      this.diagnoseTabPage.Controls.Add((Control) this.errorOverviewPanel);
      componentResourceManager.ApplyResources((object) this.diagnoseTabPage, "diagnoseTabPage");
      this.diagnoseTabPage.Name = "diagnoseTabPage";
      componentResourceManager.ApplyResources((object) this.errorOverviewPanel, "errorOverviewPanel");
      this.errorOverviewPanel.Name = "errorOverviewPanel";
      componentResourceManager.ApplyResources((object) this.readTourDataButton, "readTourDataButton");
      this.readTourDataButton.Name = "readTourDataButton";
      this.readTourDataButton.UseVisualStyleBackColor = true;
      this.readTourDataButton.Click += new EventHandler(this.readTourDataButton_Click);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.diagnoseTabControl);
      this.DoubleBuffered = true;
      this.Name = nameof (DiagnosePanel);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Layout += new LayoutEventHandler(this.DiagnosePanel_Layout);
      this.diagnoseTabControl.ResumeLayout(false);
      this.diagnoseStatusTabPage.ResumeLayout(false);
      this.diagnoseStatusTabPage.PerformLayout();
      this.accuTableLayoutPanel.ResumeLayout(false);
      this.accuTableLayoutPanel.PerformLayout();
      this.immobilizerPanel.ResumeLayout(false);
      this.immobilizerFlickerFreeTableLayout.ResumeLayout(false);
      this.immobilizerFlickerFreeTableLayout.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.immobilizerFlickerFreeTableLayout2.ResumeLayout(false);
      this.servicePanel.ResumeLayout(false);
      this.serviceFlickerFreeTableLayout.ResumeLayout(false);
      this.serviceFlickerFreeTableLayout.PerformLayout();
      ((ISupportInitialize) this.datePictureBox).EndInit();
      this.kilometerPanel.ResumeLayout(false);
      this.kilometersFlickerFreeTableLayout.ResumeLayout(false);
      this.kilometersFlickerFreeTableLayout.PerformLayout();
      ((ISupportInitialize) this.distancePictureBox).EndInit();
      this.distanceTableLayoutPanel.ResumeLayout(false);
      this.distanceTableLayoutPanel.PerformLayout();
      this.hackFlickerFreeTableLayout.ResumeLayout(false);
      this.hackFlickerFreeTableLayout.PerformLayout();
      this.flickerFreeTableLayoutPanel4.ResumeLayout(false);
      this.flickerFreeTableLayoutPanel4.PerformLayout();
      ((ISupportInitialize) this.motorHealtyPictureBox).EndInit();
      ((ISupportInitialize) this.accuHealtyPictureBox).EndInit();
      ((ISupportInitialize) this.mmiHealtyPictureBox).EndInit();
      this.flickerFreeTableLayoutPanel3.ResumeLayout(false);
      this.flickerFreeTableLayoutPanel3.PerformLayout();
      ((ISupportInitialize) this.sDiagPictureBox).EndInit();
      ((ISupportInitialize) this.firmwarePictureBox).EndInit();
      ((ISupportInitialize) this.diagnosePictureBox).EndInit();
      ((ISupportInitialize) this.bikeFlippedPictureBox).EndInit();
      ((ISupportInitialize) this.bikePictureBox).EndInit();
      this.diagnoseTabPage.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
