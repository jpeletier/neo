// Decompiled with JetBrains decompiler
// Type: ZerroWare.ImagePreview
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using MMI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using UIElements;
using ZerroWare.Properties;

namespace ZerroWare
{
  public class ImagePreview : Form
  {
    private Communication MMICom;
    private PictureIds pictureId;
    private bool invertColor;
    private string pictureName;
    private bool isImageEmpty;
    private bool isColorImage;
    private readonly Image animationImage = (Image) Resources.cMMI_StartAnimation_preview;
    private IContainer components;
    private PictureBox originalPictureBox;
    private PictureBox mmiPictureBox;
    private Button loadImageButton;
    private Button cancelButton;
    private Label originalImageLabel;
    private Label mmiPreviewLabel;
    private Label toleranceFactorLabel;
    private NumericUpDown toleranceFactorNumericUpDown;
    private Button acceptButton;
    private Button removeButton;
    private TextBox pictureNameTextBox;
    private Label pictureNameLabel;
    private Button transmitButton;
    private Button connectMMITransmitButton;
    private Label connectMMIPreviewLabel;
    private PictureBox connectMMIPictureBox;

    public ImagePreview(Communication MMICom, PictureIds pictureId, bool colorImage)
    {
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.SetDefaultUIParameter();
      this.pictureId = pictureId;
      this.invertColor = false;
      this.isImageEmpty = false;
      this.isColorImage = colorImage;
      if (colorImage)
      {
        this.toleranceFactorLabel.Visible = false;
        this.toleranceFactorNumericUpDown.Visible = false;
        this.mmiPreviewLabel.Visible = false;
        this.mmiPictureBox.Visible = false;
        this.transmitButton.Visible = false;
      }
      else
      {
        this.connectMMIPreviewLabel.Visible = false;
        this.connectMMIPictureBox.Visible = false;
        this.connectMMITransmitButton.Visible = false;
      }
      this.MMICom = MMICom;
      if (MMICom == null)
        return;
      this.MMICom.Removed += new Communication.RemovedEventHandler(this.MMIDettachedHandler);
      MainWindow.Instance.HelloLoopPassed += new MainWindow.HelloLoopPassedEventHandler(this.HelloLoopPassedHandler);
      this.FunctionsEnabled(MMICom.DeviceConnected);
    }

