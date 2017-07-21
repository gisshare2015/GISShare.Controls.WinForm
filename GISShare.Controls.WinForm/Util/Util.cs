using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.Util
{
    /// <summary>
    /// TX 图形 通用方法
    /// </summary>
    public static class UtilTX
    {
        /// <summary>
        /// 在该矩形的比例下根据指定点获取放大或缩小后的矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="point"></param>
        /// <param name="dScale"></param>
        /// <returns></returns>
        public static RectangleF CreateRectangle(RectangleF rectangle, Point point, double dScale)
        {
            RectangleF rectangleF = new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            //
            double dXScaleLeft = ((double)point.X - (double)rectangle.Left) / (double)rectangle.Width;
            double dYScaleTop = ((double)point.Y - (double)rectangle.Top) / (double)rectangle.Height;
            if (dScale != 1)
            {
                double dW = (dScale - 1) * (double)rectangle.Width;
                double dH = (dScale - 1) * (double)rectangle.Height;
                double dLeft = dXScaleLeft * dW;
                double dRight = dW - dLeft;
                double dTop = dYScaleTop * dH;
                double dBottom = dH - dTop;
                rectangleF = RectangleF.FromLTRB(
                    (float)((double)rectangle.Left - dLeft), 
                    (float)((double)rectangle.Top - dTop), 
                    (float)((double)rectangle.Right + dRight),
                    (float)((double)rectangle.Bottom + dBottom)
                    );
            }
            //
            return rectangleF;
        }

        /// <summary>
        /// 在该矩形内创建一个指定长宽的内嵌相似矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns></returns>
        public static RectangleF CreateRectangle(Rectangle rectangle, double dWidth, double dHeight)
        {
            double dW;
            double dH;
            //
            if (rectangle.Width > rectangle.Height)
            {
                dW = dWidth / dHeight * (double)rectangle.Height;
                dH = dHeight / dWidth * dW;
            }
            else
            {
                dH = dHeight / dWidth * (double)rectangle.Height;
                dW = dWidth / dHeight * dH;
            }
            //
            return new RectangleF((float)(((double)rectangle.Left + (double)rectangle.Right - dW) / 2D), (float)(((double)rectangle.Top + (double)rectangle.Bottom - dH) / 2D), (float)dW, (float)dH);
        }

        /// <summary>
        /// 在该矩形内创建一个指定长宽的矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <returns></returns>
        public static Rectangle CreateRectangle(Rectangle rectangle, int iWidth, int iHeight)
        {
            iWidth = rectangle.Width < iWidth ? rectangle.Width : iWidth;
            iHeight = rectangle.Height < iHeight ? rectangle.Height : iHeight;
            //
            return new Rectangle((rectangle.Left + rectangle.Right - iWidth) / 2, (rectangle.Top + rectangle.Bottom - iHeight) / 2, iWidth, iHeight);
        }

        /// <summary>
        /// 在该矩形内部创建一个内接正方形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Rectangle CreateSquareRectangle(Rectangle rectangle)
        {
            if (rectangle.Width > rectangle.Height)
            {
                return new Rectangle((rectangle.Left + rectangle.Right - rectangle.Height) / 2, rectangle.Top, rectangle.Height, rectangle.Height);
            }
            else if (rectangle.Width < rectangle.Height)
            {
                return new Rectangle(rectangle.Left, (rectangle.Top + rectangle.Bottom - rectangle.Width) / 2, rectangle.Width, rectangle.Width);
            }
            //
            return rectangle;
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        public static Region CreateRoundRectangle(int x1, int y1, int x2, int y2, int cx, int cy)
        {
            IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(x1, y1, x2, y2, cx, cy);
            Region region = Region.FromHrgn(hRegion);
            region.ReleaseHrgn(hRegion);
            //
            return region;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="iLeftTopRadius"></param>
        /// <param name="iRightTopRadius"></param>
        /// <param name="iLeftBottomRadius"></param>
        /// <param name="iRightBottomRadius"></param>
        /// <returns></returns>
        public static Region CreateRoundRectangle2(Rectangle rectangle, int iLeftTopRadius, int iRightTopRadius, int iLeftBottomRadius, int iRightBottomRadius)
        {
            if (iLeftTopRadius > 0 && 
                iLeftTopRadius == iRightTopRadius && 
                iLeftTopRadius == iLeftBottomRadius && 
                iLeftTopRadius == iRightBottomRadius)
            {
                IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, iLeftTopRadius, iLeftTopRadius);
                Region region = Region.FromHrgn(hRegion);
                region.ReleaseHrgn(hRegion);
                //
                return region;
            }
            //
            Region regionMain = new Region(rectangle);
            if (iLeftTopRadius > 0)
            {
                IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, iLeftTopRadius, iLeftTopRadius);
                Region region = Region.FromHrgn(hRegion);
                region.ReleaseHrgn(hRegion);
                region.Intersect(new Rectangle(rectangle.Left, rectangle.Top, iLeftTopRadius, iLeftTopRadius));
                regionMain.Xor(new Rectangle(rectangle.Left, rectangle.Top, iLeftTopRadius, iLeftTopRadius));
                regionMain.Union(region);
            }
            if (iRightTopRadius > 0)
            {
                IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, iRightTopRadius, iRightTopRadius);
                Region region = Region.FromHrgn(hRegion);
                region.ReleaseHrgn(hRegion);
                region.Intersect(new Rectangle(rectangle.Right - iRightTopRadius, rectangle.Top, iRightTopRadius, iRightTopRadius));
                regionMain.Xor(new Rectangle(rectangle.Right - iRightTopRadius, rectangle.Top, iRightTopRadius, iRightTopRadius));
                regionMain.Union(region);
            }
            if (iRightBottomRadius > 0)
            {
                IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, iRightBottomRadius, iRightBottomRadius);
                Region region = Region.FromHrgn(hRegion);
                region.ReleaseHrgn(hRegion);
                region.Intersect(new Rectangle(rectangle.Right - iRightBottomRadius, rectangle.Bottom, iRightBottomRadius, iRightBottomRadius));
                regionMain.Xor(new Rectangle(rectangle.Right - iRightBottomRadius, rectangle.Bottom, iRightBottomRadius, iRightBottomRadius));
                regionMain.Union(region);
            }
            if (iLeftBottomRadius > 0)
            {
                IntPtr hRegion = GISShare.Win32.API.CreateRoundRectRgn(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom, iLeftBottomRadius, iLeftBottomRadius);
                Region region = Region.FromHrgn(hRegion);
                region.ReleaseHrgn(hRegion);
                region.Intersect(new Rectangle(rectangle.Right - iLeftBottomRadius, rectangle.Bottom - iLeftBottomRadius, iLeftBottomRadius, iLeftBottomRadius));
                regionMain.Xor(new Rectangle(rectangle.Right - iLeftBottomRadius, rectangle.Bottom - iLeftBottomRadius, iLeftBottomRadius, iLeftBottomRadius));
                regionMain.Union(region);
            }
            return regionMain;
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="iLeftTopRadius"></param>
        /// <param name="iRightTopRadius"></param>
        /// <param name="iLeftBottomRadius"></param>
        /// <param name="iRightBottomRadius"></param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int iLeftTopRadius, int iRightTopRadius, int iLeftBottomRadius, int iRightBottomRadius)
        {
            GraphicsPath path = new GraphicsPath();
            //
            if (iLeftTopRadius <= 0)
            {
                if (iRightTopRadius > 0) { path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - iRightTopRadius, rectangle.Top); }
                else { path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top); }
            }
            else
            {
                path.AddArc(rectangle.Left, rectangle.Top, iLeftTopRadius, iLeftTopRadius, 180, 90);
                //if (iRightTopRadius > 0) { path.AddLine(rectangle.Left + iLeftTopRadius, rectangle.Top, rectangle.Right - iRightTopRadius, rectangle.Top); }
                //else { path.AddLine(rectangle.Left + iLeftTopRadius, rectangle.Top, rectangle.Right, rectangle.Top); }
            }
            //
            if (iRightTopRadius <= 0)
            {
                if (iRightBottomRadius > 0) { path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - iRightBottomRadius); }
                else { path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom); }
            }
            else
            {
                path.AddArc(rectangle.Right - iRightTopRadius, rectangle.Top, iRightTopRadius, iRightTopRadius, 270, 90);
                //if (iRightBottomRadius > 0) { path.AddLine(rectangle.Right, rectangle.Top + iRightTopRadius, rectangle.Right, rectangle.Bottom - iRightBottomRadius); }
                //else { path.AddLine(rectangle.Right, rectangle.Top + iRightTopRadius, rectangle.Right, rectangle.Bottom); }
            }
            //
            if (iRightBottomRadius <= 0)
            {
                if (iLeftBottomRadius > 0) { path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left + iLeftBottomRadius, rectangle.Bottom); }
                else { path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom); }
            }
            else
            {
                path.AddArc(rectangle.Right - iRightBottomRadius, rectangle.Bottom - iRightBottomRadius, iRightBottomRadius, iRightBottomRadius, 0, 90);
                //if (iLeftBottomRadius > 0) { path.AddLine(rectangle.Right - iRightBottomRadius, rectangle.Bottom, rectangle.Left + iLeftBottomRadius, rectangle.Bottom); }
                //else { path.AddLine(rectangle.Right - iRightBottomRadius, rectangle.Bottom, rectangle.Left, rectangle.Bottom); }
            }
            //
            if (iLeftBottomRadius <= 0)
            {
                if (iLeftTopRadius > 0) { path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top + iLeftTopRadius); }
                else { path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top); }
            }
            else
            {
                path.AddArc(rectangle.Left, rectangle.Bottom - iLeftBottomRadius, iLeftBottomRadius, iLeftBottomRadius, 90, 90);
                //if (iLeftTopRadius > 0) { path.AddLine(rectangle.Left, rectangle.Bottom - iLeftBottomRadius, rectangle.Left, rectangle.Top + iLeftTopRadius); }
                //else { path.AddLine(rectangle.Left, rectangle.Bottom - iLeftBottomRadius, rectangle.Left, rectangle.Top); }
            }
            //
            path.CloseAllFigures();

            return path;
        }

        /// <summary>
        /// 创建五角星
        /// </summary>
        /// <param name="rectangle">外接矩形</param>
        /// <returns></returns>
        public static PointF[] CreateStar(Rectangle rectangle)
        {
            PointF[] pointFArray = new PointF[10];
            //
            pointFArray[0].X = rectangle.X + (rectangle.Width / 2);
            pointFArray[0].Y = rectangle.Y;
            pointFArray[1].X = rectangle.X + (42 * rectangle.Width / 64);
            pointFArray[1].Y = rectangle.Y + (19 * rectangle.Height / 64);
            pointFArray[2].X = rectangle.X + rectangle.Width;
            pointFArray[2].Y = rectangle.Y + (22 * rectangle.Height / 64);
            pointFArray[3].X = rectangle.X + (48 * rectangle.Width / 64);
            pointFArray[3].Y = rectangle.Y + (38 * rectangle.Height / 64);
            pointFArray[4].X = rectangle.X + (52 * rectangle.Width / 64);
            pointFArray[4].Y = rectangle.Y + rectangle.Height;
            pointFArray[5].X = rectangle.X + (rectangle.Width / 2);
            pointFArray[5].Y = rectangle.Y + (52 * rectangle.Height / 64);
            pointFArray[6].X = rectangle.X + (12 * rectangle.Width / 64);
            pointFArray[6].Y = rectangle.Y + rectangle.Height;
            pointFArray[7].X = rectangle.X + rectangle.Width / 4;
            pointFArray[7].Y = rectangle.Y + (38 * rectangle.Height / 64);
            pointFArray[8].X = rectangle.X;
            pointFArray[8].Y = rectangle.Y + (22 * rectangle.Height / 64);
            pointFArray[9].X = rectangle.X + (22 * rectangle.Width / 64);
            pointFArray[9].Y = rectangle.Y + (19 * rectangle.Height / 64);
            //
            return pointFArray;
        }

        /// <summary>
        /// 创建TabButton外轮廓
        /// </summary>
        /// <param name="eTabAlignment"></param>
        /// <param name="rectangle"></param>
        /// <param name="iRadius1"></param>
        /// <param name="iRadius2"></param>
        /// <returns></returns>
        public static GraphicsPath CreateTabButtonContour(TabAlignment eTabAlignment, Rectangle rectangle, int iRadius1, int iRadius2)
        {
            GraphicsPath path = new GraphicsPath();
            //
            switch (eTabAlignment)
            {
                case TabAlignment.Left:
                    if (iRadius1 > 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left + iRadius1, rectangle.Bottom);
                    }
                    else
                    {
                        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    }
                    //
                    if (iRadius1 <= 0)
                    {
                        if (iRadius2 > 0) { path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Bottom - iRadius2); }
                        else { path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Bottom - iRadius1, iRadius1, iRadius1, 90, 90);
                    }
                    //
                    if (iRadius2 <= 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Top, iRadius2, iRadius2, 180, 90);
                        path.AddLine(rectangle.Left + iRadius2, rectangle.Top, rectangle.Right, rectangle.Top);
                    }
                    break;
                case TabAlignment.Right:
                    if (iRadius1 > 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - iRadius1, rectangle.Top);
                    }
                    else
                    {
                        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                    }
                    //
                    if (iRadius1 <= 0)
                    {
                        if (iRadius2 > 0) { path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - iRadius2); }
                        else { path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRadius1, rectangle.Top, iRadius1, iRadius1, 270, 90);
                    }
                    //
                    if (iRadius2 <= 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRadius2, rectangle.Bottom - iRadius2, iRadius2, iRadius2, 0, 90);
                        path.AddLine(rectangle.Right - iRadius2, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    }
                    break;
                case TabAlignment.Top:
                    if (iRadius1 > 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top + iRadius1);
                    }
                    else
                    {
                        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                    }
                    //
                    if (iRadius1 <= 0)
                    {
                        if (iRadius2 > 0) { path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - iRadius2, rectangle.Top); }
                        else { path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Top, iRadius1, iRadius1, 180, 90);
                    }
                    //
                    if (iRadius2 <= 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRadius2, rectangle.Top, iRadius2, iRadius2, 270, 90);
                        path.AddLine(rectangle.Right, rectangle.Top + iRadius2, rectangle.Right, rectangle.Bottom);
                    }
                    break;
                case TabAlignment.Bottom:
                default:
                    if (iRadius1 > 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - iRadius1);
                    }
                    else
                    {
                        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    }
                    //
                    if (iRadius1 <= 0)
                    {
                        if (iRadius2 > 0) { path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left - iRadius2, rectangle.Bottom); }
                        else { path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRadius1, rectangle.Bottom - iRadius1, iRadius1, iRadius1, 0, 90);
                    }
                    //
                    if (iRadius2 <= 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Bottom - iRadius2, iRadius2, iRadius2, 90, 90);
                        path.AddLine(rectangle.Left, rectangle.Bottom - iRadius2, rectangle.Left, rectangle.Top);
                    }
                    break;
            }
            //
            return path;
        }

        /// <summary>
        /// 创建TabButton外轮廓
        /// </summary>
        /// <param name="eTabAlignment"></param>
        /// <param name="rectangle"></param>
        /// <param name="iLeftTopRadius"></param>
        /// <param name="iRightTopRadius"></param>
        /// <param name="iLeftBottomRadius"></param>
        /// <param name="iRightBottomRadius"></param>
        /// <returns></returns>
        public static GraphicsPath CreateTabButtonContour(TabAlignment eTabAlignment, Rectangle rectangle, int iLeftTopRadius, int iRightTopRadius, int iLeftBottomRadius, int iRightBottomRadius)
        {
            GraphicsPath path = new GraphicsPath();
            //
            switch (eTabAlignment)
            {
                case TabAlignment.Left:
                    if (iLeftBottomRadius > 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left + iLeftBottomRadius, rectangle.Bottom);
                    }
                    else
                    {
                        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    }
                    //
                    if (iLeftBottomRadius <= 0)
                    {
                        if (iLeftTopRadius > 0) { path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Bottom - iLeftTopRadius); }
                        else { path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Bottom - iLeftBottomRadius, iLeftBottomRadius, iLeftBottomRadius, 90, 90);
                    }
                    //
                    if (iLeftTopRadius <= 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Top, iLeftTopRadius, iLeftTopRadius, 180, 90);
                        path.AddLine(rectangle.Left + iLeftTopRadius, rectangle.Top, rectangle.Right, rectangle.Top);
                    }
                    break;
                case TabAlignment.Right:
                    if (iRightTopRadius > 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - iRightTopRadius, rectangle.Top);
                    }
                    else
                    {
                        path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
                    }
                    //
                    if (iRightTopRadius <= 0)
                    {
                        if (iRightBottomRadius > 0) { path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - iRightBottomRadius); }
                        else { path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRightTopRadius, rectangle.Top, iRightTopRadius, iRightTopRadius, 270, 90);
                    }
                    //
                    if (iRightBottomRadius <= 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRightBottomRadius, rectangle.Bottom - iRightBottomRadius, iRightBottomRadius, iRightBottomRadius, 0, 90);
                        path.AddLine(rectangle.Right - iRightBottomRadius, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
                    }
                    break;
                case TabAlignment.Top:
                    if (iLeftTopRadius > 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top + iLeftTopRadius);
                    }
                    else
                    {
                        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                    }
                    //
                    if (iLeftTopRadius <= 0)
                    {
                        if (iRightTopRadius > 0) { path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right - iRightTopRadius, rectangle.Top); }
                        else { path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Top, iLeftTopRadius, iLeftTopRadius, 180, 90);
                    }
                    //
                    if (iRightTopRadius <= 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iRightTopRadius, rectangle.Top, iRightTopRadius, iRightTopRadius, 270, 90);
                        path.AddLine(rectangle.Right, rectangle.Top + iRightTopRadius, rectangle.Right, rectangle.Bottom);
                    }
                    break;
                case TabAlignment.Bottom:
                default:
                    if (iLeftBottomRadius > 0)
                    {
                        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom - iLeftBottomRadius);
                    }
                    else
                    {
                        path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom);
                    }
                    //
                    if (iLeftBottomRadius <= 0)
                    {
                        if (iRightBottomRadius > 0) { path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left - iRightBottomRadius, rectangle.Bottom); }
                        else { path.AddLine(rectangle.Right, rectangle.Bottom, rectangle.Left, rectangle.Bottom); }
                    }
                    else
                    {
                        path.AddArc(rectangle.Right - iLeftBottomRadius, rectangle.Bottom - iLeftBottomRadius, iLeftBottomRadius, iLeftBottomRadius, 0, 90);
                    }
                    //
                    if (iRightBottomRadius <= 0)
                    {
                        path.AddLine(rectangle.Left, rectangle.Bottom, rectangle.Left, rectangle.Top);
                    }
                    else
                    {
                        path.AddArc(rectangle.Left, rectangle.Bottom - iRightBottomRadius, iRightBottomRadius, iRightBottomRadius, 90, 90);
                        path.AddLine(rectangle.Left, rectangle.Bottom - iRightBottomRadius, rectangle.Left, rectangle.Top);
                    }
                    break;
            }
            //
            return path;
        }

        /// <summary>
        /// 创建灰度图片
        /// </summary>
        /// <param name="normalImage"></param>
        /// <returns></returns>
        public static Image CreateDisabledImage(Image normalImage)
        {
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.ClearColorKey();
            imageAttributes.SetColorMatrix(DisabledImageColorMatrix);
            Size size = normalImage.Size;
            Bitmap image = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.DrawImage(normalImage, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, imageAttributes);
            graphics.Dispose();
            return image;
        }
        private static ColorMatrix DisabledImageColorMatrix
        {
            get
            {
                float[][] numArray = new float[5][];
                numArray[0] = new float[] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f };
                numArray[1] = new float[] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f };
                numArray[2] = new float[] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f };
                float[] numArray3 = new float[5];
                numArray3[3] = 1f;
                numArray[3] = numArray3;
                numArray[4] = new float[] { 0.38f, 0.38f, 0.38f, 0f, 1f };
                float[][] numArray2 = new float[5][];
                float[] numArray4 = new float[5];
                numArray4[0] = 1f;
                numArray2[0] = numArray4;
                float[] numArray5 = new float[5];
                numArray5[1] = 1f;
                numArray2[1] = numArray5;
                float[] numArray6 = new float[5];
                numArray6[2] = 1f;
                numArray2[2] = numArray6;
                float[] numArray7 = new float[5];
                numArray7[3] = 0.7f;
                numArray2[3] = numArray7;
                numArray2[4] = new float[5];
                return MultiplyColorMatrix(numArray2, numArray);
            }
        }
        private static ColorMatrix MultiplyColorMatrix(float[][] matrix1, float[][] matrix2)
        {
            int num = 5;
            float[][] newColorMatrix = new float[num][];
            for (int i = 0; i < num; i++)
            {
                newColorMatrix[i] = new float[num];
            }
            float[] numArray2 = new float[num];
            for (int j = 0; j < num; j++)
            {
                for (int k = 0; k < num; k++)
                {
                    numArray2[k] = matrix1[k][j];
                }
                for (int m = 0; m < num; m++)
                {
                    float[] numArray3 = matrix2[m];
                    float num6 = 0f;
                    for (int n = 0; n < num; n++)
                    {
                        num6 += numArray3[n] * numArray2[n];
                    }
                    newColorMatrix[m][j] = num6;
                }
            }
            return new ColorMatrix(newColorMatrix);
        }

    }

    /// <summary>
    /// 比较函数
    /// </summary>
    public static class UtilCompare
    {
        /// <summary>
        /// 整型数值字符串比较（字符只能为 0 - 9，可以加正负号“+”“-”前缀）
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static int CompareNum(string strA, string strB)
        {
            if (strA.Length <= 0 && strB.Length <= 0)
            {
                return 0;
            }
            else if (strA.Length > 0 && strB.Length <= 0) 
            {
                return 1;
            }
            else if (strA.Length <= 0 && strB.Length > 0)
            {
                return -1;
            }
            else
            {
                if (strA[0] == '-' && strB[0] == '-')
                {
                    #region 都是负数
                    if (strA.Length > strB.Length)
                    {
                        return -1;
                    }
                    else if (strA.Length == strB.Length)
                    {
                        for (int i = 0; i < strA.Length; i++)
                        {
                            if (strA[i] > strB[i]) return -1;
                            else if (strA[i] < strB[i]) return 1;
                        }
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                    #endregion
                }
                else if (strA[0] == '+' && strB[0] == '+' || 
                    (strA[0] != '-' && strA[0] != '+') && (strB[0] != '-' && strB[0] != '+'))
                {
                    #region 都是正数
                    if (strA.Length > strB.Length)
                    {
                        return 1;
                    }
                    else if (strA.Length == strB.Length)
                    {
                        for (int i = 0; i < strA.Length; i++)
                        {
                            if (strA[i] > strB[i]) return 1;
                            else if (strA[i] < strB[i]) return -1;
                        }
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (strA[0] == '+' && (strB[0] != '-' && strB[0] != '+'))
                {
                    strA = strA.Remove(0, 1);
                    #region 都是正数
                    if (strA.Length > strB.Length)
                    {
                        return 1;
                    }
                    else if (strA.Length == strB.Length)
                    {
                        for (int i = 0; i < strA.Length; i++)
                        {
                            if (strA[i] > strB[i]) return 1;
                            else if (strA[i] < strB[i]) return -1;
                        }
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if ((strA[0] != '-' && strA[0] != '+') && strB[0] == '+')
                {
                    strB = strB.Remove(0, 1);
                    #region 都是正数
                    if (strA.Length > strB.Length)
                    {
                        return 1;
                    }
                    else if (strA.Length == strB.Length)
                    {
                        for (int i = 0; i < strA.Length; i++)
                        {
                            if (strA[i] > strB[i]) return 1;
                            else if (strA[i] < strB[i]) return -1;
                        }
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
                else if (strA[0] == '-' && strB[0] != '-')
                {
                    #region strA是负数，strB是正数
                    return -1;
                    #endregion
                }
                else if (strA[0] != '-' && strB[0] == '-')
                {
                    #region strA是正数，strB是负数
                    return 1;
                    #endregion
                }
                else
                {
                    #region 都是正数
                    if (strA.Length > strB.Length)
                    {
                        return 1;
                    }
                    else if (strA.Length == strB.Length)
                    {
                        for (int i = 0; i < strA.Length; i++)
                        {
                            if (strA[i] > strB[i]) return 1;
                            else if (strA[i] < strB[i]) return -1;
                        }
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 浮点型数值字符串比较（字符只能为 0 - 9，可以加正负号“+”“-”前缀）
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static int CompareNumEx(string strA, string strB)
        {
            if (strA.Length <= 0 && strB.Length <= 0)
            {
                return 0;
            }
            else if (strA.Length > 0 && strB.Length <= 0)
            {
                return 1;
            }
            else if (strA.Length <= 0 && strB.Length > 0)
            {
                return -1;
            }
            else 
            {
                if (strA.Contains(".") && strB.Contains("."))
                {
                    #region strA是小数，strB是整数
                    if (strA[0] == '-' && strB[0] == '-')
                    {
                        string[] strAList = strA.Split('.');
                        string[] strBList = strB.Split('.');
                        #region 都是负数
                        if (strAList[0].Length > strBList[0].Length)
                        {
                            return -1;
                        }
                        else if (strAList[0].Length == strBList[0].Length)
                        {
                            for (int i = 0; i < strAList[0].Length; i++)
                            {
                                if (strAList[0][i] > strBList[0][i]) return -1;
                                else if (strAList[0][i] < strBList[0][i]) return 1;
                            }
                            //比较小数部分
                            if (strAList[1].Length < strBList[1].Length)
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 1;
                            }
                            else if (strAList[1].Length > strBList[1].Length)
                            {
                                for (int i = 0; i < strBList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return -1;
                            }
                            else
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 0;
                            }
                        }
                        else
                        {
                            return 1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && strB[0] == '+' ||
                    (strA[0] != '-' && strA[0] != '+') && (strB[0] != '-' && strB[0] != '+'))
                    {
                        string[] strAList = strA.Split('.');
                        string[] strBList = strB.Split('.');
                        #region 都是正数
                        if (strAList[0].Length > strBList[0].Length)
                        {
                            return 1;
                        }
                        else if (strAList[0].Length == strBList[0].Length)
                        {
                            for (int i = 0; i < strAList[0].Length; i++)
                            {
                                if (strAList[0][i] > strBList[0][i]) return 1;
                                else if (strAList[0][i] < strBList[0][i]) return -1;
                            }
                            //比较小数部分
                            if (strAList[1].Length < strBList[1].Length)
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return -1;
                            }
                            else if (strAList[1].Length > strBList[1].Length)
                            {
                                for (int i = 0; i < strBList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 1;
                            }
                            else
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 0;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && (strB[0] != '-' && strB[0] != '+'))
                    {
                        strA = strA.Remove(0, 1);
                        string[] strAList = strA.Split('.');
                        string[] strBList = strB.Split('.');
                        #region 都是正数
                        if (strAList[0].Length > strBList[0].Length)
                        {
                            return 1;
                        }
                        else if (strAList[0].Length == strBList[0].Length)
                        {
                            for (int i = 0; i < strAList[0].Length; i++)
                            {
                                if (strAList[0][i] > strBList[0][i]) return 1;
                                else if (strAList[0][i] < strBList[0][i]) return -1;
                            }
                            //比较小数部分
                            if (strAList[1].Length < strBList[1].Length)
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return -1;
                            }
                            else if (strAList[1].Length > strBList[1].Length)
                            {
                                for (int i = 0; i < strBList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 1;
                            }
                            else
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 0;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if ((strA[0] != '-' && strA[0] != '+') && strB[0] == '+')
                    {
                        strB = strB.Remove(0, 1);
                        string[] strAList = strA.Split('.');
                        string[] strBList = strB.Split('.');
                        #region 都是正数
                        if (strAList[0].Length > strBList[0].Length)
                        {
                            return 1;
                        }
                        else if (strAList[0].Length == strBList[0].Length)
                        {
                            for (int i = 0; i < strAList[0].Length; i++)
                            {
                                if (strAList[0][i] > strBList[0][i]) return 1;
                                else if (strAList[0][i] < strBList[0][i]) return -1;
                            }
                            //比较小数部分
                            if (strAList[1].Length < strBList[1].Length)
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return -1;
                            }
                            else if (strAList[1].Length > strBList[1].Length)
                            {
                                for (int i = 0; i < strBList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 1;
                            }
                            else
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 0;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '-' && strB[0] != '-')
                    {
                        #region strA是负数，strB是正数
                        return -1;
                        #endregion
                    }
                    else if (strA[0] != '-' && strB[0] == '-')
                    {
                        #region strA是正数，strB是负数
                        return 1;
                        #endregion
                    }
                    else
                    {
                        string[] strAList = strA.Split('.');
                        string[] strBList = strB.Split('.');
                        #region 都是正数
                        if (strAList[0].Length > strBList[0].Length)
                        {
                            return 1;
                        }
                        else if (strAList[0].Length == strBList[0].Length)
                        {
                            for (int i = 0; i < strAList[0].Length; i++)
                            {
                                if (strAList[0][i] > strBList[0][i]) return 1;
                                else if (strAList[0][i] < strBList[0][i]) return -1;
                            }
                            //比较小数部分
                            if (strAList[1].Length < strBList[1].Length)
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return -1;
                            }
                            else if (strAList[1].Length > strBList[1].Length)
                            {
                                for (int i = 0; i < strBList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 1;
                            }
                            else
                            {
                                for (int i = 0; i < strAList[1].Length; i++)
                                {
                                    if (strAList[1][i] > strBList[1][i]) return -1;
                                    else if (strAList[1][i] < strBList[1][i]) return 1;
                                }
                                return 0;
                            }
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (strA.Contains(".") && !strB.Contains("."))
                {
                    #region strA是小数，strB是整数
                    if (strA[0] == '-' && strB[0] == '-')
                    {
                        strA = strA.Split('.')[0];
                        #region 都是负数
                        if (strA.Length > strB.Length)
                        {
                            return -1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return -1;
                                else if (strA[i] < strB[i]) return 1;
                            }
                            return -1;
                        }
                        else
                        {
                            return 1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && strB[0] == '+' ||
                    (strA[0] != '-' && strA[0] != '+') && (strB[0] != '-' && strB[0] != '+'))
                    {
                        strA = strA.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && (strB[0] != '-' && strB[0] != '+'))
                    {
                        strA = strA.Remove(0, 1);
                        strA = strA.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if ((strA[0] != '-' && strA[0] != '+') && strB[0] == '+')
                    {
                        strB = strB.Remove(0, 1);
                        strA = strA.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '-' && strB[0] != '-')
                    {
                        #region strA是负数，strB是正数
                        return -1;
                        #endregion
                    }
                    else if (strA[0] != '-' && strB[0] == '-')
                    {
                        #region strA是正数，strB是负数
                        return 1;
                        #endregion
                    }
                    else
                    {
                        strB = strB.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (!strA.Contains(".") && strB.Contains("."))
                {
                    #region strA是整数，strB是小数
                    if (strA[0] == '-' && strB[0] == '-')
                    {
                        strB = strB.Split('.')[0];
                        #region 都是负数
                        if (strA.Length > strB.Length)
                        {
                            return -1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return -1;
                                else if (strA[i] < strB[i]) return 1;
                            }
                            return 1;
                        }
                        else
                        {
                            return 1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && strB[0] == '+' ||
                    (strA[0] != '-' && strA[0] != '+') && (strB[0] != '-' && strB[0] != '+'))
                    {
                        strB = strB.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && (strB[0] != '-' && strB[0] != '+'))
                    {
                        strA = strA.Remove(0, 1);
                        strB = strB.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if ((strA[0] != '-' && strA[0] != '+') && strB[0] == '+')
                    {
                        strB = strB.Remove(0, 1);
                        strB = strB.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '-' && strB[0] != '-')
                    {
                        #region strA是负数，strB是正数
                        return -1;
                        #endregion
                    }
                    else if (strA[0] != '-' && strB[0] == '-')
                    {
                        #region strA是正数，strB是负数
                        return 1;
                        #endregion
                    }
                    else
                    {
                        strB = strB.Split('.')[0];
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return -1;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 都是整数
                    if (strA[0] == '-' && strB[0] == '-')
                    {
                        #region 都是负数
                        if (strA.Length > strB.Length)
                        {
                            return -1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return -1;
                                else if (strA[i] < strB[i]) return 1;
                            }
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && strB[0] == '+' ||
                     (strA[0] != '-' && strA[0] != '+') && (strB[0] != '-' && strB[0] != '+'))
                    {
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '+' && (strB[0] != '-' && strB[0] != '+'))
                    {
                        strA = strA.Remove(0, 1);
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if ((strA[0] != '-' && strA[0] != '+') && strB[0] == '+')
                    {
                        strB = strB.Remove(0, 1);
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    else if (strA[0] == '-' && strB[0] != '-')
                    {
                        #region strA是负数，strB是正数
                        return -1;
                        #endregion
                    }
                    else if (strA[0] != '-' && strB[0] == '-')
                    {
                        #region strA是正数，strB是负数
                        return 1;
                        #endregion
                    }
                    else
                    {
                        #region 都是正数
                        if (strA.Length > strB.Length)
                        {
                            return 1;
                        }
                        else if (strA.Length == strB.Length)
                        {
                            for (int i = 0; i < strA.Length; i++)
                            {
                                if (strA[i] > strB[i]) return 1;
                                else if (strA[i] < strB[i]) return -1;
                            }
                            return 0;
                        }
                        else
                        {
                            return -1;
                        }
                        #endregion
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// 纯字符串比较
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <returns></returns>
        public static int CompareStr(string strA, string strB)
        {
            int iCount = strA.Length < strB.Length ? strA.Length : strB.Length;
            for (int i = 0; i < iCount; i++)
            {
                if (strA[i] > strB[i]) return 1;
                else if (strA[i] < strB[i]) return -1;
            }
            return strA.Length - strB.Length;
        }
    }

    /// <summary>
    /// 获取颜色表
    /// </summary>
    public static class UtilColorMap
    {
        /// <summary>
        /// 获取一个Spring颜色表
        /// </summary>
        /// <param name="iLen">色带长</param>
        /// <param name="iAlphaValue">透明度</param>
        /// <returns>返回颜色表</returns>
        public static int[,] GetSpringColorMap(int iLen, int iAlphaValue)
        {
            int[,] colorMap = new int[iLen, 4];
            float[] fSpring = new float[iLen];
            for (int i = 0; i < iLen; i++)
            {
                fSpring[i] = 1.0f * i / (iLen - 1 == 0 ? 1 : iLen - 1);
                colorMap[i, 0] = iAlphaValue;
                colorMap[i, 1] = 255;
                colorMap[i, 2] = (int)(255 * fSpring[i]);
                colorMap[i, 3] = 255 - colorMap[i, 1];
            }
            return colorMap;
        }
    }
}
