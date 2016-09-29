using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
namespace WebHelper
{
    public class ImageUtil
    {
        public static string ImageToBase64(Image image)
        {
            return Convert.ToBase64String(ImageToBytes(image));
        }
        public static byte[] ImageToBytes(Image image)
        {
            if (image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                ImageFormat imgfmt;
                if (ImageFormat.Jpeg.Equals(image.RawFormat))
                    imgfmt = ImageFormat.Jpeg;
                else if (ImageFormat.Png.Equals(image.RawFormat))
                    imgfmt = ImageFormat.Png;
                else if (ImageFormat.Gif.Equals(image.RawFormat))
                    imgfmt = ImageFormat.Gif;
                else if (ImageFormat.Bmp.Equals(image.RawFormat))
                    imgfmt = ImageFormat.Bmp;
                else
                    throw new Exception("The image was invalid type.");

                image.Save(ms, imgfmt);
                return ms.GetBuffer();
            }
        }
        public static Image Base64ToImage(string base64String)
        {
            return BytesToImage(Convert.FromBase64String(base64String));
        }
        public static Image BytesToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public static Size GetAspectSize(Size originalSize, float maxDimension)
        {
            float widthRatio = maxDimension / originalSize.Width;
            float heightRatio = maxDimension / originalSize.Height;
            float aspectRatio = heightRatio > widthRatio ? widthRatio : heightRatio;
            return new Size(Convert.ToInt32(aspectRatio * originalSize.Width),
                Convert.ToInt32(aspectRatio * originalSize.Height));
        }
        public static byte[] GetThumbnail(System.Drawing.Image OriginalImg, Size aspectSize, long quality)
        {
            Bitmap thumbnail = new Bitmap(aspectSize.Width, aspectSize.Height);
            Graphics g = Graphics.FromImage((System.Drawing.Image)thumbnail);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(OriginalImg, 0, 0, aspectSize.Width, aspectSize.Height);
            g.Dispose();

            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            using (MemoryStream ms = new MemoryStream())
            {
                thumbnail.Save(ms, jpegCodec, encoderParams);
                return ms.GetBuffer();
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