    private void loadImage_Click(object sender, EventArgs e)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Multiselect = false;
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      string str1 = string.Empty;
      int num1 = 1;
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        string str2 = imageCodecInfo.CodecName.Substring(8).Replace("Codec", "Files").Trim();
        openFileDialog.Filter = string.Format("{0}{1}{2} ({3})|{3}", (object) openFileDialog.Filter, (object) str1, (object) str2, (object) imageCodecInfo.FilenameExtension);
        str1 = "|";
        if (str2.StartsWith("PNG"))
          openFileDialog.FilterIndex = num1;
        ++num1;
      }
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.Cursor = Cursors.WaitCursor;
      Bitmap b;
      try
      {
        b = new Bitmap(openFileDialog.FileName);
      }
      catch (Exception ex)
      {
        this.Cursor = Cursors.Default;
        MainWindow.Instance.Cursor = Cursors.Default;
        int num2 = (int) MessageBox.Show(GlobalResource.ImageLoad_Error_Message, GlobalResource.ImageLoad_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        return;
      }
      Bitmap sourceBitmap = this.ResizeBitmap(b, 240, 320);
      this.originalPictureBox.Image = (Image) sourceBitmap;
      double toleranceFactor = (double) this.toleranceFactorNumericUpDown.Value;
      this.mmiPictureBox.Image = (Image) this.ModifyColorsOfBitmap(sourceBitmap, toleranceFactor, Color.White, Color.Black);
      this.connectMMIPictureBox.Image = (Image) this.ModifyColorsOfBitmap565(sourceBitmap);
      this.isImageEmpty = false;
      this.Cursor = Cursors.Default;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void MMIPictureBox_Click(object sender, EventArgs e)
    {
      if (this.originalPictureBox.Image == null && this.mmiPictureBox.Image == null)
        return;
      this.Cursor = Cursors.WaitCursor;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      if (this.originalPictureBox.Image != null)
      {
        this.invertColor = !this.invertColor;
        this.mmiPictureBox.Image = !this.invertColor ? (Image) this.ModifyColorsOfBitmap((Bitmap) this.originalPictureBox.Image, (double) this.toleranceFactorNumericUpDown.Value, Color.White, Color.Black) : (Image) this.ModifyColorsOfBitmap((Bitmap) this.originalPictureBox.Image, (double) this.toleranceFactorNumericUpDown.Value, Color.Black, Color.White);
      }
      else
        this.mmiPictureBox.Image = (Image) this.ModifyColorsOfBitmap((Bitmap) this.mmiPictureBox.Image, 0.0, Color.Black, Color.White);
      this.Cursor = Cursors.Default;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void toleranceFactorNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (this.originalPictureBox.Image == null)
        return;
      this.Cursor = Cursors.WaitCursor;
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      double num = (double) this.toleranceFactorNumericUpDown.Value;
      this.mmiPictureBox.Image = !this.invertColor ? (Image) this.ModifyColorsOfBitmap((Bitmap) this.originalPictureBox.Image, (double) this.toleranceFactorNumericUpDown.Value, Color.White, Color.Black) : (Image) this.ModifyColorsOfBitmap((Bitmap) this.originalPictureBox.Image, (double) this.toleranceFactorNumericUpDown.Value, Color.Black, Color.White);
      this.Cursor = Cursors.Default;
      MainWindow.Instance.Cursor = Cursors.Default;
    }

    private void cancelButton_Click(object sender, EventArgs e) => this.Dispose();

    private void acceptButton_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.pictureNameTextBox.Text))
      {
        int num1 = (int) MessageBox.Show(GlobalResource.ImageWarningNoName, GlobalResource.ImageWarningNoNameCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else if (!this.isColorImage ? this.mmiPictureBox.Image == null : this.connectMMIPictureBox.Image == null)
      {
        int num2 = (int) MessageBox.Show(GlobalResource.ImageWarningNoPicture, GlobalResource.ImageWarningNoPictureCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        this.PictureName = this.pictureNameTextBox.Text;
        if (this.isColorImage)
          this.ColorThumbnailImage = this.connectMMIPictureBox.Image.GetThumbnailImage(Settings.Default.PreviewWidth, Settings.Default.PreviewHeight, new Image.GetThumbnailImageAbort(this.ThumbnailCallback), IntPtr.Zero);
        else
          this.ThumbnailImage = this.mmiPictureBox.Image.GetThumbnailImage(Settings.Default.PreviewWidth, Settings.Default.PreviewHeight, new Image.GetThumbnailImageAbort(this.ThumbnailCallback), IntPtr.Zero);
        this.DialogResult = DialogResult.OK;
        this.Dispose();
      }
    }

    public bool ThumbnailCallback() => true;

    private void removeButton_Click(object sender, EventArgs e)
    {
      this.MonochromeImageForMMI = new byte[76800];
      this.Colored565ImageForMMI = (byte[]) null;
      this.isImageEmpty = true;
    }

    private Bitmap ResizeBitmap(Bitmap b, int width, int height)
    {
      Bitmap bitmap = new Bitmap(width, height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        graphics.DrawImage((Image) b, 0, 0, width, height);
      return bitmap;
    }

    private Bitmap ModifyColorsOfBitmap(
      Bitmap sourceBitmap,
      double toleranceFactor,
      Color highlight,
      Color background)
    {
      Bitmap bitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
      for (int x = 0; x < bitmap.Width; ++x)
      {
        for (int y = 0; y < bitmap.Height; ++y)
        {
          Color pixel = sourceBitmap.GetPixel(x, y);
          if ((double) pixel.R >= (double) Color.White.R * (1.0 - toleranceFactor) && (double) pixel.R <= (double) Color.White.R * (1.0 + toleranceFactor) && ((double) pixel.G >= (double) Color.White.G * (1.0 - toleranceFactor) && (double) pixel.G <= (double) Color.White.G * (1.0 + toleranceFactor)) && ((double) pixel.B >= (double) Color.White.B * (1.0 - toleranceFactor) && (double) pixel.B <= (double) Color.White.B * (1.0 + toleranceFactor)) || pixel.A == (byte) 0)
            bitmap.SetPixel(x, y, highlight);
          else
            bitmap.SetPixel(x, y, background);
        }
      }
      return bitmap;
    }

    private Bitmap ModifyColorsOfBitmap565(Bitmap sourceBitmap)
    {
      Bitmap bitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);
      for (int x = 0; x < bitmap.Width; ++x)
      {
        for (int y = 0; y < bitmap.Height; ++y)
        {
          Color pixel = sourceBitmap.GetPixel(x, y);
          ushort color = this.RGB565Converter(pixel.R, pixel.G, pixel.B);
          byte red = this.RGB565GetRed(color);
          byte green = this.RGB565GetGreen(color);
          byte blue = this.RGB565GetBlue(color);
          bitmap.SetPixel(x, y, Color.FromArgb((int) red, (int) green, (int) blue));
        }
      }
      return bitmap;
    }

    private ushort RGB565Converter(byte red, byte green, byte blue) => (ushort) (((int) red & 248) << 8 | ((int) green & 252) << 3 | (int) blue >> 3);

    private byte RGB565GetRed(ushort color) => (byte) (((int) color & 63488) >> 8);

    private byte RGB565GetGreen(ushort color) => (byte) (((int) color & 2016) >> 3);

    private byte RGB565GetBlue(ushort color) => (byte) (((int) color & 31) << 3);

    public byte[] Colored565ImageForMMI
    {
      get
      {
        if (this.connectMMIPictureBox.Image == null)
          return (byte[]) null;
        byte[] numArray1 = new byte[153600];
        Bitmap image = (Bitmap) this.connectMMIPictureBox.Image;
        int num1 = 0;
        for (int y = 0; y < image.Height; ++y)
        {
          for (int x = 0; x < image.Width; ++x)
          {
            Color pixel = image.GetPixel(x, y);
            ushort num2 = this.RGB565Converter(pixel.R, pixel.G, pixel.B);
            byte[] numArray2 = numArray1;
            int index1 = num1;
            int num3 = index1 + 1;
            int num4 = (int) BitConverter.GetBytes(num2)[0];
            numArray2[index1] = (byte) num4;
            byte[] numArray3 = numArray1;
            int index2 = num3;
            num1 = index2 + 1;
            int num5 = (int) BitConverter.GetBytes(num2)[1];
            numArray3[index2] = (byte) num5;
          }
        }
        return numArray1;
      }
      set
      {
        if (value == null || value.Length != 153600)
        {
          this.isImageEmpty = true;
          this.connectMMIPictureBox.Image = this.animationImage;
        }
        else
        {
          Bitmap bitmap = new Bitmap(240, 320);
          int num1 = 0;
          for (int y = 0; y < bitmap.Height; ++y)
          {
            for (int x = 0; x < bitmap.Width; ++x)
            {
              byte[] numArray1 = value;
              int index1 = num1;
              int num2 = index1 + 1;
              byte num3 = numArray1[index1];
              byte[] numArray2 = value;
              int index2 = num2;
              num1 = index2 + 1;
              byte num4 = numArray2[index2];
              ushort color = (ushort) ((uint) num3 | (uint) num4 << 8);
              bitmap.SetPixel(x, y, Color.FromArgb((int) this.RGB565GetRed(color), (int) this.RGB565GetGreen(color), (int) this.RGB565GetBlue(color)));
            }
          }
          this.connectMMIPictureBox.Image = (Image) bitmap;
        }
      }
    }

    public byte[] MonochromeImageForMMI
    {
      get
      {
        if (this.mmiPictureBox.Image == null)
          return (byte[]) null;
        byte[] numArray = new byte[76800];
        Bitmap image = (Bitmap) this.mmiPictureBox.Image;
        int index = 0;
        for (int y = 0; y < image.Height; ++y)
        {
          for (int x = 0; x < image.Width; ++x)
          {
            Color pixel = image.GetPixel(x, y);
            numArray[index] = (int) pixel.R != (int) Color.White.R || (int) pixel.G != (int) Color.White.G || (int) pixel.B != (int) Color.White.B ? (byte) 0 : byte.MaxValue;
            ++index;
          }
        }
        return numArray;
      }
      set
      {
        if (value == null || value.Length != 76800)
          return;
        Bitmap bitmap = new Bitmap(240, 320);
        int index = 0;
        for (int y = 0; y < bitmap.Height; ++y)
        {
          for (int x = 0; x < bitmap.Width; ++x)
          {
            if (value[index] == byte.MaxValue)
              bitmap.SetPixel(x, y, Color.White);
            else
              bitmap.SetPixel(x, y, Color.Black);
            ++index;
          }
        }
        this.mmiPictureBox.Image = (Image) bitmap;
      }
    }

    public string PictureName
    {
      set
      {
        this.pictureNameTextBox.Text = value;
        this.pictureName = value;
      }
      get => this.pictureName;
    }

    public Image ThumbnailImage { set; get; }

    public Image ColorThumbnailImage { set; get; }

    public bool IsImageEmpty => this.isImageEmpty;

    private bool EnablesUserControl(bool enable)
    {
      MainWindow.Instance.EnablesUserControl(enable);
      this.Enabled = enable;
      return this.Enabled;
    }

    private void parameterTransferPictureBox_Click(object sender, EventArgs e)
    {
      if (this.MonochromeImageForMMI == null)
      {
        int num = (int) MessageBox.Show(GlobalResource.ImageWarningNoImage, GlobalResource.ImageWarningNoImageCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        MainWindow.Instance.Cursor = Cursors.WaitCursor;
        this.Cursor = Cursors.WaitCursor;
        DisconnectWarning disconnectWarning = new DisconnectWarning((Form) this, new Func<bool, bool>(this.EnablesUserControl));
        disconnectWarning.Show((IWin32Window) MainWindow.Instance);
        disconnectWarning.ProgressBar().AdditionalText = GlobalResource.Configuration_StartScreen;
        HelperClass.DoEvents();
        int maximum = Pictures.Instance.NumberOfPictures * 320 * 240 / 1000;
        int actualPosition = 0;
        new MMITransceiver(this.MMICom).TransferStartScreenToMMI(this.MonochromeImageForMMI, ref actualPosition, maximum, disconnectWarning.ProgressBar());
        disconnectWarning.ProgressBar().Value = 100;
        disconnectWarning.ProgressBar().Refresh();
        disconnectWarning.Dispose();
        FinishSound.Instance.Play();
        this.Cursor = Cursors.Default;
        MainWindow.Instance.Cursor = Cursors.Default;
        this.Focus();
      }
    }

    private void FunctionsEnabled(bool value)
    {
      if (this.transmitButton.InvokeRequired)
      {
        try
        {
          this.Invoke((Delegate) new ImagePreview.FunctionActivation(this.FunctionsEnabled), (object) value);
        }
        catch (Exception ex)
        {
          GlobalLogger.Instance.WriteLine(ex);
        }
      }
      else if (value)
      {
        if (CommandBuilder.Instance.MCUVersion == MMIMCUVersion.CONNECT_MZ)
        {
          this.transmitButton.Enabled = false;
          this.connectMMITransmitButton.Enabled = true;
        }
        else
        {
          this.transmitButton.Enabled = true;
          this.connectMMITransmitButton.Enabled = false;
        }
      }
      else
      {
        this.transmitButton.Enabled = false;
        this.connectMMITransmitButton.Enabled = false;
      }
    }

    private void HelloLoopPassedHandler(object mainWindow, MainWindowEventArgs args) => this.FunctionsEnabled(args.Done);

    private void MMIDettachedHandler(object sender, EventArgs e) => this.FunctionsEnabled(false);

    private void numericUpDown_Leave(object sender, EventArgs e)
    {
      NumericUpDown numericUpDown = (NumericUpDown) sender;
      if (!string.IsNullOrEmpty(numericUpDown.Text))
        return;
      Decimal num = numericUpDown.Value;
      numericUpDown.Value = !(numericUpDown.Value == numericUpDown.Maximum) ? (!(numericUpDown.Value == numericUpDown.Minimum) ? numericUpDown.Minimum : numericUpDown.Maximum) : numericUpDown.Minimum;
      numericUpDown.Value = num;
    }

    private void connectMMITransmitButton_Click(object sender, EventArgs e)
    {
      if (this.Colored565ImageForMMI == null)
      {
        int num = (int) MessageBox.Show(GlobalResource.ImageWarningNoImage, GlobalResource.ImageWarningNoImageCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else if (this.IsImageEmpty)
      {
        this.RemoveStartScreenFromMMI();
      }
      else
      {
        MainWindow.Instance.Cursor = Cursors.WaitCursor;
        this.Cursor = Cursors.WaitCursor;
        DisconnectWarning disconnectWarning = new DisconnectWarning((Form) this, new Func<bool, bool>(this.EnablesUserControl));
        disconnectWarning.Show((IWin32Window) MainWindow.Instance);
        disconnectWarning.ProgressBar().AdditionalText = GlobalResource.Configuration_StartScreen;
        HelperClass.DoEvents();
        int maximum = Pictures.Instance.NumberOfPictures * 320 * 240 * 2 / 1000;
        int actualPosition = 0;
        new MMITransceiver(this.MMICom).TransferStartScreenToMMI(this.Colored565ImageForMMI, ref actualPosition, maximum, disconnectWarning.ProgressBar());
        disconnectWarning.ProgressBar().Value = 100;
        disconnectWarning.ProgressBar().Refresh();
        disconnectWarning.Dispose();
        FinishSound.Instance.Play();
        this.Cursor = Cursors.Default;
        MainWindow.Instance.Cursor = Cursors.Default;
        this.Focus();
      }
    }

    private void RemoveStartScreenFromMMI()
    {
      MainWindow.Instance.Cursor = Cursors.WaitCursor;
      this.Cursor = Cursors.WaitCursor;
      DisconnectWarning disconnectWarning = new DisconnectWarning((Form) MainWindow.Instance, new Func<bool, bool>(this.EnablesUserControl));
      disconnectWarning.Show((IWin32Window) MainWindow.Instance);
      disconnectWarning.ProgressBar().AdditionalText = GlobalResource.Configuration_StartScreen;
      HelperClass.DoEvents();
      int maximum = 1;
      int actualPosition = 0;
      new MMITransceiver(this.MMICom).DeleteStartScreenFromMMI(ref actualPosition, maximum, disconnectWarning.ProgressBar());
      disconnectWarning.ProgressBar().Value = 100;
      disconnectWarning.ProgressBar().Refresh();
      disconnectWarning.Dispose();
      FinishSound.Instance.Play();
      this.Cursor = Cursors.Default;
      MainWindow.Instance.Cursor = Cursors.Default;
      this.Focus();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void SetDefaultUIParameter()
    {
      this.pictureNameLabel.Font = FontDefinition.DefaultTextFont;
      this.pictureNameTextBox.Font = FontDefinition.DefaultTextFont;
      this.toleranceFactorLabel.Font = FontDefinition.DefaultTextFont;
      this.toleranceFactorNumericUpDown.Font = FontDefinition.DefaultTextFont;
      this.originalImageLabel.Font = FontDefinition.DefaultTextFont;
      this.mmiPreviewLabel.Font = FontDefinition.DefaultTextFont;
      this.loadImageButton.Font = FontDefinition.DefaultTextFont;
      this.cancelButton.Font = FontDefinition.DefaultTextFont;
      this.removeButton.Font = FontDefinition.DefaultTextFont;
      this.acceptButton.Font = FontDefinition.DefaultTextFont;
      this.transmitButton.Font = FontDefinition.DefaultTextFont;
      this.connectMMITransmitButton.Font = FontDefinition.DefaultTextFont;
      this.loadImageButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.cancelButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.removeButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.acceptButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.transmitButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.connectMMITransmitButton.BackgroundImage = (Image) BackgroundImages.LightGrayGradient;
      this.loadImageButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.cancelButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.removeButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.acceptButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.transmitButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.connectMMITransmitButton.BackgroundImageLayout = ImageLayout.Stretch;
      this.pictureNameLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.pictureNameTextBox.ForeColor = ColorDefinition.BlackTextColor;
      this.toleranceFactorLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.toleranceFactorNumericUpDown.ForeColor = ColorDefinition.BlackTextColor;
      this.originalImageLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.mmiPreviewLabel.ForeColor = ColorDefinition.BlackTextColor;
      this.loadImageButton.ForeColor = ColorDefinition.BlackTextColor;
      this.cancelButton.ForeColor = ColorDefinition.BlackTextColor;
      this.removeButton.ForeColor = ColorDefinition.BlackTextColor;
      this.acceptButton.ForeColor = ColorDefinition.BlackTextColor;
      this.transmitButton.ForeColor = ColorDefinition.BlackTextColor;
      this.connectMMITransmitButton.ForeColor = ColorDefinition.BlackTextColor;
    }

    protected override void DestroyHandle()
    {
      if (this.MMICom != null)
      {
        this.MMICom.Removed -= new Communication.RemovedEventHandler(this.MMIDettachedHandler);
        MainWindow.Instance.HelloLoopPassed -= new MainWindow.HelloLoopPassedEventHandler(this.HelloLoopPassedHandler);
      }
      base.DestroyHandle();
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImagePreview));
      this.originalPictureBox = new PictureBox();
      this.mmiPictureBox = new PictureBox();
      this.loadImageButton = new Button();
      this.cancelButton = new Button();
      this.originalImageLabel = new Label();
      this.mmiPreviewLabel = new Label();
      this.toleranceFactorLabel = new Label();
      this.toleranceFactorNumericUpDown = new NumericUpDown();
      this.acceptButton = new Button();
      this.removeButton = new Button();
      this.pictureNameTextBox = new TextBox();
      this.pictureNameLabel = new Label();
      this.transmitButton = new Button();
      this.connectMMITransmitButton = new Button();
      this.connectMMIPreviewLabel = new Label();
      this.connectMMIPictureBox = new PictureBox();
      ((ISupportInitialize) this.originalPictureBox).BeginInit();
      ((ISupportInitialize) this.mmiPictureBox).BeginInit();
      this.toleranceFactorNumericUpDown.BeginInit();
      ((ISupportInitialize) this.connectMMIPictureBox).BeginInit();
      this.SuspendLayout();
      this.originalPictureBox.BorderStyle = BorderStyle.FixedSingle;
      componentResourceManager.ApplyResources((object) this.originalPictureBox, "originalPictureBox");
      this.originalPictureBox.Name = "originalPictureBox";
      this.originalPictureBox.TabStop = false;
      this.originalPictureBox.Click += new EventHandler(this.loadImage_Click);
      this.mmiPictureBox.BorderStyle = BorderStyle.FixedSingle;
      componentResourceManager.ApplyResources((object) this.mmiPictureBox, "mmiPictureBox");
      this.mmiPictureBox.Name = "mmiPictureBox";
      this.mmiPictureBox.TabStop = false;
      this.mmiPictureBox.Click += new EventHandler(this.MMIPictureBox_Click);
      componentResourceManager.ApplyResources((object) this.loadImageButton, "loadImageButton");
      this.loadImageButton.Name = "loadImageButton";
      this.loadImageButton.UseVisualStyleBackColor = true;
      this.loadImageButton.Click += new EventHandler(this.loadImage_Click);
      componentResourceManager.ApplyResources((object) this.cancelButton, "cancelButton");
      this.cancelButton.DialogResult = DialogResult.Cancel;
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new EventHandler(this.cancelButton_Click);
      this.originalImageLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.originalImageLabel, "originalImageLabel");
      this.originalImageLabel.Name = "originalImageLabel";
      this.mmiPreviewLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.mmiPreviewLabel, "mmiPreviewLabel");
      this.mmiPreviewLabel.Name = "mmiPreviewLabel";
      this.toleranceFactorLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.toleranceFactorLabel, "toleranceFactorLabel");
      this.toleranceFactorLabel.Name = "toleranceFactorLabel";
      this.toleranceFactorNumericUpDown.DecimalPlaces = 2;
      this.toleranceFactorNumericUpDown.Increment = new Decimal(new int[4]
      {
        2,
        0,
        0,
        131072
      });
      componentResourceManager.ApplyResources((object) this.toleranceFactorNumericUpDown, "toleranceFactorNumericUpDown");
      this.toleranceFactorNumericUpDown.Maximum = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.toleranceFactorNumericUpDown.Name = "toleranceFactorNumericUpDown";
      this.toleranceFactorNumericUpDown.Value = new Decimal(new int[4]
      {
        2,
        0,
        0,
        65536
      });
      this.toleranceFactorNumericUpDown.ValueChanged += new EventHandler(this.toleranceFactorNumericUpDown_ValueChanged);
      this.toleranceFactorNumericUpDown.Leave += new EventHandler(this.numericUpDown_Leave);
      componentResourceManager.ApplyResources((object) this.acceptButton, "acceptButton");
      this.acceptButton.Name = "acceptButton";
      this.acceptButton.UseVisualStyleBackColor = true;
      this.acceptButton.Click += new EventHandler(this.acceptButton_Click);
      componentResourceManager.ApplyResources((object) this.removeButton, "removeButton");
      this.removeButton.Name = "removeButton";
      this.removeButton.UseVisualStyleBackColor = true;
      this.removeButton.Click += new EventHandler(this.removeButton_Click);
      componentResourceManager.ApplyResources((object) this.pictureNameTextBox, "pictureNameTextBox");
      this.pictureNameTextBox.Name = "pictureNameTextBox";
      this.pictureNameLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.pictureNameLabel, "pictureNameLabel");
      this.pictureNameLabel.Name = "pictureNameLabel";
      componentResourceManager.ApplyResources((object) this.transmitButton, "transmitButton");
      this.transmitButton.Name = "transmitButton";
      this.transmitButton.UseVisualStyleBackColor = true;
      this.transmitButton.Click += new EventHandler(this.parameterTransferPictureBox_Click);
      componentResourceManager.ApplyResources((object) this.connectMMITransmitButton, "connectMMITransmitButton");
      this.connectMMITransmitButton.Name = "connectMMITransmitButton";
      this.connectMMITransmitButton.UseVisualStyleBackColor = true;
      this.connectMMITransmitButton.Click += new EventHandler(this.connectMMITransmitButton_Click);
      this.connectMMIPreviewLabel.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.connectMMIPreviewLabel, "connectMMIPreviewLabel");
      this.connectMMIPreviewLabel.Name = "connectMMIPreviewLabel";
      this.connectMMIPictureBox.BorderStyle = BorderStyle.FixedSingle;
      componentResourceManager.ApplyResources((object) this.connectMMIPictureBox, "connectMMIPictureBox");
      this.connectMMIPictureBox.Name = "connectMMIPictureBox";
      this.connectMMIPictureBox.TabStop = false;
      this.AcceptButton = (IButtonControl) this.acceptButton;
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.BackgroundImage = (Image) Resources.bg_24x1800;
      this.CancelButton = (IButtonControl) this.cancelButton;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.Controls.Add((Control) this.connectMMITransmitButton);
      this.Controls.Add((Control) this.connectMMIPreviewLabel);
      this.Controls.Add((Control) this.connectMMIPictureBox);
      this.Controls.Add((Control) this.transmitButton);
      this.Controls.Add((Control) this.pictureNameLabel);
      this.Controls.Add((Control) this.pictureNameTextBox);
      this.Controls.Add((Control) this.removeButton);
      this.Controls.Add((Control) this.acceptButton);
      this.Controls.Add((Control) this.toleranceFactorNumericUpDown);
      this.Controls.Add((Control) this.toleranceFactorLabel);
      this.Controls.Add((Control) this.mmiPreviewLabel);
      this.Controls.Add((Control) this.originalImageLabel);
      this.Controls.Add((Control) this.cancelButton);
      this.Controls.Add((Control) this.loadImageButton);
      this.Controls.Add((Control) this.mmiPictureBox);
      this.Controls.Add((Control) this.originalPictureBox);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (ImagePreview);
      this.ShowInTaskbar = false;
      ((ISupportInitialize) this.originalPictureBox).EndInit();
      ((ISupportInitialize) this.mmiPictureBox).EndInit();
      this.toleranceFactorNumericUpDown.EndInit();
      ((ISupportInitialize) this.connectMMIPictureBox).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private delegate void FunctionActivation(bool value);
  }
}
