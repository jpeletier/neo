// Decompiled with JetBrains decompiler
// Type: ZerroWare.ProfilePanel
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using MMI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using UIElements;

namespace ZerroWare
{
  public class ProfilePanel : UserControl
  {
    private IContainer components;
    private Dictionary<int, ParameterValue> allParameters;
    private ComponentResourceManager profileResources;
    private int sizeOfElementsToDraw;
    private int[] elementsToDraw;
    private bool valueChanged;
    private NumericUpDown[] elementNumericUpDown;

    protected override void Dispose(bool disposing)
    {
      MouseWheelRedirector.Detach((Control) this);
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProfilePanel));
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.DoubleBuffered = true;
      this.Name = nameof (ProfilePanel);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private static bool InDesignMode() => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    public ProfilePanel()
    {
      if (!ProfilePanel.InDesignMode())
      {
        Thread.CurrentThread.CurrentUICulture = MainWindow.Instance.CurrentCulture();
        this.profileResources = new ComponentResourceManager(typeof (ProfilePanel));
        this.valueChanged = false;
        this.allParameters = Parameters.Instance.Dictionary;
        this.sizeOfElementsToDraw = 12;
        this.InitElementsToDraw();
      }
      this.InitializeComponent();
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.elementNumericUpDown = new NumericUpDown[this.sizeOfElementsToDraw];
      if (!ProfilePanel.InDesignMode())
      {
        this.SuspendLayout();
        this.drawDictionaryElements();
        this.ResumeLayout(false);
        this.PerformLayout();
      }
      MouseWheelRedirector.Attach((Control) this);
    }

    private void drawDictionaryElements()
    {
      int versionLevelProperty = ActivationInformation.VersionLevelProperty;
      string str1 = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString();
      for (int index = 0; index < this.sizeOfElementsToDraw; ++index)
      {
        ParameterValue allParameter = this.allParameters[this.elementsToDraw[index]];
        this.elementNumericUpDown[index] = new NumericUpDown();
        this.elementNumericUpDown[index].Font = FontDefinition.DefaultTextFont;
        this.elementNumericUpDown[index].ForeColor = ColorDefinition.BlackTextColor;
        this.elementNumericUpDown[index].Minimum = (Decimal) allParameter.ReadableMinimumValue;
        this.elementNumericUpDown[index].Maximum = (Decimal) allParameter.ReadableMaximumValue;
        this.elementNumericUpDown[index].Increment = (Decimal) allParameter.StepSize;
        this.elementNumericUpDown[index].Value = (Decimal) allParameter.ReadableValue;
        this.elementNumericUpDown[index].ReadOnly = allParameter.IsReadOnly(versionLevelProperty);
        this.elementNumericUpDown[index].Enabled = !allParameter.IsReadOnly(versionLevelProperty);
        this.elementNumericUpDown[index].Name = string.Concat((object) index);
        this.elementNumericUpDown[index].ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
        this.elementNumericUpDown[index].Leave += new EventHandler(this.numericUpDown_Leave);
        this.elementNumericUpDown[index].Anchor = AnchorStyles.Left;
        string str2 = Convert.ToString(allParameter.StepSize);
        int num1 = str2.IndexOf(str1);
        int num2 = num1 <= 0 ? 0 : str2.Length - num1 - 1;
        this.elementNumericUpDown[index].DecimalPlaces = num2;
      }
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

    private void InitElementsToDraw()
    {
      int num1 = 0;
      int index1 = 0;
      this.elementsToDraw = new int[this.sizeOfElementsToDraw];
      int num2 = num1 + 1;
      this.elementsToDraw[index1] = num2;
      int num3 = num2 + 1;
      int index2 = index1 + 1;
      int num4 = num3 + 1;
      this.elementsToDraw[index2] = num4;
      int num5 = num4 + 1;
      int index3 = index2 + 1;
      this.elementsToDraw[index3] = num5;
      int num6 = num5 + 1;
      int index4 = index3 + 1;
      this.elementsToDraw[index4] = num6;
      int num7 = num6 + 1;
      int index5 = index4 + 1;
      this.elementsToDraw[index5] = num7;
      int num8 = num7 + 1;
      int index6 = index5 + 1;
      this.elementsToDraw[index6] = num8;
      int num9 = num8 + 1;
      int index7 = index6 + 1;
      this.elementsToDraw[index7] = num9;
      int num10 = num9 + 1;
      int index8 = index7 + 1;
      this.elementsToDraw[index8] = num10;
      int num11 = num10 + 1;
      int index9 = index8 + 1;
      this.elementsToDraw[index9] = num11;
      int num12 = num11 + 1;
      int index10 = index9 + 1;
      int num13 = num12 + 1 + 1;
      this.elementsToDraw[index10] = num13;
      int num14 = num13 + 1;
      int index11 = index10 + 1;
      this.elementsToDraw[index11] = num14;
      int num15 = num14 + 1;
      int index12 = index11 + 1;
      int num16 = num15 + 1;
      // ISSUE: reference to a compiler-generated method
      this.elementsToDraw[index12] = Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.NACHLAUFWEG);
      int num17 = num16 + 1;
      int num18 = index12 + 1;
    }

