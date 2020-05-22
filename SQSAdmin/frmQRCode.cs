using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmQRCode : Form
    {
        public frmQRCode()
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            InitializeComponent();
        }

        private void btngenerate_Click(object sender, EventArgs e)
        {
            if (txtEncodeData.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Data must not be empty.");
                return;
            }

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = Convert.ToInt16("15");
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid size!");
                return;
            }
            try
            {
                int version = Convert.ToInt16("7");
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid version !");
            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            Image image;
            String data = txtEncodeData.Text;
            image = qrCodeEncoder.Encode(data);
            picEncode.Image = image;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = string.Empty;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String fileName = openFileDialog1.FileName;
                picDecode.Image = new Bitmap(fileName);

            }
        }

        private void btnfind_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetProductQRCodeImage]", new System.Data.SqlClient.SqlParameter[1]
                                {
                                      new System.Data.SqlClient.SqlParameter("@productid", textBox2.Text)
                                });
                
                if(dsTemp.Tables[0].Rows.Count>0)
                {
                   byte[] byteArray = (byte[])dsTemp.Tables[0].Rows[0]["qrcodeimage"];
                   System.Drawing.Image returnImage;
                   using (var ms = new MemoryStream(byteArray, 0, byteArray.Length))
                   {
                       returnImage = System.Drawing.Image.FromStream(ms);
                   }

                   picDecode.Image = returnImage;
                    //QRCodeDecoder.Canvas = new ConsoleCanvas();
                    //String decodedString = decoder.decode(new QRCodeBitmapImage(new Bitmap(Image.FromStream(ms))));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            saveFileDialog1.Title = "Save";
            saveFileDialog1.FileName = string.Empty;
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case 4:
                        this.picEncode.Image.Save(fs,
                           System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }

                fs.Close();
            }
        }

        private void frmQRCode_Load(object sender, EventArgs e)
        {
            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
    }
}
