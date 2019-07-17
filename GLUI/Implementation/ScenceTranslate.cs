using Geometry;
using NetExtension;

namespace GLUI
{
    // 座標轉換
    internal partial class Scene
    {
        /// <summary>
        /// 縮放
        /// </summary>
        private double mZoom = 10.0;

        /// <summary>
        /// 平移
        /// </summary>
        public IPair Translate { get; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 縮放
        /// </summary>
        public double Zoom { get { return mZoom; } set { mZoom = value.Bound(0.1, 1000.0); } }

        /// <summary>
        /// 設定焦點
        /// </summary>
        public void Focus<T>(T focus) where T : IPair
        {
            Focus(focus.X, focus.Y);
        }

        /// <summary>
        /// 設定焦點
        /// </summary>
        public void Focus(int x, int y)
        {
            Translate.X = -x;
            Translate.Y = -y;
        }

        /// <summary>
        /// 實際座標轉螢幕座標
        /// </summary>
        public IPair GLToScreen<T>(T gl) where T : IPair => GLToScreen(gl.X, gl.Y);

        /// <summary>
        /// 實際座標轉螢幕座標
        /// </summary>
        public IPair GLToScreen(int x, int y)
        {
            double mX = (x + Translate.X) / Zoom;
            double mY = (y + Translate.Y) / Zoom;
            return FactoryMode.Factory.Pair(mX + mOpenGLControl.Width / 2, mOpenGLControl.Height / 2 - mY);
        }

        /// <summary>
        /// 螢幕座標轉實際座標
        /// </summary>
        public IPair ScreenToGL<T>(T screen) where T : IPair => ScreenToGL(screen.X, screen.Y);

        /// <summary>
        /// 螢幕座標轉實際座標
        /// </summary>
        public IPair ScreenToGL(int x, int y)
        {
            double mX = x - mOpenGLControl.Width / 2;
            double mY = mOpenGLControl.Height / 2 - y;
            return FactoryMode.Factory.Pair(mX * Zoom - Translate.X, mY * Zoom - Translate.Y);
        }
    }
}