    public void updateDictionaryWithUIValues()
    {
      for (int index = 0; index < this.sizeOfElementsToDraw; ++index)
      {
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated method
        if (this.elementsToDraw[index] != Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESCHWINDIGKEIT_SCHIEBEHILFE_VORWAERTS) && this.elementsToDraw[index] != Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.STANDARD_UNTERSTUETZUNGSSTUFE))
          this.allParameters[this.elementsToDraw[index]].ReadableValue = (double) this.elementNumericUpDown[index].Value;
      }
    }

    public void updateUIWithDictionaryValues()
    {
      this.allParameters = Parameters.Instance.Dictionary;
      for (int index = 0; index < this.sizeOfElementsToDraw; ++index)
      {
        // ISSUE: reference to a compiler-generated method
        if (this.elementsToDraw[index] != Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.GESCHWINDIGKEIT_SCHIEBEHILFE_VORWAERTS))
        {
          // ISSUE: reference to a compiler-generated method
          if (this.elementsToDraw[index] != Parameters.Instance.PositionOfValueInDictionary(ParameterValueElements.STANDARD_UNTERSTUETZUNGSSTUFE))
          {
            try
            {
              this.elementNumericUpDown[index].Value = (Decimal) this.allParameters[this.elementsToDraw[index]].ReadableValue;
              this.numericUpDown_ValueChanged((object) this.elementNumericUpDown[index], EventArgs.Empty);
            }
            catch (Exception ex)
            {
              this.elementNumericUpDown[index].Value = (Decimal) this.allParameters[this.elementsToDraw[index]].ReadableDefaultValue;
            }
          }
        }
      }
      this.valueChanged = false;
    }

    private void numericUpDown_ValueChanged(object sender, EventArgs e)
    {
      this.valueChanged = true;
      NumericUpDown numericUpDown = (NumericUpDown) sender;
      numericUpDown.Value = (Decimal) ParameterValue.ApproximatedValue((double) numericUpDown.Value, (double) numericUpDown.Increment);
    }

    public double[] ReadableProfileValues
    {
      set
      {
        for (int index = 0; index < this.sizeOfElementsToDraw; ++index)
          this.elementNumericUpDown[index].Value = (Decimal) value[index] > this.elementNumericUpDown[index].Maximum || (Decimal) value[index] < this.elementNumericUpDown[index].Minimum ? (Decimal) this.allParameters[this.elementsToDraw[index]].ReadableDefaultValue : (Decimal) value[index];
        this.updateDictionaryWithUIValues();
      }
      get
      {
        this.updateDictionaryWithUIValues();
        double[] numArray = new double[this.sizeOfElementsToDraw];
        for (int index = 0; index < this.sizeOfElementsToDraw; ++index)
          numArray[index] = (double) this.elementNumericUpDown[index].Value;
        return numArray;
      }
    }

    public bool ValueWasChanged
    {
      get => this.valueChanged;
      set => this.valueChanged = value;
    }

    public void ResetNumericUpDownValues()
    {
      for (int index = 0; index < this.sizeOfElementsToDraw; ++index)
      {
        ParameterValue allParameter = this.allParameters[this.elementsToDraw[index]];
        this.elementNumericUpDown[index].Value = (Decimal) allParameter.ReadableDefaultValue;
        this.numericUpDown_ValueChanged((object) this.elementNumericUpDown[index], EventArgs.Empty);
      }
    }

    public string[] GetElementNumbericUpDownValues()
    {
      string[] strArray = new string[this.elementNumericUpDown.Length];
      for (int index = 0; index < this.elementNumericUpDown.Length; ++index)
        strArray[index] = string.Concat((object) this.elementNumericUpDown[index].Value);
      return strArray;
    }
  }
}
