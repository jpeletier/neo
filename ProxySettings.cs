// Decompiled with JetBrains decompiler
// Type: ZerroWare.ProxySettings
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class ProxySettings : Form
  {
    private IContainer components;
    private GroupBox proxyGroupBox;
    private NumericUpDown portTextBox;
    private Label portLabel;
    private TextBox httpProxyTextBox;
    private Label httpProxyLabel;
    private RadioButton manualProxyRadioButton;
    private RadioButton systemProxyRadioButton;
    private RadioButton noProxyRadioButton;
    private RadioButton actualUserRadioButton;
    private RadioButton noAuthenticationRadioButton;
    private TextBox domainTextBox;
    private Label domainLabel;
    private TextBox passwordTextBox;
    private Label passwordLabel;
    private TextBox userNameTextBox;
    private Label userNameLabel;
    private RadioButton otherUserRadioButton;
    private Button okButton;
    private Button cancelButton;
    private GroupBox userGroupBox;

    private WebSettings.AuthType TypeOfAuth { set; get; }

    private WebSettings.ProxyType TypeOfProxy { set; get; }

    public ProxySettings()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.DefaultUISettings();
      this.userGroupBox.Enabled = false;
      this.httpProxyTextBox.Enabled = false;
      this.portTextBox.Enabled = false;
      this.portTextBox.Leave += new EventHandler(this.numericUpDown_Leave);
      this.userNameTextBox.Enabled = false;
      this.passwordTextBox.Enabled = false;
      this.domainTextBox.Enabled = false;
      WebSettings webSettings = WebSettings.ReadFile();
      this.portTextBox.Value = (Decimal) webSettings.Port;
      this.httpProxyTextBox.Text = webSettings.ProxyURL;
      this.userNameTextBox.Text = webSettings.UserName;
      this.passwordTextBox.Text = webSettings.Password;
      this.domainTextBox.Text = webSettings.Domain;
      this.TypeOfProxy = webSettings.TypeOfProxy;
      this.TypeOfAuth = webSettings.TypeOfAuth;
      this.ConvertTypesToCheckStates();
    }

    private void DefaultUISettings()
    {
      this.proxyGroupBox.Font = FontDefinition.TableCaptionTextFont;
      this.userGroupBox.Font = FontDefinition.TableCaptionTextFont;
      this.portTextBox.Font = FontDefinition.DefaultTextFont;
      this.portLabel.Font = FontDefinition.DefaultTextFont;
      this.httpProxyTextBox.Font = FontDefinition.DefaultTextFont;
      this.httpProxyLabel.Font = FontDefinition.DefaultTextFont;
      this.manualProxyRadioButton.Font = FontDefinition.DefaultTextFont;
      this.systemProxyRadioButton.Font = FontDefinition.DefaultTextFont;
      this.noProxyRadioButton.Font = FontDefinition.DefaultTextFont;
      this.actualUserRadioButton.Font = FontDefinition.DefaultTextFont;
      this.noAuthenticationRadioButton.Font = FontDefinition.DefaultTextFont;
      this.domainTextBox.Font = FontDefinition.DefaultTextFont;
      this.domainLabel.Font = FontDefinition.DefaultTextFont;
      this.passwordTextBox.Font = FontDefinition.DefaultTextFont;
      this.passwordLabel.Font = FontDefinition.DefaultTextFont;
      this.userNameTextBox.Font = FontDefinition.DefaultTextFont;
      this.userNameLabel.Font = FontDefinition.DefaultTextFont;
      this.otherUserRadioButton.Font = FontDefinition.DefaultTextFont;
      this.okButton.Font = FontDefinition.DefaultTextFont;
      this.cancelButton.Font = FontDefinition.DefaultTextFont;
      this.proxyGroupBox.ForeColor = ColorDefinition.BlackTextColor;
      this.userGroupBox.ForeColor = ColorDefinition.BlackTextColor;
      this.portTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.portLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.httpProxyTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.httpProxyLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.manualProxyRadioButton.ForeColor = ColorDefinition.BlackTextColor;
      this.systemProxyRadioButton.ForeColor = ColorDefinition.BlackTextColor;
      this.noProxyRadioButton.ForeColor = ColorDefinition.BlackTextColor;
      this.actualUserRadioButton.ForeColor = ColorDefinition.BlackTextColor;
      this.noAuthenticationRadioButton.ForeColor = ColorDefinition.BlackTextColor;
      this.domainTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.domainLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.passwordTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.passwordLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.userNameTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.userNameLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.otherUserRadioButton.ForeColor = ColorDefinition.BlackTextColor;
      this.okButton.ForeColor = ColorDefinition.BlackTextColor;
      this.cancelButton.ForeColor = ColorDefinition.BlackTextColor;
      this.okButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.okButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.cancelButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.cancelButton.BackgroundImageLayout = ImageLayout.Stretch;
    }

    private void proxyRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      switch (((Control) sender).Name)
      {
        case "manualProxyRadioButton":
          this.userGroupBox.Enabled = true;
          this.httpProxyTextBox.Enabled = true;
          this.portTextBox.Enabled = true;
          break;
        default:
          this.userGroupBox.Enabled = false;
          this.httpProxyTextBox.Enabled = false;
          this.portTextBox.Enabled = false;
          break;
      }
    }

    private void userRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      switch (((Control) sender).Name)
      {
        case "otherUserRadioButton":
          this.userNameTextBox.Enabled = true;
          this.passwordTextBox.Enabled = true;
          this.domainTextBox.Enabled = true;
          break;
        default:
          this.userNameTextBox.Enabled = false;
          this.passwordTextBox.Enabled = false;
          this.domainTextBox.Enabled = false;
          break;
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.ConvertCheckStatesToTypes();
      if (!this.ValidUri(this.httpProxyTextBox.Text))
      {
        int num1 = (int) MessageBox.Show(GlobalResource.ProxySettings_Invalid_Uri_Message, GlobalResource.ProxySettings_Invalid_Uri_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else if (!this.ValidCredential())
      {
        int num2 = (int) MessageBox.Show(GlobalResource.ProxySettings_Invalid_Credential_Message, GlobalResource.ProxySettings_Invalid_Credential_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        WebSettings content = new WebSettings();
        content.ProxyURL = this.httpProxyTextBox.Text;
        content.Port = (int) this.portTextBox.Value;
        content.UserName = this.userNameTextBox.Text;
        content.Password = this.passwordTextBox.Text;
        content.Domain = this.domainTextBox.Text;
        content.TypeOfProxy = this.TypeOfProxy;
        content.TypeOfAuth = this.TypeOfAuth;
        try
        {
          WebSettings.WriteFile(content);
        }
        catch (Exception ex)
        {
          UniqueError.Message(UniqueError.Number.PROXYSETTINGS_WRITE_ERROR);
          return;
        }
        this.DialogResult = DialogResult.OK;
        this.Dispose();
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Dispose();
    }

    private void ConvertCheckStatesToTypes()
    {
      if (this.manualProxyRadioButton.Checked)
      {
        this.TypeOfProxy = WebSettings.ProxyType.MANUAL_PROXY;
        if (this.otherUserRadioButton.Checked)
          this.TypeOfAuth = WebSettings.AuthType.GIVEN_USER;
        else if (this.actualUserRadioButton.Checked)
        {
          this.TypeOfAuth = WebSettings.AuthType.ACTUAL_USER;
        }
        else
        {
          if (!this.noAuthenticationRadioButton.Checked)
            return;
          this.TypeOfAuth = WebSettings.AuthType.NO_AUTH;
        }
      }
      else if (this.systemProxyRadioButton.Checked)
      {
        this.TypeOfProxy = WebSettings.ProxyType.DEFAULT_PROXY;
        this.TypeOfAuth = WebSettings.AuthType.NO_AUTH;
      }
      else
      {
        if (!this.noProxyRadioButton.Checked)
          return;
        this.TypeOfProxy = WebSettings.ProxyType.NO_PROXY;
        this.TypeOfAuth = WebSettings.AuthType.NO_AUTH;
      }
    }

    private void ConvertTypesToCheckStates()
    {
      switch (this.TypeOfProxy)
      {
        case WebSettings.ProxyType.NO_PROXY:
          this.noProxyRadioButton.Checked = true;
          break;
        case WebSettings.ProxyType.DEFAULT_PROXY:
          this.systemProxyRadioButton.Checked = true;
          break;
        case WebSettings.ProxyType.MANUAL_PROXY:
          this.manualProxyRadioButton.Checked = true;
          this.userGroupBox.Enabled = true;
          switch (this.TypeOfAuth)
          {
            case WebSettings.AuthType.NO_AUTH:
              this.noAuthenticationRadioButton.Checked = true;
              return;
            case WebSettings.AuthType.ACTUAL_USER:
              this.actualUserRadioButton.Checked = true;
              return;
            case WebSettings.AuthType.GIVEN_USER:
              this.otherUserRadioButton.Checked = true;
              return;
            default:
              this.noAuthenticationRadioButton.Checked = true;
              return;
          }
        default:
          this.noProxyRadioButton.Checked = true;
          break;
      }
    }

    private bool ValidUri(string toProof)
    {
      if (!string.IsNullOrEmpty(toProof))
      {
        if (this.manualProxyRadioButton.Checked)
        {
          try
          {
            string empty = string.Empty;
            string str = toProof.StartsWith("http://") ? toProof : "http://" + toProof;
            if (str.EndsWith("/"))
              str = str.Substring(0, str.Length - 1);
            Uri uri = new Uri(str + ":" + (object) this.portTextBox.Value + "/");
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return true;
    }

    private bool ValidCredential()
    {
      if (this.manualProxyRadioButton.Checked)
      {
        if (this.otherUserRadioButton.Checked)
        {
          try
          {
            if (!string.IsNullOrEmpty(this.userNameTextBox.Text) && !string.IsNullOrEmpty(this.passwordTextBox.Text) && string.IsNullOrEmpty(this.domainTextBox.Text))
            {
              NetworkCredential networkCredential1 = new NetworkCredential(this.userNameTextBox.Text, this.passwordTextBox.Text);
            }
            else
            {
              if (string.IsNullOrEmpty(this.userNameTextBox.Text) || string.IsNullOrEmpty(this.passwordTextBox.Text) || string.IsNullOrEmpty(this.domainTextBox.Text) || !this.ValidUri(this.domainTextBox.Text))
                return false;
              NetworkCredential networkCredential2 = new NetworkCredential(this.userNameTextBox.Text, this.passwordTextBox.Text, this.domainTextBox.Text);
            }
          }
          catch (Exception ex)
          {
            return false;
          }
        }
      }
      return true;
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProxySettings));
      this.proxyGroupBox = new GroupBox();
      this.userGroupBox = new GroupBox();
      this.noAuthenticationRadioButton = new RadioButton();
      this.actualUserRadioButton = new RadioButton();
      this.otherUserRadioButton = new RadioButton();
      this.userNameLabel = new Label();
      this.userNameTextBox = new TextBox();
      this.passwordLabel = new Label();
      this.passwordTextBox = new TextBox();
      this.domainLabel = new Label();
      this.domainTextBox = new TextBox();
      this.portTextBox = new NumericUpDown();
      this.portLabel = new Label();
      this.httpProxyTextBox = new TextBox();
      this.httpProxyLabel = new Label();
      this.manualProxyRadioButton = new RadioButton();
      this.systemProxyRadioButton = new RadioButton();
      this.noProxyRadioButton = new RadioButton();
      this.okButton = new Button();
      this.cancelButton = new Button();
      this.proxyGroupBox.SuspendLayout();
      this.userGroupBox.SuspendLayout();
      this.portTextBox.BeginInit();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.proxyGroupBox, "proxyGroupBox");
      this.proxyGroupBox.BackColor = Color.Transparent;
      this.proxyGroupBox.Controls.Add((Control) this.userGroupBox);
      this.proxyGroupBox.Controls.Add((Control) this.portTextBox);
      this.proxyGroupBox.Controls.Add((Control) this.portLabel);
      this.proxyGroupBox.Controls.Add((Control) this.httpProxyTextBox);
      this.proxyGroupBox.Controls.Add((Control) this.httpProxyLabel);
      this.proxyGroupBox.Controls.Add((Control) this.manualProxyRadioButton);
      this.proxyGroupBox.Controls.Add((Control) this.systemProxyRadioButton);
      this.proxyGroupBox.Controls.Add((Control) this.noProxyRadioButton);
      this.proxyGroupBox.Name = "proxyGroupBox";
      this.proxyGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.userGroupBox, "userGroupBox");
      this.userGroupBox.Controls.Add((Control) this.noAuthenticationRadioButton);
      this.userGroupBox.Controls.Add((Control) this.actualUserRadioButton);
      this.userGroupBox.Controls.Add((Control) this.otherUserRadioButton);
      this.userGroupBox.Controls.Add((Control) this.userNameLabel);
      this.userGroupBox.Controls.Add((Control) this.userNameTextBox);
      this.userGroupBox.Controls.Add((Control) this.passwordLabel);
      this.userGroupBox.Controls.Add((Control) this.passwordTextBox);
      this.userGroupBox.Controls.Add((Control) this.domainLabel);
      this.userGroupBox.Controls.Add((Control) this.domainTextBox);
      this.userGroupBox.Name = "userGroupBox";
      this.userGroupBox.TabStop = false;
      componentResourceManager.ApplyResources((object) this.noAuthenticationRadioButton, "noAuthenticationRadioButton");
      this.noAuthenticationRadioButton.Checked = true;
      this.noAuthenticationRadioButton.Name = "noAuthenticationRadioButton";
      this.noAuthenticationRadioButton.TabStop = true;
      this.noAuthenticationRadioButton.UseVisualStyleBackColor = true;
      this.noAuthenticationRadioButton.CheckedChanged += new EventHandler(this.userRadioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.actualUserRadioButton, "actualUserRadioButton");
      this.actualUserRadioButton.Name = "actualUserRadioButton";
      this.actualUserRadioButton.UseVisualStyleBackColor = true;
      this.actualUserRadioButton.CheckedChanged += new EventHandler(this.userRadioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.otherUserRadioButton, "otherUserRadioButton");
      this.otherUserRadioButton.Name = "otherUserRadioButton";
      this.otherUserRadioButton.UseVisualStyleBackColor = true;
      this.otherUserRadioButton.CheckedChanged += new EventHandler(this.userRadioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.userNameLabel, "userNameLabel");
      this.userNameLabel.Name = "userNameLabel";
      componentResourceManager.ApplyResources((object) this.userNameTextBox, "userNameTextBox");
      this.userNameTextBox.Name = "userNameTextBox";
      componentResourceManager.ApplyResources((object) this.passwordLabel, "passwordLabel");
      this.passwordLabel.Name = "passwordLabel";
      componentResourceManager.ApplyResources((object) this.passwordTextBox, "passwordTextBox");
      this.passwordTextBox.Name = "passwordTextBox";
      this.passwordTextBox.UseSystemPasswordChar = true;
      componentResourceManager.ApplyResources((object) this.domainLabel, "domainLabel");
      this.domainLabel.Name = "domainLabel";
      componentResourceManager.ApplyResources((object) this.domainTextBox, "domainTextBox");
      this.domainTextBox.Name = "domainTextBox";
      componentResourceManager.ApplyResources((object) this.portTextBox, "portTextBox");
      this.portTextBox.Maximum = new Decimal(new int[4]
      {
        (int) ushort.MaxValue,
        0,
        0,
        0
      });
      this.portTextBox.Name = "portTextBox";
      this.portTextBox.Value = new Decimal(new int[4]
      {
        80,
        0,
        0,
        0
      });
      componentResourceManager.ApplyResources((object) this.portLabel, "portLabel");
      this.portLabel.Name = "portLabel";
      componentResourceManager.ApplyResources((object) this.httpProxyTextBox, "httpProxyTextBox");
      this.httpProxyTextBox.Name = "httpProxyTextBox";
      componentResourceManager.ApplyResources((object) this.httpProxyLabel, "httpProxyLabel");
      this.httpProxyLabel.Name = "httpProxyLabel";
      componentResourceManager.ApplyResources((object) this.manualProxyRadioButton, "manualProxyRadioButton");
      this.manualProxyRadioButton.Name = "manualProxyRadioButton";
      this.manualProxyRadioButton.UseVisualStyleBackColor = true;
      this.manualProxyRadioButton.CheckedChanged += new EventHandler(this.proxyRadioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.systemProxyRadioButton, "systemProxyRadioButton");
      this.systemProxyRadioButton.Name = "systemProxyRadioButton";
      this.systemProxyRadioButton.UseVisualStyleBackColor = true;
      this.systemProxyRadioButton.CheckedChanged += new EventHandler(this.proxyRadioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.noProxyRadioButton, "noProxyRadioButton");
      this.noProxyRadioButton.Checked = true;
      this.noProxyRadioButton.Name = "noProxyRadioButton";
      this.noProxyRadioButton.TabStop = true;
      this.noProxyRadioButton.UseVisualStyleBackColor = true;
      this.noProxyRadioButton.CheckedChanged += new EventHandler(this.proxyRadioButton_CheckedChanged);
      componentResourceManager.ApplyResources((object) this.okButton, "okButton");
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.AcceptButton = (IButtonControl) this.okButton;
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      this.CancelButton = (IButtonControl) this.cancelButton;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.Controls.Add((Control) this.proxyGroupBox);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ProxySettings);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.proxyGroupBox.ResumeLayout(false);
      this.proxyGroupBox.PerformLayout();
      this.userGroupBox.ResumeLayout(false);
      this.userGroupBox.PerformLayout();
      this.portTextBox.EndInit();
      this.ResumeLayout(false);
    }
  }
}
