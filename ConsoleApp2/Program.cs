using System;
using System.IO;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        string ftpUrl = "ftp://example.com/path/to/your/file.txt"; 
        string localFilePath = @"C:\path\to\local\file.txt";       
        string ftpUsername = "your_username";                      
        string ftpPassword = "your_password";                     

        try
        {
            byte[] fileContents = File.ReadAllBytes(localFilePath);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(response.StatusDescription);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
