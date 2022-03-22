// Decompiled with JetBrains decompiler
// Type: ZerroWare.ErrorOverviewPanel
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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class ErrorOverviewPanel : UserControl
  {
    private readonly byte[] errorInformationZeros = new byte[24];
    private readonly byte[] errorInformationFFs = new byte[24]
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
    private int avalErrors;
    private readonly int[] headerPages = new int[5]
    {
      1,
      13,
      25,
      37,
      49
    };
    private readonly int numberOfPagesPerError = 12;
    private byte[][] errorInformation;
    private IContainer components;
    private FlickerFreeTableLayoutPanel errorListTableLayoutPanel;
    private Label dateCaptionLabel;
    private Label errorMessageCaptionLabel;
    private Label componentInformationCaptionLabel;
    private PictureBox[] detailsPictureBox;
    private ComponentDetails[] componentDetails;
    private Label[] dateLabel;
    private Label[] errorMessageLabel;
    private Button createTotalReportButton;
    private Button createShortReportButton;
    private Panel panel1;

    public ErrorOverviewPanel()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.InitUISettings();
      this.Update();
      this.InitErrorElements();
      this.errorInformation = new byte[0][];
      MouseWheelRedirector.Attach((Control) this.panel1);
      MouseWheelRedirector.Attach((Control) this.errorListTableLayoutPanel);
      MouseWheelRedirector.Attach((Control) this.dateCaptionLabel);
      MouseWheelRedirector.Attach((Control) this.errorMessageCaptionLabel);
      MouseWheelRedirector.Attach((Control) this.componentInformationCaptionLabel);
    }

    private void InitUISettings()
    {
      this.dateCaptionLabel.Font = FontDefinition.TableCaptionTextFont;
      this.errorMessageCaptionLabel.Font = FontDefinition.TableCaptionTextFont;
      this.componentInformationCaptionLabel.Font = FontDefinition.TableCaptionTextFont;
      this.createTotalReportButton.Font = FontDefinition.DefaultTextFont;
      this.createShortReportButton.Font = FontDefinition.DefaultTextFont;
      this.dateCaptionLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.errorMessageCaptionLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.componentInformationCaptionLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.createTotalReportButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.createTotalReportButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.createShortReportButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.createShortReportButton.BackgroundImageLayout = ImageLayout.Stretch;
      Image resourceImage = HelperClass.ResizeImage((Image) Resources.print_preview_24x24, new Size(20, 20));
      this.createTotalReportButton.Image = HelperClass.AddRightSpaceToImage(resourceImage, 5);
      this.createShortReportButton.Image = HelperClass.AddLeftSpaceToImage(resourceImage, 5);
    }

    private void errorListTableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
    {
      if (e.Row % 2 == 0 && e.Row != 0)
      {
        Graphics graphics = e.Graphics;
        Rectangle cellBounds = e.CellBounds;
        try
        {
          graphics.FillRectangle((Brush) UIElements.Brushes.RowHighlightBrush, cellBounds);
        }
        catch (ArgumentException ex)
        {
        }
      }
      if (e.Row != 0)
        return;
      Graphics graphics1 = e.Graphics;
      Rectangle cellBounds1 = e.CellBounds;
      try
      {
        graphics1.FillRectangle((Brush) UIElements.Brushes.TableCaptionBrush(cellBounds1), cellBounds1);
      }
      catch (ArgumentException ex)
      {
      }
    }

    private void SetEmptyErrorInformation(int page)
    {
      this.dateLabel[page].Text = "-";
      this.errorMessageLabel[page].Text = "-";
      this.componentDetails[page].BatteryHardwareVersion = "-";
      this.componentDetails[page].BatterySerialNumber = "-";
      this.componentDetails[page].BatterySoftwareActualVersion = "-";
      this.componentDetails[page].BatterySoftwareErrorVersion = "-";
      this.componentDetails[page].MotorHardwareVersion = "-";
      this.componentDetails[page].MotorSerialNumber = "-";
      this.componentDetails[page].MotorSoftwareActualVersion = "-";
      this.componentDetails[page].MotorSoftwareErrorVersion = "-";
      this.componentDetails[page].MMIHardwareVersion = "-";
      this.componentDetails[page].MMISerialNumber = "-";
      this.componentDetails[page].MMISoftwareActualVersion = "-";
      this.componentDetails[page].MMISoftwareErrorVersion = "-";
      this.detailsPictureBox[page].Image = (Image) null;
      this.detailsPictureBox[page].Name = string.Concat((object) page);
      this.detailsPictureBox[page].Click -= new EventHandler(this.detailsPictureBox_Click);
      this.detailsPictureBox[page].Cursor = Cursors.Default;
    }

    private void FillAnErrorListElement(int overviewPage)
    {
      // ISSUE: variable of a compiler-generated type
      Parameters instance = Parameters.Instance;
      int page = overviewPage / this.numberOfPagesPerError;
      if (this.errorInformation[overviewPage].Length != 24)
      {
        this.SetEmptyErrorInformation(page);
      }
      else
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        ParameterValue parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.DATE_TIME, instance.PositionOfValueInElement(ParameterValueElements.UHRZEIT_STUNDE)));
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.DATE_TIME, instance.PositionOfValueInElement(ParameterValueElements.UHRZEIT_STUNDE)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][0]
          };
          this.dateLabel[page].Text = string.Format("{0:00}", (object) parameterValue.ReadableValue) + ":";
        }
        catch (ParameterValueException ex)
        {
          Label label = this.dateLabel[page];
          label.Text = label.Text + "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.DATE_TIME, instance.PositionOfValueInElement(ParameterValueElements.UHRZEIT_MINUTE)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][1]
          };
          Label label = this.dateLabel[page];
          label.Text = label.Text + string.Format("{0:00}", (object) parameterValue.ReadableValue) + "\n";
        }
        catch (ParameterValueException ex)
        {
          Label label = this.dateLabel[page];
          label.Text = label.Text + "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.DATE_TIME, instance.PositionOfValueInElement(ParameterValueElements.DATUM_TAG)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][2]
          };
          Label label = this.dateLabel[page];
          label.Text = label.Text + string.Format("{0:00}", (object) parameterValue.ReadableValue) + ".";
        }
        catch (ParameterValueException ex)
        {
          Label label = this.dateLabel[page];
          label.Text = label.Text + "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.DATE_TIME, instance.PositionOfValueInElement(ParameterValueElements.DATUM_MONAT)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][3]
          };
          Label label = this.dateLabel[page];
          label.Text = label.Text + string.Format("{0:00}", (object) parameterValue.ReadableValue) + ".";
        }
        catch (ParameterValueException ex)
        {
          Label label = this.dateLabel[page];
          label.Text = label.Text + "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.DATE_TIME, instance.PositionOfValueInElement(ParameterValueElements.DATUM_JAHR)));
          parameterValue.LittleEndianBytes = BitConverter.GetBytes((ushort) (2000U + (uint) this.errorInformation[overviewPage][4]));
          this.dateLabel[page].Text += (string) (object) parameterValue.ReadableValue;
        }
        catch (ParameterValueException ex)
        {
          Label label = this.dateLabel[page];
          label.Text = label.Text + "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.HARDWAREVERSION_MMI)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][5]
          };
          this.componentDetails[page].MMIHardwareVersion = string.Concat((object) parameterValue.ReadableValue);
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MMIHardwareVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ANTRIEB)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][6]
          };
          this.componentDetails[page].MMISoftwareErrorVersion = string.Format("{0:0.0}", (object) parameterValue.ReadableValue).Replace(',', '.');
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MMISoftwareErrorVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.ACCU_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.HARDWAREVERSION_ACCU)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][7]
          };
          this.componentDetails[page].BatteryHardwareVersion = string.Concat((object) parameterValue.ReadableValue);
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].BatteryHardwareVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.ACCU_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ACCU)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][8]
          };
          this.componentDetails[page].BatterySoftwareErrorVersion = string.Format("{0:0.0}", (object) parameterValue.ReadableValue).Replace(',', '.');
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].BatterySoftwareErrorVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.ACCU_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SERIENNUMMER_ACCU)));
          parameterValue.BigEndianBytes = new byte[4]
          {
            (byte) 0,
            this.errorInformation[overviewPage][9],
            this.errorInformation[overviewPage][10],
            this.errorInformation[overviewPage][11]
          };
          this.componentDetails[page].BatterySerialNumber = string.Concat((object) parameterValue.ReadableValue);
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].BatterySerialNumber = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        this.componentDetails[page].BatterySerialNumber = HelperClass.AddSpacesToSerialNumber(this.componentDetails[page].BatterySerialNumber);
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.HARDWAREVERSION_ANTRIEB)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][12]
          };
          this.componentDetails[page].MotorHardwareVersion = string.Concat((object) parameterValue.ReadableValue);
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MotorHardwareVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ANTRIEB)));
          parameterValue.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[overviewPage][13]
          };
          this.componentDetails[page].MotorSoftwareErrorVersion = string.Format("{0:0.0}", (object) parameterValue.ReadableValue).Replace(',', '.');
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MotorSoftwareErrorVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ErrorNumberMotor errorNumberMotor = new ErrorNumberMotor();
        // ISSUE: reference to a compiler-generated method
        this.errorMessageLabel[page].Text = errorNumberMotor.ErrorNumberMessage(this.errorInformation[overviewPage][17], this.errorInformation[overviewPage][18], this.errorInformation[overviewPage + 1][0], this.errorInformation[overviewPage + 1][1]);
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = instance.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_MMI_A));
          this.componentDetails[page].MMISoftwareActualVersion = parameterValue.ReadableValue.ToString() + ".";
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = instance.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_MMI_B));
          this.componentDetails[page].MMISoftwareActualVersion += (string) (object) parameterValue.ReadableValue;
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MMISoftwareActualVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ANTRIEB));
          this.componentDetails[page].MotorSoftwareActualVersion = string.Format("{0:0.0}", (object) parameterValue.ReadableValue).Replace(',', '.');
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MotorSoftwareActualVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue = instance.GetParameterValueObject(ParameterIds.ACCU_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ACCU));
          this.componentDetails[page].BatterySoftwareActualVersion = string.Format("{0:0.0}", (object) parameterValue.ReadableValue).Replace(',', '.');
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].BatterySoftwareActualVersion = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        try
        {
          this.componentDetails[page].MotorSerialNumber = "";
          // ISSUE: reference to a compiler-generated method
          for (int parameterPosition = Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SERIENNUMMER_ANTRIEB_01); parameterPosition < 11; ++parameterPosition)
          {
            // ISSUE: reference to a compiler-generated method
            ParameterValue parameterValueObject = Parameters.Instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, parameterPosition);
            this.componentDetails[page].MotorSerialNumber += (string) (object) parameterValueObject.ReadableValue;
          }
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MotorSerialNumber = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        this.componentDetails[page].MotorSerialNumber = HelperClass.AddSpacesToSerialNumber(this.componentDetails[page].MotorSerialNumber);
        try
        {
          this.componentDetails[page].MMISerialNumber = "";
          // ISSUE: reference to a compiler-generated method
          for (int parameterPosition = Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SERIENNUMMER_MMI_01); parameterPosition < 11; ++parameterPosition)
          {
            // ISSUE: reference to a compiler-generated method
            ParameterValue parameterValueObject = Parameters.Instance.GetParameterValueObject(ParameterIds.MMI_PRODUCTION_INFORMATION, parameterPosition);
            this.componentDetails[page].MMISerialNumber += (string) (object) parameterValueObject.ReadableValue;
          }
        }
        catch (ParameterValueException ex)
        {
          this.componentDetails[page].MMISerialNumber = "NaN [" + BitConverter.ToString(parameterValue.ErrorValueAsBigEndian()) + "]";
        }
        this.componentDetails[page].MMISerialNumber = HelperClass.AddSpacesToSerialNumber(this.componentDetails[page].MMISerialNumber);
        this.detailsPictureBox[page].Image = (Image) Resources.print_preview_48x48;
        this.detailsPictureBox[page].Name = string.Concat((object) page);
        this.detailsPictureBox[page].Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.detailsPictureBox[page].Click += new EventHandler(this.detailsPictureBox_Click);
        this.detailsPictureBox[page].Cursor = Cursors.Hand;
      }
    }

    private int AvailableErrors
    {
      set
      {
        if (value >= 15)
          this.avalErrors = 0;
        else
          this.avalErrors = value;
      }
      get => this.avalErrors;
    }

    private void FillSystemStateErrorDetail()
    {
      int index1 = 0;
      // ISSUE: variable of a compiler-generated type
      Parameters instance = Parameters.Instance;
      this.AvailableErrors = (int) this.errorInformation[index1][0];
      string str1 = string.Empty;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      ParameterValue parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.SERIENNUMMER_ANTRIEB_01)));
      for (int index2 = 1; index2 < 12; ++index2)
      {
        try
        {
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index1][index2]
          };
          str1 += string.Format("{0:0}", (object) parameterValue1.ReadableValue);
        }
        catch (ParameterValueException ex)
        {
          str1 = str1 + "NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]";
        }
      }
      string empty1 = string.Empty;
      string str2;
      try
      {
        parameterValue1 = new ParameterValue((ParameterIds) 0, 0, ParameterValue.IsReadOnly(true, true, true, true, true), ParameterValue.Visibility(false, false, false, false, false), typeof (uint), 0L, 16777215L, 1.0, 0L, (Func<double, double>) (x => x), (Func<double, double>) (x => x), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        parameterValue1.BigEndianBytes = new byte[4]
        {
          (byte) 0,
          this.errorInformation[index1][12],
          this.errorInformation[index1][13],
          this.errorInformation[index1][14]
        };
        str2 = empty1 + (object) parameterValue1.ReadableValue;
      }
      catch (ParameterValueException ex)
      {
        str2 = empty1 + "NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]";
      }
      string empty2 = string.Empty;
      string str3;
      try
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        ParameterValue parameterValue2 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.GESAMTKILOMETER)));
        parameterValue1 = new ParameterValue(ParameterIds.MOTOR_INFORMATION, parameterValue2.PositionOfParamter, new bool[5]
        {
          false,
          false,
          false,
          false,
          true
        }, new bool[5]{ false, false, false, false, true }, typeof (uint), 0L, 16777215L, 0.01, 0L, (Func<double, double>) (x => x * 100.0), (Func<double, double>) (x => x / 100.0), parameterValue2.Label, parameterValue2.UnitLabel, parameterValue2.HelpText, parameterValue2.WarningText, parameterValue2.EnLabel);
        parameterValue1.BigEndianBytes = new byte[4]
        {
          (byte) 0,
          this.errorInformation[index1][15],
          this.errorInformation[index1][16],
          this.errorInformation[index1][17]
        };
        str3 = empty2 + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel;
      }
      catch (ParameterValueException ex)
      {
        str3 = empty2 + "NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "] " + parameterValue1.UnitLabel;
      }
      string empty3 = string.Empty;
      string str4;
      try
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.SERVICE_INTERVAL, instance.PositionOfValueInElement(ParameterValueElements.SERVICE_INTERVAL_STRECKE)));
        parameterValue1.BigEndianBytes = new byte[4]
        {
          (byte) 0,
          (byte) 0,
          this.errorInformation[index1][18],
          this.errorInformation[index1][19]
        };
        str4 = empty3 + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel;
      }
      catch (ParameterValueException ex)
      {
        str4 = empty3 + "NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "] " + parameterValue1.UnitLabel;
      }
      for (int index2 = 0; index2 < this.headerPages.Length; ++index2)
      {
        this.componentDetails[index2].MotorSerialErrorNumber = str1;
        this.componentDetails[index2].MotorSerialErrorNumber = HelperClass.AddSpacesToSerialNumber(this.componentDetails[index2].MotorSerialErrorNumber);
        this.componentDetails[index2].OperationTime = str2;
        this.componentDetails[index2].OperationTime = HelperClass.ConvertSecondsToReadable(this.componentDetails[index2].OperationTime);
        this.componentDetails[index2].TotalKilometers = str3;
        this.componentDetails[index2].ServiceKilometers = str4;
      }
    }

    public void SetErrorInformation(byte[][] errorInformation)
    {
      if (errorInformation.Length != 61)
        return;
      this.errorInformation = errorInformation;
      this.FillSystemStateErrorDetail();
      for (int page = this.headerPages.Length - 1; page >= 0; --page)
      {
        if (((IEnumerable<byte>) this.errorInformationZeros).SequenceEqual<byte>((IEnumerable<byte>) errorInformation[this.headerPages[page]]) || ((IEnumerable<byte>) this.errorInformationFFs).SequenceEqual<byte>((IEnumerable<byte>) errorInformation[this.headerPages[page]]) || page > this.AvailableErrors - 1)
          this.SetEmptyErrorInformation(page);
        else
          this.FillAnErrorListElement(this.headerPages[page]);
      }
    }

    private void detailsPictureBox_Click(object sender, EventArgs e)
    {
      int int32;
      try
      {
        int32 = Convert.ToInt32(((Control) sender).Name);
      }
      catch (FormatException ex)
      {
        return;
      }
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ErrorDetailReport errorReport = new ErrorDetailReport(this.numberOfPagesPerError, Directories.Instance.ReportLogoImageFileName);
      this.GenerateSingleReport(ref errorReport, int32);
      PreViewDialog preViewDialog = new PreViewDialog(errorReport.Document);
      MainWindow.Instance.Cursor = Cursors.Default;
      int num = (int) preViewDialog.ShowDialog();
    }

    private void GenerateSingleReport(ref ErrorDetailReport errorReport, int detailPage)
    {
      errorReport.ResetContent(errorReport.NumberOfErrorPages, Directories.Instance.ReportLogoImageFileName);
      // ISSUE: variable of a compiler-generated type
      Parameters instance = Parameters.Instance;
      errorReport.ErrorOverviewCaption = GlobalResource.ErrorReport_DetailErrorOverviewCaption;
      errorReport.BatteryStateCaption = GlobalResource.ErrorReport_DetailBatteryStateCaption;
      errorReport.DocumentSubject = GlobalResource.ErrorReport_DetailDocumentSubject;
      errorReport.DocumentTitle = GlobalResource.ErrorReport_DetailDocumentTitle;
      errorReport.MMIStateCaption = GlobalResource.ErrorReport_DetailMMIStateCaption;
      errorReport.MotorStateCaption = GlobalResource.ErrorReport_DetailMotorStateCaption;
      errorReport.PageCaption = GlobalResource.ErrorReport_DetailPageCaption;
      errorReport.SensorDataCaption = GlobalResource.ErrorReport_DetailSensorDataCaption;
      errorReport.SystemErrorCaption = GlobalResource.ErrorReport_DetailSystemErrorCaption;
      ErrorDetailReport errorDetailReport1 = errorReport;
      errorDetailReport1.ErrorOverviewInformation = errorDetailReport1.ErrorOverviewInformation + this.dateCaptionLabel.Text + ":\n" + this.dateLabel[detailPage].Text.Replace('\n', ' ') + "\n\n";
      ErrorDetailReport errorDetailReport2 = errorReport;
      errorDetailReport2.ErrorOverviewInformation = errorDetailReport2.ErrorOverviewInformation + this.errorMessageCaptionLabel.Text + ":\n" + this.errorMessageLabel[detailPage].Text + "\n\n";
      ErrorDetailReport errorDetailReport3 = errorReport;
      errorDetailReport3.ErrorOverviewInformation = errorDetailReport3.ErrorOverviewInformation + this.componentInformationCaptionLabel.Text + ":\n";
      errorReport.ErrorOverviewInformation += this.componentDetails[detailPage].ToString();
      int num = detailPage * this.numberOfPagesPerError + 1;
      for (int index1 = 1; index1 < 12; ++index1)
      {
        int index2 = num + index1;
        int index3 = index1 - 1;
        string empty = string.Empty;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SystemErrorMotor systemErrorMotor = new SystemErrorMotor(this.errorInformation[index2][0], this.errorInformation[index2][1]);
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ClientStateMMI clientStateMmi = new ClientStateMMI(this.errorInformation[index2][2]);
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ClientStateAccu clientStateAccu = new ClientStateAccu(this.errorInformation[index2][3]);
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ClientStateMotor clientStateMotor = new ClientStateMotor(this.errorInformation[index2][4]);
        string str1 = string.Empty;
        foreach (SystemErrorMotor.ErrorList error in Enum.GetValues(typeof (SystemErrorMotor.ErrorList)))
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          if (systemErrorMotor.ErrorOccurred(error) && systemErrorMotor.ErrorOccurredMessage(error) != string.Empty)
          {
            // ISSUE: reference to a compiler-generated method
            str1 = str1 + systemErrorMotor.ErrorOccurredMessage(error) + "\n";
          }
        }
        if (string.IsNullOrEmpty(str1))
          str1 = "-\n";
        errorReport.SystemErrorInformation[index3] = str1;
        string str2 = string.Empty;
        foreach (ClientStateMMI.StateList stateMessage in Enum.GetValues(typeof (ClientStateMMI.StateList)))
        {
          // ISSUE: reference to a compiler-generated method
          if (clientStateMmi.StateMessage(stateMessage) != string.Empty)
          {
            // ISSUE: reference to a compiler-generated method
            str2 = str2 + clientStateMmi.StateMessage(stateMessage) + "\n";
          }
        }
        errorReport.MMIStateInformation[index3] = str2;
        string str3 = string.Empty;
        foreach (ClientStateMotor.StateList stateMessage in Enum.GetValues(typeof (ClientStateMotor.StateList)))
        {
          // ISSUE: reference to a compiler-generated method
          if (clientStateMotor.StateMessage(stateMessage) != string.Empty)
          {
            // ISSUE: reference to a compiler-generated method
            str3 = str3 + clientStateMotor.StateMessage(stateMessage) + "\n";
          }
        }
        errorReport.MotorStateInformation[index3] = str3;
        string str4 = string.Empty;
        foreach (ClientStateAccu.StateList stateMessage in Enum.GetValues(typeof (ClientStateAccu.StateList)))
        {
          // ISSUE: reference to a compiler-generated method
          if (clientStateAccu.StateMessage(stateMessage) != string.Empty)
          {
            // ISSUE: reference to a compiler-generated method
            str4 = str4 + clientStateAccu.StateMessage(stateMessage) + "\n";
          }
        }
        errorReport.BatteryStateInformation[index3] = str4;
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        ParameterValue parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.GESAMTKILOMETER)));
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          ParameterValue parameterValue2 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.GESAMTKILOMETER)));
          parameterValue1 = new ParameterValue(ParameterIds.MOTOR_INFORMATION, parameterValue2.PositionOfParamter, new bool[5]
          {
            false,
            false,
            false,
            false,
            true
          }, new bool[5]{ false, false, false, false, true }, typeof (uint), 0L, 16777215L, 0.01, 0L, (Func<double, double>) (x => x * 100.0), (Func<double, double>) (x => x / 100.0), parameterValue2.Label, parameterValue2.UnitLabel, parameterValue2.HelpText, parameterValue2.WarningText, parameterValue2.EnLabel);
          parameterValue1.BigEndianBytes = new byte[4]
          {
            (byte) 0,
            this.errorInformation[index2][5],
            this.errorInformation[index2][6],
            this.errorInformation[index2][7]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.TEMPERATUR_MOTOR_ELEKTRONIK)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][9]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.TEMPERATUR_MOTOR)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][10]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.ACCU_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.TEMPERATUR_ACCU)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][11]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.GESCHWINDIGKEIT)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][12]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.TRITTFREQUENZ)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][13]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_BATTERY_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.MOTOR_BATTERIESPANNUNG)));
          parameterValue1.BigEndianBytes = new byte[2]
          {
            this.errorInformation[index2][14],
            this.errorInformation[index2][15]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_BATTERY_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.MOTOR_BATTERIESTROM)));
          parameterValue1.BigEndianBytes = new byte[2]
          {
            this.errorInformation[index2][16],
            this.errorInformation[index2][17]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.DREHMOMENT_KASSETTE)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][18]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.IST_UNTERSTUETZUNGSGRAD)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][19]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MMI_DRIVE_SETTINGS, instance.PositionOfValueInElement(ParameterValueElements.BREMSASSISTENT_GESCHWINDIGKEIT)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][20]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.ACCU_INFORMATION, instance.PositionOfValueInElement(ParameterValueElements.REKUPERATIONSSTROM)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][21]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          parameterValue1 = new ParameterValue(instance.GetParameterValueObject(ParameterIds.MMI_DRIVE_SETTINGS, instance.PositionOfValueInElement(ParameterValueElements.GESCHWINDIGKEIT_SCHIEBEHILFE_VORWAERTS)));
          parameterValue1.LittleEndianBytes = new byte[1]
          {
            this.errorInformation[index2][22]
          };
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": " + (object) parameterValue1.ReadableValue + " " + parameterValue1.UnitLabel + "\n";
          sensorDataInformation[index4] = str5;
        }
        catch (ParameterValueException ex)
        {
          string[] sensorDataInformation;
          int index4;
          string str5 = (sensorDataInformation = errorReport.SensorDataInformation)[(int) (index4 = index3)] + parameterValue1.Label + ": NaN [" + BitConverter.ToString(parameterValue1.ErrorValueAsBigEndian()) + "]\n";
          sensorDataInformation[index4] = str5;
        }
      }
      errorReport.AddContentToDocument();
    }

    private void createTotalReportButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ErrorDetailReport errorReport = new ErrorDetailReport(this.numberOfPagesPerError, Directories.Instance.ReportLogoImageFileName);
      for (int detailPage = 0; detailPage < this.AvailableErrors; ++detailPage)
        this.GenerateSingleReport(ref errorReport, detailPage);
      PreViewDialog preViewDialog = new PreViewDialog(errorReport.Document);
      MainWindow.Instance.Cursor = Cursors.Default;
      int num = (int) preViewDialog.ShowDialog();
    }

    private void createShortReportButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ShortErrorReport shortErrorReport = new ShortErrorReport(GlobalResource.ShortErrorReport_ServiceReport, "short report of service information", Directories.Instance.ReportLogoImageFileName, false, "");
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
      if (HelperClass.IsUpToDate(Parameters.Instance.AccuFirmwareVersion, newReadableVersion2))
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
      string[] errorMessages = this.GetErrorMessages();
      List<string> stringList1 = new List<string>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      string str4 = new ErrorNumberMotor().ErrorNumberMessage((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      for (int index = 0; index < errorMessages.Length; ++index)
      {
        if (!errorMessages[index].Equals("-") && !errorMessages[index].Equals(str4))
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
        shortErrorReport.AddErrorDetailInformation(GlobalResource.ShortErrorReport_DiagnoseReportCaption, GlobalResource.ShortErrorReport_RecommendedProcedureCaption, GlobalResource.ShortErrorReport_DiagnoseTableCaption, array, accuHasError);
      ParameterPanel parameterPanel = new ParameterPanel();
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
      PreViewDialog preViewDialog = new PreViewDialog(shortErrorReport.Document);
      MainWindow.Instance.Cursor = Cursors.Default;
      int num = (int) preViewDialog.ShowDialog();
    }

    public string[] GetErrorMessages()
    {
      string[] strArray = new string[this.AvailableErrors];
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: reference to a compiler-generated method
      new ErrorNumberMotor().ErrorNumberMessage((byte) 0, (byte) 0, (byte) 0, (byte) 0);
      for (int index1 = 0; index1 < this.AvailableErrors; ++index1)
      {
        int index2 = index1 * this.numberOfPagesPerError + 2;
        double num = 0.0;
        try
        {
          // ISSUE: reference to a compiler-generated method
          // ISSUE: reference to a compiler-generated method
          ParameterValue parameterValue = new ParameterValue(Parameters.Instance.GetParameterValueObject(ParameterIds.MOTOR_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.GESAMTKILOMETER)));
          num = new ParameterValue(ParameterIds.MOTOR_INFORMATION, parameterValue.PositionOfParamter, new bool[5]
          {
            false,
            false,
            false,
            false,
            true
          }, new bool[5]{ false, false, false, false, true }, typeof (uint), 0L, 16777215L, 0.01, 0L, (Func<double, double>) (x => x * 100.0), (Func<double, double>) (x => x / 100.0), parameterValue.Label, parameterValue.UnitLabel, parameterValue.HelpText, parameterValue.WarningText, parameterValue.EnLabel)
          {
            BigEndianBytes = new byte[4]
            {
              (byte) 0,
              this.errorInformation[index2][5],
              this.errorInformation[index2][6],
              this.errorInformation[index2][7]
            }
          }.ReadableValue;
        }
        catch (ParameterValueException ex)
        {
        }
        strArray[index1] = string.Format(GlobalResource.ErrorOverviewPanel_Report_SerialNumber_Text, (object) this.componentDetails[index1].MotorSerialErrorNumber.Replace(" ", ""), (object) this.dateLabel[index1].Text.Replace("\n", " "), (object) num, (object) this.errorMessageLabel[index1].Text);
      }
      return strArray;
    }

    public string MotorSerialErrorNumber() => this.componentDetails[0].MotorSerialErrorNumber;

    public void EmptyAllInformation()
    {
      foreach (int headerPage in this.headerPages)
        this.SetEmptyErrorInformation(headerPage / this.numberOfPagesPerError);
      this.AvailableErrors = 0;
      this.errorInformation = new byte[0][];
    }

    private void InitErrorElements()
    {
      int length = this.headerPages.Length;
      this.errorListTableLayoutPanel.RowCount = length + 1;
      this.detailsPictureBox = new PictureBox[length];
      this.componentDetails = new ComponentDetails[length];
      this.dateLabel = new Label[length];
      this.errorMessageLabel = new Label[length];
      for (int page = 0; page < length; ++page)
      {
        this.detailsPictureBox[page] = new PictureBox();
        this.detailsPictureBox[page].Image = (Image) Resources.print_preview_24x24;
        this.detailsPictureBox[page].BackColor = Color.Transparent;
        this.detailsPictureBox[page].SizeMode = PictureBoxSizeMode.Zoom;
        this.detailsPictureBox[page].Size = new Size(36, 36);
        this.componentDetails[page] = new ComponentDetails();
        this.componentDetails[page].Dock = DockStyle.Fill;
        this.dateLabel[page] = new Label();
        this.dateLabel[page].Font = FontDefinition.DefaultTextFont;
        this.dateLabel[page].ForeColor = ColorDefinition.BlackTextColor;
        this.dateLabel[page].BackColor = Color.Transparent;
        this.dateLabel[page].TextAlign = ContentAlignment.TopLeft;
        this.dateLabel[page].Dock = DockStyle.Fill;
        this.errorMessageLabel[page] = new Label();
        this.errorMessageLabel[page].Font = FontDefinition.DefaultTextFont;
        this.errorMessageLabel[page].ForeColor = ColorDefinition.BlackTextColor;
        this.errorMessageLabel[page].BackColor = Color.Transparent;
        this.errorMessageLabel[page].TextAlign = ContentAlignment.TopLeft;
        this.errorMessageLabel[page].Dock = DockStyle.Fill;
        this.errorMessageLabel[page].AutoEllipsis = true;
        this.SetEmptyErrorInformation(page);
        this.errorListTableLayoutPanel.Controls.Add((Control) this.dateLabel[page], 0, page + 1);
        this.errorListTableLayoutPanel.Controls.Add((Control) this.errorMessageLabel[page], 1, page + 1);
        this.errorListTableLayoutPanel.Controls.Add((Control) this.componentDetails[page], 2, page + 1);
        this.errorListTableLayoutPanel.Controls.Add((Control) this.detailsPictureBox[page], 3, page + 1);
      }
      MouseWheelRedirector.Attach((Control[]) this.detailsPictureBox);
      MouseWheelRedirector.Attach((Control[]) this.componentDetails);
      MouseWheelRedirector.Attach((Control[]) this.dateLabel);
      MouseWheelRedirector.Attach((Control[]) this.errorMessageLabel);
    }

    protected override void Dispose(bool disposing)
    {
      MouseWheelRedirector.Detach((Control) this.panel1);
      MouseWheelRedirector.Detach((Control) this.errorListTableLayoutPanel);
      MouseWheelRedirector.Detach((Control) this.dateCaptionLabel);
      MouseWheelRedirector.Detach((Control) this.errorMessageCaptionLabel);
      MouseWheelRedirector.Detach((Control) this.componentInformationCaptionLabel);
      MouseWheelRedirector.Detach((Control[]) this.detailsPictureBox);
      MouseWheelRedirector.Detach((Control[]) this.componentDetails);
      MouseWheelRedirector.Detach((Control[]) this.dateLabel);
      MouseWheelRedirector.Detach((Control[]) this.errorMessageLabel);
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ErrorOverviewPanel));
      this.createShortReportButton = new Button();
      this.createTotalReportButton = new Button();
      this.panel1 = new Panel();
      this.errorListTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.dateCaptionLabel = new Label();
      this.errorMessageCaptionLabel = new Label();
      this.componentInformationCaptionLabel = new Label();
      this.errorListTableLayoutPanel.SuspendLayout();
      this.SuspendLayout();
      this.createShortReportButton.Image = (Image) Resources.print_preview_24x24;
      componentResourceManager.ApplyResources((object) this.createShortReportButton, "createShortReportButton");
      this.createShortReportButton.Name = "createShortReportButton";
      this.createShortReportButton.UseVisualStyleBackColor = true;
      this.createShortReportButton.Click += new EventHandler(this.createShortReportButton_Click);
      componentResourceManager.ApplyResources((object) this.createTotalReportButton, "createTotalReportButton");
      this.createTotalReportButton.Image = (Image) Resources.print_preview_24x24;
      this.createTotalReportButton.Name = "createTotalReportButton";
      this.createTotalReportButton.UseVisualStyleBackColor = true;
      this.createTotalReportButton.Click += new EventHandler(this.createTotalReportButton_Click);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      componentResourceManager.ApplyResources((object) this.errorListTableLayoutPanel, "errorListTableLayoutPanel");
      this.errorListTableLayoutPanel.Controls.Add((Control) this.dateCaptionLabel, 0, 0);
      this.errorListTableLayoutPanel.Controls.Add((Control) this.errorMessageCaptionLabel, 1, 0);
      this.errorListTableLayoutPanel.Controls.Add((Control) this.componentInformationCaptionLabel, 2, 0);
      this.errorListTableLayoutPanel.Name = "errorListTableLayoutPanel";
      this.errorListTableLayoutPanel.CellPaint += new TableLayoutCellPaintEventHandler(this.errorListTableLayoutPanel_CellPaint);
      componentResourceManager.ApplyResources((object) this.dateCaptionLabel, "dateCaptionLabel");
      this.dateCaptionLabel.BackColor = Color.Transparent;
      this.dateCaptionLabel.Name = "dateCaptionLabel";
      componentResourceManager.ApplyResources((object) this.errorMessageCaptionLabel, "errorMessageCaptionLabel");
      this.errorMessageCaptionLabel.BackColor = Color.Transparent;
      this.errorMessageCaptionLabel.Name = "errorMessageCaptionLabel";
      componentResourceManager.ApplyResources((object) this.componentInformationCaptionLabel, "componentInformationCaptionLabel");
      this.componentInformationCaptionLabel.BackColor = Color.Transparent;
      this.componentInformationCaptionLabel.Name = "componentInformationCaptionLabel";
      this.AutoScaleMode = AutoScaleMode.Inherit;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.errorListTableLayoutPanel);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.createShortReportButton);
      this.Controls.Add((Control) this.createTotalReportButton);
      this.Name = nameof (ErrorOverviewPanel);
      this.errorListTableLayoutPanel.ResumeLayout(false);
      this.errorListTableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
