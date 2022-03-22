// Decompiled with JetBrains decompiler
// Type: ZerroWare.HashGenerating
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System;
using System.Security.Cryptography;
using System.Text;

namespace ZerroWare
{
  internal class HashGenerating
  {
    public static string ComputeHash(string plainText, byte[] saltBytes)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(plainText);
      byte[] buffer = new byte[bytes.Length + saltBytes.Length];
      for (int index = 0; index < bytes.Length; ++index)
        buffer[index] = bytes[index];
      for (int index = 0; index < saltBytes.Length; ++index)
        buffer[bytes.Length + index] = saltBytes[index];
      byte[] numArray = (byte[]) null;
      using (HashAlgorithm hashAlgorithm = (HashAlgorithm) new SHA512Managed())
        numArray = hashAlgorithm.ComputeHash(buffer);
      byte[] inArray = new byte[numArray.Length + saltBytes.Length];
      for (int index = 0; index < numArray.Length; ++index)
        inArray[index] = numArray[index];
      for (int index = 0; index < saltBytes.Length; ++index)
        inArray[numArray.Length + index] = saltBytes[index];
      return Convert.ToBase64String(inArray);
    }

    public static bool VerifyHash(string plainText, string hashValue)
    {
      byte[] numArray = Convert.FromBase64String(hashValue);
      int num = 512 / 8;
      if (numArray.Length < num)
        return false;
      byte[] saltBytes = new byte[numArray.Length - num];
      for (int index = 0; index < saltBytes.Length; ++index)
        saltBytes[index] = numArray[num + index];
      string hash = HashGenerating.ComputeHash(plainText, saltBytes);
      return hashValue == hash;
    }

    public static byte[] DefaultSaltByte() => new byte[8]
    {
      (byte) 174,
      (byte) 33,
      (byte) 245,
      (byte) 70,
      (byte) 93,
      (byte) 211,
      (byte) 106,
      (byte) 147
    };
  }
}
