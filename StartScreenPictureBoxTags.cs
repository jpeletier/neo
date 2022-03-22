// Decompiled with JetBrains decompiler
// Type: ZerroWare.StartScreenPictureBoxTags
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

namespace ZerroWare
{
  internal class StartScreenPictureBoxTags
  {
    public StartScreenPictureBoxTags(int position, string listType, int startScreenElement)
    {
      this.Position = position;
      this.StartScreenElement = startScreenElement;
      this.ListType = listType;
    }

    public int Position { set; get; }

    public int StartScreenElement { set; get; }

    public string ListType { set; get; }
  }
}
