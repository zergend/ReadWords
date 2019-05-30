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

using YandexDiskNET;
using System.Diagnostics;
using System.Net;

namespace ReadWords
{
    public partial class Form1 : Form
    {
        // массив для хранения файлов, которые нужно удалить с серверов
        static string[] deletePHP;
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
                            string title = WPutilites.docTitle(textFromWordDocument, countTitle);

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

        /*
        private void Button3_Click(object sender, EventArgs e)
        {            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) 
            {
                textFolderAT.Text = folderBrowserDialog1.SelectedPath;
                string[] htmlAT = WPutilites.toAT(folderBrowserDialog1.SelectedPath);
                listBox2.Items.Clear();
                foreach (string s in htmlAT)
                    listBox2.Items.Add(s);                   
            }            
        }
        */

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
                    btnPreparePost.Enabled = false; // чтобы не пытаться обработать jpg как word
                    pictureBox1.Image = Image.FromFile(listBox2.SelectedItem.ToString());
                }
                catch
                {
                    btnPreparePost.Enabled = true;
                    pictureBox1.Image = pictureBox1.ErrorImage;
                }
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
            textFolderAT.Text = "";
            listBox2.Items.Clear();
            DateTime someDate = new DateTime(1582, 10, 5);
            textFileName.Text = DateTime.Now.ToString("yyyy-MM-dd_HHmmss_");

