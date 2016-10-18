using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegionManager
{
   public class MMdicine
    {
        public string 编号 { get; set; }
        public string 确诊疾病 { get; set; }
        public DateTime 发病日期 { get; set; }
       public DateTime 用药日期 { get; set; }
       public string 药物名称 { get; set; }
       public string 生产厂家{get;set;}
       public string 用药途径 { get; set; }
       public int 用药疗程 { get; set; }
      

       private int ID;
       private int 疾病管理ID;

       public void setID(int id) 
       {
           this.ID = id;
       }
       public int getID() 
       {
           return this.ID;
       }

       public void setDieasID(int id) 
       {
           this.疾病管理ID = id;
       }

       public int getDieasID() {
           return this.疾病管理ID;
       }
    }
}
