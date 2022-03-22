// Decompiled with JetBrains decompiler
// Type: ZerroWare.MainWindow
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using MMI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class MainWindow : Form
  {
    private static volatile MainWindow instance = (MainWindow) null;
    private static object mutex = new object();
    private ComponentResourceManager mainResources;
    private Thread mmiThread;
    private Thread backgroundET;
    private Communication MMICom;
    private UpdateWorker updateWorker;
    private Size absoluteMinimumSize;
    private int captionHeight;
    private bool doNotChageInnerElement;
    private bool waitingForUpdateThread;
    private bool userControlEnabled;
    private bool testmodeAvailability;
    private bool newNews;
    private bool mainWindowIsShowing;
    private KnowledgePanel knowledgePanel;
    private WelcomePanel welcomePanel;
    private ConfigurationPanel configPanel;
    private DiagnosePanel diagnosePanel;
    private SettingsPanel settingsPanel;
    private HelpPanel helpPanel;
    private CultureInfo mainCultureInfo;
    private IContainer components;
    private Panel leftButtonPanel;
    private Button knowledgeBaseButton;
    private Button configartionButton;
    private Button diagnoseButton;
    private PictureBox logoPictureBox;
    private PictureBox statusPictureBox;
    private Label transparentLabel;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel copyrightToolStripStatusLabel;
    private ToolStripStatusLabel spacerToolStripStatusLabel;
    private ToolStripStatusLabel versionToolStripStatusLabel;
    private Panel centerPanel;
    private Panel statusPanel;
    private FlickerFreeTableLayoutPanel statusTableLayoutPanel;
    private Button helpButton;
    private Button settingsButton;

    public string ProductionNumber { set; get; }

    public bool IsMMIUpdateInProgress { set; get; }

    public event MainWindow.HelloLoopPassedEventHandler HelloLoopPassed;

    public event MainWindow.MMIInformationChangedEventHandler MMIInformationChanged;

    public event MainWindow.AccessLevelUpdateHanlder AccessLevelUpdate;

    public static MainWindow Instance
    {
      get
      {
        if (MainWindow.instance == null)
        {
          lock (MainWindow.mutex)
          {
            if (MainWindow.instance == null)
              MainWindow.instance = new MainWindow();
          }
        }
        return MainWindow.instance;
      }
    }

    public CultureInfo CurrentCulture() => this.mainCultureInfo;

    private MainWindow()
    {
      this.AutoScaleMode = AutoScaleMode.None;
      this.mainWindowIsShowing = false;
      this.ReadCompleteMMIData = true;
      this.doNotChageInnerElement = false;
      this.waitingForUpdateThread = false;
      this.userControlEnabled = true;
      this.testmodeAvailability = false;
      this.newNews = false;
      this.IsMMIUpdateInProgress = false;
      this.absoluteMinimumSize = new Size(740, 510);
      GlobalLogger.Instance.WriteLine("PreInit");
      Program.SetSplashPercent(75);
      if (!string.IsNullOrEmpty(Settings.Default.Language))
      {
        CultureInfo cultureInfo = new CultureInfo(Settings.Default.Language);
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        this.mainCultureInfo = cultureInfo;
        this.mainResources = new ComponentResourceManager(typeof (MainWindow));
      }
      else
      {
        this.mainCultureInfo = Thread.CurrentThread.CurrentCulture;
        Settings.Default.Language = this.mainCultureInfo.ToString();
      }
      Program.SetSplashPercent(80);
      this.updateWorker = new UpdateWorker();
      this.updateWorker.AccessLevelUpdate += new UpdateWorker.AccessLevelUpdateHanlder(this.UpdateWorkerAccessLevelUpdateHanlder);
      this.updateWorker.NewsAvailable += new UpdateWorker.NewsAvailableHandler(this.updateWorker_NewsAvailable);
      this.updateWorker.UpdateDone += new UpdateWorker.UpdateDonedHandler(this.updateWorker_UpdateDone);
      this.backgroundET = new Thread(new ThreadStart(this.updateWorker.DoUpdateInStealthMode));
      this.backgroundET.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
      this.backgroundET.Start();
      Program.SetSplashPercent(85);
      this.InitializeComponent();
      Program.SetSplashPercent(90);
      this.SetTestmodeAvailability();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
      this.Height = Settings.Default.FormHeight;
      this.Width = Settings.Default.FormWidth;
      this.captionHeight = SystemInformation.CaptionHeight;
      this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height + SystemInformation.CaptionHeight - 22);
      if (this.Height < this.MinimumSize.Height)
        this.Height = this.MinimumSize.Height;
      if (this.Width < this.MinimumSize.Width)
        this.Width = this.MinimumSize.Width;
      this.statusPanel.Location = new Point(this.ClientSize.Width - this.statusPanel.Width - 13, this.statusPanel.Location.Y);
      this.InitUISettings();
      this.InitCustomUISettings();
      Program.SetSplashPercent(95);
      this.StartConnectingToMMI();
      GlobalLogger.Instance.WriteLine("PostInit");
      Program.SetSplashPercent(97);
      this.logoPictureBox.Select();
      this.UpdateStyles();
      Program.SetSplashPercent(99);
      this.ShowMaxSpeedWarning = true;
            this.Text = "JALILI Bikes";
    }

    private void StartConnectingToMMI()
    {
      this.MMICom = new Communication(1240, 60, 20000);
      this.mmiThread = new Thread(new ThreadStart(this.MMICom.Connnect));
      this.MMICom.Inserted += new Communication.InsertedEventHandler(this.MMIAttachedHandler);
      this.MMICom.Removed += new Communication.RemovedEventHandler(this.MMIDettachedHandler);
      this.mmiThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
      this.mmiThread.Start();
    }

    private void InitCustomUISettings()
    {
      this.AddWelcomePanel();
      this.statusPictureBox.Image = (Image) Resources.button_notconnected;
      this.transparentLabel.Text = GlobalResource.MainWindow_MMINotConnected;
      this.diagnoseButton.Enabled = false;
      object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
      if (customAttributes.Length == 0)
        this.copyrightToolStripStatusLabel.Text = "";
      this.copyrightToolStripStatusLabel.Text = ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
      this.versionToolStripStatusLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

    private void InitUISettings()
    {
      this.Font = FontDefinition.DefaultTextFont;
      this.knowledgeBaseButton.Font = FontDefinition.ButtonFont;
      this.diagnoseButton.Font = FontDefinition.ButtonFont;
      this.configartionButton.Font = FontDefinition.ButtonFont;
      this.helpButton.Font = FontDefinition.ButtonFont;
      this.settingsButton.Font = FontDefinition.ButtonFont;
      this.transparentLabel.Font = FontDefinition.MenubarFont;
      this.copyrightToolStripStatusLabel.Font = FontDefinition.MenubarFont;
      this.versionToolStripStatusLabel.Font = FontDefinition.MenubarFont;
      this.leftButtonPanel.Size = SizeDefinition.ButtonPanelSize;
      this.leftButtonPanel.Location = new Point(0, 0);
      this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height - 30);
      this.Size = new Size(this.Size.Width, this.Size.Height - 30);
      this.knowledgeBaseButton.Size = SizeDefinition.ButtonSize;
      this.configartionButton.Size = SizeDefinition.ButtonSize;
      this.diagnoseButton.Size = SizeDefinition.ButtonSize;
      this.logoPictureBox.Size = SizeDefinition.LogoSize;
      this.helpButton.Size = SizeDefinition.ButtonSize;
      this.settingsButton.Size = SizeDefinition.ButtonSize;
      this.centerPanel.Location = new Point(SizeDefinition.ButtonSize.Width + 22, this.centerPanel.Location.Y - 5);
      this.removeHighlight();
    }

    private void knowledgeBaseButton_Click(object sender, EventArgs e)
    {
      if (this.knowledgeBaseButton.FlatStyle == FlatStyle.Flat)
        return;
      this.Cursor = Cursors.WaitCursor;
      if (!this.highlightSelected((object) this.knowledgeBaseButton))
      {
        this.Cursor = Cursors.Default;
      }
      else
      {
        if (this.knowledgePanel == null)
          this.knowledgePanel = new KnowledgePanel();
        if (this.knowledgePanel != null)
        {
          this.centerPanel.Controls.Add((Control) this.knowledgePanel);
          this.knowledgePanel.Visible = true;
        }
        if (this.knowledgePanel != null)
          this.knowledgePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Cursor = Cursors.Default;
      }
    }

    private void AddWelcomePanel()
    {
      this.Cursor = Cursors.WaitCursor;
      if (this.welcomePanel == null)
        this.welcomePanel = new WelcomePanel();
      if (this.welcomePanel != null)
      {
        this.centerPanel.Controls.Add((Control) this.welcomePanel);
        this.welcomePanel.Visible = true;
      }
      if (this.welcomePanel != null)
        this.welcomePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.Cursor = Cursors.Default;
    }

    private void configartionButton_Click(object sender, EventArgs e)
    {
      if (this.configartionButton.FlatStyle == FlatStyle.Flat)
        return;
      this.Cursor = Cursors.WaitCursor;
      if (!this.highlightSelected((object) this.configartionButton))
      {
        this.Cursor = Cursors.Default;
      }
      else
      {
        if (this.configPanel == null)
          this.configPanel = new ConfigurationPanel(this.MMICom);
        if (this.configPanel != null)
        {
          this.centerPanel.Controls.Add((Control) this.configPanel);
          this.configPanel.Visible = true;
        }
        if (this.configPanel != null)
          this.configPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Cursor = Cursors.Default;
      }
    }

    private void diagnoseButton_Click(object sender, EventArgs e)
    {
      if (this.diagnoseButton.FlatStyle == FlatStyle.Flat)
        return;
      this.Cursor = Cursors.WaitCursor;
      if (!this.highlightSelected((object) this.diagnoseButton))
      {
        this.Cursor = Cursors.Default;
      }
      else
      {
        if (this.diagnosePanel == null)
          this.diagnosePanel = new DiagnosePanel(this.MMICom);
        if (this.diagnosePanel != null)
        {
          this.centerPanel.Controls.Add((Control) this.diagnosePanel);
          this.diagnosePanel.Visible = true;
        }
        if (this.diagnosePanel != null)
          this.diagnosePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Cursor = Cursors.Default;
      }
    }

    private void settingsButton_Click(object sender, EventArgs e)
    {
      if (this.settingsButton.FlatStyle == FlatStyle.Flat)
        return;
      this.Cursor = Cursors.WaitCursor;
      if (!this.highlightSelected((object) this.settingsButton))
      {
        this.Cursor = Cursors.Default;
      }
      else
      {
        if (this.settingsPanel == null)
          this.settingsPanel = new SettingsPanel(this.MMICom);
        if (this.settingsPanel != null)
        {
          this.centerPanel.Controls.Add((Control) this.settingsPanel);
          this.settingsPanel.Visible = true;
        }
        if (this.settingsPanel != null)
          this.settingsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Cursor = Cursors.Default;
      }
    }

    private void helpButton_Click(object sender, EventArgs e)
    {
      if (this.helpButton.FlatStyle == FlatStyle.Flat)
        return;
      this.Cursor = Cursors.WaitCursor;
      if (!this.highlightSelected((object) this.helpButton))
      {
        this.Cursor = Cursors.Default;
      }
      else
      {
        if (this.helpPanel == null)
          this.helpPanel = new HelpPanel();
        if (this.helpPanel != null)
        {
          this.centerPanel.Controls.Add((Control) this.helpPanel);
          this.helpPanel.Visible = true;
        }
        if (this.helpPanel != null)
          this.helpPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        this.Cursor = Cursors.Default;
      }
    }

    private bool highlightSelected(object sender)
    {
      if (this.configPanel != null)
      {
        if (this.configPanel.ValueChanged && !this.configPanel.SaveChanges())
          return false;
      }
      else if (this.settingsPanel != null && this.settingsPanel.ValueChanged && this.settingsPanel.SaveChanges())
        return false;
      this.removeHighlight();
      Button button = (Button) sender;
      button.FlatStyle = FlatStyle.Flat;
      button.BackgroundImage = (Image) BackgroundImages.BlueGradient;
      switch (button.Name)
      {
        case "diagnoseButton":
          this.doNotChageInnerElement = false;
          break;
        case "updateButton":
          this.doNotChageInnerElement = true;
          break;
        case "configartionButton":
        case "rolloutButton":
        case "settingsButton":
          this.doNotChageInnerElement = true;
          break;
        default:
          this.doNotChageInnerElement = true;
          break;
      }
      return true;
    }

    private void removeHighlight()
    {
      this.knowledgeBaseButton.FlatStyle = FlatStyle.Standard;
      this.knowledgeBaseButton.ForeColor = ColorDefinition.ButtonDefaultTextColor;
      this.knowledgeBaseButton.Image = HelperClass.AddLeftSpaceToImage((Image) Resources.Icon_News_active, 10);
      this.knowledgeBaseButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.configartionButton.FlatStyle = FlatStyle.Standard;
      this.configartionButton.ForeColor = ColorDefinition.ButtonDefaultTextColor;
      this.configartionButton.Image = HelperClass.AddLeftSpaceToImage((Image) Resources.Icon_Fahrprofile_active, 10);
      this.configartionButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.diagnoseButton.FlatStyle = FlatStyle.Standard;
      this.diagnoseButton.ForeColor = ColorDefinition.ButtonDefaultTextColor;
      this.diagnoseButton.Image = HelperClass.AddLeftSpaceToImage((Image) Resources.Icon_DiagnoseStarten_active, 10);
      this.diagnoseButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.helpButton.FlatStyle = FlatStyle.Standard;
      this.helpButton.ForeColor = ColorDefinition.ButtonDefaultTextColor;
      this.helpButton.Image = HelperClass.AddLeftSpaceToImage((Image) Resources.Icon_Question_active, 10);
      this.helpButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.settingsButton.FlatStyle = FlatStyle.Standard;
      this.settingsButton.ForeColor = ColorDefinition.ButtonDefaultTextColor;
      this.settingsButton.Image = HelperClass.AddLeftSpaceToImage((Image) Resources.Icon_Settings_active, 10);
      this.settingsButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.RemovePanelsInCenterPanel();
    }

    private void RemovePanelsInCenterPanel()
    {
      if (this.knowledgePanel != null)
      {
        this.centerPanel.Controls.Remove((Control) this.knowledgePanel);
        this.knowledgePanel.Dispose();
        this.knowledgePanel = (KnowledgePanel) null;
      }
      if (this.welcomePanel != null)
      {
        this.centerPanel.Controls.Remove((Control) this.welcomePanel);
        this.welcomePanel.Dispose();
        this.welcomePanel = (WelcomePanel) null;
      }
      if (this.configPanel != null)
      {
        this.centerPanel.Controls.Remove((Control) this.configPanel);
        this.configPanel.Dispose();
        this.configPanel = (ConfigurationPanel) null;
        if (!this.ReadCompleteMMIData && !this.MMIDataWasCompletelyRead && this.MMICom.DeviceAttached)
        {
          this.ReadCompleteMMIData = true;
          MainWindow.Instance.WaitingForHello();
        }
        else
          this.ReadCompleteMMIData = true;
      }
      if (this.diagnosePanel != null)
      {
        this.diagnosePanel.Visible = false;
        this.centerPanel.Controls.Remove((Control) this.diagnosePanel);
      }
      if (this.settingsPanel != null)
      {
        this.settingsPanel.Visible = false;
        this.centerPanel.Controls.Remove((Control) this.settingsPanel);
      }
      if (this.helpPanel == null)
        return;
      this.helpPanel.Visible = false;
      this.centerPanel.Controls.Remove((Control) this.helpPanel);
    }

    private void beendenToolStripMenuItem_Click(object sender, EventArgs e) => this.Dispose();

    public ConnectionEstablished ConnectionEstablishForm { set; get; }

    public void WaitingForHello()
    {
      while (UniqueError.WaitingForClick)
        Thread.Sleep(1000);
      ConnectionEstablished ce = new ConnectionEstablished((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      this.ConnectionEstablishForm = ce;
      MainWindowEventArgs e = new MainWindowEventArgs();
      this.Invoke((MethodInvoker) (() => ce.Show((IWin32Window) MainWindow.Instance)));
      HelperClass.DoEvents();
      int num1 = 0;
      Thread.Sleep(250);
      while (this.MMICom.Response != ParseErrorCodes.SUCCESS)
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.SayHello());
        Thread.Sleep(1000);
        if (num1 > 20)
        {
          e.Done = false;
          if (this.HelloLoopPassed != null)
            this.HelloLoopPassed((object) this, e);
          ce.Dispose();
          this.ConnectionEstablishForm = (ConnectionEstablished) null;
          this.SetMMIConnected(false);
          int num2 = (int) MessageBox.Show(GlobalResource.MainWindow_MMIConnectionFailed_Message, GlobalResource.MainWindow_MMIConnectionFailed_Caption);
          return;
        }
        ++num1;
      }
      if (CommandBuilder.Instance.OperationMode != OperationModes.SERVICE)
      {
        this.MMICom.SendMessage(CommandBuilder.Instance.ModeFlag(SubCommands.SET, OperationModes.SERVICE));
        if (!new MMITransceiver(this.MMICom).ResponseWasSuccess())
        {
          ce.Dispose();
          this.ConnectionEstablishForm = (ConnectionEstablished) null;
          int num2 = (int) MessageBox.Show(GlobalResource.MainWindow_MMIConnectionFailed_Message, GlobalResource.MainWindow_MMIConnectionFailed_Caption);
          this.SetMMIConnected(false);
          e.Done = false;
          if (this.HelloLoopPassed == null)
            return;
          this.HelloLoopPassed((object) this, e);
          return;
        }
      }
      int actualPosition = 1;
      int maximum = Parameters.Instance.ParameterElements - 1 + CommandBuilder.Instance.MotorErrorRingBufferMaximumElements + CommandBuilder.Instance.AccuErrorRingBufferMaximumElements;
      byte[][] ringBufferContent1 = new byte[0][];
      byte[] ringBufferContent2 = new byte[0];
      MMITransceiver mmiTransceiver = new MMITransceiver(this.MMICom);
      CommandBuilder.Instance.UseNewFirmwareFlag = this.isDFIsupportedByMMI(mmiTransceiver.ReceiveFirmwareVersion(Command_FirmwareTyps.FIRMWARE_MMI)) || CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ;
      if (this.ReadCompleteMMIData)
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = mmiTransceiver.ReadMotorErrorLogRingBuffer(ref actualPosition, maximum, ce.ProgressBar(), ref ringBufferContent1);
        if (flag3)
          flag1 = mmiTransceiver.ReadAccuErrorLogRingBuffer(ref actualPosition, maximum, ce.ProgressBar(), ref ringBufferContent2);
        if (flag3 && flag1)
          flag2 = mmiTransceiver.ReceiveAllSettingsFromMMI(ref actualPosition, maximum, ce.ProgressBar());
        if (flag3 && flag1 && flag2)
        {
          MMIData.Instance.MotorErrorBlocks = ringBufferContent1;
          MMIData.Instance.AccuErrorBlocks = ringBufferContent2;
          MMIData.Instance.SetParameters(Parameters.Instance.Dictionary);
          MMIData.Instance.MMISerialNumber = Parameters.Instance.MMISerialNumber;
          MMIData.Instance.SavedMotorFirmwareVersion = mmiTransceiver.ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps.FIRMWARE_MOTOR);
          MMIData.Instance.SavedAccuFirmwareVersion = mmiTransceiver.ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps.FIRMWARE_ACCU);
          if (CommandBuilder.Instance.UseNewFirmwareFlag)
            MMIData.Instance.SavedAccuDFIVersion = mmiTransceiver.ReceiveSavedFirmwareVersionFromMMI(Command_FirmwareTyps.ACCU_DFI_FILE);
          if (this.MMIInformationChanged != null)
            this.MMIInformationChanged((object) this, EventArgs.Empty);
          FinishSound.Instance.Play();
          this.MMIDataWasCompletelyRead = true;
        }
        else
        {
          this.MMIDataWasCompletelyRead = false;
          return;
        }
      }
      ce.Dispose();
      this.ConnectionEstablishForm = (ConnectionEstablished) null;
      if (!this.MMICom.DeviceAttached)
      {
        this.SetMMIConnected(false);
        e.Done = false;
        if (this.HelloLoopPassed == null)
          return;
        this.HelloLoopPassed((object) this, e);
      }
      else
      {
        this.SetMMIConnected(true);
        e.Done = true;
        if (this.HelloLoopPassed != null)
          this.HelloLoopPassed((object) this, e);
        this.ShowMaxSpeedWaringMessage();
      }
    }

    public void ShowMaxSpeedWaringMessage()
    {
      if (ActivationInformation.VersionLevelProperty > 2)
      {
        if (this.ShowMaxSpeedWarning)
        {
          try
          {
            // ISSUE: reference to a compiler-generated method
            double actMaxSpeed = MMIData.Instance.GetParameterValueObject(ParameterIds.MMI_DRIVE_SETTINGS, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.MAXIMALES_UNTERSTUETUNGSGESCHWINDIGKEIT)).ReadableValue;
            try
            {
              if (actMaxSpeed > 25.0)
              {
                if (!TestDisclaimer.IsShowing())
                {
                  if (!this.IsMMIUpdateInProgress)
                  {
                    int num;
                    this.Invoke((MethodInvoker) (() => num = (int) MessageBox.Show((IWin32Window) this, string.Format(GlobalResource.MainWindow_MaxSpeedAttention_Message, (object) actMaxSpeed), GlobalResource.MainWindow_MaxSpeedAttention_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)));
                  }
                }
              }
            }
            catch (Exception ex)
            {
            }
          }
          catch (NullReferenceException ex)
          {
          }
        }
      }
      MainWindow.Instance.ShowMaxSpeedWarning = true;
    }

    public bool ShowMaxSpeedWarning { set; get; }

    private bool isDFIsupportedByMMI(string MMIVersion) => HelperClass.IsUpToDate(MMIVersion, "2.0.0.0");

    private void MMIAttachedHandler(object sender, EventArgs e) => new Thread(new ThreadStart(this.WaitingForHello))
    {
      CurrentUICulture = MainWindow.Instance.CurrentCulture()
    }.Start();

    private void MMIDettachedHandler(object sender, EventArgs e)
    {
      this.SetMMIConnected(false);
      this.MMIDataWasCompletelyRead = false;
    }

    private void SetMMIConnected(bool toggle)
    {
      this.MMICom.DeviceConnected = toggle;
      if (this.statusPictureBox.InvokeRequired && this.transparentLabel.InvokeRequired && this.diagnoseButton.InvokeRequired)
      {
        MainWindow.MMIConnected mmiConnected = new MainWindow.MMIConnected(this.SetMMIConnected);
        try
        {
          this.Invoke((Delegate) mmiConnected, (object) toggle);
        }
        catch (Exception ex)
        {
          GlobalLogger.Instance.WriteLine(ex);
        }
      }
      else
      {
        if (DisconnectWarning.InstanceCounter() == 0)
          this.diagnoseButton.Enabled = toggle;
        else
          this.diagnoseButton.Enabled = false;
        if (toggle)
        {
          this.statusPictureBox.Image = (Image) null;
          this.statusPictureBox.Image = (Image) Resources.button_connected;
          this.statusPictureBox.Update();
          if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
            this.transparentLabel.Text = GlobalResource.MainWindow_MMIConnected.Replace("sMMI", "connectMMI");
          else
            this.transparentLabel.Text = GlobalResource.MainWindow_MMIConnected;
          this.transparentLabel.Update();
          if (this.doNotChageInnerElement)
            return;
          this.diagnoseButton_Click((object) this.diagnoseButton, new EventArgs());
        }
        else
        {
          this.statusPictureBox.Image = (Image) null;
          this.statusPictureBox.Image = (Image) Resources.button_notconnected;
          this.statusPictureBox.Update();
          this.transparentLabel.Text = GlobalResource.MainWindow_MMINotConnected;
          this.transparentLabel.Update();
          if (this.diagnosePanel != null)
          {
            this.diagnosePanel.Dispose();
            this.centerPanel.Controls.Remove((Control) this.diagnosePanel);
            this.diagnosePanel = (DiagnosePanel) null;
          }
          if (this.doNotChageInnerElement)
            return;
          this.removeHighlight();
          this.AddWelcomePanel();
        }
      }
    }

    public void LicenseChangedSafety()
    {
      this.removeHighlight();
      this.AddWelcomePanel();
      this.Testmode = false;
      if (this.settingsPanel != null)
      {
        this.settingsPanel.Dispose();
        this.centerPanel.Controls.Remove((Control) this.settingsPanel);
        this.settingsPanel.Dispose();
        this.settingsPanel = (SettingsPanel) null;
      }
      if (this.diagnosePanel == null)
        return;
      this.diagnosePanel.Dispose();
      this.centerPanel.Controls.Remove((Control) this.diagnosePanel);
      this.diagnosePanel.Dispose();
      this.diagnosePanel = (DiagnosePanel) null;
    }

    public void ForceDiagnosePanelUpdate()
    {
      if (this.diagnosePanel == null)
        return;
      this.diagnosePanel.UpdateFirmwareInformation();
    }

    public bool EnablesUserControl(bool enable)
    {
      this.EnableUserControl = enable;
      return this.userControlEnabled;
    }

    private void InvokeEnableUserControl(object sender, bool enable)
    {
      if (enable)
        this.Cursor = Cursors.Default;
      else
        this.Cursor = Cursors.WaitCursor;
      this.EnablesUserControl(enable);
    }

    private bool EnableUserControl
    {
      set
      {
        if (!this.InvokeRequired)
        {
          if (!this.knowledgeBaseButton.InvokeRequired)
          {
            if (!this.configartionButton.InvokeRequired)
            {
              if (!this.diagnoseButton.InvokeRequired)
              {
                if (!this.helpButton.InvokeRequired)
                {
                  if (!this.settingsButton.InvokeRequired)
                  {
                    if (!this.statusPictureBox.InvokeRequired)
                    {
                      if (!this.transparentLabel.InvokeRequired)
                      {
                        if (!this.centerPanel.InvokeRequired)
                        {
                          this.userControlEnabled = value;
                          if (DisconnectWarning.InstanceCounter() != 0)
                            this.userControlEnabled = false;
                          this.knowledgeBaseButton.Enabled = this.userControlEnabled;
                          this.configartionButton.Enabled = this.userControlEnabled;
                          this.helpButton.Enabled = this.userControlEnabled;
                          this.settingsButton.Enabled = this.userControlEnabled;
                          this.centerPanel.Enabled = this.userControlEnabled;
                          if (this.userControlEnabled)
                            this.diagnoseButton.Enabled = this.MMICom.DeviceConnected;
                          else
                            this.diagnoseButton.Enabled = false;
                          this.statusPictureBox.Refresh();
                          this.transparentLabel.Refresh();
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
        try
        {
          this.Invoke((Delegate) new MainWindow.EnablingUI(this.InvokeEnableUserControl), (object) this, (object) value);
        }
        catch (Exception ex)
        {
          GlobalLogger.Instance.WriteLine(ex);
        }
      }
      get => this.userControlEnabled;
    }

    private void MainWindow_Shown(object sender, EventArgs e)
    {
      if (Program.SPLASH != null)
      {
        Program.SetSplashPercent(100);
        Program.SPLASH.Dispose();
        Program.SPLASH = (SplashScreen) null;
      }
      if (Program.JUST_FOR_TESTING)
      {
        this.mainWindowIsShowing = true;
        int num = (int) new TestDisclaimer().ShowDialog((IWin32Window) this);
        this.logoPictureBox.Focus();
      }
      this.mainWindowIsShowing = true;
    }

    public void UpdateWorkerAccessLevelUpdateHanlder(object update, UpdateWorkerEventArgs args)
    {
      MainWindowEventArgs e = new MainWindowEventArgs();
      e.Done = args.AccessLevelUpdate;
      if (this.AccessLevelUpdate != null)
        this.AccessLevelUpdate((object) this, e);
      this.SetTestmodeAvailability();
    }

    private void SetTestmodeAvailability()
    {
      if (this.logoPictureBox.InvokeRequired)
      {
        try
        {
          this.Invoke((Delegate) new MainWindow.UpdateMenuStripElement(this.SetTestmodeAvailability));
        }
        catch (Exception ex)
        {
          GlobalLogger.Instance.WriteLine(ex);
        }
      }
      else
      {
        if (this.Testmode)
          return;
        if (HelperClass.IsChristmasTime())
          this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo_xmas;
        else if (HelperClass.IsEasterTime())
          this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo_easter;
        else
          this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo;
      }
    }

    public bool Testmode
    {
      set
      {
        this.testmodeAvailability = value;
        if (this.testmodeAvailability)
          this.logoPictureBox.Image = (Image) Resources.testmode_logo;
        else if (HelperClass.IsChristmasTime())
          this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo_xmas;
        else if (HelperClass.IsEasterTime())
          this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo_easter;
        else
          this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo;
      }
      get => this.testmodeAvailability;
    }

    public bool ReadCompleteMMIData { set; get; }

    public bool MMIDataWasCompletelyRead { set; get; }

    public void ReReadNews()
    {
      if (this.welcomePanel != null)
      {
        this.centerPanel.Controls.Remove((Control) this.welcomePanel);
        this.welcomePanel.Dispose();
        this.welcomePanel = (WelcomePanel) null;
        this.AddWelcomePanel();
        HelperClass.DoEvents();
      }
      else
      {
        if (this.knowledgePanel == null)
          return;
        this.centerPanel.Controls.Remove((Control) this.knowledgePanel);
        this.knowledgePanel.Dispose();
        this.knowledgePanel = (KnowledgePanel) null;
        this.removeHighlight();
        this.knowledgeBaseButton_Click((object) this.knowledgeBaseButton, new EventArgs());
        HelperClass.DoEvents();
      }
    }

    public void ReReadFirmwareLists()
    {
      if (this.diagnosePanel != null)
        this.diagnosePanel.UpdateFirmwareInformation();
      if (this.settingsPanel != null && this.Testmode)
        this.settingsPanel.UpdateFirmwareLists();
      if (this.helpPanel == null)
        return;
      this.helpPanel.UpdateFirmwareInformation();
    }

    private void updateWorker_UpdateDone(object sender, UpdateWorkerEventArgs args)
    {
      if (!args.UpdateDone)
        return;
      this.updateWorker.UpdateDone -= new UpdateWorker.UpdateDonedHandler(this.updateWorker_UpdateDone);
      if (Program.ShowUpdateHint)
      {
        if (args.ConnectionError)
        {
          try
          {
            int days = (DateTime.Now - DateTime.ParseExact(ActivationInformation.LicensedDate, "yyyyMMddHHmmssfff", (IFormatProvider) CultureInfo.InvariantCulture)).Days;
            int betweenReactivation = Program.MAX_DAYS_BETWEEN_REACTIVATION;
            int num = (int) MessageBox.Show(GlobalResource.Reactivation_Warning_MessageBox_Message, GlobalResource.Reactivation_Warning_MessageBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          catch (ArgumentNullException ex)
          {
          }
        }
      }
      if (this.updateWorker.DoSelfUpdate)
        HelperClass.SelfUpdateHint();
      if (this.mainWindowIsShowing)
      {
        this.Invoke((MethodInvoker) (() => this.ReReadFirmwareLists()));
        if (this.newNews)
        {
          this.Invoke((MethodInvoker) (() => this.ReReadNews()));
          int num;
          this.Invoke((MethodInvoker) (() => num = (int) new NewsDialog().ShowDialog((IWin32Window) MainWindow.Instance)));
        }
      }
      this.updateWorker = (UpdateWorker) null;
    }

    private void updateWorker_NewsAvailable(object sender, UpdateWorkerEventArgs args)
    {
      if (!args.NewsAvailable)
        return;
      this.newNews = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (this.waitingForUpdateThread)
        return;
      if (disposing && this.components != null)
        this.components.Dispose();
      if (this.mmiThread != null)
        this.mmiThread.Abort();
      if (this.backgroundET != null)
        this.backgroundET.Abort();
      GlobalLogger.Instance.Close();
      Settings.Default.FormHeight = this.Height;
      Settings.Default.FormWidth = this.Width;
      Settings.Default.Save();
      base.Dispose(disposing);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      if (this.captionHeight != SystemInformation.CaptionHeight)
      {
        this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height - this.captionHeight);
        this.captionHeight = SystemInformation.CaptionHeight;
        this.MinimumSize = new Size(this.MinimumSize.Width, this.MinimumSize.Height + this.captionHeight);
      }
      Padding margin = this.centerPanel.Margin;
      this.absoluteMinimumSize = new Size(this.centerPanel.Size.Width - margin.Left - margin.Right, this.centerPanel.Size.Height - margin.Top - margin.Bottom);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainWindow));
      this.leftButtonPanel = new Panel();
      this.logoPictureBox = new PictureBox();
      this.helpButton = new Button();
      this.settingsButton = new Button();
      this.knowledgeBaseButton = new Button();
      this.configartionButton = new Button();
      this.diagnoseButton = new Button();
      this.statusStrip = new StatusStrip();
      this.copyrightToolStripStatusLabel = new ToolStripStatusLabel();
      this.spacerToolStripStatusLabel = new ToolStripStatusLabel();
      this.versionToolStripStatusLabel = new ToolStripStatusLabel();
      this.centerPanel = new Panel();
      this.statusPanel = new Panel();
      this.statusTableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.transparentLabel = new Label();
      this.statusPictureBox = new PictureBox();
      this.leftButtonPanel.SuspendLayout();
      ((ISupportInitialize) this.logoPictureBox).BeginInit();
      this.statusStrip.SuspendLayout();
      this.statusPanel.SuspendLayout();
      this.statusTableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.statusPictureBox).BeginInit();
      this.SuspendLayout();
      this.leftButtonPanel.BackColor = Color.Transparent;
      this.leftButtonPanel.Controls.Add((Control) this.logoPictureBox);
      this.leftButtonPanel.Controls.Add((Control) this.helpButton);
      this.leftButtonPanel.Controls.Add((Control) this.settingsButton);
      this.leftButtonPanel.Controls.Add((Control) this.knowledgeBaseButton);
      this.leftButtonPanel.Controls.Add((Control) this.configartionButton);
      this.leftButtonPanel.Controls.Add((Control) this.diagnoseButton);
      componentResourceManager.ApplyResources((object) this.leftButtonPanel, "leftButtonPanel");
      this.leftButtonPanel.Name = "leftButtonPanel";
      componentResourceManager.ApplyResources((object) this.logoPictureBox, "logoPictureBox");
      this.logoPictureBox.BackColor = Color.Transparent;
      this.logoPictureBox.BorderStyle = BorderStyle.FixedSingle;
      this.logoPictureBox.Image = (Image) Resources.neo_sdiag_logo;
      this.logoPictureBox.Name = "logoPictureBox";
      this.logoPictureBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.helpButton, "helpButton");
      this.helpButton.BackColor = Color.Transparent;
      this.helpButton.Image = (Image) Resources.Icon_Question_active;
      this.helpButton.Name = "helpButton";
      this.helpButton.UseVisualStyleBackColor = false;
      this.helpButton.Click += new EventHandler(this.helpButton_Click);
      componentResourceManager.ApplyResources((object) this.settingsButton, "settingsButton");
      this.settingsButton.BackColor = Color.Transparent;
      this.settingsButton.Image = (Image) Resources.Icon_Settings_active;
      this.settingsButton.Name = "settingsButton";
      this.settingsButton.UseVisualStyleBackColor = false;
      this.settingsButton.Click += new EventHandler(this.settingsButton_Click);
      componentResourceManager.ApplyResources((object) this.knowledgeBaseButton, "knowledgeBaseButton");
      this.knowledgeBaseButton.BackColor = Color.Transparent;
      this.knowledgeBaseButton.Image = (Image) Resources.Icon_News_active;
      this.knowledgeBaseButton.Name = "knowledgeBaseButton";
      this.knowledgeBaseButton.UseVisualStyleBackColor = false;
      this.knowledgeBaseButton.Click += new EventHandler(this.knowledgeBaseButton_Click);
      componentResourceManager.ApplyResources((object) this.configartionButton, "configartionButton");
      this.configartionButton.BackColor = Color.Transparent;
      this.configartionButton.Image = (Image) Resources.Icon_Fahrprofile_active;
      this.configartionButton.Name = "configartionButton";
      this.configartionButton.UseVisualStyleBackColor = false;
      this.configartionButton.Click += new EventHandler(this.configartionButton_Click);
      componentResourceManager.ApplyResources((object) this.diagnoseButton, "diagnoseButton");
      this.diagnoseButton.BackColor = Color.Transparent;
      this.diagnoseButton.Image = (Image) Resources.Icon_DiagnoseStarten_active;
      this.diagnoseButton.Name = "diagnoseButton";
      this.diagnoseButton.UseVisualStyleBackColor = false;
      this.diagnoseButton.Click += new EventHandler(this.diagnoseButton_Click);
      this.statusStrip.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.copyrightToolStripStatusLabel,
        (ToolStripItem) this.spacerToolStripStatusLabel,
        (ToolStripItem) this.versionToolStripStatusLabel
      });
      componentResourceManager.ApplyResources((object) this.statusStrip, "statusStrip");
      this.statusStrip.Name = "statusStrip";
      this.copyrightToolStripStatusLabel.Name = "copyrightToolStripStatusLabel";
      componentResourceManager.ApplyResources((object) this.copyrightToolStripStatusLabel, "copyrightToolStripStatusLabel");
      this.spacerToolStripStatusLabel.Name = "spacerToolStripStatusLabel";
      componentResourceManager.ApplyResources((object) this.spacerToolStripStatusLabel, "spacerToolStripStatusLabel");
      this.spacerToolStripStatusLabel.Spring = true;
      this.versionToolStripStatusLabel.Name = "versionToolStripStatusLabel";
      componentResourceManager.ApplyResources((object) this.versionToolStripStatusLabel, "versionToolStripStatusLabel");
      componentResourceManager.ApplyResources((object) this.centerPanel, "centerPanel");
      this.centerPanel.BackColor = Color.Transparent;
      this.centerPanel.Name = "centerPanel";
      componentResourceManager.ApplyResources((object) this.statusPanel, "statusPanel");
      this.statusPanel.BackColor = Color.Transparent;
      this.statusPanel.Controls.Add((Control) this.statusTableLayoutPanel);
      this.statusPanel.Name = "statusPanel";
      componentResourceManager.ApplyResources((object) this.statusTableLayoutPanel, "statusTableLayoutPanel");
      this.statusTableLayoutPanel.Controls.Add((Control) this.transparentLabel, 0, 0);
      this.statusTableLayoutPanel.Controls.Add((Control) this.statusPictureBox, 1, 0);
      this.statusTableLayoutPanel.Name = "statusTableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.transparentLabel, "transparentLabel");
      this.transparentLabel.BackColor = Color.Transparent;
      this.transparentLabel.Name = "transparentLabel";
      componentResourceManager.ApplyResources((object) this.statusPictureBox, "statusPictureBox");
      this.statusPictureBox.BackColor = Color.Transparent;
      this.statusPictureBox.Image = (Image) Resources.button_connected;
      this.statusPictureBox.Name = "statusPictureBox";
      this.statusPictureBox.TabStop = false;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.statusPanel);
      this.Controls.Add((Control) this.statusStrip);
      this.Controls.Add((Control) this.leftButtonPanel);
      this.Controls.Add((Control) this.centerPanel);
      this.Name = nameof (MainWindow);
      this.Shown += new EventHandler(this.MainWindow_Shown);
      this.leftButtonPanel.ResumeLayout(false);
      ((ISupportInitialize) this.logoPictureBox).EndInit();
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.statusPanel.ResumeLayout(false);
      this.statusTableLayoutPanel.ResumeLayout(false);
      this.statusTableLayoutPanel.PerformLayout();
      ((ISupportInitialize) this.statusPictureBox).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public delegate void HelloLoopPassedEventHandler(object sender, MainWindowEventArgs e);

    public delegate void MMIInformationChangedEventHandler(object sender, EventArgs e);

    public delegate void AccessLevelUpdateHanlder(object sender, MainWindowEventArgs e);

    private delegate void EnablingUI(object sender, bool value);

    private delegate void MMIConnected(bool enable);

    private delegate void UpdateMenuStripElement();
  }
}
