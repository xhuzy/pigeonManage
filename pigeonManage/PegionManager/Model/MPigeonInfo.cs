//---------------------------------------------------------------------
// <copyright file="MPigeonInfo.cs" company="517 Enterprises">
// * copyright (C) 2013 517Na科技有限公司 版权所有。
// * author : ziang
// * FileName:MPigeonInfo.cs
// * histoty: create by ziang 2016-10-14 20:14:29
// </copyright>
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegionManager
{
    public class MPigeonInfo
    {
        public string 编号 { get; set; }
        public string 性别 { get; set; }
        public DateTime 孵化日期 { get; set; }
        public string 配种方法 { get; set; }
        public string 配前运动 { get; set; }
        public DateTime 配种日期 { get; set; }
        public string 鸽龄 { get; set; }

        private int keyID;

        public void SetKeyID(int keyID)
        {
            this.keyID = keyID;
        }
        public int GetKeyID()
        {
            return this.keyID;
        }
    }
}
