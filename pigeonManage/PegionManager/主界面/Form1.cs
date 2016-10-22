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
            this.panel1.Show(); 
            this.panel2.Hide();
            this.panel3.Hide();

            UserMedicine.refush += this.dataGridView2_CellMouseUp;
            AddProductInfo.refush += this.button11_Click;
            BaseInfoManager.refush += button4_Click;

            this.label16.Text = string.Empty;
            int foo=this.TodayZDGE();
            if ( foo> 0) 
            {
                this.label16.Text = "今日有" + foo + "条待照蛋记录";
            }


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

        #region 菜单事件
        private void 基本信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Show();
            this.panel2.Hide();
            this.panel3.Hide();
            this.button4_Click(sender, e);
        }

        private void 健康状况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Hide();
            this.panel2.Show();
            this.panel3.Hide();
            this.button5_Click(sender, e);
        }



        private void 生产信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.Hide();
            this.panel2.Hide();
            this.panel3.Show();
            this.button11_Click(sender, e);
        }
        #endregion

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

        /// <summary>
        /// 保存鸽子基本信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            List<MPigeonInfo> lstPigInfo = (List<MPigeonInfo>)this.dataGridView1.DataSource;
            foreach (var item in lstPigInfo)
            {
                string sqlStr = string.Format("update pigeoninfo set 编号='{0}',性别='{1}',孵化日期='{2}',配种方法='{3}',配前运动='{4}',配种日期='{5}' where KeyID={6}", item.编号, item.性别, item.孵化日期, item.配种方法, item.配前运动, item.配种日期, item.GetKeyID());
                this.dataOperate.DataOper(sqlStr);
            }

            button4_Click(sender, e);

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
                    string deleteMedicine = "DELETE from pigeonmedicationmanage where 疾病管理ID in(SELECT KeyID FROM `pigeondiseasemanage` where `鸽子keyID`=" + lstPigInfo[i].GetKeyID() + ") ";
                    this.dataOperate.DataOper(deleteMedicine);

                    string deleteDisabel = "DELETE FROM `pigeondiseasemanage` where 鸽子keyID=" + lstPigInfo[i].GetKeyID();
                    this.dataOperate.DataOper(deleteDisabel);

                    string sqlProduct="DELETE FROM `productionmanage` where 鸽子KeyID="+lstPigInfo[i].GetKeyID();
                    this.dataOperate.DataOper(sqlProduct);

                    string sqlStr = string.Format("delete from pigeoninfo where KeyID={0}", lstPigInfo[i].GetKeyID());
                    this.dataOperate.DataOper(sqlStr);
                }
            }
            this.Form1_Load(sender, e);
        }

        /// <summary>
        /// 查询鸽子基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            this.label16.Text = string.Empty;
            int foo = this.TodayZDGE();
            if (foo > 0)
            {
                this.label16.Text = "今日有" + foo + "条待照蛋记录";
            }


        }


        /// <summary>
        /// 鸽子信息添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            BaseInfoManager baseInfo = new BaseInfoManager();
            baseInfo.Show();
        }

        #endregion

        #region 健康和用药状况管理

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
            if (dic == null || dic.Count < 1)
            {
                MessageBox.Show("请先选择鸽子");
                return;
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
                pigeon.发病日期 = DateTime.Parse(item["发病日期"].ToString()).Date;
                pigeon.疑似疾病 = item["疑似疾病"].ToString();
                pigeon.确诊疾病 = item["确诊疾病"].ToString();
                pigeon.基本症状 = item["基本症状"].ToString();
                pigeon.编号 = item["编号"].ToString();

                lstHealthInfo.Add(pigeon);
            }
            this.dataGridView3.Columns.Clear();
            this.DataBindWithList<MHealthInfo>(lstHealthInfo, this.dataGridView2);
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
                medicine.用药日期 = DateTime.Parse(item["用药日期"].ToString()).Date;
                medicine.药物名称 = item["药物名称"].ToString();
                medicine.生产厂家 = item["生产厂家"].ToString();
                medicine.用药途径 = item["用药途径"].ToString();
                medicine.用药疗程 = int.Parse(item["用药疗程"].ToString());
                medicine.发病日期 = DateTime.Parse(item["发病日期"].ToString()).Date;
                medicine.确诊疾病 = item["确诊疾病"].ToString();
                medicine.编号 = item["编号"].ToString();

                lstMedicine.Add(medicine);
            }

            this.DataBindWithList<MMdicine>(lstMedicine, this.dataGridView3);
        }



        /// <summary>
        /// 健康状态保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            List<MHealthInfo> lstHealthInfo = (List<MHealthInfo>)this.dataGridView2.DataSource;
            foreach (var item in lstHealthInfo)
            {
                string sqlStr = string.Format("update pigeondiseasemanage set 发病日期='{0}',疑似疾病='{1}',确诊疾病='{2}',基本症状='{3}'  where KeyID={4}", item.发病日期, item.疑似疾病, item.确诊疾病, item.基本症状, item.getKeyID());
                this.dataOperate.DataOper(sqlStr);
            }
        }


        /// <summary>
        /// 用药
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            List<MHealthInfo> lstHealthInfo = (List<MHealthInfo>)this.dataGridView2.DataSource;
            List<MHealthInfo> temp = new List<MHealthInfo>();
            for (int i = 0; i < this.dataGridView2.RowCount; i++)
            {
                if (this.dataGridView2.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    temp.Add(lstHealthInfo[i]);
                }
            }
            UserMedicine useMedicine = new UserMedicine(temp);
            useMedicine.Show();
        }


        /// <summary>
        /// 健康状况管理删除选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            List<MHealthInfo> lstHealthInfo = (List<MHealthInfo>)this.dataGridView2.DataSource;
            for (int i = 0; i < this.dataGridView2.RowCount; i++)
            {
                if (this.dataGridView2.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    string sqlStr = string.Format("delete from pigeondiseasemanage where KeyID={0}", lstHealthInfo[i].getKeyID());
                    string deleMidicine = "DELETE FROM `pigeonmedicationmanage` where `疾病管理ID`=" + lstHealthInfo[i].getKeyID();
                    this.dataOperate.DataOper(deleMidicine);
                    this.dataOperate.DataOper(sqlStr);
                }
            }

            this.button5_Click(sender, e);
        }

        /// <summary>
        /// 保存用药情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            List<MMdicine> lstMedicineInfo = (List<MMdicine>)this.dataGridView3.DataSource;
            foreach (var item in lstMedicineInfo)
            {
                string sqlStr = string.Format("update pigeonmedicationmanage set 用药日期='{0}',药物名称='{1}',生产厂家='{2}',用药途径='{3}',用药疗程={4}  where ID={5}", item.用药日期, item.药物名称, item.生产厂家, item.用药途径, item.用药疗程, item.getID());
                this.dataOperate.DataOper(sqlStr);
            }
        }

        /// <summary>
        /// 批量删除药物情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            List<MMdicine> lstMedicineInfo = (List<MMdicine>)this.dataGridView3.DataSource;
            for (int i = 0; i < this.dataGridView3.RowCount; i++)
            {
                if (this.dataGridView3.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    string sqlStr = string.Format("delete from pigeonmedicationmanage where ID={0}", lstMedicineInfo[i].getID());
                    this.dataOperate.DataOper(sqlStr);
                }
            }

            this.dataGridView2_CellMouseUp(sender, null);
        }
        #endregion

        #region 绑定数据到datagrivew

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

            var cell = gridview.GetCellDisplayRectangle(0, -1, true);
            var checkbox = new CheckBox { Left = cell.Left, Top = cell.Top, Width = 18, Height = 18 };
            gridview.Controls.Add(checkbox);
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

            var cell = gridview.GetCellDisplayRectangle(0, -1, true);
            var checkbox = new CheckBox { Left = cell.Left + 8, Top = cell.Top + 0, Width = 18, Height = 18 };
            checkbox.MouseUp += new MouseEventHandler(this.CheckBoxClickEvent);
            gridview.Controls.Add(checkbox);
            gridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;



        }

        /// <summary>
        /// 全选复选框点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxClickEvent(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            DataGridView grividw = (DataGridView)check.Parent;
            grividw.EndEdit();
            if (check.CheckState == CheckState.Checked)
            {
                for (int i = 0; i < grividw.Rows.Count; i++)
                {
                    grividw.Rows[i].Cells[0].Value = "True";
                }
            }
            else
            {
                for (int i = 0; i < grividw.Rows.Count; i++)
                {
                    grividw.Rows[i].Cells[0].Value = "False";
                }
            }

            if (grividw == this.dataGridView2)
            {
                dataGridView2_CellMouseUp(sender, null);
            }
        }

        #endregion

        #region 生成信息

        private int TodayZDGE() 
        {
            string sqlStr = "SELECT `ID`, `鸽子KeyID`, `窝次`, `产蛋日期`, `产蛋量`, `正常蛋`, `双黄蛋`, `无黄蛋`, `软壳单`, `异物蛋`, `异状蛋`, `蛋包蛋`, `第一次照蛋正常蛋数量`, `无精蛋`, `死精蛋`, `第二次照蛋正常蛋数量`, `死胚蛋`, `岀雏数`, `健雏数`, `残疾数`, `死亡数`, `孵化类型`, `批次`,`编号` FROM `productionmanage` as a LEFT JOIN pigeoninfo as b on a.`鸽子KeyID`=b.KeyID where ID>0";

            DataOperate operater = new DataOperate();
            DataSet dataSet = operater.GetData(sqlStr);
            List<MProductInfo> lstProduct = new List<MProductInfo>();
            foreach (DataRow item in dataSet.Tables[0].Rows)
            {
                MProductInfo product = new MProductInfo();
                product.SetID(int.Parse(item["ID"].ToString()));
                product.SetpigkeyID(int.Parse(item["鸽子KeyID"].ToString()));
                product.窝次 = int.Parse(item["窝次"].ToString());
                product.产蛋日期 = DateTime.Parse(item["产蛋日期"].ToString()).Date;
                product.产蛋量 = int.Parse(item["产蛋量"].ToString());
                product.正常蛋 = int.Parse(item["正常蛋"].ToString());
                product.双黄蛋 = int.Parse(item["双黄蛋"].ToString());
                product.无黄蛋 = int.Parse(item["无黄蛋"].ToString());
                product.软壳单 = int.Parse(item["软壳单"].ToString());
                product.异物蛋 = int.Parse(item["异物蛋"].ToString());
                product.异状蛋 = int.Parse(item["异状蛋"].ToString());
                product.蛋包蛋 = int.Parse(item["蛋包蛋"].ToString());
                product.第一次照蛋正常蛋数量 = int.Parse(item["第一次照蛋正常蛋数量"].ToString());
                product.无精蛋 = int.Parse(item["无精蛋"].ToString());
                product.死精蛋 = int.Parse(item["死精蛋"].ToString());
                product.第二次照蛋正常蛋数量 = int.Parse(item["第二次照蛋正常蛋数量"].ToString());
                product.死胚蛋 = int.Parse(item["死胚蛋"].ToString());
                product.岀雏数 = int.Parse(item["岀雏数"].ToString());
                product.健雏数 = int.Parse(item["健雏数"].ToString());
                product.残疾数 = int.Parse(item["残疾数"].ToString());
                product.死亡数 = int.Parse(item["死亡数"].ToString());
                product.孵化类型 = item["孵化类型"].ToString();
                product.批次 = item["批次"].ToString();
                product.编号 = item["编号"].ToString();
                product.第一次照蛋日期 = product.产蛋日期.AddDays(5).Date;
                product.第二次照蛋日期 = product.产蛋日期.AddDays(10).Date;

                lstProduct.Add(product);
            }
            lstProduct= lstProduct.FindAll(p => p.第二次照蛋日期 == DateTime.Now.Date || p.第一次照蛋日期 == DateTime.Now.Date);
             return lstProduct.Count;
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            string sqlStr = "SELECT `ID`, `鸽子KeyID`, `窝次`, `产蛋日期`, `产蛋量`, `正常蛋`, `双黄蛋`, `无黄蛋`, `软壳单`, `异物蛋`, `异状蛋`, `蛋包蛋`, `第一次照蛋正常蛋数量`, `无精蛋`, `死精蛋`, `第二次照蛋正常蛋数量`, `死胚蛋`, `岀雏数`, `健雏数`, `残疾数`, `死亡数`, `孵化类型`, `批次`,`编号` FROM `productionmanage` as a LEFT JOIN pigeoninfo as b on a.`鸽子KeyID`=b.KeyID where ID>0";
            if (!string.IsNullOrEmpty(this.textBox3.Text))
            {
                sqlStr += " and b.编号 like '" + this.textBox3.Text + "%'";
            }

            if (!string.IsNullOrEmpty(this.textBox4.Text))
            {
                sqlStr += " and a.批次 like '" + this.textBox4.Text + "%'";
            }
            string fuhuaType = this.comboBox4.SelectedItem == null ? string.Empty : this.comboBox4.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(fuhuaType))
            {
                sqlStr += " and a.孵化类型 = '" + fuhuaType + "'";
            }

            DataOperate operater = new DataOperate();
            DataSet dataSet = operater.GetData(sqlStr);

            List<MProductInfo> lstProduct = new List<MProductInfo>();
            foreach (DataRow item in dataSet.Tables[0].Rows)
            {
                MProductInfo product = new MProductInfo();
                product.SetID(int.Parse(item["ID"].ToString()));
                product.SetpigkeyID(int.Parse(item["鸽子KeyID"].ToString()));
                product.窝次 = int.Parse(item["窝次"].ToString());
                product.产蛋日期 = DateTime.Parse(item["产蛋日期"].ToString()).Date;
                product.产蛋量 = int.Parse(item["产蛋量"].ToString());
                product.正常蛋 = int.Parse(item["正常蛋"].ToString());
                product.双黄蛋 = int.Parse(item["双黄蛋"].ToString());
                product.无黄蛋 = int.Parse(item["无黄蛋"].ToString());
                product.软壳单 = int.Parse(item["软壳单"].ToString());
                product.异物蛋 = int.Parse(item["异物蛋"].ToString());
                product.异状蛋 = int.Parse(item["异状蛋"].ToString());
                product.蛋包蛋 = int.Parse(item["蛋包蛋"].ToString());
                product.第一次照蛋正常蛋数量 = int.Parse(item["第一次照蛋正常蛋数量"].ToString());
                product.无精蛋 = int.Parse(item["无精蛋"].ToString());
                product.死精蛋 = int.Parse(item["死精蛋"].ToString());
                product.第二次照蛋正常蛋数量 = int.Parse(item["第二次照蛋正常蛋数量"].ToString());
                product.死胚蛋 = int.Parse(item["死胚蛋"].ToString());
                product.岀雏数 = int.Parse(item["岀雏数"].ToString());
                product.健雏数 = int.Parse(item["健雏数"].ToString());
                product.残疾数 = int.Parse(item["残疾数"].ToString());
                product.死亡数 = int.Parse(item["死亡数"].ToString());
                product.孵化类型 = item["孵化类型"].ToString();
                product.批次 = item["批次"].ToString();
                product.编号 = item["编号"].ToString();
                product.第一次照蛋日期 = product.产蛋日期.AddDays(5).Date;
                product.第二次照蛋日期 = product.产蛋日期.AddDays(10).Date;

                lstProduct.Add(product);
            }

            if (this.comboBox5.SelectedItem != null)
            {
                if (this.comboBox5.SelectedItem.ToString() == "第一次照蛋")
                {
                    lstProduct=lstProduct.FindAll(p => p.第一次照蛋日期 == DateTime.Now.Date);
                }
                else if (this.comboBox5.SelectedItem.ToString() == "第二次照蛋")
                {
                   lstProduct= lstProduct.FindAll(p => p.第二次照蛋日期 == DateTime.Now.Date);
                }
                else
                {
                   lstProduct= lstProduct.FindAll(p => p.第二次照蛋日期 == DateTime.Now.Date || p.第一次照蛋日期 == DateTime.Now.Date);
                }
            }
            this.DataBindWithList<MProductInfo>(lstProduct, this.dataGridView4);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            List<MProductInfo> lstProduct = (List<MProductInfo>)this.dataGridView4.DataSource;
            foreach (var item in lstProduct)
            {
                string sqlStr = string.Format(@"update productionmanage set 窝次='{0}',
                                                                            产蛋日期='{1}',
                                                                            产蛋量='{2}',
                                                                            正常蛋='{3}',
                                                                            双黄蛋='{4}',
                                                                            无黄蛋='{5}',
                                                                            软壳单='{6}',
                                                                            异物蛋='{7}',
                                                                            异状蛋='{8}',
                                                                            蛋包蛋='{9}',
                                                                            第一次照蛋正常蛋数量='{10}',
                                                                            无精蛋='{11}',
                                                                            死精蛋='{12}',
                                                                            第二次照蛋正常蛋数量='{13}',
                                                                            死胚蛋='{14}',
                                                                            岀雏数='{15}',
                                                                            健雏数='{16}',
                                                                            残疾数='{17}',
                                                                            死亡数='{18}',
                                                                            孵化类型='{19}',
                                                                            批次='{20}'  
                                                                            where ID={21}",
                                                                            item.窝次,
                                                                            item.产蛋日期,
                                                                            item.产蛋量,
                                                                            item.正常蛋,
                                                                            item.双黄蛋,
                                                                            item.无黄蛋,
                                                                            item.软壳单,
                                                                            item.异物蛋,
                                                                            item.异状蛋,
                                                                            item.蛋包蛋,
                                                                            item.第一次照蛋正常蛋数量,
                                                                            item.无精蛋,
                                                                            item.死精蛋,
                                                                            item.第二次照蛋正常蛋数量,
                                                                            item.死胚蛋,
                                                                            item.岀雏数,
                                                                            item.健雏数,
                                                                            item.残疾数,
                                                                            item.死亡数,
                                                                            item.孵化类型,
                                                                            item.产蛋日期.ToString("yyyyMMdd"),
                                                                            item.GetID());
                this.dataOperate.DataOper(sqlStr);
            }

            MessageBox.Show("保存成功");
            button11_Click(sender, e);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            List<MProductInfo> lstProduct = (List<MProductInfo>)this.dataGridView4.DataSource;
            for (int i = 0; i < this.dataGridView4.RowCount; i++)
            {
                if (this.dataGridView4.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    string sqlStr = string.Format("delete from productionmanage where ID={0}", lstProduct[i].GetID());
                    this.dataOperate.DataOper(sqlStr);
                }
            }

            MessageBox.Show("删除成功");
            button11_Click(sender, e);

        }

        /// <summary>
        /// 添加产蛋信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            List<MPigeonInfo> temp = new List<MPigeonInfo>();
            List<MPigeonInfo> lstPigInfo = (List<MPigeonInfo>)this.dataGridView1.DataSource;
            for (int i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].EditedFormattedValue.ToString() == "True")
                {
                    temp.Add(lstPigInfo[i]);
                }
            }
            if (temp == null || temp.Count < 1)
            {
                MessageBox.Show("请先选择鸽子");
                return;
            }
            AddProductInfo form = new AddProductInfo(temp);
            form.Show();
        }

        /// <summary>
        /// 今日待照蛋提醒
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label16_Click(object sender, EventArgs e)
        {
            this.panel1.Hide();
            this.panel2.Hide();
            this.panel3.Show();
            this.comboBox5.SelectedIndex = 2;
            button11_Click(sender, e);
        }

        #endregion

       

       


    }
}
