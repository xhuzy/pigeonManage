//---------------------------------------------------------------------
// <copyright file="MProductInfo.cs" company="517 Enterprises">
// * copyright (C) 2013 517Na科技有限公司 版权所有。
// * author : ziang
// * FileName:MProductInfo.cs
// * histoty: create by ziang 2016-10-21 19:17:17
// </copyright>
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegionManager
{
    public class MProductInfo
    {
        private int ID;
        private int 鸽子KeyID;
        public int GetID()
        {
            return this.ID;
        }
        public void SetID(int id)
        {
            this.ID = id;
        }
        public int GetpigKeyID()
        {
            return this.鸽子KeyID;
        }
        public void SetpigkeyID(int id)
        {
            this.鸽子KeyID = id;
        }

        public string 编号 { get; set; }

        public int 窝次 { get; set; }
        public DateTime 产蛋日期 { get; set; }
        public int 产蛋量 { get; set; }
        public int 正常蛋 { get; set; }
        public int 双黄蛋 { get; set; }
        public int 无黄蛋 { get; set; }
        public int 软壳单 { get; set; }
        public int 异物蛋 { get; set; }
        public int 异状蛋 { get; set; }
        public int 蛋包蛋 { get; set; }
        public DateTime 第一次照蛋日期 { get; set; }
        public int 第一次照蛋正常蛋数量 { get; set; }
        public int 无精蛋 { get; set; }
        public int 死精蛋 { get; set; }
        public DateTime 第二次照蛋日期 { get; set; }
        public int 第二次照蛋正常蛋数量 { get; set; }
        public int 死胚蛋 { get; set; }
        public int 岀雏数 { get; set; }
        public int 健雏数 { get; set; }
        public int 残疾数 { get; set; }
        public int 死亡数 { get; set; }
        public string 孵化类型 { get; set; }
        public string 批次 { get; set; }


    }
}
