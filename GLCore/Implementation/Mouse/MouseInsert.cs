using Geometry;
using SharpGL;
using System;
using System.Collections.Generic;
using System.IO;

namespace GLCore
{
    /// <summary>
    /// 插入地圖步驟
    /// </summary>
    internal enum EInsertStep
    {
        /// <summary>
        /// 選擇第一點
        /// </summary>
        SelectFirstPoint,

        /// <summary>
        /// 選擇第二點
        /// </summary>
        SelectSecondPoint,

        /// <summary>
        /// 移動
        /// </summary>
        Translate,

        /// <summary>
        /// 完成
        /// </summary>
        Finish,

        /// <summary>
        /// 閒置
        /// </summary>
        Idle,
    }

    /// <summary>
    /// 滑鼠動作-插入地圖
    /// </summary>
    internal class MouseInsert : Mouse, IMouseInsert
    {
        private IForbiddenArea mArea = FactoryMode.Factory.ForbiddenArea("Select");
        private IAdvancedArea nArea = FactoryMode.Factory.AdvancedArea("Select");

        /// <summary>
        /// 插入地圖控制器
        /// </summary>
        private IMouseInsertPanel mPanel = null;
        private IMouseInsertPanel nPanel = null;

        /// <summary>
        /// 要加入 Database 的物件
        /// </summary>
        private IObstaclePoints mPoints = FactoryMode.Factory.ObstaclePoints();
        private IObstaclePoints nPoints = FactoryMode.Factory.ObstaclePoints();

        /// <summary>
        /// 插入地圖步驟
        /// </summary>
        private EInsertStep mStrp = EInsertStep.Idle;
        private EInsertStep nStrp = EInsertStep.Idle;

        public MouseInsert(string fileName, IMouseInsertPanel panel)
        {
            mPanel = panel;
            nPanel = panel;
            mPanel?.SetMouse(this);
            nPanel?.SetMouse(this);
            mPoints.GLSetting.MainColor = FactoryMode.Factory.Color(System.Drawing.Color.Blue, 150);
            nPoints.GLSetting.MainColor = FactoryMode.Factory.Color(System.Drawing.Color.Blue, 150);
            LoadFile(fileName);
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            mStrp = EInsertStep.Finish;
            nStrp = EInsertStep.Finish;
            mPanel?.Hide();
            nPanel?.Hide();
        }

        /// <summary>
        /// 點擊
        /// </summary>
        public override void Click(IPair pos)
        {
            base.Click(pos);
        }

