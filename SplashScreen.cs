// Decompiled with JetBrains decompiler
// Type: ZerroWare.SplashScreen
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class SplashScreen : Form
  {
    private IContainer components;
    private PictureBox neoPictureBox;
    private ZerroProgressBar progressBar;
    private Label transparentLabel1;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SplashScreen));
      this.neoPictureBox = new PictureBox();
      this.transparentLabel1 = new Label();
      this.progressBar = new ZerroProgressBar();
      ((ISupportInitialize) this.neoPictureBox).BeginInit();
      this.SuspendLayout();
      this.neoPictureBox.BackColor = Color.White;
      this.neoPictureBox.Image = (Image) Resources.neo_sdiag_logo;
      this.neoPictureBox.Location = new Point(12, 12);
      this.neoPictureBox.Name = "neoPictureBox";
      this.neoPictureBox.Size = new Size(228, 98);
      this.neoPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
      this.neoPictureBox.TabIndex = 0;
      this.neoPictureBox.TabStop = false;
      this.transparentLabel1.BackColor = Color.White;
      this.transparentLabel1.Location = new Point(12, 91);
      this.transparentLabel1.Name = "transparentLabel1";
      this.transparentLabel1.Size = new Size(228, 19);
      this.transparentLabel1.TabIndex = 2;
      this.transparentLabel1.TextAlign = ContentAlignment.MiddleRight;
      this.progressBar.AdditionalText = (string) null;
      this.progressBar.BackColor = Color.Transparent;
      this.progressBar.EndColor = Color.FromArgb(93, 206, 230);
      this.progressBar.Location = new Point(12, 118);
      this.progressBar.Name = "progressBar";
      this.progressBar.PercentLabel = false;
      this.progressBar.Size = new Size(228, 32);
      this.progressBar.StartColor = Color.FromArgb(0, 176, 219);
      this.progressBar.TabIndex = 1;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      this.ClientSize = new Size(254, 162);
      this.Controls.Add((Control) this.transparentLabel1);
      this.Controls.Add((Control) this.progressBar);
      this.Controls.Add((Control) this.neoPictureBox);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (SplashScreen);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = nameof (SplashScreen);
      ((ISupportInitialize) this.neoPictureBox).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public SplashScreen()
    {
      this.InitializeComponent();
      this.Text = Assembly.GetExecutingAssembly().GetName().Name;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.transparentLabel1.Font = FontDefinition.DefaultBoldTextFont;
      this.transparentLabel1.ForeColor = ColorDefinition.BlueTextColor;
      this.transparentLabel1.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
      this.transparentLabel1.BackColor = Color.FromArgb(99, 180, 220);
      this.transparentLabel1.ForeColor = Color.White;
      this.progressBar.PercentLabel = true;
    }

    public string ProgressText
    {
      set => this.progressBar.Text = value;
      get => this.progressBar.Text;
    }

    public int ProgressValue
    {
      set => this.progressBar.Value = value;
      get => this.progressBar.Value;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.DrawRectangle(new Pen((Brush) new SolidBrush(ColorDefinition.BlueTextColor), 1f), 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
    }
  }
}
