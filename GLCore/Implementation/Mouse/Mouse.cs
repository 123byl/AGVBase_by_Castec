using Geometry;
using SharpGL;
using System.Windows.Forms;

namespace GLCore
{
    /// <summary>
    /// 滑鼠動作
    /// </summary>
    internal abstract class Mouse : IMouse
    {
        /// <summary>
        /// 滑鼠拖曳起始位置
        /// </summary>
        private IPair MouseFrom = FactoryMode.Factory.Pair();

        /// <summary>
        /// UI 介面拖曳事件
        /// </summary>
        public event DelUITranslateEvent UITranslateEvent;

        /// <summary>
        /// 滑鼠目前位置
        /// </summary>
        public IPair CurrentPos { get; protected set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 滑鼠是否被按下
        /// </summary>
        public bool IsPress { get; protected set; } = false;

        /// <summary>
        /// 按鍵
        /// </summary>
        public Keys Key { get; protected set; } = Keys.None;

        /// <summary>
        /// 滑鼠按下時的位置
        /// </summary>
        public IPair PressPos { get; protected set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 滑鼠放開時的位置
        /// </summary>
        public IPair UpPos { get; protected set; } = FactoryMode.Factory.Pair();

        /// <summary>
        /// 點擊
        /// </summary>
        public virtual void Click(IPair pos)
        {
            CurrentPos = FactoryMode.Factory.Pair(pos);
            MouseFrom = FactoryMode.Factory.Pair(pos);
        }

        /// <summary>
        /// 按下
        /// </summary>
        public virtual void Down(IPair pos)
        {
            PressPos = FactoryMode.Factory.Pair(pos);
            MouseFrom = FactoryMode.Factory.Pair(pos);
            IsPress = true;
        }

        /// <summary>
        /// 繪圖
        /// </summary>
        public abstract void Draw(OpenGL gl);

        /// <summary>
        /// 按下按鈕
        /// </summary>
        public virtual void KeyDown(Keys key)
        {
            Key = key;
        }

        /// <summary>
        /// 放開按鈕
        /// </summary>
        public virtual void KeyUp()
        {
            Key = Keys.None;
        }

        /// <summary>
        /// 移動
        /// </summary>
        public virtual void Move(IPair pos)
        {
            CurrentPos = FactoryMode.Factory.Pair(pos);
            if (!IsPress)
            {
                MouseFrom = FactoryMode.Factory.Pair(pos);
            }
        }

        /// <summary>
        /// 釋放資源
        /// </summary>
        public virtual void Release() { }

        /// <summary>
        /// 放開
        /// </summary>
        public virtual void Up(IPair pos)
        {
            UpPos = FactoryMode.Factory.Pair(pos);
            MouseFrom = FactoryMode.Factory.Pair(pos);
            IsPress = false;
        }

        /// <summary>
        /// 移動 UI 介面
        /// </summary>
        protected void MoveUI()
        {
            if (IsPress)
            {
                UITranslateEventArgs arg = new UITranslateEventArgs() { Target = FactoryMode.Factory.Pair(CurrentPos), From = FactoryMode.Factory.Pair(MouseFrom) };
                UITranslateEvent?.Invoke(this, arg);
                if (arg.Target != null) MouseFrom = arg.Target;
            }
        }
    }
}
