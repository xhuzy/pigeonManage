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
    public partial class AddProductInfo : Form
    {
        public delegate void RefushFrom(object sender, EventArgs e);

        public static event RefushFrom refush;

        public AddProductInfo()
        {
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Now.Date;
            this.comboBox2.SelectedItem = 0;
        }

        public AddProductInfo(List<MPigeonInfo> lstpigenoInfo)
        {
            this.lstPigInfo = lstpigenoInfo;
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Now.Date;
            this.comboBox2.SelectedItem = 0;
            foreach (var item in this.lstPigInfo) 
            {
                this.richTextBox1.AppendText(item.编号 + "\n");
            }


        }

        public List<MPigeonInfo> lstPigInfo { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            int wc = int.Parse(this.maskedTextBox2.Text);
            DateTime cdrq = this.dateTimePicker1.Value;
            int cdl = int.Parse(this.maskedTextBox1.Text);
            int zcd = int.Parse(this.maskedTextBox3.Text);
            int shd = int.Parse(this.maskedTextBox4.Text);
            int whd = int.Parse(this.maskedTextBox5.Text);
            int rkd = int.Parse(this.maskedTextBox6.Text);
            int ywd = int.Parse(this.maskedTextBox8.Text);
            int yzd = int.Parse(this.maskedTextBox2.Text);
            int dbd = int.Parse(this.maskedTextBox9.Text);
            string fhlx=this.comboBox2.SelectedItem==null?string.Empty:this.comboBox2.SelectedItem.ToString();

            string pigNumStr = this.richTextBox1.Text;

            string[] pigNumArray = pigNumStr.Split('\n');
            DataOperate dop = new DataOperate();
            foreach (var item in pigNumArray)
            {
                if (string.IsNullOrEmpty(item)) 
                {
                    continue;
                }

                MPigeonInfo pigInfo = this.lstPigInfo.Find(p => p.编号 == item);

                string sqlStr = string.Format(@"INSERT INTO `pigeonmanage`.`productionmanage` (`鸽子KeyID`, `窝次`, `产蛋日期`, `产蛋量`, `正常蛋`, `双黄蛋`, `无黄蛋`, `软壳单`, `异物蛋`, `异状蛋`, `蛋包蛋`, `第一次照蛋正常蛋数量`, `无精蛋`, `死精蛋`, `第二次照蛋正常蛋数量`, `死胚蛋`, `岀雏数`, `健雏数`, `残疾数`, `死亡数`, `孵化类型`, `批次`) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}')",
                                                                                         pigInfo.GetKeyID(), wc, cdrq.ToString("yyyy-MM-dd"), cdl, zcd, shd, whd, rkd, ywd, yzd, dbd, 0, 0, 0, 0, 0, 0, 0, 0, 0, fhlx,cdrq.ToString("yyyyMMdd"));
                dop.DataOper(sqlStr);
            }

            MessageBox.Show("添加成功");

            this.Close();
            refush(this,null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
