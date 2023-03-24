using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace HI.UL
{
    public static class ULImage
    {
        public enum PicType : int
        {
            Nothing = 0,
            Employee = 1,
            License = 2
        }

        public static string SaveImage(ref DevExpress.XtraEditors.PictureEdit pic, string newName, string pathName)
        {

            try
            {
                if (!Directory.Exists(pathName))
                {
                    Directory.CreateDirectory(pathName);
                }

                newName = Strings.Replace(newName, "\\", "_");
                newName = Strings.Replace(newName, "/", "_");

                if (pic.Image != null)
                {
                    try
                    {
                        if (File.Exists(pathName + "\\" + newName + ".JPG") == true)
                        {
                            File.Delete(pathName + "\\" + newName + ".JPG");
                        }
                    }
                    catch 
                    {
                    }

                    try
                    {
                        pic.Image.Save(pathName + "\\" + newName + ".JPG");
                    }
                    catch
                    {
                    }

                    return newName + ".JPG";
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string SaveImage(Image image, string newName, string pathName)
        {

            try
            {
                DevExpress.XtraEditors.PictureEdit pic = new DevExpress.XtraEditors.PictureEdit();

                try
                {
                    pic.Image = image;
                }
                catch (Exception ex)
                {
                    pic.Image = null;
                }

                if (!Directory.Exists(pathName))
                {
                    Directory.CreateDirectory(pathName);
                }

                newName = Strings.Replace(newName, "\\", "_");
                newName = Strings.Replace(newName, "/", "_");

                if (pic.Image != null)
                {
                    try
                    {
                        if (File.Exists(pathName + "\\" + newName + ".JPG") == true)
                        {
                            File.Delete(pathName + "\\" + newName + ".JPG");
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        pic.Image.Save(pathName + "\\" + newName + ".JPG");
                    }
                    catch (Exception ex)
                    {
                    }

                    return newName + ".JPG";
                }
                else
                {
                    return "";
                }
                pic =null;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static Image LoadImage(string ImagePath)
        {
            try
            {
                return Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(ImagePath)));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static byte[] ConvertImageToByteArray(string fileName, Size _size)
        {
            MemoryStream stream = new MemoryStream();

            try
            {
                Image _img = ResizeImage(Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(fileName))), _size);
                Bitmap Bmp = new Bitmap(_img);
                Bmp.Save(stream, ImageFormat.Jpeg);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return stream.ToArray();
        }

        public static byte[] ConvertImageToByteArray(string fileName, PicType Picsize = PicType.Nothing)
        {
            MemoryStream stream = new MemoryStream();

            try
            {
                Image _img = ResizeImage(Image.FromStream(new MemoryStream(System.IO.File.ReadAllBytes(fileName))), Picsize);
                Bitmap Bmp = new Bitmap(_img);
                Bmp.Save(stream, ImageFormat.Jpeg);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return stream.ToArray();

        }

        public static byte[] ConvertImageToByteArray(Image image, Size _size)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                if (image == null)
                {
                    return stream.ToArray();
                }
                else
                {
                    Image _img = ResizeImage(image, _size);
                    Bitmap Bmp = new Bitmap(_img);
                    Bmp.Save(stream, ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return stream.ToArray();
        }

        public static byte[] ConvertImageToByteArray(Image image, PicType Picsize = PicType.Nothing)
        {
            MemoryStream stream = new MemoryStream();

            try
            {
                if (image == null)
                {
                    return stream.ToArray();
                }
                else
                {
                    Image _img = ResizeImage(image, Picsize);
                    Bitmap Bmp = new Bitmap(_img);
                    Bmp.Save(stream, ImageFormat.Jpeg);

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return stream.ToArray();
        }

        public static Image ConvertByteArrayToImmage(object fileName)
        {
            try
            {
                return Image.FromStream(new MemoryStream((byte[])fileName), true);
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static Image ResizeImage(Image image, PicType Picsize, bool preserveAspectRatio = true)
        {

            Size _size = new Size(1024, 768);
            switch (Picsize)
            {
                case PicType.Employee:
                    _size = new Size(1024, 768);
                    break;
                case PicType.License:
                    _size = new Size(1024, 768);
                    break;
            }

            int newWidth = 0;
            int newHeight = 0;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = Convert.ToSingle(_size.Width) / Convert.ToSingle(originalWidth);
                float percentHeight = Convert.ToSingle(_size.Height) / Convert.ToSingle(originalHeight);
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = Convert.ToInt32(originalWidth * percent);
                newHeight = Convert.ToInt32(originalHeight * percent);
            }
            else
            {
                newWidth = _size.Width;
                newHeight = _size.Height;
            }

            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        public static Image ResizeImage(Image image, Size _size, bool preserveAspectRatio = true)
        {
            int newWidth = 0;
            int newHeight = 0;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = Convert.ToSingle(_size.Width) / Convert.ToSingle(originalWidth);
                float percentHeight = Convert.ToSingle(_size.Height) / Convert.ToSingle(originalHeight);
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = Convert.ToInt32(originalWidth * percent);
                newHeight = Convert.ToInt32(originalHeight * percent);
            }
            else
            {
                newWidth = _size.Width;
                newHeight = _size.Height;
            }

            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
    }
}
