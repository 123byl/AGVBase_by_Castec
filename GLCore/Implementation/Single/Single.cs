using Geometry;
using SharpGL;
using System;

namespace GLCore
{
    /// <summary>
    /// 標示面
    /// </summary>
    [Serializable]
    internal abstract class SingleArea : ISingle<IArea>
    {
        private IArea mData = FactoryMode.Factory.Area();

        public SingleArea(string name, bool canDrag = true)
        {
            Name = name; CanDrag = canDrag;
        }

        public SingleArea(int x0, int y0, int x1, int y1, string name, bool canDrag = true)
        {
            mData = FactoryMode.Factory.Area(x0, x1, y0, y1);
            Name = name; CanDrag = canDrag;
        }

        public SingleArea(IArea line, string name, bool canDrag = true)
        {
            mData = FactoryMode.Factory.Area(line);
            Name = name; CanDrag = canDrag;
        }

        public SingleArea(IPair min, IPair max, string name, bool canDrag = true)
        {
            mData = FactoryMode.Factory.Area(min, max);
            Name = name; CanDrag = canDrag;
        }

        /// <summary>
        /// 當下是否可以拖曳
        /// </summary>
        public bool CanDrag { get; } = true;

        /// <summary>
        /// 座標資料
        /// </summary>
        public IArea Data { get { return mData; } }

        /// <summary>
        /// 繪圖設定
        /// </summary>
        public IGLSetting GLSetting { get; protected set; } = new GLSetting();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 建立拖曳點陣列
        /// </summary>
        public virtual IDragPoint[] CreatDragPoints()
        {
            return new IDragPoint[] {
                new DragPoint(Data.Min, MoveMinXMinY),
                new DragPoint(Data.Max,MoveMaxXMaxY),
                new DragPoint(Data.MinXMaxY(),MoveMinXMaxY),
                new DragPoint(Data.MaxXMinY(),MoveMaxXMinY),
                new DragPoint(Data.Center(), Data.Size(), MoveCenter),
            };
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public virtual void Draw(OpenGL gl)
        {
            float z = (int)GLSetting.Type + 0.1f;
            gl.Color(GLSetting.MainColor.GetFloats());
            gl.Begin(OpenGL.GL_QUADS);
            {
                gl.Vertex(Data.Min.X, Data.Min.Y, z);
                gl.Vertex(Data.Max.X, Data.Min.Y, z);
                gl.Vertex(Data.Max.X, Data.Max.Y, z);
                gl.Vertex(Data.Min.X, Data.Max.Y, z);
            }
            gl.End();
        }

        /// <summary>
        /// 回傳名稱及座標，例如：Single,-100,100,0,200
        /// </summary>
        public override string ToString()
        {
            return Name + "," + Data.ToString();
        }

        /// <summary>
        /// 中心座標改變時所對應的事件
        /// </summary>
        private void MoveCenter(IPair newCenter)
        {
            IPair move = newCenter.Subtraction(Data.Center());
            mData.Set(mData.Min.Add(move), mData.Max.Add(move));
        }

        /// <summary>
        /// MaxX MinY 改變時所對應的事件
        /// </summary>
        private void MoveMaxXMaxY(IPair p)
        {
            mData.Set(mData.Min.X, mData.Min.Y, p.X, p.Y);
        }

        /// <summary>
        /// MaxX MinY 改變時所對應的事件
        /// </summary>
        private void MoveMaxXMinY(IPair p)
        {
            mData.Set(mData.Min.X, p.Y, p.X, mData.Max.Y);
        }

        /// <summary>
        /// MinX MaxY 改變時所對應的事件
        /// </summary>
        private void MoveMinXMaxY(IPair p)
        {
            mData.Set(p.X, mData.Min.Y, mData.Max.X, p.Y);
        }

        /// <summary>
        /// MinX MinY 改變時所對應的事件
        /// </summary>
        private void MoveMinXMinY(IPair p)
        {
            mData.Set(p.X, p.Y, mData.Max.X, mData.Max.Y);
        }
    }

    /// <summary>
    /// 標示線
    /// </summary>
    [Serializable]
    internal abstract class SingleLine : ISingle<ILine>
    {
        public SingleLine(string name, bool canDrag = true)
        {
            Name = name; CanDrag = canDrag;
        }

        public SingleLine(int x0, int y0, int x1, int y1, string name, bool canDrag = true)
        {
            Data = FactoryMode.Factory.Line(x0, x1, y0, y1);
            Name = name; CanDrag = canDrag;
        }

        public SingleLine(ILine line, string name, bool canDrag = true)
        {
            Data = FactoryMode.Factory.Line(line);
            Name = name; CanDrag = canDrag;
        }

        public SingleLine(IPair beg, IPair end, string name, bool canDrag = true)
        {
            Data = FactoryMode.Factory.Line(beg, end);
            Name = name; CanDrag = canDrag;
        }

        /// <summary>
        /// 當下是否可以拖曳
        /// </summary>
        public bool CanDrag { get; } = true;

