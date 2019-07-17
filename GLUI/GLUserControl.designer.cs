namespace GLUI
{
    partial class GLUserControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.openGLControl = new SharpGL.OpenGLControl();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // openGLControl
            // 
            this.openGLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(4, 3);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(292, 294);
            this.openGLControl.TabIndex = 0;
            // 
            // GLUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.openGLControl);
            this.Name = "GLUserControl";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.GLUserControl_Load);
            this.Resize += new System.EventHandler(this.GLUserControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl;
    }
}
