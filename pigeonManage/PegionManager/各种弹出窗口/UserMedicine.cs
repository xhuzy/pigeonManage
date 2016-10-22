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
    public partial class UserMedicine : Form
    {
        public  delegate void RefushFrom(object sender, DataGridViewCellMouseEventArgs e);

        public static event RefushFrom refush;


        private List<MHealthInfo> lstHealthInfo = new List<MHealthInfo>();
        public UserMedicine()
        {
            InitializeComponent();
        }

        public UserMedicine(List<MHealthInfo> lstHealInfo)
        {
            this.lstHealthInfo = lstHealInfo;
            InitializeComponent();
            foreach (var item in lstHealInfo)
            {
                this.richTextBox1.Text += item.编号 + "|" + item.确诊疾病 + "|" + item.发病日期.ToString("yyyy-MM-dd") + "\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加用药记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Visible = false;
            try
            {
                DataOperate dataOper = new DataOperate();
                DateTime dateTiem = this.dateTimePicker1.Value;
                string medicineName = this.textBox1.Text;
                string productor = this.textBox2.Text;
                int useTime = 0;
                try
                {
                    useTime = int.Parse(this.textBox3.Text);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("用药疗程栏请输入正确的数字");
                    return;
                }

                string useType = string.Empty;

                if (this.checkBox1.CheckState == CheckState.Checked)
                {
                    useType += this.checkBox1.Text + ",";
                }
                if (this.checkBox2.CheckState == CheckState.Checked)
                {
                    useType += this.checkBox2.Text + ",";
                }
                if (this.checkBox3.CheckState == CheckState.Checked)
                {
                    useType += this.checkBox3.Text + ",";
                }
                if (this.checkBox4.CheckState == CheckState.Checked)
                {
                    useType += this.checkBox4.Text + ",";
                }
                if (this.checkBox5.CheckState == CheckState.Checked)
                {
                    useType += this.checkBox5.Text + ",";
                }

                string medicineStr = this.richTextBox1.Text;
                string[] pigeonNumArray = medicineStr.Split('\n');
                foreach (var pignumStr in pigeonNumArray)
                {
                    if (string.IsNullOrEmpty(pignumStr))
                    {
                        continue;
                    }
                    string pigNum = pignumStr.Split('|')[0];

                    var healthInfo = this.lstHealthInfo.Find(p => p.编号 == pigNum);
                    string sqlStr = string.Format("INSERT INTO `pigeonmanage`.`pigeonmedicationmanage` (`疾病管理ID`, `用药日期`,`药物名称`, `生产厂家`, `用药途径`, `用药疗程`) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", healthInfo.getKeyID(), dateTiem.ToString("yyyy-MM-dd"), medicineName, productor, useType, useTime);
                    dataOper.DataOper(sqlStr);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.button1.Visible = true;
                return;
            }
            this.button1.Visible = true;
            MessageBox.Show("保存成功");
            this.Close();
            refush(this, null);

        }
    }
}
