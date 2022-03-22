// Decompiled with JetBrains decompiler
// Type: ZerroWare.ParameterPanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using MMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class ParameterPanel : UserControl
  {
    private Dictionary<int, ParameterValue> allParameters;
    private int[] positionOfValueInDictionary;
    private Picture[] allPictures;
    private ComponentResourceManager parameterResources;
    private int accessLevel;
    private int dictionaryElements;
    private int pictureElements;
    private bool updateUIMethodIsNotWorking = true;
    private bool valueChanged;
    private ParameterPanelEventArgs args;
    private bool[] lastState;
    private int mmiDefaultsPosition;
    private MMIDefaults mmiDefaults;
    private int pushAssistantPosition;
    private readonly int pushAssistanCriticalValue = 70;
    private NumericUpDownWithActivation pushAssistant;
    private int pushBackAssistantPosition;
    private NumericUpDownWithActivation pushBackAssistant;
    private int speedLimiterPosition;
    private NumericUpDownWithActivation speedLimiter;
    private int supportLevelOnePosition;
    private int recuperationLevelOnePosition;
    private int lightSensorStepOnePosition;
    private int brightnessLevelOnePosition;
    private int mmiSystemStatePosition;
    private int driveControlStatePosition;
    private int accuControlStatePosition;
    private int buttonStatePosition;
    private int motorClientStatePosition;
    private int accuClientStatePosition;
    private Button[] stateDetailButton;
    private int serviceIntervalPosition;
    private DateControlWithActivation serviceInterval;
    private int actualDatePosition;
    private DateControl actualDate;
    private int firmwareUpdateDatePosition;
    private DateControl firmwareUpdateDate;
    private int productionDatePosition;
    private DateControl productionDate;
    private int actualTimePosition;
    private TimeControl actualTime;
    private int overtravelDistancePosition;
    private NumericUpDownWithConversion overtravelDistance;
    private int dailyDistancePosition;
    private NumericUpDownWithConversion dailyDistance;
    private int totalDistancePosition;
    private NumericUpDownWithConversion totalDistance;
    private int speedValuePosition;
    private NumericUpDownWithConversion speedValue;
    private int wheelCircumferencePosition;
    private NumericUpDownWithConversion wheelCircumference;
    private int restOfRangePosition;
    private NumericUpDownWithConversion restOfRange;
    private int maxSpeedPosition;
    private NumericUpDownWithConversion maxSpeed;
    private int averageSpeedPosition;
    private NumericUpDownWithConversion averageSpeed;
    private int nextServiceDistancePosition;
    private NumericUpDownWithSpecialValue nextServiceDistance;
    private int maxAssistanceSpeedPosition;
    private NumericUpDownWithConversion maxAssistanceSpeed;
    private int softwareVersionMMIPosition;
    private SoftwareVersionMMI softwareVersionMMI;
    private int serialnumberMotorPosition;
    private SerialnumberNumbericUpDown serialnumberMotor;
    private int serialnumberMMIPosition;
    private SerialnumberNumbericUpDown serialnumberMMI;
    private int foregroundColorPosition;
    private ColorChooser foregroundColor;
    private int backgroundColorPosition;
    private ColorChooser backgroundColor;
    private int dayTravelTimePosition;
    private TimeSpanUpDown dayTravelTime;
    private int totalTravelTimePosition;
    private TimeSpanUpDown totalTravelTime;
    private int brakeSwitchPosition;
    private CheckBox brakeSwitch;
    private int easyDisplaySwitchPosition;
    private CheckBox easyDisplaySwitch;
    private int ecoForegroundColorPosition;
    private ColorChooser ecoForegroundColor;
    private int ecoBackgroundColorPosition;
    private ColorChooser ecoBackgroundColor;
    private int boostForegroundColorPosition;
    private ColorChooser boostForegroundColor;
    private int boostBackgroundColorPosition;
    private ColorChooser boostBackgroundColor;
    private Label[] elementTextLabel;
    private PictureBox[] elementInfoPictureBox;
    private SmartToolTip[] elementHelpText;
    private Label[] elementUnitLabel;
    private NumericUpDown[] elementNumericUpDown;
    private PictureBox[] elementWarningPictureBox;
    private SmartToolTip[] elementWarningText;
    private readonly int PositionOfRowLabels;
    private readonly int PositionOfRowUnits = 3;
    private readonly int PositionOfRowValus = 4;
    private Decimal brightnessLevelOldValue;
    private Decimal lightSensorStepOldValue;
    private Decimal recuperationLevelOldValue;
    private Decimal supportLevelOldValue;
    private Decimal maxAssistanceSpeedOldValue;
    private Decimal wheelCircumferenceOldValue;
    private Decimal forgroundColorOldValue;
    private Decimal pushAssitantOldValue;
    private IContainer components;
    private FlickerFreeTableLayoutPanel tableLayoutPanel;

    public event ParameterPanel.ValueChangedEventHandler ValueChanged;

    private static bool InDesignMode() => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    public ParameterPanel()
      : this(false)
    {
    }

    public ParameterPanel(bool ignoreVersionLevel2)
    {
      this.args = new ParameterPanelEventArgs();
      this.ValueWasChanged = false;
      if (!ParameterPanel.InDesignMode())
        Thread.CurrentThread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      if (ParameterPanel.InDesignMode())
        return;
      this.accessLevel = ActivationInformation.VersionLevelProperty;
      if (ignoreVersionLevel2 && this.accessLevel < 3)
        this.accessLevel = 3;
      this.parameterResources = new ComponentResourceManager(typeof (ParameterPanel));
      this.allParameters = Parameters.Instance.Dictionary;
      this.dictionaryElements = Parameters.Instance.DictionaryElements;
      // ISSUE: reference to a compiler-generated method
      this.positionOfValueInDictionary = Parameters.Instance.ValueInDictionaryList();
      this.allPictures = Pictures.Instance.AllPictures;
      this.pictureElements = Pictures.Instance.NumberOfPictures;
      this.mmiDefaultsPosition = -1;
      this.mmiDefaults = (MMIDefaults) null;
      this.elementTextLabel = new Label[this.dictionaryElements];
      this.elementInfoPictureBox = new PictureBox[this.dictionaryElements];
      this.elementHelpText = new SmartToolTip[this.dictionaryElements];
      this.elementUnitLabel = new Label[this.dictionaryElements];
      this.elementNumericUpDown = new NumericUpDown[this.dictionaryElements];
      this.elementWarningPictureBox = new PictureBox[this.dictionaryElements];
      this.elementWarningText = new SmartToolTip[this.dictionaryElements];
      this.stateDetailButton = new Button[6];
      this.SuspendLayout();
      this.tableLayoutPanel.SuspendLayout();
      this.drawDictionaryElements();
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
      this.EnabledChanged += new EventHandler(this.ParameterPanel_EnabledChanged);
      this.EnableAllElements();
      MouseWheelRedirector.Attach((Control) this);
      MouseWheelRedirector.Attach((Control) this.tableLayoutPanel);
      MouseWheelRedirector.Attach((Control[]) this.elementTextLabel);
      MouseWheelRedirector.Attach((Control[]) this.elementUnitLabel);
      MouseWheelRedirector.Attach((Control[]) this.elementWarningPictureBox);
      MouseWheelRedirector.Attach((Control[]) this.elementInfoPictureBox);
    }

    private void drawDictionaryElements()
    {
      int row1 = 0;
      int num1 = 0;
      string str1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString();
      this.tableLayoutPanel.RowStyles.Clear();
      this.tableLayoutPanel.ColumnStyles.Clear();
      this.tableLayoutPanel.RowCount = this.dictionaryElements;
      this.tableLayoutPanel.ColumnCount = 6;
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
      this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
      foreach (KeyValuePair<int, ParameterValue> allParameter in this.allParameters)
      {
        int num2 = 0;
        Label label1 = new Label();
        string str2 = string.Format("{0:00}", (object) (allParameter.Key + 1));
        this.elementTextLabel[row1] = label1;
        this.elementTextLabel[row1].Text = "#" + str2 + ": " + allParameter.Value.Label;
        this.elementTextLabel[row1].Name = "#" + str2 + ": " + allParameter.Value.EnLabel;
        this.elementTextLabel[row1].TextAlign = ContentAlignment.MiddleLeft;
        this.elementTextLabel[row1].Font = FontDefinition.DefaultTextFont;
        this.elementTextLabel[row1].ForeColor = ColorDefinition.BlackTextColor;
        this.elementTextLabel[row1].AutoSize = true;
        this.elementTextLabel[row1].Anchor = AnchorStyles.Left;
        Label label2 = new Label();
        this.elementUnitLabel[row1] = label2;
        if (allParameter.Value.UnitLabel != "none")
        {
          this.elementUnitLabel[row1].Text = allParameter.Value.UnitLabel;
          this.elementUnitLabel[row1].TextAlign = ContentAlignment.MiddleRight;
        }
        this.elementUnitLabel[row1].Font = FontDefinition.DefaultTextFont;
        this.elementUnitLabel[row1].ForeColor = ColorDefinition.BlackTextColor;
        this.elementUnitLabel[row1].AutoSize = true;
        this.elementUnitLabel[row1].Anchor = AnchorStyles.Right;
        PictureBox pictureBox1 = new PictureBox();
        this.elementInfoPictureBox[row1] = pictureBox1;
        this.elementInfoPictureBox[row1].Anchor = AnchorStyles.None;
        this.elementInfoPictureBox[row1].Size = new Size(20, 20);
        if (allParameter.Value.HelpText != "none")
        {
          this.elementInfoPictureBox[row1].Image = (Image) Resources.buttons_information;
          this.elementInfoPictureBox[row1].Cursor = Cursors.Hand;
          this.elementInfoPictureBox[row1].Name = allParameter.Value.Label.Replace(' ', '_');
          this.elementInfoPictureBox[row1].Click += new EventHandler(this.infoPictureBox_Click);
          SmartToolTip smartToolTip = new SmartToolTip();
          this.elementHelpText[row1] = smartToolTip;
          this.elementHelpText[row1].ShowAlways = true;
          this.elementHelpText[row1].ToolTipIcon = ToolTipIcon.Info;
          this.elementHelpText[row1].ToolTipTitle = GlobalResource.ToolTip_Title_Information;
          this.elementHelpText[row1].SetToolTip((Control) this.elementInfoPictureBox[row1], allParameter.Value.HelpText);
        }
        PictureBox pictureBox2 = new PictureBox();
        this.elementWarningPictureBox[row1] = pictureBox2;
        this.elementWarningPictureBox[row1].Size = new Size(20, 20);
        this.elementWarningPictureBox[row1].Anchor = AnchorStyles.None;
        if (allParameter.Value.WarningText != "none" && !allParameter.Value.IsReadOnly(this.accessLevel))
        {
          this.elementWarningPictureBox[row1].Image = (Image) Resources.buttons_attention;
          this.elementWarningPictureBox[row1].Cursor = Cursors.Hand;
          this.elementWarningPictureBox[row1].Name = "warning_" + allParameter.Value.Label;
          this.elementWarningPictureBox[row1].Click += new EventHandler(this.warningPictureBox_Click);
          SmartToolTip smartToolTip = new SmartToolTip();
          this.elementWarningText[row1] = smartToolTip;
          this.elementWarningText[row1].ShowAlways = true;
          this.elementWarningText[row1].ToolTipIcon = ToolTipIcon.Warning;
          this.elementWarningText[row1].ToolTipTitle = GlobalResource.ToolTip_Title_Warning;
          this.elementWarningText[row1].SetToolTip((Control) this.elementWarningPictureBox[row1], allParameter.Value.WarningText);
        }
        NumericUpDown numericUpDown1 = new NumericUpDown();
        this.elementNumericUpDown[row1] = numericUpDown1;
        this.elementNumericUpDown[row1].Font = FontDefinition.DefaultTextFont;
        this.elementNumericUpDown[row1].ForeColor = ColorDefinition.BlackTextColor;
        this.elementNumericUpDown[row1].Minimum = (Decimal) allParameter.Value.ReadableMinimumValue;
        this.elementNumericUpDown[row1].Maximum = (Decimal) allParameter.Value.ReadableMaximumValue;
        this.elementNumericUpDown[row1].Increment = (Decimal) allParameter.Value.StepSize;
        try
        {
          this.elementNumericUpDown[row1].Value = (Decimal) allParameter.Value.ReadableValue;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          this.elementNumericUpDown[row1].Value = (Decimal) allParameter.Value.ReadableDefaultValue;
          allParameter.Value.ReadableValue = allParameter.Value.ReadableDefaultValue;
        }
        this.elementNumericUpDown[row1].ReadOnly = allParameter.Value.IsReadOnly(this.accessLevel);
        this.elementNumericUpDown[row1].Enabled = !allParameter.Value.IsReadOnly(this.accessLevel);
        this.elementNumericUpDown[row1].ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
        this.elementNumericUpDown[row1].Leave += new EventHandler(this.numericUpDown_Leave);
        this.elementNumericUpDown[row1].Anchor = AnchorStyles.Left;
        string str3 = Convert.ToString(allParameter.Value.StepSize);
        int num3 = str3.IndexOf(str1);
        int num4 = num3 <= 0 ? 0 : str3.Length - num3 - 1;
        this.elementNumericUpDown[row1].DecimalPlaces = num4;
        this.elementNumericUpDown[row1].Name = string.Concat((object) row1);
        Control specialElement = (Control) null;
        bool flag = this.SpecialParameterHandling(allParameter, row1, ref specialElement);
        if (allParameter.Value.IsVisible(this.accessLevel))
        {
          this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
          int num5;
          if (flag)
          {
            if (specialElement != null)
            {
              TableLayoutControlCollection controls1 = this.tableLayoutPanel.Controls;
              Label label3 = this.elementTextLabel[row1];
              int column1 = num2;
              int num6 = column1 + 1;
              int row2 = row1;
              controls1.Add((Control) label3, column1, row2);
              TableLayoutControlCollection controls2 = this.tableLayoutPanel.Controls;
              PictureBox pictureBox3 = this.elementWarningPictureBox[row1];
              int column2 = num6;
              int num7 = column2 + 1;
              int row3 = row1;
              controls2.Add((Control) pictureBox3, column2, row3);
              TableLayoutControlCollection controls3 = this.tableLayoutPanel.Controls;
              PictureBox pictureBox4 = this.elementInfoPictureBox[row1];
              int column3 = num7;
              int num8 = column3 + 1;
              int row4 = row1;
              controls3.Add((Control) pictureBox4, column3, row4);
              TableLayoutControlCollection controls4 = this.tableLayoutPanel.Controls;
              Label label4 = this.elementUnitLabel[row1];
              int column4 = num8;
              int num9 = column4 + 1;
              int row5 = row1;
              controls4.Add((Control) label4, column4, row5);
              TableLayoutControlCollection controls5 = this.tableLayoutPanel.Controls;
              Control control = specialElement;
              int column5 = num9;
              num5 = column5 + 1;
              int row6 = row1;
              controls5.Add(control, column5, row6);
              ++num1;
            }
            this.elementNumericUpDown[row1].Visible = false;
          }
          else
          {
            TableLayoutControlCollection controls1 = this.tableLayoutPanel.Controls;
            Label label3 = this.elementTextLabel[row1];
            int column1 = num2;
            int num6 = column1 + 1;
            int row2 = row1;
            controls1.Add((Control) label3, column1, row2);
            TableLayoutControlCollection controls2 = this.tableLayoutPanel.Controls;
            PictureBox pictureBox3 = this.elementWarningPictureBox[row1];
            int column2 = num6;
            int num7 = column2 + 1;
            int row3 = row1;
            controls2.Add((Control) pictureBox3, column2, row3);
            TableLayoutControlCollection controls3 = this.tableLayoutPanel.Controls;
            PictureBox pictureBox4 = this.elementInfoPictureBox[row1];
            int column3 = num7;
            int num8 = column3 + 1;
            int row4 = row1;
            controls3.Add((Control) pictureBox4, column3, row4);
            TableLayoutControlCollection controls4 = this.tableLayoutPanel.Controls;
            Label label4 = this.elementUnitLabel[row1];
            int column4 = num8;
            int num9 = column4 + 1;
            int row5 = row1;
            controls4.Add((Control) label4, column4, row5);
            TableLayoutControlCollection controls5 = this.tableLayoutPanel.Controls;
            NumericUpDown numericUpDown2 = this.elementNumericUpDown[row1];
            int column5 = num9;
            num5 = column5 + 1;
            int row6 = row1;
            controls5.Add((Control) numericUpDown2, column5, row6);
            ++num1;
          }
        }
        else
        {
          this.elementTextLabel[row1].Visible = false;
          this.elementWarningPictureBox[row1].Visible = false;
          this.elementInfoPictureBox[row1].Visible = false;
          this.elementUnitLabel[row1].Visible = false;
          this.elementNumericUpDown[row1].Visible = false;
        }
        ++row1;
      }
    }

    private void numericUpDown_Leave(object sender, EventArgs e)
    {
      NumericUpDown numericUpDown = (NumericUpDown) sender;
      if (!string.IsNullOrEmpty(numericUpDown.Text))
        return;
      Decimal num = numericUpDown.Value;
      numericUpDown.Value = !(numericUpDown.Value == numericUpDown.Maximum) ? (!(numericUpDown.Value == numericUpDown.Minimum) ? numericUpDown.Minimum : numericUpDown.Maximum) : numericUpDown.Minimum;
      numericUpDown.Value = num;
    }

    private bool SpecialParameterHandling(
      KeyValuePair<int, ParameterValue> parameter,
      int row,
      ref Control specialElement)
    {
      bool flag = false;
      if (parameter.Key == this.positionOfValueInDictionary[1])
      {
        this.pushAssistant = new NumericUpDownWithActivation();
        this.pushAssistant.BeginInit();
        this.pushAssistant.Anchor = AnchorStyles.Left;
        this.pushAssistant.ValueChanged += new NumericUpDownWithActivation.ValueChangedEventHandler(this.pushAssistant_ValueChanged);
        this.pushAssistantPosition = row;
        this.pushAssistant.Font = FontDefinition.DefaultTextFont;
        this.pushAssistant.ForeColor = ColorDefinition.BlackTextColor;
        this.pushAssistant.CheckOffset = 128;
        this.pushAssistant.Increment = 0.5M;
        this.pushAssistant.Minimum = (Decimal) parameter.Value.ReadableMinimumValue;
        this.pushAssistant.Maximum = (Decimal) parameter.Value.ReadableMaximumValue;
        try
        {
          this.pushAssistant.Value = (Decimal) parameter.Value.ReadableValue;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          this.pushAssistant.Value = (Decimal) parameter.Value.ReadableDefaultValue;
          parameter.Value.ReadableValue = parameter.Value.ReadableDefaultValue;
        }
        if (this.accessLevel == 2)
        {
          try
          {
            this.pushAssistant.Maximum = (Decimal) this.pushAssistanCriticalValue;
          }
          catch (ArgumentOutOfRangeException ex)
          {
            this.pushAssistant.Value = (Decimal) this.pushAssistanCriticalValue;
            this.pushAssistant.Maximum = (Decimal) this.pushAssistanCriticalValue;
          }
        }
        this.pushAssistant.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.pushAssistant.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        string str1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString();
        string str2 = Convert.ToString(parameter.Value.StepSize);
        int num = str2.IndexOf(str1);
        this.pushAssistant.DecimalPlaces = num <= 0 ? 0 : str2.Length - num - 1;
        this.pushAssistant.UpDownSize = new Size(this.elementNumericUpDown[0].Size.Width, this.pushAssistant.Height);
        this.pushAssistant.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.pushAssistant.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.pushAssistant.EnterEvent += new NumericUpDownWithActivation.EnterEventHandler(this.pushAssistant_EnterEvent);
        this.pushAssistant.LeaveEvent += new NumericUpDownWithActivation.LeaveEventHandler(this.pushAssistant_LeaveEvent);
        this.pushAssistant.EndInit();
        flag = true;
        specialElement = (Control) this.pushAssistant;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[2])
      {
        this.pushBackAssistant = new NumericUpDownWithActivation();
        this.pushBackAssistant.BeginInit();
        this.pushBackAssistant.Anchor = AnchorStyles.Left;
        this.pushBackAssistant.ValueChanged += new NumericUpDownWithActivation.ValueChangedEventHandler(this.pushBackAssistant_ValueChanged);
        this.pushBackAssistantPosition = row;
        this.pushBackAssistant.Font = FontDefinition.DefaultTextFont;
        this.pushBackAssistant.ForeColor = ColorDefinition.BlackTextColor;
        this.pushBackAssistant.CheckOffset = -64;
        this.pushBackAssistant.Increment = 0.5M;
        this.pushBackAssistant.Minimum = (Decimal) parameter.Value.ReadableMinimumValue;
        this.pushBackAssistant.Maximum = (Decimal) parameter.Value.ReadableMaximumValue;
        try
        {
          this.pushBackAssistant.Value = (Decimal) parameter.Value.ReadableValue;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          this.pushBackAssistant.Value = (Decimal) parameter.Value.ReadableDefaultValue;
          parameter.Value.ReadableValue = parameter.Value.ReadableDefaultValue;
        }
        this.pushBackAssistant.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.pushBackAssistant.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        string str1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString();
        string str2 = Convert.ToString(parameter.Value.StepSize);
        int num = str2.IndexOf(str1);
        this.pushBackAssistant.DecimalPlaces = num <= 0 ? 0 : str2.Length - num - 1;
        this.pushBackAssistant.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.pushBackAssistant.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.pushBackAssistant.UpDownSize = new Size(this.elementNumericUpDown[0].Size.Width, this.pushAssistant.Height);
        this.pushBackAssistant.EndInit();
        flag = true;
        specialElement = (Control) this.pushBackAssistant;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[79])
      {
        this.wheelCircumference = new NumericUpDownWithConversion();
        this.wheelCircumference.BeginInit();
        this.wheelCircumference.Anchor = AnchorStyles.Left;
        this.wheelCircumference.ConversionUnitText = HelperClass.InchAbbr;
        this.wheelCircumference.ConversionFactor = HelperClass.CentimetersToInchFactor / 10.0;
        this.wheelCircumference.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.wheelCircumference_ValueChanged);
        this.wheelCircumferencePosition = row;
        this.wheelCircumference.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.wheelCircumference.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.wheelCircumference.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.wheelCircumference.Font = this.elementNumericUpDown[row].Font;
        this.wheelCircumference.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.wheelCircumference.Value = this.elementNumericUpDown[row].Value;
        this.wheelCircumference.Minimum = this.elementNumericUpDown[row].Minimum;
        this.wheelCircumference.Maximum = this.elementNumericUpDown[row].Maximum;
        this.wheelCircumference.Increment = this.elementNumericUpDown[row].Increment;
        this.wheelCircumference.UpDownSize = this.elementNumericUpDown[0].Size;
        this.wheelCircumference.EnterEvent += new NumericUpDownWithConversion.EnterEventHandler(this.wheelCircumference_Enter);
        this.wheelCircumference.LeaveEvent += new NumericUpDownWithConversion.LeaveEventHandler(this.wheelCircumference_Leave);
        this.wheelCircumference.EndInit();
        flag = true;
        specialElement = (Control) this.wheelCircumference;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[15])
      {
        this.maxAssistanceSpeed = new NumericUpDownWithConversion();
        this.maxAssistanceSpeed.BeginInit();
        this.maxAssistanceSpeed.Anchor = AnchorStyles.Left;
        this.maxAssistanceSpeed.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.maxAssistanceSpeed.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.maxAssistanceSpeed.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.maxAssistanceSpeed_ValueChanged);
        this.maxAssistanceSpeedPosition = row;
        this.maxAssistanceSpeed.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.maxAssistanceSpeed.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.maxAssistanceSpeed.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.maxAssistanceSpeed.Font = this.elementNumericUpDown[row].Font;
        this.maxAssistanceSpeed.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.maxAssistanceSpeed.Value = this.elementNumericUpDown[row].Value;
        this.maxAssistanceSpeed.Minimum = this.elementNumericUpDown[row].Minimum;
        this.maxAssistanceSpeed.Maximum = this.elementNumericUpDown[row].Maximum;
        this.maxAssistanceSpeed.Increment = this.elementNumericUpDown[row].Increment;
        this.maxAssistanceSpeed.EnterEvent += new NumericUpDownWithConversion.EnterEventHandler(this.maxAssistanceSpeed_Enter);
        this.maxAssistanceSpeed.LeaveEvent += new NumericUpDownWithConversion.LeaveEventHandler(this.maxAssistanceSpeed_Leave);
        this.maxAssistanceSpeed.UpDownSize = this.elementNumericUpDown[0].Size;
        this.maxAssistanceSpeed.EndInit();
        flag = true;
        specialElement = (Control) this.maxAssistanceSpeed;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[4] || parameter.Key == this.positionOfValueInDictionary[5] || (parameter.Key == this.positionOfValueInDictionary[6] || parameter.Key == this.positionOfValueInDictionary[7]) || parameter.Key == this.positionOfValueInDictionary[8])
      {
        if (parameter.Key == this.positionOfValueInDictionary[4])
          this.supportLevelOnePosition = row;
        this.elementNumericUpDown[row].Enter += new EventHandler(this.supportLevel_Enter);
        this.elementNumericUpDown[row].Leave += new EventHandler(this.supportLevel_Leave);
      }
      else if (parameter.Key == this.positionOfValueInDictionary[9] || parameter.Key == this.positionOfValueInDictionary[10] || parameter.Key == this.positionOfValueInDictionary[11])
      {
        if (parameter.Key == this.positionOfValueInDictionary[9])
          this.recuperationLevelOnePosition = row;
        this.elementNumericUpDown[row].Enter += new EventHandler(this.recuperationLevel_Enter);
        this.elementNumericUpDown[row].Leave += new EventHandler(this.recuperationLevel_Leave);
      }
      else if (parameter.Key == this.positionOfValueInDictionary[31] || parameter.Key == this.positionOfValueInDictionary[32] || (parameter.Key == this.positionOfValueInDictionary[33] || parameter.Key == this.positionOfValueInDictionary[34]) || (parameter.Key == this.positionOfValueInDictionary[35] || parameter.Key == this.positionOfValueInDictionary[36] || (parameter.Key == this.positionOfValueInDictionary[37] || parameter.Key == this.positionOfValueInDictionary[38])) || (parameter.Key == this.positionOfValueInDictionary[39] || parameter.Key == this.positionOfValueInDictionary[40]))
      {
        if (parameter.Key == this.positionOfValueInDictionary[31])
          this.lightSensorStepOnePosition = row;
        this.elementNumericUpDown[row].Enter += new EventHandler(this.lightSensorStep_Enter);
        this.elementNumericUpDown[row].Leave += new EventHandler(this.lightSensorStep_Leave);
      }
      else if (parameter.Key == this.positionOfValueInDictionary[41] || parameter.Key == this.positionOfValueInDictionary[42] || (parameter.Key == this.positionOfValueInDictionary[43] || parameter.Key == this.positionOfValueInDictionary[44]) || (parameter.Key == this.positionOfValueInDictionary[45] || parameter.Key == this.positionOfValueInDictionary[46] || (parameter.Key == this.positionOfValueInDictionary[47] || parameter.Key == this.positionOfValueInDictionary[48])) || (parameter.Key == this.positionOfValueInDictionary[49] || parameter.Key == this.positionOfValueInDictionary[50]))
      {
        if (parameter.Key == this.positionOfValueInDictionary[41])
          this.brightnessLevelOnePosition = row;
        this.elementNumericUpDown[row].Enter += new EventHandler(this.brightnessLevel_Enter);
        this.elementNumericUpDown[row].Leave += new EventHandler(this.brightnessLevel_Leave);
      }
      else if (parameter.Key == this.positionOfValueInDictionary[12])
      {
        this.speedLimiter = new NumericUpDownWithActivation();
        this.speedLimiter.BeginInit();
        this.speedLimiter.Anchor = AnchorStyles.Left;
        this.speedLimiter.ValueChanged += new NumericUpDownWithActivation.ValueChangedEventHandler(this.speedLimiter_ValueChanged);
        this.speedLimiterPosition = row;
        this.speedLimiter.Font = FontDefinition.DefaultTextFont;
        this.speedLimiter.ForeColor = ColorDefinition.BlackTextColor;
        this.speedLimiter.CheckOffset = 128;
        this.speedLimiter.Increment = (Decimal) parameter.Value.StepSize;
        this.speedLimiter.Minimum = (Decimal) parameter.Value.ReadableMinimumValue;
        this.speedLimiter.Maximum = (Decimal) parameter.Value.ReadableMaximumValue;
        try
        {
          this.speedLimiter.Value = (Decimal) parameter.Value.ReadableValue;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          this.speedLimiter.Value = (Decimal) parameter.Value.ReadableDefaultValue;
          parameter.Value.ReadableValue = parameter.Value.ReadableDefaultValue;
        }
        this.speedLimiter.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.speedLimiter.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        string str1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString();
        string str2 = Convert.ToString(parameter.Value.StepSize);
        int num = str2.IndexOf(str1);
        this.speedLimiter.DecimalPlaces = num <= 0 ? 0 : str2.Length - num - 1;
        this.speedLimiter.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.speedLimiter.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.speedLimiter.UpDownSize = new Size(this.elementNumericUpDown[0].Size.Width, this.speedLimiter.Height);
        this.speedLimiter.EndInit();
        flag = true;
        specialElement = (Control) this.speedLimiter;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[17])
      {
        this.mmiDefaults = new MMIDefaults();
        this.mmiDefaults.Anchor = AnchorStyles.Left;
        this.mmiDefaults.ValueChanged += new MMIDefaults.ValueChangedEventHandler(this.mmiDefaults_ValueChanged);
        this.mmiDefaultsPosition = row;
        this.mmiDefaults.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.mmiDefaults;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[18])
      {
        this.stateDetailButton[0] = new Button();
        this.stateDetailButton[0].BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        this.stateDetailButton[0].BackgroundImageLayout = ImageLayout.Stretch;
        this.stateDetailButton[0].Font = FontDefinition.DefaultTextFont;
        this.stateDetailButton[0].Anchor = AnchorStyles.Left;
        this.stateDetailButton[0].Name = string.Concat((object) row);
        this.stateDetailButton[0].Text = GlobalResource.ParameterPanel_State_Details;
        this.stateDetailButton[0].Click += new EventHandler(this.stateDetailButton_Click);
        this.mmiSystemStatePosition = row;
        flag = true;
        specialElement = (Control) this.stateDetailButton[0];
      }
      else if (parameter.Key == this.positionOfValueInDictionary[19])
      {
        this.stateDetailButton[1] = new Button();
        this.stateDetailButton[1].BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        this.stateDetailButton[1].BackgroundImageLayout = ImageLayout.Stretch;
        this.stateDetailButton[1].Font = FontDefinition.DefaultTextFont;
        this.stateDetailButton[1].Anchor = AnchorStyles.Left;
        this.stateDetailButton[1].Name = string.Concat((object) row);
        this.stateDetailButton[1].Text = GlobalResource.ParameterPanel_State_Details;
        this.stateDetailButton[1].Click += new EventHandler(this.stateDetailButton_Click);
        this.driveControlStatePosition = row;
        flag = true;
        specialElement = (Control) this.stateDetailButton[1];
      }
      else if (parameter.Key == this.positionOfValueInDictionary[30])
      {
        this.stateDetailButton[2] = new Button();
        this.stateDetailButton[2].BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        this.stateDetailButton[2].BackgroundImageLayout = ImageLayout.Stretch;
        this.stateDetailButton[2].Font = FontDefinition.DefaultTextFont;
        this.stateDetailButton[2].Anchor = AnchorStyles.Left;
        this.stateDetailButton[2].Name = string.Concat((object) row);
        this.stateDetailButton[2].Text = GlobalResource.ParameterPanel_State_Details;
        this.stateDetailButton[2].Click += new EventHandler(this.stateDetailButton_Click);
        this.buttonStatePosition = row;
        flag = true;
        specialElement = (Control) this.stateDetailButton[2];
      }
      else if (parameter.Key == this.positionOfValueInDictionary[106])
      {
        this.stateDetailButton[3] = new Button();
        this.stateDetailButton[3].BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        this.stateDetailButton[3].BackgroundImageLayout = ImageLayout.Stretch;
        this.stateDetailButton[3].Font = FontDefinition.DefaultTextFont;
        this.stateDetailButton[3].Anchor = AnchorStyles.Left;
        this.stateDetailButton[3].Name = string.Concat((object) row);
        this.stateDetailButton[3].Text = GlobalResource.ParameterPanel_State_Details;
        this.stateDetailButton[3].Click += new EventHandler(this.stateDetailButton_Click);
        this.motorClientStatePosition = row;
        flag = true;
        specialElement = (Control) this.stateDetailButton[3];
      }
      else if (parameter.Key == this.positionOfValueInDictionary[118])
      {
        this.stateDetailButton[4] = new Button();
        this.stateDetailButton[4].BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        this.stateDetailButton[4].BackgroundImageLayout = ImageLayout.Stretch;
        this.stateDetailButton[4].Font = FontDefinition.DefaultTextFont;
        this.stateDetailButton[4].Anchor = AnchorStyles.Left;
        this.stateDetailButton[4].Name = string.Concat((object) row);
        this.stateDetailButton[4].Text = GlobalResource.ParameterPanel_State_Details;
        this.stateDetailButton[4].Click += new EventHandler(this.stateDetailButton_Click);
        this.accuClientStatePosition = row;
        flag = true;
        specialElement = (Control) this.stateDetailButton[4];
      }
      else if (parameter.Key == this.positionOfValueInDictionary[20])
      {
        this.stateDetailButton[5] = new Button();
        this.stateDetailButton[5].BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
        this.stateDetailButton[5].BackgroundImageLayout = ImageLayout.Stretch;
        this.stateDetailButton[5].Font = FontDefinition.DefaultTextFont;
        this.stateDetailButton[5].Anchor = AnchorStyles.Left;
        this.stateDetailButton[5].Name = string.Concat((object) row);
        this.stateDetailButton[5].Text = GlobalResource.ParameterPanel_State_Details;
        this.stateDetailButton[5].Click += new EventHandler(this.stateDetailButton_Click);
        this.accuControlStatePosition = row;
        flag = true;
        specialElement = (Control) this.stateDetailButton[5];
      }
      else if (parameter.Key == this.positionOfValueInDictionary[22])
      {
        this.serviceInterval = new DateControlWithActivation();
        this.serviceInterval.Anchor = AnchorStyles.Left;
        this.serviceInterval.MinYear = (int) parameter.Value.ReadableMinimumValue;
        this.serviceInterval.VisibleMinYear = 2012;
        this.serviceInterval.MaxYear = (int) parameter.Value.ReadableMaximumValue;
        this.serviceInterval.DefaultYear = (int) parameter.Value.ReadableDefaultValue;
        this.serviceInterval.SpecialYear = 2000;
        this.serviceInterval.Size = this.elementNumericUpDown[0].Size;
        this.serviceInterval.ValueChanged += new DateControlWithActivation.ValueChangedEventHandler(this.serviceInterval_ValueChanged);
        this.serviceIntervalPosition = row;
        this.serviceInterval.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.serviceInterval;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[23])
      {
        this.serviceInterval.MinMonth = (int) parameter.Value.ReadableMinimumValue;
        this.serviceInterval.MaxMonth = (int) parameter.Value.ReadableMaximumValue;
        this.serviceInterval.DefaultMonth = (int) parameter.Value.ReadableDefaultValue;
        this.serviceInterval.SpecialMonth = 1;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[24])
      {
        this.serviceInterval.MinDay = (int) parameter.Value.ReadableMinimumValue;
        this.serviceInterval.MaxDay = (int) parameter.Value.ReadableMaximumValue;
        this.serviceInterval.DefaultDay = (int) parameter.Value.ReadableDefaultValue;
        this.serviceInterval.SpecialDay = 1;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[51])
      {
        this.actualDate = new DateControl();
        this.actualDate.DateCheck = true;
        this.actualDate.Anchor = AnchorStyles.Left;
        this.actualDate.MinYear = (int) parameter.Value.ReadableMinimumValue;
        this.actualDate.MaxYear = (int) parameter.Value.ReadableMaximumValue;
        this.actualDate.Size = this.elementNumericUpDown[0].Size;
        this.actualDate.ValueChanged += new DateControl.ValueChangedEventHandler(this.actualDate_ValueChanged);
        this.actualDatePosition = row;
        this.actualDate.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.actualDate;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[52])
      {
        this.actualDate.MinMonth = (int) parameter.Value.ReadableMinimumValue;
        this.actualDate.MaxMonth = (int) parameter.Value.ReadableMaximumValue;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[53])
      {
        this.actualDate.MinDay = (int) parameter.Value.ReadableMinimumValue;
        this.actualDate.MaxDay = (int) parameter.Value.ReadableMaximumValue;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[60])
      {
        this.firmwareUpdateDate = new DateControl();
        this.firmwareUpdateDate.Anchor = AnchorStyles.Left;
        this.firmwareUpdateDate.MinYear = (int) parameter.Value.ReadableMinimumValue;
        this.firmwareUpdateDate.MaxYear = (int) parameter.Value.ReadableMaximumValue;
        this.firmwareUpdateDate.Size = this.elementNumericUpDown[0].Size;
        this.firmwareUpdateDate.ValueChanged += new DateControl.ValueChangedEventHandler(this.firmwareUpdateDate_ValueChanged);
        this.firmwareUpdateDatePosition = row;
        this.firmwareUpdateDate.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.firmwareUpdateDate;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[61])
      {
        this.firmwareUpdateDate.MinMonth = (int) parameter.Value.ReadableMinimumValue;
        this.firmwareUpdateDate.MaxMonth = (int) parameter.Value.ReadableMaximumValue;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[62])
      {
        this.firmwareUpdateDate.MinDay = (int) parameter.Value.ReadableMinimumValue;
        this.firmwareUpdateDate.MaxDay = (int) parameter.Value.ReadableMaximumValue;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[76])
      {
        this.productionDate = new DateControl();
        this.productionDate.Anchor = AnchorStyles.Left;
        this.productionDate.MinYear = (int) parameter.Value.ReadableMinimumValue;
        this.productionDate.MaxYear = (int) parameter.Value.ReadableMaximumValue;
        this.productionDate.Size = this.elementNumericUpDown[0].Size;
        this.productionDate.ValueChanged += new DateControl.ValueChangedEventHandler(this.productionDate_ValueChanged);
        this.productionDatePosition = row;
        this.productionDate.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.productionDate;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[77])
      {
        this.productionDate.MinMonth = (int) parameter.Value.ReadableMinimumValue;
        this.productionDate.MaxMonth = (int) parameter.Value.ReadableMaximumValue;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[78])
      {
        this.productionDate.MinDay = (int) parameter.Value.ReadableMinimumValue;
        this.productionDate.MaxDay = (int) parameter.Value.ReadableMaximumValue;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[54])
      {
        this.actualTime = new TimeControl();
        this.actualTime.TimeCheck = true;
        this.actualTime.Anchor = AnchorStyles.Left;
        this.actualTime.Size = this.elementNumericUpDown[0].Size;
        this.actualTime.ValueChanged += new TimeControl.ValueChangedEventHandler(this.actualTime_ValueChanged);
        this.actualTimePosition = row;
        this.actualTime.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.actualTime;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[55])
        flag = true;
      else if (parameter.Key == this.positionOfValueInDictionary[56])
      {
        this.softwareVersionMMI = new SoftwareVersionMMI();
        this.softwareVersionMMI.Anchor = AnchorStyles.Left;
        this.softwareVersionMMI.VersionSeparator = true;
        this.softwareVersionMMI.MaxNumberOfDigits = 1;
        this.softwareVersionMMI.Size = this.elementNumericUpDown[0].Size;
        this.softwareVersionMMI.ValueChanged += new EventHandler(this.softwareVersionMMINumbericUpDown_ValueChanged);
        this.softwareVersionMMIPosition = row;
        this.softwareVersionMMI.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.softwareVersionMMI.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.softwareVersionMMI;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[57] || parameter.Key == this.positionOfValueInDictionary[58] || parameter.Key == this.positionOfValueInDictionary[59])
      {
        ++this.softwareVersionMMI.MaxNumberOfDigits;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[65])
      {
        this.serialnumberMMI = new SerialnumberNumbericUpDown();
        this.serialnumberMMI.Anchor = AnchorStyles.Left;
        this.serialnumberMMI.ThousandsSeparator = true;
        this.serialnumberMMI.MaxNumberOfDigits = 1;
        this.serialnumberMMI.Size = this.elementNumericUpDown[0].Size;
        this.serialnumberMMI.ValueChanged += new EventHandler(this.serialnumberMMINumbericUpDown_ValueChanged);
        this.serialnumberMMIPosition = row;
        this.serialnumberMMI.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.serialnumberMMI.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.serialnumberMMI;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[66] || parameter.Key == this.positionOfValueInDictionary[67] || (parameter.Key == this.positionOfValueInDictionary[68] || parameter.Key == this.positionOfValueInDictionary[69]) || (parameter.Key == this.positionOfValueInDictionary[70] || parameter.Key == this.positionOfValueInDictionary[71] || (parameter.Key == this.positionOfValueInDictionary[72] || parameter.Key == this.positionOfValueInDictionary[73])) || (parameter.Key == this.positionOfValueInDictionary[74] || parameter.Key == this.positionOfValueInDictionary[75]))
      {
        ++this.serialnumberMMI.MaxNumberOfDigits;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[93])
      {
        this.serialnumberMotor = new SerialnumberNumbericUpDown();
        this.serialnumberMotor.Anchor = AnchorStyles.Left;
        this.serialnumberMotor.ThousandsSeparator = true;
        this.serialnumberMotor.MaxNumberOfDigits = 1;
        this.serialnumberMotor.Size = this.elementNumericUpDown[0].Size;
        this.serialnumberMotor.ValueChanged += new EventHandler(this.serialnumberMotorNumbericUpDown_ValueChanged);
        this.serialnumberMotorPosition = row;
        this.serialnumberMotor.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.serialnumberMotor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.serialnumberMotor;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[94] || parameter.Key == this.positionOfValueInDictionary[95] || (parameter.Key == this.positionOfValueInDictionary[96] || parameter.Key == this.positionOfValueInDictionary[97]) || (parameter.Key == this.positionOfValueInDictionary[98] || parameter.Key == this.positionOfValueInDictionary[99] || (parameter.Key == this.positionOfValueInDictionary[100] || parameter.Key == this.positionOfValueInDictionary[101])) || (parameter.Key == this.positionOfValueInDictionary[102] || parameter.Key == this.positionOfValueInDictionary[103]))
      {
        ++this.serialnumberMotor.MaxNumberOfDigits;
        flag = true;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[119])
      {
        this.foregroundColor = new ColorChooser();
        this.foregroundColor.Anchor = AnchorStyles.Left;
        this.foregroundColor.ValueChanged += new ColorChooser.ValueChangedEventHandler(this.foreground_ValueChanged);
        this.foregroundColor.Enter += new EventHandler(this.foregroundColor_Enter);
        this.foregroundColorPosition = row;
        this.foregroundColor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.foregroundColor;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[120])
      {
        this.backgroundColor = new ColorChooser();
        this.backgroundColor.Anchor = AnchorStyles.Left;
        this.backgroundColor.ValueChanged += new ColorChooser.ValueChangedEventHandler(this.background_ValueChanged);
        this.backgroundColorPosition = row;
        this.backgroundColor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.backgroundColor;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[27])
      {
        this.dayTravelTime = new TimeSpanUpDown();
        this.dayTravelTime.Anchor = AnchorStyles.Left;
        this.dayTravelTime.ValueChanged += new TimeSpanUpDown.ValueChangedEventHandler(this.dayTravelTime_ValueChanged);
        this.dayTravelTimePosition = row;
        this.dayTravelTime.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.dayTravelTime.Minimum = (Decimal) parameter.Value.ReadableMinimumValue;
        this.dayTravelTime.Maximum = (Decimal) parameter.Value.ReadableMaximumValue;
        flag = true;
        specialElement = (Control) this.dayTravelTime;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[28])
      {
        this.totalTravelTime = new TimeSpanUpDown();
        this.totalTravelTime.Anchor = AnchorStyles.Left;
        this.totalTravelTime.ValueChanged += new TimeSpanUpDown.ValueChangedEventHandler(this.totalTravelTime_ValueChanged);
        this.totalTravelTimePosition = row;
        this.totalTravelTime.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.totalTravelTime.Minimum = (Decimal) parameter.Value.ReadableMinimumValue;
        this.totalTravelTime.Maximum = (Decimal) parameter.Value.ReadableMaximumValue;
        flag = true;
        specialElement = (Control) this.totalTravelTime;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[123])
      {
        this.brakeSwitch = new CheckBox();
        this.brakeSwitch.Anchor = AnchorStyles.Left;
        this.brakeSwitch.Text = GlobalResource.Parameter_Checking;
        this.brakeSwitch.CheckedChanged += new EventHandler(this.brakeSwitch_CheckedChanged);
        this.brakeSwitchPosition = row;
        this.brakeSwitch.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.brakeSwitch.Checked = parameter.Value.ReadableValue != 0.0;
        flag = true;
        specialElement = (Control) this.brakeSwitch;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[124])
      {
        this.easyDisplaySwitch = new CheckBox();
        this.easyDisplaySwitch.Anchor = AnchorStyles.Left;
        this.easyDisplaySwitch.Text = GlobalResource.Parameter_Checking;
        this.easyDisplaySwitch.CheckedChanged += new EventHandler(this.easyDisplaySwitch_CheckedChanged);
        this.easyDisplaySwitchPosition = row;
        this.easyDisplaySwitch.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.easyDisplaySwitch.Checked = parameter.Value.ReadableValue != 0.0;
        flag = true;
        specialElement = (Control) this.easyDisplaySwitch;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[122])
      {
        this.overtravelDistance = new NumericUpDownWithConversion();
        this.overtravelDistance.BeginInit();
        this.overtravelDistance.Anchor = AnchorStyles.Left;
        this.overtravelDistance.ConversionUnitText = HelperClass.InchAbbr;
        this.overtravelDistance.ConversionFactor = HelperClass.CentimetersToInchFactor;
        this.overtravelDistance.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.overtravelDistance_ValueChanged);
        this.overtravelDistancePosition = row;
        this.overtravelDistance.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.overtravelDistance.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.overtravelDistance.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.overtravelDistance.Font = this.elementNumericUpDown[row].Font;
        this.overtravelDistance.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.overtravelDistance.Value = this.elementNumericUpDown[row].Value;
        this.overtravelDistance.Minimum = this.elementNumericUpDown[row].Minimum;
        this.overtravelDistance.Maximum = this.elementNumericUpDown[row].Maximum;
        this.overtravelDistance.Increment = this.elementNumericUpDown[row].Increment;
        this.overtravelDistance.UpDownSize = this.elementNumericUpDown[0].Size;
        this.overtravelDistance.EndInit();
        flag = true;
        specialElement = (Control) this.overtravelDistance;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[88])
      {
        this.dailyDistance = new NumericUpDownWithConversion();
        this.dailyDistance.BeginInit();
        this.dailyDistance.Anchor = AnchorStyles.Left;
        this.dailyDistance.ConversionUnitText = HelperClass.MilesAbbr;
        this.dailyDistance.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.dailyDistance.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.dailyDistance_ValueChanged);
        this.dailyDistancePosition = row;
        this.dailyDistance.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.dailyDistance.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.dailyDistance.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.dailyDistance.Font = this.elementNumericUpDown[row].Font;
        this.dailyDistance.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.dailyDistance.Value = this.elementNumericUpDown[row].Value;
        this.dailyDistance.Minimum = this.elementNumericUpDown[row].Minimum;
        this.dailyDistance.Maximum = this.elementNumericUpDown[row].Maximum;
        this.dailyDistance.Increment = this.elementNumericUpDown[row].Increment;
        this.dailyDistance.UpDownSize = this.elementNumericUpDown[0].Size;
        this.dailyDistance.EndInit();
        flag = true;
        specialElement = (Control) this.dailyDistance;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[87])
      {
        this.totalDistance = new NumericUpDownWithConversion();
        this.totalDistance.BeginInit();
        this.totalDistance.Anchor = AnchorStyles.Left;
        this.totalDistance.ConversionUnitText = HelperClass.MilesAbbr;
        this.totalDistance.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.totalDistance.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.totalDistance_ValueChanged);
        this.totalDistancePosition = row;
        this.totalDistance.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.totalDistance.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.totalDistance.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.totalDistance.Font = this.elementNumericUpDown[row].Font;
        this.totalDistance.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.totalDistance.Value = this.elementNumericUpDown[row].Value;
        this.totalDistance.Minimum = this.elementNumericUpDown[row].Minimum;
        this.totalDistance.Maximum = this.elementNumericUpDown[row].Maximum;
        this.totalDistance.Increment = this.elementNumericUpDown[row].Increment;
        this.totalDistance.UpDownSize = this.elementNumericUpDown[0].Size;
        this.totalDistance.EndInit();
        flag = true;
        specialElement = (Control) this.totalDistance;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[86])
      {
        this.speedValue = new NumericUpDownWithConversion();
        this.speedValue.BeginInit();
        this.speedValue.Anchor = AnchorStyles.Left;
        this.speedValue.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.speedValue.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.speedValue.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.speedValue_ValueChanged);
        this.speedValuePosition = row;
        this.speedValue.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.speedValue.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.speedValue.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.speedValue.Font = this.elementNumericUpDown[row].Font;
        this.speedValue.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.speedValue.Value = this.elementNumericUpDown[row].Value;
        this.speedValue.Minimum = this.elementNumericUpDown[row].Minimum;
        this.speedValue.Maximum = this.elementNumericUpDown[row].Maximum;
        this.speedValue.Increment = this.elementNumericUpDown[row].Increment;
        this.speedValue.UpDownSize = this.elementNumericUpDown[0].Size;
        this.speedValue.EndInit();
        flag = true;
        specialElement = (Control) this.speedValue;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[64])
      {
        this.restOfRange = new NumericUpDownWithConversion();
        this.restOfRange.BeginInit();
        this.restOfRange.Anchor = AnchorStyles.Left;
        this.restOfRange.ConversionUnitText = HelperClass.MilesAbbr;
        this.restOfRange.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.restOfRange.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.restOfRange_ValueChanged);
        this.restOfRangePosition = row;
        this.restOfRange.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.restOfRange.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.restOfRange.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.restOfRange.Font = this.elementNumericUpDown[row].Font;
        this.restOfRange.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.restOfRange.Value = this.elementNumericUpDown[row].Value;
        this.restOfRange.Minimum = this.elementNumericUpDown[row].Minimum;
        this.restOfRange.Maximum = this.elementNumericUpDown[row].Maximum;
        this.restOfRange.Increment = this.elementNumericUpDown[row].Increment;
        this.restOfRange.UpDownSize = this.elementNumericUpDown[0].Size;
        this.restOfRange.EndInit();
        flag = true;
        specialElement = (Control) this.restOfRange;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[26])
      {
        this.maxSpeed = new NumericUpDownWithConversion();
        this.maxSpeed.BeginInit();
        this.maxSpeed.Anchor = AnchorStyles.Left;
        this.maxSpeed.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.maxSpeed.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.maxSpeed.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.maxSpeed_ValueChanged);
        this.maxSpeedPosition = row;
        this.maxSpeed.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.maxSpeed.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.maxSpeed.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.maxSpeed.Font = this.elementNumericUpDown[row].Font;
        this.maxSpeed.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.maxSpeed.Value = this.elementNumericUpDown[row].Value;
        this.maxSpeed.Minimum = this.elementNumericUpDown[row].Minimum;
        this.maxSpeed.Maximum = this.elementNumericUpDown[row].Maximum;
        this.maxSpeed.Increment = this.elementNumericUpDown[row].Increment;
        this.maxSpeed.UpDownSize = this.elementNumericUpDown[0].Size;
        this.maxSpeed.EndInit();
        flag = true;
        specialElement = (Control) this.maxSpeed;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[25])
      {
        this.averageSpeed = new NumericUpDownWithConversion();
        this.averageSpeed.BeginInit();
        this.averageSpeed.Anchor = AnchorStyles.Left;
        this.averageSpeed.ConversionUnitText = HelperClass.MilesPerHourAbbr;
        this.averageSpeed.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.averageSpeed.ValueChanged += new NumericUpDownWithConversion.ValueChangedEventHandler(this.averageSpeed_ValueChanged);
        this.averageSpeedPosition = row;
        this.averageSpeed.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.averageSpeed.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.averageSpeed.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.averageSpeed.Font = this.elementNumericUpDown[row].Font;
        this.averageSpeed.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.averageSpeed.Value = this.elementNumericUpDown[row].Value;
        this.averageSpeed.Minimum = this.elementNumericUpDown[row].Minimum;
        this.averageSpeed.Maximum = this.elementNumericUpDown[row].Maximum;
        this.averageSpeed.Increment = this.elementNumericUpDown[row].Increment;
        this.averageSpeed.UpDownSize = this.elementNumericUpDown[0].Size;
        this.averageSpeed.EndInit();
        flag = true;
        specialElement = (Control) this.averageSpeed;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[21])
      {
        this.nextServiceDistance = new NumericUpDownWithSpecialValue();
        this.nextServiceDistance.BeginInit();
        this.nextServiceDistance.Anchor = AnchorStyles.Left;
        this.nextServiceDistance.ConversionUnitText = HelperClass.MilesAbbr;
        this.nextServiceDistance.ConversionFactor = HelperClass.KilometersToMilesFactor;
        this.nextServiceDistance.ValueChanged += new NumericUpDownWithSpecialValue.ValueChangedEventHandler(this.nextServiceDistance_ValueChanged);
        this.nextServiceDistancePosition = row;
        this.nextServiceDistance.SpecialValue = 0M;
        this.nextServiceDistance.ReadOnly = parameter.Value.IsReadOnly(this.accessLevel);
        this.nextServiceDistance.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        this.nextServiceDistance.DecimalPlaces = this.elementNumericUpDown[row].DecimalPlaces;
        this.nextServiceDistance.Font = this.elementNumericUpDown[row].Font;
        this.nextServiceDistance.ForeColor = this.elementNumericUpDown[row].ForeColor;
        this.nextServiceDistance.Minimum = this.elementNumericUpDown[row].Minimum;
        this.nextServiceDistance.Maximum = this.elementNumericUpDown[row].Maximum;
        this.nextServiceDistance.Value = this.elementNumericUpDown[row].Value;
        this.nextServiceDistance.DefaultValue = (Decimal) parameter.Value.ReadableDefaultValue;
        this.nextServiceDistance.Increment = this.elementNumericUpDown[row].Increment;
        this.nextServiceDistance.UpDownSize = this.elementNumericUpDown[0].Size;
        this.nextServiceDistance.EndInit();
        flag = true;
        specialElement = (Control) this.nextServiceDistance;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[125])
      {
        this.ecoForegroundColor = new ColorChooser();
        this.ecoForegroundColor.Anchor = AnchorStyles.Left;
        this.ecoForegroundColor.ValueChanged += new ColorChooser.ValueChangedEventHandler(this.ecoForeground_ValueChanged);
        this.ecoForegroundColor.Enter += new EventHandler(this.foregroundColor_Enter);
        this.ecoForegroundColorPosition = row;
        this.ecoForegroundColor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.ecoForegroundColor;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[126])
      {
        this.ecoBackgroundColor = new ColorChooser();
        this.ecoBackgroundColor.Anchor = AnchorStyles.Left;
        this.ecoBackgroundColor.ValueChanged += new ColorChooser.ValueChangedEventHandler(this.ecoBackground_ValueChanged);
        this.ecoBackgroundColorPosition = row;
        this.ecoBackgroundColor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.ecoBackgroundColor;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[(int) sbyte.MaxValue])
      {
        this.boostForegroundColor = new ColorChooser();
        this.boostForegroundColor.Anchor = AnchorStyles.Left;
        this.boostForegroundColor.ValueChanged += new ColorChooser.ValueChangedEventHandler(this.boostForeground_ValueChanged);
        this.boostForegroundColor.Enter += new EventHandler(this.foregroundColor_Enter);
        this.boostForegroundColorPosition = row;
        this.boostForegroundColor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.boostForegroundColor;
      }
      else if (parameter.Key == this.positionOfValueInDictionary[128])
      {
        this.boostBackgroundColor = new ColorChooser();
        this.boostBackgroundColor.Anchor = AnchorStyles.Left;
        this.boostBackgroundColor.ValueChanged += new ColorChooser.ValueChangedEventHandler(this.boostBackground_ValueChanged);
        this.boostBackgroundColorPosition = row;
        this.boostBackgroundColor.Enabled = !parameter.Value.IsReadOnly(this.accessLevel);
        flag = true;
        specialElement = (Control) this.boostBackgroundColor;
      }
      return flag;
    }

    private void infoPictureBox_Click(object sender, EventArgs e)
    {
      ((Control) sender).Focus();
      string text = string.Empty;
      for (int index = 0; index < this.elementHelpText.Length; ++index)
      {
        if (this.elementHelpText[index] != null && !string.IsNullOrEmpty(this.elementHelpText[index].GetToolTip((Control) sender)))
          text = this.elementHelpText[index].GetToolTip((Control) sender);
      }
      int num = (int) MessageBox.Show(text, GlobalResource.ToolTip_Title_Information, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void warningPictureBox_Click(object sender, EventArgs e)
    {
      ((Control) sender).Focus();
      string text = string.Empty;
      for (int index = 0; index < this.elementWarningText.Length; ++index)
      {
        if (this.elementWarningText[index] != null && !string.IsNullOrEmpty(this.elementWarningText[index].GetToolTip((Control) sender)))
          text = this.elementWarningText[index].GetToolTip((Control) sender);
      }
      int num = (int) MessageBox.Show(text, GlobalResource.ToolTip_Title_Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    public void updateDictionaryWithAccessableUIValues()
    {
      for (int key = 0; key < this.dictionaryElements; ++key)
      {
        if (this.allParameters[key].IsVisible(this.accessLevel) && !this.allParameters[key].IsReadOnly(this.accessLevel) || ActivationInformation.IsAllAccessActive && this.accessLevel == 4)
          this.allParameters[key].ReadableValue = (double) this.elementNumericUpDown[key].Value;
      }
    }

    public bool ValuesValidForConnectedMMI()
    {
      bool flag = true;
      if (CommandBuilder.Instance.MCUVersion != MMIMCUVersion.CONNECT_MZ && (byte) ((uint) (byte) this.mmiDefaults.Value & 15U) >= (byte) 4)
        flag = false;
      if (!flag)
      {
        int num = (int) MessageBox.Show(GlobalResource.LanguageInvalid_ParameterPanel_Message, GlobalResource.LanguageInvalid_ParameterPanel_Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      return flag;
    }

    public void updateDictionaryWithUIValues()
    {
      for (int key = 0; key < this.dictionaryElements; ++key)
        this.allParameters[key].ReadableValue = (double) this.elementNumericUpDown[key].Value;
    }

    public void updateUIWithDictionaryValues()
    {
      this.serviceInterval.Reset = true;
      this.updateUIMethodIsNotWorking = false;
      for (int key = 0; key < this.dictionaryElements; ++key)
      {
        try
        {
          this.elementNumericUpDown[key].Value = (Decimal) this.allParameters[key].ReadableValue;
          this.numericUpDown_ValueChanged((object) this.elementNumericUpDown[key], EventArgs.Empty);
        }
        catch (Exception ex)
        {
          this.elementNumericUpDown[key].Value = (Decimal) this.allParameters[key].ReadableDefaultValue;
          this.numericUpDown_ValueChanged((object) this.elementNumericUpDown[key], EventArgs.Empty);
        }
      }
      this.sortBrightnessAndLightSensor();
      this.updateUIMethodIsNotWorking = true;
      this.serviceInterval.Reset = false;
      this.ValueWasChanged = false;
    }

    private void sortBrightnessAndLightSensor()
    {
      int length = 10;
      Decimal[] array1 = new Decimal[length];
      for (int index = 0; index < array1.Length; ++index)
        array1[index] = this.elementNumericUpDown[this.lightSensorStepOnePosition + index].Value;
      Array.Sort<Decimal>(array1);
      for (int index = 0; index < array1.Length; ++index)
        this.elementNumericUpDown[this.lightSensorStepOnePosition + index].Value = array1[index];
      Decimal[] array2 = new Decimal[length];
      for (int index = 0; index < array2.Length; ++index)
        array2[index] = this.elementNumericUpDown[this.brightnessLevelOnePosition + index].Value;
      Array.Sort<Decimal>(array2);
      for (int index = 0; index < array2.Length; ++index)
        this.elementNumericUpDown[this.brightnessLevelOnePosition + index].Value = array2[index];
    }

    private void brightnessLevel_Enter(object sender, EventArgs e) => this.brightnessLevelOldValue = ((NumericUpDown) sender).Value;

    private void brightnessLevel_Leave(object sender, EventArgs e)
    {
      if (!(((NumericUpDown) sender).Value != this.brightnessLevelOldValue))
        return;
      int length = 0;
      for (int index = 0; index < 10; ++index)
      {
        if (this.elementNumericUpDown[this.brightnessLevelOnePosition + index].Visible)
          ++length;
      }
      Decimal[] array = new Decimal[length];
      for (int index = 0; index < array.Length; ++index)
        array[index] = this.elementNumericUpDown[this.brightnessLevelOnePosition + index].Value;
      Array.Sort<Decimal>(array);
      for (int index = 0; index < array.Length; ++index)
        this.elementNumericUpDown[this.brightnessLevelOnePosition + index].Value = array[index];
    }

    private void lightSensorStep_Enter(object sender, EventArgs e) => this.lightSensorStepOldValue = ((NumericUpDown) sender).Value;

    private void lightSensorStep_Leave(object sender, EventArgs e)
    {
      if (!(((NumericUpDown) sender).Value != this.lightSensorStepOldValue))
        return;
      int length = 0;
      for (int index = 0; index < 10; ++index)
      {
        if (this.elementNumericUpDown[this.lightSensorStepOnePosition + index].Visible)
          ++length;
      }
      Decimal[] array = new Decimal[length];
      for (int index = 0; index < array.Length; ++index)
        array[index] = this.elementNumericUpDown[this.lightSensorStepOnePosition + index].Value;
      Array.Sort<Decimal>(array);
      for (int index = 0; index < array.Length; ++index)
        this.elementNumericUpDown[this.lightSensorStepOnePosition + index].Value = array[index];
    }

    private void recuperationLevel_Enter(object sender, EventArgs e) => this.recuperationLevelOldValue = ((NumericUpDown) sender).Value;

    private void recuperationLevel_Leave(object sender, EventArgs e)
    {
      if (!(((NumericUpDown) sender).Value != this.recuperationLevelOldValue))
        return;
      int length = 0;
      for (int index = 0; index <= 3; ++index)
      {
        if (this.elementNumericUpDown[this.recuperationLevelOnePosition + index].Visible)
          ++length;
      }
      Decimal[] array = new Decimal[length];
      for (int index = 0; index < array.Length; ++index)
        array[index] = this.elementNumericUpDown[this.recuperationLevelOnePosition + index].Value;
      Array.Sort<Decimal>(array);
      for (int index = 0; index < array.Length; ++index)
        this.elementNumericUpDown[this.recuperationLevelOnePosition + index].Value = array[index];
    }

    private void supportLevel_Enter(object sender, EventArgs e) => this.supportLevelOldValue = ((NumericUpDown) sender).Value;

    private void supportLevel_Leave(object sender, EventArgs e)
    {
      if (!(((NumericUpDown) sender).Value != this.supportLevelOldValue))
        return;
      Decimal[] array = new Decimal[5];
      for (int index = 0; index < array.Length; ++index)
        array[index] = this.elementNumericUpDown[this.supportLevelOnePosition + index].Value;
      Array.Sort<Decimal>(array);
      for (int index = 0; index < array.Length; ++index)
        this.elementNumericUpDown[this.supportLevelOnePosition + index].Value = array[index];
    }

    private void maxAssistanceSpeed_Enter(object sender, EventArgs e) => this.maxAssistanceSpeedOldValue = this.maxAssistanceSpeed.Value;

    private void maxAssistanceSpeed_Leave(object sender, EventArgs e)
    {
      if (!(this.maxAssistanceSpeed.Value != this.maxAssistanceSpeedOldValue) || MessageBox.Show(GlobalResource.ParameterMaxAssistanceSpeed_Message, GlobalResource.ParameterMaxAssistanceSpeed_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel)
        return;
      this.maxAssistanceSpeed.Value = this.maxAssistanceSpeedOldValue;
    }

    private void wheelCircumference_Enter(object sender, EventArgs e) => this.wheelCircumferenceOldValue = this.wheelCircumference.Value;

    private void wheelCircumference_Leave(object sender, EventArgs e)
    {
      if (!(this.wheelCircumference.Value != this.wheelCircumferenceOldValue) || MessageBox.Show(GlobalResource.ParameterWheelCircumference_Message, GlobalResource.ParameterWheelCircumference_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel)
        return;
      this.wheelCircumference.Value = this.wheelCircumferenceOldValue;
    }

    private void numericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.updateUIMethodIsNotWorking)
        this.ValueWasChanged = true;
      NumericUpDown numericUpDown = (NumericUpDown) sender;
      if (this.accessLevel == 2)
      {
        if (numericUpDown.Name == string.Concat((object) this.pushAssistantPosition))
        {
          try
          {
            numericUpDown.Value = (Decimal) ParameterValue.ApproximatedValue((double) numericUpDown.Value, (double) numericUpDown.Increment);
            this.pushAssistant.Value = numericUpDown.Value;
            goto label_7;
          }
          catch (ArgumentOutOfRangeException ex)
          {
            numericUpDown.Value = this.pushAssistant.Maximum;
            this.pushAssistant.Value = this.pushAssistant.Maximum;
            goto label_7;
          }
        }
      }
      numericUpDown.Value = (Decimal) ParameterValue.ApproximatedValue((double) numericUpDown.Value, (double) numericUpDown.Increment);
label_7:
      if (numericUpDown.Name == string.Concat((object) this.wheelCircumferencePosition))
        this.wheelCircumference.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.pushAssistantPosition))
        this.pushAssistant.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.pushBackAssistantPosition))
        this.pushBackAssistant.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.speedLimiterPosition))
        this.speedLimiter.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.mmiDefaultsPosition))
        this.mmiDefaults.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.serviceIntervalPosition))
        this.serviceInterval.ValueYear = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.serviceIntervalPosition + 1)))
        this.serviceInterval.ValueMonth = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.serviceIntervalPosition + 2)))
        this.serviceInterval.ValueDay = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.actualDatePosition))
        this.actualDate.ValueYear = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.actualDatePosition + 1)))
        this.actualDate.ValueMonth = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.actualDatePosition + 2)))
        this.actualDate.ValueDay = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.firmwareUpdateDatePosition))
        this.firmwareUpdateDate.ValueYear = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.firmwareUpdateDatePosition + 1)))
        this.firmwareUpdateDate.ValueMonth = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.firmwareUpdateDatePosition + 2)))
        this.firmwareUpdateDate.ValueDay = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.productionDatePosition))
        this.productionDate.ValueYear = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.productionDatePosition + 1)))
        this.productionDate.ValueMonth = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.productionDatePosition + 2)))
        this.productionDate.ValueDay = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.actualTimePosition))
        this.actualTime.Hour = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) (this.actualTimePosition + 1)))
        this.actualTime.Minute = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.serialnumberMMIPosition))
        this.serialnumberMMI.SetSerialAtPosition(0, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 1)))
        this.serialnumberMMI.SetSerialAtPosition(1, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 2)))
        this.serialnumberMMI.SetSerialAtPosition(2, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 3)))
        this.serialnumberMMI.SetSerialAtPosition(3, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 4)))
        this.serialnumberMMI.SetSerialAtPosition(4, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 5)))
        this.serialnumberMMI.SetSerialAtPosition(5, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 6)))
        this.serialnumberMMI.SetSerialAtPosition(6, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 7)))
        this.serialnumberMMI.SetSerialAtPosition(7, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 8)))
        this.serialnumberMMI.SetSerialAtPosition(8, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 9)))
        this.serialnumberMMI.SetSerialAtPosition(9, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMMIPosition + 10)))
        this.serialnumberMMI.SetSerialAtPosition(10, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) this.serialnumberMotorPosition))
        this.serialnumberMotor.SetSerialAtPosition(0, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 1)))
        this.serialnumberMotor.SetSerialAtPosition(1, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 2)))
        this.serialnumberMotor.SetSerialAtPosition(2, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 3)))
        this.serialnumberMotor.SetSerialAtPosition(3, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 4)))
        this.serialnumberMotor.SetSerialAtPosition(4, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 5)))
        this.serialnumberMotor.SetSerialAtPosition(5, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 6)))
        this.serialnumberMotor.SetSerialAtPosition(6, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 7)))
        this.serialnumberMotor.SetSerialAtPosition(7, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 8)))
        this.serialnumberMotor.SetSerialAtPosition(8, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 9)))
        this.serialnumberMotor.SetSerialAtPosition(9, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.serialnumberMotorPosition + 10)))
        this.serialnumberMotor.SetSerialAtPosition(10, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) this.softwareVersionMMIPosition))
        this.softwareVersionMMI.SetVersionAtPosition(0, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.softwareVersionMMIPosition + 1)))
        this.softwareVersionMMI.SetVersionAtPosition(1, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.softwareVersionMMIPosition + 2)))
        this.softwareVersionMMI.SetVersionAtPosition(2, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) (this.softwareVersionMMIPosition + 3)))
        this.softwareVersionMMI.SetVersionAtPosition(3, (int) numericUpDown.Value);
      else if (numericUpDown.Name == string.Concat((object) this.foregroundColorPosition))
        this.foregroundColor.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.backgroundColorPosition))
        this.backgroundColor.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.dayTravelTimePosition))
        this.dayTravelTime.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.totalTravelTimePosition))
        this.totalTravelTime.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.brakeSwitchPosition))
      {
        if (numericUpDown.Value == 0M)
          this.brakeSwitch.Checked = false;
        else
          this.brakeSwitch.Checked = true;
      }
      else if (numericUpDown.Name == string.Concat((object) this.easyDisplaySwitchPosition))
      {
        if (numericUpDown.Value == 0M)
          this.easyDisplaySwitch.Checked = false;
        else
          this.easyDisplaySwitch.Checked = true;
      }
      else if (numericUpDown.Name == string.Concat((object) this.ecoForegroundColorPosition))
        this.ecoForegroundColor.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.ecoBackgroundColorPosition))
        this.ecoBackgroundColor.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.boostForegroundColorPosition))
        this.boostForegroundColor.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.boostBackgroundColorPosition))
        this.boostBackgroundColor.Value = (int) numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.overtravelDistancePosition))
        this.overtravelDistance.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.dailyDistancePosition))
        this.dailyDistance.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.totalDistancePosition))
        this.totalDistance.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.speedValuePosition))
        this.speedValue.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.restOfRangePosition))
        this.restOfRange.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.maxSpeedPosition))
        this.maxSpeed.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.averageSpeedPosition))
        this.averageSpeed.Value = numericUpDown.Value;
      else if (numericUpDown.Name == string.Concat((object) this.nextServiceDistancePosition))
      {
        this.nextServiceDistance.Value = numericUpDown.Value;
      }
      else
      {
        if (!(numericUpDown.Name == string.Concat((object) this.maxAssistanceSpeedPosition)))
          return;
        this.maxAssistanceSpeed.Value = numericUpDown.Value;
      }
    }

    public bool ValueWasChanged
    {
      get => this.valueChanged;
      set
      {
        if (value != this.valueChanged)
        {
          this.args.ValueChanged = value;
          if (this.ValueChanged != null)
            this.ValueChanged((object) this, this.args);
        }
        this.valueChanged = value;
      }
    }

    private void mmiDefaults_ValueChanged(object sender, EventArgs e)
    {
      if (this.mmiDefaultsPosition <= 0)
        return;
      this.elementNumericUpDown[this.mmiDefaultsPosition].Value = (Decimal) this.mmiDefaults.Value;
    }

    private void serviceInterval_ValueChanged(object sender, EventArgs e)
    {
      if (this.serviceIntervalPosition <= 0)
        return;
      this.elementNumericUpDown[this.serviceIntervalPosition].Value = (Decimal) this.serviceInterval.ValueYear;
      this.elementNumericUpDown[this.serviceIntervalPosition + 1].Value = (Decimal) this.serviceInterval.ValueMonth;
      this.elementNumericUpDown[this.serviceIntervalPosition + 2].Value = (Decimal) this.serviceInterval.ValueDay;
    }

    private void actualDate_ValueChanged(object sender, EventArgs e)
    {
      if (this.actualDatePosition <= 0)
        return;
      this.elementNumericUpDown[this.actualDatePosition].Value = (Decimal) this.actualDate.ValueYear;
      this.elementNumericUpDown[this.actualDatePosition + 1].Value = (Decimal) this.actualDate.ValueMonth;
      this.elementNumericUpDown[this.actualDatePosition + 2].Value = (Decimal) this.actualDate.ValueDay;
    }

    private void firmwareUpdateDate_ValueChanged(object sender, EventArgs e)
    {
      if (this.actualDatePosition <= 0)
        return;
      this.elementNumericUpDown[this.firmwareUpdateDatePosition].Value = (Decimal) this.firmwareUpdateDate.ValueYear;
      this.elementNumericUpDown[this.firmwareUpdateDatePosition + 1].Value = (Decimal) this.firmwareUpdateDate.ValueMonth;
      this.elementNumericUpDown[this.firmwareUpdateDatePosition + 2].Value = (Decimal) this.firmwareUpdateDate.ValueDay;
    }

    private void productionDate_ValueChanged(object sender, EventArgs e)
    {
      if (this.actualDatePosition <= 0)
        return;
      this.elementNumericUpDown[this.productionDatePosition].Value = (Decimal) this.productionDate.ValueYear;
      this.elementNumericUpDown[this.productionDatePosition + 1].Value = (Decimal) this.productionDate.ValueMonth;
      this.elementNumericUpDown[this.productionDatePosition + 2].Value = (Decimal) this.productionDate.ValueDay;
    }

    private void actualTime_ValueChanged(object sender, EventArgs e)
    {
      if (this.actualDatePosition <= 0)
        return;
      this.elementNumericUpDown[this.actualTimePosition].Value = (Decimal) this.actualTime.Hour;
      this.elementNumericUpDown[this.actualTimePosition + 1].Value = (Decimal) this.actualTime.Minute;
    }

    private void serialnumberMMINumbericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.serialnumberMMIPosition <= 0)
        return;
      this.elementNumericUpDown[this.serialnumberMMIPosition].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(0);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 1].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(1);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 2].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(2);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 3].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(3);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 4].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(4);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 5].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(5);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 6].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(6);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 7].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(7);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 8].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(8);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 9].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(9);
      this.elementNumericUpDown[this.serialnumberMMIPosition + 10].Value = (Decimal) this.serialnumberMMI.GetSerialAtPosition(10);
    }

    private void serialnumberMotorNumbericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.serialnumberMMIPosition <= 0)
        return;
      this.elementNumericUpDown[this.serialnumberMotorPosition].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(0);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 1].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(1);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 2].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(2);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 3].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(3);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 4].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(4);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 5].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(5);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 6].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(6);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 7].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(7);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 8].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(8);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 9].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(9);
      this.elementNumericUpDown[this.serialnumberMotorPosition + 10].Value = (Decimal) this.serialnumberMotor.GetSerialAtPosition(10);
    }

    private void softwareVersionMMINumbericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.serialnumberMMIPosition <= 0)
        return;
      this.elementNumericUpDown[this.softwareVersionMMIPosition].Value = (Decimal) this.softwareVersionMMI.GetVersionAtPosition(0);
      this.elementNumericUpDown[this.softwareVersionMMIPosition + 1].Value = (Decimal) this.softwareVersionMMI.GetVersionAtPosition(1);
      this.elementNumericUpDown[this.softwareVersionMMIPosition + 2].Value = (Decimal) this.softwareVersionMMI.GetVersionAtPosition(2);
      this.elementNumericUpDown[this.softwareVersionMMIPosition + 3].Value = (Decimal) this.softwareVersionMMI.GetVersionAtPosition(3);
    }

    private void stateDetailButton_Click(object sender, EventArgs e)
    {
      Button button = (Button) sender;
      byte state = 0;
      ComponentResourceManager resource = (ComponentResourceManager) null;
      if (button.Name == string.Concat((object) this.mmiSystemStatePosition))
      {
        state = (byte) this.elementNumericUpDown[this.mmiSystemStatePosition].Value;
        resource = new ComponentResourceManager(typeof (MMISystemStates));
      }
      else if (button.Name == string.Concat((object) this.driveControlStatePosition))
      {
        state = (byte) this.elementNumericUpDown[this.driveControlStatePosition].Value;
        resource = new ComponentResourceManager(typeof (DriveControlStates));
      }
      else if (button.Name == string.Concat((object) this.accuControlStatePosition))
      {
        state = (byte) this.elementNumericUpDown[this.accuControlStatePosition].Value;
        resource = new ComponentResourceManager(typeof (AccuControlStates));
      }
      else if (button.Name == string.Concat((object) this.buttonStatePosition))
      {
        state = (byte) this.elementNumericUpDown[this.buttonStatePosition].Value;
        resource = new ComponentResourceManager(typeof (ButtonStates));
      }
      else if (button.Name == string.Concat((object) this.motorClientStatePosition))
      {
        state = (byte) this.elementNumericUpDown[this.motorClientStatePosition].Value;
        resource = new ComponentResourceManager(typeof (MotorClientStates));
      }
      else if (button.Name == string.Concat((object) this.accuClientStatePosition))
      {
        state = (byte) this.elementNumericUpDown[this.accuClientStatePosition].Value;
        resource = new ComponentResourceManager(typeof (AccuClientStates));
      }
      int num = (int) new StateDetailsDialog(state, resource, (Form) MainWindow.Instance).ShowDialog();
    }

    private void foreground_ValueChanged(object sender, EventArgs e)
    {
      if (this.foregroundColorPosition <= 0)
        return;
      this.elementNumericUpDown[this.foregroundColorPosition].Value = (Decimal) this.foregroundColor.Value;
    }

    private void background_ValueChanged(object sender, EventArgs e)
    {
      if (this.backgroundColorPosition <= 0)
        return;
      this.elementNumericUpDown[this.backgroundColorPosition].Value = (Decimal) this.backgroundColor.Value;
    }

    private void ecoForeground_ValueChanged(object sender, EventArgs e)
    {
      if (this.ecoForegroundColorPosition <= 0)
        return;
      this.elementNumericUpDown[this.ecoForegroundColorPosition].Value = (Decimal) this.ecoForegroundColor.Value;
    }

    private void ecoBackground_ValueChanged(object sender, EventArgs e)
    {
      if (this.ecoBackgroundColorPosition <= 0)
        return;
      this.elementNumericUpDown[this.ecoBackgroundColorPosition].Value = (Decimal) this.ecoBackgroundColor.Value;
    }

    private void boostForeground_ValueChanged(object sender, EventArgs e)
    {
      if (this.boostForegroundColorPosition <= 0)
        return;
      this.elementNumericUpDown[this.boostForegroundColorPosition].Value = (Decimal) this.boostForegroundColor.Value;
    }

    private void boostBackground_ValueChanged(object sender, EventArgs e)
    {
      if (this.boostBackgroundColorPosition <= 0)
        return;
      this.elementNumericUpDown[this.boostBackgroundColorPosition].Value = (Decimal) this.boostBackgroundColor.Value;
    }

    private void foregroundColor_Enter(object sender, EventArgs e) => this.forgroundColorOldValue = (Decimal) ((ColorChooser) sender).Value;

    private void pushAssistant_EnterEvent(object sender, EventArgs e)
    {
      this.pushAssitantOldValue = ((NumericUpDownWithActivation) sender).Value;
      if (!(this.pushAssitantOldValue > (Decimal) this.pushAssistanCriticalValue))
        return;
      this.pushAssitantOldValue = (Decimal) this.pushAssistanCriticalValue;
    }

    private void pushAssistant_LeaveEvent(object sender, EventArgs e)
    {
      if (this.pushAssistantPosition <= 0 || !(this.pushAssistant.Value > (Decimal) this.pushAssistanCriticalValue) || MessageBox.Show(GlobalResource.PushAssistant_Warning_Message, GlobalResource.PushAssistant_Warning_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.Cancel)
        return;
      ((NumericUpDownWithActivation) sender).Value = this.pushAssitantOldValue;
    }

    private void pushAssistant_ValueChanged(object sender, EventArgs e)
    {
      if (this.pushAssistantPosition <= 0)
        return;
      this.elementNumericUpDown[this.pushAssistantPosition].Value = this.pushAssistant.Value;
    }

    private void pushBackAssistant_ValueChanged(object sender, EventArgs e)
    {
      if (this.pushAssistantPosition <= 0)
        return;
      this.elementNumericUpDown[this.pushBackAssistantPosition].Value = this.pushBackAssistant.Value;
    }

    private void speedLimiter_ValueChanged(object sender, EventArgs e)
    {
      if (this.speedLimiterPosition <= 0)
        return;
      this.elementNumericUpDown[this.speedLimiterPosition].Value = this.speedLimiter.Value;
    }

    private void dayTravelTime_ValueChanged(object sender, EventArgs e)
    {
      if (this.dayTravelTimePosition <= 0)
        return;
      this.elementNumericUpDown[this.dayTravelTimePosition].Value = this.dayTravelTime.Value;
    }

    private void totalTravelTime_ValueChanged(object sender, EventArgs e)
    {
      if (this.totalTravelTimePosition <= 0)
        return;
      this.elementNumericUpDown[this.totalTravelTimePosition].Value = this.totalTravelTime.Value;
    }

    private void overtravelDistance_ValueChanged(object sender, EventArgs e)
    {
      if (this.overtravelDistancePosition <= 0)
        return;
      this.elementNumericUpDown[this.overtravelDistancePosition].Value = this.overtravelDistance.Value;
    }

    private void dailyDistance_ValueChanged(object sender, EventArgs e)
    {
      if (this.dailyDistancePosition <= 0)
        return;
      this.elementNumericUpDown[this.dailyDistancePosition].Value = this.dailyDistance.Value;
    }

    private void totalDistance_ValueChanged(object sender, EventArgs e)
    {
      if (this.totalDistancePosition <= 0)
        return;
      this.elementNumericUpDown[this.totalDistancePosition].Value = this.totalDistance.Value;
    }

    private void speedValue_ValueChanged(object sender, EventArgs e)
    {
      if (this.speedValuePosition <= 0)
        return;
      this.elementNumericUpDown[this.speedValuePosition].Value = this.speedValue.Value;
    }

    private void wheelCircumference_ValueChanged(object sender, EventArgs e)
    {
      if (this.wheelCircumferencePosition <= 0)
        return;
      this.elementNumericUpDown[this.wheelCircumferencePosition].Value = this.wheelCircumference.Value;
    }

    private void restOfRange_ValueChanged(object sender, EventArgs e)
    {
      if (this.restOfRangePosition <= 0)
        return;
      this.elementNumericUpDown[this.restOfRangePosition].Value = this.restOfRange.Value;
    }

    private void maxSpeed_ValueChanged(object sender, EventArgs e)
    {
      if (this.maxSpeedPosition <= 0)
        return;
      this.elementNumericUpDown[this.maxSpeedPosition].Value = this.maxSpeed.Value;
    }

    private void averageSpeed_ValueChanged(object sender, EventArgs e)
    {
      if (this.averageSpeedPosition <= 0)
        return;
      this.elementNumericUpDown[this.averageSpeedPosition].Value = this.averageSpeed.Value;
    }

    private void nextServiceDistance_ValueChanged(object sender, EventArgs e)
    {
      if (this.nextServiceDistancePosition <= 0)
        return;
      this.elementNumericUpDown[this.nextServiceDistancePosition].Value = this.nextServiceDistance.Value;
    }

    private void maxAssistanceSpeed_ValueChanged(object sender, EventArgs e)
    {
      if (this.maxAssistanceSpeedPosition <= 0)
        return;
      this.elementNumericUpDown[this.maxAssistanceSpeedPosition].Value = this.maxAssistanceSpeed.Value;
    }

    private void brakeSwitch_CheckedChanged(object sender, EventArgs e)
    {
      if (this.brakeSwitchPosition <= 0)
        return;
      if (this.brakeSwitch.Checked)
        this.elementNumericUpDown[this.brakeSwitchPosition].Value = 1M;
      else
        this.elementNumericUpDown[this.brakeSwitchPosition].Value = 0M;
    }

    private void easyDisplaySwitch_CheckedChanged(object sender, EventArgs e)
    {
      if (this.easyDisplaySwitchPosition <= 0)
        return;
      if (this.easyDisplaySwitch.Checked)
        this.elementNumericUpDown[this.easyDisplaySwitchPosition].Value = 1M;
      else
        this.elementNumericUpDown[this.easyDisplaySwitchPosition].Value = 0M;
    }

    public void ResetNumericUpDownValues()
    {
      int index = 0;
      foreach (KeyValuePair<int, ParameterValue> allParameter in this.allParameters)
      {
        this.elementNumericUpDown[index].Value = (Decimal) allParameter.Value.ReadableDefaultValue;
        this.numericUpDown_ValueChanged((object) this.elementNumericUpDown[index], EventArgs.Empty);
        ++index;
      }
    }

    public string[] GetElementTextLabels()
    {
      List<string> stringList = new List<string>();
      for (int row = 0; row < this.tableLayoutPanel.RowCount; ++row)
      {
        Label controlFromPosition = (Label) this.tableLayoutPanel.GetControlFromPosition(this.PositionOfRowLabels, row);
        if (controlFromPosition != null)
          stringList.Add(controlFromPosition.Text);
      }
      return stringList.ToArray();
    }

    public string[] GetElementTextEnglishLabels()
    {
      List<string> stringList = new List<string>();
      for (int row = 0; row < this.tableLayoutPanel.RowCount; ++row)
      {
        Label controlFromPosition = (Label) this.tableLayoutPanel.GetControlFromPosition(this.PositionOfRowLabels, row);
        if (controlFromPosition != null)
          stringList.Add(controlFromPosition.Name);
      }
      return stringList.ToArray();
    }

    public bool IsPushAssistantActive() => this.pushAssistant.GetCheckState();

    public bool IsSpeedLimiterActive() => this.speedLimiter.GetCheckState();

    public string[] GetElementUnitLabels()
    {
      List<string> stringList = new List<string>();
      for (int row = 0; row < this.tableLayoutPanel.RowCount; ++row)
      {
        Label controlFromPosition = (Label) this.tableLayoutPanel.GetControlFromPosition(this.PositionOfRowUnits, row);
        if (controlFromPosition != null)
          stringList.Add(controlFromPosition.Text);
      }
      return stringList.ToArray();
    }

    public string[] GetElementNumbericUpDownValues(int maxLength = 45)
    {
      List<string> stringList = new List<string>();
      for (int row = 0; row < this.tableLayoutPanel.RowCount; ++row)
      {
        Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(this.PositionOfRowValus, row);
        if (controlFromPosition != null)
        {
          if (controlFromPosition.GetType() == typeof (NumericUpDown))
          {
            string format;
            switch (((NumericUpDown) controlFromPosition).DecimalPlaces)
            {
              case 1:
                format = "{0:0.0}";
                break;
              case 2:
                format = "{0:0.00}";
                break;
              default:
                format = "{0:0}";
                break;
            }
            stringList.Add(string.Format(format, (object) ((NumericUpDown) controlFromPosition).Value));
          }
          else if (controlFromPosition.GetType() == typeof (Button))
          {
            Button button = (Button) controlFromPosition;
            byte state = 0;
            ComponentResourceManager resource = (ComponentResourceManager) null;
            if (button.Name == string.Concat((object) this.mmiSystemStatePosition))
            {
              state = (byte) this.elementNumericUpDown[this.mmiSystemStatePosition].Value;
              resource = new ComponentResourceManager(typeof (MMISystemStates));
            }
            else if (button.Name == string.Concat((object) this.driveControlStatePosition))
            {
              state = (byte) this.elementNumericUpDown[this.driveControlStatePosition].Value;
              resource = new ComponentResourceManager(typeof (DriveControlStates));
            }
            else if (button.Name == string.Concat((object) this.accuControlStatePosition))
            {
              state = (byte) this.elementNumericUpDown[this.accuControlStatePosition].Value;
              resource = new ComponentResourceManager(typeof (AccuControlStates));
            }
            else if (button.Name == string.Concat((object) this.buttonStatePosition))
            {
              state = (byte) this.elementNumericUpDown[this.buttonStatePosition].Value;
              resource = new ComponentResourceManager(typeof (ButtonStates));
            }
            else if (button.Name == string.Concat((object) this.motorClientStatePosition))
            {
              state = (byte) this.elementNumericUpDown[this.motorClientStatePosition].Value;
              resource = new ComponentResourceManager(typeof (MotorClientStates));
            }
            else if (button.Name == string.Concat((object) this.accuClientStatePosition))
            {
              state = (byte) this.elementNumericUpDown[this.accuClientStatePosition].Value;
              resource = new ComponentResourceManager(typeof (AccuClientStates));
            }
            StateDetailsDialog stateDetailsDialog = new StateDetailsDialog(state, resource, (Form) MainWindow.Instance);
            stringList.Add(stateDetailsDialog.ValueAsText(maxLength));
          }
          else if (controlFromPosition.GetType() == typeof (DateControl))
            stringList.Add(((DateControl) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (DateControlWithActivation))
          {
            if (!((DateControlWithActivation) controlFromPosition).GetCheckState())
              stringList.Add(GlobalResource.Parameter_Not_Checked);
            else
              stringList.Add(((DateControlWithActivation) controlFromPosition).ValueAsText());
          }
          else if (controlFromPosition.GetType() == typeof (TimeControl))
            stringList.Add(((TimeControl) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (SoftwareVersionMMI))
            stringList.Add(((SoftwareVersionMMI) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (SerialnumberNumbericUpDown))
            stringList.Add(((SerialnumberNumbericUpDown) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (MMIDefaults))
            stringList.Add(((MMIDefaults) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (ColorChooser))
            stringList.Add(((ColorChooser) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithActivation))
          {
            string format;
            switch (((NumericUpDownWithActivation) controlFromPosition).DecimalPlaces)
            {
              case 1:
                format = "{0:0.0}";
                break;
              case 2:
                format = "{0:0.00}";
                break;
              default:
                format = "{0:0}";
                break;
            }
            stringList.Add(string.Format(format, (object) ((NumericUpDownWithActivation) controlFromPosition).GetNumericUpDownValue()));
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithSpecialValue))
          {
            string format;
            switch (((NumericUpDownWithSpecialValue) controlFromPosition).DecimalPlaces)
            {
              case 1:
                format = "{0:0.0}";
                break;
              case 2:
                format = "{0:0.00}";
                break;
              default:
                format = "{0:0}";
                break;
            }
            if (!((NumericUpDownWithSpecialValue) controlFromPosition).GetCheckState())
              stringList.Add(GlobalResource.Parameter_Not_Checked);
            else
              stringList.Add(string.Format(format, (object) ((NumericUpDownWithSpecialValue) controlFromPosition).GetNumericUpDownValue()));
          }
          else if (controlFromPosition.GetType() == typeof (TimeSpanUpDown))
            stringList.Add(((TimeSpanUpDown) controlFromPosition).ValueAsText());
          else if (controlFromPosition.GetType() == typeof (CheckBox))
          {
            if (((CheckBox) controlFromPosition).Checked)
              stringList.Add(GlobalResource.Parameter_Checked);
            else
              stringList.Add(GlobalResource.Parameter_Not_Checked);
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithConversion))
          {
            string format;
            switch (((NumericUpDownWithConversion) controlFromPosition).DecimalPlaces)
            {
              case 1:
                format = "{0:0.0}";
                break;
              case 2:
                format = "{0:0.00}";
                break;
              default:
                format = "{0:0}";
                break;
            }
            stringList.Add(string.Format(format, (object) ((NumericUpDownWithConversion) controlFromPosition).Value));
          }
          else
            stringList.Add("unkown");
        }
      }
      return stringList.ToArray();
    }

    public void EnableAllElements()
    {
      if (!ActivationInformation.IsAllAccessActive || ActivationInformation.VersionLevelProperty < 4)
        return;
      this.lastState = new bool[this.tableLayoutPanel.RowCount];
      for (int row = 0; row < this.tableLayoutPanel.RowCount; ++row)
      {
        Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(this.PositionOfRowValus, row);
        if (controlFromPosition != null)
        {
          if (controlFromPosition.GetType() == typeof (NumericUpDown))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
            ((UpDownBase) controlFromPosition).ReadOnly = false;
          }
          else if (controlFromPosition.GetType() == typeof (DateControl))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
          }
          else if (controlFromPosition.GetType() == typeof (DateControlWithActivation))
          {
            this.lastState[row] = ((DateControlWithActivation) controlFromPosition).Enabled;
            ((DateControlWithActivation) controlFromPosition).Enabled = true;
          }
          else if (controlFromPosition.GetType() == typeof (TimeControl))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
          }
          else if (controlFromPosition.GetType() == typeof (SoftwareVersionMMI))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
            ((UpDownBase) controlFromPosition).ReadOnly = false;
          }
          else if (controlFromPosition.GetType() == typeof (SerialnumberNumbericUpDown))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
            ((UpDownBase) controlFromPosition).ReadOnly = false;
          }
          else if (controlFromPosition.GetType() == typeof (MMIDefaults))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
          }
          else if (controlFromPosition.GetType() == typeof (ColorChooser))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithActivation))
          {
            this.lastState[row] = ((NumericUpDownWithActivation) controlFromPosition).Enabled;
            ((NumericUpDownWithActivation) controlFromPosition).Enabled = true;
            ((NumericUpDownWithActivation) controlFromPosition).ReadOnly = false;
          }
          else if (controlFromPosition.GetType() == typeof (TimeSpanUpDown))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithConversion))
          {
            this.lastState[row] = ((NumericUpDownWithConversion) controlFromPosition).Enabled;
            ((NumericUpDownWithConversion) controlFromPosition).Enabled = true;
            ((NumericUpDownWithConversion) controlFromPosition).ReadOnly = false;
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithSpecialValue))
          {
            this.lastState[row] = ((NumericUpDownWithSpecialValue) controlFromPosition).Enabled;
            ((NumericUpDownWithSpecialValue) controlFromPosition).Enabled = true;
            ((NumericUpDownWithSpecialValue) controlFromPosition).ReadOnly = false;
          }
          else if (controlFromPosition.GetType() == typeof (CheckBox))
          {
            this.lastState[row] = controlFromPosition.Enabled;
            controlFromPosition.Enabled = true;
          }
        }
      }
    }

    public void DisableElements()
    {
      if (ActivationInformation.IsAllAccessActive || ActivationInformation.VersionLevelProperty != 4 || this.lastState == null)
        return;
      for (int row = 0; row < this.tableLayoutPanel.RowCount; ++row)
      {
        Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(this.PositionOfRowValus, row);
        if (controlFromPosition != null)
        {
          if (controlFromPosition.GetType() == typeof (NumericUpDown))
          {
            if (!this.lastState[row])
            {
              controlFromPosition.Enabled = false;
              ((UpDownBase) controlFromPosition).ReadOnly = true;
            }
          }
          else if (controlFromPosition.GetType() == typeof (DateControl))
          {
            if (!this.lastState[row])
              controlFromPosition.Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (DateControlWithActivation))
          {
            if (!this.lastState[row])
              ((DateControlWithActivation) controlFromPosition).Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (TimeControl))
          {
            if (!this.lastState[row])
              controlFromPosition.Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (SoftwareVersionMMI))
          {
            if (!this.lastState[row])
            {
              controlFromPosition.Enabled = false;
              ((UpDownBase) controlFromPosition).ReadOnly = true;
            }
          }
          else if (controlFromPosition.GetType() == typeof (SerialnumberNumbericUpDown))
          {
            if (!this.lastState[row])
            {
              controlFromPosition.Enabled = false;
              ((UpDownBase) controlFromPosition).ReadOnly = true;
            }
          }
          else if (controlFromPosition.GetType() == typeof (MMIDefaults))
          {
            if (!this.lastState[row])
              controlFromPosition.Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (ColorChooser))
          {
            if (!this.lastState[row])
              controlFromPosition.Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithActivation))
          {
            if (!this.lastState[row])
            {
              ((NumericUpDownWithActivation) controlFromPosition).Enabled = false;
              ((NumericUpDownWithActivation) controlFromPosition).ReadOnly = true;
            }
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithSpecialValue))
          {
            if (!this.lastState[row])
            {
              ((NumericUpDownWithActivation) controlFromPosition).Enabled = false;
              ((NumericUpDownWithActivation) controlFromPosition).ReadOnly = true;
            }
          }
          else if (controlFromPosition.GetType() == typeof (TimeSpanUpDown))
          {
            if (!this.lastState[row])
              controlFromPosition.Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (CheckBox))
          {
            if (!this.lastState[row])
              controlFromPosition.Enabled = false;
          }
          else if (controlFromPosition.GetType() == typeof (NumericUpDownWithConversion) && !this.lastState[row])
          {
            ((NumericUpDownWithConversion) controlFromPosition).Enabled = false;
            ((NumericUpDownWithConversion) controlFromPosition).ReadOnly = true;
          }
        }
      }
    }

    private void ParameterPanel_EnabledChanged(object sender, EventArgs e)
    {
      for (int index = 0; index < this.elementInfoPictureBox.Length; ++index)
      {
        if (this.elementInfoPictureBox[index] != null && this.elementInfoPictureBox[index].Image != null)
          this.elementInfoPictureBox[index].Image = !this.Enabled ? (Image) Resources.buttons_information_gray : (Image) Resources.buttons_information;
        if (this.elementWarningPictureBox[index] != null && this.elementWarningPictureBox[index].Image != null)
          this.elementWarningPictureBox[index].Image = !this.Enabled ? (Image) Resources.buttons_attention_gray : (Image) Resources.buttons_attention;
      }
    }

    protected override void Dispose(bool disposing)
    {
      MouseWheelRedirector.Detach((Control) this);
      MouseWheelRedirector.Detach((Control) this.tableLayoutPanel);
      MouseWheelRedirector.Detach((Control[]) this.elementTextLabel);
      MouseWheelRedirector.Detach((Control[]) this.elementUnitLabel);
      MouseWheelRedirector.Detach((Control[]) this.elementWarningPictureBox);
      MouseWheelRedirector.Detach((Control[]) this.elementInfoPictureBox);
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ParameterPanel));
      this.tableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.tableLayoutPanel);
      this.DoubleBuffered = true;
      this.Name = nameof (ParameterPanel);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public delegate void ValueChangedEventHandler(object sender, ParameterPanelEventArgs e);
  }
}
