using BusModel;
using DataHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHelper
{
    public class SQLBusiness
    {
        public static Dictionary<string, string> GetBusinessInfo(string Name)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                SQLHelper sql = new SQLHelper("DefaultConnection");
                string sqlText = string.Format(@"select id, case FREQUENCY when  '天数' then CAST(VALUE as int)*24 * 60
													    when '小时'  then CAST(VALUE as int) * 60  
														else CAST(VALUE as int)  end as 'VALUE'
                                                        from [dbo].[Business]   where CODE='{0}';", Name);
                DataTable infoDt = sql.GetDataTable(CommandType.Text, sqlText, null);
                if (infoDt != null && infoDt.Rows.Count > 0)
                {
                    dic.Add("id", infoDt.Rows[0]["id"].ToString());
                    dic.Add("value", infoDt.Rows[0]["VALUE"].ToString());
                }
                return dic;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        /// <summary>
        ///详细日志
        /// </summary>
        /// <param name="logdetails"></param>
        public static void WriteDetailLog(List<LogDetail> logdetails)
        {
            try
            {
                SQLHelper sql = new SQLHelper("DefaultConnection");
                string strSql = String.Empty;
                string allSqlTxt = String.Empty;
           
                if (logdetails.Count > 0)
                {
                    allSqlTxt = string.Format("DELETE FROM [dbo].[Logdetail]  WHERE busId='{0}';", logdetails[0].busId);
                    foreach (LogDetail detail in logdetails)
                    {
                        strSql = @"INSERT INTO [dbo].[Logdetail]
                                               ([busId]
                                               ,[code]
                                               ,[date]
                                               ,[url]
                                               ,[postData]
                                               ,[postType]
                                               ,[returnData]
                                               ,[result]
                                               ,[description]
                                               ,[mark])
                                         VALUES
                                               ('{0}','{1}' ,'{2}','{3}','{4}','{5}','{6}' ,'{7}','{8}','{9}');";
                        strSql = string.Format(strSql, detail.busId, detail.code, detail.date, detail.url, detail.postData, detail.postType, detail.returnData, detail.result, detail.description, detail.mark);
                        allSqlTxt += strSql;
                    }
                    sql.ExecuteTrans(allSqlTxt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void WriteCountLog(CountDto log)
        {
            try
            {
                SQLHelper sql = new SQLHelper("DefaultConnection");
                string strSql = string.Format(@"select id from [dbo].[Count]  WHERE BusId='{0}'", log.BusId);
                string id = sql.ExecuteReader(strSql);
                if (!string.IsNullOrEmpty(id))
                {
                    strSql = string.Format(@"update [dbo].[Count] 
                                                                set sucsum='{0}',failsum='{1}',lattime='{2}',
                                                                succount=succount+{0},failcount=failcount+{1} 
                                                                 where BusId='{3}'", log.sucsum, log.failsum, log.latTime, log.BusId);
                    bool flag = sql.ExcuteNonQuery(CommandType.Text, strSql, null);
                }
                else
                {
                    strSql = @"INSERT INTO [dbo].[Count]
                                                   ([BusId]
                                                   ,[sucsum]
                                                   ,[failsum]
                                                   ,[lattime]
                                                   ,[succount]
                                                   ,[failcount])
                                             VALUES
                                               (@BusId
                                               ,@sucsum
                                               ,@failsum
                                               ,@lattime
                                               ,@succount
                                               ,@failcount)";
                    SqlParameter[] parm = {
                                      new SqlParameter("@BusId",log.BusId),
                                      new SqlParameter("@sucsum",log.sucsum),
                                      new SqlParameter("@failsum",log.failsum),
                                      new SqlParameter("@lattime",log.latTime),
                                      new SqlParameter("@succount",log.sucsum),
                                      new SqlParameter("@failcount",log.failsum)
                                  };
                    bool flag = sql.ExcuteNonQuery(CommandType.Text, strSql, parm);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
