using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Web.UI;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Filters;
using Kaliko.ImageLibrary.Scaling;


namespace Library.Core.Imaging
{
    public class ThumbnailHelper
    {

        public static string CreateThumbnail(string imageFilePath, string thumbFilePath, string blurImgFilePathTemp, string croppedThumbFilePathTemp, int thumbWidth, int thumbHeight, int pageId = 0, bool maintainAspect = true,
            int thumbQuality = 90, int defaultImageWidth = 800, int defaultImageHeight = 600, int gaussianBlurValue = 25)
        {
            try
            {
                KalikoImage image = null;

                try
                {
                    image = new KalikoImage(imageFilePath);
                }
                catch { }


                if (image != null)
                {
                    if (pageId == 6)
                    {
                        // this is for Agency Logo 

                        //thumbHeight = 35;

                        if (thumbHeight == 35 || thumbHeight == 60)
                        {
                            decimal wid = (decimal) image.Width;
                            decimal ht = (decimal) image.Height;
                            decimal thumbHt = (decimal) thumbHeight;
                            decimal newWidth = (wid/ht)*thumbHt;

                            thumbWidth = (int) Math.Round(newWidth);
                        }
                        else
                        {
                            decimal wid = (decimal) image.Width;
                            decimal ht = (decimal) image.Height;
                            decimal thumbWd = (decimal) thumbWidth;
                            decimal newHt = (ht/wid)*thumbWd;

                            thumbHeight = (int) Math.Round(newHt);
                        }

                        // Create thumbnail by fit width and height with aspect ratio true
                        var thumb = image.Scale(new FitScaling(thumbWidth, thumbHeight));

                        if (imageFilePath.ToLower().Contains(".jpg") || imageFilePath.ToLower().Contains(".jpeg"))
                        {
                            thumb.SaveJpg(thumbFilePath, thumbQuality);
                        }
                        else if (imageFilePath.ToLower().Contains(".gif"))
                        {
                            thumb.SaveGif(thumbFilePath);
                        }
                        else if (imageFilePath.ToLower().Contains(".png"))
                        {
                            thumb.SavePng(thumbFilePath);
                        }
                        else if (imageFilePath.ToLower().Contains(".bmp"))
                        {
                            thumb.SaveBmp(thumbFilePath);
                        }
                    }
                    else
                    {
                        // Create image without maintaining Aspect Ratio using Resize Mehtod.
                        if (imageFilePath.ToLower().Contains(".jpg") || imageFilePath.ToLower().Contains(".jpeg"))
                        {
                            image.Resize(thumbWidth, thumbHeight);
                            image.SaveJpg(thumbFilePath, thumbQuality);
                        }
                        else if (imageFilePath.ToLower().Contains(".gif"))
                        {
                            image.Resize(thumbWidth, thumbHeight);
                            image.SaveGif(thumbFilePath);
                        }
                        else if (imageFilePath.ToLower().Contains(".png"))
                        {
                            image.Resize(thumbWidth, thumbHeight);
                            image.SavePng(thumbFilePath);
                        }
                        else if (imageFilePath.ToLower().Contains(".bmp"))
                        {
                            image.Resize(thumbWidth, thumbHeight);
                            image.SaveBmp(thumbFilePath);
                        }
                    }
                }
                else// if Kaliko Image library not gonna work to make image thumbnail then this following Microsoft Img Api will work for second try (in case of Kaliko Image library "Out of Memory" exception found as in history i found it for two image one of property image and other agency logo image)
                {
                    if (pageId == 6)
                    {
                        // this is for Agency Logo 
                        Bitmap source = new Bitmap(imageFilePath);
                        
                        //thumbHeight = 35;
                        if (thumbHeight == 35 || thumbHeight == 60)
                        {

                            decimal wid = (decimal)source.Width;
                            decimal ht = (decimal)source.Height;
                            decimal thumbHt = (decimal)thumbHeight;
                            decimal newWidth = (wid / ht) * thumbHt;

                            thumbWidth = (int)Math.Round(newWidth);
                        }
                        else
                        {
                            decimal wid = (decimal)source.Width;
                            decimal ht = (decimal)source.Height;
                            decimal thumbWd = (decimal)thumbWidth;
                            decimal newHt = (ht / wid) * thumbWd;

                            thumbHeight = (int)Math.Round(newHt);
                        }

                        Bitmap thumbImgBitmap = CreateThumbnailMicrosoftApi(imageFilePath, thumbWidth, thumbHeight, false); // no need to send true for maintainAspect ratio, as its calculating its height and width to our own formula in above code
                        if (thumbImgBitmap != null)
                        {
                            thumbImgBitmap.Save(thumbFilePath);
                        }
                    }
                    else
                    {
                        Bitmap thumbImgBitmap = CreateThumbnailMicrosoftApi(imageFilePath, thumbWidth, thumbHeight, false); 
                        if (thumbImgBitmap != null)
                        {
                            thumbImgBitmap.Save(thumbFilePath);
                        }
                    }
                }

                #region Old code for MaintainAspectRation == true

                //if (maintainAspect)
                //{
                //    //if (pageId != (int)EnumImagePageNos.AgencyProfileLogoLarge)
                //    if (pageId != 6)
                //    {
                //        // Create thumbnail by fit width and height with aspect ratio true
                //        KalikoImage thumb;

                //        //if (image.Width > thumbWidth || image.Height > thumbHeight) // i.e. original img height or width is more than required thumbs width or height, so now croping will done
                //        //{

                //        thumb = image.Scale(new FitScaling(thumbWidth, thumbHeight));
                //        // croping i.e. making thumbnail of req dimensions.

                //        if ((thumb.Width == thumbWidth || thumb.Width - 1 == thumbWidth || thumb.Width + 1 == thumbWidth) &&
                //            (thumb.Height == thumbHeight || thumb.Height - 1 == thumbHeight ||
                //             thumb.Height + 1 == thumbHeight))
                //        {
                //            if (imageFilePath.ToLower().Contains(".jpg") || imageFilePath.ToLower().Contains(".jpeg"))
                //            {
                //                thumb.SaveJpg(thumbFilePath, thumbQuality);
                //            }
                //            else if (imageFilePath.ToLower().Contains(".gif"))
                //            {
                //                thumb.SaveGif(thumbFilePath);
                //            }
                //            else if (imageFilePath.ToLower().Contains(".png"))
                //            {
                //                thumb.SavePng(thumbFilePath);
                //            }
                //            else if (imageFilePath.ToLower().Contains(".bmp"))
                //            {
                //                thumb.SaveBmp(thumbFilePath);
                //            }
                //        }
                //        else
                //        {
                //            // i.e. now blur technique will b applied. 
                //            var originalImg = new KalikoImage(imageFilePath);
                //            originalImg.Resize(thumbWidth, thumbHeight);

                //            originalImg.ApplyFilter(new GaussianBlurFilter(gaussianBlurValue));

                //            if (blurImgFilePathTemp.ToLower().Contains(".jpg") ||
                //                blurImgFilePathTemp.ToLower().Contains(".jpeg"))
                //            {
                //                originalImg.SaveJpg(blurImgFilePathTemp, thumbQuality);
                //            }
                //            else if (blurImgFilePathTemp.ToLower().Contains(".gif"))
                //            {
                //                originalImg.SaveGif(blurImgFilePathTemp);
                //            }
                //            else if (blurImgFilePathTemp.ToLower().Contains(".png"))
                //            {
                //                originalImg.SavePng(blurImgFilePathTemp);
                //            }
                //            else if (blurImgFilePathTemp.ToLower().Contains(".bmp"))
                //            {
                //                originalImg.SaveBmp(blurImgFilePathTemp);
                //            }

                //            // now add opacity in blurred image
                //            Image originalBlurredImg = new Bitmap(blurImgFilePathTemp);

                //            Bitmap originalBlurredOpacityImg = ChangeOpacity(originalBlurredImg, (float) 0.3);

                //            var imgWithBlurredAndOpacity = new KalikoImage(originalBlurredOpacityImg);


                //            // now save thumbnail that will be place in center of above Blurred and Opacited Image
                //            if (croppedThumbFilePathTemp.ToLower().Contains(".jpg") ||
                //                croppedThumbFilePathTemp.ToLower().Contains(".jpeg"))
                //            {
                //                thumb.SaveJpg(croppedThumbFilePathTemp, thumbQuality);
                //            }
                //            else if (croppedThumbFilePathTemp.ToLower().Contains(".gif"))
                //            {
                //                thumb.SaveGif(croppedThumbFilePathTemp);
                //            }
                //            else if (croppedThumbFilePathTemp.ToLower().Contains(".png"))
                //            {
                //                thumb.SavePng(croppedThumbFilePathTemp);
                //            }
                //            else if (croppedThumbFilePathTemp.ToLower().Contains(".bmp"))
                //            {
                //                thumb.SaveBmp(croppedThumbFilePathTemp);
                //            }

                //            var croppedThumbImage2BeCenter = new KalikoImage(croppedThumbFilePathTemp);

                //            int centerWidth = (thumbWidth - croppedThumbImage2BeCenter.Width)/2;
                //            int centerHeight = (thumbHeight - croppedThumbImage2BeCenter.Height)/2;


                //            // Now placing cropped thumbnail image on Blurred and Opacited image rite in center of it.
                //            imgWithBlurredAndOpacity.BlitImage(croppedThumbImage2BeCenter, centerWidth, centerHeight);

                //            //saving into file now.
                //            if (thumbFilePath.ToLower().Contains(".jpg") || thumbFilePath.ToLower().Contains(".jpeg"))
                //            {
                //                imgWithBlurredAndOpacity.SaveJpg(thumbFilePath, thumbQuality);
                //            }
                //            else if (thumbFilePath.ToLower().Contains(".gif"))
                //            {
                //                imgWithBlurredAndOpacity.SaveGif(thumbFilePath);
                //            }
                //            else if (thumbFilePath.ToLower().Contains(".png"))
                //            {
                //                imgWithBlurredAndOpacity.SavePng(thumbFilePath);
                //            }
                //            else if (thumbFilePath.ToLower().Contains(".bmp"))
                //            {
                //                imgWithBlurredAndOpacity.SaveBmp(thumbFilePath);
                //            }

                //            try
                //            {
                //                originalImg.Destroy();
                //                originalImg.Dispose();
                //                imgWithBlurredAndOpacity.Destroy();
                //                imgWithBlurredAndOpacity.Dispose();
                //                croppedThumbImage2BeCenter.Destroy();
                //                croppedThumbImage2BeCenter.Dispose();


                //                new FileInfo(blurImgFilePathTemp).DeleteIfExists();
                //                new FileInfo(croppedThumbFilePathTemp).DeleteIfExists();

                //            }
                //            catch
                //            {
                //            }

                //        }
                //        //}
                //    }
                //    else if (pageId == 6)
                //    {
                //        // this is for Agency Logo 

                //        //thumbHeight = 35;
                        
                //        decimal wid = (decimal) image.Width;
                //        decimal ht = (decimal) image.Height;
                //        decimal thumbHt = (decimal) thumbHeight;
                //        decimal newWidth = (wid/ht) * thumbHt;
                        
                //        thumbWidth = (int) Math.Round(newWidth);


                //        // Create thumbnail by fit width and height with aspect ratio true
                //        var thumb = image.Scale(new FitScaling(thumbWidth, thumbHeight));

                //        if (imageFilePath.ToLower().Contains(".jpg") || imageFilePath.ToLower().Contains(".jpeg"))
                //        {
                //            thumb.SaveJpg(thumbFilePath, thumbQuality);
                //        }
                //        else if (imageFilePath.ToLower().Contains(".gif"))
                //        {
                //            thumb.SaveGif(thumbFilePath);
                //        }
                //        else if (imageFilePath.ToLower().Contains(".png"))
                //        {
                //            thumb.SavePng(thumbFilePath);
                //        }
                //        else if (imageFilePath.ToLower().Contains(".bmp"))
                //        {
                //            thumb.SaveBmp(thumbFilePath);
                //        }
                //    }
                //    else
                //    {
                //        // Create thumbnail by fit width and height with aspect ratio true
                //        var thumb = image.Scale(new FitScaling(thumbWidth, thumbHeight));

                //        if (imageFilePath.ToLower().Contains(".jpg") || imageFilePath.ToLower().Contains(".jpeg"))
                //        {
                //            thumb.SaveJpg(thumbFilePath, thumbQuality);
                //        }
                //        else if (imageFilePath.ToLower().Contains(".gif"))
                //        {
                //            thumb.SaveGif(thumbFilePath);
                //        }
                //        else if (imageFilePath.ToLower().Contains(".png"))
                //        {
                //            thumb.SavePng(thumbFilePath);
                //        }
                //        else if (imageFilePath.ToLower().Contains(".bmp"))
                //        {
                //            thumb.SaveBmp(thumbFilePath);
                //        }
                //    }
                //}
                //else
                //{
                //    // Create image without maintaining Aspect Ratio using Resize Mehtod.
                //    if (imageFilePath.ToLower().Contains(".jpg") || imageFilePath.ToLower().Contains(".jpeg"))
                //    {
                //        image.Resize(thumbWidth, thumbHeight);
                //        image.SaveJpg(thumbFilePath, thumbQuality);
                //    }
                //    else if (imageFilePath.ToLower().Contains(".gif"))
                //    {
                //        image.Resize(thumbWidth, thumbHeight);
                //        image.SaveGif(thumbFilePath);
                //    }
                //    else if (imageFilePath.ToLower().Contains(".png"))
                //    {
                //        image.Resize(thumbWidth, thumbHeight);
                //        image.SavePng(thumbFilePath);
                //    }
                //    else if (imageFilePath.ToLower().Contains(".bmp"))
                //    {
                //        image.Resize(thumbWidth, thumbHeight);
                //        image.SaveBmp(thumbFilePath);
                //    }
                //}

#endregion
                
                return "Ok";
            }
            catch (Exception ex)
            {
                return "Exception: " + ex.Message;
            }
        }

