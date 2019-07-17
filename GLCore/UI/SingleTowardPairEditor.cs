using Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static FactoryMode;

namespace GLCore.UI
{
    public partial class SingleTowardPairEditor : Form
    {
        /// <summary>
        /// 控制對象
        /// </summary>
        private ISingle<ITowardPair> Target;

        /// <summary>
        /// 建立標示物編輯器
        /// </summary>
        public SingleTowardPairEditor(ISingle<ITowardPair> target)
        {
            InitializeComponent();
            Target = target;
            UpdateInfo();
            ShowDialog();
        }

        /// <summary>
        /// 更新狀態
        /// </summary>
        private void UpdateInfo()
        {
            txtName.Text = Target.Name;
            nmrX.Value = Target.Data.Position.X;
            nmrY.Value = Target.Data.Position.Y;
            nmrToward.Value = (decimal)Target.Data.Toward.Theta;
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                // 寫入名稱
                Target.Name = txtName.Text;

                // 寫入座標
                Target.SetLocation(FactoryMode.Factory.TowardPair((double)nmrX.Value, (double)nmrY.Value, (double)nmrToward.Value));
            }
            catch (Exception)
            {
            }

            Close();
        }
    }
}
