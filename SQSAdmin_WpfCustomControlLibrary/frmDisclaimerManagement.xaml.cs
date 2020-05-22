using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

//
// Add Microsoft.Office.Interop.Word
//  
using Word = Microsoft.Office.Interop.Word;
using System.IO;
using System.Net;
using System.Data;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmDisclaimerManagement.xaml
    /// </summary>
    public partial class frmDisclaimerManagement : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private CommonResource cr;
        public frmDisclaimerManagement(int stateid, string usercode)
        {
            loginstateid = stateid;
            cr = new CommonResource(stateid, 0);
            InitializeComponent();
            this.DataContext = cr;

            LoadRTBContent(null, null);
        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LoadBrandDropDown();
            int pstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            cr.LoadBrand(pstateid);
            cr.LoadRegion(pstateid);
            cmbBrand.SelectedIndex=0;
            cmbRegion.SelectedIndex=0;
        }
        private void bthSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchDisclaimer();
        }

        // Handle "Save RichTextBox Content" button click.
        private void bthSaveRTBContent_Click(Object sender, RoutedEventArgs args)
        {

            // Send an arbitrary URL and file name string specifying
            // the location to save the XAML in.
            SaveXamlPackage(Environment.CurrentDirectory + "\\test.xaml");
        }

        private void SearchDisclaimer()
        {
            string type = (cmbType.SelectedIndex+1).ToString();
            string state = cmbState.Text;
            int regionid = 0;
            int brandid = 0;

            int.TryParse(cmbRegion.SelectedValue.ToString(), out regionid);
            int.TryParse(cmbBrand.SelectedValue.ToString(), out brandid);

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            string disclaimer = client.SQSAdmin_SearchDisclaimer(type, state, regionid, brandid);

            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(ConvertHtmlToText(disclaimer))));
            document.Blocks.Add(paragraph);
            richTB.Document = document;            
        }

        private static string ConvertRtfToXaml(string rtfText)

        {

            var richTextBox = new RichTextBox();

            if (string.IsNullOrEmpty(rtfText)) return "";

            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            using (var rtfMemoryStream = new MemoryStream())

            {

                using (var rtfStreamWriter = new StreamWriter(rtfMemoryStream))

                {

                    rtfStreamWriter.Write(rtfText);

                    rtfStreamWriter.Flush();

                    rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                    textRange.Load(rtfMemoryStream, DataFormats.Rtf);

                }

            }

            using (var rtfMemoryStream = new MemoryStream())

            {

                textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                textRange.Save(rtfMemoryStream, DataFormats.Xaml);

                rtfMemoryStream.Seek(0, SeekOrigin.Begin);

                using (var rtfStreamReader = new StreamReader(rtfMemoryStream))

                {

                    return rtfStreamReader.ReadToEnd();

                }

            }

        }

        public static string ConvertHtmlToText(string source)
        {

            string result;

            // Remove HTML Development formatting
            // Replace line breaks with space
            // because browsers inserts space
            result = source;
            //result = source.Replace("\r", " ");
            // Replace line breaks with space
            // because browsers inserts space
            //result = result.Replace("\n", " ");
            // Remove step-formatting
            //result = result.Replace("\t", string.Empty);
            // Remove repeating speces becuase browsers ignore them
            result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                  @"( )+", " ");

            // Remove the header (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*head([^>])*>", "<head>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<( )*(/)( )*head( )*>)", "</head>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(<head>).*(</head>)", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // remove all scripts (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*script([^>])*>", "<script>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<( )*(/)( )*script( )*>)", "</script>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //result = System.Text.RegularExpressions.Regex.Replace(result, 
            //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
            //         string.Empty, 
            //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<script>).*(</script>)", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // remove all styles (prepare first by clearing attributes)
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*style([^>])*>", "<style>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"(<( )*(/)( )*style( )*>)", "</style>",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(<style>).*(</style>)", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // insert tabs in spaces of <td> tags
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*td([^>])*>", "\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // insert line breaks in places of <BR> and <LI> tags
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*br( )*>", "\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*li( )*>", "\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // insert line paragraphs (double line breaks) in place
            // if <P>, <DIV> and <TR> tags
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*div([^>])*>", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*tr([^>])*>", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<( )*p([^>])*>", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // Remove remaining tags like <a>, links, images,
            // comments etc - anything thats enclosed inside < >
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<[^>]*>", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            // replace special characters:
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&nbsp;", " ",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&bull;", " * ",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&lsaquo;", "<",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&rsaquo;", ">",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&trade;", "(tm)",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&frasl;", "/",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"<", "<",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @">", ">",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&copy;", "(c)",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&reg;", "(r)",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Remove all others. More can be added, see
            // http://hotwired.lycos.com/webmonkey/reference/special_characters/
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     @"&(.{2,6});", string.Empty,
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);


            // make line breaking consistent
            result = result.Replace("\n", "\r");

            // Remove extra line breaks and tabs:
            // replace over 2 breaks with 2 and over 4 tabs with 4. 
            // Prepare first to remove any whitespaces inbetween
            // the escaped characters and remove redundant tabs inbetween linebreaks
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)( )+(\r)", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\t)( )+(\t)", "\t\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\t)( )+(\r)", "\t\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)( )+(\t)", "\r\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Remove redundant tabs
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)(\t)+(\r)", "\r\r",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Remove multible tabs followind a linebreak with just one tab
            result = System.Text.RegularExpressions.Regex.Replace(result,
                     "(\r)(\t)+", "\r\t",
                     System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            // Initial replacement target string for linebreaks
            string breaks = "\r\r\r";
            // Initial replacement target string for tabs
            string tabs = "\t\t\t\t\t";
            for (int index = 0; index < result.Length; index++)
            {
                result = result.Replace(breaks, "\r\r");
                result = result.Replace(tabs, "\t\t\t\t");
                breaks = breaks + "\r";
                tabs = tabs + "\t";
            }

            // Thats it.
            return result;

        }

        // Handle "Load RichTextBox Content" button click.
        void LoadRTBContent(Object sender, RoutedEventArgs args)
        {
            // Send URL string specifying what file to retrieve XAML
            // from to load into the RichTextBox.
            LoadXamlPackage(Environment.CurrentDirectory + "\\test.xaml");
        }

        // Handle "Print RichTextBox Content" button click.
        void PrintRTBContent(Object sender, RoutedEventArgs args)
        {
            PrintCommand();
        }

        // Save XAML in RichTextBox to a file specified by _fileName
        void SaveXamlPackage(string _fileName)
        {
            TextRange range;
            FileStream fStream;
            range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
            fStream = new FileStream(_fileName, FileMode.Create);
            StreamReader sr = new StreamReader(fStream);
            string rtfText = sr.ReadToEnd();
            range.Save(fStream, DataFormats.Xaml);
            fStream.Close();

            if (File.Exists(_fileName + ".rtf"))
            {
                File.Delete(_fileName + ".rtf");
            }
            fStream = new FileStream(_fileName + ".rtf", FileMode.Create);
            sr = new StreamReader(fStream);
            rtfText = sr.ReadToEnd();
            range.Save(fStream, DataFormats.Rtf);
            fStream.Close();

            //RtfToHtml(rtfText);
        }

        // Load XAML into RichTextBox from a file specified by _fileName
        void LoadXamlPackage(string _fileName)
        {
            TextRange range;
            FileStream fStream;
            if (File.Exists(_fileName + ".rtf"))
            {
                range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
                fStream = new FileStream(_fileName + ".rtf", FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.Rtf);
                fStream.Close();
            }
        }

        // Print RichTextBox content
        private void PrintCommand()
        {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                pd.PrintVisual(richTB as Visual, "printing as visual");
                pd.PrintDocument((((IDocumentPaginatorSource)richTB.Document).DocumentPaginator), "printing as paginator");
            }
        }

        private string HtmlToRtf(string strHTML)
        {
            //Declare a Word Application Object and a Word WdSaveOptions object
            Microsoft.Office.Interop.Word.Application myWord;
            Object oDoNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            //Declare two strings to handle the data
            string sReturnString = string.Empty;
            string sConvertedString = string.Empty;

            try
            {
                //Instantiate the Word application,
                //set visible to false and create a document
                myWord = new Microsoft.Office.Interop.Word.Application();
                myWord.Visible = false;
                myWord.Documents.Add();
                // Create a DataObject to hold the Rich Text
                //and copy it to the clipboard
                System.Windows.Forms.DataObject doRTF = new System.Windows.Forms.DataObject();
                doRTF.SetData("Html Format", strHTML);

                Clipboard.SetDataObject(doRTF);
                //  'Paste the contents of the clipboard to the empty,
                //'hidden Word Document
//                myWord.Windows[1].Selection.Paste();
                // 'â€¦then, select the entire contents of the document
                //'and copy back to the clipboard
                myWord.Windows[1].Selection.WholeStory();
                myWord.Windows[1].Selection.Copy();
                // 'Now retrieve the HTML property of the DataObject
                //'stored on the clipboard
                sConvertedString = Clipboard.GetData(System.Windows.Forms.DataFormats.Rtf).ToString();
                // Remove some leading text that shows up in some instances
                // '(like when you insert it into an email in Outlook
                //sConvertedString = sConvertedString.Substring(sConvertedString.IndexOf("<html"));
                // 'Also remove multiple Ã‚ characters that somehow end up in there
                //sConvertedString = sConvertedString.Replace("Ã‚", "");
                //'â€¦and you're done.
                sReturnString = sConvertedString;
                if (myWord != null)
                {
                    myWord.Documents[1].Close(oDoNotSaveChanges);
                    ((Word._Application)myWord).Quit(oDoNotSaveChanges);
                    myWord = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting HTML to Rich Text: " + ex.Message);
            }

            return sReturnString;
        }

        private string RtfToHtml(string strRTF)
        {
            //Declare a Word Application Object and a Word WdSaveOptions object
            Microsoft.Office.Interop.Word.Application myWord;
            Object oDoNotSaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
            //Declare two strings to handle the data
            string sReturnString = string.Empty;
            string sConvertedString = string.Empty;

            try
            {
                //Instantiate the Word application,
                //set visible to false and create a document
                myWord = new Microsoft.Office.Interop.Word.Application();
                myWord.Visible = false;
                myWord.Documents.Add();
                // Create a DataObject to hold the Rich Text
                //and copy it to the clipboard
                System.Windows.Forms.DataObject doRTF = new System.Windows.Forms.DataObject();
                doRTF.SetData("Rich Text Format", strRTF);

                Clipboard.SetDataObject(doRTF);
                //  'Paste the contents of the clipboard to the empty,
                //'hidden Word Document
                myWord.Windows[1].Selection.Paste();
                // 'â€¦then, select the entire contents of the document
                //'and copy back to the clipboard
                myWord.Windows[1].Selection.WholeStory();
                myWord.Windows[1].Selection.Copy();
                // 'Now retrieve the HTML property of the DataObject
                //'stored on the clipboard
                sConvertedString = Clipboard.GetData(System.Windows.Forms.DataFormats.Html).ToString();
                // Remove some leading text that shows up in some instances
                // '(like when you insert it into an email in Outlook
                sConvertedString = sConvertedString.Substring(sConvertedString.IndexOf("<html"));
                // 'Also remove multiple Ã‚ characters that somehow end up in there
                sConvertedString = sConvertedString.Replace("Ã‚", "");
                //'â€¦and you're done.
                sReturnString = sConvertedString;
                if (myWord != null)
                {
                    myWord.Documents[1].Close(oDoNotSaveChanges);
                    ((Word._Application)myWord).Quit(oDoNotSaveChanges);
                    myWord = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting Rich Text to HTML: " + ex.Message);
            }

            string fileName = Environment.CurrentDirectory + "\\test.htm";
            File.WriteAllText(fileName, sReturnString);

            return sReturnString;
        }

        public string ExtractHtml(string rtfText)
        {
            try
            {
                //Create word object
                Word.Application applicationObject = new Word.Application();
                //define path for save your temporary file.
                string userTemp = Environment.CurrentDirectory;
                //Open and save your rtf as HTML in your temp path.
                object missing = Type.Missing;
                object fileName = Environment.CurrentDirectory + "\\test.htm";
                object False = false;
                applicationObject.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

                Word.Document documentObject =
                   applicationObject.Documents.Open(ref fileName, ref missing, ref missing, ref missing,
                         ref missing, ref missing, ref missing, ref missing, ref missing,
                         ref missing, ref missing, ref False, ref missing, ref missing,
                          ref missing, ref missing);

                object tempFileName = System.IO.Path.Combine(userTemp, "tempHtm.html");
                object fileFormt = Word.WdSaveFormat.wdFormatHTML;
                object makeFalse = false;
                object makeTrue = true;
                string absolutePath = tempFileName.ToString();
                if (File.Exists(absolutePath))
                {
                    try
                    {
                        File.Delete(absolutePath);
                    }
                    catch { }
                }

                documentObject.SaveAs(ref tempFileName, ref fileFormt,
                   ref makeFalse, ref missing, ref makeFalse,
                   ref missing, ref missing, ref missing, ref makeFalse, ref missing, ref missing,
                   ref missing, ref missing, ref missing, ref missing, ref missing);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                documentObject.Close(ref makeFalse, ref missing, ref missing);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                //File.Delete(rtfText);

                String htmlCode = "";

                //Extract html source from the temporary html file.
                if (File.Exists(absolutePath))
                {
                    WebClient client = new WebClient();
                    htmlCode = client.DownloadString(absolutePath);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    try
                    {
                        //File.Delete(absolutePath);
                    }
                    catch { }
                }

                else
                {
                    htmlCode = "";
                }

                return htmlCode;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

    }

    //public static class RtfToHtmlConverter
    //{
    //    private const string FlowDocumentFormat = "<FlowDocument>{0}</FlowDocument>";

    //    public static string ConvertRtfToHtml(string rtfText)
    //    {
    //        var xamlText = string.Format(FlowDocumentFormat, ConvertRtfToXaml(rtfText));
    //        return string.Empty;
    //        //return HtmlFromXamlConverter.ConvertXamlToHtml(xamlText, false);
    //    }

    //    private static string ConvertRtfToXaml(string rtfText)
    //    {
    //        var richTextBox = new RichTextBox();
    //        if (string.IsNullOrEmpty(rtfText)) return "";

    //        var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

    //        //Create a MemoryStream of the Rtf content 

    //        using (var rtfMemoryStream = new MemoryStream())
    //        {
    //            using (var rtfStreamWriter = new StreamWriter(rtfMemoryStream))
    //            {
    //                rtfStreamWriter.Write(rtfText);
    //                rtfStreamWriter.Flush();
    //                rtfMemoryStream.Seek(0, SeekOrigin.Begin);

    //                //Load the MemoryStream into TextRange ranging from start to end of RichTextBox. 
    //                textRange.Load(rtfMemoryStream, DataFormats.Rtf);
    //            }
    //        }

    //        using (var rtfMemoryStream = new MemoryStream())
    //        {

    //            textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
    //            textRange.Save(rtfMemoryStream, DataFormats.Xaml);
    //            rtfMemoryStream.Seek(0, SeekOrigin.Begin);
    //            using (var rtfStreamReader = new StreamReader(rtfMemoryStream))
    //            {
    //                return rtfStreamReader.ReadToEnd();
    //            }
    //        }

    //    }




    //}
}
