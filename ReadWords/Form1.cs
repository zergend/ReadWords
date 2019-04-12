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
            
            if (openFile1.ShowDialog() == DialogResult.OK)
            {
                // Open document 
                string originalfilename = System.IO.Path.GetFullPath(openFile1.FileName);
                
                if (openFile1.CheckFileExists && new[] { ".docx", ".doc", ".txt", ".rtf" }.Contains(Path.GetExtension(originalfilename).ToLower()))
                {
                   // Microsoft.Office.Interop.Word.Application wordObject = new Microsoft.Office.Interop.Word.Application();
                    object File = originalfilename;
                    object nullobject = System.Reflection.Missing.Value;
                    Microsoft.Office.Interop.Word.Application wordobject = new Microsoft.Office.Interop.Word.Application();
                    wordobject.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
                    Microsoft.Office.Interop.Word._Document docs = wordobject.Documents.Open(ref File, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject);
                    //docs.ActiveWindow.Selection.WholeStory();
                    
                    //docs.ActiveWindow.Selection.Copy();

                    Microsoft.Office.Interop.Word.Range wordContentRange = docs.Content;
                    string textFromWordDocument = wordContentRange.Text;
                    /* for (int par = 1; par <= docs.Paragraphs.Count; par++)
                    {
                        //richTextBox1.Text = par.ToString();
                        // richTextBox1.Text = docs.ActiveWindow.Selection.ToString();
                    } */
                    richTextBox1.Text = textFromWordDocument; 
                    //richTextBox1.Paste();

                    docs.Close(ref nullobject, ref nullobject, ref nullobject);
                    wordobject.Quit(ref nullobject, ref nullobject, ref nullobject);


                    MessageBox.Show("file loaded");
                }
            }
        }
    }
}