            foreach (string obj in (string[])e.Data.GetData(DataFormats.FileDrop))
                if (Directory.Exists(obj))
                {
                    textFolderAT.Text = obj;
                    string[] htmlAT = WPutilites.toAT(obj);
                    listBox2.Items.Clear();
                    foreach (string s in htmlAT)
                        listBox2.Items.Add(s);
                }
                else
                {
                    listBox2.Items.Add(obj);
                }
            try
            {
                listBox2.SetSelected(0, true);
                FileInfo fileInf = new FileInfo(listBox2.SelectedItem.ToString());
                if (fileInf.Exists)
                    textFolderAT.Text = fileInf.DirectoryName;
            }
            catch
            {
                MessageBox.Show("Файлы отсутствуют!");
            }
            
        }

        void Panel1_DragLeave(object sender, EventArgs e)
        {
            labelDrop.Text = "Перетащите сюда папку и/или файлы. Щелчок - выбор папки.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnPreparePost.Enabled = false;
            string[] px = { "700", "850", "960", "1024", "1280" };
            listWidth.Items.AddRange(px);
            listWidth.SetSelected(1, true);
            string[] host = { "korablinorono", "atkorablino", "ddt", "korablino62" };
            deletePHP = { "","","" };
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
                string[] htmlAT = WPutilites.toAT(folderBrowserDialog1.SelectedPath);
                listBox2.Items.Clear();
                foreach (string s in htmlAT)
                    listBox2.Items.Add(s);
            }
        }

        private void ListHost_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime newsDate = new DateTime(1582, 10, 5);
            checkCat.Items.Clear(); // и заполняем новыми значениями            

            switch (listHost.SelectedItem.ToString().ToLower())
            {
                // "atkorablino", "ddt", "korablinorono", "korablino62"
                case "atkorablino": // atkorablino.ru
                    textHost.Text = "ftp.atkorablin.nichost.ru";
                    textUname.Text = "atkorablin_ftp0419";
                    textPassword.Text = "vBgB0QVuoBvuP";
                    textPath.Text = "/atkorablino.ru/docs/images/news";
                    textPath.Text += DateTime.Now.ToString("-MM-yyyy") + "/";
                    textPathPost.Text = "/atkorablino.ru/docs/";
                    textOld.Text = "/atkorablino.ru/docs/";
                    textNew.Text = "http://atkorablino.ru/";
                    textJPath.Text = "/home/atkorablin/atkorablino.ru/docs";
                    textFactory.Text = ".DS.'libraries'.DS.'joomla'.DS.'database'.DS.'factory.php'";
                    // категории
                    checkCat.Items.Add("Новости техникума;34");
                    checkCat.Items.Add("Конкурсы (методработа);48");
                    checkCat.Items.Add("Открытые уроки и внеклассные мероприятия;29");
                    checkCat.Items.Add("Вебинары;71");
                    checkCat.SetItemChecked(0, true);

                    break;
                case "ddt": // ddt
                    textHost.Text = "ftp.korablinod.nichost.ru";
                    textUname.Text = "korablinod_ftp";
                    textPassword.Text = "2ixh17bw";
                    textPath.Text = "/korablinoddt.org.ru/docs/news/";
                    textPath.Text += DateTime.Now.ToString("MM-yyyy") + "/";
                    textOld.Text = "/korablinoddt.org.ru/docs/";
                    textNew.Text = "http://korablinoddt.org.ru/";
                    textJPath.Text = "/home/korablinod/korablinoddt.org.ru/docs";
                    textFactory.Text = ".DS.'libraries'.DS.'joomla'.DS.'factory.php'";
                    // категории
                    checkCat.Items.Add("Новости;20");
                    checkCat.SetItemChecked(0, true);
                    break;
                case "korablinorono": // korablinorono
                    textHost.Text = "ftp.korablino.nichost.ru";
                    textUname.Text = "korablino_ftpadmin";
                    textPassword.Text = "h/B5jiCQ";
                    textPath.Text = "/korablinorono.org.ru/docs/photo/news";
                    textPath.Text += DateTime.Now.ToString("-MM-yyyy") + "/";
                    textPathPost.Text = "/korablinorono.org.ru/docs/";
                    textOld.Text = "/korablinorono.org.ru/docs/";
                    textNew.Text = "http://korablinorono.org.ru/";
                    textJPath.Text = "";
                    textFactory.Text = "";
                    // категории
                    checkCat.Items.Add("Новости, объявления;22");
                    checkCat.Items.Add("Конкурсы для обучающихся;46");
                    checkCat.Items.Add("Конкурсы для педагогов;47");
                    checkCat.SetItemChecked(0, true);

                    break;
                case "korablino62": // korablinorono
                    textHost.Text = "s19.webhost1.ru";
                    textUname.Text = "zergend_2018";
                    textPassword.Text = "6W1fJRSlj4uWw";
                    textPath.Text = "/korablino62.ru/photo/news";
                    textPath.Text += DateTime.Now.ToString("-MM-yyyy") + "/";
                    textPathPost.Text = "/korablino62.ru/";
                    textOld.Text = "/korablino62.ru/";
                    textNew.Text = "http://korablino62.ru/";
                    textJPath.Text = "";
                    textFactory.Text = "";
                    // категории
                    checkCat.Items.Add("Анонсы, объявления;82");
                    checkCat.Items.Add("Росреестр;231");
                    checkCat.Items.Add("КУМИиЗР;5");
                    checkCat.SetItemChecked(0, true);

                    break;
                case "":
                    break;
                default:
                    break;
            }
        }    

        private void BtnRU_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                string s = string.Empty;
                string dirName = textFolderAT.Text;
                string newFolder = textFolderAT.Text;
                textFileName.Text = Transliteration.Front(textFileName.Text).ToLower();
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

                string[] res = { "", "" };
                
                res = WPutilites.FTPUploadFile(newFolder,
                                    textHost.Text,
                                    textUname.Text,
                                    textPassword.Text,
                                    textPath.Text,
                                    textOld.Text,
                                    textNew.Text,
                                    px);
                if (res[0] != string.Empty)
                {
                    textPost.Text = res[0];
                    txtPostImage.Text = res[1];
                    // MessageBox.Show("Картинки изменены UND загружены!");
                    btnPreparePost.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Картинки NOT загружены!");
                }
            }
        }

        private void BtnPreparePost_Click(object sender, EventArgs e)
        {
            //int countRow = 0;
            string html = string.Empty;
            FileInfo fileInf = new FileInfo(listBox2.SelectedItem.ToString());

            // Open document 
            string originalfilename = listBox2.SelectedItem.ToString();

            if (fileInf.Exists && new[] { ".docx", ".doc", ".txt", ".rtf" }.Contains(Path.GetExtension(originalfilename).ToLower()))
            {
                object File = originalfilename;
                object nullobject = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application wordobject = new Microsoft.Office.Interop.Word.Application();
                wordobject.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
                Microsoft.Office.Interop.Word._Document docs = wordobject.Documents.Open(ref File, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject);

                Microsoft.Office.Interop.Word.Range wordContentRange = docs.Content;

                string textFromWordDocument = string.Empty;
                string strDocTitle = string.Empty;
                int title = 1;

                for (int i = 1; i <= wordContentRange.Paragraphs.Count; i++)
                {
                    // !!! концом абзаца могут притворяться разрывы строк или куча пробелов ???
                    textFromWordDocument = wordContentRange.Paragraphs[i].Range.Text;
                    textFromWordDocument = textFromWordDocument.Replace(Environment.NewLine, " ");
                    if (textFromWordDocument.Trim() == string.Empty)
                    {
                        // textBox.Text += i.ToString() + ": !___ПУСТОЙ ПАРАГРАФ___!" + "\r\n";
                    }
                    else
                    {
                        if ( title == 1 )
                        {
                            // Regex.Replace - заменяет двойные пробелы и разрывы строк на одинарный пробел
                            textTitle.Text = System.Text.RegularExpressions.Regex.Replace(textFromWordDocument.Trim(), @"\s+", " ");                            
                            title = 2;
                        }
                        else
                            // Regex.Replace - заменяет двойные пробелы и разрывы строк на одинарный пробел
                            html += "<p>" + System.Text.RegularExpressions.Regex.Replace(textFromWordDocument.Trim(), @"\s+", " ") + "</p>\r\n";     
                    }
                }

                textPost.Text = html + "\r\n" + textPost.Text;                

                docs.Close(ref nullobject, ref nullobject, ref nullobject);
                wordobject.Quit(ref nullobject, ref nullobject, ref nullobject);

                // MessageBox.Show("html prepare!");
                btnPost.Enabled = true;
            }
        }

        private void BtnPost_Click(object sender, EventArgs e)
        {
            string res = string.Empty;                
            switch (listHost.SelectedItem.ToString().ToLower())
            {
                case "atkorablino": 
                    res = PostPHP("joomla");
                    break;
                case "ddt": 
                    res = PostPHP("joomla");
                    break;
                case "korablinorono": 
                    res = PostPHP("wp");
                    break;
                case "korablino62":
                    res = PostPHP("wp");
                    break;
                case "":
                    break;
                default:
                    break;
            }
            // MessageBox.Show(res);
            labelDrop.Text = "Перетащите сюда папку и/или файлы. Щелчок - выбор папки.";
            if (checkClearList.CheckState == CheckState.Checked)
            {
                listBox2.Items.Clear();
                textFolderAT.Text = "";
                textFileName.Text = "";
            }
            textTitle.Text = "";
            textPost.Text = "";
            txtPostImage.Text = "";
            textBox2.Text = res;
        }

        string PostPHP(string cms)
        {            
            string writeFile = string.Empty;
            string urlSite = textNew.Text;
            string phpFile = textPHPfileName.Text;
            string res = string.Empty;
            string php = string.Empty;
            
            string[] cats;
            string catToPHP = string.Empty;

            cms = cms.ToLower();
            switch (cms)
            {
                case "wp":
                    // выбранные категории
                    foreach (object item in checkCat.CheckedItems)
                    {
                        cats = item.ToString().Split(';');
                        catToPHP += cats[1] + ",";
                    }
                    catToPHP = "array(" + catToPHP.Trim(',') + ")"; // конец выбранных категорий            
                    //формируем php-файл для публикации поста на WordPress
                    php = ReadWords.Properties.Resources.post_wp; //upload_post.txt в ресурсах
                    php = php.Replace("###title###", textTitle.Text);
                    php = php.Replace("###content###", textPost.Text);

                    if (checkDraft.CheckState == CheckState.Checked)
                        php = php.Replace("###status###", "draft");
                    else
                        php = php.Replace("###status###", "publish");

                    php = php.Replace("###category###", catToPHP);
                    php = php.Replace("###url###", txtPostImage.Text); // изображение записи

                    writeFile = WritePhp(php);
                    break;
                case "joomla":
                    // выбранные категории
                    foreach (object item in checkCat.CheckedItems)
                    {
                        cats = item.ToString().Split(';');
                        catToPHP = cats[1]; 

                        // заменим двойные кавычки в тексте на одинарные (экранированные для БД), 
                        // т.к. двойные вызывают ошибку? при запросе к БД
                        textPost.Text = textPost.Text.Replace("\"", "''");
                        //формируем php-файл для публикации поста на Joomla
                        php = ReadWords.Properties.Resources.post_joomla; //post_joomla.txt в ресурсах
                        php = php.Replace("###jpath###", textJPath.Text);
                        php = php.Replace("###factory###", textFactory.Text);
                        php = php.Replace("###title###", textTitle.Text);
                        php = php.Replace("###alias###", Transliteration.Front(textTitle.Text).ToLower() + "_" + catToPHP);                        
                        php = php.Replace("###introtext###", textPost.Text);

                        if (checkDraft.CheckState == CheckState.Checked)
                            php = php.Replace("###state###", "0");
                        else
                            php = php.Replace("###state###", "1");

                        php = php.Replace("###catid###", catToPHP);

                        writeFile = WritePhp(php);
                        // в Joomla можно опубликовать материал только в одной категории
                        // поэтому, сразу выходим, публикуем только в одной категории (первой в списке)
                        break;
                    }

                    break;
                default:
                    break;
            }            

            res = WPutilites.UploadPHP(writeFile,
                                  textHost.Text,
                                  textUname.Text,
                                  textPassword.Text,
                                  textPathPost.Text);
            if (res != string.Empty)
            {
                webPost.Navigate(urlSite + phpFile);

                res += " published \r\n";

                // MessageBox.Show("Черновик Поста подготовлен!");
            }
            else
            {
                MessageBox.Show("Пост NOT опубликован!");
            }

            MessageBox.Show(res);
            return res;
        }

        string WritePhp(string php)
        {
            string writeFile = string.Empty;
            string phpFile = textPHPfileName.Text;
            string dirName = textFolderAT.Text;
            string subDirName = @"\uppost\";
            string subDir = @"uppost";

            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            if (!dirInfo.Exists)
                dirInfo.Create();

            dirInfo.CreateSubdirectory(subDir);

            if (Directory.Exists(dirName + subDirName))
            {
                writeFile = dirName + subDirName + phpFile;

                FileInfo fileInf = new FileInfo(writeFile);
                if (fileInf.Exists)
                    fileInf.Delete();

                // UTF8Encoding(false) UTF8 without BOM
                using (StreamWriter sw = new StreamWriter(writeFile, true, new System.Text.UTF8Encoding(false)))
                {
                    sw.Write(php);
                    //sw.Close();
                }
            }

            return writeFile;
        }

        private void BtnTitleToPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (textTitle.SelectedText.Trim() != string.Empty)
                    textPost.Text = "<p>" + textTitle.SelectedText.Trim() + "</p>\r\n" +  textPost.Text;
                textTitle.Text = textTitle.Text.Remove(textTitle.SelectedText.Count());
            }
            catch
            {
                MessageBox.Show("Возможно, не выделен текст в заголовке!");
            }
        }

        private void BtnTransFN_Click(object sender, EventArgs e)
        {
            textFileName.Text = Transliteration.Front(textFileName.Text).ToLower();
        }

        private void WebPost_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            MessageBox.Show(webPost.DocumentText); 
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            textTitle.Text = "";
            textPost.Text = "";
        }

        private void ButtonDelPhp_Click(object sender, EventArgs e)
        {
            string res;
            //удалим файл с фтп
            res = WPutilites.UploadPHP(textPHPfileName.Text,
                              textHost.Text,
                              textUname.Text,
                              textPassword.Text,
                              textPathPost.Text, true);
            MessageBox.Show(res);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Удалить все загруженные по FTP php-файлы?",
                "Удаление php-файлов",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
                MessageBox.Show("Удалено");             
        }
    }     
}
