using AGVDefine;
using Geometry;
using GLCore.Properties;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace GLCore
{
    /// <summary>
    /// 提供使用頂點數據組繪製 2D 物件的操作方法
    /// </summary>
    public static class GLDrawArrays
    {
        /// <summary>
        /// <para>提供使用頂點數據組繪製 2D 物件的操作方法</para>
        /// <para><paramref name="mode"/> 表示繪圖模式，<paramref name="count"/> 為資料組數量</para>
        /// <para>繪圖模式參考 OpenGL 原生的 DrawArray() 函式</para>
        /// </summary>
        public static void DrawArrays(this OpenGL gl, uint mode, int count, int[] array)
        {
            if (count <= 0 || array == null) return;
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.VertexPointer(2, 0, array);
            gl.DrawArrays(mode, 0, count);
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
        }
    }

    /// <summary>
    /// 圖形相交
    /// </summary>
    public static class GLInterference
    {
        /// <summary>
        /// 是否相交
        /// </summary>
        public static bool Interference(this ISingle<ITowardPair> single, IPair pos)
        {
            IPair distance = single.Data.Position.Subtraction(pos).Abs();
            return distance.X <= single.GLSetting.Size.X / 2 && distance.Y <= single.GLSetting.Size.Y / 2;
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        public static bool Interference(this ISingle<IArea> single, IPair pos)
        {
            return single.Data.Interference(pos);
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        public static bool Interference(this IArea area, IPair pos)
        {
            if (pos.X > area.Max.X) return false;
            if (pos.Y > area.Max.Y) return false;
            if (pos.X < area.Min.X) return false;
            if (pos.Y < area.Min.Y) return false;
            return true;
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        public static bool Interference(this ISingle<ILine> single, IPair pos)
        {
            return single.Data.Interference(pos);
        }

        /// <summary>
        /// 是否相交
        /// </summary>
        public static bool Interference(this ILine line, IPair pos)
        {
            double length = line.End.LengthTo(line.Begin);
            double length0 = pos.LengthTo(line.Begin);
            double length1 = pos.LengthTo(line.End);
            if (length0 <= 100 || length1 <= 100 || length0 + length1 < 1.01 * length) return true;
            return false;
        }

        /// <summary>
        /// 路徑是否與 (textX,textY) 相交
        /// </summary>
        public static bool IntersectPath(this IPath path, int testX, int testY, double safetyArea)
        {
            IPair test = FactoryMode.Factory.Pair(testX, testY);
            bool intersect = false;
            path.DataList.SafetyForLoop((item) =>
            {
                if (!intersect)
                {
                    intersect = item.LengthTo(test) < safetyArea;
                }
            });
            return intersect;
        }
    }

    /// <summary>
    /// 提供 GL 設定虛線模式的操作方法
    /// </summary>
    public static class GLStippleLine
    {
        /// <summary>
        /// 開啟虛線
        /// </summary>
        public static void BeginStippleLine(this OpenGL gl, ELineStyle style, int factor = 5)
        {
            if (style == ELineStyle._1111111111111111) return;
            ushort pattern = Convert.ToUInt16(style.ToString().Substring(1), 2);
            gl.Enable(OpenGL.GL_LINE_STIPPLE);
            gl.LineStipple(factor, pattern);
        }

        /// <summary>
        /// 關閉虛線
        /// </summary>
        public static void EndStippleLine(this OpenGL gl)
        {
            gl.Disable(OpenGL.GL_LINE_STIPPLE);
        }
    }

    /// <summary>
    /// 提供 GL 文字顯示的操作方法
    /// </summary>
    public static class GLText2D
    {
        /// <summary>
        /// 顯示文字字體
        /// </summary>
        public static readonly System.Drawing.Font TextFont = new System.Drawing.Font("Arial", 24);

        /// <summary>
        /// 執行緒鎖
        /// </summary>
        private static readonly object mKey = new object();

        /// <summary>
        /// 顯示文字
        /// </summary>
        private static List<DisplayText> DisplayTextList = new List<DisplayText>();

        /// <summary>
        /// 座標轉換，實際座標轉 2D 文字座標
        /// </summary>
        public delegate IPair DelGL2Text2D(IPair gl);

        /// <summary>
        /// 顯示文字顏色
        /// </summary>
        public static IColor TextColor { get; } = new Color(System.Drawing.Color.MediumBlue);

        /// <summary>
        /// 清除所有的 Text 資訊
        /// </summary>
        public static void ClearText(this OpenGL gl)
        {
            lock (mKey)
            {
                DisplayTextList.Clear();
                gl.DrawText(0, 0, TextColor.R / 255.0f, TextColor.G / 255.0f, TextColor.B / 255.0f, TextFont.Name, TextFont.Size, "");
            }
        }

        /// <summary>
        /// 印出所有的 Text 資訊
        /// </summary>
        public static void DrawTextList(this OpenGL gl, DelGL2Text2D convert)
        {
            lock (mKey)
            {
                foreach (var item in DisplayTextList)
                {
                    if (item.Text == null) continue;
                    IPair screen = convert(item.Position);
                    gl.DrawText(screen.X, screen.Y, TextColor.R / 255.0f, TextColor.G / 255.0f, TextColor.B / 255.0f, TextFont.Name, TextFont.Size, item.Text);
                }
                DisplayTextList.Clear();
            }
        }

        /// <summary>
        /// 繪製顯示文字
        /// </summary>
        public static void PushText(this OpenGL gl, int x, int y, string format, params object[] arg)
        {
            gl.PushText(x, y, string.Format(format, arg));
        }

        /// <summary>
        /// 繪製顯示文字
        /// </summary>
        public static void PushText(this OpenGL gl, int x, int y, string str)
        {
            lock (mKey)
                DisplayTextList.Add(new DisplayText(x, y, str));
        }

        /// <summary>
        /// 繪製顯示文字
        /// </summary>
        public static void PushText(this OpenGL gl, IPair position, string str)
        {
            gl.PushText(position.X, position.Y, str);
        }

        /// <summary>
        /// 繪製顯示文字
        /// </summary>
        public static void PushText(this OpenGL gl, IPair position, string format, params object[] arg)
        {
            gl.PushText(position.X, position.Y, string.Format(format, arg));
        }

        /// <summary>
        /// 顯示文字結構
        /// </summary>
        private struct DisplayText
        {
            public DisplayText(int x, int y, string text)
            {
                Position = FactoryMode.Factory.Pair(x, y);
                Text = text;
            }

            public IPair Position { get; }
            public string Text { get; set; }
        }
    }

    /// <summary>
    /// GL 貼圖
    /// </summary>
    public static class GLTexture
    {
        /// <summary>
        /// 執行緒鎖
        /// </summary>
        private static readonly object mKey = new object();

        /// <summary>
        /// 地圖轉 byte 對應資料
        /// </summary>
        private static Dictionary<string, uint> mData = new Dictionary<string, uint>();

        /// <summary>
        /// 使用貼圖繪製矩形，如果貼圖不存在，則用 color 著色
        /// </summary>
        public static void TextureBmp(this OpenGL gl, string name, IPair size, IColor color, EType type)
        {
            lock (mKey)
            {
                if (!mData.ContainsKey(name)) ConvertBmpToBytes(gl, name);
                if (mData.ContainsKey(name))
                {
                    float z = (int)type;
                    gl.Color(1.0, 1.0, 1.0, 1.0);
                    gl.BindTexture(OpenGL.GL_TEXTURE_2D, mData[name]);
                    gl.Enable(OpenGL.GL_TEXTURE_2D);
                    {
                        gl.Begin(OpenGL.GL_QUADS);
                        {
                            gl.TexCoord(0.0, 0.0);
                            gl.Vertex(-size.X / 2, -size.Y / 2, z);
                            gl.TexCoord(1.0, 0.0);
                            gl.Vertex(+size.X / 2, -size.Y / 2, z);
                            gl.TexCoord(1.0, 1.0);
                            gl.Vertex(+size.X / 2, +size.Y / 2, z);
                            gl.TexCoord(0.0, 1.0);
                            gl.Vertex(-size.X / 2, +size.Y / 2, z);
                        }
                        gl.End();
                    }
                    gl.Disable(OpenGL.GL_TEXTURE_2D);
                }
                else
                {
                    float z = (int)type;
                    gl.Color(color.GetFloats());
                    gl.Begin(OpenGL.GL_QUADS);
                    {
                        gl.Vertex(-size.X / 2, -size.Y / 2, z);
                        gl.Vertex(+size.X / 2, -size.Y / 2, z);
                        gl.Vertex(+size.X / 2, +size.Y / 2, z);
                        gl.Vertex(-size.X / 2, +size.Y / 2, z);
                    }
                    gl.End();
                }
            }
        }

        private static void ConvertBmpToBytes(OpenGL gl, string name)
        {
            lock (mKey)
            {
                Bitmap image = (Bitmap)Resources.ResourceManager.GetObject(name);
                if (image != null)
                {
                    image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    BitmapData bitmapdata;
                    Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
                    bitmapdata = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                    byte[] rgba = new byte[image.Width * image.Height * 4];
                    unsafe
                    {
                        // p 的順序是 BGR
                        byte* p = (byte*)(void*)bitmapdata.Scan0;
                        for (int h = 0; h < image.Height; ++h)
                        {
                            for (int w = 0; w < image.Width; ++w)
                            {
                                int index = h * image.Width * 4 + w * 4;
                                // B
                                rgba[index + 2] = p[0];
                                p++;
                                // G
                                rgba[index + 1] = p[0];
                                p++;
                                // R
                                rgba[index + 0] = p[0];
                                p++;
                                if (rgba[index + 0] == 255 && rgba[index + 1] == 255 && rgba[index + 2] == 255)
                                {
                                    rgba[index + 3] = 0;
                                }
                                else
                                {
                                    rgba[index + 3] = 255;
                                }
                            }
                        }
                    }
                    int size = Marshal.SizeOf(rgba[0]) * rgba.Length;

                    IntPtr ptr = Marshal.AllocHGlobal(size);
                    Marshal.Copy(rgba, 0, ptr, rgba.Length);
                    uint[] tArray = new uint[1];
                    gl.GenTextures(1, tArray);

                    gl.PixelStore(OpenGL.GL_UNPACK_ALIGNMENT, 1);

                    gl.BindTexture(OpenGL.GL_TEXTURE_2D, tArray[0]);
                    gl.Build2DMipmaps(OpenGL.GL_TEXTURE_2D, 4, image.Width, image.Height, OpenGL.GL_RGBA, OpenGL.GL_UNSIGNED_BYTE, ptr);

                    gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_LINEAR_MIPMAP_NEAREST);
                    gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_LINEAR_MIPMAP_LINEAR);

                    mData.Add(name, tArray[0]);
                }
            }
        }
    }
    
    /// <summary>
    /// <see cref="ISingle{TGeometry}"/>操作方法
    /// </summary>
    public static class Extension {

        /// <summary>
        /// 設定標示物位置
        /// </summary>
        /// <param name="single">標示物</param>
        /// <param name="location">新位置</param>
        public static void SetLocation(this ISingle<ITowardPair> single, ITowardPair location) {
            single.Data.Position.X = location.Position.X;
            single.Data.Position.Y = location.Position.Y;
            single.Data.Toward.Theta = location.Toward.Theta;
        }

    }
}
