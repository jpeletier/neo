// Decompiled with JetBrains decompiler
// Type: ZerroWare.NewsDialog
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class NewsDialog : Form
  {
    private IContainer components;
    private Button okButton;
    private NewsPanel newsPanel1;

    public NewsDialog()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.Font = FontDefinition.DefaultTextFont;
      this.okButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.okButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.BringToFront();
      this.TopMost = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewsDialog));
      this.okButton = new Button();
      this.newsPanel1 = new NewsPanel();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.okButton, "okButton");
      this.okButton.DialogResult = DialogResult.Cancel;
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.newsPanel1, "newsPanel1");
      this.newsPanel1.BackColor = Color.Transparent;
      this.newsPanel1.Name = "newsPanel1";
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.newsPanel1);
      this.Controls.Add((Control) this.okButton);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (NewsDialog);
      this.ResumeLayout(false);
    }
  }
}
