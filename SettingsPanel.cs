// Decompiled with JetBrains decompiler
// Type: ZerroWare.SettingsPanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using MMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class SettingsPanel : UserControl
  {
    private IContainer components;
    private TabPage testModeTabPage;
    private TabPage myDataTabPage;
    private TabPage optionTabPage;
    private TabControl tabControl;
    private GroupBox soundGroupBox;
    private GroupBox soundFileGroupBox;
    private FlickerFreeTableLayoutPanel soundTableLayoutPanel;
    private RadioButton defaultSoundRadioButton;
    private Button chooseSoundButton;
    private RadioButton userSoundRadioButton;
    private TextBox soundFilePathTextBox;
    private RadioButton playSoundRadioButton;
    private RadioButton noSoundRadioButton;
    private GroupBox fileSettingsGroupBox;
    private RadioButton userFilesRadioButton;
    private RadioButton globalFilesRadioButton;
    private GroupBox internetGroupBox;
    private FlickerFreeTableLayoutPanel flickerFreeTableLayoutPanel1;
    private Label actualProxyLabel;
    private RadioButton manualProxyRadioButton;
    private RadioButton noProxyRadioButton;
    private RadioButton systemProxyRadioButton;
    private Button proxySettingButton;
    private GroupBox languageGroupBox;
    private FlickerFreeTableLayoutPanel languageTableLayoutPanel;
    private RadioButton englishRadioButton;
    private RadioButton germanRadioButton;
    private Label englishLabel;
    private Label germanLabel;
    private CheckBox userAgreementCheckBox;
    private LinkLabel privacyPolicyLinkLabel;
    private Label infoLabel;
    private Label captionLabel;
    private Button saveDataButton;
    private Label requiredLabel;
    private FlickerFreeTableLayoutPanel tableLayoutPanel1;
    private Label companyLabel;
    private Label streetLabel;
    private Label streetNumberLabel;
    private Label zipLabel;
    private Label cityLabel;
    private Label countryLabel;
    private Label eMailLabel;
    private Label telephoneLabel;
    private TextBox cityTextBox;
    private TextBox zipTextBox;
    private TextBox streetNumberTextBox;
    private TextBox streetTextBox;
    private TextBox companyTextBox;
    private ComboBox countryComboBox;
    private TextBox eMailTextBox;
    private TextBox telephoneTextBox;
    private Label contactPersonLabel;
    private TextBox contactPersonTextBox;
    private Button saveOptionsButton;
    private Button activateTestModeButton;
    private Panel firmwareListPanel;
    private FlickerFreeTableLayoutPanel firmwareListTableLayoutPanel;
    private Label label10;
    private Label label8;
    private Label label9;
    private PictureBox firmwareTranmissionOkPictureBox;
    private Button deactivateTestModeButton;
    private Panel addFirmarePanel;
    private FlickerFreeTableLayoutPanel addFirmwareTableLayout;
    private Button addMMIFirmwareButton;
    private Button addEngineFirmwareButton;
    private Button addBatteryFirmwareButton;
    private Button addDFIFirmwareButton;
    private Label label11;
    private Button addConnectMMIFirmwareButton;
    private Label label1;
    private Color highlightingColor = Color.OrangeRed;
    private Color defaultColor = Color.Black;
    private int defaultLCID = 1031;
    private Dictionary<string, int> countryDictionary;
    private string country = string.Empty;
    private int lcId;
    private bool canelLeavingActualTab;
    private FirmwareListFile motorList;
    private int motorListLength;
    private FirmwareListElement[] motorFirmwareListElement;
    private FirmwareListFile mmiList;
    private int mmiListLength;
    private FirmwareListElement[] mmiFirmwareListElement;
    private FirmwareListFile connectMMIList;
    private int connectMMIListLength;
    private FirmwareListElement[] connectMMIFirmwareListElement;
    private FirmwareListFile accuList;
    private int accuListLength;
    private FirmwareListElement[] accuFirmwareListElement;
    private FirmwareListFile dfiList;
    private int dfiListLength;
    private FirmwareListElement[] dfiFirmwareListElement;
    private FlickerFreeTableLayoutPanel[] removeFirmwareFromMMI;
    private PictureBox[] removeFirmwareFromMMIPic;
    private Label[] removeFirmwareFromMMILabel;
    private Communication mmiCom;
    private MMITransceiver transceiver;
    private string installedMMIFirmwareVersion = string.Empty;
    private string installedConnectMMIFirmwareVersion = string.Empty;
    private string installedAccuFirmwareVersion = string.Empty;
    private string installedMotorFirmwareVersion = string.Empty;
    private string installedDFIFirmwareVersion = string.Empty;
    private string mmiSerialNumber = string.Empty;
    private WaitingForReboot rebootDialog;
    private bool initPassed;

    protected override void Dispose(bool disposing)
    {
      MouseWheelRedirector.Detach((Control) this.firmwareListPanel);
      MouseWheelRedirector.Detach((Control) this.firmwareListTableLayoutPanel);
      MouseWheelRedirector.Detach((Control) this.label8);
      MouseWheelRedirector.Detach((Control) this.label9);
      MouseWheelRedirector.Detach((Control) this.label10);
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void FillProfilesTablePanel()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SettingsPanel));
      this.testModeTabPage = new TabPage();
      this.addFirmarePanel = new Panel();
      this.addFirmwareTableLayout = new FlickerFreeTableLayoutPanel();
      this.addConnectMMIFirmwareButton = new Button();
      this.addMMIFirmwareButton = new Button();
      this.addEngineFirmwareButton = new Button();
      this.addBatteryFirmwareButton = new Button();
      this.addDFIFirmwareButton = new Button();
      this.deactivateTestModeButton = new Button();
      this.firmwareTranmissionOkPictureBox = new PictureBox();
      this.firmwareListPanel = new Panel();
      this.firmwareListTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.label1 = new Label();
      this.label10 = new Label();
      this.label8 = new Label();
      this.label9 = new Label();
      this.label11 = new Label();
      this.activateTestModeButton = new Button();
      this.myDataTabPage = new TabPage();
      this.userAgreementCheckBox = new CheckBox();
      this.privacyPolicyLinkLabel = new LinkLabel();
      this.infoLabel = new Label();
      this.captionLabel = new Label();
      this.saveDataButton = new Button();
      this.requiredLabel = new Label();
      this.tableLayoutPanel1 = new FlickerFreeTableLayoutPanel();
      this.companyLabel = new Label();
      this.streetLabel = new Label();
      this.streetNumberLabel = new Label();
      this.zipLabel = new Label();
      this.cityLabel = new Label();
      this.countryLabel = new Label();
      this.eMailLabel = new Label();
      this.telephoneLabel = new Label();
      this.cityTextBox = new TextBox();
      this.zipTextBox = new TextBox();
      this.streetNumberTextBox = new TextBox();
      this.streetTextBox = new TextBox();
      this.companyTextBox = new TextBox();
      this.countryComboBox = new ComboBox();
      this.eMailTextBox = new TextBox();
      this.telephoneTextBox = new TextBox();
      this.contactPersonLabel = new Label();
      this.contactPersonTextBox = new TextBox();
      this.optionTabPage = new TabPage();
      this.saveOptionsButton = new Button();
      this.soundGroupBox = new GroupBox();
      this.soundFileGroupBox = new GroupBox();
      this.soundTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.defaultSoundRadioButton = new RadioButton();
      this.chooseSoundButton = new Button();
      this.userSoundRadioButton = new RadioButton();
      this.soundFilePathTextBox = new TextBox();
      this.playSoundRadioButton = new RadioButton();
      this.noSoundRadioButton = new RadioButton();
      this.fileSettingsGroupBox = new GroupBox();
      this.userFilesRadioButton = new RadioButton();
      this.globalFilesRadioButton = new RadioButton();
      this.internetGroupBox = new GroupBox();
      this.flickerFreeTableLayoutPanel1 = new FlickerFreeTableLayoutPanel();
      this.actualProxyLabel = new Label();
      this.manualProxyRadioButton = new RadioButton();
      this.noProxyRadioButton = new RadioButton();
      this.systemProxyRadioButton = new RadioButton();
      this.proxySettingButton = new Button();
      this.languageGroupBox = new GroupBox();
      this.languageTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.englishRadioButton = new RadioButton();
      this.germanRadioButton = new RadioButton();
      this.englishLabel = new Label();
      this.germanLabel = new Label();
      this.tabControl = new TabControl();
      this.testModeTabPage.SuspendLayout();
      this.addFirmarePanel.SuspendLayout();
      this.addFirmwareTableLayout.SuspendLayout();
      ((ISupportInitialize) this.firmwareTranmissionOkPictureBox).BeginInit();
      this.firmwareListPanel.SuspendLayout();
      this.firmwareListTableLayoutPanel.SuspendLayout();
      this.myDataTabPage.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.optionTabPage.SuspendLayout();
      this.soundGroupBox.SuspendLayout();
      this.soundFileGroupBox.SuspendLayout();
      this.soundTableLayoutPanel.SuspendLayout();
      this.fileSettingsGroupBox.SuspendLayout();
      this.internetGroupBox.SuspendLayout();
      this.flickerFreeTableLayoutPanel1.SuspendLayout();
      this.languageGroupBox.SuspendLayout();
      this.languageTableLayoutPanel.SuspendLayout();
      this.tabControl.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.testModeTabPage, "testModeTabPage");
      this.testModeTabPage.BackColor = Color.White;
      this.testModeTabPage.Controls.Add((Control) this.addFirmarePanel);
      this.testModeTabPage.Controls.Add((Control) this.deactivateTestModeButton);
      this.testModeTabPage.Controls.Add((Control) this.firmwareTranmissionOkPictureBox);
      this.testModeTabPage.Controls.Add((Control) this.firmwareListPanel);
      this.testModeTabPage.Controls.Add((Control) this.activateTestModeButton);
      this.testModeTabPage.Name = "testModeTabPage";
      this.testModeTabPage.SizeChanged += new EventHandler(this.testModeTabPage_SizeChanged);
      componentResourceManager.ApplyResources((object) this.addFirmarePanel, "addFirmarePanel");
      this.addFirmarePanel.Controls.Add((Control) this.addFirmwareTableLayout);
      this.addFirmarePanel.Name = "addFirmarePanel";
      componentResourceManager.ApplyResources((object) this.addFirmwareTableLayout, "addFirmwareTableLayout");
      this.addFirmwareTableLayout.Controls.Add((Control) this.addConnectMMIFirmwareButton, 0, 0);
      this.addFirmwareTableLayout.Controls.Add((Control) this.addMMIFirmwareButton, 1, 0);
      this.addFirmwareTableLayout.Controls.Add((Control) this.addEngineFirmwareButton, 2, 0);
      this.addFirmwareTableLayout.Controls.Add((Control) this.addBatteryFirmwareButton, 3, 0);
      this.addFirmwareTableLayout.Controls.Add((Control) this.addDFIFirmwareButton, 4, 0);
      this.addFirmwareTableLayout.Cursor = Cursors.Default;
      this.addFirmwareTableLayout.Name = "addFirmwareTableLayout";
      componentResourceManager.ApplyResources((object) this.addConnectMMIFirmwareButton, "addConnectMMIFirmwareButton");
      this.addConnectMMIFirmwareButton.Name = "addConnectMMIFirmwareButton";
      this.addConnectMMIFirmwareButton.UseVisualStyleBackColor = true;
      this.addConnectMMIFirmwareButton.Click += new EventHandler(this.addConnectMMIFirmwareButton_Click);
      componentResourceManager.ApplyResources((object) this.addMMIFirmwareButton, "addMMIFirmwareButton");
      this.addMMIFirmwareButton.Name = "addMMIFirmwareButton";
      this.addMMIFirmwareButton.UseVisualStyleBackColor = true;
      this.addMMIFirmwareButton.Click += new EventHandler(this.addMMIFirmwareButton_Click);
      componentResourceManager.ApplyResources((object) this.addEngineFirmwareButton, "addEngineFirmwareButton");
      this.addEngineFirmwareButton.Name = "addEngineFirmwareButton";
      this.addEngineFirmwareButton.UseVisualStyleBackColor = true;
      this.addEngineFirmwareButton.Click += new EventHandler(this.addEngineFirmwareButton_Click);
      componentResourceManager.ApplyResources((object) this.addBatteryFirmwareButton, "addBatteryFirmwareButton");
      this.addBatteryFirmwareButton.Name = "addBatteryFirmwareButton";
      this.addBatteryFirmwareButton.UseVisualStyleBackColor = true;
      this.addBatteryFirmwareButton.Click += new EventHandler(this.addBatteryFirmwareButton_Click);
      componentResourceManager.ApplyResources((object) this.addDFIFirmwareButton, "addDFIFirmwareButton");
      this.addDFIFirmwareButton.Name = "addDFIFirmwareButton";
      this.addDFIFirmwareButton.UseVisualStyleBackColor = true;
      this.addDFIFirmwareButton.Click += new EventHandler(this.addDFIFirmwareButton_Click);
      componentResourceManager.ApplyResources((object) this.deactivateTestModeButton, "deactivateTestModeButton");
      this.deactivateTestModeButton.DialogResult = DialogResult.Cancel;
      this.deactivateTestModeButton.Name = "deactivateTestModeButton";
      this.deactivateTestModeButton.UseVisualStyleBackColor = true;
      this.deactivateTestModeButton.Click += new EventHandler(this.deactivateTestModeButton_Click);
      componentResourceManager.ApplyResources((object) this.firmwareTranmissionOkPictureBox, "firmwareTranmissionOkPictureBox");
      this.firmwareTranmissionOkPictureBox.Name = "firmwareTranmissionOkPictureBox";
      this.firmwareTranmissionOkPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.firmwareListPanel, "firmwareListPanel");
      this.firmwareListPanel.Controls.Add((Control) this.firmwareListTableLayoutPanel);
      this.firmwareListPanel.Name = "firmwareListPanel";
      componentResourceManager.ApplyResources((object) this.firmwareListTableLayoutPanel, "firmwareListTableLayoutPanel");
      this.firmwareListTableLayoutPanel.Controls.Add((Control) this.label1, 0, 0);
      this.firmwareListTableLayoutPanel.Controls.Add((Control) this.label10, 1, 0);
      this.firmwareListTableLayoutPanel.Controls.Add((Control) this.label8, 2, 0);
      this.firmwareListTableLayoutPanel.Controls.Add((Control) this.label9, 3, 0);
      this.firmwareListTableLayoutPanel.Controls.Add((Control) this.label11, 4, 0);
      this.firmwareListTableLayoutPanel.Cursor = Cursors.Default;
      this.firmwareListTableLayoutPanel.Name = "firmwareListTableLayoutPanel";
      this.firmwareListTableLayoutPanel.CellPaint += new TableLayoutCellPaintEventHandler(this.firmwareListTableLayoutPanel_CellPaint);
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.BackColor = Color.Transparent;
      this.label1.Cursor = Cursors.Default;
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.label10, "label10");
      this.label10.BackColor = Color.Transparent;
      this.label10.Cursor = Cursors.Default;
      this.label10.Name = "label10";
      componentResourceManager.ApplyResources((object) this.label8, "label8");
      this.label8.BackColor = Color.Transparent;
      this.label8.Cursor = Cursors.Default;
      this.label8.Name = "label8";
      componentResourceManager.ApplyResources((object) this.label9, "label9");
      this.label9.BackColor = Color.Transparent;
      this.label9.Cursor = Cursors.Default;
      this.label9.Name = "label9";
      componentResourceManager.ApplyResources((object) this.label11, "label11");
      this.label11.BackColor = Color.Transparent;
      this.label11.Name = "label11";
      componentResourceManager.ApplyResources((object) this.activateTestModeButton, "activateTestModeButton");
      this.activateTestModeButton.DialogResult = DialogResult.Cancel;
      this.activateTestModeButton.Name = "activateTestModeButton";
      this.activateTestModeButton.UseVisualStyleBackColor = true;
      this.activateTestModeButton.Click += new EventHandler(this.activateTestModeButton_Click);
      componentResourceManager.ApplyResources((object) this.myDataTabPage, "myDataTabPage");
      this.myDataTabPage.BackColor = Color.White;
      this.myDataTabPage.Controls.Add((Control) this.userAgreementCheckBox);
      this.myDataTabPage.Controls.Add((Control) this.privacyPolicyLinkLabel);
      this.myDataTabPage.Controls.Add((Control) this.infoLabel);
      this.myDataTabPage.Controls.Add((Control) this.captionLabel);
      this.myDataTabPage.Controls.Add((Control) this.saveDataButton);
      this.myDataTabPage.Controls.Add((Control) this.requiredLabel);
      this.myDataTabPage.Controls.Add((Control) this.tableLayoutPanel1);
      this.myDataTabPage.Name = "myDataTabPage";
      componentResourceManager.ApplyResources((object) this.userAgreementCheckBox, "userAgreementCheckBox");
      this.userAgreementCheckBox.BackColor = Color.Transparent;
      this.userAgreementCheckBox.Name = "userAgreementCheckBox";
      this.userAgreementCheckBox.UseVisualStyleBackColor = false;
      this.userAgreementCheckBox.CheckStateChanged += new EventHandler(this.userAgreementCheckBox_CheckStateChanged);
      componentResourceManager.ApplyResources((object) this.privacyPolicyLinkLabel, "privacyPolicyLinkLabel");
      this.privacyPolicyLinkLabel.BackColor = Color.Transparent;
      this.privacyPolicyLinkLabel.Name = "privacyPolicyLinkLabel";
      this.privacyPolicyLinkLabel.TabStop = true;
      this.privacyPolicyLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.privacyPolicyLinkLabel_LinkClicked);
      componentResourceManager.ApplyResources((object) this.infoLabel, "infoLabel");
      this.infoLabel.BackColor = Color.Transparent;
      this.infoLabel.Name = "infoLabel";
      componentResourceManager.ApplyResources((object) this.captionLabel, "captionLabel");
      this.captionLabel.BackColor = Color.Transparent;
      this.captionLabel.Name = "captionLabel";
      componentResourceManager.ApplyResources((object) this.saveDataButton, "saveDataButton");
      this.saveDataButton.DialogResult = DialogResult.Cancel;
      this.saveDataButton.Name = "saveDataButton";
      this.saveDataButton.UseVisualStyleBackColor = true;
      this.saveDataButton.Click += new EventHandler(this.saveDataButton_Click);
      componentResourceManager.ApplyResources((object) this.requiredLabel, "requiredLabel");
      this.requiredLabel.BackColor = Color.Transparent;
      this.requiredLabel.Name = "requiredLabel";
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
      this.tableLayoutPanel1.BackColor = Color.Transparent;
      this.tableLayoutPanel1.Controls.Add((Control) this.companyLabel, 0, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.streetLabel, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.streetNumberLabel, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.zipLabel, 0, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.cityLabel, 0, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.countryLabel, 0, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.eMailLabel, 0, 7);
      this.tableLayoutPanel1.Controls.Add((Control) this.telephoneLabel, 0, 8);
      this.tableLayoutPanel1.Controls.Add((Control) this.cityTextBox, 1, 5);
      this.tableLayoutPanel1.Controls.Add((Control) this.zipTextBox, 1, 4);
      this.tableLayoutPanel1.Controls.Add((Control) this.streetNumberTextBox, 1, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.streetTextBox, 1, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.companyTextBox, 1, 1);
      this.tableLayoutPanel1.Controls.Add((Control) this.countryComboBox, 1, 6);
      this.tableLayoutPanel1.Controls.Add((Control) this.eMailTextBox, 1, 7);
      this.tableLayoutPanel1.Controls.Add((Control) this.telephoneTextBox, 1, 8);
      this.tableLayoutPanel1.Controls.Add((Control) this.contactPersonLabel, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.contactPersonTextBox, 1, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.companyLabel, "companyLabel");
      this.companyLabel.BackColor = Color.Transparent;
      this.companyLabel.Name = "companyLabel";
      componentResourceManager.ApplyResources((object) this.streetLabel, "streetLabel");
      this.streetLabel.BackColor = Color.Transparent;
      this.streetLabel.Name = "streetLabel";
      componentResourceManager.ApplyResources((object) this.streetNumberLabel, "streetNumberLabel");
      this.streetNumberLabel.BackColor = Color.Transparent;
      this.streetNumberLabel.Name = "streetNumberLabel";
      componentResourceManager.ApplyResources((object) this.zipLabel, "zipLabel");
      this.zipLabel.BackColor = Color.Transparent;
      this.zipLabel.Name = "zipLabel";
      componentResourceManager.ApplyResources((object) this.cityLabel, "cityLabel");
      this.cityLabel.BackColor = Color.Transparent;
      this.cityLabel.Name = "cityLabel";
      componentResourceManager.ApplyResources((object) this.countryLabel, "countryLabel");
      this.countryLabel.BackColor = Color.Transparent;
      this.countryLabel.Name = "countryLabel";
      componentResourceManager.ApplyResources((object) this.eMailLabel, "eMailLabel");
      this.eMailLabel.BackColor = Color.Transparent;
      this.eMailLabel.Name = "eMailLabel";
      componentResourceManager.ApplyResources((object) this.telephoneLabel, "telephoneLabel");
      this.telephoneLabel.BackColor = Color.Transparent;
      this.telephoneLabel.Name = "telephoneLabel";
      componentResourceManager.ApplyResources((object) this.cityTextBox, "cityTextBox");
      this.cityTextBox.Name = "cityTextBox";
      this.cityTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.zipTextBox, "zipTextBox");
      this.zipTextBox.Name = "zipTextBox";
      this.zipTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.streetNumberTextBox, "streetNumberTextBox");
      this.streetNumberTextBox.Name = "streetNumberTextBox";
      this.streetNumberTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.streetTextBox, "streetTextBox");
      this.streetTextBox.Name = "streetTextBox";
      this.streetTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.companyTextBox, "companyTextBox");
      this.companyTextBox.BackColor = SystemColors.Window;
      this.companyTextBox.Name = "companyTextBox";
      this.companyTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.countryComboBox, "countryComboBox");
      this.countryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.countryComboBox.FormattingEnabled = true;
      this.countryComboBox.Name = "countryComboBox";
      this.countryComboBox.SelectedIndexChanged += new EventHandler(this.countryComboBox_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.eMailTextBox, "eMailTextBox");
      this.eMailTextBox.Name = "eMailTextBox";
      this.eMailTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.telephoneTextBox, "telephoneTextBox");
      this.telephoneTextBox.Name = "telephoneTextBox";
      this.telephoneTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.contactPersonLabel, "contactPersonLabel");
      this.contactPersonLabel.Name = "contactPersonLabel";
      componentResourceManager.ApplyResources((object) this.contactPersonTextBox, "contactPersonTextBox");
      this.contactPersonTextBox.Name = "contactPersonTextBox";
      this.contactPersonTextBox.TextChanged += new EventHandler(this.dataTextChanged);
      componentResourceManager.ApplyResources((object) this.optionTabPage, "optionTabPage");
      this.optionTabPage.BackColor = Color.White;
      this.optionTabPage.Controls.Add((Control) this.saveOptionsButton);
      this.optionTabPage.Controls.Add((Control) this.soundGroupBox);
      this.optionTabPage.Controls.Add((Control) this.fileSettingsGroupBox);
      this.optionTabPage.Controls.Add((Control) this.internetGroupBox);
      this.optionTabPage.Controls.Add((Control) this.languageGroupBox);
      this.optionTabPage.ForeColor = SystemColors.ControlText;
      this.optionTabPage.Name = "optionTabPage";
      componentResourceManager.ApplyResources((object) this.saveOptionsButton, "saveOptionsButton");
      this.saveOptionsButton.DialogResult = DialogResult.Cancel;
      this.saveOptionsButton.Name = "saveOptionsButton";
      this.saveOptionsButton.UseVisualStyleBackColor = true;
      this.saveOptionsButton.Click += new EventHandler(this.saveOptionsButton_Click);
      componentResourceManager.ApplyResources((object) this.soundGroupBox, "soundGroupBox");
      this.soundGroupBox.BackColor = Color.Transparent;
      this.soundGroupBox.Controls.Add((Control) this.soundFileGroupBox);
      this.soundGroupBox.Controls.Add((Control) this.playSoundRadioButton);
      this.soundGroupBox.Controls.Add((Control) this.noSoundRadioButton);
      this.soundGroupBox.Name = "soundGroupBox";
      this.soundGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.soundFileGroupBox, "soundFileGroupBox");
      this.soundFileGroupBox.Controls.Add((Control) this.soundTableLayoutPanel);
      this.soundFileGroupBox.Name = "soundFileGroupBox";
      this.soundFileGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.soundTableLayoutPanel, "soundTableLayoutPanel");
      this.soundTableLayoutPanel.Controls.Add((Control) this.defaultSoundRadioButton, 0, 0);
      this.soundTableLayoutPanel.Controls.Add((Control) this.chooseSoundButton, 2, 1);
      this.soundTableLayoutPanel.Controls.Add((Control) this.userSoundRadioButton, 0, 1);
      this.soundTableLayoutPanel.Controls.Add((Control) this.soundFilePathTextBox, 1, 1);
      this.soundTableLayoutPanel.Name = "soundTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.defaultSoundRadioButton, "defaultSoundRadioButton");
      this.defaultSoundRadioButton.Checked = true;
      this.defaultSoundRadioButton.Name = "defaultSoundRadioButton";
      this.defaultSoundRadioButton.TabStop = true;
      this.defaultSoundRadioButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.chooseSoundButton, "chooseSoundButton");
      this.chooseSoundButton.Name = "chooseSoundButton";
      this.chooseSoundButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.userSoundRadioButton, "userSoundRadioButton");
      this.userSoundRadioButton.Name = "userSoundRadioButton";
      this.userSoundRadioButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.soundFilePathTextBox, "soundFilePathTextBox");
      this.soundFilePathTextBox.Name = "soundFilePathTextBox";
      componentResourceManager.ApplyResources((object) this.playSoundRadioButton, "playSoundRadioButton");
      this.playSoundRadioButton.Name = "playSoundRadioButton";
      this.playSoundRadioButton.UseVisualStyleBackColor = true;
      this.playSoundRadioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.noSoundRadioButton, "noSoundRadioButton");
      this.noSoundRadioButton.Checked = true;
      this.noSoundRadioButton.Name = "noSoundRadioButton";
      this.noSoundRadioButton.TabStop = true;
      this.noSoundRadioButton.UseVisualStyleBackColor = true;
      this.noSoundRadioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.fileSettingsGroupBox, "fileSettingsGroupBox");
      this.fileSettingsGroupBox.BackColor = Color.Transparent;
      this.fileSettingsGroupBox.Controls.Add((Control) this.userFilesRadioButton);
      this.fileSettingsGroupBox.Controls.Add((Control) this.globalFilesRadioButton);
      this.fileSettingsGroupBox.Name = "fileSettingsGroupBox";
      this.fileSettingsGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.userFilesRadioButton, "userFilesRadioButton");
      this.userFilesRadioButton.Name = "userFilesRadioButton";
      this.userFilesRadioButton.UseVisualStyleBackColor = true;
      this.userFilesRadioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.globalFilesRadioButton, "globalFilesRadioButton");
      this.globalFilesRadioButton.Checked = true;
      this.globalFilesRadioButton.Name = "globalFilesRadioButton";
      this.globalFilesRadioButton.TabStop = true;
      this.globalFilesRadioButton.UseVisualStyleBackColor = true;
      this.globalFilesRadioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.internetGroupBox, "internetGroupBox");
      this.internetGroupBox.BackColor = Color.Transparent;
      this.internetGroupBox.Controls.Add((Control) this.flickerFreeTableLayoutPanel1);
      this.internetGroupBox.Name = "internetGroupBox";
      this.internetGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.flickerFreeTableLayoutPanel1, "flickerFreeTableLayoutPanel1");
      this.flickerFreeTableLayoutPanel1.Controls.Add((Control) this.actualProxyLabel, 0, 0);
      this.flickerFreeTableLayoutPanel1.Controls.Add((Control) this.manualProxyRadioButton, 1, 2);
      this.flickerFreeTableLayoutPanel1.Controls.Add((Control) this.noProxyRadioButton, 1, 0);
      this.flickerFreeTableLayoutPanel1.Controls.Add((Control) this.systemProxyRadioButton, 1, 1);
      this.flickerFreeTableLayoutPanel1.Controls.Add((Control) this.proxySettingButton, 2, 2);
      this.flickerFreeTableLayoutPanel1.Name = "flickerFreeTableLayoutPanel1";
      componentResourceManager.ApplyResources((object) this.actualProxyLabel, "actualProxyLabel");
      this.actualProxyLabel.Name = "actualProxyLabel";
      componentResourceManager.ApplyResources((object) this.manualProxyRadioButton, "manualProxyRadioButton");
      this.manualProxyRadioButton.Name = "manualProxyRadioButton";
      this.manualProxyRadioButton.TabStop = true;
      this.manualProxyRadioButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.noProxyRadioButton, "noProxyRadioButton");
      this.noProxyRadioButton.Name = "noProxyRadioButton";
      this.noProxyRadioButton.TabStop = true;
      this.noProxyRadioButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.systemProxyRadioButton, "systemProxyRadioButton");
      this.systemProxyRadioButton.Name = "systemProxyRadioButton";
      this.systemProxyRadioButton.TabStop = true;
      this.systemProxyRadioButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.proxySettingButton, "proxySettingButton");
      this.proxySettingButton.Name = "proxySettingButton";
      this.proxySettingButton.UseVisualStyleBackColor = true;
      this.proxySettingButton.Click += new EventHandler(this.proxySettingButton_Click);
      componentResourceManager.ApplyResources((object) this.languageGroupBox, "languageGroupBox");
      this.languageGroupBox.BackColor = Color.Transparent;
      this.languageGroupBox.Controls.Add((Control) this.languageTableLayoutPanel);
      this.languageGroupBox.Name = "languageGroupBox";
      this.languageGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.languageTableLayoutPanel, "languageTableLayoutPanel");
      this.languageTableLayoutPanel.Controls.Add((Control) this.englishRadioButton, 0, 0);
      this.languageTableLayoutPanel.Controls.Add((Control) this.germanRadioButton, 0, 1);
      this.languageTableLayoutPanel.Controls.Add((Control) this.englishLabel, 1, 0);
      this.languageTableLayoutPanel.Controls.Add((Control) this.germanLabel, 1, 1);
      this.languageTableLayoutPanel.Name = "languageTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.englishRadioButton, "englishRadioButton");
      this.englishRadioButton.Checked = true;
      this.englishRadioButton.Image = (Image) Resources.gb;
      this.englishRadioButton.Name = "englishRadioButton";
      this.englishRadioButton.TabStop = true;
      this.englishRadioButton.UseVisualStyleBackColor = true;
      this.englishRadioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.germanRadioButton, "germanRadioButton");
      this.germanRadioButton.Image = (Image) Resources.de;
      this.germanRadioButton.Name = "germanRadioButton";
      this.germanRadioButton.UseVisualStyleBackColor = true;
      this.germanRadioButton.CheckedChanged += new EventHandler(this.radioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.englishLabel, "englishLabel");
      this.englishLabel.Name = "englishLabel";
      this.englishLabel.Click += new EventHandler(this.englishLabel_Click);
      componentResourceManager.ApplyResources((object) this.germanLabel, "germanLabel");
      this.germanLabel.Name = "germanLabel";
      this.germanLabel.Click += new EventHandler(this.germanLabel_Click);
      componentResourceManager.ApplyResources((object) this.tabControl, "tabControl");
      this.tabControl.Controls.Add((Control) this.optionTabPage);
      this.tabControl.Controls.Add((Control) this.myDataTabPage);
      this.tabControl.Controls.Add((Control) this.testModeTabPage);
      this.tabControl.Cursor = Cursors.Default;
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.tabControl);
      this.DoubleBuffered = true;
      this.Name = nameof (SettingsPanel);
      this.Layout += new LayoutEventHandler(this.SettingsPanel_Layout);
      this.testModeTabPage.ResumeLayout(false);
      this.addFirmarePanel.ResumeLayout(false);
      this.addFirmarePanel.PerformLayout();
      this.addFirmwareTableLayout.ResumeLayout(false);
      ((ISupportInitialize) this.firmwareTranmissionOkPictureBox).EndInit();
      this.firmwareListPanel.ResumeLayout(false);
      this.firmwareListPanel.PerformLayout();
      this.firmwareListTableLayoutPanel.ResumeLayout(false);
      this.myDataTabPage.ResumeLayout(false);
      this.myDataTabPage.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.optionTabPage.ResumeLayout(false);
      this.soundGroupBox.ResumeLayout(false);
      this.soundGroupBox.PerformLayout();
      this.soundFileGroupBox.ResumeLayout(false);
      this.soundTableLayoutPanel.ResumeLayout(false);
      this.soundTableLayoutPanel.PerformLayout();
      this.fileSettingsGroupBox.ResumeLayout(false);
      this.fileSettingsGroupBox.PerformLayout();
      this.internetGroupBox.ResumeLayout(false);
      this.flickerFreeTableLayoutPanel1.ResumeLayout(false);
      this.flickerFreeTableLayoutPanel1.PerformLayout();
      this.languageGroupBox.ResumeLayout(false);
      this.languageTableLayoutPanel.ResumeLayout(false);
      this.languageTableLayoutPanel.PerformLayout();
      this.tabControl.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public SettingsPanel(Communication mmiCom)
    {
      this.InitializeComponent();
      this.mmiCom = mmiCom;
      this.transceiver = new MMITransceiver(this.mmiCom);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.tabControl.Font = FontDefinition.MenubarFont;
      this.tabControl.Selecting += new TabControlCancelEventHandler(this.tabControl_Selecting);
      this.saveDataButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.saveDataButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.saveOptionsButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.saveOptionsButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.activateTestModeButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.activateTestModeButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.deactivateTestModeButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.deactivateTestModeButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.proxySettingButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.proxySettingButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.addBatteryFirmwareButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.addBatteryFirmwareButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.addMMIFirmwareButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.addMMIFirmwareButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.addConnectMMIFirmwareButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.addConnectMMIFirmwareButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.addEngineFirmwareButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.addEngineFirmwareButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.addDFIFirmwareButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.addDFIFirmwareButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.infoLabel.Font = FontDefinition.DefaultTextFont;
      this.userAgreementCheckBox.Font = FontDefinition.DefaultTextFont;
      this.privacyPolicyLinkLabel.Font = FontDefinition.DefaultTextFont;
      this.requiredLabel.Font = FontDefinition.DefaultTextFont;
      this.label8.Font = FontDefinition.TableCaptionTextFont;
      this.label9.Font = FontDefinition.TableCaptionTextFont;
      this.label10.Font = FontDefinition.TableCaptionTextFont;
      this.label11.Font = FontDefinition.TableCaptionTextFont;
      this.label1.Font = FontDefinition.TableCaptionTextFont;
      this.label8.ForeColor = ColorDefinition.BlackTextColor;
      this.label9.ForeColor = ColorDefinition.BlackTextColor;
      this.label10.ForeColor = ColorDefinition.BlackTextColor;
      this.label11.ForeColor = ColorDefinition.BlackTextColor;
      this.label11.ForeColor = ColorDefinition.BlackTextColor;
      this.contactPersonTextBox.MaxLength = 128;
      this.companyTextBox.MaxLength = 128;
      this.telephoneTextBox.MaxLength = 128;
      this.eMailTextBox.MaxLength = 128;
      this.streetTextBox.MaxLength = 60;
      this.streetNumberTextBox.MaxLength = 60;
      this.zipTextBox.MaxLength = 60;
      this.cityTextBox.MaxLength = 60;
      this.ReadOptionSettings();
      this.UpdateProxySettings();
      this.InitCountryDictionary();
      foreach (KeyValuePair<string, int> keyValuePair in (IEnumerable<KeyValuePair<string, int>>) this.countryDictionary.OrderBy<KeyValuePair<string, int>, string>((Func<KeyValuePair<string, int>, string>) (key => key.Key)))
        this.countryComboBox.Items.Add((object) keyValuePair.Key);
      this.ReadRegSettings();
      this.saveOptionsButton.Enabled = false;
      this.saveDataButton.Enabled = false;
      this.firmwareListPanel.Visible = false;
      this.firmwareTranmissionOkPictureBox.Visible = false;
      this.deactivateTestModeButton.Visible = false;
      this.addFirmarePanel.Visible = false;
      this.canelLeavingActualTab = false;
      MouseWheelRedirector.Attach((Control) this.firmwareListPanel);
      MouseWheelRedirector.Attach((Control) this.firmwareListTableLayoutPanel);
      MouseWheelRedirector.Attach((Control) this.label8);
      MouseWheelRedirector.Attach((Control) this.label9);
      MouseWheelRedirector.Attach((Control) this.label10);
      MouseWheelRedirector.Attach((Control) this.label11);
      MouseWheelRedirector.Attach((Control) this.label1);
      if (ActivationInformation.VersionLevelProperty >= 4)
        return;
      this.tabControl.Controls.Remove((Control) this.testModeTabPage);
    }

    private void SettingsPanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.Parent == null || !(this.Size != this.Parent.Size))
        return;
      this.Size = this.Parent.Size;
    }

    private void firmwareListTableLayoutPanel_CellPaint(
      object sender,
      TableLayoutCellPaintEventArgs e)
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

    private void saveOptionsButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      LocalSettingsFile localSettingsFile = LocalSettingsFile.ReadFile();
      localSettingsFile.GlobalListFiles = this.globalFilesRadioButton.Checked || !this.userFilesRadioButton.Checked;
      Directories.Instance.GlobalListFile = localSettingsFile.GlobalListFiles;
      if (!localSettingsFile.GlobalListFiles)
        new DirectoryPermissions().CreateNonGlobalUserDataDirectory();
      localSettingsFile.PlayFinishedSound = this.playSoundRadioButton.Checked || !this.noSoundRadioButton.Checked && false;
      localSettingsFile.PlayDefaultSound = this.defaultSoundRadioButton.Checked || !this.userSoundRadioButton.Checked;
      localSettingsFile.SoundFile = this.soundFilePathTextBox.Text;
      FinishSound.Instance.UpdateSoundSettings(localSettingsFile);
      LocalSettingsFile.WriteFile(localSettingsFile);
      if (this.englishRadioButton.Checked)
      {
        Settings.Default.Language = "en-US";
        if (Thread.CurrentThread.CurrentUICulture.ToString() != Settings.Default.Language)
        {
          int num = (int) MessageBox.Show(GlobalResource.MainWindow_englischToolStripMenuItem_Click_Message, GlobalResource.MainWindow_englischToolStripMenuItem_Click_Caption);
        }
      }
      else if (this.germanRadioButton.Checked)
      {
        Settings.Default.Language = "de-DE";
        if (Thread.CurrentThread.CurrentUICulture.ToString() != Settings.Default.Language)
        {
          int num = (int) MessageBox.Show(GlobalResource.MainWindow_germanToolStripMenuItem_Click_Message, GlobalResource.MainWindow_germanToolStripMenuItem_Click_Caption);
        }
      }
      else
      {
        Settings.Default.Language = "en-US";
        if (Thread.CurrentThread.CurrentUICulture.ToString() != Settings.Default.Language)
        {
          int num = (int) MessageBox.Show(GlobalResource.MainWindow_englischToolStripMenuItem_Click_Message, GlobalResource.MainWindow_englischToolStripMenuItem_Click_Caption);
        }
      }
      Settings.Default.Save();
      this.saveOptionsButton.Enabled = false;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void UpdateProxySettings()
    {
      switch (WebSettings.ReadFile().TypeOfProxy)
      {
        case WebSettings.ProxyType.NO_PROXY:
          this.noProxyRadioButton.Checked = true;
          break;
        case WebSettings.ProxyType.DEFAULT_PROXY:
          this.systemProxyRadioButton.Checked = true;
          break;
        case WebSettings.ProxyType.MANUAL_PROXY:
          this.manualProxyRadioButton.Checked = true;
          break;
        default:
          this.noProxyRadioButton.Checked = true;
          break;
      }
    }

    private void ReadOptionSettings()
    {
      LocalSettingsFile localSettingsFile = LocalSettingsFile.ReadFile();
      if (localSettingsFile.GlobalListFiles)
      {
        this.globalFilesRadioButton.Checked = true;
        this.userFilesRadioButton.Checked = false;
      }
      else
      {
        this.globalFilesRadioButton.Checked = false;
        this.userFilesRadioButton.Checked = true;
      }
      switch (new CultureInfo(Settings.Default.Language).Name)
      {
        case "en-US":
          this.englishRadioButton.Checked = true;
          this.germanRadioButton.Checked = false;
          break;
        case "de-DE":
          this.englishRadioButton.Checked = false;
          this.germanRadioButton.Checked = true;
          break;
        default:
          this.englishRadioButton.Checked = true;
          this.germanRadioButton.Checked = false;
          break;
      }
      if (localSettingsFile.PlayFinishedSound)
      {
        this.playSoundRadioButton.Checked = true;
        this.noSoundRadioButton.Checked = false;
      }
      else
      {
        this.playSoundRadioButton.Checked = false;
        this.noSoundRadioButton.Checked = true;
      }
      if (localSettingsFile.PlayDefaultSound)
      {
        this.defaultSoundRadioButton.Checked = true;
        this.userSoundRadioButton.Checked = false;
        this.chooseSoundButton.Enabled = false;
        this.soundFilePathTextBox.Text = localSettingsFile.SoundFile;
      }
      else
      {
        this.defaultSoundRadioButton.Checked = false;
        this.userSoundRadioButton.Checked = true;
        this.chooseSoundButton.Enabled = true;
        this.soundFilePathTextBox.Text = localSettingsFile.SoundFile;
      }
    }

    private void ReadRegSettings()
    {
      if (!RegistrationInformation.RegistrationInformationExists)
        return;
      RegistrationInformation registrationInformation = RegistrationInformation.ReadFile();
      this.contactPersonTextBox.Text = registrationInformation.ContactPerson;
      this.companyTextBox.Text = registrationInformation.ComanyName;
      this.streetTextBox.Text = registrationInformation.Street;
      this.streetNumberTextBox.Text = registrationInformation.StreetNumber;
      this.zipTextBox.Text = registrationInformation.Zip;
      this.cityTextBox.Text = registrationInformation.City;
      this.Country = registrationInformation.Country;
      this.LCID = registrationInformation.LCID;
      this.eMailTextBox.Text = registrationInformation.EMail;
      this.telephoneTextBox.Text = registrationInformation.PhoneNumber;
    }

    public int LCID
    {
      set
      {
        bool flag = false;
        foreach (KeyValuePair<string, int> country in this.countryDictionary)
        {
          if (country.Value == value)
          {
            flag = true;
            this.countryComboBox.SelectedItem = (object) country.Key;
            this.lcId = country.Value;
          }
        }
        if (flag)
          return;
        this.LCID = this.defaultLCID;
        this.lcId = this.defaultLCID;
      }
      get => this.lcId;
    }

    public string Country
    {
      set
      {
        if (this.countryDictionary.ContainsKey(value))
        {
          this.countryComboBox.SelectedItem = (object) value;
          this.country = value;
        }
        else
          this.LCID = this.defaultLCID;
      }
      get => this.country;
    }

    private void proxySettingButton_Click(object sender, EventArgs e)
    {
      if (new ProxySettings().ShowDialog() != DialogResult.OK)
        return;
      this.UpdateProxySettings();
      this.saveOptionsButton.Enabled = true;
      int num = (int) MessageBox.Show(GlobalResource.ProxySettings_Information_Message, GlobalResource.ProxySettings_Information_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void InitCountryDictionary()
    {
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      try
      {
        foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures | CultureTypes.InstalledWin32Cultures))
        {
          try
          {
            RegionInfo regionInfo = new RegionInfo(culture.LCID);
            if (!dictionary.ContainsKey(regionInfo.DisplayName))
              dictionary.Add(regionInfo.DisplayName, culture.LCID);
          }
          catch (Exception ex)
          {
          }
        }
      }
      catch (Exception ex)
      {
      }
      this.countryDictionary = dictionary;
    }

    private bool requiredFieldsFilled()
    {
      bool flag = true;
      if (string.IsNullOrEmpty(this.companyTextBox.Text))
      {
        this.companyLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      if (string.IsNullOrEmpty(this.streetTextBox.Text))
      {
        this.streetLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      if (string.IsNullOrEmpty(this.streetNumberTextBox.Text))
      {
        this.streetNumberLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      if (string.IsNullOrEmpty(this.zipTextBox.Text))
      {
        this.zipLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      if (string.IsNullOrEmpty(this.cityTextBox.Text))
      {
        this.cityLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      if (string.IsNullOrEmpty((string) this.countryComboBox.SelectedItem))
      {
        this.countryLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      if (string.IsNullOrEmpty(this.eMailTextBox.Text))
      {
        this.eMailLabel.ForeColor = this.highlightingColor;
        flag = false;
      }
      return flag;
    }

    private void saveDataButton_Click(object sender, EventArgs e)
    {
      if (!this.requiredFieldsFilled())
      {
        int num = (int) MessageBox.Show(GlobalResource.RegInfoDialog_FillRequired_Message, GlobalResource.RegInfoDialog_FillRequired_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        this.canelLeavingActualTab = true;
      }
      else if (!string.IsNullOrEmpty(this.eMailTextBox.Text) && !Regex.IsMatch(this.eMailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
      {
        int num = (int) MessageBox.Show(GlobalResource.RegInfoDialog_InvalidEMail_Message, GlobalResource.RegInfoDialog_InvalidEMail_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        this.canelLeavingActualTab = true;
      }
      else if (!this.userAgreementCheckBox.Checked)
      {
        int num = (int) MessageBox.Show(GlobalResource.RegInfoDialog_AcceptEProcessing_Message, GlobalResource.RegInfoDialog_AcceptEProcessing_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this.canelLeavingActualTab = true;
      }
      else
      {
        MainWindow.Instance.Cursor = Cursors.WaitCursor;
        RegistrationInformation content = new RegistrationInformation();
        content.ContactPerson = this.contactPersonTextBox.Text;
        content.ComanyName = this.companyTextBox.Text;
        content.Street = this.streetTextBox.Text;
        content.StreetNumber = this.streetNumberTextBox.Text;
        content.Zip = this.zipTextBox.Text;
        content.City = this.cityTextBox.Text;
        content.Country = (string) this.countryComboBox.SelectedItem;
        this.LCID = !this.countryDictionary.ContainsKey((string) this.countryComboBox.SelectedItem) ? this.defaultLCID : this.countryDictionary[(string) this.countryComboBox.SelectedItem];
        content.LCID = this.LCID;
        content.EMail = this.eMailTextBox.Text;
        content.PhoneNumber = this.telephoneTextBox.Text;
        RegistrationInformation.WriteFile(content);
        this.companyLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.streetLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.streetNumberLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.zipLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.cityLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.countryLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.eMailLabel.ForeColor = ColorDefinition.BlackTextColor;
        this.userAgreementCheckBox.Checked = false;
        this.saveDataButton.Enabled = false;
        MainWindow.Instance.Cursor = Cursors.Default;
      }
    }

    private void deactivateTestModeButton_Click(object sender, EventArgs e)
    {
      this.deactivateTestModeButton.Visible = false;
      this.activateTestModeButton.Visible = true;
      MainWindow.Instance.Testmode = false;
      this.firmwareListPanel.Visible = false;
      this.firmwareTranmissionOkPictureBox.Visible = false;
      this.addFirmarePanel.Visible = false;
    }

    private void activateTestModeButton_Click(object sender, EventArgs e)
    {
      this.activateTestModeButton.Visible = false;
      this.deactivateTestModeButton.Visible = true;
      MainWindow.Instance.Testmode = true;
      this.firmwareListPanel.Visible = true;
      this.firmwareTranmissionOkPictureBox.Visible = true;
      this.addFirmarePanel.Visible = true;
      this.InitTestMode();
      this.FillFirmwareTablePanel();
      this.addFirmwareTableLayout.Size = new Size(this.firmwareListTableLayoutPanel.Width, this.addFirmwareTableLayout.Height);
      this.addFirmwareTableLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
      HelperClass.DoEvents();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
    }

    private void radioButton_CheckedChanged(object sender, EventArgs e) => this.saveOptionsButton.Enabled = true;

    private void englishLabel_Click(object sender, EventArgs e) => this.englishRadioButton.Checked = true;

    private void germanLabel_Click(object sender, EventArgs e) => this.germanRadioButton.Checked = true;

    private void privacyPolicyLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      int num = (int) new PrivacyPolicyDialog().ShowDialog();
    }

    private void userAgreementCheckBox_CheckStateChanged(object sender, EventArgs e) => this.saveDataButton.Enabled = true;

    private void dataTextChanged(object sender, EventArgs e) => this.saveDataButton.Enabled = true;

    private void countryComboBox_SelectedIndexChanged(object sender, EventArgs e) => this.saveDataButton.Enabled = true;

    private void addEngineFirmwareButton_Click(object sender, EventArgs e) => this.FirmwareFileDialog(Directories.Instance.MotorFirmwarePath, FirmwareListFile.FirmwareType.MOTOR, ((Control) sender).Text);

    private void addMMIFirmwareButton_Click(object sender, EventArgs e) => this.FirmwareFileDialog(Directories.Instance.MMIFirmwarePath, FirmwareListFile.FirmwareType.MMI, ((Control) sender).Text);

    private void addConnectMMIFirmwareButton_Click(object sender, EventArgs e) => this.FirmwareFileDialog(Directories.Instance.ConnectMMIFirmwarePath, FirmwareListFile.FirmwareType.CONNECT_MMI, ((Control) sender).Text);

    private void addBatteryFirmwareButton_Click(object sender, EventArgs e) => this.FirmwareFileDialog(Directories.Instance.BatteryFirmwarePath, FirmwareListFile.FirmwareType.ACCU, ((Control) sender).Text);

    private void addDFIFirmwareButton_Click(object sender, EventArgs e) => this.FirmwareFileDialog(Directories.Instance.DFIFirmwarePath, FirmwareListFile.FirmwareType.DFI, ((Control) sender).Text);

    private void FirmwareFileDialog(
      string initialDirectory,
      FirmwareListFile.FirmwareType type,
      string menuText)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Title = menuText + " Firmware";
      openFileDialog.Multiselect = false;
      openFileDialog.InitialDirectory = initialDirectory;
      string empty1 = string.Empty;
      if (type != FirmwareListFile.FirmwareType.DFI)
        openFileDialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", (object) openFileDialog.Filter, (object) empty1, (object) "Firmware (*.hex)", (object) "*.hex");
      else
        openFileDialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", (object) openFileDialog.Filter, (object) empty1, (object) "Firmware (*.dfi)", (object) "*.dfi");
      openFileDialog.FilterIndex = 0;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      string empty2 = string.Empty;
      string firmwareManualAdded = GlobalResource.TestMode_Firmware_ManualAdded;
      if (FirmwareVersionInput.Show((FirmwareVersionInput.FirmwareType) type, "Eingabe der Versionsnummer:", "Versionsnummer", "Beschreibung:", GlobalResource.ok, GlobalResource.cancel, ref empty2, ref firmwareManualAdded) != DialogResult.OK)
        return;
      string str = "testmode\\";
      string withoutExtension = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
      string fileName = Path.GetFileName(openFileDialog.FileName);
      string directoryName = initialDirectory + str;
      try
      {
        FileOperation.CreateDirectory(directoryName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(GlobalResource.TestMode_Firmware_ManualAdded_DirCreateFailed, GlobalResource.ToolTip_Title_Warning, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      try
      {
        if (openFileDialog.FileName == directoryName + fileName)
        {
          int num = (int) MessageBox.Show(GlobalResource.TestMode_Firmware_ManualAdded_SameFiles, GlobalResource.ToolTip_Title_Warning, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        if (File.Exists(directoryName + fileName))
        {
          int num = (int) MessageBox.Show(GlobalResource.TestMode_Firmware_ManualAdded_AlreadyExits, GlobalResource.ToolTip_Title_Warning, MessageBoxButtons.OK, MessageBoxIcon.Hand);
          return;
        }
        File.Copy(openFileDialog.FileName, directoryName + fileName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(GlobalResource.TestMode_Firmware_ManualAdded_CopyFailed, GlobalResource.ToolTip_Title_Warning, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return;
      }
      FirmwareListFile content = FirmwareListFile.ReadFile(type);
      content.AddListElement(withoutExtension, str + Path.GetFileName(openFileDialog.FileName), -1, empty2, warning: firmwareManualAdded);
      FirmwareListFile.WriteFile(content, type);
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
      int num1 = (int) MessageBox.Show(GlobalResource.TestMode_Firmware_ManualAdded_Done, GlobalResource.ToolTip_Title_Information, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void testModeTabPage_SizeChanged(object sender, EventArgs e) => this.addFirmwareTableLayout.Size = new Size(this.firmwareListTableLayoutPanel.Width, this.addFirmwareTableLayout.Height);

    private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (!this.SaveChanges())
        return;
      e.Cancel = true;
    }

    public bool ValueChanged => this.saveDataButton.Enabled || this.saveOptionsButton.Enabled;

    public bool SaveChanges()
    {
      this.canelLeavingActualTab = false;
      if (this.saveDataButton.Enabled || this.saveOptionsButton.Enabled)
      {
        if (MessageBox.Show(GlobalResource.SaveSettingsQuestion_Message, GlobalResource.SaveSettingsQuestion_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          if (this.saveOptionsButton.Enabled)
            this.saveOptionsButton_Click((object) this.saveOptionsButton, new EventArgs());
          else if (this.saveDataButton.Enabled)
            this.saveDataButton_Click((object) this.saveDataButton, new EventArgs());
        }
        else if (this.saveOptionsButton.Enabled)
        {
          this.ReadOptionSettings();
          this.UpdateProxySettings();
          this.saveOptionsButton.Enabled = false;
        }
        else if (this.saveDataButton.Enabled)
        {
          this.ReadRegSettings();
          this.saveDataButton.Enabled = false;
        }
      }
      return this.canelLeavingActualTab;
    }

    private void InitTestMode()
    {
      if (this.initPassed)
        return;
      this.motorListLength = 0;
      this.motorFirmwareListElement = new FirmwareListElement[this.motorListLength];
      this.mmiListLength = 0;
      this.mmiFirmwareListElement = new FirmwareListElement[this.mmiListLength];
      this.connectMMIListLength = 0;
      this.connectMMIFirmwareListElement = new FirmwareListElement[this.connectMMIListLength];
      this.accuListLength = 0;
      this.accuFirmwareListElement = new FirmwareListElement[this.accuListLength];
      this.dfiListLength = 0;
      this.dfiFirmwareListElement = new FirmwareListElement[this.dfiListLength];
      this.removeFirmwareFromMMI = new FlickerFreeTableLayoutPanel[3];
      this.removeFirmwareFromMMIPic = new PictureBox[3];
      this.removeFirmwareFromMMILabel = new Label[3];
      this.mmiCom.Removed += new Communication.RemovedEventHandler(this.MMIDettachedHandler);
      MainWindow.Instance.HelloLoopPassed += new MainWindow.HelloLoopPassedEventHandler(this.HelloLoopPassedHandler);
      MainWindow.Instance.MMIInformationChanged += new MainWindow.MMIInformationChangedEventHandler(this.MMIInformationChanged);
      this.installedAccuFirmwareVersion = MMIData.Instance.AccuFirmwareVersion;
      this.installedMMIFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
      this.installedConnectMMIFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
      this.installedMotorFirmwareVersion = MMIData.Instance.MotorFirmwareVersion;
      this.installedDFIFirmwareVersion = MMIData.Instance.AccuDFIVersion;
      this.mmiSerialNumber = MMIData.Instance.MMISerialNumber;
      this.initPassed = true;
    }

    public void UpdateFirmwareLists()
    {
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
    }

    private void FillFirmwareTablePanel()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.firmwareListTableLayoutPanel.SuspendLayout();
      this.firmwareListTableLayoutPanel.Visible = false;
      this.RemoveFirmwareElements(this.motorFirmwareListElement);
      this.RemoveFirmwareElements(this.mmiFirmwareListElement);
      this.RemoveFirmwareElements(this.connectMMIFirmwareListElement);
      this.RemoveFirmwareElements(this.accuFirmwareListElement);
      this.RemoveFirmwareElements(this.dfiFirmwareListElement);
      this.RemoveRemoveButtons();
      this.firmwareListTableLayoutPanel.RowCount = 1;
      this.motorList = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MOTOR);
      this.motorListLength = this.motorList.Length;
      this.motorFirmwareListElement = new FirmwareListElement[this.motorListLength];
      this.connectMMIList = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.CONNECT_MMI);
      this.connectMMIListLength = this.connectMMIList.Length;
      this.connectMMIFirmwareListElement = new FirmwareListElement[this.connectMMIListLength];
      this.mmiList = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.MMI);
      this.mmiListLength = this.mmiList.Length;
      this.mmiFirmwareListElement = new FirmwareListElement[this.mmiListLength];
      this.accuList = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.ACCU);
      this.accuListLength = this.accuList.Length;
      this.accuFirmwareListElement = new FirmwareListElement[this.accuListLength];
      this.dfiList = FirmwareListFile.ReadFile(FirmwareListFile.FirmwareType.DFI);
      this.dfiListLength = this.dfiList.Length;
      this.dfiFirmwareListElement = new FirmwareListElement[this.dfiListLength];
      this.firmwareListTableLayoutPanel.RowStyles.Clear();
      this.firmwareListTableLayoutPanel.ColumnStyles.Clear();
      this.firmwareListTableLayoutPanel.ColumnCount = 5;
      for (int index = 0; index < this.firmwareListTableLayoutPanel.ColumnCount; ++index)
        this.firmwareListTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
      this.FillFirmwareTablePanelWithFirmwares(this.connectMMIList, this.connectMMIListLength, this.connectMMIFirmwareListElement, new EventHandler(this.connectMMIElementTransmitPictureBox_Click), new EventHandler(this.connectMMIElementDeletePictureBox_Click), new EventHandler(this.connectMMIElementInfoPictureBox_Click), 0);
      this.FillFirmwareTablePanelWithFirmwares(this.mmiList, this.mmiListLength, this.mmiFirmwareListElement, new EventHandler(this.mmiElementTransmitPictureBox_Click), new EventHandler(this.mmiElementDeletePictureBox_Click), new EventHandler(this.mmiElementInfoPictureBox_Click), 1);
      this.FillFirmwareTablePanelWithFirmwares(this.motorList, this.motorListLength, this.motorFirmwareListElement, new EventHandler(this.motorElementTransmitPictureBox_Click), new EventHandler(this.motorElementDeletePictureBox_Click), new EventHandler(this.motorElementInfoPictureBox_Click), 2);
      this.FillFirmwareTablePanelWithFirmwares(this.accuList, this.accuListLength, this.accuFirmwareListElement, new EventHandler(this.accuElementTransmitPictureBox_Click), new EventHandler(this.accuElementDeletePictureBox_Click), new EventHandler(this.accuElementInfoPictureBox_Click), 3);
      this.FillFirmwareTablePanelWithFirmwares(this.dfiList, this.dfiListLength, this.dfiFirmwareListElement, new EventHandler(this.dfiElementTransmitPictureBox_Click), new EventHandler(this.dfiElementDeletePictureBox_Click), new EventHandler(this.dfiElementInfoPictureBox_Click), 4);
      this.firmwareListTableLayoutPanel.RowCount = this.GetMaxNumberOfRows(this.motorListLength - this.NumberOfInvisbleElements(this.motorList), this.mmiListLength - this.NumberOfInvisbleElements(this.mmiList), this.connectMMIListLength - this.NumberOfInvisbleElements(this.connectMMIList), this.accuListLength - this.NumberOfInvisbleElements(this.accuList), this.dfiListLength - this.NumberOfInvisbleElements(this.dfiList)) + 1;
      this.AddRemoveButtonsToTable();
      this.firmwareListTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      for (int index = 1; index < this.firmwareListTableLayoutPanel.RowCount; ++index)
        this.firmwareListTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      this.firmwareListTableLayoutPanel.ResumeLayout(false);
      this.firmwareListTableLayoutPanel.PerformLayout();
      if (this.firmwareListTableLayoutPanel.AutoSize)
        this.firmwareListTableLayoutPanel.Size = this.firmwareListTableLayoutPanel.GetPreferredSize(this.firmwareListTableLayoutPanel.Size);
      this.firmwareListTableLayoutPanel.Visible = true;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void RemoveFirmwareElements(FirmwareListElement[] firmwareListElement)
    {
      for (int index = 0; index < firmwareListElement.Length; ++index)
      {
        if (firmwareListElement[index] != null)
          firmwareListElement[index].Dispose();
      }
      firmwareListElement = (FirmwareListElement[]) null;
    }

    private void RemoveRemoveButtons()
    {
      for (int index = 0; index < this.removeFirmwareFromMMI.Length; ++index)
      {
        if (this.removeFirmwareFromMMI[index] != null)
          this.removeFirmwareFromMMI[index].Dispose();
      }
    }

    private void AddRemoveButtonsToTable()
    {
      for (int index = 0; index < this.removeFirmwareFromMMI.Length; ++index)
      {
        this.removeFirmwareFromMMI[index] = new FlickerFreeTableLayoutPanel();
        this.removeFirmwareFromMMI[index].RowCount = 1;
        this.removeFirmwareFromMMI[index].ColumnCount = 2;
        this.removeFirmwareFromMMI[index].Anchor = AnchorStyles.None;
        this.removeFirmwareFromMMI[index].Dock = DockStyle.Fill;
        this.removeFirmwareFromMMI[index].AutoSize = true;
        this.removeFirmwareFromMMI[index].BackColor = Color.Transparent;
        this.removeFirmwareFromMMI[index].ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
        this.removeFirmwareFromMMI[index].ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        this.removeFirmwareFromMMI[index].RowStyles.Add(new RowStyle(SizeType.AutoSize));
        this.removeFirmwareFromMMIPic[index] = new PictureBox();
        this.removeFirmwareFromMMIPic[index].Image = (Image) Resources.trashcan;
        this.removeFirmwareFromMMIPic[index].Anchor = AnchorStyles.None;
        this.removeFirmwareFromMMIPic[index].SizeMode = PictureBoxSizeMode.AutoSize;
        this.removeFirmwareFromMMIPic[index].BackColor = Color.Transparent;
        this.removeFirmwareFromMMIPic[index].Cursor = Cursors.Hand;
        this.removeFirmwareFromMMIPic[index].Click += new EventHandler(this.RemoveFirmwareButton_Click);
        this.removeFirmwareFromMMILabel[index] = new Label();
        this.removeFirmwareFromMMILabel[index].Text = string.Format(GlobalResource.UpdatePanel_RemoveButtons_Text, (object) "");
        this.removeFirmwareFromMMILabel[index].Font = FontDefinition.DefaultTextFont;
        this.removeFirmwareFromMMILabel[index].TextAlign = ContentAlignment.MiddleLeft;
        this.removeFirmwareFromMMILabel[index].Anchor = AnchorStyles.Left | AnchorStyles.Right;
        this.removeFirmwareFromMMILabel[index].BackColor = Color.Transparent;
        this.removeFirmwareFromMMI[index].Controls.Add((Control) this.removeFirmwareFromMMILabel[index], 0, 0);
        this.removeFirmwareFromMMI[index].Controls.Add((Control) this.removeFirmwareFromMMIPic[index], 1, 0);
        this.firmwareListTableLayoutPanel.Controls.Add((Control) this.removeFirmwareFromMMI[index], index + 2, this.firmwareListTableLayoutPanel.RowCount);
      }
      ++this.firmwareListTableLayoutPanel.RowCount;
    }

    private int NumberOfInvisbleElements(FirmwareListFile listContent)
    {
      int num = 0;
      if (MainWindow.Instance.Testmode)
        return num;
      for (int position = listContent.Length - 1; position >= 0; --position)
      {
        if (listContent.ServerId(position) == -1)
          ++num;
      }
      return num;
    }

    private void FillFirmwareTablePanelWithFirmwares(
      FirmwareListFile listContent,
      int listLength,
      FirmwareListElement[] firmwareListElement,
      EventHandler transmitHandler,
      EventHandler deleteHandler,
      EventHandler infoHandler,
      int columnToFill)
    {
      int num1 = 0;
      if (MainWindow.Instance.Testmode)
      {
        for (int position = listLength - 1; position >= 0; --position)
        {
          if (listContent.ServerId(position) == -1)
            ++num1;
        }
      }
      for (int counter = 0; counter < listLength - num1; ++counter)
        this.FillSingleFirmwareElement(listContent, listLength, firmwareListElement, transmitHandler, deleteHandler, infoHandler, columnToFill, counter + num1 + 1, counter);
      if (!MainWindow.Instance.Testmode)
        return;
      int num2 = 1;
      for (int counter = listLength - num1; counter < listLength; ++counter)
      {
        this.FillSingleFirmwareElement(listContent, listLength, firmwareListElement, transmitHandler, deleteHandler, infoHandler, columnToFill, num2, counter);
        this.firmwareListTableLayoutPanel.Controls.Add((Control) firmwareListElement[counter], columnToFill, num2);
        ++num2;
      }
    }

    private void FillSingleFirmwareElement(
      FirmwareListFile listContent,
      int listLength,
      FirmwareListElement[] firmwareListElement,
      EventHandler transmitHandler,
      EventHandler deleteHandler,
      EventHandler infoHandler,
      int columnToFill,
      int rowPosition,
      int counter)
    {
      if (firmwareListElement[counter] == null)
        firmwareListElement[counter] = new FirmwareListElement();
      firmwareListElement[counter].SuspendLayout();
      firmwareListElement[counter].Size = new Size(230, 47);
      firmwareListElement[counter].Dock = DockStyle.Fill;
      firmwareListElement[counter].Name = string.Concat((object) counter);
      firmwareListElement[counter].FirmwareVersionLabel = listContent.Version(counter);
      firmwareListElement[counter].FirmwareVersionToolTip = listContent.Version(counter) + "\n" + listContent.Warning(counter);
      firmwareListElement[counter].WarningInfo = listContent.Warning(counter);
      firmwareListElement[counter].AddTransmitClickEvent = transmitHandler;
      firmwareListElement[counter].TransmitCursor = Cursors.Hand;
      firmwareListElement[counter].TransmitImage = (Image) Resources.Icon_sTransceive_Inactive;
      firmwareListElement[counter].TransmitToolTip = GlobalResource.FirmwareList_Transmit_ToolTip;
      firmwareListElement[counter].AddTrashClickEvent = deleteHandler;
      firmwareListElement[counter].TrashCursor = Cursors.Hand;
      firmwareListElement[counter].TrashImage = (Image) Resources.trashcan;
      firmwareListElement[counter].TrashToolTip = GlobalResource.FirmwareList_Delete_ToolTip;
      firmwareListElement[counter].AddInfoClickEvent = infoHandler;
      firmwareListElement[counter].InfoCursor = Cursors.Hand;
      firmwareListElement[counter].InfoImage = (Image) Resources.buttons_information;
      firmwareListElement[counter].InfoToolTip = GlobalResource.FirmwareList_Info_ToolTip;
      firmwareListElement[counter].IsCritical = listContent.Critical(counter);
      if (firmwareListElement[counter].AutoSize)
        firmwareListElement[counter].Size = firmwareListElement[counter].GetPreferredSize(firmwareListElement[counter].Size);
      firmwareListElement[counter].ResumeLayout(false);
      firmwareListElement[counter].PerformLayout();
      if (listContent.ServerId(counter) == -1)
        return;
      this.firmwareListTableLayoutPanel.Controls.Add((Control) firmwareListElement[counter], columnToFill, rowPosition);
    }

    private void RemoveFirmwareEnabled(bool value)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string str3 = string.Empty;
      if (value)
      {
        str1 = this.transceiver.ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps.FIRMWARE_MOTOR);
        str2 = this.transceiver.ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps.FIRMWARE_ACCU);
        str3 = this.transceiver.ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps.ACCU_DFI_FILE);
      }
      for (int index = 0; index < this.removeFirmwareFromMMI.Length; ++index)
      {
        if (this.removeFirmwareFromMMI[index] != null)
        {
          this.removeFirmwareFromMMI[index].Enabled = value;
          if (value)
          {
            this.removeFirmwareFromMMIPic[index].Image = (Image) Resources.trashcan;
            if (index == 0 && !string.IsNullOrEmpty(str1) && !str1.Equals("0"))
              this.removeFirmwareFromMMILabel[index].Text = string.Format(GlobalResource.UpdatePanel_RemoveButtons_Text, (object) str1);
            else if (index == 1 && !string.IsNullOrEmpty(str2) && !str2.Equals("0"))
              this.removeFirmwareFromMMILabel[index].Text = string.Format(GlobalResource.UpdatePanel_RemoveButtons_Text, (object) str2);
            else if (index == 2 && !string.IsNullOrEmpty(str3) && !str3.Equals("0x00 0x00"))
            {
              this.removeFirmwareFromMMILabel[index].Text = string.Format(GlobalResource.UpdatePanel_RemoveButtons_Text, (object) str3);
            }
            else
            {
              this.removeFirmwareFromMMIPic[index].Image = (Image) Resources.trashcan_gray;
              this.removeFirmwareFromMMILabel[index].Text = string.Format(GlobalResource.UpdatePanel_RemoveButtons_Text, (object) "");
              this.removeFirmwareFromMMI[index].Enabled = false;
            }
          }
          else
          {
            this.removeFirmwareFromMMIPic[index].Image = (Image) Resources.trashcan_gray;
            this.removeFirmwareFromMMILabel[index].Text = string.Format(GlobalResource.UpdatePanel_RemoveButtons_Text, (object) "");
          }
        }
      }
    }

    private void motorElementInfoPictureBox_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      int num = (int) MessageBox.Show(this.motorList.Description(int32), this.motorList.Version(int32));
    }

    private void accuElementInfoPictureBox_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      int num = (int) MessageBox.Show(this.accuList.Description(int32), this.accuList.Version(int32));
    }

    private void dfiElementInfoPictureBox_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      int num = (int) MessageBox.Show(this.dfiList.Description(int32), this.dfiList.Version(int32));
    }

    private void connectMMIElementInfoPictureBox_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      int num = (int) MessageBox.Show(this.connectMMIList.Description(int32), this.connectMMIList.Version(int32));
    }

    private void mmiElementInfoPictureBox_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      int num = (int) MessageBox.Show(this.mmiList.Description(int32), this.mmiList.Version(int32));
    }

    private void motorElementTransmitPictureBox_Click(object sender, EventArgs e)
    {
      if (!this.hasMMITheRightVersion())
        return;
      this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      if (MessageBox.Show(GlobalResource.Update_Firmware_Transmit_MessageBox_Message, GlobalResource.Update_Firmware_Transmit_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      MainWindow.Instance.ForceDiagnosePanelUpdate();
      DisconnectWarning warningDialog = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      warningDialog.Show((IWin32Window) MainWindow.Instance);
      warningDialog.ProgressBar().AdditionalText = string.Empty;
      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
      HelperClass.DoEvents();
      if (!this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.MotorBootloaderFileName))
      {
        this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
      }
      else
      {
        int maximum1 = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
        int actualPosition1 = 0;
        if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.BOOTLOADER_MOTOR, ref actualPosition1, maximum1, warningDialog.ProgressBar()))
        {
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        }
        else
        {
          this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
          int int32 = Convert.ToInt32(((Control) sender).Name);
          if (!this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.MotorFirmwarePath + this.motorList.FirmwareFilename(int32)))
          {
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          }
          else
          {
            this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
            int maximum2 = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
            int actualPosition2 = 0;
            if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_MOTOR, ref actualPosition2, maximum2, warningDialog.ProgressBar(), true, this.motorList.Version(int32)))
              this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
            else if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.MOTOR))
            {
              this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
            }
            else
            {
              try
              {
                FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(this.mmiSerialNumber, this.motorList.Version(int32), this.motorList.ServerId(int32), FirmwareListFile.FirmwareType.MOTOR));
              }
              catch (Exception ex)
              {
              }
              this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
              warningDialog.Dispose();
              this.RemoveFirmwareEnabled(this.mmiCom.DeviceConnected);
              MMIData.Instance.SavedMotorFirmwareVersion = this.motorList.Version(int32);
              MainWindow.Instance.ForceDiagnosePanelUpdate();
              MainWindow.Instance.Cursor = Cursors.Default;
              this.firmwareTranmissionOkPictureBox.Image = (Image) Resources.button_ok;
              FinishSound.Instance.Play();
              int num = (int) MessageBox.Show(GlobalResource.FirmwareTransmissionSucceeded_Message, GlobalResource.FirmwareTransmissionSucceeded_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
          }
        }
      }
    }

    private void motorElementDeletePictureBox_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.Update_Firmware_Delete_MessageBox_Message, GlobalResource.Update_Firmware_Delete_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int int32 = Convert.ToInt32(((Control) sender).Name);
      this.RemoveFirmwareFile(Directories.Instance.MotorFirmwarePath, this.motorList.FirmwareFilename(int32), this.motorList.ServerId(int32));
      this.motorList.RemoveListElement(int32);
      try
      {
        FirmwareListFile.WriteFile(this.motorList, FirmwareListFile.FirmwareType.MOTOR);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MOTOR_DELETE_LIST_WRITE_ERROR);
        return;
      }
      if (int32 == 0)
      {
        if (this.motorList.Length > 0)
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.MOTOR, this.motorList.ServerId(0), this.motorList.Version(0));
        else
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.MOTOR, 0, "0.0.0.0");
      }
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
    }

    private void accuElementTransmitPictureBox_Click(object sender, EventArgs e)
    {
      if (!this.hasMMITheRightVersion())
        return;
      this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      if (MessageBox.Show(GlobalResource.Update_Firmware_Transmit_MessageBox_Message, GlobalResource.Update_Firmware_Transmit_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      MainWindow.Instance.ForceDiagnosePanelUpdate();
      DisconnectWarning warningDialog = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      warningDialog.Show((IWin32Window) MainWindow.Instance);
      warningDialog.ProgressBar().AdditionalText = string.Empty;
      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
      HelperClass.DoEvents();
      int int32 = Convert.ToInt32(((Control) sender).Name);
      if (!this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.BatteryFirmwarePath + this.accuList.FirmwareFilename(int32)))
      {
        this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
      }
      else
      {
        int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
        int actualPosition = 0;
        if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_ACCU, ref actualPosition, maximum, warningDialog.ProgressBar(), true, this.accuList.Version(int32)))
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        else if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.ACCU))
        {
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        }
        else
        {
          try
          {
            FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(this.mmiSerialNumber, this.accuList.Version(int32), this.accuList.ServerId(int32), FirmwareListFile.FirmwareType.ACCU));
          }
          catch (Exception ex)
          {
          }
          this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
          this.RemoveFirmwareEnabled(this.mmiCom.DeviceConnected);
          warningDialog.Dispose();
          MMIData.Instance.SavedAccuFirmwareVersion = this.accuList.Version(int32);
          MainWindow.Instance.ForceDiagnosePanelUpdate();
          MainWindow.Instance.Cursor = Cursors.Default;
          this.firmwareTranmissionOkPictureBox.Image = (Image) Resources.button_ok;
          FinishSound.Instance.Play();
          int num = (int) MessageBox.Show(GlobalResource.FirmwareTransmissionSucceeded_Message, GlobalResource.FirmwareTransmissionSucceeded_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
    }

    private void accuElementDeletePictureBox_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.Update_Firmware_Delete_MessageBox_Message, GlobalResource.Update_Firmware_Delete_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int int32 = Convert.ToInt32(((Control) sender).Name);
      this.RemoveFirmwareFile(Directories.Instance.BatteryFirmwarePath, this.accuList.FirmwareFilename(int32), this.accuList.ServerId(int32));
      this.accuList.RemoveListElement(int32);
      try
      {
        FirmwareListFile.WriteFile(this.accuList, FirmwareListFile.FirmwareType.ACCU);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.ACCU_DELETE_LIST_WRITE_ERROR);
        return;
      }
      if (int32 == 0)
      {
        if (this.accuList.Length > 0)
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.ACCU, this.accuList.ServerId(0), this.accuList.Version(0));
        else
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.ACCU, 0, "0.0.0.0");
      }
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
    }

    private void dfiElementTransmitPictureBox_Click(object sender, EventArgs e)
    {
      if (!this.hasMMITheRightVersion())
        return;
      this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      if (MessageBox.Show(GlobalResource.Update_Firmware_Transmit_MessageBox_Message, GlobalResource.Update_Firmware_Transmit_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      MainWindow.Instance.ForceDiagnosePanelUpdate();
      DisconnectWarning warningDialog = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      warningDialog.Show((IWin32Window) MainWindow.Instance);
      warningDialog.ProgressBar().AdditionalText = string.Empty;
      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
      HelperClass.DoEvents();
      int int32 = Convert.ToInt32(((Control) sender).Name);
      if (!this.transceiver.ReadBytesFromFile(Directories.Instance.DFIFirmwarePath + this.dfiList.FirmwareFilename(int32)))
      {
        this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
      }
      else
      {
        int maximum = (int) ((double) this.transceiver.FirmwareContentElements / 1000.0) + 1;
        int actualPosition = 0;
        if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.ACCU_DFI_FILE, ref actualPosition, maximum, warningDialog.ProgressBar(), true, this.dfiList.Version(int32)))
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        else if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.DFI))
        {
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        }
        else
        {
          try
          {
            FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(this.mmiSerialNumber, this.dfiList.Version(int32), this.dfiList.ServerId(int32), FirmwareListFile.FirmwareType.DFI));
          }
          catch (Exception ex)
          {
          }
          this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
          this.RemoveFirmwareEnabled(this.mmiCom.DeviceConnected);
          warningDialog.Dispose();
          MMIData.Instance.SavedAccuDFIVersion = this.dfiList.Version(int32);
          MainWindow.Instance.ForceDiagnosePanelUpdate();
          MainWindow.Instance.Cursor = Cursors.Default;
          this.firmwareTranmissionOkPictureBox.Image = (Image) Resources.button_ok;
          FinishSound.Instance.Play();
          int num = (int) MessageBox.Show(GlobalResource.FirmwareTransmissionSucceeded_Message, GlobalResource.FirmwareTransmissionSucceeded_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
      }
    }

    private void dfiElementDeletePictureBox_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.Update_Firmware_Delete_MessageBox_Message, GlobalResource.Update_Firmware_Delete_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int int32 = Convert.ToInt32(((Control) sender).Name);
      this.RemoveFirmwareFile(Directories.Instance.DFIFirmwarePath, this.dfiList.FirmwareFilename(int32), this.dfiList.ServerId(int32));
      this.dfiList.RemoveListElement(int32);
      try
      {
        FirmwareListFile.WriteFile(this.dfiList, FirmwareListFile.FirmwareType.DFI);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.DFI_DELETE_LIST_WRITE_ERROR);
        return;
      }
      if (int32 == 0)
      {
        if (this.dfiList.Length > 0)
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.DFI, this.dfiList.ServerId(0), this.dfiList.Version(0));
        else
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.DFI, 0, "0.0.0.0");
      }
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
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

    private void connectMMIElementTransmitPictureBox_Click(object sender, EventArgs e)
    {
      this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      if (MessageBox.Show(GlobalResource.Update_Firmware_Transmit_MessageBox_Message, GlobalResource.Update_Firmware_Transmit_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning warningDialog = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      warningDialog.Show((IWin32Window) MainWindow.Instance);
      warningDialog.ProgressBar().AdditionalText = string.Empty;
      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
      HelperClass.DoEvents();
      if (!this.RemoveComponentFirmwareFromMMI(warningDialog.ProgressBar()))
      {
        this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
      }
      else
      {
        warningDialog.ProgressBar().AdditionalText = string.Empty;
        int int32 = Convert.ToInt32(((Control) sender).Name);
        if (!this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.ConnectMMIFirmwarePath + this.connectMMIList.FirmwareFilename(int32)))
        {
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        }
        else
        {
          int actualPosition = 0;
          int num1 = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
          int num2 = Parameters.Instance.ParameterElements - 1;
          int maximum = num2 * 2 + num1;
          if (!this.transceiver.ReceiveAllSettingsFromMMI(ref actualPosition, maximum, warningDialog.ProgressBar()))
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          else if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_MMI, ref actualPosition, maximum, warningDialog.ProgressBar()))
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          else if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.MMI))
          {
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            byte[] setOfParameters1 = Parameters.Instance.GetSetOfParameters(ParameterIds.MMI_PRODUCTION_INFORMATION);
            // ISSUE: reference to a compiler-generated method
            byte[] setOfParameters2 = Parameters.Instance.GetSetOfParameters(ParameterIds.ACCU_VERSION_INFORMATION);
            // ISSUE: reference to a compiler-generated method
            byte[] setOfParameters3 = Parameters.Instance.GetSetOfParameters(ParameterIds.MOTOR_VERSION_INFORMATION);
            MainWindow.Instance.ShowMaxSpeedWarning = false;
            if (!this.transceiver.SetUpdateFlag(UpdateFlags.DOUPDATE))
            {
              this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
            }
            else
            {
              warningDialog.Visible = false;
              if (!this.ShowRebootDialog())
              {
                this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
              }
              else
              {
                warningDialog.Visible = true;
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
                if (!this.transceiver.TransferAllSettingsToMMI(ref actualPosition, maximum, warningDialog.ProgressBar()))
                {
                  this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                }
                else
                {
                  Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
                  thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
                  thread.Start();
                  while (thread.IsAlive)
                    Thread.Sleep(50);
                  if (!this.transceiver.WaitingForMMISucceeded)
                    this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                  else if (!this.transceiver.SetUpdateFlag(UpdateFlags.DOUPDATE))
                  {
                    this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                  }
                  else
                  {
                    warningDialog.Visible = false;
                    if (!this.ShowRebootDialog())
                    {
                      this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                    }
                    else
                    {
                      warningDialog.Visible = true;
                      try
                      {
                        FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(this.mmiSerialNumber, this.connectMMIList.Version(int32), this.connectMMIList.ServerId(int32), FirmwareListFile.FirmwareType.CONNECT_MMI));
                      }
                      catch (Exception ex)
                      {
                      }
                      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
                      warningDialog.Dispose();
                      MainWindow.Instance.Cursor = Cursors.Default;
                      this.firmwareTranmissionOkPictureBox.Image = (Image) Resources.button_ok;
                      FinishSound.Instance.Play();
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    private void mmiElementTransmitPictureBox_Click(object sender, EventArgs e)
    {
      this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      if (MessageBox.Show(GlobalResource.Update_Firmware_Transmit_MessageBox_Message, GlobalResource.Update_Firmware_Transmit_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning warningDialog = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      warningDialog.Show((IWin32Window) MainWindow.Instance);
      warningDialog.ProgressBar().AdditionalText = string.Empty;
      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
      HelperClass.DoEvents();
      if (!this.RemoveComponentFirmwareFromMMI(warningDialog.ProgressBar()))
      {
        this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
      }
      else
      {
        warningDialog.ProgressBar().AdditionalText = string.Empty;
        int int32 = Convert.ToInt32(((Control) sender).Name);
        if (!this.transceiver.ReadLinesFromFirmwareFile(Directories.Instance.MMIFirmwarePath + this.mmiList.FirmwareFilename(int32)))
        {
          this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
        }
        else
        {
          int actualPosition = 0;
          int num1 = (int) ((double) this.transceiver.FirmwareContentElements / 2.0 / 1000.0);
          int num2 = Parameters.Instance.ParameterElements - 1;
          int maximum = num2 * 2 + num1;
          if (!this.transceiver.ReceiveAllSettingsFromMMI(ref actualPosition, maximum, warningDialog.ProgressBar()))
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          else if (!this.transceiver.TransferFirmwareToMMI(Command_FirmwareTyps.FIRMWARE_MMI, ref actualPosition, maximum, warningDialog.ProgressBar()))
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          else if (!this.transceiver.SetFirmwareFlag(FirmwareFlagIds.MMI))
          {
            this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
          }
          else
          {
            // ISSUE: reference to a compiler-generated method
            byte[] setOfParameters1 = Parameters.Instance.GetSetOfParameters(ParameterIds.MMI_PRODUCTION_INFORMATION);
            // ISSUE: reference to a compiler-generated method
            byte[] setOfParameters2 = Parameters.Instance.GetSetOfParameters(ParameterIds.ACCU_VERSION_INFORMATION);
            // ISSUE: reference to a compiler-generated method
            byte[] setOfParameters3 = Parameters.Instance.GetSetOfParameters(ParameterIds.MOTOR_VERSION_INFORMATION);
            MainWindow.Instance.ShowMaxSpeedWarning = false;
            if (!this.transceiver.SetUpdateFlag(UpdateFlags.DOUPDATE))
            {
              this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
            }
            else
            {
              warningDialog.Visible = false;
              if (!this.ShowRebootDialog())
              {
                this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
              }
              else
              {
                warningDialog.Visible = true;
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
                if (!this.transceiver.TransferAllSettingsToMMI(ref actualPosition, maximum, warningDialog.ProgressBar()))
                {
                  this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                }
                else
                {
                  Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
                  thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
                  thread.Start();
                  while (thread.IsAlive)
                    Thread.Sleep(50);
                  if (!this.transceiver.WaitingForMMISucceeded)
                    this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                  else if (!this.transceiver.SetUpdateFlag(UpdateFlags.DOUPDATE))
                  {
                    this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                  }
                  else
                  {
                    warningDialog.Visible = false;
                    if (!this.ShowRebootDialog())
                    {
                      this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
                    }
                    else
                    {
                      warningDialog.Visible = true;
                      try
                      {
                        FirmwareTransmission.WriteFile(new FirmwareTransmissionDetails(this.mmiSerialNumber, this.mmiList.Version(int32), this.mmiList.ServerId(int32), FirmwareListFile.FirmwareType.MMI));
                      }
                      catch (Exception ex)
                      {
                      }
                      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
                      warningDialog.Dispose();
                      MainWindow.Instance.Cursor = Cursors.Default;
                      this.firmwareTranmissionOkPictureBox.Image = (Image) Resources.button_ok;
                      FinishSound.Instance.Play();
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    private void connectMMIElementDeletePictureBox_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.Update_Firmware_Delete_MessageBox_Message, GlobalResource.Update_Firmware_Delete_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int int32 = Convert.ToInt32(((Control) sender).Name);
      this.RemoveFirmwareFile(Directories.Instance.ConnectMMIFirmwarePath, this.connectMMIList.FirmwareFilename(int32), this.connectMMIList.ServerId(int32));
      this.connectMMIList.RemoveListElement(int32);
      try
      {
        FirmwareListFile.WriteFile(this.connectMMIList, FirmwareListFile.FirmwareType.CONNECT_MMI);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MMI_DELETE_LIST_WRITE_ERROR);
        return;
      }
      if (int32 == 0)
      {
        if (this.connectMMIList.Length > 0)
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.CONNECT_MMI, this.connectMMIList.ServerId(0), this.connectMMIList.Version(0));
        else
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.CONNECT_MMI, 0, "0.0.0.0");
      }
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
    }

    private void mmiElementDeletePictureBox_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.Update_Firmware_Delete_MessageBox_Message, GlobalResource.Update_Firmware_Delete_MessageBox_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      int int32 = Convert.ToInt32(((Control) sender).Name);
      this.RemoveFirmwareFile(Directories.Instance.MMIFirmwarePath, this.mmiList.FirmwareFilename(int32), this.mmiList.ServerId(int32));
      this.mmiList.RemoveListElement(int32);
      try
      {
        FirmwareListFile.WriteFile(this.mmiList, FirmwareListFile.FirmwareType.MMI);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MMI_DELETE_LIST_WRITE_ERROR);
        return;
      }
      if (int32 == 0)
      {
        if (this.mmiList.Length > 0)
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.MMI, this.mmiList.ServerId(0), this.mmiList.Version(0));
        else
          this.UpdateLatestVersionsFile(LatestVersionsFile.Element.MMI, 0, "0.0.0.0");
      }
      this.FillFirmwareTablePanel();
      this.FunctionsEnabled(this.mmiCom.DeviceConnected);
    }

    private void RemoveFirmwareFile(string firmwarePath, string pathOfFile, int serverVersion)
    {
      try
      {
        if (File.GetAttributes(firmwarePath + pathOfFile) != FileAttributes.Archive)
          File.SetAttributes(firmwarePath + pathOfFile, FileAttributes.Archive);
        File.Delete(firmwarePath + pathOfFile);
        if (serverVersion < 0)
          return;
        Directory.Delete(firmwarePath + serverVersion.ToString("00000000000"));
      }
      catch (Exception ex)
      {
        GlobalLogger.Instance.WriteLine(ex);
        UniqueError.Message(UniqueError.Number.FIRMWAREFILE_DELETE_ERROR, firmwarePath + pathOfFile);
      }
    }

    private void RemoveFirmwareButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.UpdatePanel_RemoveFirmware_Message, GlobalResource.UpdatePanel_RemoveFirmware_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        return;
      int num = -1;
      for (int index = 0; index < this.removeFirmwareFromMMI.Length; ++index)
      {
        if ((PictureBox) sender == this.removeFirmwareFromMMIPic[index])
          num = index;
      }
      DisconnectWarning warningDialog = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      warningDialog.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      this.firmwareTranmissionOkPictureBox.Refresh();
      this.UpdateProgessBarValue(warningDialog.ProgressBar(), 0);
      bool flag1 = false;
      switch (num)
      {
        case 0:
          bool flag2 = this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.BOOTLOADER_MOTOR);
          if (flag2)
            this.UpdateProgessBarValue(warningDialog.ProgressBar(), 50);
          bool flag3 = this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.FIRMWARE_MOTOR);
          if (flag2 && flag3)
          {
            flag1 = true;
            MMIData.Instance.SavedMotorFirmwareVersion = "0.0";
            break;
          }
          flag1 = false;
          break;
        case 1:
          flag1 = this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.FIRMWARE_ACCU);
          if (flag1)
          {
            MMIData.Instance.SavedAccuFirmwareVersion = "0.0";
            break;
          }
          break;
        case 2:
          flag1 = this.transceiver.RemoveFirmwareFromMMI(Command_FirmwareTyps.ACCU_DFI_FILE);
          if (flag1)
          {
            MMIData.Instance.SavedAccuDFIVersion = "0x00 0x00";
            break;
          }
          break;
      }
      if (!flag1)
      {
        this.ErrorOccurred(warningDialog.ProgressBar(), this.firmwareTranmissionOkPictureBox, warningDialog);
      }
      else
      {
        this.RemoveFirmwareEnabled(this.mmiCom.DeviceConnected);
        this.UpdateProgessBarValue(warningDialog.ProgressBar(), 100);
        this.firmwareTranmissionOkPictureBox.Image = (Image) Resources.button_ok;
        MainWindow.Instance.ForceDiagnosePanelUpdate();
        warningDialog.Dispose();
        MainWindow.Instance.Cursor = Cursors.Default;
      }
    }

    private int GetMaxNumberOfRows(
      int numOfMotor,
      int numOfMMI,
      int numOfConnectMMI,
      int numOfAccu,
      int numOfDFI)
    {
      int num = numOfMotor <= numOfMMI ? numOfMMI : numOfMotor;
      if (num < numOfAccu)
        num = numOfAccu;
      if (num < numOfDFI)
        num = numOfDFI;
      return num;
    }

    private bool hasMMITheRightVersion()
    {
      if (HelperClass.IsUpToDate(this.installedMMIFirmwareVersion, "1.3.0.4") || CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
        return true;
      int num = (int) MessageBox.Show(GlobalResource.UpdatePanel_WrongMmiFirmwareVersion_Message, GlobalResource.UpdatePanel_WrongMmiFirmwareVersion_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      return false;
    }

    private void ErrorOccurred(
      ZerroProgressBar progressbar,
      PictureBox picture,
      DisconnectWarning warningDialog)
    {
      this.UpdateProgessBarValue(progressbar, 0);
      warningDialog.Dispose();
      MainWindow.Instance.Cursor = Cursors.Default;
      picture.Image = (Image) Resources.buttons_error;
    }

    private void UpdateProgessBarValue(ZerroProgressBar progressbar, int value)
    {
      progressbar.Value = value;
      progressbar.Refresh();
    }

    private void MMIDettachedHandler(object sender, EventArgs e)
    {
      SettingsPanel.FunctionActive functionActive = new SettingsPanel.FunctionActive(this.FunctionsEnabled);
      try
      {
        this.Invoke((Delegate) functionActive, (object) false);
      }
      catch (Exception ex)
      {
        GlobalLogger.Instance.WriteLine(ex);
      }
    }

    private void HelloLoopPassedHandler(object mainWindow, MainWindowEventArgs args)
    {
      SettingsPanel.FunctionActive functionActive = new SettingsPanel.FunctionActive(this.FunctionsEnabled);
      try
      {
        this.Invoke((Delegate) functionActive, (object) args.Done);
      }
      catch (Exception ex)
      {
        GlobalLogger.Instance.WriteLine(ex);
      }
      if (this.rebootDialog == null)
        return;
      this.rebootDialog.MMIIsBack();
    }

    private void MMIInformationChanged(object sender, EventArgs e)
    {
      this.installedAccuFirmwareVersion = MMIData.Instance.AccuFirmwareVersion;
      if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
        this.installedConnectMMIFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
      else
        this.installedMMIFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
      this.installedMotorFirmwareVersion = MMIData.Instance.MotorFirmwareVersion;
      this.installedDFIFirmwareVersion = MMIData.Instance.AccuDFIVersion;
      this.mmiSerialNumber = MMIData.Instance.MMISerialNumber;
    }

    private void FunctionsEnabled(bool value)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      if (value)
      {
        if (string.IsNullOrEmpty(this.installedMMIFirmwareVersion) && string.IsNullOrEmpty(this.installedConnectMMIFirmwareVersion) && (string.IsNullOrEmpty(this.installedAccuFirmwareVersion) && string.IsNullOrEmpty(this.installedMotorFirmwareVersion)) && string.IsNullOrEmpty(this.installedDFIFirmwareVersion))
        {
          this.installedAccuFirmwareVersion = MMIData.Instance.AccuFirmwareVersion;
          this.installedMMIFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
          this.installedConnectMMIFirmwareVersion = MMIData.Instance.MMIFirmwareVersion;
          this.installedMotorFirmwareVersion = MMIData.Instance.MotorFirmwareVersion;
          this.installedDFIFirmwareVersion = MMIData.Instance.AccuDFIVersion;
          this.mmiSerialNumber = MMIData.Instance.MMISerialNumber;
        }
        this.FirmwareElementsEnabled(value, this.motorFirmwareListElement, this.installedMotorFirmwareVersion);
        if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
        {
          this.FirmwareElementsEnabled(value, this.connectMMIFirmwareListElement, this.installedConnectMMIFirmwareVersion);
          this.FirmwareElementsEnabled(false, this.mmiFirmwareListElement, this.installedMMIFirmwareVersion);
        }
        else
        {
          this.FirmwareElementsEnabled(value, this.mmiFirmwareListElement, this.installedMMIFirmwareVersion);
          this.FirmwareElementsEnabled(false, this.connectMMIFirmwareListElement, this.installedConnectMMIFirmwareVersion);
        }
        this.FirmwareElementsEnabled(value, this.accuFirmwareListElement, this.installedAccuFirmwareVersion);
        if (CommandBuilder.Instance.UseNewFirmwareFlag)
          this.FirmwareElementsEnabled(value, this.dfiFirmwareListElement, this.installedDFIFirmwareVersion);
        else
          this.FirmwareElementsEnabled(false, this.dfiFirmwareListElement, this.installedDFIFirmwareVersion);
        this.RemoveFirmwareEnabled(value);
      }
      else
      {
        this.FirmwareElementsEnabled(value, this.motorFirmwareListElement, string.Empty);
        this.FirmwareElementsEnabled(value, this.mmiFirmwareListElement, string.Empty);
        this.FirmwareElementsEnabled(value, this.connectMMIFirmwareListElement, string.Empty);
        this.FirmwareElementsEnabled(value, this.accuFirmwareListElement, string.Empty);
        this.FirmwareElementsEnabled(value, this.dfiFirmwareListElement, string.Empty);
        this.RemoveFirmwareEnabled(value);
        this.installedAccuFirmwareVersion = string.Empty;
        this.installedMMIFirmwareVersion = string.Empty;
        this.installedConnectMMIFirmwareVersion = string.Empty;
        this.installedMotorFirmwareVersion = string.Empty;
        this.installedDFIFirmwareVersion = string.Empty;
        this.mmiSerialNumber = string.Empty;
        this.firmwareTranmissionOkPictureBox.Image = (Image) null;
      }
      HelperClass.DoEvents();
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private bool EnablesUserControl(bool enable)
    {
      MainWindow.Instance.EnablesUserControl(enable);
      this.Enabled = enable;
      return enable;
    }

    private void UpdateLatestVersionsFile(
      LatestVersionsFile.Element elementToChange,
      int serverID,
      string readableVersion)
    {
      LatestVersionsFile content = LatestVersionsFile.ReadFile();
      content.ReadableVersionID((int) elementToChange, readableVersion);
      content.ServerVersionID((int) elementToChange, serverID);
      try
      {
        LatestVersionsFile.WriteFile(content);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.UPDATE_LATESTVERSIONFILE_WRITE_ERROR);
      }
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

    private void FirmwareElementsEnabled(
      bool value,
      FirmwareListElement[] firmwareList,
      string installedFirmwareVersion)
    {
      if (value)
      {
        foreach (FirmwareListElement firmware in firmwareList)
        {
          if (firmware != null)
          {
            if (!firmware.IsCritical)
            {
              firmware.TransmitImage = (Image) Resources.Icon_sTransceive_Active;
              firmware.TransmitEnabled = true;
            }
            else
            {
              firmware.TransmitImage = (Image) Resources.Icon_sTransceive_Inactive;
              firmware.TransmitEnabled = false;
            }
            if (firmware.FirmwareVersionLabel == installedFirmwareVersion)
            {
              if (!firmware.IsCritical)
              {
                firmware.InstalledInfo = GlobalResource.UpdatePanel_FirmwareInstalled;
              }
              else
              {
                firmware.InstalledInfo = GlobalResource.UpdatePanel_FirmwareInstalled;
                firmware.Highlight(true);
              }
            }
          }
        }
      }
      else
      {
        foreach (FirmwareListElement firmware in firmwareList)
        {
          if (firmware != null)
          {
            firmware.InstalledInfo = string.Empty;
            firmware.Highlight(false);
            firmware.TransmitImage = (Image) Resources.Icon_sTransceive_Inactive;
            firmware.TransmitEnabled = false;
          }
        }
      }
    }

    private delegate void FunctionActive(bool value);
  }
}
