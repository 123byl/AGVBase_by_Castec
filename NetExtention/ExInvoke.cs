using System;
using System.Windows.Forms;

namespace NetExtension
{
    /// <summary>
    /// <para>提供視覺控制物件(<see cref="Control"/>)執行緒安全的操作方法</para>
    /// </summary>
    public static class ExInvoke
    {
        /// <summary>
        /// <para>對供視覺控制物件(<see cref="Control"/>)進行執行緒安全的操作</para>
        /// </summary>
        public static void InvokeIfNecessary(this Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired) { ctrl.Invoke(action); }
            else { action(); }
        }

        /// <summary>
        /// <para>對供視覺控制物件(<see cref="Control"/>)進行執行緒安全的操作</para>
        /// </summary>
        public static void InvokeIfNecessary(this Control ctrl, Action<Control> action)
        {
            if (ctrl.InvokeRequired) { ctrl.Invoke(action); }
            else { action(ctrl); }
        }
    }
}
