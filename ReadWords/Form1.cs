using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using YandexDiskNET;
using System.Text.RegularExpressions;

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
            int countRow = 0;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) // (openFile1.ShowDialog() == DialogResult.OK)
            {
                textFolderAT.Text = folderBrowserDialog1.SelectedPath;
                string[] htmlAT = toAT(folderBrowserDialog1.SelectedPath);
                listBox2.Items.Clear();
                foreach (string s in htmlAT)
                {
                    listBox2.Items.Insert(countRow, s);
                    countRow++;
                }
                MessageBox.Show("всё закончилось!");
            }
            
        }

        // готовим фото + текст для публикации на atkorablino.ru
        // ресайз фото => загрузка на ftp => 
        // => получение ссылок на файлы => формирование html-кода с картинками => 
        // => добавление текста => копирование в буфер???
        static string[] toAT(string dirName)
        {
            string[] r = new string[1];
            r[0] = "empty";
            if (Directory.Exists(dirName))
            {
                /* Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine(); */

                // Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(dirName);

                string[] names = new string[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    names[i] = files[i];
                }
                return names;
            }
            else return r;
            /* return @"<p>25 апреля было проведено мероприятие – Единый урок  на тему «Моей семьи война коснулась», подготовленная  преподавателем Ольгой Юрьевной Елмановой. Урок начался  с показа  документальных кадров о начале Великой Отечественной войны. В ходе изложения материала студенты  были ознакомлены с основными  историческими фактами и итогами войны. Основной акцент был сделан на то, что война коснулась каждой семьи. В презентации были показаны материалы об участниках Великой Отечественной войны – жителей нашего района.</p>
<p>На уроке звучали песни военных лет, показаны отрывки из художественных фильмов о войне. Студенты были ориентированы на то, чтобы быть достойными своих предков – участников войны и сделать все возможное для мира на земле.</p>
<p><img style=""display: block; margin-left: auto; margin-right: auto;"" src=""images/042019-news/urok-25042019-001.jpg"" alt="""" width=""850"" /></p>
<hr id=""system-readmore"" />
<p> </p>
<p><img style=""display: block; margin-left: auto; margin-right: auto;"" src=""images/042019-news/urok-25042019-002.jpg"" alt="""" width=""850"" /></p>
<p> </p>
<p><img style=""display: block; margin-left: auto; margin-right: auto;"" src=""images/042019-news/urok-25042019-003.jpg"" alt="""" width=""850"" /></p>
<p> </p>
<p><img style=""display: block; margin-left: auto; margin-right: auto;"" src=""images/042019-news/urok-25042019-004.jpg"" alt="""" width=""850"" /></p>
<p> </p>
<p><img style=""display: block; margin-left: auto; margin-right: auto;"" src=""images/042019-news/urok-25042019-005.jpg"" alt="""" width=""850"" /></p>
<p> </p>"; */
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

        private void Button4_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                for (int i = 0; i< listBox2.Items.Count; i++)
                {

                }
            }
        }


    }     
}
