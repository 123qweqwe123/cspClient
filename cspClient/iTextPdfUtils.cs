using cspClient.utils;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Forms;
using Gma.QrCodeNet.Encoding.Windows.Render;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace cspClient
{
    class iTextPdfUtils
    {

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
                                                        BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                BaseFont font2 = BaseFont.CreateFont(Environment.CurrentDirectory + "\\Fonts\\汉仪中黑简.ttf",
                                                        BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12f);
                
                if (dic.ContainsKey("name") && pdfFormFields.Fields.ContainsKey("name"))
                {
                    pdfFormFields.SetFieldProperty("name", "textfont", baseFont, null);
                    if (dic["name"].ToString().Length <= 7)
                    {
                        pdfFormFields.SetFieldProperty("name", "textsize", 146f, null);
                    }
                    else if (dic["name"].ToString().Length <= 13)
                    {
                        pdfFormFields.SetFieldProperty("name", "textsize", 76f, null);
                    }
                    else if (dic["name"].ToString().Length <= 26)
                    {
                        pdfFormFields.SetFieldProperty("name", "textsize", 36f, null);
                    }
                    else
                    {
                        pdfFormFields.SetFieldProperty("name", "textsize", 18f, null);
                    }

                    pdfFormFields.SetFieldProperty("name", "textcolor", new BaseColor(ColorTranslator.FromHtml("#002A5B")), null);
                }                


                if (dic.ContainsKey("prov") && pdfFormFields.Fields.ContainsKey("prov"))
                {
                    pdfFormFields.SetFieldProperty("prov", "textfont", font2, null);
                    pdfFormFields.SetFieldProperty("prov", "textsize", 76f, null);

                    if (dic["prov"].ToString().Length <= 13)
                    {
                        pdfFormFields.SetFieldProperty("prov", "textsize", 76f, null);
                    }
                    else if (dic["prov"].ToString().Length <= 27)
                    {
                        pdfFormFields.SetFieldProperty("prov", "textsize", 36f, null);
                    }
                    else if (dic["prov"].ToString().Length <= 54)
                    {
                        pdfFormFields.SetFieldProperty("prov", "textsize", 18f, null);
                    }
                    else
                    {
                        pdfFormFields.SetFieldProperty("prov", "textsize", 9f, null);
                    }
                    pdfFormFields.SetFieldProperty("prov", "textcolor", new BaseColor(ColorTranslator.FromHtml("#002A5B")), null);
                }
                
                
                if ( dic.ContainsKey("place") && pdfFormFields.Fields.ContainsKey("place") )
                {
                    pdfFormFields.SetFieldProperty("place", "textfont", font2, null);
                    pdfFormFields.SetFieldProperty("place", "textsize", 76f, null);
                    if (dic["place"].ToString().Length <= 13)
                    {
                        pdfFormFields.SetFieldProperty("place", "textsize", 76f, null);
                    }
                    else
                    {
                        if (dic["place"].ToString().Length <= 26)
                        {
                            String placeStr = dic["place"].ToString();
                            dic["place"] = placeStr.Substring(0, 13);
                            dic["p2"] = placeStr.Substring(13);
                            pdfFormFields.SetFieldProperty("place", "textsize", 76f, null);
                            pdfFormFields.SetFieldProperty("p2", "textsize", 76f, null);
                        }
                        else if (dic["place"].ToString().Length <= 52)
                        {
                            String placeStr = dic["place"].ToString();
                            dic["place"] = placeStr.Substring(0, 26);
                            dic["p2"] = placeStr.Substring(26);
                            pdfFormFields.SetFieldProperty("place", "textsize", 36f, null);
                            pdfFormFields.SetFieldProperty("p2", "textsize", 36f, null);
                        }
                        else if (dic["place"].ToString().Length <= 104)
                        {
                            String placeStr = dic["place"].ToString();
                            dic["place"] = placeStr.Substring(0, 52);
                            dic["p2"] = placeStr.Substring(52);
                            pdfFormFields.SetFieldProperty("place", "textsize", 18f, null);
                            pdfFormFields.SetFieldProperty("p2", "textsize", 18f, null);
                        }
                        pdfFormFields.SetFieldProperty("p2", "textcolor", new BaseColor(ColorTranslator.FromHtml("#002A5B")), null);
                    }
                    pdfFormFields.SetFieldProperty("place", "textcolor", new BaseColor(ColorTranslator.FromHtml("#002A5B")), null);
                    if (dic.ContainsKey("p2"))
                    {
                        pdfFormFields.SetFieldProperty("p2", "textfont", font2, null);
                    }
                }
                

                foreach (KeyValuePair<string, string> de in dic)
                {
                    pdfFormFields.SetField(de.Key, de.Value);
                }
                pdfStamper.FormFlattening = true;

                var qrCodeGraphicControl1 = new QrCodeGraphicControl();
                qrCodeGraphicControl1.Text = dic["qrCode"];
                GraphicsRenderer gRender = new GraphicsRenderer(new FixedModuleSize(30, QuietZoneModules.Four));
                BitMatrix matrix = qrCodeGraphicControl1.GetQrMatrix();
                using (MemoryStream stream = new MemoryStream())
                {
                    gRender.WriteToStream(matrix, ImageFormat.Png, stream);
                    var rect = pdfFormFields.GetFieldPositions("qrCode")[0].position;

                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(System.Drawing.Image.FromStream(stream), ImageFormat.Png);
                    image.SetAbsolutePosition(rect.GetLeft(0), rect.GetBottom(0));
                    image.ScaleToFit(rect);
                    var pdfContentByte = pdfStamper.GetOverContent(1);
                    pdfContentByte.AddImage(image);
                }

                //using (Stream inputImageStream = new FileStream(System.IO.Path.GetTempPath() + "\\20171128161244660.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                //{
                //    var rect = pdfFormFields.GetFieldPositions("qrCode")[0].position;
                //    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                //    image.SetAbsolutePosition(rect.GetLeft(0), rect.GetBottom(0));
                //    image.ScaleToFit(rect);
                //    var pdfContentByte = pdfStamper.GetOverContent(1);
                //    pdfContentByte.AddImage(image);
                //}

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

        public void savePdfAsImage(String pdfPath, String imageName)
        {

            Spire.Pdf.PdfDocument pd = new Spire.Pdf.PdfDocument();
            pd.LoadFromFile(pdfPath);
            System.Drawing.Image img = pd.SaveAsImage(0);

            img.Save(imageName, ImageFormat.Png);

        }
    }
}
