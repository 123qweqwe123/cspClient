using cspClient.utils;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace cspClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.printDocument1.OriginAtMargins = true;//启用页边距
            this.pageSetupDialog1.EnableMetric = true; //以毫米为单位
            this.printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            textBox1.TextChanged += TextBox1_TextChanged;




        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            qrCodeGraphicControl1.Text = textBox1.Text;
            qrCodeImgControl1.Text = textBox1.Text;
        }

        private void btnSetPrint_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.Document = printDocument1;//指向要打印的对象

            this.pageSetupDialog1.ShowDialog();

        }

        private void btnPrePrint_Click(object sender, EventArgs e)
        {
            Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
            doc.LoadFromFile(Environment.CurrentDirectory + "\\替换.pdf");

            
            printPreviewDialog1.Document = doc.PrintDocument;//指向要打印的对象
            printPreviewDialog1.Document.PrinterSettings.MaximumPage = doc.Pages.Count;
            printPreviewDialog1.Document.PrinterSettings.FromPage = 1;
            printPreviewDialog1.Document.PrinterSettings.ToPage = doc.Pages.Count;
            printPreviewDialog1.PrintPreviewControl.Document.PrinterSettings.MaximumPage = 1;
            //doc.Close();
            this.printPreviewDialog1.ShowDialog();

        }

