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
    public partial class Form1 : Form
    {
        DataOperate dataOperate = new DataOperate();
        public Form1()
        {
            InitializeComponent();
            this.dateTimePicker2.Value = DateTime.Now.Date;
            this.dateTimePicker4.Value = DateTime.Now.Date;
            this.panel2.Hide();
            this.panel1.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadPigenInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        #region 鸽子基本信息管理
        /// <summary>
        /// 加载鸽子信息
        /// </summary>
        private void LoadPigenInfo()
        {
            string sqlStr = "SELECT KeyID,编号, 性别, 孵化日期,  配种方法, 配前运动, 配种日期 from pigeoninfo WHERE TRUE";
            DataSet set = dataOperate.GetData(sqlStr);
            List<MPigeonInfo> lstPigonInfo = new List<MPigeonInfo>();
            foreach (DataRow item in set.Tables[0].Rows)
            {
                MPigeonInfo pigeon = new MPigeonInfo();
                pigeon.编号 = item["编号"].ToString();
                pigeon.孵化日期 = DateTime.Parse(item["孵化日期"].ToString());
                pigeon.鸽龄 = this.CaculatePigenAge(pigeon.孵化日期);
                pigeon.配前运动 = item["配前运动"].ToString();
                pigeon.配种方法 = item["配种方法"].ToString();
                pigeon.配种日期 = DateTime.Parse(item["配种日期"].ToString());
                pigeon.性别 = item["性别"].ToString();
                pigeon.SetKeyID(int.Parse(item["KeyID"].ToString()));

                lstPigonInfo.Add(pigeon);

            }

            this.DataBindWithList<MPigeonInfo>(lstPigonInfo, this.dataGridView1);
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthday">生日</param>
        /// <returns>年龄</returns>
        private string CaculatePigenAge(DateTime birthday)
        {
            string result = string.Empty;
            DateTime now = DateTime.Now;
            TimeSpan span = now - birthday;
            int i = span.Days;
            int year = i / 365;
            i = i % 365;
            int month = i / 30;
            i = i % 30;

            result = year + "年" + month + "月" + i + "日";

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<MPigeonInfo> lstPigInfo = (List<MPigeonInfo>)this.dataGridView1.DataSource;
            foreach (var item in lstPigInfo)
            {
                string sqlStr = string.Format("update pigeoninfo set 编号='{0}',性别='{1}',孵化日期='{2}',配种方法='{3}',配前运动='{4}',配种日期='{5}' where KeyID={6}", item.编号, item.性别, item.孵化日期, item.配种方法, item.配前运动, item.配种日期, item.GetKeyID());
                this.dataOperate.DataOper(sqlStr);
            }

        }

        /// <summary>
        /// 批量删除鸽子基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            List<MPigeonInfo> lstPigInfo = (List<MPigeonInfo>)this.dataGridView1.DataSource;
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    string sqlStr = string.Format("delete from pigeoninfo where KeyID={0}", lstPigInfo[i].GetKeyID());
                    this.dataOperate.DataOper(sqlStr);
                }
            }
            this.Form1_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string num = this.textBox1.Text;
            string sex = this.comboBox1.SelectedItem == null ? string.Empty : this.comboBox1.SelectedItem.ToString();
            string breedType = this.comboBox3.SelectedItem == null ? string.Empty : this.comboBox3.SelectedItem.ToString();
            string breedSport = this.comboBox2.SelectedItem == null ? string.Empty : this.comboBox2.SelectedItem.ToString();
            DateTime breedStrartTime = this.dateTimePicker3.Value;
            DateTime breedEndTime = this.dateTimePicker4.Value;

            DateTime hatchStrartTime = this.dateTimePicker1.Value;
            DateTime hatchEndTime = this.dateTimePicker2.Value;


            string sqlStr = string.Format("SELECT KeyID,编号, 性别, 孵化日期,  配种方法, 配前运动, 配种日期 from pigeoninfo WHERE 孵化日期>='{0}' and 孵化日期<='{1}' and 配种日期>='{2}' and 配种日期<='{3}'", hatchStrartTime, hatchEndTime, breedStrartTime, breedEndTime);
            if (!string.IsNullOrEmpty(num))
            {
                sqlStr += string.Format("  and 编号 like '{0}%'", num);
            }
            if (!string.IsNullOrEmpty(sex))
            {
                sqlStr += string.Format("  and 性别 = '{0}'", sex);
            }

            if (!string.IsNullOrEmpty(breedType))
            {
                sqlStr += string.Format("  and 配种方法 = '{0}'", breedType);
            }

            if (!string.IsNullOrEmpty(breedSport))
            {
                sqlStr += string.Format("  and 配前运动 = '{0}'", breedSport);
            }

            DataSet set = dataOperate.GetData(sqlStr);
            List<MPigeonInfo> lstPigonInfo = new List<MPigeonInfo>();
            foreach (DataRow item in set.Tables[0].Rows)
            {
                MPigeonInfo pigeon = new MPigeonInfo();
                pigeon.编号 = item["编号"].ToString();
                pigeon.孵化日期 = DateTime.Parse(item["孵化日期"].ToString());
                pigeon.鸽龄 = this.CaculatePigenAge(pigeon.孵化日期);
                pigeon.配前运动 = item["配前运动"].ToString();
                pigeon.配种方法 = item["配种方法"].ToString();
                pigeon.配种日期 = DateTime.Parse(item["配种日期"].ToString());
                pigeon.性别 = item["性别"].ToString();
                pigeon.SetKeyID(int.Parse(item["KeyID"].ToString()));

                lstPigonInfo.Add(pigeon);

            }

            this.DataBindWithList<MPigeonInfo>(lstPigonInfo, this.dataGridView1);

        }


        #endregion



        /// <summary>
        /// 使用DataSet绑定数据
        /// </summary>
        /// <param name="set">数据集</param>
        /// <param name="gridview">视图</param>
        private void DataBindWithDataSet(DataSet set, DataGridView gridview)
        {
            ////DataTable dt = set.Tables[0];
            gridview.Columns.Clear();

            DataGridViewCheckBoxColumn dtCheck = new DataGridViewCheckBoxColumn();
            dtCheck.DataPropertyName = "check";
            dtCheck.HeaderText = "";
            gridview.Columns.Add(dtCheck);
            gridview.Columns[0].Width = 30;

            gridview.DataSource = set.Tables[0];
        }

        /// <summary>
        /// 使用List绑定数据
        /// </summary>
        /// <param name="set">数据集</param>
        /// <param name="gridview">视图</param>
        private void DataBindWithList<T>(List<T> lstData, DataGridView gridview)
        {
            ////DataTable dt = set.Tables[0];
            gridview.Columns.Clear();

            DataGridViewCheckBoxColumn dtCheck = new DataGridViewCheckBoxColumn();
            dtCheck.DataPropertyName = "check";
            dtCheck.HeaderText = "";
            gridview.Columns.Add(dtCheck);
            gridview.Columns[0].Width = 30;

            gridview.DataSource = lstData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BaseInfoManager baseInfo = new BaseInfoManager();
            baseInfo.Show();
        }

        /// <summary>
        /// 批量添加健康状况
        /// </summary>
        /// <param name="sender">事件原</param>
        /// <param name="e">事件</param>
        private void button7_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            List<MPigeonInfo> lstPigInfo = (List<MPigeonInfo>)this.dataGridView1.DataSource;
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    dic.Add(lstPigInfo[i].编号, lstPigInfo[i].GetKeyID());
                }
            }

            HealthManagerAdd form = new HealthManagerAdd(dic);
            form.Show();

        }


        /// <summary>
        /// 健康状况管理查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            string sqlStr = "select * from pigeondiseasemanage as a left join pigeoninfo as b on a.鸽子keyID=b.KeyID ";
            if (!string.IsNullOrEmpty(this.textBox2.Text)) 
            {
                sqlStr += " where b.编号 like '" + this.textBox2.Text + "%'";
            }

            sqlStr += " order by a.发病日期 desc";

            DataSet set = dataOperate.GetData(sqlStr);
            List<MHealthInfo> lstHealthInfo = new List<MHealthInfo>();
            foreach (DataRow item in set.Tables[0].Rows)
            {
                MHealthInfo pigeon = new MHealthInfo();
                pigeon.setKeyID(int.Parse(item["KeyID"].ToString()));
                pigeon.setPigkeyID(int.Parse(item["鸽子keyID"].ToString()));
                pigeon.发病日期 = DateTime.Parse(item["发病日期"].ToString());
                pigeon.疑似疾病 = item["疑似疾病"].ToString();
                pigeon.确诊疾病 = item["确诊疾病"].ToString();
                pigeon.基本症状 = item["基本症状"].ToString();
                pigeon.编号 = item["编号"].ToString();

                lstHealthInfo.Add(pigeon);
            }
            this.dataGridView3.Columns.Clear();
            this.DataBindWithList<MHealthInfo>(lstHealthInfo, this.dataGridView2);
        }

        private void 健康状况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Hide();
            this.panel2.Show();
            this.button5_Click(sender, e);
        }

        /// <summary>
        /// 鼠标放开方式加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            List<int> lstDiseadId = new List<int>();
            List<MHealthInfo> lstHeal = (List<MHealthInfo>)this.dataGridView2.DataSource;
            for (int i = 0; i < this.dataGridView2.RowCount; i++)
            {
                if (this.dataGridView2.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    lstDiseadId.Add(lstHeal[i].getKeyID());
                }
            }

            string foo = string.Join("','", lstDiseadId.ToArray());
            string selectSql = "SELECT * FROM `pigeonmedicationmanage` as m LEFT JOIN pigeondiseasemanage as d on m.`疾病管理ID`=d.`KeyID` LEFT JOIN pigeoninfo as p on d.`鸽子keyID`=p.KeyID where `疾病管理ID` in('" + foo + "')";
            DataSet set = dataOperate.GetData(selectSql);
            List<MMdicine> lstMedicine = new List<MMdicine>();
            foreach (DataRow item in set.Tables[0].Rows)
            {
                MMdicine medicine = new MMdicine();
                medicine.setID(int.Parse(item["ID"].ToString()));
                medicine.setDieasID(int.Parse(item["疾病管理ID"].ToString()));
                medicine.用药日期 = DateTime.Parse(item["用药日期"].ToString());
                medicine.药物名称 = item["药物名称"].ToString();
                medicine.生产厂家 = item["生产厂家"].ToString();
                medicine.用药途径 = item["用药途径"].ToString();
                medicine.用药疗程 = int.Parse(item["用药疗程"].ToString());
                medicine.发病日期 = DateTime.Parse(item["发病日期"].ToString());
                medicine.确诊疾病 = item["确诊疾病"].ToString();
                medicine.编号 = item["编号"].ToString();

                lstMedicine.Add(medicine);
            }

            this.DataBindWithList<MMdicine>(lstMedicine, this.dataGridView3);
        }





    }
}
