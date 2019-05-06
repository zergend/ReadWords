using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSCP;

using YandexDiskNET;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net;

namespace ReadWords
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            UploadToYaDisk();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        async void UploadToYaDisk()
        {
            string oauth = "AQAAAAAHhRICAAWZSNtoJ5RZgkjxi7cp1fye9VE";
            string diskFileName = "test/Итоги викторины. Протоколы жюри по возрастным номинациям.pdf";
            string myFileName = @"D:\dnlds\Итоги викторины. Протоколы жюри по возрастным номинациям.pdf";


            YandexDiskRest disk = new YandexDiskRest(oauth);

            // var err = disk.DownloadResource(sourceFileName, destFileName);
            /* 
            if (err.Message == null)
                textBox.Text += string.Format("Success downloaded {0}", Path.GetFileName(sourceFileName)) + "\r\n";
            else
                textBox.Text += string.Format(err.Message) + "\r\n";
             */


            // var err = disk.CreateFolder(folder);
            var err = disk.UploadResource(diskFileName, myFileName);
            if (err.Message == null)
                textBox.Text += string.Format("{0} upload successful.", diskFileName) + "\r\n";
            else
                textBox.Text += string.Format(err.Message) + "\r\n";

            err = disk.PublicResource(diskFileName);
            if (err.Message == null)
                textBox.Text += string.Format("{0} public successful.", diskFileName) + "\r\n";
            else
                textBox.Text += string.Format(err.Message) + "\r\n";

            ResInfo filesByPublicFields = disk.GetResourcePublic(
                3,
                null,
                new ResFields[] {
                    ResFields.Name,
                    ResFields.Path,
                    ResFields.Public_url
                },
                0
                );

            if (filesByPublicFields.ErrorResponse.Message == null)
            {
                textBox.Text += string.Format("{0} - {1}", filesByPublicFields.Name, filesByPublicFields.Public_url) + "\r\n";

                if (filesByPublicFields._Embedded.Items.Count != 0)
                    foreach (var s in filesByPublicFields._Embedded.Items)
                        textBox.Text += string.Format("{0} - {1} - {2}", s.Name, s.Path, s.Public_url) + "\r\n";
            }
            else
                textBox.Text += string.Format(filesByPublicFields.ErrorResponse.Message);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int countRow = 0;

            if (openFile1.ShowDialog() == DialogResult.OK)
            {
                // Open document 
                string originalfilename = System.IO.Path.GetFullPath(openFile1.FileName);

                /* countTitle: № поиска подстроки = № элемента в заголовке
                 * 1 - тип документа (начальное значение) 
                 * 2 - дата документа   \
                 *   +                   > как правило, в одном абзаце !!!
                 *   - номер документа  /
                 * 3 - название документа
                 */

                int countTitle = 1;

                if (openFile1.CheckFileExists && new[] { ".docx", ".doc", ".txt", ".rtf" }.Contains(Path.GetExtension(originalfilename).ToLower()))
                {
                    object File = originalfilename;
                    object nullobject = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Application wordobject = new Microsoft.Office.Interop.Word.Application();
                    wordobject.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
                    Microsoft.Office.Interop.Word._Document docs = wordobject.Documents.Open(ref File, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject);


                    Microsoft.Office.Interop.Word.Range wordContentRange = docs.Content;

                    string textFromWordDocument = string.Empty;
                    string strDocTitle = string.Empty;

                    for (int i = 1; i <= wordContentRange.Paragraphs.Count; i++)
                    {
                        // !!! концом абзаца могут притворяться разрывы строк или куча пробелов
                        textFromWordDocument = wordContentRange.Paragraphs[i].Range.Text;
                        if (textFromWordDocument.Trim() == string.Empty)
                        {
                            // textBox.Text += i.ToString() + ": !___ПУСТОЙ ПАРАГРАФ___!" + "\r\n";
                        }
                        else
                        {
                            string title = docTitle(textFromWordDocument, countTitle);

                            switch (countTitle)
                            {
                                case 1:
                                case 2:
                                case 3: // тип документа + номер + дата + название
                                    if (title != string.Empty)
                                    {
                                        strDocTitle += title;
                                        countTitle++;
                                    }
                                    break;
                                default:
                                    // 
                                    break;
                            }

                            if (countTitle > 3)
                            {
                                listBox1.Items.Insert(countRow, strDocTitle);
                                countRow++;
                                break;
                            }

                            if (i > 25)
                            {
                                listBox1.Items.Insert(countRow, "? не НМПА ?");
                                countRow++;
                                break;
                            }

                        }

                    }

                    textBox.Text += strDocTitle + "\r\n";


                    docs.Close(ref nullobject, ref nullobject, ref nullobject);
                    wordobject.Quit(ref nullobject, ref nullobject, ref nullobject);

                    MessageBox.Show("file loaded");
                }
            }
        }



        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) 
            {
                textFolderAT.Text = folderBrowserDialog1.SelectedPath;
                string[] htmlAT = toAT(folderBrowserDialog1.SelectedPath);
                listBox2.Items.Clear();
                foreach (string s in htmlAT)
                    listBox2.Items.Add(s);                   
                // MessageBox.Show("всё закончилось!");
            }
            
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileInfo fileInf = new FileInfo(listBox2.SelectedItem.ToString());
            if (fileInf.Exists)
            {
                textBox2.Text = "Имя файла: {0}" + fileInf.Name + "\r\n";
                textBox2.Text += "Размер: {0}" + fileInf.Length + "\r\n";
                textBox2.Text += "Создан: {0}" + fileInf.CreationTime + "\r\n";
                try
                {
                    pictureBox1.Image = Image.FromFile(listBox2.SelectedItem.ToString());
                }
                catch
                {                   
                    pictureBox1.Image = pictureBox1.ErrorImage;
                }
            }
        }

        static string[] toAT(string dirName)
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

        // загружаем изображения на FTP
        private string FTPUploadFile(string newFolder, 
                                    string hN, 
                                    string uN, 
                                    string pW, 
                                    string rP, 
                                    string replaceOld, 
                                    string replaceNew, 
                                    string pxW)
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
                        }
                    }
                }
                res = res.Replace(replaceOld, @"<p><img style=""display: block; margin-left: auto; margin-right: auto; "" src=""" + replaceNew);
                res = res.Replace(".jpg", @".jpg"" width = """ + pxW + @"""/></p>");

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
        /// functions
        /// 
        static string docTitle(string s, int i)
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

        void Panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                labelDrop.Text = "Отпустите мышь";
                e.Effect = DragDropEffects.Copy;
            }                
        }

        void Panel1_DragDrop(object sender, DragEventArgs e)
        {
            labelDrop.Text = "Папка/файлы приняты";
            DateTime someDate = new DateTime(1582, 10, 5);
            textFileName.Text = DateTime.Now.ToString("yyyy-MM-dd_HHmmss_");
            List<string> paths = new List<string>();
            foreach (string obj in (string[])e.Data.GetData(DataFormats.FileDrop))
                if (Directory.Exists(obj))
                {
                    // paths.AddRange(Directory.GetFiles(obj, "*.*", SearchOption.TopDirectoryOnly));
                    textFolderAT.Text = obj;
                    string[] htmlAT = toAT(obj);
                    listBox2.Items.Clear();
                    foreach (string s in htmlAT)
                        listBox2.Items.Add(s);
                    //stBox2.Items.Add(obj);
                }
                else
                {
                    listBox2.Items.Add(obj);
                    // paths.Add(obj);
                }
                    
        }

        void Panel1_DragLeave(object sender, EventArgs e)
        {
            labelDrop.Text = "Перетащите сюда папку и/или файлы. Щелчок - выбор папки.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            string[] px = { "700", "850", "960", "1024", "1280" };
            listWidth.Items.AddRange(px);
            listWidth.SetSelected(1, true);
            string[] host = { "atkorablino.ru", "ddt/uoimp", "korablinorono" };
            listHost.Items.AddRange(host);
            listHost.SetSelected(0, true);

            FileInfo fileInf = new FileInfo(@"C:\IrfanView\i_view32.exe");
            if (fileInf.Exists == false)
            {
                textBox2.BackColor = Color.DarkRed;
                textBox2.ForeColor = Color.WhiteSmoke;
                textBox2.Text = "Не найден файл " + @"C:\IrfanView\i_view32.exe" + "\r\n";
                textBox2.Text += "Программа не будет корректно работать!!!" + "\r\n";
                btnRU.Enabled = false;
            }

        }

        private void Panel1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime someDate = new DateTime(1582, 10, 5);
                textFileName.Text = DateTime.Now.ToString("yyyy-MM-dd_HHmmss_");
                textFolderAT.Text = folderBrowserDialog1.SelectedPath;
                string[] htmlAT = toAT(folderBrowserDialog1.SelectedPath);
                listBox2.Items.Clear();
                foreach (string s in htmlAT)
                    listBox2.Items.Add(s);
            }
        }

        private void ListHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime newsDate = new DateTime(1582, 10, 5);

            switch (listHost.SelectedIndex)
            {
                case 0: // atkorablino.ru
                    textHost.Text = "ftp.atkorablin.nichost.ru";
                    textUname.Text = "atkorablin_ftp0419";
                    textPassword.Text = "vBgB0QVuoBvuP";
                    textPath.Text = "/atkorablino.ru/docs/images/news";
                    textPath.Text += DateTime.Now.ToString("-MM-yyyy") + "/";
                    textOld.Text = "/atkorablino.ru/docs/";
                    textNew.Text = "http://atkorablino.ru/";
                    break;
                case 1: // ddt/uoimp
                    textHost.Text = "";
                    textUname.Text = "";
                    textPassword.Text = "";
                    textPath.Text = "";
                    textPath.Text += DateTime.Now.ToString("-MM-yyyy") + "/";
                    textOld.Text = "";
                    textNew.Text = "";
                    break;
                case 2: // korablinorono
                    textHost.Text = "ftp.korablino.nichost.ru";
                    textUname.Text = "korablino_ftpadmin";
                    textPassword.Text = "h/B5jiCQ";
                    textPath.Text = "/korablinorono.org.ru/docs/photo/news";
                    textPath.Text += DateTime.Now.ToString("-MM-yyyy") + "/";
                    textOld.Text = "/korablinorono.org.ru/docs/";
                    textNew.Text = "http://korablinorono.org.ru/";
                    break;
                case -1:
                    break;
                default:
                    break;
            }
        }

        void Button3_Click_1(object sender, EventArgs e)
        {
            // string php = string.Empty;

            string php = ReadWords.Properties.Resources.upload_post; //upload_post.txt в ресурсах
            php = php.Replace("###title###", "Заголовок записи");
            php = php.Replace("###content###", "Текст записи");

            textBox2.Text = php;

            string writePath = @"D:\dnlds\test.php";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.UTF8))
                {
                    sw.Write(php);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        // PublicPostToWordpress
        private string PublicPostToWordpress(string newFolder,
                                    string hN,
                                    string uN,
                                    string pW,
                                    string rP,
                                    string replaceOld,
                                    string replaceNew)
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
                        }
                    }
                }
                //res = res.Replace(replaceOld, @"<p><img style=""display: block; margin-left: auto; margin-right: auto; "" src=""" + replaceNew);
                //res = res.Replace(".jpg", @".jpg"" width = """ + pxW + @"""/></p>");

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e);
                return string.Empty;
            }
            // MessageBox.Show("Загрузили");
        }

        private void BtnRU_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                string s = string.Empty;
                string dirName = textFolderAT.Text;
                string newFolder = textFolderAT.Text;
                ProcessStartInfo psi = new ProcessStartInfo();
                //Имя запускаемого приложения
                psi.FileName = @"C:\IrfanView\i_view32.exe";

                if (Directory.Exists(dirName))
                {
                    newFolder = dirName + @"\to_ftp\";
                }
                int nFile = 0;
                string px = listWidth.SelectedItem.ToString();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    s = listBox2.Items[i].ToString();
                    nFile = i + 1;

                    s += @" /resize=(" + px + @", 0) /aspectratio /resample /silent /convert=""" + newFolder + textFileName.Text + nFile.ToString("d3") + @".jpg""";
                    try
                    {
                        //команда, которую надо выполнить
                        psi.Arguments = s;
                        Process.Start(psi);
                        s = newFolder + textFileName.Text + nFile.ToString("d3") + ".jpg";
                    }
                    catch
                    {
                        pictureBox1.Image = pictureBox1.ErrorImage;
                    }
                }

                string res = string.Empty;
                res = FTPUploadFile(newFolder,
                                    textHost.Text,
                                    textUname.Text,
                                    textPassword.Text,
                                    textPath.Text,
                                    textOld.Text,
                                    textNew.Text,
                                    px);
                if (res != string.Empty)
                {
                    textBox2.Text = res;
                    textBox2.SelectAll();
                    textBox2.Copy();
                    MessageBox.Show("Картинки изменены UND загружены!");
                }
                else
                {
                    MessageBox.Show("Картинки NOT загружены!");
                }
            }
        }
    }     
}
