// Decompiled with JetBrains decompiler
// Type: ZerroWare.LicenseKeyDialog
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class LicenseKeyDialog : Form
  {
    private IContainer components;
    private TextBox textBox1;
    private TextBox textBox2;
    private TextBox textBox3;
    private TextBox textBox4;
    private TextBox textBox5;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Button activateButton;
    private Regex regEx;
    private PictureBox pictureBox1;
    private Label label5;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LicenseKeyDialog));
      this.textBox1 = new TextBox();
      this.textBox2 = new TextBox();
      this.textBox3 = new TextBox();
      this.textBox4 = new TextBox();
      this.textBox5 = new TextBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.activateButton = new Button();
      this.label5 = new Label();
      this.pictureBox1 = new PictureBox();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.textBox1.CharacterCasing = CharacterCasing.Upper;
      componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
      this.textBox1.Name = "textBox1";
      this.textBox1.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.textBox2.CharacterCasing = CharacterCasing.Upper;
      componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
      this.textBox2.Name = "textBox2";
      this.textBox2.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.textBox2.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
      this.textBox3.CharacterCasing = CharacterCasing.Upper;
      componentResourceManager.ApplyResources((object) this.textBox3, "textBox3");
      this.textBox3.Name = "textBox3";
      this.textBox3.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.textBox3.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
      this.textBox4.CharacterCasing = CharacterCasing.Upper;
      componentResourceManager.ApplyResources((object) this.textBox4, "textBox4");
      this.textBox4.Name = "textBox4";
      this.textBox4.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.textBox4.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
      this.textBox5.CharacterCasing = CharacterCasing.Upper;
      componentResourceManager.ApplyResources((object) this.textBox5, "textBox5");
      this.textBox5.Name = "textBox5";
      this.textBox5.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.textBox5.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
      componentResourceManager.ApplyResources((object) this.label1, "label1");
      this.label1.BackColor = Color.Transparent;
      this.label1.Name = "label1";
      componentResourceManager.ApplyResources((object) this.label2, "label2");
      this.label2.BackColor = Color.Transparent;
      this.label2.Name = "label2";
      componentResourceManager.ApplyResources((object) this.label3, "label3");
      this.label3.BackColor = Color.Transparent;
      this.label3.Name = "label3";
      componentResourceManager.ApplyResources((object) this.label4, "label4");
      this.label4.BackColor = Color.Transparent;
      this.label4.Name = "label4";
      componentResourceManager.ApplyResources((object) this.activateButton, "activateButton");
      this.activateButton.Name = "activateButton";
      this.activateButton.UseVisualStyleBackColor = true;
      this.activateButton.Click += new EventHandler(this.activateButton_Click);
      this.label5.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.label5, "label5");
      this.label5.Name = "label5";
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.Image = (Image) Resources.buttons_error;
      componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.TabStop = false;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.activateButton);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.textBox5);
      this.Controls.Add((Control) this.textBox4);
      this.Controls.Add((Control) this.textBox3);
      this.Controls.Add((Control) this.textBox2);
      this.Controls.Add((Control) this.textBox1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.Name = nameof (LicenseKeyDialog);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public LicenseKeyDialog()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.textBox1.Font = FontDefinition.InfoLineLabelFont;
      this.textBox2.Font = FontDefinition.InfoLineLabelFont;
      this.textBox3.Font = FontDefinition.InfoLineLabelFont;
      this.textBox4.Font = FontDefinition.InfoLineLabelFont;
      this.textBox5.Font = FontDefinition.InfoLineLabelFont;
      this.activateButton.Font = FontDefinition.DefaultTextFont;
      this.activateButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.activateButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.label1.Font = FontDefinition.InfoLineLabelFont;
      this.label2.Font = FontDefinition.InfoLineLabelFont;
      this.label3.Font = FontDefinition.InfoLineLabelFont;
      this.label4.Font = FontDefinition.InfoLineLabelFont;
      this.label5.Font = FontDefinition.InfoLineLabelFont;
      this.pictureBox1.Image = (Image) Resources.buttons_error;
      this.activateButton.Enabled = false;
      this.textBox1.Focus();
      this.regEx = new Regex("^[0-9A-Z\\s]{1,5}$");
    }

    private void activateButton_Click(object sender, EventArgs e)
    {
      ActivationInformation.SetLicenseKey(this.textBox1.Text + "-" + this.textBox2.Text + "-" + this.textBox3.Text + "-" + this.textBox4.Text + "-" + this.textBox5.Text);
      this.DialogResult = DialogResult.OK;
      this.Dispose();
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      TextBox toProof = (TextBox) sender;
      if (toProof.Text.Length == 29 && toProof.Name == "textBox1")
      {
        string[] strArray = toProof.Text.Split('-');
        if (strArray.Length == 5)
        {
          this.textBox1.Text = strArray[0];
          this.textBox2.Text = strArray[1];
          this.textBox3.Text = strArray[2];
          this.textBox4.Text = strArray[3];
          this.textBox5.Text = strArray[4];
        }
      }
      this.validElementsInString(toProof);
      this.stringLengthValidation();
      if (toProof.Text.Length < 5)
        return;
      toProof.Text = toProof.Text.Substring(0, 5);
      switch (toProof.Name)
      {
        case "textBox1":
          this.textBox2.Focus();
          break;
        case "textBox2":
          this.textBox3.Focus();
          break;
        case "textBox3":
          this.textBox4.Focus();
          break;
        case "textBox4":
          this.textBox5.Focus();
          break;
        default:
          this.activateButton.Focus();
          break;
      }
    }

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      TextBox textBox = (TextBox) sender;
      if (e.KeyChar != '\b' || textBox.Text.Length >= 1)
        return;
      switch (textBox.Name)
      {
        case "textBox2":
          this.textBox1.Focus();
          break;
        case "textBox3":
          this.textBox2.Focus();
          break;
        case "textBox4":
          this.textBox3.Focus();
          break;
        default:
          this.textBox4.Focus();
          break;
      }
    }

    private void stringLengthValidation()
    {
      if (this.textBox1.Text.Length == 5 && this.textBox2.Text.Length == 5 && (this.textBox3.Text.Length == 5 && this.textBox4.Text.Length == 5) && this.textBox5.Text.Length == 5)
      {
        this.pictureBox1.Image = (Image) Resources.button_ok;
        this.activateButton.Enabled = true;
      }
      else
      {
        this.pictureBox1.Image = (Image) Resources.buttons_error;
        this.activateButton.Enabled = false;
      }
    }

    private void validElementsInString(TextBox toProof)
    {
      while (!this.regEx.IsMatch(toProof.Text) && toProof.Text.Length > 0)
      {
        toProof.Text = toProof.Text.Substring(0, toProof.Text.Length - 1);
        toProof.SelectionStart = toProof.Text.Length;
        toProof.ScrollToCaret();
      }
    }
  }
}
