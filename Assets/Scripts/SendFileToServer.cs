using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;

public class SendFileToServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sendFileToFTPServer03("Data.csv", Application.persistentDataPath);
    }

    public void sendFileToFTPServer01(string fileName, string filePath)
    {
        Debug.Log("File Path : " + filePath + "/" + fileName);
        Debug.Log("File Uploading...");

        FileInfo fileInfo = new FileInfo(fileName);

        // Setup request and credentials
        string url = "https://demo.gestureresearch.com/xealistic/textures/" + fileName;
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
        request.Method = WebRequestMethods.Ftp.UploadFile;
        request.Credentials = new NetworkCredential("vexpocmy", "Gesture@!2345");

        // Create two stream one for Data file and another for request
        Stream ftpStream = request.GetRequestStream();
        FileStream file = File.OpenRead(filePath + "/" + fileName);

        // Variables for file upload
        int length = 1024;
        byte[] buffer = new byte[length];
        int bytesRead = 0;

        // write the file to the request stream
        do
        {
            bytesRead = file.Read(buffer, 0, length);
            ftpStream.Write(buffer, 0, bytesRead);
        }

        while (bytesRead != 0);

        // close streams
        file.Close();
        ftpStream.Close();

        Debug.Log("File Uploaded.");
        //try
        //{

        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.Message);
        //}
    }

    public void sendFileToFTPServer02(string fileName, string filePath)
    {
        Debug.Log("File Path : " + filePath + "/" + fileName);
        Debug.Log("File Uploading...");

        FileInfo fileInfo = new FileInfo(fileName);

        // Setup request and credentials
        string url = "https://demo.gestureresearch.com/xealistic/textures/" + fileName;
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
        request.Credentials = new NetworkCredential("vexpocmy", "Gesture@!2345");
        request.Method = WebRequestMethods.Ftp.UploadFile;

        using (Stream fileStream = File.OpenRead(filePath + "/" + fileName))
        using (Stream ftpStream = request.GetRequestStream())
        {
            byte[] buffer = new byte[10240];
            int read;
            while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ftpStream.Write(buffer, 0, read);
                Console.WriteLine("Uploaded {0} bytes", fileStream.Position);
            }
        }

        Debug.Log("File Uploaded.");
        //try
        //{

        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.Message);
        //}
    }

    public void sendFileToFTPServer03(string fileName, string filePath)
    {
        Debug.Log("File Path : " + filePath + "/" + fileName);
        Debug.Log("File Uploading...");

        string url = "https://demo.gestureresearch.com/xealistic/textures/" + fileName;
        string fileCompletePath = filePath + "/" + fileName;

        WebClient client = new WebClient();
        client.Credentials = new NetworkCredential("vexpocmy", "Gesture@!2345");
        client.UploadFile(url, fileCompletePath);

        Debug.Log("File Uploaded.");
        //try
        //{

        //}
        //catch (Exception e)
        //{
        //    Debug.Log(e.Message);
        //}
    }
}
