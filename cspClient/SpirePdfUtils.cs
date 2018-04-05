using Spire.Pdf;
using Spire.Pdf.Fields;
using Spire.Pdf.Graphics;
using Spire.Pdf.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace cspClient
{
    class SpirePdfUtils
    {

        public void FillForm() {

            PdfDocument document = new PdfDocument();
            document.LoadFromFile(Environment.CurrentDirectory + "\\模版.pdf");
            PdfPageBase page = document.Pages[0];

            PdfFormWidget form = document.Form as PdfFormWidget;
            PdfTextBoxFieldWidget textboxField = form.FieldsWidget[0] as PdfTextBoxFieldWidget;
            
            String fontFileName = Environment.CurrentDirectory + "\\Fonts\\汉仪大宋简.ttf";
            PdfTrueTypeFont trueTypeFont = new PdfTrueTypeFont(fontFileName, 72f);
            textboxField.Font = trueTypeFont;
            textboxField.Text = "测试文本内容";

            document.SaveToFile("Spire结果测试_"+DateTime.Now.ToString("yyyyMMddHHmmss")+".pdf", FileFormat.PDF);
        }

        /**
         * 创建PDF模版
         */
        public void createTemplate() {
            PdfDocument doc = new PdfDocument();
            Image image = Image.FromFile("原图.png"); //System.IO.Path.GetTempPath()
            PdfImage pdfimage = PdfImage.FromImage(image);
            PdfUnitConvertor uinit = new PdfUnitConvertor();
            //SizeF pageSize = uinit.ConvertFromPixels(image.Size, PdfGraphicsUnit.Point);
            SizeF pageSize = image.Size;
            PdfPageBase page = doc.Pages.Add(pageSize, new PdfMargins(0f));
            page.Canvas.DrawImage(pdfimage, new PointF(0, 0), pageSize);

            PdfTextBoxField name = new PdfTextBoxField(page, "name");
            name.Bounds = new RectangleF(85, 711, 1000, 170);
            name.BorderWidth = 0f;
            //textbox.BorderStyle = PdfBorderStyle.Solid;
            name.TextAlignment = PdfTextAlignment.Center;
            doc.Form.Fields.Add(name);

            PdfTextBoxField prov = new PdfTextBoxField(page, "prov");
            prov.Bounds = new RectangleF(85, 925, 1000, 110);
            prov.BorderWidth = 0f;
            prov.TextAlignment = PdfTextAlignment.Center;
            doc.Form.Fields.Add(prov);

            PdfTextBoxField place = new PdfTextBoxField(page, "place");
            place.Bounds = new RectangleF(85, 1035, 1000, 110);
            place.BorderWidth = 0f;
            place.TextAlignment = PdfTextAlignment.Center;
            doc.Form.Fields.Add(place);

            PdfTextBoxField qrCode = new PdfTextBoxField(page, "qrCode");
            qrCode.Bounds = new RectangleF(491, 1252, 188, 188);
            qrCode.BorderWidth = 0f;
            qrCode.TextAlignment = PdfTextAlignment.Center;
            qrCode.Visible = false;
            doc.Form.Fields.Add(qrCode);

            doc.SaveToFile("模版.pdf");
            doc.LoadFromFile("模版.pdf");
            Image img = doc.SaveAsImage(0);
            img.Save("模版转储图片.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            //PdfDocument pdtest = new PdfDocument();
            //Image src = Image.FromFile(@"原图.png");
            //PdfImage pi = PdfImage.FromImage(src);

            //PdfUnitConvertor puinit = new PdfUnitConvertor();

            ////SizeF ps = puinit.ConvertFromPixels(src.Size, PdfGraphicsUnit.Point);
            //SizeF ps = src.Size;


            //PdfPageBase p = pdtest.Pages.Add(ps, new PdfMargins(0f));

            //p.Canvas.DrawImage(pi, new PointF(0, 0),ps);
            //pdtest.SaveToFile("sample.pdf");

            //pdtest.LoadFromFile("sample.pdf");
            //Image res = pdtest.SaveAsImage(0);
            //res.Save("sample.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
