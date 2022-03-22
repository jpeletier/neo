// Decompiled with JetBrains decompiler
// Type: ZerroWare.WelcomePanel
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
  public class WelcomePanel : UserControl
  {
    private IContainer components;
    private PictureBox pleaseConnectPictureBox;
    private TransparentLabel pleaseConnectTransparentLabel;
    private NewsPanel newsPanel;

    public WelcomePanel()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.pleaseConnectTransparentLabel.Text = GlobalResource.PleaseConnect_Title;
      this.pleaseConnectTransparentLabel.Font = FontDefinition.InfoLineLabelFont;
      this.pleaseConnectTransparentLabel.ForeColor = ColorDefinition.ButtonDefaultTextColor;
    }

    private void WelcomePanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.Parent == null || !(this.Size != this.Parent.Size))
        return;
      this.Size = this.Parent.Size;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WelcomePanel));
      this.pleaseConnectPictureBox = new PictureBox();
      this.pleaseConnectTransparentLabel = new TransparentLabel();
      this.newsPanel = new NewsPanel();
      ((ISupportInitialize) this.pleaseConnectPictureBox).BeginInit();
      this.SuspendLayout();
      this.pleaseConnectPictureBox.Image = (Image) Resources.PleaseConnect;
      componentResourceManager.ApplyResources((object) this.pleaseConnectPictureBox, "pleaseConnectPictureBox");
      this.pleaseConnectPictureBox.Name = "pleaseConnectPictureBox";
      this.pleaseConnectPictureBox.TabStop = false;
      this.pleaseConnectTransparentLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.pleaseConnectTransparentLabel, "pleaseConnectTransparentLabel");
      this.pleaseConnectTransparentLabel.Name = "pleaseConnectTransparentLabel";
      this.pleaseConnectTransparentLabel.TabStop = false;
      this.pleaseConnectTransparentLabel.TextAlign = ContentAlignment.TopLeft;
      componentResourceManager.ApplyResources((object) this.newsPanel, "newsPanel");
      this.newsPanel.BackColor = Color.Transparent;
      this.newsPanel.Name = "newsPanel";
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.newsPanel);
      this.Controls.Add((Control) this.pleaseConnectTransparentLabel);
      this.Controls.Add((Control) this.pleaseConnectPictureBox);
      this.DoubleBuffered = true;
      this.Name = nameof (WelcomePanel);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Layout += new LayoutEventHandler(this.WelcomePanel_Layout);
      ((ISupportInitialize) this.pleaseConnectPictureBox).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
