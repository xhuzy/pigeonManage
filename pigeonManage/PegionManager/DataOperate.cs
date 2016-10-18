using MySql.Data.MySqlClient;
//---------------------------------------------------------------------
// <copyright file="DataOperate.cs" company="517 Enterprises">
// * copyright (C) 2013 517Na科技有限公司 版权所有。
// * author : ziang
// * FileName:DataOperate.cs
// * histoty: create by ziang 2016-10-11 21:08:04
// </copyright>
//---------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PegionManager
{
    public class DataOperate
    {
        public static string ConnectTionStr = "server=127.0.0.1;database=pigeonmanage;charset=utf8;uid=root;pwd=root";

        public DataSet GetData(string sqlStr)
        {
            DataSet set = null;
            using (IDbConnection conn = new MySqlConnection(ConnectTionStr))
            {
                set = MySqlHelper.ExecuteDataset(conn as MySqlConnection, sqlStr);
            }

            return set;
        }

        public int DataOper(string sqlStr)
        {
            using (IDbConnection conn = new MySqlConnection(ConnectTionStr))
            {
                conn.Open();
                return MySqlHelper.ExecuteNonQuery(conn as MySqlConnection, sqlStr, null);
            }
        }
    }
}
