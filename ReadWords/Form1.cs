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
                 *   +                   > как правило, в одном абзаце!!!
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
                        textFromWordDocument = wordContentRange.Paragraphs[i].Range.Text;
                        if (textFromWordDocument.Trim() == string.Empty)
                        {
                            textBox.Text += i.ToString() + ": !___ПУСТОЙ ПАРАГРАФ___!" + "\r\n";
                        }
                        else
                        {
                            // textBox.Text += i.ToString() + ": " + textFromWordDocument + "\r\n";
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

                            /* if ()
                                tableLayoutPanel1.SetRow()
                            textBox.Text += i.ToString() + ": " + textFromWordDocument + "\r\n";
                            */

                        }

                    }

                    textBox.Text += strDocTitle + "\r\n";

                    if (countTitle > 2)
                    {
                        listBox1.Items.Insert(countRow, strDocTitle);
                        countRow++;
                    }


                    docs.Close(ref nullobject, ref nullobject, ref nullobject);
                    wordobject.Quit(ref nullobject, ref nullobject, ref nullobject);

                    MessageBox.Show("file loaded");
                }
            }
        }

        static string docTitle(string s, int i)
        {
            string sT = string.Empty;
            switch (i)
            {
                case 1:
                    Regex regex1 = new Regex(@"решен(\w*)|постановл(\w*)|распоряж(\w*)");
                    MatchCollection matches1 = regex1.Matches(s.ToLower());
                    if (matches1.Count > 0)
                    {
                        foreach (Match match in matches1)
                            sT += " " + match.Value;
                        sT = sT.Trim();
                        return sT.ToUpper();
                    }
                    else
                    {
                        return string.Empty;
                    }
                case 2:
                    Regex regex3 = new Regex(@"(\d+)\D*(январ[ьея]|феврал[ьея]|март[еа]?|апрел[ьея]|ма[йея]|ию[нл][яье]|август[еа]?|(?:сент|окт|но|дек)[ая]бр[яье])(\D*\d+)\D*(\d+)", RegexOptions.IgnoreCase);

                    MatchCollection matches3 = regex3.Matches(s);
                    if (matches3.Count > 0)
                    {
                        foreach (Match match in matches3)
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
    }     
}
