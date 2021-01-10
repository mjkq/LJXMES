using DataHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 获取ERP数据
    /// </summary>
    public class GetERPData
    {

        /// <summary>
        /// 获取供应商存货价格
        /// </summary>
        /// <param name="code">供应商编码</param>
        /// <returns></returns>
        public static DataTable GetVendorPrice(string code) {
            try
            {

                SQLHelper dbERP = new SQLHelper("Tplusconn");
                string sqlText = string.Format(@"SELECT ISNULL(A.AgreementPrice,'0') agreementPrice,C.code cInvCode,d.code Vencode FROM AA_VendorInventoryPriceDetail A 
                                                                            LEFT JOIN AA_VendorInventoryPrice B ON A.IdVendorInventoryPrice=B.id
                                                                            left join AA_Inventory C ON B.idinventory=C.id
                                                                          LEFT JOIN AA_Partner D ON B.idvendor=D.id
                                                                          where GETDATE() between A.EffectiveStartDate and A.EffectiveEndDate
                                                                            AND D.code='{0}'", code);
                return dbERP.GetDataTable(CommandType.Text, sqlText, null);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取客户存货价格
        /// </summary>
        /// <param name="code">供应商编码</param>
        /// <returns></returns>
        public static DataTable GetCustomerPrice(string code)
        {
            try
            {

                SQLHelper dbERP = new SQLHelper("SZU8Conn");
                string sqlText = string.Format(@"SELECT cinvcode,A.ccuscode,
                                                                        fnormprice*(CASE CustomerKCode 
                                                                        WHEN 001 THEN 0.9
                                                                        WHEN 002 THEN 0.8
                                                                        WHEN 003 THEN 0.7
                                                                        WHEN 004 THEN 0.6 
                                                                        ELSE 1 END) AS fnormprice   FROM ex_inventorypricedetail A 
                                                                        LEFT JOIN Customer  B ON A.ccccode=B.cCCCode
                                                                        WHERE  A.ccccode='{0}'", code);
                return dbERP.GetDataTable(CommandType.Text, sqlText, null);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static string GetMaxCode(string rule,string database,string table,string feild= "cCode") {
            try
            {
                string connName = string.Empty;
                switch (database)
                {

                    case "AH":
                        {
                            connName = "AHU8Conn";
                        }
                        break;
                    case "SZ":
                        {
                            connName = "SZU8Conn";
                        }
                        break;
                }
                SQLHelper dbERP = new SQLHelper(connName);
                string sqlText = string.Format(@"SELECT MAX({2})  FROM {0} WHERE {2} LIKE '{1}%' ", table, rule,feild);
                DataTable dt =dbERP.GetDataTable(CommandType.Text, sqlText, null);
                string maxcode = string.Empty;
                if (dt != null && dt.Rows.Count > 0)
                {
                    maxcode = dt.Rows[0][0].ToString();
                }

                if (string.IsNullOrWhiteSpace(maxcode))
                {
                    maxcode = rule + "0001";
                }
                else {
                    int newIndex= Convert.ToInt32(maxcode.Substring(maxcode.Length - 4, 4))+1;
                    maxcode = rule + newIndex.ToString().PadLeft(4, '0');
                }
                return maxcode;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 判断U8是否存在人员
        /// </summary>
        /// <param name="database"></param>
        /// <param name="personCode"></param>
        /// <returns></returns>

        public static bool IsExistsPerson(string database,string personCode) {
            bool isExists = true;
            try
            {
                string connName = string.Empty;
                switch (database)
                {

                    case "AH":
                        {
                            connName = "AHU8Conn";
                        }
                        break;
                    case "SZ":
                        {
                            connName = "SZU8Conn";
                        }
                        break;
                }
                SQLHelper dbERP = new SQLHelper(connName);
                string sqlText = string.Format("SELECT cPersonName  FROM Person where cPersonCode='{0}'", personCode);
                string cPersonName = dbERP.ExecuteReader(sqlText);
                if (!string.IsNullOrWhiteSpace(cPersonName))
                {
                    isExists = true;
                }
                else {
                    isExists = false;
                }
                return isExists;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
