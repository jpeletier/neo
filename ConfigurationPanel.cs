// Decompiled with JetBrains decompiler
// Type: ZerroWare.ConfigurationPanel
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
using System.Media;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class ConfigurationPanel : UserControl
  {
    private IContainer components;
    private TabControl configTabControl;
    private TabPage parameterListTabPage;
    private TabPage startScreensTabPage;
    private TabPage autoTransTabPage;
    private TabPage autoRecTabPage;
    private TabPage parameterEditTabPage;
    private Panel parameterListPanel;
    private FlickerFreeTableLayoutPanel parameterTableLayoutPanel;
    private Label parameterListQuickConfigLabel;
    private Label parameterListNameLabel;
    private Label parameterListDateLabel;
    private PictureBox editMMIParameterPictureBox;
    private Label actMMIDataLabel;
    private Button createNewParameterSetButton;
    private ComboBox parameterComboBox;
    private CheckBox allAccessCheckBox;
    private PictureBox blueLinePictureBox;
    private TextBox parameterNameTextBox;
    private RichTextBox parameterDescriptionRichTextBox;
    private Label parameterDescriptionLabel;
    private Label parameterNameLabel;
    private NonFocusButton transmitParameterButton;
    private NonFocusButton saveParameterButton;
    private NonFocusButton deleteParameterButton;
    private NonFocusButton saveParameterAsButton;
    private Label parameterSelectionLabel;
    private Panel pictureListPanel;
    private FlickerFreeTableLayoutPanel pictureListTableLayoutPanel;
    private Label pictureTableCaptionLabel;
    private Button newStartScreenButton;
    private Panel transOnlyPanel;
    private PictureBox startScreenMassPictureBox;
    private PictureBox massTabOkPictureBox;
    private Label massTabActualLabel;
    private PictureBox speakerPictureBox;
    private Panel autoTransListPanel;
    private FlickerFreeTableLayoutPanel autoTransTableLayout;
    private CheckBox autoCheckBox;
    private Label receiveLabel;
    private PictureBox receiveTabIconPictureBox;
    private PictureBox receiveTabOkPictureBox;
    private PictureBox receiveSpeakerPictureBox;
    private Button backButton;
    private ComboBox profileComboBox;
    private Label profileSelectionLabel;
    private ParameterPanel parameterPanel;
    private PictureBox blueLinePictureBox2;
    private Label dummyLabel;
    private Label autoTransCaptionLabel;
    private FlickerFreeTableLayoutPanel massTransTableLayoutPanel;
    private FlickerFreeTableLayoutPanel receiveTableLayoutPanel;
    private Panel checkBoxPanel;
    private Button deleteStartScreenFromMMIButton;
    private PictureBox colorStartScreenMassPictureBox;
    private ListSettingFile parameterList;
    private ListSettingFile defaultParameterList;
    private int numberOfDefaultParameters;
    private int numberOfParameterFastConfig;
    private int actualSelectedParameterItem;
    private ZerroCheckBox[] parameterCheckboxes;
    private Label[] parameterNameLabels;
    private SmartToolTip[] parameterDescriptionToolTip;
    private Label[] parameterDateLabels;
    private PictureBox[] parameterEditPictureBoxes;
    private PictureBox[] parameterDeletePictureBoxes;
    private SmartToolTip[] parameterEditToolTip;
    private SmartToolTip[] parameterDeleteToolTip;
    private SmartToolTip[] parameterMassConfigToolTip;
    private int maxNumberForFastConfig = 9;
    private int newActualSelectedParameterItem = -1;
    private ListSettingFile defaultProfileList;
    private ToolTip profileComboToolTip;
    private int numberOfDefaultProfiles;
    private int actualSelectedProfileItem;
    private ProfilePanel profilePanel;
    private bool actualMMIDataHasBeenSelected;
    private Communication MMICom;
    private MMITransceiver transceiver;
    private bool lastSelectedWasAuto;
    private bool speakerActivated;
    private bool lastTabWasEditParameter;
    private bool canelLeavingActualTab;
    private SmartToolTip mmiToolTip;
    private bool parameterIsSaveable;
    private StartScreensListFile startScreensList;
    private StartScreensListFile defaultStartScreenList;
    private int numberOfDefaultStartScreen;
    private PictureBox[] thumbnailsPictureBoxes;
    private PictureBox[] colorThumbnailsPictureBoxes;
    private Label[] screenDescriptions;
    private PictureBox[] trashPictureBoxes;
    private SmartToolTip[] screenEditToolTip;
    private SmartToolTip[] screenDeleteToolTip;
    private readonly Padding defaultPadding = new Padding(2, 2, 2, 2);
    private readonly Size pictureBoxSize = new Size(38, 38);
    private readonly Size largePictureBoxSize = new Size(45, 45);
    private readonly Size startScreenPreviewSize = new Size(32, 32);
    private PictureBox[] fastParameterTransmissionPictuerBox;
    private SmartToolTip[] fastParameterTransmissionToolTip;
    private PictureBox[] fastParameterFastTransmissionPictuerBox;
    private SmartToolTip[] fastParameterFastTransmissionToolTip;
    private PictureBox[] fastParameterStartScreenPictuerBox;
    private PictureBox[] fastParameterColorStartScreenPictuerBox;
    private SmartToolTip[] fastParameterStartScreenToolTip;
    private PictureBox[] fastParameterTransmissionOkPictuerBox;
    private Label[] fastParameterNameLabel;
    private SmartToolTip[] fastParameterDescriptionToolTip;
    private readonly string defaultList = "def";
    private readonly string usersList = "usr";
    private readonly Image screen_empty = (Image) Resources.screen_empty;
    private readonly Image screen_empty_active = (Image) Resources.screen_empty_active;
    private List<ScreenElement> screens;

    protected override void Dispose(bool disposing)
    {
      this.MouseWheelRedirector(false);
      ActivationInformation.IsAllAccessActive = false;
      this.parameterPanel.DisableElements();
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationPanel));
      this.configTabControl = new TabControl();
      this.parameterListTabPage = new TabPage();
      this.parameterListPanel = new Panel();
      this.parameterTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.parameterListQuickConfigLabel = new Label();
      this.editMMIParameterPictureBox = new PictureBox();
      this.parameterListNameLabel = new Label();
      this.parameterListDateLabel = new Label();
      this.actMMIDataLabel = new Label();
      this.dummyLabel = new Label();
      this.createNewParameterSetButton = new Button();
      this.parameterEditTabPage = new TabPage();
      this.parameterPanel = new ParameterPanel();
      this.profileComboBox = new ComboBox();
      this.profileSelectionLabel = new Label();
      this.transmitParameterButton = new NonFocusButton();
      this.saveParameterButton = new NonFocusButton();
      this.deleteParameterButton = new NonFocusButton();
      this.saveParameterAsButton = new NonFocusButton();
      this.parameterComboBox = new ComboBox();
      this.allAccessCheckBox = new CheckBox();
      this.parameterNameTextBox = new TextBox();
      this.parameterDescriptionRichTextBox = new RichTextBox();
      this.parameterDescriptionLabel = new Label();
      this.parameterSelectionLabel = new Label();
      this.parameterNameLabel = new Label();
      this.blueLinePictureBox = new PictureBox();
      this.blueLinePictureBox2 = new PictureBox();
      this.startScreensTabPage = new TabPage();
      this.pictureListPanel = new Panel();
      this.pictureListTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.pictureTableCaptionLabel = new Label();
      this.newStartScreenButton = new Button();
      this.deleteStartScreenFromMMIButton = new Button();
      this.autoTransTabPage = new TabPage();
      this.transOnlyPanel = new Panel();
      this.massTransTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.startScreenMassPictureBox = new PictureBox();
      this.massTabOkPictureBox = new PictureBox();
      this.massTabActualLabel = new Label();
      this.speakerPictureBox = new PictureBox();
      this.backButton = new Button();
      this.autoTransListPanel = new Panel();
      this.autoTransTableLayout = new FlickerFreeTableLayoutPanel();
      this.autoTransCaptionLabel = new Label();
      this.autoRecTabPage = new TabPage();
      this.receiveTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.checkBoxPanel = new Panel();
      this.autoCheckBox = new CheckBox();
      this.receiveTabIconPictureBox = new PictureBox();
      this.receiveSpeakerPictureBox = new PictureBox();
      this.receiveTabOkPictureBox = new PictureBox();
      this.receiveLabel = new Label();
      this.colorStartScreenMassPictureBox = new PictureBox();
      this.configTabControl.SuspendLayout();
      this.parameterListTabPage.SuspendLayout();
      this.parameterListPanel.SuspendLayout();
      this.parameterTableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.editMMIParameterPictureBox).BeginInit();
      this.parameterEditTabPage.SuspendLayout();
      ((ISupportInitialize) this.blueLinePictureBox).BeginInit();
      ((ISupportInitialize) this.blueLinePictureBox2).BeginInit();
      this.startScreensTabPage.SuspendLayout();
      this.pictureListPanel.SuspendLayout();
      this.pictureListTableLayoutPanel.SuspendLayout();
      this.autoTransTabPage.SuspendLayout();
      this.transOnlyPanel.SuspendLayout();
      this.massTransTableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.startScreenMassPictureBox).BeginInit();
      ((ISupportInitialize) this.massTabOkPictureBox).BeginInit();
      ((ISupportInitialize) this.speakerPictureBox).BeginInit();
      this.autoTransListPanel.SuspendLayout();
      this.autoTransTableLayout.SuspendLayout();
      this.autoRecTabPage.SuspendLayout();
      this.receiveTableLayoutPanel.SuspendLayout();
      this.checkBoxPanel.SuspendLayout();
      ((ISupportInitialize) this.receiveTabIconPictureBox).BeginInit();
      ((ISupportInitialize) this.receiveSpeakerPictureBox).BeginInit();
      ((ISupportInitialize) this.receiveTabOkPictureBox).BeginInit();
      ((ISupportInitialize) this.colorStartScreenMassPictureBox).BeginInit();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.configTabControl, "configTabControl");
      this.configTabControl.Controls.Add((Control) this.parameterListTabPage);
      this.configTabControl.Controls.Add((Control) this.parameterEditTabPage);
      this.configTabControl.Controls.Add((Control) this.startScreensTabPage);
      this.configTabControl.Controls.Add((Control) this.autoTransTabPage);
      this.configTabControl.Controls.Add((Control) this.autoRecTabPage);
      this.configTabControl.Name = "configTabControl";
      this.configTabControl.SelectedIndex = 0;
      this.parameterListTabPage.BackColor = Color.White;
      this.parameterListTabPage.Controls.Add((Control) this.parameterListPanel);
      this.parameterListTabPage.Controls.Add((Control) this.createNewParameterSetButton);
      componentResourceManager.ApplyResources((object) this.parameterListTabPage, "parameterListTabPage");
      this.parameterListTabPage.Name = "parameterListTabPage";
      componentResourceManager.ApplyResources((object) this.parameterListPanel, "parameterListPanel");
      this.parameterListPanel.Controls.Add((Control) this.parameterTableLayoutPanel);
      this.parameterListPanel.Name = "parameterListPanel";
      componentResourceManager.ApplyResources((object) this.parameterTableLayoutPanel, "parameterTableLayoutPanel");
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterListQuickConfigLabel, 0, 0);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.editMMIParameterPictureBox, 1, 1);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterListNameLabel, 2, 0);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterListDateLabel, 3, 0);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.actMMIDataLabel, 2, 1);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.dummyLabel, 3, 1);
      this.parameterTableLayoutPanel.Name = "parameterTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.parameterListQuickConfigLabel, "parameterListQuickConfigLabel");
      this.parameterListQuickConfigLabel.BackColor = Color.Transparent;
      this.parameterListQuickConfigLabel.Name = "parameterListQuickConfigLabel";
      componentResourceManager.ApplyResources((object) this.editMMIParameterPictureBox, "editMMIParameterPictureBox");
      this.editMMIParameterPictureBox.Cursor = Cursors.Hand;
      this.editMMIParameterPictureBox.Image = (Image) Resources.MMI_inactive;
      this.editMMIParameterPictureBox.Name = "editMMIParameterPictureBox";
      this.editMMIParameterPictureBox.TabStop = false;
      this.editMMIParameterPictureBox.Click += new EventHandler(this.editMMIParameterPictureBox_Click);
      this.parameterListNameLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.parameterListNameLabel, "parameterListNameLabel");
      this.parameterListNameLabel.Name = "parameterListNameLabel";
      this.parameterListDateLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.parameterListDateLabel, "parameterListDateLabel");
      this.parameterListDateLabel.Name = "parameterListDateLabel";
      componentResourceManager.ApplyResources((object) this.actMMIDataLabel, "actMMIDataLabel");
      this.actMMIDataLabel.BorderStyle = BorderStyle.FixedSingle;
      this.actMMIDataLabel.Name = "actMMIDataLabel";
      componentResourceManager.ApplyResources((object) this.dummyLabel, "dummyLabel");
      this.dummyLabel.BorderStyle = BorderStyle.FixedSingle;
      this.dummyLabel.Name = "dummyLabel";
      componentResourceManager.ApplyResources((object) this.createNewParameterSetButton, "createNewParameterSetButton");
      this.createNewParameterSetButton.Name = "createNewParameterSetButton";
      this.createNewParameterSetButton.UseVisualStyleBackColor = true;
      this.createNewParameterSetButton.Click += new EventHandler(this.createNewParameterSetButton_Click);
      this.parameterEditTabPage.BackColor = Color.White;
      this.parameterEditTabPage.Controls.Add((Control) this.parameterPanel);
      this.parameterEditTabPage.Controls.Add((Control) this.profileComboBox);
      this.parameterEditTabPage.Controls.Add((Control) this.profileSelectionLabel);
      this.parameterEditTabPage.Controls.Add((Control) this.transmitParameterButton);
      this.parameterEditTabPage.Controls.Add((Control) this.saveParameterButton);
      this.parameterEditTabPage.Controls.Add((Control) this.deleteParameterButton);
      this.parameterEditTabPage.Controls.Add((Control) this.saveParameterAsButton);
      this.parameterEditTabPage.Controls.Add((Control) this.parameterComboBox);
      this.parameterEditTabPage.Controls.Add((Control) this.allAccessCheckBox);
      this.parameterEditTabPage.Controls.Add((Control) this.parameterNameTextBox);
      this.parameterEditTabPage.Controls.Add((Control) this.parameterDescriptionRichTextBox);
      this.parameterEditTabPage.Controls.Add((Control) this.parameterDescriptionLabel);
      this.parameterEditTabPage.Controls.Add((Control) this.parameterSelectionLabel);
      this.parameterEditTabPage.Controls.Add((Control) this.parameterNameLabel);
      this.parameterEditTabPage.Controls.Add((Control) this.blueLinePictureBox);
      this.parameterEditTabPage.Controls.Add((Control) this.blueLinePictureBox2);
      componentResourceManager.ApplyResources((object) this.parameterEditTabPage, "parameterEditTabPage");
      this.parameterEditTabPage.Name = "parameterEditTabPage";
      componentResourceManager.ApplyResources((object) this.parameterPanel, "parameterPanel");
      this.parameterPanel.BackColor = Color.Transparent;
      this.parameterPanel.Name = "parameterPanel";
      this.parameterPanel.ValueWasChanged = false;
      this.profileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.profileComboBox.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.profileComboBox, "profileComboBox");
      this.profileComboBox.Name = "profileComboBox";
      this.profileComboBox.SelectedIndexChanged += new EventHandler(this.profileComboBox_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.profileSelectionLabel, "profileSelectionLabel");
      this.profileSelectionLabel.Name = "profileSelectionLabel";
      componentResourceManager.ApplyResources((object) this.transmitParameterButton, "transmitParameterButton");
      this.transmitParameterButton.Name = "transmitParameterButton";
      this.transmitParameterButton.UseVisualStyleBackColor = true;
      this.transmitParameterButton.Click += new EventHandler(this.transmitParameterButton_Click);
      componentResourceManager.ApplyResources((object) this.saveParameterButton, "saveParameterButton");
      this.saveParameterButton.Name = "saveParameterButton";
      this.saveParameterButton.UseVisualStyleBackColor = true;
      this.saveParameterButton.Click += new EventHandler(this.saveParameterButton_Click);
      componentResourceManager.ApplyResources((object) this.deleteParameterButton, "deleteParameterButton");
      this.deleteParameterButton.Name = "deleteParameterButton";
      this.deleteParameterButton.UseVisualStyleBackColor = true;
      this.deleteParameterButton.Click += new EventHandler(this.deleteParameterButton_Click);
      componentResourceManager.ApplyResources((object) this.saveParameterAsButton, "saveParameterAsButton");
      this.saveParameterAsButton.Name = "saveParameterAsButton";
      this.saveParameterAsButton.UseVisualStyleBackColor = true;
      this.saveParameterAsButton.Click += new EventHandler(this.saveParameterAsButton_Click);
      this.parameterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.parameterComboBox.FormattingEnabled = true;
      componentResourceManager.ApplyResources((object) this.parameterComboBox, "parameterComboBox");
      this.parameterComboBox.Name = "parameterComboBox";
      componentResourceManager.ApplyResources((object) this.allAccessCheckBox, "allAccessCheckBox");
      this.allAccessCheckBox.ForeColor = Color.DarkRed;
      this.allAccessCheckBox.Name = "allAccessCheckBox";
      this.allAccessCheckBox.UseVisualStyleBackColor = false;
      this.allAccessCheckBox.CheckedChanged += new EventHandler(this.allAccessCheckBox_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.parameterNameTextBox, "parameterNameTextBox");
      this.parameterNameTextBox.Name = "parameterNameTextBox";
      this.parameterNameTextBox.TextChanged += new EventHandler(this.parameterText_TextChanged);
      componentResourceManager.ApplyResources((object) this.parameterDescriptionRichTextBox, "parameterDescriptionRichTextBox");
      this.parameterDescriptionRichTextBox.Name = "parameterDescriptionRichTextBox";
      this.parameterDescriptionRichTextBox.TextChanged += new EventHandler(this.parameterText_TextChanged);
      componentResourceManager.ApplyResources((object) this.parameterDescriptionLabel, "parameterDescriptionLabel");
      this.parameterDescriptionLabel.Name = "parameterDescriptionLabel";
      componentResourceManager.ApplyResources((object) this.parameterSelectionLabel, "parameterSelectionLabel");
      this.parameterSelectionLabel.Name = "parameterSelectionLabel";
      componentResourceManager.ApplyResources((object) this.parameterNameLabel, "parameterNameLabel");
      this.parameterNameLabel.Name = "parameterNameLabel";
      componentResourceManager.ApplyResources((object) this.blueLinePictureBox, "blueLinePictureBox");
      this.blueLinePictureBox.Name = "blueLinePictureBox";
      this.blueLinePictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.blueLinePictureBox2, "blueLinePictureBox2");
      this.blueLinePictureBox2.Name = "blueLinePictureBox2";
      this.blueLinePictureBox2.TabStop = false;
      this.startScreensTabPage.BackColor = Color.White;
      this.startScreensTabPage.Controls.Add((Control) this.pictureListPanel);
      this.startScreensTabPage.Controls.Add((Control) this.newStartScreenButton);
      this.startScreensTabPage.Controls.Add((Control) this.deleteStartScreenFromMMIButton);
      componentResourceManager.ApplyResources((object) this.startScreensTabPage, "startScreensTabPage");
      this.startScreensTabPage.Name = "startScreensTabPage";
      componentResourceManager.ApplyResources((object) this.pictureListPanel, "pictureListPanel");
      this.pictureListPanel.Controls.Add((Control) this.pictureListTableLayoutPanel);
      this.pictureListPanel.Name = "pictureListPanel";
      componentResourceManager.ApplyResources((object) this.pictureListTableLayoutPanel, "pictureListTableLayoutPanel");
      this.pictureListTableLayoutPanel.Controls.Add((Control) this.pictureTableCaptionLabel, 0, 0);
      this.pictureListTableLayoutPanel.Name = "pictureListTableLayoutPanel";
      this.pictureTableCaptionLabel.BackColor = Color.Transparent;
      this.pictureListTableLayoutPanel.SetColumnSpan((Control) this.pictureTableCaptionLabel, 3);
      componentResourceManager.ApplyResources((object) this.pictureTableCaptionLabel, "pictureTableCaptionLabel");
      this.pictureTableCaptionLabel.Name = "pictureTableCaptionLabel";
      componentResourceManager.ApplyResources((object) this.newStartScreenButton, "newStartScreenButton");
      this.newStartScreenButton.Name = "newStartScreenButton";
      this.newStartScreenButton.UseVisualStyleBackColor = true;
      this.newStartScreenButton.Click += new EventHandler(this.newStartScreenButton_Click);
      componentResourceManager.ApplyResources((object) this.deleteStartScreenFromMMIButton, "deleteStartScreenFromMMIButton");
      this.deleteStartScreenFromMMIButton.Name = "deleteStartScreenFromMMIButton";
      this.deleteStartScreenFromMMIButton.UseVisualStyleBackColor = true;
      this.deleteStartScreenFromMMIButton.Click += new EventHandler(this.deleteStartScreenFromMMIButton_Click);
      this.autoTransTabPage.BackColor = Color.White;
      this.autoTransTabPage.Controls.Add((Control) this.transOnlyPanel);
      this.autoTransTabPage.Controls.Add((Control) this.autoTransListPanel);
      componentResourceManager.ApplyResources((object) this.autoTransTabPage, "autoTransTabPage");
      this.autoTransTabPage.Name = "autoTransTabPage";
      componentResourceManager.ApplyResources((object) this.transOnlyPanel, "transOnlyPanel");
      this.transOnlyPanel.Controls.Add((Control) this.massTransTableLayoutPanel);
      this.transOnlyPanel.Controls.Add((Control) this.backButton);
      this.transOnlyPanel.Name = "transOnlyPanel";
      componentResourceManager.ApplyResources((object) this.massTransTableLayoutPanel, "massTransTableLayoutPanel");
      this.massTransTableLayoutPanel.Controls.Add((Control) this.colorStartScreenMassPictureBox, 2, 0);
      this.massTransTableLayoutPanel.Controls.Add((Control) this.startScreenMassPictureBox, 1, 0);
      this.massTransTableLayoutPanel.Controls.Add((Control) this.massTabOkPictureBox, 4, 0);
      this.massTransTableLayoutPanel.Controls.Add((Control) this.massTabActualLabel, 3, 0);
      this.massTransTableLayoutPanel.Controls.Add((Control) this.speakerPictureBox, 0, 0);
      this.massTransTableLayoutPanel.Name = "massTransTableLayoutPanel";
      this.startScreenMassPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.startScreenMassPictureBox.Image = (Image) Resources.screen_empty_active;
      componentResourceManager.ApplyResources((object) this.startScreenMassPictureBox, "startScreenMassPictureBox");
      this.startScreenMassPictureBox.Name = "startScreenMassPictureBox";
      this.startScreenMassPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.massTabOkPictureBox, "massTabOkPictureBox");
      this.massTabOkPictureBox.Name = "massTabOkPictureBox";
      this.massTabOkPictureBox.TabStop = false;
      this.massTabActualLabel.AutoEllipsis = true;
      componentResourceManager.ApplyResources((object) this.massTabActualLabel, "massTabActualLabel");
      this.massTabActualLabel.Name = "massTabActualLabel";
      this.speakerPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.speakerPictureBox.Cursor = Cursors.Hand;
      this.speakerPictureBox.Image = (Image) Resources.volume_on;
      componentResourceManager.ApplyResources((object) this.speakerPictureBox, "speakerPictureBox");
      this.speakerPictureBox.Name = "speakerPictureBox";
      this.speakerPictureBox.TabStop = false;
      this.speakerPictureBox.Click += new EventHandler(this.speakerPictureBox_Click);
      componentResourceManager.ApplyResources((object) this.backButton, "backButton");
      this.backButton.Name = "backButton";
      this.backButton.UseVisualStyleBackColor = true;
      this.backButton.Click += new EventHandler(this.backButton_Click);
      componentResourceManager.ApplyResources((object) this.autoTransListPanel, "autoTransListPanel");
      this.autoTransListPanel.Controls.Add((Control) this.autoTransTableLayout);
      this.autoTransListPanel.Name = "autoTransListPanel";
      componentResourceManager.ApplyResources((object) this.autoTransTableLayout, "autoTransTableLayout");
      this.autoTransTableLayout.Controls.Add((Control) this.autoTransCaptionLabel, 4, 0);
      this.autoTransTableLayout.Name = "autoTransTableLayout";
      this.autoTransCaptionLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.autoTransCaptionLabel, "autoTransCaptionLabel");
      this.autoTransCaptionLabel.Name = "autoTransCaptionLabel";
      this.autoRecTabPage.BackColor = Color.White;
      this.autoRecTabPage.Controls.Add((Control) this.receiveTableLayoutPanel);
      this.autoRecTabPage.Controls.Add((Control) this.receiveTabOkPictureBox);
      this.autoRecTabPage.Controls.Add((Control) this.receiveLabel);
      componentResourceManager.ApplyResources((object) this.autoRecTabPage, "autoRecTabPage");
      this.autoRecTabPage.Name = "autoRecTabPage";
      componentResourceManager.ApplyResources((object) this.receiveTableLayoutPanel, "receiveTableLayoutPanel");
      this.receiveTableLayoutPanel.Controls.Add((Control) this.checkBoxPanel, 2, 0);
      this.receiveTableLayoutPanel.Controls.Add((Control) this.receiveTabIconPictureBox, 1, 0);
      this.receiveTableLayoutPanel.Controls.Add((Control) this.receiveSpeakerPictureBox, 0, 0);
      this.receiveTableLayoutPanel.Name = "receiveTableLayoutPanel";
      this.checkBoxPanel.BorderStyle = BorderStyle.FixedSingle;
      this.checkBoxPanel.Controls.Add((Control) this.autoCheckBox);
      componentResourceManager.ApplyResources((object) this.checkBoxPanel, "checkBoxPanel");
      this.checkBoxPanel.Name = "checkBoxPanel";
      componentResourceManager.ApplyResources((object) this.autoCheckBox, "autoCheckBox");
      this.autoCheckBox.BackColor = Color.Transparent;
      this.autoCheckBox.Name = "autoCheckBox";
      this.autoCheckBox.UseVisualStyleBackColor = false;
      componentResourceManager.ApplyResources((object) this.receiveTabIconPictureBox, "receiveTabIconPictureBox");
      this.receiveTabIconPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.receiveTabIconPictureBox.Cursor = Cursors.Hand;
      this.receiveTabIconPictureBox.Image = (Image) Resources.Icon_mReceive_Inactive;
      this.receiveTabIconPictureBox.Name = "receiveTabIconPictureBox";
      this.receiveTabIconPictureBox.TabStop = false;
      this.receiveTabIconPictureBox.Click += new EventHandler(this.receiveTabIconPictureBox_Click);
      componentResourceManager.ApplyResources((object) this.receiveSpeakerPictureBox, "receiveSpeakerPictureBox");
      this.receiveSpeakerPictureBox.Cursor = Cursors.Hand;
      this.receiveSpeakerPictureBox.Image = (Image) Resources.volume_on;
      this.receiveSpeakerPictureBox.Name = "receiveSpeakerPictureBox";
      this.receiveSpeakerPictureBox.TabStop = false;
      this.receiveSpeakerPictureBox.Click += new EventHandler(this.speakerPictureBox_Click);
      componentResourceManager.ApplyResources((object) this.receiveTabOkPictureBox, "receiveTabOkPictureBox");
      this.receiveTabOkPictureBox.Name = "receiveTabOkPictureBox";
      this.receiveTabOkPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.receiveLabel, "receiveLabel");
      this.receiveLabel.Name = "receiveLabel";
      this.colorStartScreenMassPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.colorStartScreenMassPictureBox.Image = (Image) Resources.screen_empty_active;
      componentResourceManager.ApplyResources((object) this.colorStartScreenMassPictureBox, "colorStartScreenMassPictrueBox");
      this.colorStartScreenMassPictureBox.Name = "colorStartScreenMassPictrueBox";
      this.colorStartScreenMassPictureBox.TabStop = false;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.configTabControl);
      this.DoubleBuffered = true;
      this.Name = nameof (ConfigurationPanel);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Layout += new LayoutEventHandler(this.ConfigurationPanel_Layout);
      this.configTabControl.ResumeLayout(false);
      this.parameterListTabPage.ResumeLayout(false);
      this.parameterListPanel.ResumeLayout(false);
      this.parameterListPanel.PerformLayout();
      this.parameterTableLayoutPanel.ResumeLayout(false);
      this.parameterTableLayoutPanel.PerformLayout();
      ((ISupportInitialize) this.editMMIParameterPictureBox).EndInit();
      this.parameterEditTabPage.ResumeLayout(false);
      this.parameterEditTabPage.PerformLayout();
      ((ISupportInitialize) this.blueLinePictureBox).EndInit();
      ((ISupportInitialize) this.blueLinePictureBox2).EndInit();
      this.startScreensTabPage.ResumeLayout(false);
      this.pictureListPanel.ResumeLayout(false);
      this.pictureListTableLayoutPanel.ResumeLayout(false);
      this.autoTransTabPage.ResumeLayout(false);
      this.transOnlyPanel.ResumeLayout(false);
      this.massTransTableLayoutPanel.ResumeLayout(false);
      ((ISupportInitialize) this.startScreenMassPictureBox).EndInit();
      ((ISupportInitialize) this.massTabOkPictureBox).EndInit();
      ((ISupportInitialize) this.speakerPictureBox).EndInit();
      this.autoTransListPanel.ResumeLayout(false);
      this.autoTransListPanel.PerformLayout();
      this.autoTransTableLayout.ResumeLayout(false);
      this.autoRecTabPage.ResumeLayout(false);
      this.autoRecTabPage.PerformLayout();
      this.receiveTableLayoutPanel.ResumeLayout(false);
      this.checkBoxPanel.ResumeLayout(false);
      this.checkBoxPanel.PerformLayout();
      ((ISupportInitialize) this.receiveTabIconPictureBox).EndInit();
      ((ISupportInitialize) this.receiveSpeakerPictureBox).EndInit();
      ((ISupportInitialize) this.receiveTabOkPictureBox).EndInit();
      ((ISupportInitialize) this.colorStartScreenMassPictureBox).EndInit();
      this.ResumeLayout(false);
    }

    private void InitParameter()
    {
      this.parameterCheckboxes = new ZerroCheckBox[0];
      this.parameterNameLabels = new Label[0];
      this.parameterDescriptionToolTip = new SmartToolTip[0];
      this.parameterDateLabels = new Label[0];
      this.parameterEditPictureBoxes = new PictureBox[0];
      this.parameterDeletePictureBoxes = new PictureBox[0];
      this.parameterEditToolTip = new SmartToolTip[0];
      this.parameterDeleteToolTip = new SmartToolTip[0];
      this.parameterMassConfigToolTip = new SmartToolTip[0];
      this.numberOfParameterFastConfig = 0;
      this.FillParametersTablePanel();
      this.FillProfileList();
      this.actualMMIDataHasBeenSelected = false;
      this.dummyLabel.Font = FontDefinition.TableCaptionTextFont;
      this.editMMIParameterPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.editMMIParameterPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.editMMIParameterPictureBox.Margin = this.defaultPadding;
      this.editMMIParameterPictureBox.Size = this.pictureBoxSize;
    }

    private void FillParametersTablePanel()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.SuspendLayout();
      this.parameterTableLayoutPanel.SuspendLayout();
      this.defaultParameterList = ListSettingFile.ReadFile(Directories.Instance.DefaultParametersListFileName);
      this.parameterList = ListSettingFile.ReadFile(Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
      this.numberOfDefaultParameters = this.defaultParameterList.Length;
      for (int index = 0; index < this.parameterTableLayoutPanel.RowCount - 2; ++index)
      {
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterCheckboxes[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterNameLabels[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterDateLabels[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterEditPictureBoxes[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterDeletePictureBoxes[index]);
        this.parameterCheckboxes[index].Dispose();
        this.parameterNameLabels[index].Dispose();
        this.parameterDescriptionToolTip[index].Dispose();
        this.parameterDateLabels[index].Dispose();
        this.parameterEditPictureBoxes[index].Dispose();
        this.parameterDeletePictureBoxes[index].Dispose();
        this.parameterEditToolTip[index].Dispose();
        this.parameterDeleteToolTip[index].Dispose();
        this.parameterMassConfigToolTip[index].Dispose();
        this.parameterTableLayoutPanel.RowStyles.Clear();
      }
      this.parameterComboBox.Items.Clear();
      this.parameterTableLayoutPanel.RowCount = 2;
      this.parameterListQuickConfigLabel.Visible = false;
      this.parameterListNameLabel.Visible = false;
      this.parameterListDateLabel.Visible = false;
      this.parameterTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
      this.parameterTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      this.parameterTableLayoutPanel.AutoSize = true;
      int length = this.numberOfDefaultParameters + this.parameterList.Length;
      this.parameterCheckboxes = new ZerroCheckBox[length];
      this.parameterNameLabels = new Label[length];
      this.parameterDescriptionToolTip = new SmartToolTip[length];
      this.parameterDateLabels = new Label[length];
      this.parameterEditPictureBoxes = new PictureBox[length];
      this.parameterDeletePictureBoxes = new PictureBox[length];
      this.parameterEditToolTip = new SmartToolTip[length];
      this.parameterDeleteToolTip = new SmartToolTip[length];
      this.parameterMassConfigToolTip = new SmartToolTip[length];
      this.parameterTableLayoutPanel.RowCount = length + 2;
      this.numberOfParameterFastConfig = 0;
      for (int index = 0; index < this.numberOfDefaultParameters; ++index)
        this.CreateElementsForParameterList(index, this.defaultParameterList.IsFastConfig(index), this.defaultParameterList.Name(index), this.defaultParameterList.Description(index), this.defaultParameterList.ChangeDate(index), this.defaultParameterList.IsDeletable(index));
      for (int defaultParameters = this.numberOfDefaultParameters; defaultParameters < length; ++defaultParameters)
      {
        int position = defaultParameters - this.numberOfDefaultParameters;
        this.CreateElementsForParameterList(defaultParameters, this.parameterList.IsFastConfig(position), this.parameterList.Name(position), this.parameterList.Description(position), this.parameterList.ChangeDate(position), this.parameterList.IsDeletable(position));
      }
      if (this.parameterTableLayoutPanel.RowCount >= 2)
      {
        this.parameterListQuickConfigLabel.Visible = true;
        this.parameterListNameLabel.Visible = true;
        this.parameterListDateLabel.Visible = true;
      }
      if (this.parameterTableLayoutPanel.AutoSize)
        this.parameterTableLayoutPanel.Size = this.parameterTableLayoutPanel.GetPreferredSize(this.parameterTableLayoutPanel.Size);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.parameterCheckboxes);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.parameterNameLabels);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.parameterDateLabels);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.parameterEditPictureBoxes);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.parameterDeletePictureBoxes);
      if (ActivationInformation.VersionLevelProperty < 3)
        this.parameterTableLayoutPanel.ColumnStyles[0].Width = 0.0f;
      this.parameterTableLayoutPanel.ResumeLayout(false);
      this.parameterTableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void FillProfileList()
    {
      this.profilePanel = new ProfilePanel();
      this.defaultProfileList = ListSettingFile.ReadFile(Directories.Instance.DefaultProfilesListFileName);
      this.numberOfDefaultProfiles = this.defaultProfileList.Length;
      this.profileComboToolTip = new ToolTip();
      for (int position = 0; position < this.defaultProfileList.Length; ++position)
        this.profileComboBox.Items.Add((object) this.defaultProfileList.Name(position));
    }

    private void CreateElementsForParameterList(
      int listCounter,
      bool fastConfig,
      string parameterLabel,
      string parameterDescription,
      string date,
      bool deletable,
      bool checkBoxEditable = true)
    {
      this.parameterCheckboxes[listCounter] = new ZerroCheckBox();
      this.parameterCheckboxes[listCounter].SuspendLayout();
      this.parameterCheckboxes[listCounter].Checked = fastConfig;
      this.parameterCheckboxes[listCounter].ForeColor = Color.Transparent;
      if (fastConfig)
        ++this.numberOfParameterFastConfig;
      this.parameterCheckboxes[listCounter].Name = string.Concat((object) listCounter);
      this.parameterCheckboxes[listCounter].CheckedChanged += new ZerroCheckBox.CheckedChangedEventHandler(this.parameterCheckBox_CheckedChanged);
      this.parameterCheckboxes[listCounter].Enabled = checkBoxEditable;
      this.parameterCheckboxes[listCounter].BackColor = Color.Transparent;
      this.parameterCheckboxes[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.parameterCheckboxes[listCounter].Margin = this.defaultPadding;
      this.parameterCheckboxes[listCounter].Size = this.pictureBoxSize;
      this.parameterMassConfigToolTip[listCounter] = new SmartToolTip();
      this.parameterMassConfigToolTip[listCounter].ShowAlways = true;
      this.parameterMassConfigToolTip[listCounter].SetToolTip((Control) this.parameterCheckboxes[listCounter].GetCheckBoxPictureBox(), GlobalResource.ConfigPanel_ToolTip_MassConfigParameter);
      this.parameterCheckboxes[listCounter].Anchor = AnchorStyles.None;
      this.parameterCheckboxes[listCounter].ResumeLayout(false);
      this.parameterCheckboxes[listCounter].PerformLayout();
      this.parameterNameLabels[listCounter] = new Label();
      this.parameterNameLabels[listCounter].SuspendLayout();
      this.parameterNameLabels[listCounter].Font = FontDefinition.TableCaptionTextFont;
      this.parameterNameLabels[listCounter].ForeColor = ColorDefinition.BlackTextColor;
      if (parameterLabel.Length > this.parameterNameTextBox.MaxLength)
        parameterLabel = parameterLabel.Substring(0, this.parameterNameTextBox.MaxLength);
      this.parameterNameLabels[listCounter].Text = parameterLabel;
      this.parameterNameLabels[listCounter].TextAlign = ContentAlignment.MiddleLeft;
      this.parameterNameLabels[listCounter].Dock = DockStyle.Fill;
      this.parameterNameLabels[listCounter].AutoSize = true;
      this.parameterNameLabels[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.parameterNameLabels[listCounter].Margin = this.defaultPadding;
      this.parameterNameLabels[listCounter].Name = string.Concat((object) listCounter);
      this.parameterDescriptionToolTip[listCounter] = new SmartToolTip();
      this.parameterDescriptionToolTip[listCounter].ToolTipIcon = ToolTipIcon.Info;
      this.parameterDescriptionToolTip[listCounter].ShowAlways = true;
      this.parameterDescriptionToolTip[listCounter].SetToolTip((Control) this.parameterNameLabels[listCounter], parameterDescription);
      this.parameterNameLabels[listCounter].BackColor = Color.Transparent;
      this.parameterNameLabels[listCounter].ResumeLayout(false);
      this.parameterNameLabels[listCounter].PerformLayout();
      this.parameterComboBox.Items.Add((object) parameterLabel);
      this.parameterDateLabels[listCounter] = new Label();
      this.parameterDateLabels[listCounter].SuspendLayout();
      this.parameterDateLabels[listCounter].Font = FontDefinition.TableCaptionTextFont;
      this.parameterDateLabels[listCounter].ForeColor = ColorDefinition.BlackTextColor;
      this.parameterDateLabels[listCounter].TextAlign = ContentAlignment.MiddleLeft;
      this.parameterDateLabels[listCounter].Dock = DockStyle.Fill;
      this.parameterDateLabels[listCounter].AutoSize = true;
      this.parameterDateLabels[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.parameterDateLabels[listCounter].Margin = this.defaultPadding;
      this.parameterDateLabels[listCounter].Name = string.Concat((object) listCounter);
      this.parameterDateLabels[listCounter].Text = date;
      this.parameterDateLabels[listCounter].BackColor = Color.Transparent;
      this.parameterDateLabels[listCounter].ResumeLayout(false);
      this.parameterDateLabels[listCounter].PerformLayout();
      this.parameterEditPictureBoxes[listCounter] = new PictureBox();
      this.parameterEditPictureBoxes[listCounter].SuspendLayout();
      this.parameterEditPictureBoxes[listCounter].Cursor = Cursors.Hand;
      this.parameterEditPictureBoxes[listCounter].Name = string.Concat((object) listCounter);
      this.parameterEditPictureBoxes[listCounter].TabStop = false;
      this.parameterEditPictureBoxes[listCounter].AutoSize = true;
      this.parameterEditPictureBoxes[listCounter].SizeMode = PictureBoxSizeMode.CenterImage;
      this.parameterEditPictureBoxes[listCounter].Click += new EventHandler(this.editParameterPictureBox_Click);
      this.parameterEditPictureBoxes[listCounter].Image = (Image) Resources.note;
      this.parameterEditPictureBoxes[listCounter].BackColor = Color.Transparent;
      this.parameterEditPictureBoxes[listCounter].Anchor = AnchorStyles.None;
      this.parameterEditPictureBoxes[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.parameterEditPictureBoxes[listCounter].Margin = this.defaultPadding;
      this.parameterEditPictureBoxes[listCounter].Size = this.pictureBoxSize;
      this.parameterEditToolTip[listCounter] = new SmartToolTip();
      this.parameterEditToolTip[listCounter].ShowAlways = true;
      this.parameterEditToolTip[listCounter].SetToolTip((Control) this.parameterEditPictureBoxes[listCounter], GlobalResource.ConfigPanel_ToolTip_EditParameter);
      this.parameterEditPictureBoxes[listCounter].ResumeLayout(false);
      this.parameterEditPictureBoxes[listCounter].PerformLayout();
      this.parameterDeletePictureBoxes[listCounter] = new PictureBox();
      this.parameterDeletePictureBoxes[listCounter].SuspendLayout();
      this.parameterDeletePictureBoxes[listCounter].Name = string.Concat((object) listCounter);
      this.parameterDeletePictureBoxes[listCounter].TabStop = false;
      this.parameterDeletePictureBoxes[listCounter].AutoSize = true;
      this.parameterDeletePictureBoxes[listCounter].Image = (Image) new Bitmap(1, 1);
      if (deletable)
      {
        this.parameterDeletePictureBoxes[listCounter].Click += new EventHandler(this.deleteParameterPictureBox_Click);
        this.parameterDeletePictureBoxes[listCounter].Cursor = Cursors.Hand;
        this.parameterDeletePictureBoxes[listCounter].Image = (Image) Resources.trashcan;
        this.parameterDeletePictureBoxes[listCounter].BorderStyle = BorderStyle.FixedSingle;
        this.parameterDeletePictureBoxes[listCounter].Size = this.pictureBoxSize;
        this.parameterDeletePictureBoxes[listCounter].Margin = this.defaultPadding;
        this.parameterDeletePictureBoxes[listCounter].SizeMode = PictureBoxSizeMode.CenterImage;
      }
      else
        this.parameterDeletePictureBoxes[listCounter].Image = (Image) new Bitmap(1, 1);
      this.parameterDeletePictureBoxes[listCounter].BackColor = Color.Transparent;
      this.parameterDeletePictureBoxes[listCounter].Anchor = AnchorStyles.None;
      this.parameterDeleteToolTip[listCounter] = new SmartToolTip();
      this.parameterDeleteToolTip[listCounter].ShowAlways = true;
      this.parameterDeleteToolTip[listCounter].SetToolTip((Control) this.parameterDeletePictureBoxes[listCounter], GlobalResource.ConfigPanel_ToolTip_DeleteParameter);
      this.parameterDeletePictureBoxes[listCounter].ResumeLayout(false);
      this.parameterDeletePictureBoxes[listCounter].PerformLayout();
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterCheckboxes[listCounter], 0, listCounter + 2);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterEditPictureBoxes[listCounter], 1, listCounter + 2);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterNameLabels[listCounter], 2, listCounter + 2);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterDateLabels[listCounter], 3, listCounter + 2);
      this.parameterTableLayoutPanel.Controls.Add((Control) this.parameterDeletePictureBoxes[listCounter], 4, listCounter + 2);
    }

    private void parameterCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ZerroCheckBox zerroCheckBox = (ZerroCheckBox) sender;
      if (zerroCheckBox.Checked)
        ++this.numberOfParameterFastConfig;
      else
        --this.numberOfParameterFastConfig;
      if (this.numberOfParameterFastConfig > this.maxNumberForFastConfig)
      {
        zerroCheckBox.Checked = false;
        --this.numberOfParameterFastConfig;
        MainWindow.Instance.Cursor = Cursors.Default;
        int num = (int) MessageBox.Show(string.Format(GlobalResource.ConfigPanel_FastConfig_MaxNumbers_Exceeded_Message, (object) this.maxNumberForFastConfig), GlobalResource.ConfigPanel_FastConfig_MaxNumbers_Exceeded_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        int int32 = Convert.ToInt32(zerroCheckBox.Name);
        try
        {
          if (int32 < this.numberOfDefaultParameters)
          {
            this.defaultParameterList.IsFastConfig(int32, zerroCheckBox.Checked);
            ListSettingFile.WriteFile(this.defaultParameterList, Directories.Instance.DefaultParametersListFileName);
          }
          else
          {
            this.parameterList.IsFastConfig(int32 - this.numberOfDefaultParameters, zerroCheckBox.Checked);
            ListSettingFile.WriteFile(this.parameterList, Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
          }
        }
        catch (Exception ex)
        {
          MainWindow.Instance.Cursor = Cursors.Default;
          UniqueError.Message(UniqueError.Number.PARAMETERLISTFILE_WRITE_ERROR);
          return;
        }
        this.FillFastParametersTablePanel();
        this.EnableFunctions(this.MMICom.DeviceConnected);
        MainWindow.Instance.Cursor = Cursors.Default;
      }
    }

    private void editParameterPictureBox_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      if (sender.GetType() == typeof (PictureBox))
        this.actualSelectedParameterItem = Convert.ToInt32(((Control) sender).Name);
      else if (sender.GetType() == typeof (ComboBox))
      {
        if (this.parameterPanel.ValueWasChanged)
        {
          object selectedItem = ((ComboBox) sender).SelectedItem;
          this.SavingParameter();
          if (this.canelLeavingActualTab)
          {
            this.parameterComboBox.SelectedIndexChanged -= new EventHandler(this.editParameterPictureBox_Click);
            this.parameterComboBox.SelectedIndex = this.actualSelectedParameterItem;
            this.parameterComboBox.SelectedIndexChanged += new EventHandler(this.editParameterPictureBox_Click);
            MainWindow.Instance.Cursor = Cursors.Default;
            return;
          }
          ((ComboBox) sender).SelectedItem = selectedItem;
        }
        this.actualSelectedParameterItem = ((ListControl) sender).SelectedIndex;
        if (((string) ((ComboBox) sender).SelectedItem).Equals(this.actMMIDataLabel.Text))
        {
          MainWindow.Instance.Cursor = Cursors.Default;
          this.editMMIParameterPictureBox_Click(sender, e);
          return;
        }
      }
      int selectedParameterItem = this.actualSelectedParameterItem;
      if (this.MMICom.DeviceConnected)
      {
        if (sender.GetType() == typeof (ComboBox))
          --selectedParameterItem;
        else if (sender.GetType() == typeof (PictureBox))
          ++this.actualSelectedParameterItem;
      }
      ListSettingFile listSettingFile;
      bool isDefault;
      if (selectedParameterItem < this.numberOfDefaultParameters)
      {
        listSettingFile = this.defaultParameterList;
        isDefault = true;
        this.deleteParameterButton.Enabled = false;
        this.saveParameterButton.Enabled = false;
        this.parameterIsSaveable = false;
      }
      else
      {
        listSettingFile = this.parameterList;
        selectedParameterItem -= this.numberOfDefaultParameters;
        isDefault = false;
        this.deleteParameterButton.Enabled = true;
        this.saveParameterButton.Enabled = false;
        this.parameterIsSaveable = true;
      }
      this.parameterDescriptionRichTextBox.Text = listSettingFile.Description(selectedParameterItem);
      if (listSettingFile.Name(selectedParameterItem).Length > this.parameterNameTextBox.MaxLength)
        this.parameterNameTextBox.Text = listSettingFile.Name(selectedParameterItem).Substring(0, this.parameterNameTextBox.MaxLength);
      else
        this.parameterNameTextBox.Text = listSettingFile.Name(selectedParameterItem);
      this.parameterComboBox.SelectedIndexChanged -= new EventHandler(this.editParameterPictureBox_Click);
      this.parameterComboBox.SelectedIndex = this.actualSelectedParameterItem;
      this.parameterComboBox.SelectedIndexChanged += new EventHandler(this.editParameterPictureBox_Click);
      if (!this.ReadParameterFile(listSettingFile.ContentFile(selectedParameterItem), isDefault))
      {
        MainWindow.Instance.Cursor = Cursors.Default;
      }
      else
      {
        if (listSettingFile.IsDeletable(selectedParameterItem))
        {
          this.parameterDescriptionRichTextBox.Enabled = true;
          this.parameterNameTextBox.Enabled = true;
        }
        else
        {
          this.parameterDescriptionRichTextBox.Enabled = false;
          this.parameterNameTextBox.Enabled = false;
        }
        ((Control) this.parameterEditTabPage).Enabled = true;
        this.configTabControl.SelectedTab = this.parameterEditTabPage;
        this.parameterPanel.ValueWasChanged = false;
        this.actualMMIDataHasBeenSelected = false;
        this.saveParameterAsButton.Enabled = true;
        MainWindow.Instance.Cursor = Cursors.Default;
      }
    }

    private void deleteParameterPictureBox_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(GlobalResource.ConfigPanel_parameterDeleteButton_Message, GlobalResource.ConfigPanel_parameterDeleteButton_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        return;
      this.actualSelectedParameterItem = Convert.ToInt32(((Control) sender).Name);
      this.deleteParameterFromList(this.actualSelectedParameterItem);
    }

    private void deleteParameterFromList(int pos)
    {
      bool flag1;
      ListSettingFile content;
      if (pos < this.numberOfDefaultParameters)
      {
        flag1 = true;
        content = this.defaultParameterList;
      }
      else
      {
        flag1 = false;
        pos -= this.numberOfDefaultParameters;
        content = this.parameterList;
      }
      if (!content.IsDeletable(pos))
      {
        int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Delete_DefaultParameter_Message, GlobalResource.ConfigPanel_Delete_DefaultParameter_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        string additionalInfo = content.ContentFile(pos);
        bool flag2 = content.IsFastConfig(pos);
        content.RemoveListElement(pos);
        try
        {
          if (flag1)
            ListSettingFile.WriteFile(content, Directories.Instance.DefaultParametersListFileName);
          else
            ListSettingFile.WriteFile(content, Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.PARAMETERLISTFILE_DELETE_WRITE_ERROR);
          return;
        }
        try
        {
          if (flag1)
            File.Delete(Directories.Instance.DefaultsPath + additionalInfo);
          else
            File.Delete(Directories.Instance.UserDataPath + additionalInfo);
        }
        catch (FileNotFoundException ex)
        {
          UniqueError.Message(UniqueError.Number.PARAMETERSETTINGS_DELETE_ERROR, additionalInfo);
        }
        this.FillParametersTablePanel();
        this.parameterComboBoxAddActMMI();
        if (flag2)
        {
          this.FillFastParametersTablePanel();
          this.EnableFunctions(this.MMICom.DeviceConnected);
        }
        this.actualSelectedParameterItem = -1;
      }
    }

    private void SavingParameter()
    {
      if (MessageBox.Show(GlobalResource.SaveSettingsQuestion_Message, GlobalResource.SaveSettingsQuestion_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        this.canelLeavingActualTab = this.SavingParameterWithoutQuestion();
      else
        this.canelLeavingActualTab = false;
    }

    private bool SavingParameterWithoutQuestion()
    {
      if (this.parameterNameTextBox.Text == "")
      {
        int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Parameter_Name_Missing_Message, GlobalResource.ConfigPanel_Parameter_Name_Missing_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        this.parameterNameTextBox.Focus();
        return true;
      }
      try
      {
        if (this.actualMMIDataHasBeenSelected)
        {
          int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultParameter_Message, GlobalResource.ConfigPanel_Update_DefaultParameter_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return true;
        }
        int num1 = 0;
        if (this.MMICom.DeviceConnected)
          num1 = 1;
        if (this.actualSelectedParameterItem - num1 < this.numberOfDefaultParameters && this.actualSelectedParameterItem >= 0)
        {
          int num2 = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultParameter_Message, GlobalResource.ConfigPanel_Update_DefaultParameter_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return true;
        }
        if (!this.parameterList.IsDeletable(this.actualSelectedParameterItem - this.numberOfDefaultParameters - num1))
        {
          int num2 = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultParameter_Message, GlobalResource.ConfigPanel_Update_DefaultParameter_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return true;
        }
        this.newActualSelectedParameterItem = this.WriteParameterFile(this.actualSelectedParameterItem - this.numberOfDefaultParameters - num1);
        if (this.MMICom.DeviceConnected)
          ++this.newActualSelectedParameterItem;
      }
      catch (IndexOutOfRangeException ex)
      {
        this.newActualSelectedParameterItem = this.WriteParameterFile(-1);
        if (this.MMICom.DeviceConnected)
          ++this.newActualSelectedParameterItem;
      }
      return false;
    }

    private void createNewParameterSetButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      ((Control) this.parameterEditTabPage).Enabled = true;
      this.configTabControl.SelectedTab = this.parameterEditTabPage;
      this.parameterDescriptionRichTextBox.Text = GlobalResource.ConfigPanel_newParameter_Description;
      this.parameterNameTextBox.Text = GlobalResource.ConfigPanel_newParameter_Label;
      this.parameterPanel.ResetNumericUpDownValues();
      this.deleteParameterButton.Name = "";
      this.parameterPanel.ValueWasChanged = true;
      this.actualSelectedParameterItem = -1;
      this.parameterComboBox.SelectedIndexChanged -= new EventHandler(this.editParameterPictureBox_Click);
      this.parameterComboBox.SelectedValue = (object) "";
      this.parameterComboBox.SelectedItem = (object) "";
      this.parameterComboBox.SelectedIndex = -1;
      this.parameterComboBox.SelectedIndexChanged += new EventHandler(this.editParameterPictureBox_Click);
      this.profileComboBox.SelectedIndexChanged -= new EventHandler(this.profileComboBox_SelectedIndexChanged);
      this.profileComboBox.SelectedValue = (object) "";
      this.profileComboBox.SelectedItem = (object) "";
      this.profileComboBox.SelectedIndex = -1;
      this.profileComboBox.SelectedIndexChanged += new EventHandler(this.profileComboBox_SelectedIndexChanged);
      this.parameterDescriptionRichTextBox.Enabled = true;
      this.parameterNameTextBox.Enabled = true;
      this.saveParameterButton.Enabled = true;
      this.parameterIsSaveable = true;
      this.actualMMIDataHasBeenSelected = false;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void editMMIParameterPictureBox_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      // ISSUE: reference to a compiler-generated method
      Parameters.Instance.SetDictionary(MMIData.Instance.MMIParameters());
      this.parameterPanel.updateUIWithDictionaryValues();
      ((Control) this.parameterEditTabPage).Enabled = true;
      this.parameterDescriptionRichTextBox.Text = GlobalResource.ConfigPanel_parameterDescriptionRichTextBox_MMI_Text;
      this.parameterNameTextBox.Text = GlobalResource.ConfigPanel_parameterNameTextBox_MMI_Text;
      this.parameterDescriptionRichTextBox.Enabled = false;
      this.parameterNameTextBox.Enabled = false;
      this.parameterComboBox.SelectedIndexChanged -= new EventHandler(this.editParameterPictureBox_Click);
      this.parameterComboBox.SelectedIndex = 0;
      this.parameterComboBox.SelectedIndexChanged += new EventHandler(this.editParameterPictureBox_Click);
      this.profileComboBox.SelectedIndexChanged -= new EventHandler(this.profileComboBox_SelectedIndexChanged);
      this.profileComboBox.SelectedIndex = 0;
      this.profileComboBox.SelectedIndexChanged += new EventHandler(this.profileComboBox_SelectedIndexChanged);
      this.parameterPanel.ValueWasChanged = false;
      this.configTabControl.SelectedTab = this.parameterEditTabPage;
      this.parameterDescriptionRichTextBox.Enabled = false;
      this.parameterNameTextBox.Enabled = false;
      this.deleteParameterButton.Enabled = false;
      this.saveParameterButton.Enabled = false;
      this.saveParameterAsButton.Enabled = true;
      this.parameterIsSaveable = false;
      this.actualMMIDataHasBeenSelected = true;
      this.saveParameterAsButton.Focus();
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void saveParameterAsButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.parameterDescriptionRichTextBox.Text = "";
      this.parameterNameTextBox.Text = "";
      this.actualSelectedParameterItem = -1;
      this.parameterComboBox.SelectedIndexChanged -= new EventHandler(this.editParameterPictureBox_Click);
      this.parameterComboBox.SelectedValue = (object) "";
      this.parameterComboBox.SelectedItem = (object) "";
      this.parameterComboBox.SelectedIndex = -1;
      this.parameterComboBox.SelectedIndexChanged += new EventHandler(this.editParameterPictureBox_Click);
      this.parameterDescriptionRichTextBox.Enabled = true;
      this.parameterNameTextBox.Enabled = true;
      this.deleteParameterButton.Enabled = false;
      this.saveParameterButton.Enabled = true;
      this.parameterIsSaveable = true;
      this.saveParameterAsButton.Enabled = false;
      this.actualMMIDataHasBeenSelected = false;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void saveParameterButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      if (!this.SavingParameterWithoutQuestion())
      {
        this.parameterComboBox.SelectedIndex = this.newActualSelectedParameterItem + this.numberOfDefaultParameters;
        this.parameterPanel.ValueWasChanged = false;
      }
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void deleteParameterButton_Click(object sender, EventArgs e)
    {
      if (this.actualSelectedParameterItem < 0 || MessageBox.Show(GlobalResource.ConfigPanel_parameterDeleteButton_Message, GlobalResource.ConfigPanel_parameterDeleteButton_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      if (this.MMICom.DeviceConnected)
        --this.actualSelectedParameterItem;
      this.deleteParameterFromList(this.actualSelectedParameterItem);
      this.configTabControl.SelectedTab = this.parameterListTabPage;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void transmitParameterButton_Click(object sender, EventArgs e)
    {
      if (!this.parameterPanel.ValuesValidForConnectedMMI() || MessageBox.Show(GlobalResource.ParameterWheelCircumference_Message, GlobalResource.ParameterWheelCircumference_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      bool flag1 = false;
      bool valueWasChanged = this.parameterPanel.ValueWasChanged;
      DisconnectWarning dw = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      dw.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      if (this.profileComboBox.Visible && this.profileComboBox.SelectedIndex != -1)
      {
        if (!((string) this.profileComboBox.SelectedItem).Equals(this.actMMIDataLabel.Text))
          flag1 = this.transmitProfile(dw);
      }
      else
        flag1 = true;
      dw.ProgressBar().AdditionalText = GlobalResource.Configuration_Parameters;
      int maximum = (Parameters.Instance.ParameterElements - 1) * 3;
      int actualPosition = 1;
      if (!ActivationInformation.IsAllAccessActive)
        flag1 = this.transceiver.ReceiveAllAccessibleSettingsFromMMI(ref actualPosition, maximum, dw.ProgressBar());
      this.parameterPanel.updateDictionaryWithAccessableUIValues();
      bool flag2 = flag1 && this.transceiver.TransferAllAccessibleSettingsToMMI(ref actualPosition, maximum, dw.ProgressBar());
      Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
      thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread.Start();
      while (thread.IsAlive)
        Thread.Sleep(50);
      bool waitingForMmiSucceeded = this.transceiver.WaitingForMMISucceeded;
      bool allSettingsFromMmi = this.transceiver.ReceiveAllSettingsFromMMI(ref actualPosition, maximum, dw.ProgressBar());
      bool flag3 = flag2 && allSettingsFromMmi && waitingForMmiSucceeded;
      this.parameterPanel.updateUIWithDictionaryValues();
      dw.ProgressBar().Value = 100;
      dw.ProgressBar().Refresh();
      dw.Dispose();
      if (flag3)
      {
        if (ActivationInformation.VersionLevelProperty >= 3)
          new ParameterTransmissionLogger().Write(Parameters.Instance.MotorSerialNumber, Parameters.Instance.MMISerialNumber, Parameters.Instance.AccuSerialNumber, MainWindow.Instance.ProductionNumber, DateTime.Now.ToString("yyyyMMddHHmmss"));
        MMIData.Instance.SetParameters(Parameters.Instance.Dictionary);
        MMIData.Instance.MMISerialNumber = Parameters.Instance.MMISerialNumber;
      }
      this.parameterPanel.ValueWasChanged = valueWasChanged;
      FinishSound.Instance.Play();
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void profileComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      object selectedItem = ((ComboBox) sender).SelectedItem;
      this.actualSelectedProfileItem = ((ListControl) sender).SelectedIndex;
      int selectedProfileItem = this.actualSelectedProfileItem;
      if (this.MMICom.DeviceConnected)
      {
        --selectedProfileItem;
        if (selectedProfileItem < 0)
        {
          this.profileComboToolTip.Active = false;
          return;
        }
      }
      this.profileComboBox.SelectedIndexChanged -= new EventHandler(this.profileComboBox_SelectedIndexChanged);
      this.profileComboBox.SelectedIndex = this.actualSelectedProfileItem;
      this.profileComboBox.SelectedIndexChanged += new EventHandler(this.profileComboBox_SelectedIndexChanged);
      if (!this.ReadProfileFile(this.defaultProfileList.ContentFile(selectedProfileItem), true))
        return;
      this.profileComboToolTip.Active = true;
      this.profileComboToolTip.SetToolTip((Control) this.profileComboBox, this.defaultProfileList.Description(selectedProfileItem));
      this.profilePanel.ValueWasChanged = false;
    }

    private bool transmitProfile(DisconnectWarning dw)
    {
      bool flag1 = false;
      int maximum = 6;
      int actualPosition = 1;
      dw.ProgressBar().AdditionalText = GlobalResource.Configuration_Profile;
      HelperClass.DoEvents();
      flag1 = this.transceiver.ReceiveSettingsFromMMI(ParameterIds.MMI_DRIVE_SETTINGS, ref actualPosition, maximum, dw.ProgressBar()) && this.transceiver.ReceiveSettingsFromMMI(ParameterIds.MMI_SHUTDOWN_OVERTRAVEL, ref actualPosition, maximum, dw.ProgressBar());
      this.profilePanel.updateDictionaryWithUIValues();
      flag1 = this.transceiver.TransferSettingToMMI(ParameterIds.MMI_DRIVE_SETTINGS, ref actualPosition, maximum, dw.ProgressBar()) && this.transceiver.TransferSettingToMMI(ParameterIds.MMI_SHUTDOWN_OVERTRAVEL, ref actualPosition, maximum, dw.ProgressBar());
      Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
      thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread.Start();
      while (thread.IsAlive)
        Thread.Sleep(50);
      bool flag2 = this.transceiver.ReceiveSettingsFromMMI(ParameterIds.MMI_DRIVE_SETTINGS, ref actualPosition, maximum, dw.ProgressBar()) && this.transceiver.ReceiveSettingsFromMMI(ParameterIds.MMI_SHUTDOWN_OVERTRAVEL, ref actualPosition, maximum, dw.ProgressBar()) && this.transceiver.WaitingForMMISucceeded;
      this.profilePanel.updateUIWithDictionaryValues();
      return flag2;
    }

    private void parameterComboBoxAddActMMI()
    {
      if (this.MMICom.DeviceConnected)
      {
        try
        {
          if (this.parameterComboBox.Items[0].ToString().Equals(this.actMMIDataLabel.Text))
            return;
          this.parameterComboBox.Items.Insert(0, (object) this.actMMIDataLabel.Text);
        }
        catch (Exception ex)
        {
          this.parameterComboBox.Items.Insert(0, (object) this.actMMIDataLabel.Text);
        }
      }
      else
        this.parameterComboBox.Items.Remove((object) this.actMMIDataLabel.Text);
    }

    public ConfigurationPanel(Communication MMICom)
    {
      this.InitializeComponent();
      Thread.CurrentThread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      ActivationInformation.IsAllAccessActive = false;
      this.MMICom = MMICom;
      this.transceiver = new MMITransceiver(this.MMICom);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      MainWindow.Instance.HelloLoopPassed += new MainWindow.HelloLoopPassedEventHandler(this.HelloLoopPassedHandler);
      MainWindow.Instance.AccessLevelUpdate += new MainWindow.AccessLevelUpdateHanlder(this.AccessLevelUpdateHanlder);
      MMICom.Removed += new Communication.RemovedEventHandler(this.MMIRemovedHandler);
      this.configTabControl.SelectedIndexChanged += new EventHandler(this.configTabControl_SelectedIndexChanged);
      this.configTabControl.Selecting += new TabControlCancelEventHandler(this.configTabControl_Selecting);
      this.autoRecTabPage.Leave += new EventHandler(this.autoTabPage_Leave);
      this.autoTransTabPage.Leave += new EventHandler(this.autoTabPage_Leave);
      this.parameterPanel.ValueChanged += new ParameterPanel.ValueChangedEventHandler(this.parameterPanel_ValueChanged);
      this.DefaultUISettings();
      this.MouseWheelRedirector(true);
      this.InitStartScreen();
      this.InitParameter();
      this.InitFastTransElements();
      this.lastSelectedWasAuto = false;
      this.lastTabWasEditParameter = false;
      this.canelLeavingActualTab = false;
      this.speakerActivated = true;
      this.mmiToolTip = new SmartToolTip();
      this.mmiToolTip.ShowAlways = true;
      this.mmiToolTip.SetToolTip((Control) this.editMMIParameterPictureBox, GlobalResource.EditActualMMIData);
      if (ActivationInformation.VersionLevelProperty >= 3)
      {
        this.profileComboBox.Enabled = false;
        this.profileComboBox.Visible = false;
        this.blueLinePictureBox2.Visible = false;
        this.profileSelectionLabel.Visible = false;
        int num = this.profileComboBox.Height + this.blueLinePictureBox2.Height + 5;
        this.parameterPanel.Location = new Point(this.parameterPanel.Location.X, this.parameterPanel.Location.Y - num);
        this.parameterPanel.Size = new Size(this.parameterPanel.Width, this.parameterPanel.Height + num);
        if (ActivationInformation.VersionLevelProperty == 4 && MainWindow.Instance.Testmode)
        {
          this.allAccessCheckBox.Enabled = true;
          this.allAccessCheckBox.Visible = true;
        }
      }
      else
      {
        this.configTabControl.Controls.Remove((Control) this.autoTransTabPage);
        this.configTabControl.Controls.Remove((Control) this.autoRecTabPage);
        this.configTabControl.Controls.Remove((Control) this.startScreensTabPage);
      }
      this.EnableFunctions(MMICom.DeviceConnected);
      ((Control) this.parameterEditTabPage).Enabled = false;
      this.transOnlyPanel.Visible = false;
    }

    private void parameterPanel_ValueChanged(object sender, ParameterPanelEventArgs e)
    {
      if (!this.parameterIsSaveable)
        return;
      this.saveParameterButton.Enabled = e.ValueChanged;
    }

    protected override void DestroyHandle()
    {
      this.MMICom.Removed -= new Communication.RemovedEventHandler(this.MMIRemovedHandler);
      MainWindow.Instance.HelloLoopPassed -= new MainWindow.HelloLoopPassedEventHandler(this.HelloLoopPassedHandler);
      MainWindow.Instance.AccessLevelUpdate -= new MainWindow.AccessLevelUpdateHanlder(this.AccessLevelUpdateHanlder);
      this.configTabControl.SelectedIndexChanged -= new EventHandler(this.configTabControl_SelectedIndexChanged);
      this.autoRecTabPage.Leave -= new EventHandler(this.autoTabPage_Leave);
      this.autoTransTabPage.Leave -= new EventHandler(this.autoTabPage_Leave);
      base.DestroyHandle();
    }

    public bool ValueChanged => this.parameterPanel.ValueWasChanged;

    public bool SaveChanges()
    {
      if (!this.lastTabWasEditParameter || !this.parameterPanel.ValueWasChanged)
        return true;
      this.SavingParameter();
      return !this.canelLeavingActualTab;
    }

    private void speakerPictureBox_Click(object sender, EventArgs e)
    {
      if (this.speakerActivated)
      {
        this.speakerPictureBox.Image = HelperClass.ResizeImage((Image) Resources.volume_off, new Size(27, 20), true);
        this.receiveSpeakerPictureBox.Image = HelperClass.ResizeImage((Image) Resources.volume_off, new Size(27, 20), true);
        this.speakerActivated = false;
      }
      else
      {
        this.speakerPictureBox.Image = HelperClass.ResizeImage((Image) Resources.volume_on, new Size(27, 20), true);
        this.receiveSpeakerPictureBox.Image = HelperClass.ResizeImage((Image) Resources.volume_on, new Size(27, 20), true);
        this.speakerActivated = true;
      }
    }

    private void receiveTabIconPictureBox_Click(object sender, EventArgs e)
    {
      if (!this.SetProductionNumber())
        return;
      this.ProductionLogging();
    }

    private void configTabControl_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.configTabControl.SelectedTab == this.autoTransTabPage || this.configTabControl.SelectedTab == this.autoRecTabPage)
      {
        MainWindow.Instance.ReadCompleteMMIData = false;
      }
      else
      {
        if (!MainWindow.Instance.MMIDataWasCompletelyRead && this.lastSelectedWasAuto && this.MMICom.DeviceConnected)
        {
          MainWindow.Instance.ReadCompleteMMIData = true;
          MainWindow.Instance.WaitingForHello();
        }
        MainWindow.Instance.ReadCompleteMMIData = true;
        this.lastSelectedWasAuto = false;
      }
      this.transOnlyPanel.Visible = false;
    }

    private void configTabControl_Selecting(object sender, TabControlCancelEventArgs e)
    {
      if (this.lastTabWasEditParameter && this.parameterPanel.ValueWasChanged)
        this.SavingParameter();
      if (this.canelLeavingActualTab)
      {
        e.Cancel = this.canelLeavingActualTab;
        this.canelLeavingActualTab = false;
      }
      else if (this.configTabControl.SelectedTab == this.parameterEditTabPage)
      {
        this.parameterPanel.ValueWasChanged = false;
        this.lastTabWasEditParameter = true;
      }
      else
      {
        this.lastTabWasEditParameter = false;
        this.parameterDescriptionRichTextBox.Text = "";
        this.parameterNameTextBox.Text = "";
        this.parameterComboBox.SelectedIndexChanged -= new EventHandler(this.editParameterPictureBox_Click);
        this.parameterComboBox.SelectedValue = (object) "";
        this.parameterComboBox.SelectedItem = (object) "";
        this.parameterComboBox.SelectedIndex = -1;
        this.parameterComboBox.SelectedIndexChanged += new EventHandler(this.editParameterPictureBox_Click);
        this.profileComboBox.SelectedIndexChanged -= new EventHandler(this.profileComboBox_SelectedIndexChanged);
        this.profileComboBox.SelectedValue = (object) "";
        this.profileComboBox.SelectedItem = (object) "";
        this.profileComboBox.SelectedIndex = -1;
        this.profileComboBox.SelectedIndexChanged += new EventHandler(this.profileComboBox_SelectedIndexChanged);
        this.parameterPanel.ValueWasChanged = false;
        this.profilePanel.ValueWasChanged = false;
        ((Control) this.parameterEditTabPage).Enabled = false;
      }
    }

    private void autoTabPage_Leave(object sender, EventArgs e) => this.lastSelectedWasAuto = true;

    private void HelloLoopPassedHandler(object mainWindow, MainWindowEventArgs args)
    {
      this.EnableFunctions(args.Done);
      this.EnableMassFunctionality(args.Done);
    }

    private void MMIRemovedHandler(object sender, EventArgs e) => this.EnableFunctions(false);

    private void EnableMassFunctionality(bool value)
    {
      if (!this.autoRecTabPage.InvokeRequired)
      {
        if (!this.autoTransTabPage.InvokeRequired)
        {
          if (!this.configTabControl.InvokeRequired)
          {
            if (!this.transOnlyPanel.InvokeRequired)
            {
              if (!this.massTabActualLabel.InvokeRequired)
              {
                if (value && this.configTabControl.SelectedTab.Equals((object) this.autoTransTabPage) && (this.transOnlyPanel.Visible && this.massTabActualLabel.Text != ""))
                {
                  this.massTabActualLabel.ForeColor = ColorDefinition.BlackTextColor;
                  this.MassTransferParameterDataToMMI();
                  return;
                }
                if (!this.configTabControl.SelectedTab.Equals((object) this.autoRecTabPage) || !value)
                  return;
                this.ReceiveParameterDataFromMMI();
                return;
              }
            }
          }
        }
      }
      try
      {
        this.Invoke((Delegate) new ConfigurationPanel.FunctionActivation(this.EnableMassFunctionality), (object) value);
      }
      catch (Exception ex)
      {
        GlobalLogger.Instance.WriteLine(ex);
      }
    }

    private void EnableFunctions(bool value)
    {
      if (!this.parameterEditTabPage.InvokeRequired)
      {
        if (!this.autoRecTabPage.InvokeRequired)
        {
          if (!this.autoTransTabPage.InvokeRequired)
          {
            if (!this.configTabControl.InvokeRequired)
            {
              if (!this.receiveTabIconPictureBox.InvokeRequired)
              {
                if (!this.editMMIParameterPictureBox.InvokeRequired)
                {
                  if (!this.parameterComboBox.InvokeRequired)
                  {
                    if (!this.profileComboBox.InvokeRequired)
                    {
                      if (!this.transmitParameterButton.InvokeRequired)
                      {
                        if (!this.receiveTabOkPictureBox.InvokeRequired)
                        {
                          if (!this.receiveLabel.InvokeRequired)
                          {
                            if (!this.massTabOkPictureBox.InvokeRequired)
                            {
                              if (!this.parameterPanel.InvokeRequired)
                              {
                                if (!this.actMMIDataLabel.InvokeRequired)
                                {
                                  if (value)
                                  {
                                    this.receiveTabIconPictureBox.Image = (Image) Resources.Icon_mReceive_Active;
                                    this.mmiToolTip.ShowAlways = true;
                                    this.editMMIParameterPictureBox.Image = (Image) Resources.MMI_active;
                                    this.actMMIDataLabel.Font = FontDefinition.TableCaptionBoldTextFont;
                                    try
                                    {
                                      if (!this.parameterComboBox.Items[0].ToString().Equals(this.actMMIDataLabel.Text))
                                      {
                                        this.parameterComboBox.Items.Insert(0, (object) this.actMMIDataLabel.Text);
                                        if (this.actualSelectedParameterItem != -1)
                                          ++this.actualSelectedParameterItem;
                                      }
                                    }
                                    catch (Exception ex)
                                    {
                                      this.parameterComboBox.Items.Insert(0, (object) this.actMMIDataLabel.Text);
                                      if (this.actualSelectedParameterItem != -1)
                                        ++this.actualSelectedParameterItem;
                                    }
                                    try
                                    {
                                      if (!this.profileComboBox.Items[0].ToString().Equals(this.actMMIDataLabel.Text))
                                        this.profileComboBox.Items.Insert(0, (object) this.actMMIDataLabel.Text);
                                    }
                                    catch (Exception ex)
                                    {
                                      this.profileComboBox.Items.Insert(0, (object) this.actMMIDataLabel.Text);
                                    }
                                  }
                                  else
                                  {
                                    this.receiveTabIconPictureBox.Image = (Image) Resources.Icon_mReceive_Inactive;
                                    this.mmiToolTip.ShowAlways = false;
                                    this.editMMIParameterPictureBox.Image = (Image) Resources.MMI_inactive;
                                    this.actMMIDataLabel.Font = FontDefinition.TableCaptionTextFont;
                                    this.parameterComboBox.Items.Remove((object) this.actMMIDataLabel.Text);
                                    if (this.actualSelectedParameterItem != -1)
                                      --this.actualSelectedParameterItem;
                                    this.profileComboBox.Items.Remove((object) this.actMMIDataLabel.Text);
                                    foreach (PictureBox pictureBox in this.fastParameterTransmissionOkPictuerBox)
                                      pictureBox.Image = (Image) null;
                                  }
                                  this.deleteStartScreenFromMMIButton.Enabled = value;
                                  this.receiveTabIconPictureBox.Enabled = value;
                                  this.transmitParameterButton.Enabled = value;
                                  foreach (PictureBox pictureBox in this.fastParameterTransmissionPictuerBox)
                                  {
                                    pictureBox.Enabled = value;
                                    pictureBox.Image = !value ? (Image) Resources.Icon_sTransceive_Inactive : (Image) Resources.Icon_sTransceive_Active;
                                  }
                                  this.editMMIParameterPictureBox.Enabled = value;
                                  if (value)
                                    return;
                                  this.receiveTabOkPictureBox.Image = (Image) null;
                                  this.receiveLabel.Text = string.Empty;
                                  this.massTabOkPictureBox.Image = (Image) null;
                                  if (!this.configTabControl.SelectedTab.Equals((object) this.parameterEditTabPage) || !this.actualMMIDataHasBeenSelected)
                                    return;
                                  this.parameterPanel.ValueWasChanged = false;
                                  this.configTabControl.SelectedTab = this.parameterListTabPage;
                                  return;
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      try
      {
        this.Invoke((Delegate) new ConfigurationPanel.FunctionActivation(this.EnableFunctions), (object) value);
      }
      catch (Exception ex)
      {
        GlobalLogger.Instance.WriteLine(ex);
      }
    }

    private void backButton_Click(object sender, EventArgs e)
    {
      this.transOnlyPanel.Visible = false;
      this.massTabOkPictureBox.Image = (Image) null;
    }

    private void allAccessCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.allAccessCheckBox.Checked)
      {
        if (MessageBox.Show("Übertragung aller Parameter wirklich aktivieren?", "AllAccess Mode", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          ActivationInformation.IsAllAccessActive = true;
          int num = (int) MessageBox.Show("Mit dieser Einstellung werden auch Seriennummern überschrieben!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          this.parameterPanel.EnableAllElements();
        }
        else
        {
          ActivationInformation.IsAllAccessActive = false;
          this.allAccessCheckBox.Checked = false;
        }
      }
      else
      {
        ActivationInformation.IsAllAccessActive = false;
        this.parameterPanel.DisableElements();
      }
    }

    private void AccessLevelUpdateHanlder(object update, MainWindowEventArgs args)
    {
      if (!args.Done)
        return;
      if (ActivationInformation.VersionLevelProperty < 4)
      {
        this.allAccessCheckBox.Visible = false;
        this.allAccessCheckBox.Enabled = false;
        ActivationInformation.IsAllAccessActive = false;
      }
      else
      {
        this.allAccessCheckBox.Visible = true;
        this.allAccessCheckBox.Enabled = true;
      }
    }

    private void parameterText_TextChanged(object sender, EventArgs e) => this.parameterPanel.ValueWasChanged = true;

    private void InitStartScreen()
    {
      this.thumbnailsPictureBoxes = new PictureBox[0];
      this.colorThumbnailsPictureBoxes = new PictureBox[0];
      this.screenDescriptions = new Label[0];
      this.trashPictureBoxes = new PictureBox[0];
      this.screenEditToolTip = new SmartToolTip[0];
      this.screenDeleteToolTip = new SmartToolTip[0];
      this.pictureListTableLayoutPanel.RowCount = 1;
      this.pictureListTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
      this.FillStartScreensTablePanel();
    }

    private void editStartScreenButton_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      ImagePreview imagePreview = new ImagePreview(this.MMICom, PictureIds.STARTSCREEN, false);
      StartScreensListFile content1;
      bool flag;
      StartScreenImageFile content2;
      if (int32 < this.numberOfDefaultStartScreen)
      {
        content1 = this.defaultStartScreenList;
        flag = true;
        content2 = StartScreenImageFile.ReadFile(Directories.Instance.DefaultsPath + this.defaultStartScreenList.Filename(int32));
      }
      else
      {
        content1 = this.startScreensList;
        int32 -= this.numberOfDefaultStartScreen;
        flag = false;
        content2 = StartScreenImageFile.ReadFile(Directories.Instance.UserDataPath + this.startScreensList.Filename(int32));
      }
      imagePreview.MonochromeImageForMMI = content2.Image;
      imagePreview.PictureName = content1.Description(int32);
      if (imagePreview.ShowDialog() != DialogResult.OK)
        return;
      content2.Image = imagePreview.MonochromeImageForMMI;
      try
      {
        if (flag)
        {
          int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultScreen_Message, GlobalResource.ConfigPanel_Update_DefaultScreen_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        StartScreenImageFile.WriteFile(content2, Directories.Instance.UserDataPath + content1.Filename(int32));
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MMI_EDIT_IMAGE_WRITE_ERROR);
        return;
      }
      content1.Description(int32, imagePreview.PictureName);
      content1.Thumbnail(imagePreview.ThumbnailImage, int32);
      content1.SortListElements();
      try
      {
        if (flag)
        {
          int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultScreen_Message, GlobalResource.ConfigPanel_Update_DefaultScreen_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        StartScreensListFile.WriteFile(content1, Directories.Instance.UserDataPath + Directories.Instance.StartScreensFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.IMAGELISTFILE_EDIT_WRITE_ERROR);
        return;
      }
      this.FillStartScreensTablePanel();
      this.screens = new List<ScreenElement>((IEnumerable<ScreenElement>) this.defaultStartScreenList.Screens);
      this.screens.AddRange((IEnumerable<ScreenElement>) this.startScreensList.Screens);
      this.FillFastParametersTablePanel();
      this.EnableFunctions(this.MMICom.DeviceConnected);
    }

    private void editColorStartScreenButton_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      ImagePreview imagePreview = new ImagePreview(this.MMICom, PictureIds.STARTSCREEN, true);
      StartScreensListFile content1;
      bool flag;
      StartScreenImageFile content2;
      if (int32 < this.numberOfDefaultStartScreen)
      {
        content1 = this.defaultStartScreenList;
        flag = true;
        content2 = StartScreenImageFile.ReadFile(Directories.Instance.DefaultsPath + this.defaultStartScreenList.ColorFilename(int32));
      }
      else
      {
        content1 = this.startScreensList;
        int32 -= this.numberOfDefaultStartScreen;
        flag = false;
        content2 = StartScreenImageFile.ReadFile(Directories.Instance.UserDataPath + this.startScreensList.ColorFilename(int32));
      }
      imagePreview.Colored565ImageForMMI = content2.Image;
      imagePreview.PictureName = content1.Description(int32);
      if (imagePreview.ShowDialog() != DialogResult.OK)
        return;
      content2.Image = imagePreview.Colored565ImageForMMI;
      try
      {
        if (flag)
        {
          int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultScreen_Message, GlobalResource.ConfigPanel_Update_DefaultScreen_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        if (imagePreview.IsImageEmpty)
        {
          string str = content1.ColorFilename(int32);
          try
          {
            if (!string.IsNullOrEmpty(str))
              File.Delete(Directories.Instance.UserDataPath + str);
          }
          catch (Exception ex)
          {
          }
          this.UpdateStartScreenInListFile(content1.Filename(int32), string.Empty, true);
          content1.ColorFilename(int32, string.Empty);
          imagePreview.ColorThumbnailImage = (Image) null;
        }
        else
        {
          if (string.IsNullOrEmpty(content1.ColorFilename(int32)))
          {
            string str = "Screen_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_c.xml";
            content1.ColorFilename(int32, "Screen_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_c.xml");
          }
          this.UpdateStartScreenInListFile(content1.Filename(int32), content1.ColorFilename(int32), true);
          StartScreenImageFile.WriteFile(content2, Directories.Instance.UserDataPath + content1.ColorFilename(int32));
        }
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MMI_EDIT_IMAGE_WRITE_ERROR);
        return;
      }
      content1.Description(int32, imagePreview.PictureName);
      content1.ColorThumbnail(imagePreview.ColorThumbnailImage, int32);
      content1.SortListElements();
      try
      {
        if (flag)
        {
          int num = (int) MessageBox.Show(GlobalResource.ConfigPanel_Update_DefaultScreen_Message, GlobalResource.ConfigPanel_Update_DefaultScreen_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        StartScreensListFile.WriteFile(content1, Directories.Instance.UserDataPath + Directories.Instance.StartScreensFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.IMAGELISTFILE_EDIT_WRITE_ERROR);
        return;
      }
      this.FillStartScreensTablePanel();
      this.screens = new List<ScreenElement>((IEnumerable<ScreenElement>) this.defaultStartScreenList.Screens);
      this.screens.AddRange((IEnumerable<ScreenElement>) this.startScreensList.Screens);
      this.FillFastParametersTablePanel();
      this.EnableFunctions(this.MMICom.DeviceConnected);
    }

    private void deleteStartScreenButton_Click(object sender, EventArgs e)
    {
      int int32 = Convert.ToInt32(((Control) sender).Name);
      if (int32 < this.numberOfDefaultStartScreen)
      {
        int num1 = (int) MessageBox.Show(GlobalResource.ConfigPanel_Delete_DefaultScreen_Message, GlobalResource.ConfigPanel_Delete_DefaultScreen_Caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        if (MessageBox.Show(GlobalResource.ConfigPanel_ScreenDelete_Message, GlobalResource.ConfigPanel_ScreenDelete_Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
          return;
        int num2 = int32 - this.numberOfDefaultStartScreen;
        string str = this.startScreensList.Filename(num2);
        string colorImageName = this.startScreensList.ColorFilename(num2);
        this.RemoveStartScreenFromListFile(str, colorImageName);
        this.startScreensList.RemoveListElement(num2);
        try
        {
          StartScreensListFile.WriteFile(this.startScreensList, Directories.Instance.UserDataPath + Directories.Instance.StartScreensFile);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.IMAGELISTFILE_DELETE_WRITE_ERROR);
          return;
        }
        try
        {
          if (!string.IsNullOrEmpty(str))
            File.Delete(Directories.Instance.UserDataPath + str);
          if (!string.IsNullOrEmpty(colorImageName))
            File.Delete(Directories.Instance.UserDataPath + colorImageName);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.MMI_IMAGE_DELETE_ERROR, str);
        }
        this.FillStartScreensTablePanel();
        this.screens = new List<ScreenElement>((IEnumerable<ScreenElement>) this.defaultStartScreenList.Screens);
        this.screens.AddRange((IEnumerable<ScreenElement>) this.startScreensList.Screens);
      }
    }

    private void newStartScreenButton_Click(object sender, EventArgs e)
    {
      ImagePreview imagePreview = new ImagePreview(this.MMICom, PictureIds.STARTSCREEN, false);
      if (imagePreview.ShowDialog() != DialogResult.OK)
        return;
      StartScreenImageFile content = new StartScreenImageFile();
      content.Image = imagePreview.MonochromeImageForMMI;
      string filename = "Screen_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".xml";
      try
      {
        StartScreenImageFile.WriteFile(content, Directories.Instance.UserDataPath + filename);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MMI_IMAGE_WRITE_ERROR);
        return;
      }
      this.startScreensList.AddScreenElement(imagePreview.PictureName, filename, imagePreview.ThumbnailImage, (string) null, (Image) null, true);
      this.startScreensList.SortListElements();
      try
      {
        StartScreensListFile.WriteFile(this.startScreensList, Directories.Instance.UserDataPath + Directories.Instance.StartScreensFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.IMAGELISTFILE_WRITE_ERROR);
        return;
      }
      this.FillStartScreensTablePanel();
      this.screens = new List<ScreenElement>((IEnumerable<ScreenElement>) this.defaultStartScreenList.Screens);
      this.screens.AddRange((IEnumerable<ScreenElement>) this.startScreensList.Screens);
    }

    private void deleteStartScreenFromMMIButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      disconnectWarning.ProgressBar().AdditionalText = GlobalResource.Configuration_StartScreen;
      HelperClass.DoEvents();
      int maximum = 1;
      int actualPosition = 0;
      new MMITransceiver(this.MMICom).DeleteStartScreenFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      disconnectWarning.ProgressBar().Value = 100;
      disconnectWarning.ProgressBar().Refresh();
      disconnectWarning.Dispose();
      FinishSound.Instance.Play();
      this.Cursor = Cursors.Default;
      MainWindow.Instance.Cursor = Cursors.Default;
      this.Focus();
    }

    private void RemoveStartScreenFromListFile(string imageName, string colorImageName)
    {
      int position1 = -1;
      int position2 = -1;
      bool flag1 = false;
      if (!string.IsNullOrEmpty(imageName))
        position1 = this.defaultParameterList.StartScreenListElement(imageName);
      for (; position1 != -1; position1 = this.defaultParameterList.StartScreenListElement(imageName))
      {
        flag1 = true;
        this.defaultParameterList.StartScreenFileName(position1, string.Empty);
        this.defaultParameterList.ColorStartScreenFileName(position1, string.Empty);
      }
      if (!string.IsNullOrEmpty(colorImageName))
        position2 = this.defaultParameterList.ColorStartScreenListElement(colorImageName);
      for (; position2 != -1; position2 = this.defaultParameterList.ColorStartScreenListElement(colorImageName))
      {
        flag1 = true;
        this.defaultParameterList.StartScreenFileName(position2, string.Empty);
        this.defaultParameterList.ColorStartScreenFileName(position2, string.Empty);
      }
      if (flag1)
      {
        try
        {
          ListSettingFile.WriteFile(this.defaultParameterList, Directories.Instance.DefaultParametersListFileName);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.DEFAULT_PARAMETERLISTFILE_REMOVE_WRITE_ERROR);
          return;
        }
      }
      bool flag2 = false;
      int position3 = -1;
      int position4 = -1;
      if (!string.IsNullOrEmpty(imageName))
        position3 = this.parameterList.StartScreenListElement(imageName);
      for (; position3 != -1; position3 = this.parameterList.StartScreenListElement(imageName))
      {
        flag2 = true;
        this.parameterList.StartScreenFileName(position3, string.Empty);
        this.parameterList.ColorStartScreenFileName(position3, string.Empty);
      }
      if (!string.IsNullOrEmpty(colorImageName))
        position4 = this.parameterList.ColorStartScreenListElement(colorImageName);
      for (; position4 != -1; position4 = this.parameterList.ColorStartScreenListElement(colorImageName))
      {
        flag2 = true;
        this.parameterList.StartScreenFileName(position4, string.Empty);
        this.parameterList.ColorStartScreenFileName(position4, string.Empty);
      }
      if (!flag2)
        return;
      try
      {
        ListSettingFile.WriteFile(this.parameterList, Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.PARAMETERLISTFILE_REMOVE_WRITE_ERROR);
        return;
      }
      this.FillFastParametersTablePanel();
      this.EnableFunctions(this.MMICom.DeviceConnected);
    }

    private void UpdateStartScreenInListFile(
      string imageName,
      string colorImageName,
      bool updateColorImage)
    {
      bool flag1 = false;
      if (updateColorImage)
      {
        if (!string.IsNullOrEmpty(imageName))
        {
          foreach (int screenListElement in this.defaultParameterList.StartScreenListElements(imageName))
          {
            flag1 = true;
            this.defaultParameterList.ColorStartScreenFileName(screenListElement, colorImageName);
          }
        }
      }
      else if (!string.IsNullOrEmpty(colorImageName))
      {
        foreach (int screenListElement in this.defaultParameterList.ColorStartScreenListElements(colorImageName))
        {
          flag1 = true;
          this.defaultParameterList.StartScreenFileName(screenListElement, imageName);
        }
      }
      if (flag1)
      {
        try
        {
          ListSettingFile.WriteFile(this.defaultParameterList, Directories.Instance.DefaultParametersListFileName);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.DEFAULT_PARAMETERLISTFILE_REMOVE_WRITE_ERROR);
          return;
        }
      }
      bool flag2 = false;
      if (updateColorImage)
      {
        if (!string.IsNullOrEmpty(imageName))
        {
          foreach (int screenListElement in this.parameterList.StartScreenListElements(imageName))
          {
            flag2 = true;
            this.parameterList.ColorStartScreenFileName(screenListElement, colorImageName);
          }
        }
      }
      else if (!string.IsNullOrEmpty(colorImageName))
      {
        foreach (int screenListElement in this.parameterList.ColorStartScreenListElements(colorImageName))
        {
          flag2 = true;
          this.parameterList.StartScreenFileName(screenListElement, imageName);
        }
      }
      if (!flag2)
        return;
      try
      {
        ListSettingFile.WriteFile(this.parameterList, Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.PARAMETERLISTFILE_REMOVE_WRITE_ERROR);
        return;
      }
      this.FillFastParametersTablePanel();
      this.EnableFunctions(this.MMICom.DeviceConnected);
    }

    private void FillStartScreensTablePanel()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.SuspendLayout();
      this.pictureListTableLayoutPanel.SuspendLayout();
      this.defaultStartScreenList = StartScreensListFile.ReadFile(Directories.Instance.DefaultStartScreensFileName);
      this.startScreensList = StartScreensListFile.ReadFile(Directories.Instance.UserDataPath + Directories.Instance.StartScreensFile);
      this.numberOfDefaultStartScreen = this.defaultStartScreenList.Length;
      for (int index = 0; index < this.pictureListTableLayoutPanel.RowCount - 1; ++index)
      {
        UIElements.MouseWheelRedirector.Detach((Control) this.screenDescriptions[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.thumbnailsPictureBoxes[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.colorThumbnailsPictureBoxes[index]);
        UIElements.MouseWheelRedirector.Detach((Control) this.trashPictureBoxes[index]);
        this.thumbnailsPictureBoxes[index].Dispose();
        this.colorThumbnailsPictureBoxes[index].Dispose();
        this.screenDescriptions[index].Dispose();
        this.trashPictureBoxes[index].Dispose();
        this.screenEditToolTip[index].Dispose();
        this.screenDeleteToolTip[index].Dispose();
        this.pictureListTableLayoutPanel.RowStyles.Clear();
      }
      this.pictureListTableLayoutPanel.RowCount = 1;
      this.pictureListTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
      this.pictureTableCaptionLabel.Visible = false;
      this.pictureListTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
      this.pictureListTableLayoutPanel.AutoSize = true;
      this.thumbnailsPictureBoxes = new PictureBox[this.numberOfDefaultStartScreen + this.startScreensList.Length];
      this.colorThumbnailsPictureBoxes = new PictureBox[this.numberOfDefaultStartScreen + this.startScreensList.Length];
      this.screenDescriptions = new Label[this.numberOfDefaultStartScreen + this.startScreensList.Length];
      this.trashPictureBoxes = new PictureBox[this.numberOfDefaultStartScreen + this.startScreensList.Length];
      this.screenEditToolTip = new SmartToolTip[this.numberOfDefaultStartScreen + this.startScreensList.Length];
      this.screenDeleteToolTip = new SmartToolTip[this.numberOfDefaultStartScreen + this.startScreensList.Length];
      this.pictureListTableLayoutPanel.RowCount = this.numberOfDefaultStartScreen + this.startScreensList.Length + 1;
      for (int index = 0; index < this.numberOfDefaultStartScreen; ++index)
        this.CreateElementsForStartScreenList(index, this.defaultStartScreenList.Description(index), this.defaultStartScreenList.ThumbnailImage(index), this.defaultStartScreenList.ColorThumbnailImage(index), this.defaultStartScreenList.IsDeletable(index));
      for (int defaultStartScreen = this.numberOfDefaultStartScreen; defaultStartScreen < this.numberOfDefaultStartScreen + this.startScreensList.Length; ++defaultStartScreen)
      {
        int position = defaultStartScreen - this.numberOfDefaultStartScreen;
        this.CreateElementsForStartScreenList(defaultStartScreen, this.startScreensList.Description(position), this.startScreensList.ThumbnailImage(position), this.startScreensList.ColorThumbnailImage(position), this.startScreensList.IsDeletable(position));
      }
      if (this.pictureListTableLayoutPanel.RowCount > 1)
        this.pictureTableCaptionLabel.Visible = true;
      if (this.pictureListTableLayoutPanel.AutoSize)
        this.pictureListTableLayoutPanel.Size = this.pictureListTableLayoutPanel.GetPreferredSize(this.pictureListTableLayoutPanel.Size);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.screenDescriptions);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.thumbnailsPictureBoxes);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.colorThumbnailsPictureBoxes);
      UIElements.MouseWheelRedirector.Attach((Control[]) this.trashPictureBoxes);
      this.pictureListTableLayoutPanel.ResumeLayout(false);
      this.pictureListTableLayoutPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void CreateElementsForStartScreenList(
      int listCounter,
      string description,
      Image thumbnail,
      Image colorThumbnail,
      bool isDeletable)
    {
      this.screenDescriptions[listCounter] = new Label();
      this.screenDescriptions[listCounter].SuspendLayout();
      this.screenDescriptions[listCounter].Text = description;
      this.screenDescriptions[listCounter].TextAlign = ContentAlignment.MiddleLeft;
      this.screenDescriptions[listCounter].Dock = DockStyle.Fill;
      this.screenDescriptions[listCounter].Font = FontDefinition.TableCaptionTextFont;
      this.screenDescriptions[listCounter].ForeColor = ColorDefinition.BlackTextColor;
      this.screenDescriptions[listCounter].BackColor = Color.Transparent;
      this.screenDescriptions[listCounter].Margin = this.defaultPadding;
      this.screenDescriptions[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.screenDescriptions[listCounter].ResumeLayout(false);
      this.screenDescriptions[listCounter].PerformLayout();
      this.thumbnailsPictureBoxes[listCounter] = new PictureBox();
      this.thumbnailsPictureBoxes[listCounter].SuspendLayout();
      this.thumbnailsPictureBoxes[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.thumbnailsPictureBoxes[listCounter].Image = thumbnail;
      if (thumbnail == null)
        this.thumbnailsPictureBoxes[listCounter].Image = (Image) Resources.screen_empty_active;
      this.thumbnailsPictureBoxes[listCounter].SizeMode = PictureBoxSizeMode.Zoom;
      this.thumbnailsPictureBoxes[listCounter].Margin = this.defaultPadding;
      this.thumbnailsPictureBoxes[listCounter].TabStop = false;
      this.thumbnailsPictureBoxes[listCounter].Dock = DockStyle.Fill;
      this.thumbnailsPictureBoxes[listCounter].Name = string.Concat((object) listCounter);
      this.thumbnailsPictureBoxes[listCounter].Click += new EventHandler(this.editStartScreenButton_Click);
      this.thumbnailsPictureBoxes[listCounter].Cursor = Cursors.Hand;
      this.thumbnailsPictureBoxes[listCounter].BackColor = Color.Transparent;
      this.screenEditToolTip[listCounter] = new SmartToolTip();
      this.screenEditToolTip[listCounter].ShowAlways = true;
      this.screenEditToolTip[listCounter].SetToolTip((Control) this.thumbnailsPictureBoxes[listCounter], GlobalResource.ConfigPanel_ToolTip_EditStartScreen);
      this.thumbnailsPictureBoxes[listCounter].ResumeLayout(false);
      this.thumbnailsPictureBoxes[listCounter].PerformLayout();
      this.colorThumbnailsPictureBoxes[listCounter] = new PictureBox();
      this.colorThumbnailsPictureBoxes[listCounter].SuspendLayout();
      this.colorThumbnailsPictureBoxes[listCounter].BorderStyle = BorderStyle.FixedSingle;
      this.colorThumbnailsPictureBoxes[listCounter].Image = colorThumbnail;
      if (colorThumbnail == null)
        this.colorThumbnailsPictureBoxes[listCounter].Image = (Image) Resources.cMMI_StartAnimation_active;
      this.colorThumbnailsPictureBoxes[listCounter].SizeMode = PictureBoxSizeMode.Zoom;
      this.colorThumbnailsPictureBoxes[listCounter].Margin = this.defaultPadding;
      this.colorThumbnailsPictureBoxes[listCounter].TabStop = false;
      this.colorThumbnailsPictureBoxes[listCounter].Dock = DockStyle.Fill;
      this.colorThumbnailsPictureBoxes[listCounter].Name = string.Concat((object) listCounter);
      this.colorThumbnailsPictureBoxes[listCounter].Click += new EventHandler(this.editColorStartScreenButton_Click);
      this.colorThumbnailsPictureBoxes[listCounter].Cursor = Cursors.Hand;
      this.colorThumbnailsPictureBoxes[listCounter].BackColor = Color.Transparent;
      this.screenEditToolTip[listCounter] = new SmartToolTip();
      this.screenEditToolTip[listCounter].ShowAlways = true;
      this.screenEditToolTip[listCounter].SetToolTip((Control) this.colorThumbnailsPictureBoxes[listCounter], GlobalResource.ConfigPanel_ToolTip_EditStartScreen);
      this.colorThumbnailsPictureBoxes[listCounter].ResumeLayout(false);
      this.colorThumbnailsPictureBoxes[listCounter].PerformLayout();
      this.trashPictureBoxes[listCounter] = new PictureBox();
      this.trashPictureBoxes[listCounter].SuspendLayout();
      if (isDeletable)
      {
        this.trashPictureBoxes[listCounter].Image = (Image) Resources.trashcan;
        this.trashPictureBoxes[listCounter].Click += new EventHandler(this.deleteStartScreenButton_Click);
        this.trashPictureBoxes[listCounter].Cursor = Cursors.Hand;
        this.trashPictureBoxes[listCounter].BorderStyle = BorderStyle.FixedSingle;
        this.trashPictureBoxes[listCounter].Size = this.pictureBoxSize;
        this.trashPictureBoxes[listCounter].Margin = this.defaultPadding;
        this.trashPictureBoxes[listCounter].SizeMode = PictureBoxSizeMode.CenterImage;
      }
      else
        this.trashPictureBoxes[listCounter].Image = (Image) null;
      this.trashPictureBoxes[listCounter].TabStop = false;
      this.trashPictureBoxes[listCounter].SizeMode = PictureBoxSizeMode.CenterImage;
      this.trashPictureBoxes[listCounter].Dock = DockStyle.Fill;
      this.trashPictureBoxes[listCounter].Name = string.Concat((object) listCounter);
      this.trashPictureBoxes[listCounter].BackColor = Color.Transparent;
      this.screenDeleteToolTip[listCounter] = new SmartToolTip();
      this.screenDeleteToolTip[listCounter].ShowAlways = true;
      this.screenDeleteToolTip[listCounter].SetToolTip((Control) this.trashPictureBoxes[listCounter], GlobalResource.ConfigPanel_ToolTip_DeleteStartScreen);
      this.trashPictureBoxes[listCounter].ResumeLayout(false);
      this.trashPictureBoxes[listCounter].PerformLayout();
      this.pictureListTableLayoutPanel.Controls.Add((Control) this.thumbnailsPictureBoxes[listCounter], 0, listCounter + 1);
      this.pictureListTableLayoutPanel.Controls.Add((Control) this.colorThumbnailsPictureBoxes[listCounter], 1, listCounter + 1);
      this.pictureListTableLayoutPanel.Controls.Add((Control) this.screenDescriptions[listCounter], 2, listCounter + 1);
      this.pictureListTableLayoutPanel.Controls.Add((Control) this.trashPictureBoxes[listCounter], 3, listCounter + 1);
    }

    private void DefaultUISettings()
    {
      this.SuspendLayout();
      this.createNewParameterSetButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.createNewParameterSetButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.saveParameterButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.saveParameterButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.saveParameterAsButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.saveParameterAsButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.deleteParameterButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.deleteParameterButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.transmitParameterButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.transmitParameterButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.newStartScreenButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.newStartScreenButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.deleteStartScreenFromMMIButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.deleteStartScreenFromMMIButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.deleteStartScreenFromMMIButton.Visible = false;
      this.backButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.backButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.parameterListTabPage.Font = FontDefinition.DefaultTextFont;
      this.parameterListTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.startScreensTabPage.Font = FontDefinition.DefaultTextFont;
      this.startScreensTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.autoTransTabPage.Font = FontDefinition.DefaultTextFont;
      this.autoTransTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.autoRecTabPage.Font = FontDefinition.DefaultTextFont;
      this.autoRecTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.parameterEditTabPage.Font = FontDefinition.DefaultTextFont;
      this.parameterEditTabPage.ForeColor = ColorDefinition.BlackTextColor;
      this.parameterListDateLabel.Font = FontDefinition.BoldCaptionTextFont;
      this.parameterListDateLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.parameterListNameLabel.Font = FontDefinition.BoldCaptionTextFont;
      this.parameterListNameLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.parameterListQuickConfigLabel.Font = FontDefinition.BoldCaptionTextFont;
      this.parameterListQuickConfigLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.pictureTableCaptionLabel.Font = FontDefinition.BoldCaptionTextFont;
      this.pictureTableCaptionLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.autoTransCaptionLabel.Font = FontDefinition.BoldCaptionTextFont;
      this.autoTransCaptionLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.actMMIDataLabel.Font = FontDefinition.TableCaptionTextFont;
      this.actMMIDataLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.parameterNameTextBox.Font = FontDefinition.DefaultTextFont;
      this.parameterNameTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.parameterDescriptionRichTextBox.Font = FontDefinition.DefaultTextFont;
      this.parameterDescriptionRichTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.autoCheckBox.Font = FontDefinition.DefaultTextFont;
      this.autoCheckBox.ForeColor = ColorDefinition.BlackTextColor;
      this.receiveLabel.Font = FontDefinition.TableCaptionTextFont;
      this.receiveLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.massTabActualLabel.Font = FontDefinition.TableCaptionTextFont;
      this.massTabActualLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.blueLinePictureBox.BackColor = Color.Transparent;
      this.blueLinePictureBox.Image = (Image) BackgroundImages.LightGrayGradient;
      this.blueLinePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.blueLinePictureBox2.BackColor = Color.Transparent;
      this.blueLinePictureBox2.Image = (Image) BackgroundImages.LightGrayGradient;
      this.blueLinePictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
      this.profileComboBox.BackColor = ColorDefinition.ThomasSpecialBlueColor;
      this.profileComboBox.DropDownStyle = ComboBoxStyle.DropDown;
      this.profileSelectionLabel.Font = FontDefinition.DefaultBoldTextFont;
      this.configTabControl.Font = FontDefinition.MenubarFont;
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void MouseWheelRedirector(bool attach)
    {
      if (attach)
      {
        UIElements.MouseWheelRedirector.Attach((Control) this.createNewParameterSetButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterTableLayoutPanel);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterListPanel);
        UIElements.MouseWheelRedirector.Attach((Control) this.saveParameterButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.saveParameterAsButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.deleteParameterButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.transmitParameterButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.newStartScreenButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.deleteStartScreenFromMMIButton);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterListTabPage);
        UIElements.MouseWheelRedirector.Attach((Control) this.startScreensTabPage);
        UIElements.MouseWheelRedirector.Attach((Control) this.autoTransTabPage);
        UIElements.MouseWheelRedirector.Attach((Control) this.pictureListTableLayoutPanel);
        UIElements.MouseWheelRedirector.Attach((Control) this.autoRecTabPage);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterEditTabPage);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterListDateLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterListNameLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterListQuickConfigLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.actMMIDataLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterNameTextBox);
        UIElements.MouseWheelRedirector.Attach((Control) this.parameterDescriptionRichTextBox);
        UIElements.MouseWheelRedirector.Attach((Control) this.pictureTableCaptionLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.autoCheckBox);
        UIElements.MouseWheelRedirector.Attach((Control) this.receiveLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.massTabActualLabel);
        UIElements.MouseWheelRedirector.Attach((Control) this.blueLinePictureBox);
      }
      else
      {
        UIElements.MouseWheelRedirector.Detach((Control) this.createNewParameterSetButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterTableLayoutPanel);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterListPanel);
        UIElements.MouseWheelRedirector.Detach((Control) this.saveParameterButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.saveParameterAsButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.deleteParameterButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.transmitParameterButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.newStartScreenButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.deleteStartScreenFromMMIButton);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterListTabPage);
        UIElements.MouseWheelRedirector.Detach((Control) this.startScreensTabPage);
        UIElements.MouseWheelRedirector.Detach((Control) this.autoTransTabPage);
        UIElements.MouseWheelRedirector.Detach((Control) this.pictureListTableLayoutPanel);
        UIElements.MouseWheelRedirector.Detach((Control) this.autoRecTabPage);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterEditTabPage);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterListDateLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterListNameLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterListQuickConfigLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.actMMIDataLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterNameTextBox);
        UIElements.MouseWheelRedirector.Detach((Control) this.parameterDescriptionRichTextBox);
        UIElements.MouseWheelRedirector.Detach((Control) this.pictureTableCaptionLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.autoCheckBox);
        UIElements.MouseWheelRedirector.Detach((Control) this.receiveLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.massTabActualLabel);
        UIElements.MouseWheelRedirector.Detach((Control) this.blueLinePictureBox);
      }
    }

    private void ConfigurationPanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.Parent == null || !(this.Size != this.Parent.Size))
        return;
      this.Size = this.Parent.Size;
    }

    public bool ReadProfileFile(string filename, bool isDefault)
    {
      ProfileSettingsFile profileSettingsFile = !isDefault ? ProfileSettingsFile.ReadFile(Directories.Instance.UserDataPath + filename) : ProfileSettingsFile.ReadFile(Directories.Instance.DefaultsPath + filename);
      if (profileSettingsFile.NumberOfProfileValues == 0)
      {
        int num = (int) MessageBox.Show(GlobalResource.SettingFile_NotFound_Message, GlobalResource.SettingFile_NotFound_Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      this.profilePanel.ReadableProfileValues = profileSettingsFile.ProfileValues;
      this.profilePanel.updateUIWithDictionaryValues();
      return true;
    }

    public bool ReadParameterFile(string filename, bool isDefault, bool changeTab = true)
    {
      ParameterSettingsFile parameterSettingsFile = !isDefault ? ParameterSettingsFile.ReadFile(Directories.Instance.UserDataPath + filename) : ParameterSettingsFile.ReadFile(Directories.Instance.DefaultsPath + filename);
      if (parameterSettingsFile.ParameterId.Length == 0)
      {
        int num = (int) MessageBox.Show(GlobalResource.SettingFile_NotFound_Message, GlobalResource.SettingFile_NotFound_Caption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return false;
      }
      foreach (ParameterIds parameterIds in Enum.GetValues(typeof (ParameterIds)))
      {
        int index = (int) (parameterIds - (byte) 1);
        try
        {
          // ISSUE: reference to a compiler-generated method
          Parameters.Instance.SetSetOfParameters((ParameterIds) parameterSettingsFile.ParameterId[index], parameterSettingsFile.ParameterContent[index]);
        }
        catch (IndexOutOfRangeException ex)
        {
          // ISSUE: reference to a compiler-generated method
          Parameters.Instance.SetSetOfParametersToDefault((ParameterIds) (index + 1));
        }
        catch (ParameterValueException ex)
        {
          // ISSUE: reference to a compiler-generated method
          ParameterValue parameterValueObject = Parameters.Instance.GetParameterValueObject(ex.ParameterId, ex.ParameterPosition);
          parameterValueObject.ReadableValue = parameterValueObject.ReadableDefaultValue;
          // ISSUE: reference to a compiler-generated method
          Parameters.Instance.SetParameterValueObject(ex.ParameterId, ex.ParameterPosition, parameterValueObject);
        }
      }
      if (changeTab)
        this.configTabControl.SelectedTab = this.parameterEditTabPage;
      this.parameterPanel.updateUIWithDictionaryValues();
      this.parameterDescriptionRichTextBox.Text = parameterSettingsFile.Description;
      this.parameterNameTextBox.Text = parameterSettingsFile.Label;
      return true;
    }

    public int WriteParameterFile(int position)
    {
      DateTime now = DateTime.Now;
      string changeDate = now.ToString("d", (IFormatProvider) Thread.CurrentThread.CurrentUICulture);
      string empty = string.Empty;
      string filename;
      try
      {
        filename = this.parameterList.ContentFile(position);
      }
      catch (IndexOutOfRangeException ex)
      {
        filename = "Parameter_" + now.ToString("yyyyMMddHHmmssffff") + ".xml";
      }
      ParameterSettingsFile content = new ParameterSettingsFile(this.parameterNameTextBox.Text, this.parameterDescriptionRichTextBox.Text);
      this.parameterPanel.updateDictionaryWithUIValues();
      int length = Parameters.Instance.ParameterElements - 1;
      content.ParameterContent = new byte[length][];
      content.ParameterId = new int[length];
      foreach (ParameterIds id in Enum.GetValues(typeof (ParameterIds)))
      {
        int index = (int) (id - (byte) 1);
        // ISSUE: reference to a compiler-generated method
        content.ParameterContent[index] = Parameters.Instance.GetSetOfParameters(id);
        content.ParameterId[index] = (int) id;
      }
      try
      {
        ParameterSettingsFile.WriteFile(content, Directories.Instance.UserDataPath + filename);
      }
      catch (FileNotFoundException ex)
      {
        UniqueError.Message(UniqueError.Number.PARAMETERSETTINGS_WRITE_ERROR);
        return -1;
      }
      if (position >= 0)
        this.parameterList.UpdateListElement(position, this.parameterList.IsFastConfig(position), this.parameterNameTextBox.Text, this.parameterDescriptionRichTextBox.Text, changeDate, filename, this.parameterList.IsDeletable(position), this.parameterList.StartScreenFileName(position));
      else
        this.parameterList.AddListElement(false, this.parameterNameTextBox.Text, this.parameterDescriptionRichTextBox.Text, changeDate, filename);
      this.parameterList.SortListElements();
      try
      {
        ListSettingFile.WriteFile(this.parameterList, Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.PARAMETERLISTFILE_UPDATE_WRITE_ERROR);
        return -1;
      }
      int num = 0;
      for (int position1 = 0; position1 < this.parameterList.Length; ++position1)
      {
        if (filename == this.parameterList.ContentFile(position1))
          num = position1;
      }
      this.FillParametersTablePanel();
      this.parameterComboBoxAddActMMI();
      this.parameterPanel.ValueWasChanged = false;
      return num;
    }

    private void InitFastTransElements()
    {
      this.fastParameterTransmissionPictuerBox = new PictureBox[0];
      this.fastParameterFastTransmissionPictuerBox = new PictureBox[0];
      this.fastParameterStartScreenPictuerBox = new PictureBox[0];
      this.fastParameterColorStartScreenPictuerBox = new PictureBox[0];
      this.fastParameterTransmissionOkPictuerBox = new PictureBox[0];
      this.fastParameterNameLabel = new Label[0];
      this.fastParameterDescriptionToolTip = new SmartToolTip[0];
      this.fastParameterStartScreenToolTip = new SmartToolTip[0];
      this.fastParameterFastTransmissionToolTip = new SmartToolTip[0];
      this.fastParameterTransmissionToolTip = new SmartToolTip[0];
      this.screens = new List<ScreenElement>((IEnumerable<ScreenElement>) this.defaultStartScreenList.Screens);
      this.screens.AddRange((IEnumerable<ScreenElement>) this.startScreensList.Screens);
      this.UIMassDefaultSettings();
      this.FillFastParametersTablePanel();
    }

    private void UIMassDefaultSettings()
    {
      this.massTabActualLabel.SuspendLayout();
      this.massTabActualLabel.Font = FontDefinition.TableCaptionTextFont;
      this.massTabActualLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.massTabActualLabel.BorderStyle = BorderStyle.FixedSingle;
      this.massTabActualLabel.TextAlign = ContentAlignment.MiddleLeft;
      this.massTabActualLabel.AutoEllipsis = true;
      this.massTabActualLabel.BackColor = Color.Transparent;
      this.massTabActualLabel.Margin = this.defaultPadding;
      this.massTabActualLabel.ResumeLayout(false);
      this.massTabActualLabel.PerformLayout();
      this.startScreenMassPictureBox.SuspendLayout();
      this.startScreenMassPictureBox.Size = this.largePictureBoxSize;
      this.startScreenMassPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.startScreenMassPictureBox.TabStop = false;
      this.startScreenMassPictureBox.BackColor = Color.Transparent;
      this.startScreenMassPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.startScreenMassPictureBox.Margin = this.defaultPadding;
      this.startScreenMassPictureBox.ResumeLayout(false);
      this.startScreenMassPictureBox.PerformLayout();
      this.colorStartScreenMassPictureBox.SuspendLayout();
      this.colorStartScreenMassPictureBox.Size = this.largePictureBoxSize;
      this.colorStartScreenMassPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.colorStartScreenMassPictureBox.TabStop = false;
      this.colorStartScreenMassPictureBox.BackColor = Color.Transparent;
      this.colorStartScreenMassPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.colorStartScreenMassPictureBox.Margin = this.defaultPadding;
      this.colorStartScreenMassPictureBox.ResumeLayout(false);
      this.colorStartScreenMassPictureBox.PerformLayout();
      this.speakerPictureBox.SuspendLayout();
      this.speakerPictureBox.TabStop = false;
      this.speakerPictureBox.BackColor = Color.Transparent;
      this.speakerPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.speakerPictureBox.Size = this.largePictureBoxSize;
      this.speakerPictureBox.Margin = this.defaultPadding;
      this.speakerPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.speakerPictureBox.ResumeLayout(false);
      this.speakerPictureBox.PerformLayout();
      this.receiveSpeakerPictureBox.SuspendLayout();
      this.receiveSpeakerPictureBox.TabStop = false;
      this.receiveSpeakerPictureBox.BackColor = Color.Transparent;
      this.receiveSpeakerPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.receiveSpeakerPictureBox.Size = this.largePictureBoxSize;
      this.receiveSpeakerPictureBox.Margin = this.defaultPadding;
      this.receiveSpeakerPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.receiveSpeakerPictureBox.ResumeLayout(false);
      this.receiveSpeakerPictureBox.PerformLayout();
      this.receiveTabIconPictureBox.SuspendLayout();
      this.receiveTabIconPictureBox.TabStop = false;
      this.receiveTabIconPictureBox.BackColor = Color.Transparent;
      this.receiveTabIconPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.receiveTabIconPictureBox.Size = this.largePictureBoxSize;
      this.receiveTabIconPictureBox.Margin = this.defaultPadding;
      this.receiveTabIconPictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
      this.receiveTabIconPictureBox.ResumeLayout(false);
      this.receiveTabIconPictureBox.PerformLayout();
    }

    private void FillFastParametersTablePanel()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.autoTransTableLayout.SuspendLayout();
      this.defaultParameterList = ListSettingFile.ReadFile(Directories.Instance.DefaultParametersListFileName);
      this.parameterList = ListSettingFile.ReadFile(Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
      for (int index = 0; index < this.fastParameterFastTransmissionPictuerBox.Length; ++index)
      {
        this.fastParameterFastTransmissionPictuerBox[index].Dispose();
        this.fastParameterTransmissionPictuerBox[index].Dispose();
        this.fastParameterStartScreenPictuerBox[index].Dispose();
        this.fastParameterColorStartScreenPictuerBox[index].Dispose();
        this.fastParameterTransmissionOkPictuerBox[index].Dispose();
        this.fastParameterNameLabel[index].Dispose();
        this.fastParameterDescriptionToolTip[index].Dispose();
        this.fastParameterStartScreenToolTip[index].Dispose();
        this.fastParameterFastTransmissionToolTip[index].Dispose();
        this.fastParameterTransmissionToolTip[index].Dispose();
        this.autoTransTableLayout.RowStyles.Clear();
      }
      this.autoTransTableLayout.RowCount = 1;
      this.autoTransTableLayout.AutoSize = true;
      int length = this.defaultParameterList.NumberOfFastConfigElements + this.parameterList.NumberOfFastConfigElements;
      this.fastParameterTransmissionPictuerBox = new PictureBox[length];
      this.fastParameterFastTransmissionPictuerBox = new PictureBox[length];
      this.fastParameterStartScreenPictuerBox = new PictureBox[length];
      this.fastParameterColorStartScreenPictuerBox = new PictureBox[length];
      this.fastParameterTransmissionOkPictuerBox = new PictureBox[length];
      this.fastParameterNameLabel = new Label[length];
      this.fastParameterDescriptionToolTip = new SmartToolTip[length];
      this.fastParameterStartScreenToolTip = new SmartToolTip[length];
      this.fastParameterFastTransmissionToolTip = new SmartToolTip[length];
      this.fastParameterTransmissionToolTip = new SmartToolTip[length];
      this.autoTransTableLayout.RowCount = 1 + length;
      int listElement = 0;
      for (int index = 0; index < this.defaultParameterList.Length; ++index)
      {
        if (this.defaultParameterList.IsFastConfig(index))
        {
          this.CreateElementsForParameterListe(index, this.defaultList, listElement, this.defaultParameterList.Name(index), this.defaultParameterList.Description(index), this.defaultParameterList.StartScreenFileName(index));
          ++listElement;
        }
      }
      for (int index = 0; index < this.parameterList.Length; ++index)
      {
        if (this.parameterList.IsFastConfig(index))
        {
          this.CreateElementsForParameterListe(index, this.usersList, listElement, this.parameterList.Name(index), this.parameterList.Description(index), this.parameterList.StartScreenFileName(index));
          ++listElement;
        }
      }
      if (this.autoTransTableLayout.RowCount == 1)
        this.autoTransCaptionLabel.Text = GlobalResource.ConfigurationPanel_MassTrans_NoElement;
      else
        this.autoTransCaptionLabel.Text = new ComponentResourceManager(typeof (ConfigurationPanel)).GetString("autoTransCaptionLabel.Text");
      if (this.autoTransTableLayout.AutoSize)
        this.autoTransTableLayout.Size = this.autoTransTableLayout.GetPreferredSize(this.autoTransTableLayout.Size);
      this.autoTransTableLayout.ResumeLayout(false);
      this.autoTransTableLayout.PerformLayout();
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void CreateElementsForParameterListe(
      int listCounter,
      string listType,
      int listElement,
      string label,
      string description,
      string startScreen)
    {
      this.autoTransTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45f));
      this.fastParameterFastTransmissionPictuerBox[listElement] = new PictureBox();
      this.fastParameterFastTransmissionPictuerBox[listElement].SuspendLayout();
      this.fastParameterFastTransmissionPictuerBox[listElement].Cursor = Cursors.Hand;
      this.fastParameterFastTransmissionPictuerBox[listElement].Name = listType + (object) listElement;
      this.fastParameterFastTransmissionPictuerBox[listElement].TabStop = false;
      this.fastParameterFastTransmissionPictuerBox[listElement].Dock = DockStyle.Fill;
      this.fastParameterFastTransmissionPictuerBox[listElement].Click += new EventHandler(this.parameterFastTransmissionPictuerBox_Click);
      this.fastParameterFastTransmissionPictuerBox[listElement].Image = (Image) Resources.Icon_mTransceive_Active;
      this.fastParameterFastTransmissionPictuerBox[listElement].BackColor = Color.Transparent;
      this.fastParameterFastTransmissionPictuerBox[listElement].BorderStyle = BorderStyle.FixedSingle;
      this.fastParameterFastTransmissionPictuerBox[listElement].Size = this.largePictureBoxSize;
      this.fastParameterFastTransmissionPictuerBox[listElement].Margin = this.defaultPadding;
      this.fastParameterFastTransmissionPictuerBox[listElement].SizeMode = PictureBoxSizeMode.CenterImage;
      this.fastParameterFastTransmissionToolTip[listElement] = new SmartToolTip();
      this.fastParameterFastTransmissionToolTip[listElement].ShowAlways = true;
      this.fastParameterFastTransmissionToolTip[listElement].SetToolTip((Control) this.fastParameterFastTransmissionPictuerBox[listElement], GlobalResource.MassConfigPanel_ToolTip_Parameter_FastTransmission);
      this.fastParameterFastTransmissionPictuerBox[listElement].ResumeLayout(false);
      this.fastParameterFastTransmissionPictuerBox[listElement].PerformLayout();
      this.fastParameterTransmissionPictuerBox[listElement] = new PictureBox();
      this.fastParameterTransmissionPictuerBox[listElement].SuspendLayout();
      this.fastParameterTransmissionPictuerBox[listElement].Cursor = Cursors.Hand;
      this.fastParameterTransmissionPictuerBox[listElement].Name = listType + (object) listElement;
      this.fastParameterTransmissionPictuerBox[listElement].TabStop = false;
      this.fastParameterTransmissionPictuerBox[listElement].Dock = DockStyle.Fill;
      this.fastParameterTransmissionPictuerBox[listElement].Click += new EventHandler(this.parameterTransmissionPictuerBox_Click);
      this.fastParameterTransmissionPictuerBox[listElement].Image = (Image) Resources.Icon_sTransceive_Active;
      this.fastParameterTransmissionPictuerBox[listElement].BackColor = Color.Transparent;
      this.fastParameterTransmissionPictuerBox[listElement].BorderStyle = BorderStyle.FixedSingle;
      this.fastParameterTransmissionPictuerBox[listElement].Size = this.largePictureBoxSize;
      this.fastParameterTransmissionPictuerBox[listElement].Margin = this.defaultPadding;
      this.fastParameterTransmissionPictuerBox[listElement].SizeMode = PictureBoxSizeMode.CenterImage;
      this.fastParameterTransmissionToolTip[listElement] = new SmartToolTip();
      this.fastParameterTransmissionToolTip[listElement].ShowAlways = true;
      this.fastParameterTransmissionToolTip[listElement].SetToolTip((Control) this.fastParameterTransmissionPictuerBox[listElement], GlobalResource.MassConfigPanel_ToolTip_Parameter_Transmission);
      this.fastParameterTransmissionPictuerBox[listElement].ResumeLayout(false);
      this.fastParameterTransmissionPictuerBox[listElement].PerformLayout();
      this.fastParameterStartScreenPictuerBox[listElement] = new PictureBox();
      this.fastParameterStartScreenPictuerBox[listElement].SuspendLayout();
      this.fastParameterStartScreenPictuerBox[listElement].Cursor = Cursors.Hand;
      this.fastParameterStartScreenPictuerBox[listElement].Name = listType + (object) listCounter;
      this.fastParameterStartScreenPictuerBox[listElement].Tag = (object) new StartScreenPictureBoxTags(listElement, listType, listCounter);
      this.fastParameterStartScreenPictuerBox[listElement].Size = this.largePictureBoxSize;
      this.fastParameterStartScreenPictuerBox[listElement].SizeMode = PictureBoxSizeMode.CenterImage;
      this.fastParameterStartScreenPictuerBox[listElement].Dock = DockStyle.Fill;
      this.fastParameterStartScreenPictuerBox[listElement].TabStop = false;
      this.fastParameterStartScreenPictuerBox[listElement].Click += new EventHandler(this.startScreenParameterPictureBox_Click);
      this.fastParameterStartScreenPictuerBox[listElement].BackColor = Color.Transparent;
      this.fastParameterStartScreenPictuerBox[listElement].BorderStyle = BorderStyle.FixedSingle;
      this.fastParameterStartScreenPictuerBox[listElement].Margin = this.defaultPadding;
      this.fastParameterStartScreenPictuerBox[listElement].Image = this.screen_empty_active;
      if (string.IsNullOrEmpty(startScreen))
      {
        this.fastParameterStartScreenPictuerBox[listElement].Image = HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true);
      }
      else
      {
        int num1 = this.startScreensList.StartScreenListElement(startScreen);
        int num2 = this.defaultStartScreenList.StartScreenListElement(startScreen);
        this.fastParameterStartScreenPictuerBox[listElement].Image = num1 < 0 ? (num2 < 0 ? HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true) : (this.defaultStartScreenList.ThumbnailImage(this.defaultStartScreenList.StartScreenListElement(startScreen)) != null ? HelperClass.ResizeImage(this.defaultStartScreenList.ThumbnailImage(this.defaultStartScreenList.StartScreenListElement(startScreen)), this.startScreenPreviewSize, true) : HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true))) : (this.startScreensList.ThumbnailImage(this.startScreensList.StartScreenListElement(startScreen)) != null ? HelperClass.ResizeImage(this.startScreensList.ThumbnailImage(this.startScreensList.StartScreenListElement(startScreen)), this.startScreenPreviewSize, true) : HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true));
      }
      this.fastParameterStartScreenToolTip[listElement] = new SmartToolTip();
      this.fastParameterStartScreenToolTip[listElement].ShowAlways = true;
      this.fastParameterStartScreenToolTip[listElement].SetToolTip((Control) this.fastParameterStartScreenPictuerBox[listElement], GlobalResource.MassConfigPanel_ToolTip_Parameter_StartScreen);
      this.fastParameterStartScreenPictuerBox[listElement].ResumeLayout(false);
      this.fastParameterStartScreenPictuerBox[listElement].PerformLayout();
      this.fastParameterColorStartScreenPictuerBox[listElement] = new PictureBox();
      this.fastParameterColorStartScreenPictuerBox[listElement].SuspendLayout();
      this.fastParameterColorStartScreenPictuerBox[listElement].Cursor = Cursors.Hand;
      this.fastParameterColorStartScreenPictuerBox[listElement].Name = listType + (object) listCounter;
      this.fastParameterColorStartScreenPictuerBox[listElement].Tag = (object) new StartScreenPictureBoxTags(listElement, listType, listCounter);
      this.fastParameterColorStartScreenPictuerBox[listElement].Size = this.largePictureBoxSize;
      this.fastParameterColorStartScreenPictuerBox[listElement].SizeMode = PictureBoxSizeMode.CenterImage;
      this.fastParameterColorStartScreenPictuerBox[listElement].Dock = DockStyle.Fill;
      this.fastParameterColorStartScreenPictuerBox[listElement].TabStop = false;
      this.fastParameterColorStartScreenPictuerBox[listElement].Click += new EventHandler(this.startScreenParameterPictureBox_Click);
      this.fastParameterColorStartScreenPictuerBox[listElement].BackColor = Color.Transparent;
      this.fastParameterColorStartScreenPictuerBox[listElement].BorderStyle = BorderStyle.FixedSingle;
      this.fastParameterColorStartScreenPictuerBox[listElement].Margin = this.defaultPadding;
      this.fastParameterColorStartScreenPictuerBox[listElement].Image = this.screen_empty_active;
      if (string.IsNullOrEmpty(startScreen))
      {
        this.fastParameterColorStartScreenPictuerBox[listElement].Image = HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true);
      }
      else
      {
        int num1 = this.startScreensList.StartScreenListElement(startScreen);
        int num2 = this.defaultStartScreenList.StartScreenListElement(startScreen);
        this.fastParameterColorStartScreenPictuerBox[listElement].Image = num1 < 0 ? (num2 < 0 ? HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true) : (this.defaultStartScreenList.ColorThumbnailImage(this.defaultStartScreenList.StartScreenListElement(startScreen)) != null ? HelperClass.ResizeImage(this.defaultStartScreenList.ColorThumbnailImage(this.defaultStartScreenList.StartScreenListElement(startScreen)), this.startScreenPreviewSize, true) : HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true))) : (this.startScreensList.ColorThumbnailImage(this.startScreensList.StartScreenListElement(startScreen)) != null ? HelperClass.ResizeImage(this.startScreensList.ColorThumbnailImage(this.startScreensList.StartScreenListElement(startScreen)), this.startScreenPreviewSize, true) : HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true));
      }
      this.fastParameterStartScreenToolTip[listElement] = new SmartToolTip();
      this.fastParameterStartScreenToolTip[listElement].ShowAlways = true;
      this.fastParameterStartScreenToolTip[listElement].SetToolTip((Control) this.fastParameterColorStartScreenPictuerBox[listElement], GlobalResource.MassConfigPanel_ToolTip_Parameter_StartScreen);
      this.fastParameterColorStartScreenPictuerBox[listElement].ResumeLayout(false);
      this.fastParameterColorStartScreenPictuerBox[listElement].PerformLayout();
      this.fastParameterTransmissionOkPictuerBox[listElement] = new PictureBox();
      this.fastParameterTransmissionOkPictuerBox[listElement].SuspendLayout();
      this.fastParameterTransmissionOkPictuerBox[listElement].Cursor = Cursors.Default;
      this.fastParameterTransmissionOkPictuerBox[listElement].Name = listType + (object) listCounter;
      this.fastParameterTransmissionOkPictuerBox[listElement].TabStop = false;
      this.fastParameterTransmissionOkPictuerBox[listElement].Size = this.largePictureBoxSize;
      this.fastParameterTransmissionOkPictuerBox[listElement].SizeMode = PictureBoxSizeMode.CenterImage;
      this.fastParameterTransmissionOkPictuerBox[listElement].Dock = DockStyle.Fill;
      this.fastParameterTransmissionOkPictuerBox[listElement].Image = (Image) new Bitmap(1, 1);
      this.fastParameterTransmissionOkPictuerBox[listElement].BackColor = Color.Transparent;
      this.fastParameterTransmissionOkPictuerBox[listElement].ResumeLayout(false);
      this.fastParameterTransmissionOkPictuerBox[listElement].PerformLayout();
      this.fastParameterNameLabel[listElement] = new Label();
      this.fastParameterNameLabel[listElement].SuspendLayout();
      this.fastParameterNameLabel[listElement].Font = FontDefinition.TableCaptionTextFont;
      this.fastParameterNameLabel[listElement].ForeColor = ColorDefinition.BlackTextColor;
      this.fastParameterNameLabel[listElement].BorderStyle = BorderStyle.FixedSingle;
      this.fastParameterNameLabel[listElement].Text = label;
      this.fastParameterNameLabel[listElement].TextAlign = ContentAlignment.MiddleLeft;
      this.fastParameterNameLabel[listElement].Dock = DockStyle.Fill;
      this.fastParameterNameLabel[listElement].AutoEllipsis = true;
      this.fastParameterNameLabel[listElement].Name = listType + (object) listCounter;
      this.fastParameterNameLabel[listElement].BackColor = Color.Transparent;
      this.fastParameterNameLabel[listElement].Margin = this.defaultPadding;
      this.fastParameterDescriptionToolTip[listElement] = new SmartToolTip();
      this.fastParameterDescriptionToolTip[listElement].ToolTipIcon = ToolTipIcon.Info;
      this.fastParameterDescriptionToolTip[listElement].ShowAlways = true;
      this.fastParameterDescriptionToolTip[listElement].SetToolTip((Control) this.fastParameterNameLabel[listElement], description);
      this.fastParameterNameLabel[listElement].ResumeLayout(false);
      this.fastParameterNameLabel[listElement].PerformLayout();
      this.autoTransTableLayout.Controls.Add((Control) this.fastParameterFastTransmissionPictuerBox[listElement], 0, listElement + 1);
      this.autoTransTableLayout.Controls.Add((Control) this.fastParameterTransmissionPictuerBox[listElement], 1, listElement + 1);
      this.autoTransTableLayout.Controls.Add((Control) this.fastParameterStartScreenPictuerBox[listElement], 2, listElement + 1);
      this.autoTransTableLayout.Controls.Add((Control) this.fastParameterColorStartScreenPictuerBox[listElement], 3, listElement + 1);
      this.autoTransTableLayout.Controls.Add((Control) this.fastParameterNameLabel[listElement], 4, listElement + 1);
      this.autoTransTableLayout.Controls.Add((Control) this.fastParameterTransmissionOkPictuerBox[listElement], 5, listElement + 1);
    }

    private void parameterFastTransmissionPictuerBox_Click(object sender, EventArgs e)
    {
      string name = ((Control) sender).Name;
      ListSettingFile listSettingFile;
      int int32_1;
      int int32_2;
      bool isDefault;
      if (name.StartsWith(this.defaultList))
      {
        listSettingFile = this.defaultParameterList;
        int32_1 = Convert.ToInt32(name.Substring(this.defaultList.Length));
        int32_2 = Convert.ToInt32(this.fastParameterNameLabel[int32_1].Name.Substring(this.defaultList.Length));
        isDefault = true;
      }
      else
      {
        listSettingFile = this.parameterList;
        int32_1 = Convert.ToInt32(name.Substring(this.usersList.Length));
        int32_2 = Convert.ToInt32(this.fastParameterNameLabel[int32_1].Name.Substring(this.usersList.Length));
        isDefault = false;
      }
      foreach (PictureBox pictureBox in this.fastParameterTransmissionOkPictuerBox)
        pictureBox.Image = (Image) null;
      if (!this.ReadParameterFile(listSettingFile.ContentFile(int32_2), isDefault, false))
        return;
      this.transOnlyPanel.Visible = true;
      this.massTabActualLabel.Text = this.fastParameterNameLabel[int32_1].Text;
      this.startScreenMassPictureBox.Image = this.fastParameterStartScreenPictuerBox[int32_1].Image;
      this.startScreenMassPictureBox.Visible = true;
      this.colorStartScreenMassPictureBox.Image = this.fastParameterColorStartScreenPictuerBox[int32_1].Image;
      this.colorStartScreenMassPictureBox.Visible = true;
      string startScreen = listSettingFile.StartScreenFileName(int32_2);
      if (string.IsNullOrEmpty(startScreen))
      {
        this.startScreenMassPictureBox.Name = string.Concat((object) 0);
        this.colorStartScreenMassPictureBox.Name = string.Concat((object) 0);
      }
      else if (this.defaultStartScreenList.StartScreenListElement(startScreen) >= 0)
      {
        this.startScreenMassPictureBox.Name = string.Concat((object) (this.defaultStartScreenList.StartScreenListElement(startScreen) + 1));
        this.colorStartScreenMassPictureBox.Name = string.Concat((object) (this.defaultStartScreenList.StartScreenListElement(startScreen) + 1));
      }
      else if (this.startScreensList.StartScreenListElement(startScreen) >= 0)
      {
        this.startScreenMassPictureBox.Name = string.Concat((object) (this.startScreensList.StartScreenListElement(startScreen) + 1 + this.defaultStartScreenList.Length));
        this.colorStartScreenMassPictureBox.Name = string.Concat((object) (this.startScreensList.StartScreenListElement(startScreen) + 1 + this.defaultStartScreenList.Length));
      }
      else
      {
        this.startScreenMassPictureBox.Name = string.Concat((object) 0);
        this.colorStartScreenMassPictureBox.Name = string.Concat((object) 0);
      }
    }

    private bool CheckIfValidValuesInParameter(int positionOfParameter, bool isDefault)
    {
      if (isDefault)
        this.ReadParameterFile(this.defaultParameterList.ContentFile(positionOfParameter), isDefault, false);
      else
        this.ReadParameterFile(this.parameterList.ContentFile(positionOfParameter), isDefault, false);
      return this.parameterPanel.ValuesValidForConnectedMMI();
    }

    private void parameterTransmissionPictuerBox_Click(object sender, EventArgs e)
    {
      string name = ((Control) sender).Name;
      ListSettingFile listSettingFile;
      int int32_1;
      int int32_2;
      bool isDefault;
      if (name.StartsWith(this.defaultList))
      {
        listSettingFile = this.defaultParameterList;
        int32_1 = Convert.ToInt32(name.Substring(this.defaultList.Length));
        int32_2 = Convert.ToInt32(this.fastParameterNameLabel[int32_1].Name.Substring(this.defaultList.Length));
        isDefault = true;
      }
      else
      {
        listSettingFile = this.parameterList;
        int32_1 = Convert.ToInt32(name.Substring(this.usersList.Length));
        int32_2 = Convert.ToInt32(this.fastParameterNameLabel[int32_1].Name.Substring(this.usersList.Length));
        isDefault = false;
      }
      foreach (PictureBox pictureBox in this.fastParameterTransmissionOkPictuerBox)
        pictureBox.Image = (Image) null;
      if (!this.CheckIfValidValuesInParameter(int32_2, isDefault))
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      int actualPosition = 0;
      int maximum = (Parameters.Instance.ParameterElements - 1) * 3;
      if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
      {
        if (!string.IsNullOrEmpty(listSettingFile.ColorStartScreenFileName(int32_2)))
          maximum += Pictures.Instance.NumberOfPictures * 320 * 240 * 2 / 1000;
      }
      else if (!string.IsNullOrEmpty(listSettingFile.StartScreenFileName(int32_2)))
        maximum += Pictures.Instance.NumberOfPictures * 320 * 240 / 1000;
      bool flag = this.TransferParameterDataToMMI(disconnectWarning.ProgressBar(), int32_2, ref actualPosition, maximum, isDefault);
      if (flag)
        flag = this.transceiver.ReceiveAllSettingsFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
      {
        if (flag && !string.IsNullOrEmpty(listSettingFile.ColorStartScreenFileName(int32_2)))
        {
          string empty = string.Empty;
          flag = this.transceiver.TransferStartScreenToMMI(StartScreenImageFile.ReadFile(this.defaultStartScreenList.StartScreenListElement(listSettingFile.ColorStartScreenFileName(int32_2)) < 0 ? Directories.Instance.UserDataPath + listSettingFile.ColorStartScreenFileName(int32_2) : Directories.Instance.DefaultsPath + listSettingFile.ColorStartScreenFileName(int32_2)).Image, ref actualPosition, maximum, disconnectWarning.ProgressBar());
        }
        else if (flag && string.IsNullOrEmpty(listSettingFile.ColorStartScreenFileName(int32_2)))
          flag = this.transceiver.DeleteStartScreenFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      }
      else if (flag && !string.IsNullOrEmpty(listSettingFile.StartScreenFileName(int32_2)))
      {
        string empty = string.Empty;
        flag = this.transceiver.TransferStartScreenToMMI(StartScreenImageFile.ReadFile(this.defaultStartScreenList.StartScreenListElement(listSettingFile.StartScreenFileName(int32_2)) < 0 ? Directories.Instance.UserDataPath + listSettingFile.StartScreenFileName(int32_2) : Directories.Instance.DefaultsPath + listSettingFile.StartScreenFileName(int32_2)).Image, ref actualPosition, maximum, disconnectWarning.ProgressBar());
      }
      disconnectWarning.ProgressBar().Value = 100;
      disconnectWarning.ProgressBar().Refresh();
      disconnectWarning.Dispose();
      if (flag)
      {
        this.LogProductionInformation();
        this.fastParameterTransmissionOkPictuerBox[int32_1].Image = (Image) Resources.button_ok;
        MMIData.Instance.SetParameters(Parameters.Instance.Dictionary);
        MMIData.Instance.MMISerialNumber = Parameters.Instance.MMISerialNumber;
      }
      else
        this.fastParameterTransmissionOkPictuerBox[int32_1].Image = (Image) Resources.buttons_error;
      FinishSound.Instance.Play();
      MainWindow.Instance.Cursor = Cursors.Default;
      if (flag)
        return;
      int num = (int) MessageBox.Show(GlobalResource.MassConfigPanel_MassTransmit_Failed, GlobalResource.MassConfigPanel_MassTransmit_Failed_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void startScreenParameterPictureBox_Click(object sender, EventArgs e)
    {
      StartScreenPictureBoxTags tag = (StartScreenPictureBoxTags) ((Control) sender).Tag;
      ListSettingFile content;
      int startScreenElement;
      bool flag;
      if (tag.ListType.Equals(this.defaultList))
      {
        content = this.defaultParameterList;
        startScreenElement = tag.StartScreenElement;
        flag = true;
      }
      else
      {
        content = this.parameterList;
        startScreenElement = tag.StartScreenElement;
        flag = false;
      }
      int selectedRadioButton = 0;
      string startScreen = content.StartScreenFileName(startScreenElement);
      if (startScreen != string.Empty)
        selectedRadioButton = this.defaultStartScreenList.StartScreenListElement(startScreen) < 0 ? this.startScreensList.StartScreenListElement(startScreen) + this.defaultStartScreenList.Length + 1 : this.defaultStartScreenList.StartScreenListElement(startScreen) + 1;
      SelectStartScreenDialog startScreenDialog = new SelectStartScreenDialog(this.screens.ToArray(), selectedRadioButton);
      if (startScreenDialog.ShowDialog() != DialogResult.OK)
        return;
      int selectedScreen = startScreenDialog.SelectedScreen;
      if (selectedScreen == 0)
      {
        this.fastParameterStartScreenPictuerBox[tag.Position].Image = HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true);
        this.fastParameterColorStartScreenPictuerBox[tag.Position].Image = HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true);
        content.StartScreenFileName(startScreenElement, string.Empty);
      }
      else if (selectedScreen <= this.defaultStartScreenList.Length)
      {
        this.fastParameterStartScreenPictuerBox[tag.Position].Image = this.defaultStartScreenList.ThumbnailImage(selectedScreen - 1) != null ? HelperClass.ResizeImage(this.defaultStartScreenList.ThumbnailImage(selectedScreen - 1), this.startScreenPreviewSize, true) : HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true);
        this.fastParameterColorStartScreenPictuerBox[tag.Position].Image = this.defaultStartScreenList.ColorThumbnailImage(selectedScreen - 1) != null ? HelperClass.ResizeImage(this.defaultStartScreenList.ColorThumbnailImage(selectedScreen - 1), this.startScreenPreviewSize, true) : HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true);
        content.StartScreenFileName(startScreenElement, this.defaultStartScreenList.Filename(selectedScreen - 1));
        content.ColorStartScreenFileName(startScreenElement, this.defaultStartScreenList.ColorFilename(selectedScreen - 1));
      }
      else
      {
        this.fastParameterStartScreenPictuerBox[tag.Position].Image = this.startScreensList.ThumbnailImage(selectedScreen - 1 - this.defaultStartScreenList.Length) != null ? HelperClass.ResizeImage(this.startScreensList.ThumbnailImage(selectedScreen - 1 - this.defaultStartScreenList.Length), this.startScreenPreviewSize, true) : HelperClass.ResizeImage(this.screen_empty_active, this.startScreenPreviewSize, true);
        this.fastParameterColorStartScreenPictuerBox[tag.Position].Image = this.startScreensList.ColorThumbnailImage(selectedScreen - 1 - this.defaultStartScreenList.Length) != null ? HelperClass.ResizeImage(this.startScreensList.ColorThumbnailImage(selectedScreen - 1 - this.defaultStartScreenList.Length), this.startScreenPreviewSize, true) : HelperClass.ResizeImage((Image) Resources.cMMI_StartAnimation_active, this.startScreenPreviewSize, true);
        content.StartScreenFileName(startScreenElement, this.startScreensList.Filename(selectedScreen - 1 - this.defaultStartScreenList.Length));
        content.ColorStartScreenFileName(startScreenElement, this.startScreensList.ColorFilename(selectedScreen - 1 - this.defaultStartScreenList.Length));
      }
      try
      {
        if (flag)
          ListSettingFile.WriteFile(content, Directories.Instance.DefaultParametersListFileName);
        else
          ListSettingFile.WriteFile(content, Directories.Instance.UserDataPath + Directories.Instance.ParametersListFile);
      }
      catch (Exception ex)
      {
        UniqueError.Message(UniqueError.Number.MASS_PARAMETERLISTFILE_WRITE_ERROR);
      }
    }

    private void LogProductionInformation()
    {
      MMITransceiver mmiTransceiver = new MMITransceiver(this.MMICom);
      mmiTransceiver.ReceiveSettingsFromMMI(ParameterIds.MMI_PRODUCTION_INFORMATION);
      mmiTransceiver.ReceiveSettingsFromMMI(ParameterIds.MOTOR_VERSION_INFORMATION);
      mmiTransceiver.ReceiveSettingsFromMMI(ParameterIds.ACCU_VERSION_INFORMATION);
      new ParameterTransmissionLogger().Write(Parameters.Instance.MotorSerialNumber, Parameters.Instance.MMISerialNumber, Parameters.Instance.AccuSerialNumber, MainWindow.Instance.ProductionNumber, DateTime.Now.ToString("yyyyMMddHHmmsss"));
    }

    private void ProductionLogging()
    {
      int actualPosition = 0;
      int maximum = Parameters.Instance.ParameterElements - 1;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      HelperClass.DoEvents();
      this.receiveTabIconPictureBox.Image = (Image) Resources.Icon_mReceive_Active;
      this.receiveTabIconPictureBox.Enabled = true;
      this.SetReceiveLabelAndSuccessImage(GlobalResource.MassConfigPanel_MassReceive + "\n" + MainWindow.Instance.ProductionNumber, (Image) null);
      if (new MMITransceiver(this.MMICom).ReceiveAllAccessibleSettingsFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar()))
      {
        this.parameterPanel.updateUIWithDictionaryValues();
        disconnectWarning.ProgressBar().Value = 100;
        disconnectWarning.ProgressBar().Refresh();
        disconnectWarning.Dispose();
        string[] textEnglishLabels = this.parameterPanel.GetElementTextEnglishLabels();
        string[] numbericUpDownValues = this.parameterPanel.GetElementNumbericUpDownValues();
        bool immobilizerActive = Parameters.Instance.ImmobilizerActive;
        bool flag1 = this.parameterPanel.IsSpeedLimiterActive();
        bool flag2 = this.parameterPanel.IsPushAssistantActive();
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string enLabel1 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.DURSCHNITTSGESCHWINDIGKEIT)).EnLabel;
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string enLabel2 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.MAX_GESCHWINDIGKEIT_EVER)).EnLabel;
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string enLabel3 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.TAGES_FAHRZEIT)).EnLabel;
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string enLabel4 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.TAGESKILOMETER)).EnLabel;
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        string enLabel5 = Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESAMT_FAHRZEIT)).EnLabel;
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        for (int index = 0; index < textEnglishLabels.Length; ++index)
        {
          string str1 = textEnglishLabels[index].Substring(textEnglishLabels[index].IndexOf(':') + 2);
          if (str1 != enLabel1 && str1 != enLabel2 && (str1 != enLabel3 && str1 != enLabel4) && str1 != enLabel5)
          {
            string str2 = textEnglishLabels[index].Substring(textEnglishLabels[index].IndexOf(':') + 2);
            // ISSUE: reference to a compiler-generated method
            // ISSUE: reference to a compiler-generated method
            if (Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_STRECKE)).EnLabel == str2)
            {
              stringList1.Add(textEnglishLabels[index]);
              if (numbericUpDownValues[index] == GlobalResource.Parameter_Not_Checked)
                stringList2.Add("0");
              else
                stringList2.Add(numbericUpDownValues[index].Replace(',', '.'));
            }
            else
            {
              // ISSUE: reference to a compiler-generated method
              // ISSUE: reference to a compiler-generated method
              if (Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.SERVICE_INTERVAL_DATUM_JAHR)).EnLabel == str2)
              {
                stringList1.Add(textEnglishLabels[index]);
                if (numbericUpDownValues[index] == GlobalResource.Parameter_Not_Checked)
                  stringList2.Add("01.01.2000");
                else
                  stringList2.Add(numbericUpDownValues[index].Replace(',', '.'));
              }
              else
              {
                stringList1.Add(textEnglishLabels[index]);
                stringList2.Add(numbericUpDownValues[index].Replace(',', '.'));
              }
            }
          }
        }
        stringList1.Add("sMMI Lock Active");
        stringList2.Add(string.Concat((object) immobilizerActive));
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        stringList1.Add(Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESCHWINDIGKEIT_SCHIEBEHILFE_VORWAERTS)).EnLabel + " Active");
        stringList2.Add(string.Concat((object) flag2));
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        stringList1.Add(Parameters.Instance.GetParameterValueObject(Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.BREMSASSISTENT_GESCHWINDIGKEIT)).EnLabel + " Active");
        stringList2.Add(string.Concat((object) flag1));
        string[] array1 = stringList1.ToArray();
        string[] array2 = stringList2.ToArray();
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        for (int index = 0; index < array1.Length; ++index)
        {
          array1[index] = textInfo.ToTitleCase(array1[index]).Replace(" ", string.Empty);
          array1[index] = array1[index].Replace("(", "_");
          array1[index] = array1[index].Replace(")", string.Empty);
          array1[index] = array1[index].Replace("/", string.Empty);
          int num = array1[index].IndexOf(':');
          array1[index] = array1[index].Substring(num + 1);
        }
        ParameterSettingsLogger.Write(array1, array2, MainWindow.Instance.ProductionNumber, ActivationInformation.OemPartnerId(), DateTime.Now.ToString("yyyyMMddHHmmsss"));
        if (this.speakerActivated)
          SystemSounds.Beep.Play();
        this.SetReceiveLabelAndSuccessImage(GlobalResource.MassConfigPanel_MassReceive_Done + "\n" + MainWindow.Instance.ProductionNumber, (Image) Resources.button_ok);
        new UpdateWorker().DoParameterTransmission();
        MainWindow.Instance.Cursor = Cursors.Default;
      }
      else
      {
        disconnectWarning.ProgressBar().Value = 100;
        disconnectWarning.ProgressBar().Refresh();
        disconnectWarning.Dispose();
        MainWindow.Instance.Cursor = Cursors.Default;
        this.SetReceiveLabelAndSuccessImage(GlobalResource.MassConfigPanel_MassReceive_Failed, (Image) Resources.buttons_error);
        int num = (int) MessageBox.Show(GlobalResource.MassConfigPanel_MassReceive_Failed, GlobalResource.MassConfigPanel_MassReceive_Failed_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void SetReceiveLabelAndSuccessImage(string text, Image successImage)
    {
      this.receiveLabel.Text = text;
      this.receiveLabel.AutoSize = true;
      this.receiveLabel.AutoEllipsis = false;
      if (this.receiveLabel.Width > this.receiveTableLayoutPanel.Width)
      {
        this.receiveLabel.Size = new Size(this.receiveTableLayoutPanel.Width, this.receiveLabel.Height);
        this.receiveLabel.AutoEllipsis = true;
        this.receiveLabel.AutoSize = false;
      }
      this.receiveTabOkPictureBox.Location = new Point(this.receiveLabel.Location.X + this.receiveLabel.Width + 5, this.receiveTabOkPictureBox.Location.Y);
      this.receiveTabOkPictureBox.Image = successImage;
    }

    private bool SetProductionNumber()
    {
      string empty = string.Empty;
      if (InputBox.Show(GlobalResource.ProductionNumber_InputBox_Message, GlobalResource.ProductionNumber_InputBox_Caption, GlobalResource.ok, GlobalResource.cancel, ref empty) != DialogResult.OK)
        return false;
      if (Regex.Match(empty, HelperClass.ValidCharacters, RegexOptions.IgnoreCase).Success && empty.Length < (int) byte.MaxValue)
      {
        MainWindow.Instance.ProductionNumber = empty;
        return true;
      }
      UniqueError.Message(UniqueError.Number.MASS_PRODUCTIONNUMBER_INVALID);
      return false;
    }

    private bool TransferParameterDataToMMI(
      ZerroProgressBar bar,
      int positionOfParameter,
      ref int actualPosition,
      int maximum,
      bool isDefault)
    {
      if (!(!isDefault ? this.ReadParameterFile(this.parameterList.ContentFile(positionOfParameter), isDefault, false) : this.ReadParameterFile(this.defaultParameterList.ContentFile(positionOfParameter), isDefault, false)))
        return false;
      bool accessibleSettingsFromMmi = this.transceiver.ReceiveAllAccessibleSettingsFromMMI(ref actualPosition, maximum, bar);
      this.parameterPanel.updateDictionaryWithAccessableUIValues();
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
      if (!accessibleSettingsFromMmi)
        return accessibleSettingsFromMmi;
      bool mmi = this.transceiver.TransferAllAccessibleSettingsToMMI(ref actualPosition, maximum, bar);
      if (!mmi)
        return mmi;
      Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
      thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread.Start();
      while (thread.IsAlive)
        Thread.Sleep(50);
      bool waitingForMmiSucceeded = this.transceiver.WaitingForMMISucceeded;
      return mmi && waitingForMmiSucceeded;
    }

    private bool EnablesUserControl(bool enable)
    {
      MainWindow.Instance.EnablesUserControl(enable);
      this.Enabled = enable;
      return enable;
    }

    private void MassTransferParameterDataToMMI()
    {
      this.massTabOkPictureBox.Image = (Image) null;
      if (!this.parameterPanel.ValuesValidForConnectedMMI())
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      disconnectWarning.ProgressBar().Value = 0;
      HelperClass.DoEvents();
      int actualPosition = 0;
      string filename = string.Empty;
      string empty = string.Empty;
      int maximum = (Parameters.Instance.ParameterElements - 1) * 2;
      int int32 = Convert.ToInt32(this.startScreenMassPictureBox.Name);
      if (int32 > 0)
      {
        if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
        {
          if (int32 <= this.defaultStartScreenList.Length)
          {
            string str = this.defaultStartScreenList.ColorFilename(int32 - 1);
            if (!string.IsNullOrEmpty(str))
              filename = Directories.Instance.DefaultsPath + str;
          }
          else
          {
            string str = this.startScreensList.ColorFilename(int32 - 1 - this.defaultStartScreenList.Length);
            if (!string.IsNullOrEmpty(str))
              filename = Directories.Instance.UserDataPath + str;
          }
          if (!string.IsNullOrEmpty(filename))
            maximum += Pictures.Instance.NumberOfPictures * 320 * 240 * 2 / 1000;
        }
        else
        {
          if (int32 <= this.defaultStartScreenList.Length)
          {
            string str = this.defaultStartScreenList.Filename(int32 - 1);
            if (!string.IsNullOrEmpty(str))
              filename = Directories.Instance.DefaultsPath + str;
          }
          else
          {
            string str = this.startScreensList.Filename(int32 - 1 - this.defaultStartScreenList.Length);
            if (!string.IsNullOrEmpty(str))
              filename = Directories.Instance.UserDataPath + str;
          }
          if (!string.IsNullOrEmpty(filename))
            maximum += Pictures.Instance.NumberOfPictures * 320 * 240 / 1000;
        }
      }
      bool flag = this.transceiver.ReceiveAllAccessibleSettingsFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      this.parameterPanel.updateDictionaryWithAccessableUIValues();
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
      if (flag)
        flag = this.transceiver.TransferAllAccessibleSettingsToMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      if (flag)
      {
        Thread thread = new Thread(new ThreadStart(this.transceiver.WaitingForWritingDataToMemoryOfMMI));
        thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
        thread.Start();
        while (thread.IsAlive)
          Thread.Sleep(50);
        if (!string.IsNullOrEmpty(filename))
        {
          byte[] image = StartScreenImageFile.ReadFile(filename).Image;
          bool waitingForMmiSucceeded = this.transceiver.WaitingForMMISucceeded;
          flag = this.transceiver.TransferStartScreenToMMI(image, ref actualPosition, maximum, disconnectWarning.ProgressBar()) && waitingForMmiSucceeded;
        }
        else if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ && string.IsNullOrEmpty(filename))
          flag = this.transceiver.DeleteStartScreenFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      }
      disconnectWarning.ProgressBar().Value = 100;
      disconnectWarning.ProgressBar().Refresh();
      disconnectWarning.Dispose();
      if (flag)
      {
        this.LogProductionInformation();
        this.massTabOkPictureBox.Image = (Image) Resources.button_ok;
      }
      else
        this.massTabOkPictureBox.Image = (Image) Resources.buttons_error;
      if (this.speakerActivated)
        SystemSounds.Beep.Play();
      MainWindow.Instance.Cursor = Cursors.Default;
      if (flag)
        return;
      int num = (int) MessageBox.Show(GlobalResource.MassConfigPanel_MassTransmit_Failed, GlobalResource.MassConfigPanel_MassTransmit_Failed_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    private void ReceiveParameterDataFromMMI()
    {
      if (!this.autoCheckBox.Checked || !this.SetProductionNumber())
        return;
      this.ProductionLogging();
    }

    private delegate void FunctionActivation(bool value);
  }
}
