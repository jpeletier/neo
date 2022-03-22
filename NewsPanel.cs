// Decompiled with JetBrains decompiler
// Type: ZerroWare.NewsPanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using UIElements;

namespace ZerroWare
{
  public class NewsPanel : UserControl
  {
    private IContainer components;
    private NewsContent newsContent;
    private TabControl tabControl;
    private TabPage newsTabPage;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (NewsPanel));
      this.tabControl = new TabControl();
      this.newsTabPage = new TabPage();
      this.newsContent = new NewsContent();
      this.tabControl.SuspendLayout();
      this.newsTabPage.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.tabControl, "tabControl");
      this.tabControl.Controls.Add((Control) this.newsTabPage);
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.newsTabPage.Controls.Add((Control) this.newsContent);
      componentResourceManager.ApplyResources((object) this.newsTabPage, "newsTabPage");
      this.newsTabPage.Name = "newsTabPage";
      this.newsTabPage.UseVisualStyleBackColor = true;
      componentResourceManager.ApplyResources((object) this.newsContent, "newsContent");
      this.newsContent.BackColor = Color.White;
      this.newsContent.Name = "newsContent";
      this.newsContent.TabStop = false;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.tabControl);
      this.DoubleBuffered = true;
      this.Name = nameof (NewsPanel);
      this.tabControl.ResumeLayout(false);
      this.newsTabPage.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public NewsPanel()
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
      NewsListFile newsListFile = NewsListFile.ReadFile();
      string[] heading = new string[newsListFile.Length];
      string[] content = new string[newsListFile.Length];
      string[] document = new string[newsListFile.Length];
      bool[] wasRead = new bool[newsListFile.Length];
      int[] position1 = new int[newsListFile.Length];
      for (int position2 = 0; position2 < newsListFile.Length; ++position2)
      {
        position1[position2] = position2;
        heading[position2] = newsListFile.Caption(position2);
        content[position2] = newsListFile.Text(position2);
        document[position2] = Directories.Instance.NewsPath + newsListFile.PdfFile(position2);
        wasRead[position2] = newsListFile.WasRead(position2);
      }
      this.newsContent.SetElements(position1, heading, content, document, wasRead);
      this.tabControl.Font = FontDefinition.MenubarFont;
    }
  }
}
