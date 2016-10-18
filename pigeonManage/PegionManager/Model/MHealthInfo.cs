//---------------------------------------------------------------------
// <copyright file="MHealthInfo.cs" company="517 Enterprises">
// * copyright (C) 2013 517Na科技有限公司 版权所有。
// * author : ziang
// * FileName:MHealthInfo.cs
// * histoty: create by ziang 2016-10-17 21:47:48
// </copyright>
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegionManager
{
    public class MHealthInfo
    {
        private int keyID;
        private int pigkeyID;
        public int getKeyID()
        {
            return this.keyID;
        }
        public void setKeyID(int keyID)
        {
            this.keyID = keyID;
        }

        public int getPigkeyID()
        {
            return this.pigkeyID;
        }
        public void setPigkeyID(int keyID)
        {
            this.pigkeyID = keyID;
        }
        public string 编号 { get; set; }
        public DateTime 发病日期 { get; set; }
        public string 基本症状 { get; set; }
        public string 疑似疾病 { get; set; }
        public string 确诊疾病 { get; set; }
    }
}
