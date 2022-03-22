// Decompiled with JetBrains decompiler
// Type: ZerroWare.SelectStartScreenDialog
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class SelectStartScreenDialog : Form
  {
    private string[] pictureNames;
    private Image[] thumbnails;
    private Image[] colorThumbnails;
    private RadioButton[] pictureRadioButtons;
    private PictureBox[] thumbnailsPictureBoxes;
    private PictureBox[] colorThumbnailsPictureBoxes;
    private int selectedRadioButton;
    private IContainer components;
    private Panel radioButtonPanel;
    private Button okButton;
    private Button cancelButton;
    private FlickerFreeTableLayoutPanel tableLayoutPanel;

    public SelectStartScreenDialog(ScreenElement[] elements, int selectedRadioButton)
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.okButton.Font = FontDefinition.DefaultTextFont;
      this.cancelButton.Font = FontDefinition.DefaultTextFont;
      this.okButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.cancelButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.okButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.cancelButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.selectedRadioButton = selectedRadioButton;
      int length = elements.Length;
      this.pictureNames = new string[length];
      this.thumbnails = new Image[length];
      this.colorThumbnails = new Image[length];
      for (int index = 0; index < length; ++index)
      {
        this.pictureNames[index] = elements[index].Description;
        this.thumbnails[index] = ScreenElement.byteArrayToImage(elements[index].Thumbnail);
        this.colorThumbnails[index] = ScreenElement.byteArrayToImage(elements[index].ColorThumbnail);
      }
      this.pictureRadioButtons = new RadioButton[this.pictureNames.Length + 1];
      this.thumbnailsPictureBoxes = new PictureBox[this.pictureNames.Length + 1];
      this.colorThumbnailsPictureBoxes = new PictureBox[this.pictureNames.Length + 1];
      this.InitRadioButtonList();
    }

    private void InitRadioButtonList()
    {
      this.tableLayoutPanel.SuspendLayout();
      this.tableLayoutPanel.RowCount = this.pictureNames.Length + 1;
      this.tableLayoutPanel.ColumnCount = 3;
      int row1 = 0;
      this.pictureRadioButtons[row1] = new RadioButton();
      this.pictureRadioButtons[row1].Text = GlobalResource.NoImage_RadioButton_Text;
      this.pictureRadioButtons[row1].Name = string.Concat((object) row1);
      this.pictureRadioButtons[row1].TextAlign = ContentAlignment.MiddleLeft;
      this.pictureRadioButtons[row1].Dock = DockStyle.Fill;
      this.pictureRadioButtons[row1].Font = FontDefinition.DefaultTextFont;
      this.pictureRadioButtons[row1].ForeColor = ColorDefinition.BlackTextColor;
      this.pictureRadioButtons[row1].Checked = this.selectedRadioButton == row1;
      this.tableLayoutPanel.Controls.Add((Control) this.pictureRadioButtons[row1], 0, row1);
      this.thumbnailsPictureBoxes[row1] = new PictureBox();
      this.thumbnailsPictureBoxes[row1].BorderStyle = BorderStyle.None;
      this.thumbnailsPictureBoxes[row1].Image = (Image) Resources.screen_empty_active;
      this.thumbnailsPictureBoxes[row1].SizeMode = PictureBoxSizeMode.Zoom;
      this.thumbnailsPictureBoxes[row1].TabStop = false;
      this.thumbnailsPictureBoxes[row1].Dock = DockStyle.Fill;
      this.tableLayoutPanel.Controls.Add((Control) this.thumbnailsPictureBoxes[row1], 1, row1);
      this.colorThumbnailsPictureBoxes[row1] = new PictureBox();
      this.colorThumbnailsPictureBoxes[row1].BorderStyle = BorderStyle.None;
      this.colorThumbnailsPictureBoxes[row1].Image = (Image) Resources.screen_empty_active;
      this.colorThumbnailsPictureBoxes[row1].SizeMode = PictureBoxSizeMode.Zoom;
      this.colorThumbnailsPictureBoxes[row1].TabStop = false;
      this.colorThumbnailsPictureBoxes[row1].Dock = DockStyle.Fill;
      this.tableLayoutPanel.Controls.Add((Control) this.colorThumbnailsPictureBoxes[row1], 2, row1);
      for (int row2 = 1; row2 < this.pictureNames.Length + 1; ++row2)
      {
        this.pictureRadioButtons[row2] = new RadioButton();
        this.pictureRadioButtons[row2].Text = this.pictureNames[row2 - 1];
        this.pictureRadioButtons[row2].Name = string.Concat((object) row2);
        this.pictureRadioButtons[row2].TextAlign = ContentAlignment.MiddleLeft;
        this.pictureRadioButtons[row2].Dock = DockStyle.Fill;
        this.pictureRadioButtons[row2].Font = FontDefinition.DefaultTextFont;
        this.pictureRadioButtons[row2].ForeColor = ColorDefinition.BlackTextColor;
        this.pictureRadioButtons[row2].Checked = this.selectedRadioButton == row2;
        this.tableLayoutPanel.Controls.Add((Control) this.pictureRadioButtons[row2], 0, row2);
        this.thumbnailsPictureBoxes[row2] = new PictureBox();
        this.thumbnailsPictureBoxes[row2].BorderStyle = BorderStyle.None;
        this.thumbnailsPictureBoxes[row2].Image = this.thumbnails[row2 - 1];
        if (this.thumbnails[row2 - 1] == null)
          this.thumbnailsPictureBoxes[row2].Image = (Image) Resources.screen_empty_active;
        this.thumbnailsPictureBoxes[row2].SizeMode = PictureBoxSizeMode.Zoom;
        this.thumbnailsPictureBoxes[row2].TabStop = false;
        this.thumbnailsPictureBoxes[row2].Dock = DockStyle.Fill;
        this.tableLayoutPanel.Controls.Add((Control) this.thumbnailsPictureBoxes[row2], 1, row2);
        this.colorThumbnailsPictureBoxes[row2] = new PictureBox();
        this.colorThumbnailsPictureBoxes[row2].BorderStyle = BorderStyle.None;
        this.colorThumbnailsPictureBoxes[row2].Image = this.colorThumbnails[row2 - 1];
        if (this.colorThumbnails[row2 - 1] == null)
          this.colorThumbnailsPictureBoxes[row2].Image = (Image) Resources.cMMI_StartAnimation_active;
        this.colorThumbnailsPictureBoxes[row2].SizeMode = PictureBoxSizeMode.Zoom;
        this.colorThumbnailsPictureBoxes[row2].TabStop = false;
        this.colorThumbnailsPictureBoxes[row2].Dock = DockStyle.Fill;
        this.tableLayoutPanel.Controls.Add((Control) this.colorThumbnailsPictureBoxes[row2], 2, row2);
      }
      this.tableLayoutPanel.AutoSize = true;
      this.tableLayoutPanel.ResumeLayout(false);
      this.tableLayoutPanel.PerformLayout();
    }

    private void cancelButton_Click(object sender, EventArgs e) => this.Dispose();

    private void okButton_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.pictureRadioButtons.Length; ++index)
      {
        if (this.pictureRadioButtons[index].Checked)
          this.SelectedScreen = index;
      }
      this.Dispose();
    }

    public int SelectedScreen { set; get; }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectStartScreenDialog));
      this.radioButtonPanel = new Panel();
      this.tableLayoutPanel = new FlickerFreeTableLayoutPanel();
      this.okButton = new Button();
      this.cancelButton = new Button();
      this.radioButtonPanel.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.radioButtonPanel, "radioButtonPanel");
      this.radioButtonPanel.BackColor = Color.Transparent;
      this.radioButtonPanel.Controls.Add((Control) this.tableLayoutPanel);
      this.radioButtonPanel.Name = "radioButtonPanel";
      componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
      this.tableLayoutPanel.Name = "tableLayoutPanel";
      componentResourceManager.ApplyResources((object) this.okButton, "okButton");
      this.okButton.DialogResult = DialogResult.OK;
      this.okButton.Name = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.okButton_Click);
      componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.AcceptButton = (IButtonControl) this.okButton;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      this.CancelButton = (IButtonControl) this.cancelButton;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.okButton);
      this.Controls.Add((Control) this.radioButtonPanel);
      this.DoubleBuffered = true;
      this.MinimizeBox = false;
      this.Name = nameof (SelectStartScreenDialog);
      this.ShowInTaskbar = false;
      this.radioButtonPanel.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
