// Decompiled with JetBrains decompiler
// Type: ZerroWare.MMIData
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using MMI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ZerroWare
{
  internal class MMIData
  {
    private static volatile MMIData instance = (MMIData) null;
    private static object mutex = new object();
    private Dictionary<int, ParameterValue> parameters;

    public static MMIData Instance
    {
      get
      {
        if (MMIData.instance == null)
        {
          lock (MMIData.mutex)
          {
            if (MMIData.instance == null)
              MMIData.instance = new MMIData();
          }
        }
        return MMIData.instance;
      }
    }

    private MMIData()
    {
      this.MotorErrorBlocks = new byte[0][];
      this.AccuErrorBlocks = new byte[0];
      this.parameters = new Dictionary<int, ParameterValue>();
    }

    public byte[][] MotorErrorBlocks { set; get; }

    public byte[] AccuErrorBlocks { set; get; }

    public void SetParameters(Dictionary<int, ParameterValue> parameter)
    {
      lock (MMIData.mutex)
      {
        this.parameters.Clear();
        foreach (KeyValuePair<int, ParameterValue> keyValuePair in parameter)
          this.parameters.Add(keyValuePair.Key, new ParameterValue(keyValuePair.Value));
      }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Dictionary<int, ParameterValue> MMIParameters() => this.parameters;

    public ParameterValue GetParameterValueObject(
      ParameterIds id,
      int parameterPosition)
    {
      foreach (KeyValuePair<int, ParameterValue> parameter in this.parameters)
      {
        if (parameter.Value.ParameterID == id && parameter.Value.PositionOfParamter == parameterPosition)
          return parameter.Value;
      }
      return (ParameterValue) null;
    }

    public ParameterValue GetParameterValueObject(int dictionaryID) => this.parameters[dictionaryID];

    public void SetParameterValueObject(int dictionaryID, ParameterValue value) => this.parameters[dictionaryID] = value;

    public string MotorFirmwareVersion
    {
      get
      {
        string empty = string.Empty;
        string str;
        try
        {
          str = (empty + (object) this.GetParameterValueObject(ParameterIds.MOTOR_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ANTRIEB)).ReadableValue).Replace(',', '.');
          if (str.IndexOf('.') < 0)
            str += ".0";
        }
        catch (Exception ex)
        {
          str = "0.0";
        }
        return str;
      }
    }

    public string SavedMotorFirmwareVersion { set; get; }

    public string AccuDFIVersion
    {
      get
      {
        byte[] numArray = new byte[2]{ (byte) 0, (byte) 0 };
        StringBuilder stringBuilder = new StringBuilder(numArray.Length * 2);
        if (this.AccuErrorBlocks.Length != 0)
        {
          numArray[0] = this.AccuErrorBlocks[this.AccuErrorBlocks.Length - 2];
          numArray[1] = this.AccuErrorBlocks[this.AccuErrorBlocks.Length - 1];
        }
        stringBuilder.AppendFormat("0x{0:X2}", (object) numArray[0]);
        stringBuilder.Append(" ");
        stringBuilder.AppendFormat("0x{0:X2}", (object) numArray[1]);
        return stringBuilder.ToString();
      }
    }

    public string SavedAccuDFIVersion { set; get; }

    public string AccuFirmwareVersion
    {
      get
      {
        string empty = string.Empty;
        string str;
        try
        {
          str = (empty + (object) this.GetParameterValueObject(ParameterIds.ACCU_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_ACCU)).ReadableValue).Replace(',', '.');
          if (str.IndexOf('.') < 0)
            str += ".0";
        }
        catch (Exception ex)
        {
          str = "0.0";
        }
        return str;
      }
    }

    public string SavedAccuFirmwareVersion { set; get; }

    public string MMIFirmwareVersion
    {
      get
      {
        string empty = string.Empty;
        try
        {
          return empty + (object) this.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_MMI_A)).ReadableValue + "." + (object) this.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_MMI_B)).ReadableValue + "." + (object) this.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_MMI_C)).ReadableValue + "." + (object) this.GetParameterValueObject(ParameterIds.MMI_VERSION_INFORMATION, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.SOFTWAREVERSION_MMI_D)).ReadableValue;
        }
        catch (Exception ex)
        {
          return "0.0.0.0";
        }
      }
    }

    public bool ImmobilizerActive => ((int) (byte) this.GetParameterValueObject(ParameterIds.MMI_DEFAULT_SETTINGS, Parameters.Instance.PositionOfValueInElement(ParameterValueElements.MMI_DEFAULT_SETTINGS)).ReadableValue & 16) != 0;

    public string MMISerialNumber { set; get; }
  }
}