        /// <summary>
        /// 座標資料
        /// </summary>
        public ILine Data { get; } = FactoryMode.Factory.Line();

        /// <summary>
        /// 繪圖設定
        /// </summary>
        public IGLSetting GLSetting { get; protected set; } = new GLSetting();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 建立拖曳點陣列
        /// </summary>
        public virtual IDragPoint[] CreatDragPoints()
        {
            return new IDragPoint[] {
                new DragPoint(Data.Begin, MoveBegin),
                new DragPoint(Data.End, MoveEnd),
                new DragPoint(Data.Center(), MoveCenter),
            };
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public virtual void Draw(OpenGL gl)
        {
            float z = (int)GLSetting.Type + 0.1f;
            gl.Color(GLSetting.MainColor.GetFloats());
            gl.LineWidth(GLSetting.LineWidth);
            gl.Begin(OpenGL.GL_LINES);
            {
                gl.Vertex(Data.Begin.X, Data.Begin.Y, z);
                gl.Vertex(Data.End.X, Data.End.Y, z);
            }
            gl.End();
        }

        /// <summary>
        /// 回傳名稱及座標，例如：Single,-100,100,0,200
        /// </summary>
        public override string ToString()
        {
            return Name + "," + Data.ToString();
        }

        /// <summary>
        /// 起點座標改變時所對應的事件
        /// </summary>
        private void MoveBegin(IPair beg)
        {
            Data.Begin = beg;
        }

        /// <summary>
        /// 中心座標改變時所對應的事件
        /// </summary>
        private void MoveCenter(IPair newCenter)
        {
            IPair move = newCenter.Subtraction(Data.Center());
            Data.Begin.X += move.X;
            Data.Begin.Y += move.Y;
            Data.End.X += move.X;
            Data.End.Y += move.Y;
        }

        /// <summary>
        /// 終點座標改變時所對應的事件
        /// </summary>
        private void MoveEnd(IPair end)
        {
            Data.End = end;
        }
    }

    /// <summary>
    /// 標示點
    /// </summary>
    [Serializable]
    internal abstract class SingleTowardPair : ISingle<ITowardPair>
    {
        public SingleTowardPair(string name, bool canDrag = true)
        {
            Name = name; CanDrag = canDrag;
        }

        public SingleTowardPair(int x, int y, double toward, string name, bool canDrag = true)
        {
            Data = FactoryMode.Factory.TowardPair(x, y, toward);
            Name = name; CanDrag = canDrag;
        }

        public SingleTowardPair(int x, int y, IAngle toward, string name, bool canDrag = true)
        {
            Data = FactoryMode.Factory.TowardPair(x, y, toward);
            Name = name; CanDrag = canDrag;
        }

        public SingleTowardPair(ITowardPair towardPair, string name, bool canDrag = true)
        {
            Data = FactoryMode.Factory.TowardPair(towardPair);
            Name = name; CanDrag = canDrag;
        }

        /// <summary>
        /// 當下是否可以拖曳
        /// </summary>
        public bool CanDrag { get; } = true;

        /// <summary>
        /// 座標資料
        /// </summary>
        public ITowardPair Data { get; } = FactoryMode.Factory.TowardPair();

        /// <summary>
        /// 繪圖設定
        /// </summary>
        public IGLSetting GLSetting { get; protected set; } = new GLSetting();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 建立拖曳點陣列
        /// </summary>
        public virtual IDragPoint[] CreatDragPoints()
        {
            return new IDragPoint[] {
                new DragPoint(Data.Position.Shift(Data.Toward,GLSetting.TowardLength), MoveToward),
                new DragPoint(Data.Position, GLSetting.Size, MoveCenter),
            };
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public virtual void Draw(OpenGL gl)
        {
            gl.PushMatrix();
            {
                gl.Translate(Data.Position.X, Data.Position.Y, 0);
                gl.TextureBmp(GLSetting.BmpName, GLSetting.Size, GLSetting.MainColor, GLSetting.Type);
                float z = (int)GLSetting.Type + 0.1f;
                gl.Color(GLSetting.SubColor.GetFloats());
                gl.LineWidth(GLSetting.LineWidth);
                gl.Begin(OpenGL.GL_LINES);
                {
                    IPair endPos = FactoryMode.Factory.Pair().Shift(Data.Toward, GLSetting.TowardLength);
                    gl.Vertex(0, 0, z);
                    gl.Vertex(endPos.X, endPos.Y, z);
                }
                gl.End();
            }
            gl.PopMatrix();
        }

        /// <summary>
        /// 回傳名稱及座標，例如：Single,-100,100,90.00
        /// </summary>
        public override string ToString()
        {
            return Name + "," + Data.ToString();
        }

        /// <summary>
        /// 中心座標改變時所對應的事件
        /// </summary>
        private void MoveCenter(IPair newCenter)
        {
            Data.Position = newCenter;
        }

        /// <summary>
        /// 方向角改變時所對應的事件
        /// </summary>
        private void MoveToward(IPair newToward)
        {
            IPair direction = newToward.Subtraction(Data.Position);
            Data.Toward.Theta = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI;
        }
    }
}