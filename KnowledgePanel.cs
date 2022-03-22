// Decompiled with JetBrains decompiler
// Type: ZerroWare.KnowledgePanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ZerroWare
{
  public class KnowledgePanel : UserControl
  {
    private IContainer components;
    private NewsPanel newsPanel;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (KnowledgePanel));
      this.newsPanel = new NewsPanel();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.newsPanel, "newsPanel");
      this.newsPanel.BackColor = Color.Transparent;
      this.newsPanel.Name = "newsPanel";
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.newsPanel);
      this.DoubleBuffered = true;
      this.Name = nameof (KnowledgePanel);
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Layout += new LayoutEventHandler(this.KnowledgePanel_Layout);
      this.ResumeLayout(false);
    }

    public KnowledgePanel()
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      string empty = string.Empty;
      switch (Thread.CurrentThread.CurrentUICulture.ToString())
      {
        case "en-US":
          string enPath1 = Directories.Instance.EnPath;
          break;
        case "de-DE":
          string dePath = Directories.Instance.DePath;
          break;
        default:
          string enPath2 = Directories.Instance.EnPath;
          break;
      }
    }

    private void KnowledgePanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.Parent == null || !(this.Size != this.Parent.Size))
        return;
      this.Size = this.Parent.Size;
    }
  }
}