public PrintDocument pd = new PrintDocument();

        private void btnPrient_Click(object sender, EventArgs e)
        {
            //float y = 18.5f * 100f / 2.54f; // 728
            //float x = 13.5f * 100f / 2.54f; // 531
            
            Margins margin = new Margins(0, 0, 0, 0);
            pd.DefaultPageSettings.Margins = margin;
            pd.DefaultPageSettings.PaperSize = new PaperSize("Common Test 1", 531, 728);
            pd.DefaultPageSettings.PrinterSettings.PrinterName = "Canon iP110 series";
            pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
            pd.Print();
        }

        //打印内容的设置
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            //e.Graphics.DrawImage(System.Drawing.Image.FromFile("替换结果图片.png"),new PointF(0,0));
            //e.PageBounds.Width;
            //e.PageBounds.Height;
            System.Drawing.Image temp = System.Drawing.Image.FromFile("替换结果图片.png");//
            //System.Drawing.Image newImg = temp.GetThumbnailImage(e.PageBounds.Width, e.PageBounds.Height,
            //    new System.Drawing.Image.GetThumbnailImageAbort(IsTrue), IntPtr.Zero);
            e.Graphics.PageScale = 0.7f;
            e.Graphics.DrawImage(temp, new System.Drawing.Rectangle(0, 0, 135, 185));
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            //qrCodeGraphicControl1.Text = "https://www.baidu.com";
            SpirePdfUtils utils = new SpirePdfUtils();
            utils.createTemplate();
            Dictionary<String, String> dic = new Dictionary<string, string>();
            String data = "name=杨强惠;prov=昆明市呈贡区;place=斗南街道社区卫生服务中心";
            String[] dataStrArr = data.Split(';');
            foreach(String value in dataStrArr) {
                String[] valueArr = value.Split('=');
                if (dic.ContainsKey(valueArr[0])) continue;
                dic.Add(valueArr[0],valueArr[1]);
            }

            FillForm(//Environment.CurrentDirectory + "\\模版.pdf",
                Environment.CurrentDirectory + "\\无图纯字模版.pdf", 
                     Environment.CurrentDirectory + "\\替换.pdf",dic);
            Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
            pd.LoadFromFile(Environment.CurrentDirectory + "\\替换.pdf");

            System.Drawing.Image img = pd.SaveAsImage(0);
            img.Save("替换结果图片.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        // itext方式填充PDF表单
        public void FillForm(string pdfTemplate, string newFile, Dictionary<string, string> dic)
        {
            PdfReader pdfReader = null;
            PdfStamper pdfStamper = null;
            try
            {
                pdfReader = new PdfReader(pdfTemplate);
                pdfStamper = new PdfStamper(pdfReader, new FileStream(
                 newFile, FileMode.Create));
                AcroFields pdfFormFields = pdfStamper.AcroFields;
                //pdfFormFields.GenerateAppearances = true;
                pdfFormFields.RemoveXfa();
                //设置支持中文字体  
                BaseFont baseFont = BaseFont.CreateFont(Environment.CurrentDirectory + "\\Fonts\\汉仪大宋简.ttf",
                                                        BaseFont.IDENTITY_H, BaseFont.EMBEDDED );
                BaseFont font2 = BaseFont.CreateFont(Environment.CurrentDirectory + "\\Fonts\\汉仪中黑简.ttf",
                                                        BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont , 12f);
                //pdfFormFields.AddSubstitutionFont(baseFont);
                pdfFormFields.SetFieldProperty("name", "textfont", baseFont, null);
                pdfFormFields.SetFieldProperty("name", "textsize", 146f, null);
                //pdfFormFields.SetFieldProperty("name", "textcolor", new BaseColor(Color.MidnightBlue), null);//.FromName("#002A5C")

                pdfFormFields.SetFieldProperty("prov", "textfont", font2, null);
                pdfFormFields.SetFieldProperty("prov", "textsize", 72f, null);
                //pdfFormFields.SetFieldProperty("prov", "textcolor", new BaseColor(Color.Navy), null);//FromName("#002659")

                pdfFormFields.SetFieldProperty("place", "textfont", font2, null);
                pdfFormFields.SetFieldProperty("place", "textsize", 72f, null);
                //pdfFormFields.SetFieldProperty("place", "textcolor", new BaseColor(Color.Navy), null);

                foreach (KeyValuePair <string,string> de in dic)  
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }
                pdfStamper.FormFlattening = true;
                //pdfFormFields.GenerateAppearances = true;
                using (Stream inputImageStream = new FileStream(System.IO.Path.GetTempPath() + "\\20171128161244660.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var rect = pdfFormFields.GetFieldPositions("qrCode")[0].position;
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                    //image.SetAbsolutePosition(100,100);
                    image.SetAbsolutePosition(rect.GetLeft(0) , rect.GetBottom(0));
                    //image.ScaleAbsolute(200,200);
                    image.ScaleToFit(rect);
                    var pdfContentByte = pdfStamper.GetOverContent(1);
                    pdfContentByte.AddImage(image);
                }

            }
            catch (Exception ex)
            {
                Log.WriteLogLine(ex.Message);
            }
            finally
            {
                if (pdfStamper != null)
                {
                    pdfStamper.Close();
                }
                if (pdfReader != null)
                {
                    pdfReader.Close();
                }
            }
        }


        private void skinButton2_Click(object sender, EventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile("替换结果图片.png");

            float width = image.Width / image.HorizontalResolution;
            float height = image.Height / image.VerticalResolution;
            // 13.5 * 18.5

            float piexl = 13.5f / 2.54f / height;

            System.Drawing.Image newImg = image.GetThumbnailImage((int)Math.Round(image.Width * piexl), (int)Math.Round(image.Height * piexl),
                new System.Drawing.Image.GetThumbnailImageAbort(IsTrue), IntPtr.Zero);

            newImg.Save("新尺寸.png");


            List<String> lstPrinter = new List<string>();
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
               
                PrintDocument print = new PrintDocument();
                string sDefault = print.PrinterSettings.PrinterName;//默认打印机名
                lstPrinter.Add(sPrint);
            }



        }

        private static bool IsTrue()
        {
            return true;
        }
    }

    public static class FontSize {
        public static String[] FontSieArr = {
            "72", // 1号
            "63", // 2号 大特号
            "54", // 3号 特号
            "42", // 4号 初号
            "36", // 5号 小初号
            "31.5", // 6号 大一号
            "28", // 7号 一号
            "21", // 8号 二号
            "18", // 9号 小二号
            "16", // 10号 小三号
            "14", // 11号 四号
            "12", // 12号小四号
            "10.5", // 13号 五号
            "9", // 14号 小五号
            "8", // 15号 六号
            "6.875", //16号 小六号
            "5.25", // 17号 七号
            "4.5" // 18号 八号
        };

    }
}
