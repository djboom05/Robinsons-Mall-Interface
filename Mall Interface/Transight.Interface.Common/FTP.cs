using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Transight.Interface.Common
{
    public static class FTP
    {
        private static string address;
        private static string username;
        private static string password;

        public static void SetDetails(string Address, string Username, string Password)
        {
            if (string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Username) || Password == null)
            {
                ErrorTracking.Log("[FTP/SetDetails] FTP details not complete.");
                throw new ApplicationException("FTP details not complete.");
            }

            address = Address;
            username = Username;
            password = Password;
        }

        /// <summary>
        /// Return the name of files in the FTP address. Support Windows FTP server only. 
        /// </summary>
        /// <returns></returns>
        public static List<string> ListDirectoryFiles() 
        {
            List<string> fileList = new List<string> { };

            if (address == null || username == null || password == null) 
                throw new ApplicationException("FTP details not set.");

            try
            {
                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(address);
                ftpReq.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftpReq.Credentials = new NetworkCredential(username, password);
                FtpWebResponse ftpRes = (FtpWebResponse)ftpReq.GetResponse();
                Stream stream = ftpRes.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                string s;

                while (!reader.EndOfStream)
                {
                    s = reader.ReadLine();

                    if (s.Substring(24, 5) != "<DIR>") //not directory
                        fileList.Add(s.Substring(39, s.Length - 39));
                }

                stream.Close();
                stream.Dispose();
                reader.Close();
                reader.Dispose();
                ftpRes.Close();

                return fileList;
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[FTP/ListDirectoryFiles] Error listing FTP files.");
                ErrorTracking.Log(ex);
                throw new ApplicationException("Error listing FTP files.");
            }
        }

        public static void DownloadFile(string FileName, string LocalFileFullName)
        {
            try 
            {
                FileStream fileStream = new FileStream(LocalFileFullName, FileMode.Create);
                string downloadPath = Func.IIf(address.Substring(address.Length - 1, 1) == "/", address + FileName, address + "/" + FileName);

                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(downloadPath);
                ftpReq.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpReq.UseBinary = true;
                ftpReq.Credentials = new NetworkCredential(username, password);
                FtpWebResponse ftpRes = (FtpWebResponse)ftpReq.GetResponse();
                Stream stream = ftpRes.GetResponseStream();

                int bufferSize = 2048;
                byte[] buffer = new byte[bufferSize];
                int readCount = stream.Read(buffer, 0, bufferSize);

                while (readCount > 0)
                {
                    fileStream.Write(buffer, 0, readCount);
                    readCount = stream.Read(buffer, 0, bufferSize);
                }

                stream.Close();
                stream.Dispose();
                fileStream.Close();
                fileStream.Dispose();
                ftpRes.Close();
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[FTP/DownloadFile] Error downloading file from FTP server [" + LocalFileFullName + "].");
                ErrorTracking.Log(ex);
                throw new ApplicationException("Error downloading file from FTP server.");
            }
        }

        public static void UploadFile(string LocalFileFullName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(LocalFileFullName);
                string uploadPath = Func.IIf(address.Substring(address.Length - 1, 1) == "/", address + fileInfo.Name, address + "/" + fileInfo.Name);

                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(uploadPath);
                ftpReq.Method = WebRequestMethods.Ftp.UploadFile;
                ftpReq.UseBinary = true;
                ftpReq.KeepAlive = false;
                ftpReq.Credentials = new NetworkCredential(username, password);
                ftpReq.ContentLength = fileInfo.Length;
                Stream stream = ftpReq.GetRequestStream();

                int bufferSize = 2048; //set buffer size to 2kb
                byte[] buffer = new byte[bufferSize];
                int bytesRead;

                FileStream fileStream = fileInfo.OpenRead();

                //read from the file stream 2kb at a time
                bytesRead = fileStream.Read(buffer, 0, bufferSize);

                //till Stream content ends
                while (bytesRead != 0)
                {
                    // Write Content from the file stream to the FTP Upload stream
                    stream.Write(buffer, 0, bytesRead);
                    bytesRead = fileStream.Read(buffer, 0, bufferSize);
                }

                stream.Close();
                stream.Dispose();
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[FTP/UploadFile] Error uploading file to FTP server [" + LocalFileFullName + "].");
                ErrorTracking.Log(ex);
                throw new ApplicationException("Error uploading file to FTP server.");
            }
        }

        public static void DeleteFile(string FileName)
        {
            try
            {
                string ftpFilePath = Func.IIf(address.Substring(address.Length - 1, 1) == "/", address + FileName, address + "/" + FileName);

                FtpWebRequest ftpReq = (FtpWebRequest)WebRequest.Create(ftpFilePath);
                ftpReq.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpReq.Credentials = new NetworkCredential(username, password);
                FtpWebResponse ftpRes = (FtpWebResponse)ftpReq.GetResponse();
                ftpRes.Close();
            }
            catch (Exception ex)
            {
                ErrorTracking.Log("[FTP/DeleteFile] Error deleting file from FTP server [" + FileName + "].");
                ErrorTracking.Log(ex);
                throw new ApplicationException("Error deleting file from FTP server.");
            }
        }
    }
}