        /// <summary>
        /// 按下
        /// </summary>
        public override void Down(IPair pos)
        {
            base.Down(pos);

            if (mStrp == EInsertStep.SelectFirstPoint)
            {
                mStrp = EInsertStep.SelectSecondPoint;
                mArea.Data.Set(pos, pos);
            }

            if (mStrp == EInsertStep.Idle)
            {
                mStrp = EInsertStep.Translate;
            }

            if (nStrp == EInsertStep.SelectFirstPoint)
            {
                nStrp = EInsertStep.SelectSecondPoint;
                nArea.Data.Set(pos, pos);
            }

            if (nStrp == EInsertStep.Idle)
            {
                nStrp = EInsertStep.Translate;
            }
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public override void Draw(OpenGL gl)
        {
            if (mStrp != EInsertStep.Finish)
            {
                mPoints?.Draw(gl);
                mArea?.Draw(gl);
            }

            if (nStrp != EInsertStep.Finish)
            {
                nPoints?.Draw(gl);
                nArea?.Draw(gl);
            }
        }

        /// <summary>
        /// 地圖左/右移
        /// </summary>
        public void Horizontal(int delta)
        {
            mStrp = EInsertStep.Idle;
            nStrp = EInsertStep.Idle;
            mPoints.Shift.X += delta;
            nPoints.Shift.X += delta;
        }

        /// <summary>
        /// 決定插入
        /// </summary>
        public void Insert()
        {
            if (mStrp == EInsertStep.Finish || nStrp == EInsertStep.Finish)
                return;

            mStrp = EInsertStep.Finish;
            nStrp = EInsertStep.Finish;
            mPanel?.Hide();
            nPanel?.Hide();

            List<IPair> points = GetSelectPoints();
            if (points.Count != 0)
                Database.ObstaclePointsGM.DataList.AddRange(points);
        }

        /// <summary>
        /// 移動
        /// </summary>
        public override void Move(IPair pos)
        {
            base.Move(pos);

            if (IsPress)
            {
                if (mStrp == EInsertStep.SelectSecondPoint)
                {
                    mArea.Data.Set(PressPos, pos);
                }
                else if (mStrp == EInsertStep.Translate)
                {
                    MovePoints();
                }
                else if (mStrp == EInsertStep.Idle || mStrp == EInsertStep.Finish)
                {
                    MoveUI();
                }

                if (nStrp == EInsertStep.SelectSecondPoint)
                {
                    nArea.Data.Set(PressPos, pos);
                }
                else if (nStrp == EInsertStep.Translate)
                {
                    MovePoints();
                }
                else if (nStrp == EInsertStep.Idle || nStrp == EInsertStep.Finish)
                {
                    MoveUI();
                }
            }
        }

        /// <summary>
        /// 釋放資源
        /// </summary>
        public override void Release()
        {
            mPanel?.Hide();
            nPanel?.Hide();
            mPoints = null;
            nPoints = null;
            mArea = null;
            nArea = null;
        }

        /// <summary>
        /// 旋轉
        /// </summary>
        public void Rotate(double theta)
        {
            mStrp = EInsertStep.Idle;
            nStrp = EInsertStep.Idle;
            mPoints.Rotate.Theta -= theta;
            nPoints.Rotate.Theta -= theta;
        }

        /// <summary>
        /// 選擇插入區域
        /// </summary>
        public void SetRange()
        {
            mStrp = EInsertStep.SelectFirstPoint;
            nStrp = EInsertStep.SelectFirstPoint;
        }

        /// <summary>
        /// 放開
        /// </summary>
        public override void Up(IPair pos)
        {
            base.Up(pos);

            if (mStrp != EInsertStep.Finish)
                mStrp = EInsertStep.Idle;

            if (nStrp != EInsertStep.Finish)
                nStrp = EInsertStep.Idle;
        }

        /// <summary>
        /// 地圖上/下移
        /// </summary>
        public void Vertical(int delta)
        {
            mStrp = EInsertStep.Idle;
            mPoints.Shift.Y += delta;

            nStrp = EInsertStep.Idle;
            nPoints.Shift.Y += delta;
        }

        /// <summary>
        /// 獲得選擇區域中的點
        /// </summary>
        private List<IPair> GetSelectPoints()
        {
            List<IPair> res = new List<IPair>();
            mPoints.DataList.SafetyForLoop((item) =>
                {
                    IPair p = item.Rotate(mPoints.Rotate).Add(mPoints.Shift);
                    if (mArea.Interference(p)) res.Add(p);
                });
            nPoints.DataList.SafetyForLoop((item) =>
            {
                IPair p = item.Rotate(nPoints.Rotate).Add(nPoints.Shift);
                if (nArea.Interference(p)) res.Add(p);
            });
            return res;
        }

        /// <summary>
        /// 載入障礙點
        /// </summary>
        private void LoadFile(string fileName)
        {
            try
            {
                bool startReadPoints = false;
                string[] lines = File.ReadAllLines(fileName);
                foreach (var data in lines)
                {
                    if (data == AGVDefine.MapFile.Head.ObstaclePoints)
                    {
                        startReadPoints = true;
                        continue;
                    }
                    if (startReadPoints)
                    {
                        try
                        {
                            string[] elm = data.Split(AGVDefine.MapFile.SplitChar);
                            IPair point = FactoryMode.Factory.Pair(double.Parse(elm[0]), double.Parse(elm[1]));
                            mPoints.DataList.Add(point);
                        }
                        catch (Exception)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 移動插入點
        /// </summary>
        private void MovePoints()
        {
            IPair delta = CurrentPos.Subtraction(PressPos);
            PressPos = FactoryMode.Factory.Pair(CurrentPos);
            mPoints.Shift = mPoints.Shift.Add(delta);
        }
    }
}