        public static Bitmap CreateThumbnailMicrosoftApi(string imageFilePath, int thumbWidth, int thumbHeight, bool maintainAspect = true)
        {
            Bitmap source = new Bitmap(imageFilePath);
            return CreateThumbnail(source, thumbWidth, thumbHeight, maintainAspect);
        }

        public static Bitmap CreateThumbnailfromUrl(string imageFileUrl, int thumbWidth, int thumbHeight,
            bool maintainAspect = true)
        {
            try
            {
                System.Net.WebRequest request =
                    System.Net.WebRequest.Create(imageFileUrl);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                Bitmap source = new Bitmap(responseStream);
                return CreateThumbnail(source, thumbWidth, thumbHeight, maintainAspect);
            }
            catch { }
            return null;
        }

        public static Bitmap CreateThumbnail(byte[] imageBytes, int thumbWidth, int thumbHeight, bool maintainAspect = true)
        {
            MemoryStream imgStream = new MemoryStream(imageBytes);
            Bitmap source = new Bitmap(imgStream);
            return CreateThumbnail(source, thumbWidth, thumbHeight, maintainAspect);
        }


        public static Bitmap CreateThumbnail(Bitmap source, int thumbWidth, int thumbHeight, bool maintainAspect = true)
        {
            System.Drawing.Bitmap ret = null;

            if (source != null)
            {
                // return the source image if it's smaller than the designated thumbnail
                //if (source.Width < thumbWidth && source.Height < thumbHeight) return source;

                try
                {
                    int wi, hi;

                    wi = thumbWidth;
                    hi = thumbHeight;

                    if (maintainAspect)
                    {
                        // maintain the aspect ratio despite the thumbnail size parameters
                        if (source.Width > source.Height)
                        {
                            wi = thumbWidth;
                            hi = (int)(source.Height * ((decimal)thumbWidth / source.Width));
                        }
                        else
                        {
                            hi = thumbHeight;
                            wi = (int)(source.Width * ((decimal)thumbHeight / source.Height));
                        }
                    }

                    // original code that creates lousy thumbnails
                    // System.Drawing.Image ret = source.GetThumbnailImage(wi,hi,null,IntPtr.Zero);

                    ret = new Bitmap(wi, hi);
                    using (Graphics g = Graphics.FromImage(ret))
                    {
                        //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

                        //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        //g.SmoothingMode = SmoothingMode.HighQuality;
                        //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        //g.CompositingQuality = CompositingQuality.HighQuality;

                        g.FillRectangle(Brushes.White, 0, 0, wi, hi);
                        g.DrawImage(source, 0, 0, wi, hi);
                        source.Dispose();
                    }
                }
                catch(Exception ex)
                {

                    throw ex;
                }

            }

            return ret;
        }

        public static Bitmap ChangeOpacity(Image imgPic, float opacityvalue)
        {

            Bitmap bmpPic = new Bitmap(imgPic.Width, imgPic.Height);
            Graphics gfxPic = Graphics.FromImage(bmpPic);

            gfxPic.Clear(Color.White);

            ColorMatrix cmxPic = new ColorMatrix();
            cmxPic.Matrix33 = opacityvalue;

            ImageAttributes iaPic = new ImageAttributes();
            iaPic.SetColorMatrix(cmxPic, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            gfxPic.DrawImage(imgPic, new Rectangle(0, 0, bmpPic.Width, bmpPic.Height), 0, 0, imgPic.Width, imgPic.Height,
                GraphicsUnit.Pixel, iaPic);
            gfxPic.Dispose();

            return bmpPic;
        }

        
    }
}
