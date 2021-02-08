using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;


public class ftpManager : MonoBehaviour
{
    public static ftpManager instance = null;
    private string host = null;
    private string user = null;
    private string pass = null;
    private FtpWebRequest request = null;
    private FtpWebResponse response = null;
    private Stream stream = null;
    private int bufferSize = 2048;

    public ftpManager(string hostIP, string userName, string password) {
        if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        host = hostIP;
        user = userName;
        pass = password;

    }

    public void download(string remoteFile, string localFile)
    {
        try
        {
            request = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            request.Credentials = new NetworkCredential(user, pass);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            response = (FtpWebResponse)request.GetResponse();
            stream = response.GetResponseStream();
            FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            byte[] byteBuffer = new byte[bufferSize];
            int bytesRead = stream.Read(byteBuffer, 0, bufferSize);
            try
            {
                while (bytesRead > 0)
                {
                    localFileStream.Write(byteBuffer, 0, bytesRead);
                    bytesRead = stream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            localFileStream.Close();
            stream.Close();
            response.Close();
            request = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

    public void upload(string remoteFile, string localFile)
    {
        try
        {
            request = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
            request.Credentials = new NetworkCredential(user, pass);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            stream = request.GetRequestStream();
            FileStream localFileStream = new FileStream(localFile, FileMode.Create);
            byte[] byteBuffer = new byte[bufferSize];
            int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
             try
            {
                while (bytesSent != 0)
                {
                    stream.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            localFileStream.Close();
            stream.Close();
            request = null;
        }
        catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        return;
    }

   
   
   }