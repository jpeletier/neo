// Decompiled with JetBrains decompiler
// Type: ZerroWare.HttpAccess
// Assembly: neo smartDiagnostic, Version=3.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 70CB7B0B-DEF6-498D-9868-6C83F964A51F
// Assembly location: C:\Users\Jpel\Downloads\neo smartDiagnostic\neo smartDiagnostic.exe

using FileHandling;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ZerroWare
{
  internal class HttpAccess
  {
    private WebRequest request;

    public event HttpAccess.ConnectionErrorInformationHandler ConnectionError;

    public event HttpAccess.DataPackageReceivedHanlder DataPackageReceive;

    public HttpAccess(string urlForRequest, WebSettings webSettings)
    {
      this.request = WebRequest.Create(urlForRequest);
      ((HttpWebRequest) this.request).ProtocolVersion = HttpVersion.Version10;
      this.request.Method = "POST";
      this.ProxySettings(webSettings);
    }

    public void ProxySettings(WebSettings webSettings)
    {
      switch (webSettings.TypeOfProxy)
      {
        case WebSettings.ProxyType.NO_PROXY:
          this.SetEmptyProxyForRequest();
          break;
        case WebSettings.ProxyType.DEFAULT_PROXY:
          this.SetProxyForRequest();
          break;
        case WebSettings.ProxyType.MANUAL_PROXY:
          this.SwitchingSpecialProxySettings(webSettings);
          break;
        default:
          this.SetEmptyProxyForRequest();
          break;
      }
    }

    private void SwitchingSpecialProxySettings(WebSettings webSettings)
    {
      string empty = string.Empty;
      string str = webSettings.ProxyURL.StartsWith("http://") ? webSettings.ProxyURL : "http://" + webSettings.ProxyURL;
      if (str.EndsWith("/"))
        str = str.Substring(0, str.Length - 1);
      string uri = str + ":" + (object) webSettings.Port + "/";
      switch (webSettings.TypeOfAuth)
      {
        case WebSettings.AuthType.NO_AUTH:
          this.SetProxyForRequest(uri, false);
          break;
        case WebSettings.AuthType.ACTUAL_USER:
          this.SetProxyForRequest(uri, true);
          break;
        case WebSettings.AuthType.GIVEN_USER:
          if (string.IsNullOrEmpty(webSettings.Domain))
          {
            this.SetProxyForRequest(uri, webSettings.UserName, webSettings.Password);
            break;
          }
          this.SetProxyForRequest(uri, webSettings.UserName, webSettings.Password, webSettings.Domain);
          break;
        default:
          this.SetProxyForRequest(uri, false);
          break;
      }
    }

    public void SetProxyForRequest()
    {
      this.request.Proxy = (IWebProxy) new WebProxy(WebRequest.GetSystemWebProxy().GetProxy(this.request.RequestUri), false, (string[]) null, CredentialCache.DefaultCredentials);
      this.request.UseDefaultCredentials = true;
    }

    public void SetEmptyProxyForRequest()
    {
      this.request.Proxy = (IWebProxy) null;
      this.request.Credentials = (ICredentials) null;
      this.request.UseDefaultCredentials = false;
    }

    public void SetProxyForRequest(string uri, bool useDefaultCredentials)
    {
      if (useDefaultCredentials)
      {
        this.request.Proxy = (IWebProxy) new WebProxy(uri, false, (string[]) null, CredentialCache.DefaultCredentials);
        this.request.UseDefaultCredentials = true;
      }
      else
      {
        this.request.Proxy = (IWebProxy) new WebProxy(uri, false, (string[]) null, (ICredentials) null);
        this.request.Credentials = (ICredentials) null;
        this.request.UseDefaultCredentials = false;
      }
    }

    public void SetProxyForRequest(string uri, string username, string password)
    {
      NetworkCredential networkCredential = new NetworkCredential(username, password);
      this.request.Proxy = (IWebProxy) new WebProxy(uri, false, (string[]) null, (ICredentials) networkCredential);
      this.request.Credentials = (ICredentials) networkCredential;
      this.request.UseDefaultCredentials = false;
    }

    public void SetProxyForRequest(string uri, string username, string password, string domain)
    {
      NetworkCredential networkCredential = new NetworkCredential(username, password, domain);
      this.request.Proxy = (IWebProxy) new WebProxy(uri, false, (string[]) null, (ICredentials) networkCredential);
      this.request.Credentials = (ICredentials) networkCredential;
      this.request.UseDefaultCredentials = false;
    }

    public string Request(HttpAccess.RequestCommand command, string content, bool silent = false)
    {
      this.request.ContentType = "application/x-www-form-urlencoded";
      string empty = string.Empty;
      string str1;
      switch (command)
      {
        case HttpAccess.RequestCommand.CHECK_UPDATE:
          str1 = "cmd=check_upd&";
          break;
        case HttpAccess.RequestCommand.GET_UPDATE:
          str1 = "cmd=get_upd&";
          break;
        case HttpAccess.RequestCommand.ET:
          str1 = "cmd=rec_et&";
          break;
        case HttpAccess.RequestCommand.LICENSE:
          str1 = "cmd=lic&";
          break;
        case HttpAccess.RequestCommand.PING:
          str1 = "cmd=ping&";
          break;
        default:
          str1 = string.Empty;
          break;
      }
      content = str1 + content;
      byte[] bytes = Encoding.UTF8.GetBytes(content);
      this.request.ContentLength = (long) bytes.Length;
      string str2 = string.Empty;
      try
      {
        using (Stream requestStream = this.request.GetRequestStream())
          requestStream.Write(bytes, 0, bytes.Length);
        using (WebResponse response = this.request.GetResponse())
        {
          if (((HttpWebResponse) response).StatusDescription == HttpStatusCode.OK.ToString())
          {
            Stream stream = (Stream) null;
            try
            {
              stream = response.GetResponseStream();
              using (StreamReader streamReader = new StreamReader(stream))
              {
                stream = (Stream) null;
                str2 = streamReader.ReadToEnd();
              }
            }
            finally
            {
              stream?.Dispose();
            }
          }
        }
      }
      catch (WebException ex)
      {
        if (silent)
          return "WebException";
        if (this.ConnectionError != null)
          this.ConnectionError(this, ex.Message);
        GlobalLogger.Instance.WriteLine((Exception) ex);
        UniqueError.Message(UniqueError.Number.HTTP_REQUEST, (Exception) ex, GlobalResource.WebException_Check_Proxy_Settings);
        return "WebException";
      }
      return str2;
    }

    public byte[] DataRequest(string content, ref string fileName, bool silent = false)
    {
      this.request.ContentType = "application/x-www-form-urlencoded";
      byte[] bytes = Encoding.UTF8.GetBytes(content);
      this.request.ContentLength = (long) bytes.Length;
      byte[] numArray = (byte[]) null;
      try
      {
        using (Stream requestStream = this.request.GetRequestStream())
          requestStream.Write(bytes, 0, bytes.Length);
        using (WebResponse response = this.request.GetResponse())
        {
          if (((HttpWebResponse) response).StatusDescription == HttpStatusCode.OK.ToString())
          {
            Stream stream = (Stream) null;
            try
            {
              bool flag = false;
              int contentLength = 0;
              for (int index = 0; index < response.Headers.Count; ++index)
              {
                Console.WriteLine("\nHeader Name:{0}, Header value :{1}", (object) response.Headers.Keys[index], (object) response.Headers[index]);
                if (response.Headers[index].IndexOf("filename=") > 0)
                {
                  fileName = response.Headers[index].Substring(response.Headers[index].IndexOf('=') + 2);
                  fileName = fileName.Substring(0, fileName.Length - 1);
                }
                else if (response.Headers.Keys[index] == "Content-Length")
                  contentLength = Convert.ToInt32(response.Headers[index]);
                else if (response.Headers.Keys[index] == "Content-Type" && response.Headers[index] == "application/zip")
                  flag = true;
              }
              if (flag)
              {
                stream = response.GetResponseStream();
                using (StreamReader streamReader = new StreamReader(stream))
                {
                  stream = (Stream) null;
                  numArray = this.ReadFully(streamReader.BaseStream, contentLength);
                }
              }
            }
            finally
            {
              stream?.Dispose();
            }
          }
        }
      }
      catch (WebException ex)
      {
        if (silent)
          return (byte[]) null;
        if (this.ConnectionError != null)
          this.ConnectionError(this, ex.Message);
        GlobalLogger.Instance.WriteLine((Exception) ex);
        UniqueError.Message(UniqueError.Number.HTTP_DATAREQUEST, (Exception) ex, GlobalResource.WebException_Check_Proxy_Settings);
        return (byte[]) null;
      }
      return numArray;
    }

    public bool TransmitData(
      string hardwareId,
      string activationDate,
      string hashedLicense,
      string softwareVersion,
      string filename,
      bool silent = false)
    {
      if (!System.IO.File.Exists(filename))
        return true;
      string str = "----------------------------" + DateTime.Now.Ticks.ToString("x");
      this.request.ContentType = "multipart/form-data; boundary=" + str;
      byte[] bytes1 = Encoding.ASCII.GetBytes("\r\n--" + str + "\r\n");
      try
      {
        using (Stream memStream = (Stream) new MemoryStream())
        {
          memStream.Write(bytes1, 0, bytes1.Length);
          this.AddFormData(memStream, bytes1, "cmd", "rec_et");
          this.AddFormData(memStream, bytes1, "hw", hardwareId);
          this.AddFormData(memStream, bytes1, "time", activationDate);
          this.AddFormData(memStream, bytes1, "lic", hashedLicense);
          this.AddFormData(memStream, bytes1, "version", softwareVersion);
          byte[] bytes2 = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: attachment; name=\"{0}\"; filename=\"{1}\"\r\n; Content-Type: application/octet-stream\r\n\r\n", (object) "etfile", (object) filename));
          memStream.Write(bytes2, 0, bytes2.Length);
          using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
          {
            byte[] buffer = new byte[1024];
            int count;
            while ((count = fileStream.Read(buffer, 0, buffer.Length)) != 0)
              memStream.Write(buffer, 0, count);
            fileStream.Close();
          }
          byte[] bytes3 = Encoding.ASCII.GetBytes("\r\n--" + str + "--");
          memStream.Write(bytes3, 0, bytes3.Length);
          this.request.ContentLength = memStream.Length;
          using (Stream requestStream = this.request.GetRequestStream())
          {
            memStream.Position = 0L;
            byte[] buffer = new byte[memStream.Length];
            memStream.Read(buffer, 0, buffer.Length);
            memStream.Close();
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();
          }
        }
        using (WebResponse response = this.request.GetResponse())
        {
          string end = new StreamReader(response.GetResponseStream()).ReadToEnd();
          response.Close();
          if (!end.Contains("ET DATA RECEIVED"))
            return false;
        }
      }
      catch (WebException ex)
      {
        if (silent)
          return false;
        if (this.ConnectionError != null)
          this.ConnectionError(this, ex.Message);
        GlobalLogger.Instance.WriteLine((Exception) ex);
        UniqueError.Message(UniqueError.Number.HTTP_TRANSMITDATA, (Exception) ex, GlobalResource.WebException_Check_Proxy_Settings);
        return false;
      }
      catch (OutOfMemoryException ex)
      {
        if (silent)
          return false;
        if (this.ConnectionError != null)
          this.ConnectionError(this, ex.Message);
        GlobalLogger.Instance.WriteLine((Exception) ex);
        UniqueError.Message(UniqueError.Number.HTTP_OUTOFMEMORYEXCEPTION, (Exception) ex);
        return false;
      }
      return true;
    }

    private void AddFormData(
      Stream memStream,
      byte[] boundarybytes,
      string formDataName,
      string formDataValue)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}", (object) formDataName, (object) formDataValue));
      memStream.Write(bytes, 0, bytes.Length);
      memStream.Write(boundarybytes, 0, boundarybytes.Length);
    }

    private byte[] ReadFully(Stream stream, int contentLength)
    {
      byte[] buffer = new byte[32768];
      int num = 0;
      int percentage = 0;
      using (MemoryStream memoryStream = new MemoryStream(contentLength))
      {
        while (true)
        {
          try
          {
            int count = stream.Read(buffer, 0, buffer.Length);
            num += count;
            if (percentage != (int) ((double) num / (double) contentLength * 100.0))
            {
              percentage = (int) ((double) num / (double) contentLength * 100.0);
              if (percentage < 100 && this.DataPackageReceive != null)
                this.DataPackageReceive(this, percentage);
            }
            if (count <= 0)
            {
              try
              {
                if (this.DataPackageReceive != null)
                  this.DataPackageReceive(this, 100);
                return memoryStream.ToArray();
              }
              catch (OutOfMemoryException ex)
              {
                GlobalLogger.Instance.WriteLine((Exception) ex);
                UniqueError.Message(UniqueError.Number.HTTP_READ_OUTOFMEMORYEXCEPTION, (Exception) ex);
                return (byte[]) null;
              }
            }
            else
              memoryStream.Write(buffer, 0, count);
          }
          catch (IOException ex)
          {
            if (this.ConnectionError != null)
              this.ConnectionError(this, ex.Message);
            throw new WebException(ex.Message);
          }
        }
      }
    }

    public delegate void ConnectionErrorInformationHandler(HttpAccess sender, string information);

    public enum RequestCommand
    {
      CHECK_UPDATE,
      GET_UPDATE,
      ET,
      LICENSE,
      PING,
    }

    public delegate void DataPackageReceivedHanlder(HttpAccess sender, int percentage);
  }
}
