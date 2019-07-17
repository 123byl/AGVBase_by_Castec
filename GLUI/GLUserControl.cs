using System.ComponentModel;
using System.Windows.Forms;

namespace GLUI
{
    /// <summary>
    /// OpenGL UI 控制項
    /// </summary>
    public partial class GLUserControl : UserControl
    {
        /// <summary>
        /// 建立新的 OpenGL UI 控制項
        /// </summary>
        public GLUserControl()
        {
            InitializeComponent();
            BaseCtrl = new Scene(openGLControl);
        }

        /// <summary>
        /// 基礎控制項
        /// </summary>
        public IScene BaseCtrl { get; } = null;

        /// <summary>
        /// OpenGL 編譯版本，無法修改
        /// </summary>
        [Description("OpenGL 編譯版本"), Category("SharpGL")]
        public string BuildVersion => openGLControl?.OpenGL?.Version ?? string.Empty;

        /// <summary>
        /// OpenGL 版本，無法修改，目前使用 <see cref="SharpGL.Version.OpenGLVersion.OpenGL2_1"/>
        /// </summary>
        [Description("OpenGL 版本"), Category("SharpGL")]
        public SharpGL.Version.OpenGLVersion OpenGLVersion => openGLControl?.OpenGLVersion ?? SharpGL.Version.OpenGLVersion.OpenGL1_1;

        /// <summary>
        /// 渲染類型，無法修改，目前使用 <see cref="SharpGL.RenderContextType.NativeWindow"/>
        /// </summary>
        [Description("渲染類型"), Category("SharpGL")]
        public SharpGL.RenderContextType RenderContextType => openGLControl?.RenderContextType ?? SharpGL.RenderContextType.DIBSection;

        /// <summary>
        /// 顯示卡名稱，無法修改
        /// </summary>
        [Description("顯示卡名稱"), Category("SharpGL")]
        public string Renderer => openGLControl?.OpenGL?.Renderer ?? string.Empty;

        /// <summary>
        /// 顯示卡製造商，無法修改
        /// </summary>
        [Description("顯示卡製造商"), Category("SharpGL")]
        public string Vendor => openGLControl?.OpenGL?.Vendor ?? string.Empty;

        private void GLUserControl_Resize(object sender, System.EventArgs e)
        {
            openGLControl.Height = Height - 6;
            openGLControl.Width = Width - 8;
        }

        private void GLUserControl_Load(object sender, System.EventArgs e)
        {
            openGLControl.Height = Height - 6;
            openGLControl.Width = Width - 8;
        }
    }
}
