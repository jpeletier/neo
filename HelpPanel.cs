// Decompiled with JetBrains decompiler
// Type: ZerroWare.HelpPanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class HelpPanel : UserControl
  {
    private bool newNews;
    private bool downloadFailed;
    private bool aclUpdate;
    private bool updateDone;
    private IContainer components;
    private TabPage profileTabPage;
    private TabControl tabControl;
    private Panel panel1;
    private FlickerFreeTableLayoutPanel tableLayoutPanel;
    private Label licDurationLabel;
    private Label label2;
    private Label label1;
    private Label licInfoLabel;
    private Label licKeyLabel;
    private Label labelProductName;
    private Label labelVersion;
    private Label labelCopyright;
    private PictureBox spacePictureBox;
    private Button changeLicenseButton;
    private Button showManualButton;
    private Button updateButton;
    private PictureBox pictureBox1;
    private Label mmiFirmwareInfoLabel;
    private Label batteryFirmwareInfoLabel;
    private Label motorFirmwareInfoLabel;
    private Label versionOverview;
    private Label connectMMIFirmwareInfoLabel;

    public HelpPanel()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.tabControl.Font = FontDefinition.MenubarFont;
      this.changeLicenseButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.changeLicenseButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.changeLicenseButton.Font = FontDefinition.DefaultTextFont;
      this.showManualButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.showManualButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.showManualButton.Font = FontDefinition.DefaultTextFont;
      this.updateButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.updateButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.updateButton.Font = FontDefinition.DefaultTextFont;
      ComponentResourceManager resources = new ComponentResourceManager(typeof (HelpPanel));
      this.Text = string.Format("{0}", (object) this.AssemblyTitle);
      this.labelProductName.Text = this.AssemblyProduct;
      this.labelVersion.Text = string.Format("{0} {1}", (object) resources.GetString("labelVersion.Text"), (object) this.AssemblyVersion) + "-" + (object) Program.NEO_SDIAG_BUILD_NUMBER;
      this.labelCopyright.Text = this.AssemblyCopyright;
      this.licKeyLabel.Text = ActivationInformation.LicenseKey();
      this.licInfoLabel.Text = ActivationInformation.AdditionalInformation();
      try
      {
        int days = (DateTime.Now - DateTime.ParseExact(ActivationInformation.LicensedDate, "yyyyMMddHHmmssfff", (IFormatProvider) CultureInfo.InvariantCulture)).Days;
        int num = Program.MAX_DAYS_BETWEEN_REACTIVATION - days;
        this.licDurationLabel.Text = string.Format(resources.GetString("licDurationLabel.Text"), (object) num);
      }
      catch (Exception ex)
      {
        this.licDurationLabel.Text = "";
      }
      this.spacePictureBox.Image = (Image) BackgroundImages.LightGrayGradient;
      this.label1.Font = FontDefinition.DefaultBoldTextFont;
      this.label2.Font = FontDefinition.DefaultBoldTextFont;
      this.licDurationLabel.Font = FontDefinition.DefaultTextFont;
      this.versionOverview.Font = FontDefinition.DefaultBoldTextFont;
      this.SetLatestVersionInformation(resources);
    }

    private void HelpPanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.Parent == null || !(this.Size != this.Parent.Size))
        return;
      this.Size = this.Parent.Size;
    }

    public string AssemblyTitle
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyTitleAttribute), false);
        if (customAttributes.Length > 0)
        {
          AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute) customAttributes[0];
          if (assemblyTitleAttribute.Title != "")
            return assemblyTitleAttribute.Title;
        }
        return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

    public string AssemblyProduct
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyProductAttribute), false);
        return customAttributes.Length == 0 ? "" : ((AssemblyProductAttribute) customAttributes[0]).Product;
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof (AssemblyCopyrightAttribute), false);
        return customAttributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute) customAttributes[0]).Copyright;
      }
    }

    private void showManualButton_Click(object sender, EventArgs e)
    {
      string manualFileName = Directories.Instance.ManualFileName;
      try
      {
        if (string.IsNullOrEmpty(manualFileName) || !File.Exists(manualFileName))
          return;
        this.Cursor = Cursors.WaitCursor;
        if (CheckForDefaults.PdfReaderAssociated())
        {
          Process.Start(manualFileName);
          this.Cursor = Cursors.Default;
        }
        else
        {
          this.Cursor = Cursors.Default;
          int num = (int) MessageBox.Show(GlobalResource.PdfReader_Not_Available_Message, GlobalResource.PdfReader_Not_Available_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void changeLicenseButton_Click(object sender, EventArgs e)
    {
      if (new LicenseKeyDialog().ShowDialog() != DialogResult.OK)
        return;
      HelperClass.DoEvents();
      MainWindow.Instance.EnablesUserControl(false);
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      Thread thread1 = new Thread(new ThreadStart(this.showPleaseWait));
      thread1.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread1.Start();
      Thread thread2;
      if (new Activation().DoActivation())
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (HelpPanel));
        this.licKeyLabel.Text = ActivationInformation.LicenseKey();
        this.licInfoLabel.Text = ActivationInformation.AdditionalInformation();
        try
        {
          this.disposePleaseWait();
          HelperClass.DoEvents();
          thread1.Abort();
          thread2 = (Thread) null;
        }
        catch (Exception ex)
        {
        }
        MainWindow.Instance.Cursor = Cursors.Default;
        try
        {
          int days = (DateTime.Now - DateTime.ParseExact(ActivationInformation.LicensedDate, "yyyyMMddHHmmssfff", (IFormatProvider) CultureInfo.InvariantCulture)).Days;
          int num = Program.MAX_DAYS_BETWEEN_REACTIVATION - days;
          this.licDurationLabel.Text = string.Format(componentResourceManager.GetString("licDurationLabel.Text"), (object) num);
        }
        catch (Exception ex)
        {
          this.licDurationLabel.Text = "";
        }
        MainWindow.Instance.LicenseChangedSafety();
      }
      else
      {
        new Activation().WasActivated();
        try
        {
          HelperClass.DoEvents();
          if (this.WaitingDialog != null)
          {
            if (this.WaitingDialog.InvokeRequired)
            {
              this.Invoke((MethodInvoker) (() => this.WaitingDialog.Dispose()));
              this.WaitingDialog = (PleaseWait) null;
            }
            else
            {
              this.WaitingDialog.Dispose();
              this.WaitingDialog = (PleaseWait) null;
            }
          }
          thread1.Abort();
          HelperClass.DoEvents();
          thread2 = (Thread) null;
        }
        catch (Exception ex)
        {
        }
        MainWindow.Instance.Cursor = Cursors.Default;
        int num = (int) MessageBox.Show(GlobalResource.AboutBox_MessageBox_InvalidLicense_Message, GlobalResource.AboutBox_MessageBox_InvalidLicense_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      MainWindow.Instance.EnablesUserControl(true);
      MainWindow.Instance.Enabled = true;
      this.changeLicenseButton.Focus();
    }

    private PleaseWait WaitingDialog { set; get; }

    private void showPleaseWait()
    {
      try
      {
        this.WaitingDialog = new PleaseWait((Form) MainWindow.Instance);
        int num = (int) this.WaitingDialog.ShowDialog();
      }
      catch (ThreadAbortException ex)
      {
      }
    }

    private void disposePleaseWait()
    {
      if (this.WaitingDialog == null)
        return;
      if (this.WaitingDialog.InvokeRequired)
      {
        this.Invoke((MethodInvoker) (() => this.WaitingDialog.Dispose()));
        this.WaitingDialog = (PleaseWait) null;
      }
      else
      {
        this.WaitingDialog.Dispose();
        this.WaitingDialog = (PleaseWait) null;
      }
    }

    private void updateButton_Click(object sender, EventArgs e)
    {
      MainWindow.Instance.EnablesUserControl(false);
      this.Enabled = false;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      Thread thread = new Thread(new ThreadStart(this.showPleaseWait));
      thread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
      thread.Start();
      this.newNews = false;
      this.downloadFailed = false;
      this.aclUpdate = false;
      this.updateDone = false;
      UpdateWorker updateWorker = new UpdateWorker();
      updateWorker.NewsAvailable += new UpdateWorker.NewsAvailableHandler(this.updateWorker_NewsAvailable);
      updateWorker.ConnectionError += new UpdateWorker.ConnectionErrorHandler(this.updateWorker_ConnectionError);
      updateWorker.AccessLevelUpdate += new UpdateWorker.AccessLevelUpdateHanlder(this.updateWorker_AccessLevelUpdate);
      updateWorker.UpdateDone += new UpdateWorker.UpdateDonedHandler(this.updateWorker_UpdateDone);
      int versionLevelProperty = ActivationInformation.VersionLevelProperty;
      updateWorker.DoUpdate();
      this.disposePleaseWait();
      HelperClass.DoEvents();
      thread.Abort();
      this.Enabled = true;
      MainWindow.Instance.EnablesUserControl(true);
      MainWindow.Instance.Cursor = Cursors.Default;
      if (this.downloadFailed)
      {
        int num1 = (int) MessageBox.Show((IWin32Window) MainWindow.Instance, GlobalResource.HelpPanel_UpdateFailedMessage, GlobalResource.HelpPanel_UpdateCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else if (!this.downloadFailed)
      {
        int num2 = (int) MessageBox.Show((IWin32Window) MainWindow.Instance, GlobalResource.HelpPanel_UpdateSuccessMessage, GlobalResource.HelpPanel_UpdateCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      if (updateWorker.DoSelfUpdate)
        HelperClass.SelfUpdateHint();
      if (this.updateDone)
        MainWindow.Instance.ReReadFirmwareLists();
      if (this.newNews)
      {
        MainWindow.Instance.ReReadNews();
        int num3 = (int) new NewsDialog().ShowDialog((IWin32Window) MainWindow.Instance);
      }
      this.updateButton.Focus();
      updateWorker.AccessLevelUpdate -= new UpdateWorker.AccessLevelUpdateHanlder(this.updateWorker_AccessLevelUpdate);
      updateWorker.NewsAvailable -= new UpdateWorker.NewsAvailableHandler(this.updateWorker_NewsAvailable);
      updateWorker.ConnectionError -= new UpdateWorker.ConnectionErrorHandler(this.updateWorker_ConnectionError);
      updateWorker.UpdateDone -= new UpdateWorker.UpdateDonedHandler(this.updateWorker_UpdateDone);
      if (!this.aclUpdate || versionLevelProperty == ActivationInformation.VersionLevelProperty)
        return;
      MainWindow.Instance.LicenseChangedSafety();
    }

    private void updateWorker_UpdateDone(object sender, UpdateWorkerEventArgs e)
    {
      if (!e.UpdateDone)
        return;
      this.updateDone = true;
    }

    private void updateWorker_AccessLevelUpdate(object sender, UpdateWorkerEventArgs args)
    {
      if (!args.AccessLevelUpdate)
        return;
      this.aclUpdate = true;
    }

    private void updateWorker_NewsAvailable(object sender, UpdateWorkerEventArgs args)
    {
      if (!args.NewsAvailable)
        return;
      this.newNews = true;
    }

    private void updateWorker_ConnectionError(object sender, UpdateWorkerEventArgs args)
    {
      if (!args.ConnectionError)
        return;
      this.downloadFailed = true;
    }

    private void SetLatestVersionInformation(ComponentResourceManager resources)
    {
      LatestVersionsFile latestVersionsFile = LatestVersionsFile.ReadFile();
      foreach (LatestVersionsFile.Element element in Enum.GetValues(typeof (LatestVersionsFile.Element)))
      {
        switch (element)
        {
          case LatestVersionsFile.Element.MOTOR:
            this.motorFirmwareInfoLabel.Text = string.Format(resources.GetString("motorFirmwareInfoLabel.Text"), (object) latestVersionsFile.ReadableVersionID((int) element));
            continue;
          case LatestVersionsFile.Element.ACCU:
            this.batteryFirmwareInfoLabel.Text = string.Format(resources.GetString("batteryFirmwareInfoLabel.Text"), (object) latestVersionsFile.ReadableVersionID((int) element));
            continue;
          case LatestVersionsFile.Element.MMI:
            this.mmiFirmwareInfoLabel.Text = string.Format(resources.GetString("mmiFirmwareInfoLabel.Text"), (object) latestVersionsFile.ReadableVersionID((int) element));
            continue;
          case LatestVersionsFile.Element.CONNECT_MMI:
            this.connectMMIFirmwareInfoLabel.Text = string.Format(resources.GetString("connectMMIFirmwareInfoLabel.Text"), (object) latestVersionsFile.ReadableVersionID((int) element));
            continue;
          default:
            continue;
        }
      }
    }

    public void UpdateFirmwareInformation()
    {
      this.SetLatestVersionInformation(new ComponentResourceManager(typeof (HelpPanel)));
      HelperClass.DoEvents();
    }

    protected override void Dispose(bool disposing)
    {
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (HelpPanel));
      this.profileTabPage = new TabPage();
      this.updateButton = new Button();
      this.showManualButton = new Button();
      this.changeLicenseButton = new Button();
      this.panel1 = new Panel();
      this.tableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.licDurationLabel = new Label();
      this.label2 = new Label();
      this.label1 = new Label();
      this.licInfoLabel = new Label();
      this.licKeyLabel = new Label();
      this.labelProductName = new Label();
      this.versionOverview = new Label();
      this.labelVersion = new Label();
      this.labelCopyright = new Label();
      this.spacePictureBox = new PictureBox();
      this.pictureBox1 = new PictureBox();
      this.mmiFirmwareInfoLabel = new Label();
      this.batteryFirmwareInfoLabel = new Label();
      this.motorFirmwareInfoLabel = new Label();
      this.tabControl = new TabControl();
      this.connectMMIFirmwareInfoLabel = new Label();
      this.profileTabPage.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tableLayoutPanel.SuspendLayout();
      ((ISupportInitialize) this.spacePictureBox).BeginInit();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.tabControl.SuspendLayout();
      this.SuspendLayout();
      this.profileTabPage.BackColor = Color.White;
      this.profileTabPage.Controls.Add((Control) this.updateButton);
      this.profileTabPage.Controls.Add((Control) this.showManualButton);
      this.profileTabPage.Controls.Add((Control) this.changeLicenseButton);
      this.profileTabPage.Controls.Add((Control) this.panel1);
      this.profileTabPage.ForeColor = SystemColors.ControlText;
      componentResourceManager.ApplyResources((object) this.profileTabPage, "profileTabPage");
      this.profileTabPage.Name = "profileTabPage";
      componentResourceManager.ApplyResources((object) this.updateButton, "updateButton");
      this.updateButton.Name = "updateButton";
      this.updateButton.UseVisualStyleBackColor = true;
      this.updateButton.Click += new EventHandler(this.updateButton_Click);
      componentResourceManager.ApplyResources((object) this.showManualButton, "showManualButton");
      this.showManualButton.Name = "showManualButton";
      this.showManualButton.UseVisualStyleBackColor = true;
      this.showManualButton.Click += new EventHandler(this.showManualButton_Click);
      componentResourceManager.ApplyResources((object) this.changeLicenseButton, "changeLicenseButton");
      this.changeLicenseButton.Name = "changeLicenseButton";
      this.changeLicenseButton.UseVisualStyleBackColor = true;
      this.changeLicenseButton.Click += new EventHandler(this.changeLicenseButton_Click);
      this.panel1.Controls.Add((Control) this.tableLayoutPanel);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.tableLayoutPanel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
      this.tableLayoutPanel.Controls.Add((Control) this.licDurationLabel, 1, 11);
      this.tableLayoutPanel.Controls.Add((Control) this.label2, 1, 9);
      this.tableLayoutPanel.Controls.Add((Control) this.label1, 1, 5);
      this.tableLayoutPanel.Controls.Add((Control) this.licInfoLabel, 1, 10);
      this.tableLayoutPanel.Controls.Add((Control) this.licKeyLabel, 1, 6);
      this.tableLayoutPanel.Controls.Add((Control) this.labelProductName, 1, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.versionOverview, 1, 13);
      this.tableLayoutPanel.Controls.Add((Control) this.labelVersion, 1, 1);
      this.tableLayoutPanel.Controls.Add((Control) this.labelCopyright, 1, 2);
      this.tableLayoutPanel.Controls.Add((Control) this.spacePictureBox, 1, 4);
      this.tableLayoutPanel.Controls.Add((Control) this.pictureBox1, 0, 0);
      this.tableLayoutPanel.Controls.Add((Control) this.mmiFirmwareInfoLabel, 1, 16);
      this.tableLayoutPanel.Controls.Add((Control) this.batteryFirmwareInfoLabel, 1, 14);
      this.tableLayoutPanel.Controls.Add((Control) this.motorFirmwareInfoLabel, 1, 15);
      this.tableLayoutPanel.Controls.Add((Control) this.connectMMIFirmwareInfoLabel, 1, 17);
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.licDurationLabel, "licDurationLabel");
      this.licDurationLabel.Name = "licDurationLabel";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.licInfoLabel, "licInfoLabel");
      this.licInfoLabel.Name = "licInfoLabel";
      componentResourceManager.ApplyResources((object) this.licKeyLabel, "licKeyLabel");
      this.licKeyLabel.Name = "licKeyLabel";
      componentResourceManager.ApplyResources((object) this.labelProductName, "labelProductName");
      this.labelProductName.Name = "labelProductName";
      componentResourceManager.ApplyResources((object) this.versionOverview, "versionOverview");
      this.versionOverview.BackColor = Color.Transparent;
      this.versionOverview.Name = "versionOverview";
      componentResourceManager.ApplyResources((object) this.labelVersion, "labelVersion");
      this.labelVersion.Name = "labelVersion";
      componentResourceManager.ApplyResources((object) this.labelCopyright, "labelCopyright");
      this.labelCopyright.Name = "labelCopyright";
      componentResourceManager.ApplyResources((object) this.spacePictureBox, "spacePictureBox");
      this.spacePictureBox.Name = "spacePictureBox";
      this.spacePictureBox.TabStop = false;
      this.pictureBox1.Image = (Image) Resources.SmartDiagnostic;
      componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
      this.pictureBox1.Name = "pictureBox1";
      this.tableLayoutPanel.SetRowSpan((Control) this.pictureBox1, 11);
      this.pictureBox1.TabStop = false;
      componentResourceManager.ApplyResources((object) this.mmiFirmwareInfoLabel, "mmiFirmwareInfoLabel");
      this.mmiFirmwareInfoLabel.BackColor = Color.Transparent;
      this.mmiFirmwareInfoLabel.Name = "mmiFirmwareInfoLabel";
      componentResourceManager.ApplyResources((object) this.batteryFirmwareInfoLabel, "batteryFirmwareInfoLabel");
      this.batteryFirmwareInfoLabel.BackColor = Color.Transparent;
      this.batteryFirmwareInfoLabel.Name = "batteryFirmwareInfoLabel";
      componentResourceManager.ApplyResources((object) this.motorFirmwareInfoLabel, "motorFirmwareInfoLabel");
      this.motorFirmwareInfoLabel.BackColor = Color.Transparent;
      this.motorFirmwareInfoLabel.Name = "motorFirmwareInfoLabel";
      componentResourceManager.ApplyResources((object) this.tabControl, "tabControl");
      this.tabControl.Controls.Add((Control) this.profileTabPage);
      this.tabControl.Cursor = Cursors.Default;
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      componentResourceManager.ApplyResources((object) this.connectMMIFirmwareInfoLabel, "connectMMIFirmwareInfoLabel");
      this.connectMMIFirmwareInfoLabel.BackColor = Color.Transparent;
      this.connectMMIFirmwareInfoLabel.Name = "connectMMIFirmwareInfoLabel";
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.tabControl);
      this.DoubleBuffered = true;
      this.Name = nameof (HelpPanel);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Layout += new LayoutEventHandler(this.HelpPanel_Layout);
      this.profileTabPage.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
      ((ISupportInitialize) this.spacePictureBox).EndInit();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.tabControl.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
