using AGVDefine;
using Geometry;
using SerialCommunicationData;
using SharpGL;
using System;

namespace GLCore
{
    /// <summary>
    /// 車
    /// </summary>
    [Serializable]
    internal class AGV : SingleTowardPair, IAGV
    {
        public AGV(string name) : base(name)
        {
            GLSetting = new GLSetting(EType.AGV);
            Status.Name = name;
        }

        public AGV(int x, int y, double toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.AGV);
            Status.Name = name;
            Status.Data.Position.X = x;
            Status.Data.Position.Y = y;
            Status.Data.Toward.Theta = toward;
        }

        public AGV(int x, int y, IAngle toward, string name) : base(x, y, toward, name)
        {
            GLSetting = new GLSetting(EType.AGV);
            Status.Name = name;
            Status.Data.Position.X = x;
            Status.Data.Position.Y = y;
            Status.Data.Toward.Theta = toward.Theta;
        }

        public AGV(ITowardPair towardPair, string name) : base(towardPair, name)
        {
            GLSetting = new GLSetting(EType.AGV);
            Status.Name = name;
            Status.Data.Position.X = towardPair.Position.X;
            Status.Data.Position.Y = towardPair.Position.Y;
            Status.Data.Toward.Theta = towardPair.Toward.Theta;
        }

        /// <summary>
        /// 座標資料
        /// </summary>
        public new ITowardPair Data { get { return Status.Data; } }

        /// <summary>
        /// 遠端 IP 位置
        /// </summary>
        public string IP { get; set; } = string.Empty;

        /// <summary>
        /// 雷射線
        /// </summary>
        public ILaserPoints LaserAPoints { get; } = FactoryMode.Factory.LaserPoints();

        /// <summary>
        /// 雷射線
        /// </summary>
        public ILaserPoints LaserBPoints { get; } = FactoryMode.Factory.LaserPoints();

        /// <summary>
        /// 雷射強度
        /// </summary>
        public ILaserStrength LaserStrength { get; } = FactoryMode.Factory.LaserStrength();

        /// <summary>
        /// 物件名稱
        /// </summary>
        public new string Name { get { return Status.Name; } set { Status.Name = value; } }

        /// <summary>
        /// 路徑
        /// </summary>
        public IPath Path { get; } = FactoryMode.Factory.Path();

        /// <summary>
        /// <para>安全框(mm)</para>
        /// </summary>
        public IArea SafetyArea { get { return Status.SafetyArea; } }

        /// <summary>
        /// <para>AGV 狀態</para>
        /// </summary>
        public IStatus Status { get; set; } = FactoryMode.Factory.Status();

        /// <summary>
        /// 建立拖曳點陣列
        /// </summary>
        public override IDragPoint[] CreatDragPoints()
        {
            return new IDragPoint[] {
                new DragPoint(Data.Position, GLSetting.Size, null),
            };
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            if (Name == string.Empty) return;
            gl.PushMatrix();
            {
                gl.Translate(Data.Position.X, Data.Position.Y, 0);
                gl.Rotate(Data.Toward.Theta, 0, 0, 1);
                gl.TextureBmp(GLSetting.BmpName, GLSetting.Size, GLSetting.MainColor, GLSetting.Type);

                // 安全框
                float z = (int)GLSetting.Type - 0.1f;
                gl.LineWidth(GLSetting.LineWidth);
                gl.Color(GLSetting.SubColor.GetFloats());
                gl.Begin(OpenGL.GL_LINE_LOOP);
                {
                    gl.Vertex(SafetyArea.Min.X, SafetyArea.Min.Y, z);
                    gl.Vertex(SafetyArea.Max.X, SafetyArea.Min.Y, z);
                    gl.Vertex(SafetyArea.Max.X, SafetyArea.Max.Y, z);
                    gl.Vertex(SafetyArea.Min.X, SafetyArea.Max.Y, z);
                }
                gl.End();
            }
            gl.PopMatrix();
            Path?.Draw(gl);
            LaserAPoints?.Draw(gl);
            LaserBPoints?.Draw(gl);
            LaserStrength?.Draw(gl);
        }
    }
}