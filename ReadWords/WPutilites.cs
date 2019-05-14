using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSCP;
using System.Text.RegularExpressions;

namespace ReadWords
{
    public class WPutilites
    {
        // загружаем изображения на FTP
        public static string[] FTPUploadFile(string newFolder,
                                    string hN,
                                    string uN,
                                    string pW,
                                    string rP,
                                    string replaceOld,
                                    string replaceNew,
                                    string pxW)
        {
            string[] r = { "", "" };
            int i = 0;
            string res = string.Empty;
            try
            {
                // Задать параметры сессии
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = hN,
                    PortNumber = 21,
                    UserName = uN,
                    Password = pW,
                };

                string localPath = newFolder;
                string remotePath = rP;

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    // Enumerate files and directories to upload
                    IEnumerable<FileSystemInfo> fileInfos =
                        new DirectoryInfo(localPath).EnumerateFileSystemInfos(
                            "*", SearchOption.AllDirectories);

                    foreach (FileSystemInfo fileInfo in fileInfos)
                    {
                        string remoteFilePath =
                            RemotePath.TranslateLocalPathToRemote(
                                fileInfo.FullName, localPath, remotePath);

                        if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
                        {
                            // Create remote subdirectory, if it does not exist yet
                            if (!session.FileExists(remoteFilePath))
                            {
                                session.CreateDirectory(remoteFilePath);
                            }
                        }
                        else
                        {
                            //Console.WriteLine("Moving file {0}...", fileInfo.FullName);                            
                            // Upload file and remove original
                            session.PutFiles(fileInfo.FullName, remoteFilePath, true).Check();
                            res += remoteFilePath + "\r\n";
                            if (i == 0)
                                r[1] = remoteFilePath.Replace(replaceOld, replaceNew);
                            i = 999;
                        }
                    }
                }

                if ( pxW != "0" )
                {
                    res = res.Replace(replaceOld, @"<p><img style=""display: block; margin-left: auto; margin-right: auto; "" src=""" + replaceNew);
                    res = res.Replace(".jpg", @".jpg"" width = """ + pxW + @"""/></p>");
                    r[0] = res;                    
                }
                else
                {
                    res = replaceNew;
                }


                return r;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return r;
            }
            // MessageBox.Show("Загрузили");

        }

        // PublicPostToWordpress
        public static string UploadPHP(string localFile, 
                                string hN, 
                                string uN,
                                string pW,
                                string remotePath)
        {
            string res = string.Empty;
            try
            {
                // Задать параметры сессии
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = hN,
                    PortNumber = 21,
                    UserName = uN,
                    Password = pW,
                };

                // string localPath = newFolder;
                // string remotePath = rP;

                using (Session session = new Session())
                {
                    // Connect
                    session.Open(sessionOptions);

                    // Upload files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult;
                    transferResult =
                        session.PutFiles(localFile, remotePath, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    // Print results
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        Console.WriteLine("Upload of {0} succeeded", transfer.FileName);
                        res = transfer.FileName;
                    }                                      
                }

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return string.Empty;
            }
            // MessageBox.Show("Загрузили");
        }

        ////////////////////
        /// docTitle
        /// 
        public static string docTitle(string s, int i)
        {
            string sT = string.Empty;
            Regex regex;
            MatchCollection matches;
            switch (i)
            {
                case 1:
                    regex = new Regex(@"решен(\w*)|постановл(\w*)|распоряж(\w*)");
                    matches = regex.Matches(s.ToLower());
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                            sT += " " + match.Value;
                        sT = sT.Trim();
                        return sT.ToUpper();
                    }
                    else
                    {
                        return string.Empty;
                    }
                case 2:
                    regex = new Regex(@"(\d+)\D*(январ[ьея]|феврал[ьея]|март[еа]?|апрел[ьея]|ма[йея]|ию[нл][яье]|август[еа]?|(?:сент|окт|но|дек)[ая]бр[яье])(\D*\d+)\D*(\d+)", RegexOptions.IgnoreCase);
                    matches = regex.Matches(s);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            GroupCollection groups = match.Groups;
                            sT += " № " + groups[4].Value.Trim();
                            sT += " от " + groups[1].Value.Trim();
                            sT += " " + groups[2].Value.Trim();
                            sT += " " + groups[3].Value.Trim();
                        }
                        sT = sT.ToLower();
                    }
                    return sT;
                case 3:
                    return " " + s.Trim();
                default:
                    return string.Empty;
            }
        }

        public static string[] toAT(string dirName)
        {
            string[] r = new string[1];
            r[0] = "empty";
            if (Directory.Exists(dirName))
            {
                string[] files = Directory.GetFiles(dirName);

                string[] names = new string[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    names[i] = files[i];
                }
                return names;
            }
            else return r;
        }
    }
}
