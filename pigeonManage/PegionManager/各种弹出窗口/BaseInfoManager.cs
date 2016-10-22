using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PegionManager
{
    public partial class BaseInfoManager : Form
    {
        public delegate void RefushFrom(object sender, EventArgs e);

        public static event RefushFrom refush;

        public BaseInfoManager()
        {
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Now.Date;
            this.dateTimePicker3.Value = DateTime.Now.Date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string num = this.textBox1.Text;
                string sex = this.comboBox1.SelectedItem.ToString();
                string breedType = this.comboBox3.SelectedItem.ToString();
                string breedSport = this.comboBox2.SelectedItem.ToString();
                DateTime breedTime = this.dateTimePicker3.Value;
                DateTime hatchTime = this.dateTimePicker1.Value;

                string insertSql = string.Format("INSERT INTO `pigeonmanage`.`pigeoninfo` (`编号`, `性别`, `孵化日期`, `配种方法`, `配前运动`, `配种日期`) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", num, sex, hatchTime.ToString("yyyy-MM-dd"), breedType, breedSport, breedTime.ToString("yyyy-MM-dd"));
                DataOperate dataOperate = new DataOperate();
                int i = dataOperate.DataOper(insertSql);

                if (i > 0)
                {
                    MessageBox.Show("增加成功");
                    this.Close();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            refush(this, null);
        }
    }
}
