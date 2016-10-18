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
    public partial class HealthManagerAdd : Form
    {
        public Dictionary<string, int> healthManagerPig = new Dictionary<string, int>();

        public HealthManagerAdd(Dictionary<string, int> healthManagerPig)
        {
            this.healthManagerPig = healthManagerPig;
            if (this.healthManagerPig == null || this.healthManagerPig.Count <= 0)
            {
                MessageBox.Show("请先选择鸽子");
                this.Close();
            }

            InitializeComponent();
            foreach (var item in this.healthManagerPig)
            {
                this.richTextBox4.AppendText(item.Value + "\r\n");
            }
        }

        public HealthManagerAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<MHealthInfo> lstHealth = new List<MHealthInfo>();
            string pigNum = this.richTextBox4.Text;
            string[] pigNumArray = pigNum.Split('\r');
            foreach (var item in pigNumArray)
            {
                if (this.healthManagerPig.Keys.Contains(item))
                {
                    MHealthInfo health = new MHealthInfo();
                    health.setPigkeyID(this.healthManagerPig[item]);
                    health.发病日期 = this.dateTimePicker1.Value;
                    health.基本症状 = this.richTextBox1.Text;
                    health.疑似疾病 = this.richTextBox2.Text;
                    health.确诊疾病 = this.richTextBox3.Text;

                    lstHealth.Add(health);
                }
            }

            ////将数据保存到数据库中
            DataOperate operate = new DataOperate();
            foreach (var item in lstHealth)
            {
                string sqlStr = string.Format("INSERT INTO `pigeonmanage`.`pigeondiseasemanage` (`鸽子编号`, `发病日期`, `疑似疾病`, `确诊疾病`, `基本症状`) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}')", item.getPigkeyID(), item.发病日期.ToString("yyyy-MM-dd"), item.疑似疾病, item.确诊疾病, item.基本症状);
                operate.DataOper(sqlStr);
            }

            MessageBox.Show("保存成功");
            this.Close();

        }
    }
}
