// Decompiled with JetBrains decompiler
// Type: ZerroWare.FirstStartDialog
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class FirstStartDialog : Form
  {
    private IContainer components;
    private Label label1;
    private ComboBox comboBox1;
    private Label label2;
    private Button keyButton;
    private Button closeButton;
    private Button proxyButton;
    private CultureInfo ci;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FirstStartDialog));
      this.label1 = new Label();
      this.comboBox1 = new ComboBox();
      this.label2 = new Label();
      this.keyButton = new Button();
      this.closeButton = new Button();
      this.proxyButton = new Button();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.BackColor = Color.Transparent;
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.comboBox1, "comboBox1");
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[2]
      {
        (object) componentResourceManager.GetString("comboBox1.Items"),
        (object) componentResourceManager.GetString("comboBox1.Items1")
      });
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Sorted = true;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.BackColor = Color.Transparent;
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.keyButton, "keyButton");
      this.keyButton.Name = "keyButton";
      this.keyButton.UseVisualStyleBackColor = true;
      this.keyButton.Click += new EventHandler(this.keyButton_Click);
      componentResourceManager.ApplyResources((object) this.closeButton, "closeButton");
      this.closeButton.DialogResult = DialogResult.Cancel;
      this.closeButton.Name = "closeButton";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new EventHandler(this.closeButton_Click);
      componentResourceManager.ApplyResources((object) this.proxyButton, "proxyButton");
      this.proxyButton.Name = "proxyButton";
      this.proxyButton.UseVisualStyleBackColor = true;
      this.proxyButton.Click += new EventHandler(this.proxyButton_Click);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      this.CancelButton = (IButtonControl) this.closeButton;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.proxyButton);
      this.Controls.Add((Control) this.closeButton);
      this.Controls.Add((Control) this.keyButton);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (FirstStartDialog);
      this.FormClosing += new FormClosingEventHandler(this.FirstStartDialog_FormClosing);
      this.ResumeLayout(false);
    }

    public FirstStartDialog()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.Font = FontDefinition.DefaultTextFont;
      this.label1.Font = FontDefinition.InfoLineLabelFont;
      this.label2.Font = FontDefinition.DefaultTextFont;
      this.keyButton.Font = FontDefinition.DefaultTextFont;
      this.closeButton.Font = FontDefinition.DefaultTextFont;
      this.proxyButton.Font = FontDefinition.DefaultTextFont;
      this.comboBox1.Font = FontDefinition.DefaultTextFont;
      this.closeButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.proxyButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.keyButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.closeButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.proxyButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.keyButton.BackgroundImageLayout = ImageLayout.Stretch;
      CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
      if (!string.IsNullOrEmpty(Settings.Default.Language))
      {
        cultureInfo = new CultureInfo(Settings.Default.Language);
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
      }
      this.ActualCultureInfo = cultureInfo;
      switch (cultureInfo.ToString())
      {
        case "en-US":
          this.comboBox1.SelectedItem = (object) "English";
          break;
        case "de-DE":
          this.comboBox1.SelectedItem = (object) "Deutsch";
          break;
        default:
          this.comboBox1.SelectedItem = (object) "English";
          break;
      }
      if (Program.SPLASH == null)
        return;
      Program.SPLASH.Dispose();
      Program.SPLASH = (SplashScreen) null;
    }

    public CultureInfo ActualCultureInfo
    {
      set => this.ci = value;
      get => this.ci;
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FirstStartDialog));
      if ((string) this.comboBox1.SelectedItem == "Deutsch")
      {
        CultureInfo cultureInfo = new CultureInfo("de-De");
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        this.ActualCultureInfo = cultureInfo;
      }
      else
      {
        CultureInfo cultureInfo = new CultureInfo("en-Us");
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        this.ActualCultureInfo = cultureInfo;
      }
      this.Text = componentResourceManager.GetString("$this.Text");
      this.label1.Text = componentResourceManager.GetString("label1.Text");
      this.label2.Text = componentResourceManager.GetString("label2.Text");
      this.keyButton.Text = componentResourceManager.GetString("keyButton.Text");
      this.closeButton.Text = componentResourceManager.GetString("closeButton.Text");
      this.proxyButton.Text = componentResourceManager.GetString("proxyButton.Text");
      Settings.Default.Language = Thread.CurrentThread.CurrentUICulture.ToString();
      Settings.Default.Save();
    }

    private void keyButton_Click(object sender, EventArgs e)
    {
            return;
      ActivationInformation.SetLicenseKey("");
      int num = (int) new LicenseKeyDialog().ShowDialog((IWin32Window) this);
      this.Dispose();
    }

    private void closeButton_Click(object sender, EventArgs e) => this.ExitApplication();

    private void proxyButton_Click(object sender, EventArgs e)
    {
      if (new ProxySettings().ShowDialog() != DialogResult.OK)
        return;
      GlobalLogger.Instance.Close();
      Process.Start(Application.ExecutablePath);
      Environment.Exit(0);
    }

    private void FirstStartDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (e.CloseReason != CloseReason.UserClosing)
        return;
      this.ExitApplication();
    }

    private void ExitApplication()
    {
      GlobalLogger.Instance.Close();
      Environment.Exit(0);
    }
  }
}
