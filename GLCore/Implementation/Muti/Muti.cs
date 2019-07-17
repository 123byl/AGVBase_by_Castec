using Geometry;
using SharpGL;
using System;
using System.Collections.Generic;

namespace GLCore
{
    /// <summary>
    /// 可繪面集合
    /// </summary>
    internal abstract class MutiArea : IMuti<IArea>
    {
        /// <summary>
        /// 執行緒鎖
        /// </summary>
        private readonly object mKey = new object();

        private int[] mArray = new int[] { };

        /// <summary>
        /// 最後重繪時間
        /// </summary>
        private DateTime mLastRender = default(DateTime);

        /// <summary>
        /// 頂點數
        /// </summary>
        private int mVertexCount = -1;

        public MutiArea()
        {
        }

        public MutiArea(IEnumerable<IArea> collection)
        {
            DataList = new SafetyList<IArea>(collection);
        }

        public MutiArea(IArea item)
        {
            DataList.Add(item);
        }

        /// <summary>
        /// 集合資料
        /// </summary>
        public ISafetyList<IArea> DataList { get; } = new SafetyList<IArea>();

        /// <summary>
        /// 繪圖設定
        /// </summary>
        public IGLSetting GLSetting { get; protected set; } = new GLSetting();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 旋轉
        /// </summary>
        public IAngle Rotate { get; set; } = FactoryMode.Factory.Angle();

