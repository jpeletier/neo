// Decompiled with JetBrains decompiler
// Type: ZerroWare.BikeAnimation
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using ZerroWare.Properties;

namespace ZerroWare
{
  internal class BikeAnimation
  {
    public event BikeAnimation.AnimatedSequenceElement AnimatedElement;

    public BikeAnimation(Image startImage, Image endImage)
    {
      this.StartImage = startImage;
      this.EndImage = endImage;
    }

    public Image StartImage { set; get; }

    public Image EndImage { set; get; }

    public void Start()
    {
      BikeAnimationEventArgs e = new BikeAnimationEventArgs();
      PointF offset = new PointF((float) (this.StartImage.Width / 2), (float) (this.StartImage.Height / 2));
      Image startImage = this.StartImage;
      for (int index = 0; index <= 185; index += 2)
      {
        e.Animated = HelperClass.RotateImage(startImage, offset, (float) index);
        if (this.AnimatedElement != null)
          this.AnimatedElement((object) this, e);
        Thread.Sleep(10);
      }
      e.Animated = this.EndImage;
      if (this.AnimatedElement != null)
        this.AnimatedElement((object) this, e);
      Thread.Sleep(10);
      try
      {
        new SoundPlayer()
        {
          Stream = ((Stream) Resources.easteregg)
        }.Play();
      }
      catch (Exception ex)
      {
      }
    }

    public delegate void AnimatedSequenceElement(object sender, BikeAnimationEventArgs e);
  }
}
