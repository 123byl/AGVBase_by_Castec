using AGVDefine;
using Geometry;
using GLCore;
using SharpGL;

namespace GLUI
{
    // 網格繪製、座標軸繪製
    internal partial class Scene
    {
        /// <summary>
        /// 網格陣列
        /// </summary>
        private int[] mGridArray = new int[] { };

        /// <summary>
        /// 坐標軸大小(mm)
        /// </summary>
        public int AxisLength { get; } = 1000;

        /// <summary>
        /// 網格大小(mm)
        /// </summary>
        public int GridSize { get; } = 1000;

        /// <summary>
        /// 繪製坐標軸
        /// </summary>
        private void DrawAxis()
        {
            float z = (int)EType.Axis;
            mGL.LineWidth(5);

            // 畫XY軸 
            mGL.Begin(OpenGL.GL_LINES);
            {
                mGL.Color(AxisXColor.GetFloats());
                mGL.Vertex(0, 0, z);
                mGL.Vertex(AxisLength, 0, z);
                mGL.Color(AxisYColor.GetFloats());
                mGL.Vertex(0, 0, z);
                mGL.Vertex(0, AxisLength, z);
            }
            mGL.End();

            // 使用虛線畫負XY軸 
            mGL.BeginStippleLine(ELineStyle._1111111011111110);
            {
                mGL.Begin(OpenGL.GL_LINES);
                {
                    mGL.Color(AxisXColor.GetFloats());
                    mGL.Vertex(0, 0, z);
                    mGL.Vertex(-AxisLength, 0, z);
                    mGL.Color(AxisYColor.GetFloats());
                    mGL.Vertex(0, 0, z);
                    mGL.Vertex(0, -AxisLength, z);
                }
                mGL.End();
            }
            mGL.EndStippleLine();
        }

        /// <summary>
        /// 繪製格線
        /// </summary>
        private void DrawGrid()
        {
            if (Zoom > 40) return;

            // 顏色、大小
            mGL.Color(GridColor.GetFloats());
            mGL.LineWidth(1);

            // 座標設置
            mGL.PushMatrix();
            {
                mGL.LoadIdentity();
                mGL.Translate((Translate.X) % GridSize, (Translate.Y) % GridSize, (int)EType.Grid);
                if (mGridArray.Length != 0)
                {
                    mGL.DrawArrays(OpenGL.GL_LINES, mGridArray.Length / 2, mGridArray);
                }
                else
                {
                    GenGridVertex();
                }
            }
            mGL.PopMatrix();
        }

        /// <summary>
        /// 繪製名字
        /// </summary>
        private void DrawNames()
        {
            PushNames(Database.AGVGM);
            PushNames(Database.GoalGM);
            PushNames(Database.PowerGM);
            mGL.DrawTextList(GLToText);
        }

        /// <summary>
        /// 產生網格
        /// </summary>
        private void GenGridVertex()
        {
            int rowCount = 30;
            int columonCount = 60;
            mGridArray = new int[(rowCount * 2 + 1 + columonCount * 2 + 1) * 4];
            int index = 0;
            for (int row = -rowCount; row <= rowCount; ++row)
            {
                mGridArray[index] = -columonCount * GridSize; // begin.X
                index++;
                mGridArray[index] = row * GridSize; // begin.Y
                index++;
                mGridArray[index] = columonCount * GridSize; // end.X
                index++;
                mGridArray[index] = row * GridSize; // end.Y
                index++;
            }
            for (int column = -columonCount; column <= columonCount; ++column)
            {
                mGridArray[index] = column * GridSize; // begin.X
                index++;
                mGridArray[index] = -rowCount * GridSize; // begin.Y
                index++;
                mGridArray[index] = column * GridSize; // end.X
                index++;
                mGridArray[index] = rowCount * GridSize; // end.Y
                index++;
            }
        }

        /// <summary>
        /// GL 座標轉文字座標
        /// </summary>
        private IPair GLToText(IPair gl)
        {
            IPair screen = GLToScreen(gl.X, gl.Y);
            return FactoryMode.Factory.Pair(screen.X, mOpenGLControl.Height - screen.Y);
        }

        #region 顏色

        /// <summary>
        /// X 軸顏色
        /// </summary>
        public IColor AxisXColor { get; } = FactoryMode.Factory.Color(System.Drawing.Color.Red);

        /// <summary>
        /// Y 軸顏色
        /// </summary>
        public IColor AxisYColor { get; } = FactoryMode.Factory.Color(System.Drawing.Color.Green);

        /// <summary>
        /// 背景色
        /// </summary>
        public IColor BackgroundColor { get; } = FactoryMode.Factory.Color(System.Drawing.Color.Wheat);

        /// <summary>
        /// 網格顏色
        /// </summary>
        public IColor GridColor { get; } = FactoryMode.Factory.Color(System.Drawing.Color.Gray);

        /// <summary>
        /// 文字顏色
        /// </summary>
        public IColor TextColor { get; } = FactoryMode.Factory.Color(System.Drawing.Color.Black);

        #endregion 顏色
    }
}