        /// <summary>
        /// 偏移
        /// </summary>
        public IPair Shift { get; set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 重新生成頂點陣列(加速顯示)
        /// </summary>
        public void BuildVertexArray()
        {
            lock (mKey)
            {
                mVertexCount = -1;
                mArray = null;
            }
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public void Draw(OpenGL gl)
        {
            float z = (int)GLSetting.Type;
            gl.Color(GLSetting.MainColor.GetFloats());
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            GenVertexArray();
            gl.PushMatrix();
            {
                gl.Translate(Shift.X, Shift.Y, 0);
                gl.Rotate(0, 0, -(float)Rotate.Theta);
                gl.DrawArrays(OpenGL.GL_QUADS, mVertexCount, mArray);
            }
            gl.PopMatrix();
            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        private void GenVertexArray()
        {
            lock (mKey)
            {
                if (mLastRender == DataList.LastEdit && mVertexCount == DataList.Count * 4) return;
                BuildVertexArray();
                mLastRender = DataList.LastEdit;
                mArray = new int[DataList.Count * 8];
                mVertexCount = mArray.Length / 2;
                int index = 0;
                try
                {
                    DataList.SafetyForLoop((item) =>
                    {
                        mArray[index] = item.Min.X;
                        index++;
                        mArray[index] = item.Min.Y;
                        index++;
                        mArray[index] = item.Max.X;
                        index++;
                        mArray[index] = item.Min.Y;
                        index++;
                        mArray[index] = item.Max.X;
                        index++;
                        mArray[index] = item.Max.Y;
                        index++;
                        mArray[index] = item.Min.X;
                        index++;
                        mArray[index] = item.Max.Y;
                        index++;
                    });
                }
                catch (Exception)
                {
                }
            }
        }
    }

    /// <summary>
    /// 可繪線集合
    /// </summary>
    internal abstract class MutiLine : IMuti<ILine>
    {
        /// <summary>
        /// 執行緒鎖
        /// </summary>
        private readonly object mKey = new object();

        private int[] mArray = new int[] { };

        /// <summary>
        /// 最後重繪時間
        /// </summary>
        private DateTime mLastRender = default(DateTime);

        /// <summary>
        /// 頂點數
        /// </summary>
        private int mVertexCount = -1;

        public MutiLine()
        {
        }

        public MutiLine(IEnumerable<ILine> collection)
        {
            DataList = new SafetyList<ILine>(collection);
        }

        public MutiLine(ILine item)
        {
            DataList.Add(item);
        }

        /// <summary>
        /// 集合資料
        /// </summary>
        public ISafetyList<ILine> DataList { get; } = new SafetyList<ILine>();

        /// <summary>
        /// 繪圖設定
        /// </summary>
        public IGLSetting GLSetting { get; protected set; } = new GLSetting();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 旋轉
        /// </summary>
        public IAngle Rotate { get; set; } = FactoryMode.Factory.Angle();

        /// <summary>
        /// 偏移
        /// </summary>
        public IPair Shift { get; set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 重新生成頂點陣列(加速顯示)
        /// </summary>
        public void BuildVertexArray()
        {
            lock (mKey)
            {
                mVertexCount = -1;
                mArray = null;
            }
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public void Draw(OpenGL gl)
        {
            float z = (int)GLSetting.Type;
            gl.LineWidth(GLSetting.LineWidth);
            gl.Color(GLSetting.MainColor.GetFloats());
            gl.BeginStippleLine(GLSetting.LineStyle);
            {
                gl.Disable(OpenGL.GL_DEPTH_TEST);
                GenVertexArray();
                gl.PushMatrix();
                {
                    gl.Translate(Shift.X, Shift.Y, 0);
                    gl.Rotate(0, 0, -(float)Rotate.Theta);
                    gl.DrawArrays(OpenGL.GL_LINES, mVertexCount, mArray);
                }
                gl.PopMatrix();
                gl.Enable(OpenGL.GL_DEPTH_TEST);
            }
            gl.EndStippleLine();
        }

        private void GenVertexArray()
        {
            lock (mKey)
            {
                if (mLastRender == DataList.LastEdit && mVertexCount == DataList.Count * 2) return;
                BuildVertexArray();
                mLastRender = DataList.LastEdit;
                mArray = new int[DataList.Count * 4];
                mVertexCount = mArray.Length / 2;
                int index = 0;
                try
                {
                    DataList.SafetyForLoop((item) =>
                    {
                        mArray[index] = item.Begin.X;
                        index++;
                        mArray[index] = item.Begin.Y;
                        index++;
                        mArray[index] = item.End.X;
                        index++;
                        mArray[index] = item.End.Y;
                        index++;
                    });
                }
                catch (Exception)
                {
                }
            }
        }
    }

    /// <summary>
    /// 可繪點集合
    /// </summary>
    internal abstract class MutiPair : IMuti<IPair>
    {
        /// <summary>
        /// 執行緒鎖
        /// </summary>
        private readonly object mKey = new object();

        private int[] mArray = new int[] { };

        /// <summary>
        /// 最後重繪時間
        /// </summary>
        private DateTime mLastRender = default(DateTime);

        /// <summary>
        /// 頂點數
        /// </summary>
        private int mVertexCount = -1;

        public MutiPair()
        {
        }

        public MutiPair(IEnumerable<IPair> collection)
        {
            mDataList = new SafetyList<IPair>(collection);
        }

        public MutiPair(IPair item)
        {
            DataList.Add(item);
        }

        /// <summary>
        /// 集合資料
        /// </summary>
        public ISafetyList<IPair> DataList { get { lock (mKey) return mDataList; } }

        /// <summary>
        /// 繪圖設定
        /// </summary>
        public IGLSetting GLSetting { get; protected set; } = new GLSetting();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 旋轉
        /// </summary>
        public IAngle Rotate { get; set; } = FactoryMode.Factory.Angle();

        /// <summary>
        /// 偏移
        /// </summary>
        public IPair Shift { get; set; } = FactoryMode.Factory.Pair();

        private ISafetyList<IPair> mDataList { get; } = new SafetyList<IPair>();

        /// <summary>
        /// 重新生成頂點陣列(加速顯示)
        /// </summary>
        public void BuildVertexArray()
        {
            lock (mKey)
            {
                mVertexCount = -1;
                mArray = null;
            }
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public virtual void Draw(OpenGL gl)
        {
            float z = (int)GLSetting.Type;
            gl.Color(GLSetting.MainColor.GetFloats());
            gl.PointSize(GLSetting.PointSize);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            GenVertexArray();
            gl.PushMatrix();
            {
                gl.Translate(Shift.X, Shift.Y, 0);
                gl.Rotate(0, 0, -(float)Rotate.Theta);
                gl.DrawArrays(OpenGL.GL_POINTS, mVertexCount, mArray);
            }
            gl.PopMatrix();
            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public void DrawAsLine(OpenGL gl)
        {
            float z = (int)GLSetting.Type;
            gl.Color(GLSetting.MainColor.GetFloats());
            gl.LineWidth(GLSetting.LineWidth);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            GenVertexArray();
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, mVertexCount, mArray);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        private void GenVertexArray()
        {
            lock (mKey)
            {
                if (mLastRender == DataList.LastEdit && mVertexCount == DataList.Count) return;
                BuildVertexArray();
                mLastRender = DataList.LastEdit;
                mArray = new int[DataList.Count * 2];
                mVertexCount = mArray.Length / 2;
                int index = 0;
                try
                {
                    DataList.SafetyForLoop((item) =>
                    {
                        mArray[index] = item.X;
                        index++;
                        mArray[index] = item.Y;
                        index++;
                    });
                }
                catch (Exception)
                {
                }
            }
        }
    }
}