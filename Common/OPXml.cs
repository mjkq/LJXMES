using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class OPXml
    {
        /// <summary>
        /// 修改客户档案
        /// </summary>
        /// <param name="dt">待同步数据</param>
        /// <param name="url"></param>
        public static void UpdateCustomerXML(DataRow[] drs, string url, string operate, string sender)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode root = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement rootxe = (XmlElement)root;
            rootxe.SetAttribute("proc", operate);
            rootxe.SetAttribute("sender", sender);
            XmlNodeList root_node_in = xmlDoc.SelectSingleNode("ufinterface").ChildNodes;//获取customer节点的所有子节点
            XmlNode xml_in = root_node_in.Item(0);
            //XmlElement xe_child = (XmlElement)xml_in;
            //删除结点：
            XmlNodeList xnl = xmlDoc.SelectSingleNode("ufinterface").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement xe = (XmlElement)xnl.Item(i);
                root.RemoveChild(xe);
                i = i - 1;
            }
            foreach (DataRow dr in drs)
            {
                XmlElement xe_child = (XmlElement)xml_in.Clone();
                XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                foreach (XmlNode xn in nls1)//遍历所有子节点 
                {
                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                    switch (xe.Name)
                    {
                        case "code":
                            xe.InnerText = dr["code"].ToString();
                            break;
                        case "name":
                            xe.InnerText = dr["name"].ToString();
                            break;
                        case "abbrname":
                            xe.InnerText = dr["partnerAbbName"].ToString();
                            break;
                        case "seed_date":
                            xe.InnerText = Convert.ToDateTime(dr["createdTime"]).ToShortDateString();
                            break;
                        case "domain_code":
                            xe.InnerText = dr["domain_code"].ToString();
                            break;
                        case "sort_code":
                            xe.InnerText = dr["sort_code"].ToString(); ; //默认客户分类编码
                            break;
                        case "ccusmngtypecode":
                            xe.InnerText = "999";
                            break;
                        case "ccusexch_name":
                            xe.InnerText = "人民币";//默认货币码为人民币
                            break;
                        case "Phone":
                            xe.InnerText = dr["TelephoneNo"].ToString();
                            break;
                        case "Fax":
                            xe.InnerText = dr["Fax"].ToString();
                            break;
                        case "mobile":
                            xe.InnerText = dr["mobilePhone"].ToString();
                            break;
                        case "ModifyPerson":
                            xe.InnerText = dr["updatedBy"].ToString();  //t+ 变更人
                            break;
                        case "CreatePerson":
                            xe.InnerText = "demo";  //t+ 变更人
                            break;
                        case "ModifyDate":
                            {
                                if (DBNull.Value.Equals(dr["updated"]))
                                {
                                    xe.InnerText = null;
                                }
                                else
                                {
                                    xe.InnerText = Convert.ToDateTime(dr["updated"]).ToShortDateString();   //变更日期  
                                }

                            }
                            break;
                        case "address":
                            xe.InnerText = dr["customeraddressphone"].ToString();
                            break;
                        case "bcusdomestic":
                            xe.InnerText = "1";
                            break;
                        case "bOnGPinStore":
                            xe.InnerText = "0";
                            break;
                        case "contact":
                            xe.InnerText = dr["Contact"].ToString();
                            break;
                        case "self_define2":
                            xe.InnerText = dr["priuserdefnvc1"].ToString();
                            break;
                        default:
                            break;

                    }
                }
                root.AppendChild(xe_child);
            }
            xmlDoc.Save(url);
        }



        
        /// <summary>
        /// 修改供应商档案
        /// </summary>
        /// <param name="dt">待同步数据</param>
        /// <param name="url"></param>
        public static void UpdateVnedorXML(DataRow[] drs, string url, string operate, string sender)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode root = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement rootxe = (XmlElement)root;
            rootxe.SetAttribute("proc", operate);
            rootxe.SetAttribute("sender", sender);
            XmlNodeList root_node_in = xmlDoc.SelectSingleNode("ufinterface").ChildNodes;//获取customer节点的所有子节点
            XmlNode xml_in = root_node_in.Item(0);
            //XmlElement xe_child = (XmlElement)xml_in;
            //删除结点：
            XmlNodeList xnl = xmlDoc.SelectSingleNode("ufinterface").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement xe = (XmlElement)xnl.Item(i);
                root.RemoveChild(xe);
                i = i - 1;
            }
            foreach (DataRow dr in drs)
            {
                XmlElement xe_child = (XmlElement)xml_in.Clone();
                XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                foreach (XmlNode xn in nls1)//遍历所有子节点 
                {
                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                    switch (xe.Name)
                    {
                        case "code":
                            xe.InnerText = dr["code"].ToString();
                            break;
                        case "name":
                            xe.InnerText = dr["name"].ToString();
                            break;
                        case "abbrname":
                            xe.InnerText = dr["partnerAbbName"].ToString();
                            break;
                        case "seed_date":
                            xe.InnerText = Convert.ToDateTime(dr["createdTime"]).ToShortDateString();
                            break;
                        case "domain_code":
                            xe.InnerText = dr["domain_code"].ToString();
                            break;
                        case "sort_code":
                            xe.InnerText = dr["sort_code"].ToString(); ; //默认供应商分类编码
                            break;
                        case "head_corp_code":
                            xe.InnerText =  dr["code"].ToString();
                            break;
                        case "cvenexch_name":
                            xe.InnerText = "人民币";//默认货币码为人民币
                            break;
                        case "phone":
                            xe.InnerText = dr["TelephoneNo"].ToString();
                            break;
                        case "fax":
                            xe.InnerText = dr["Fax"].ToString();
                            break;
                        case "mobile":
                            xe.InnerText = dr["mobilePhone"].ToString();
                            break;
                        case "ModifyPerson":
                            xe.InnerText = dr["updatedBy"].ToString();  //t+ 变更人
                            break;
                        case "CreatePerson":
                            xe.InnerText = "demo";  //t+ 变更人
                            break;
                        case "ModifyDate":
                            {
                                if (DBNull.Value.Equals(dr["updated"]))
                                {
                                    xe.InnerText = null;
                                }
                                else
                                {
                                    xe.InnerText = Convert.ToDateTime(dr["updated"]).ToShortDateString();   //变更日期  
                                }

                            }
                            break;
                        case "address":
                            xe.InnerText = dr["customeraddressphone"].ToString();
                               break;
                        case "contact":
                            xe.InnerText = dr["Contact"].ToString();
                            break;
                        case "self_define2":
                            xe.InnerText = dr["priuserdefnvc1"].ToString();
                            break;
                        default:
                            break;

                    }
                }
                root.AppendChild(xe_child);
            }
            xmlDoc.Save(url);
        }

          /// <summary>
        /// 修改存货档案
        /// </summary>
        /// <param name="dt">待同步数据</param>
        /// <param name="url"></param>
        public static void UpdateInventoryXML(DataRow[] drs, string url, string operate, string sender)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);

            XmlNode root = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement rootxe = (XmlElement)root;
            rootxe.SetAttribute("proc", operate);
            rootxe.SetAttribute("sender", sender);
            XmlNodeList root_node_in = xmlDoc.SelectNodes("ufinterface/inventory");//获取U8Inventory节点的所有子节点
            XmlNode xml_in = root_node_in.Item(0);
            //删除结点：
            XmlNodeList xnl = xmlDoc.SelectSingleNode("ufinterface").ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement xe = (XmlElement)xnl.Item(i);
                root.RemoveChild(xe);
                i = i - 1;
            }

            foreach (DataRow dr in drs)
            {
                XmlElement xe_child = (XmlElement)xml_in.Clone();
                XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                XmlNode xmlhead = xe_child.SelectSingleNode("header");
                foreach (XmlNode xn in xmlhead.ChildNodes)//遍历所有子节点 
                {

                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                    switch (xe.Name)
                    {
                        case "code":
                            xe.InnerText = dr["code"].ToString();
                            break;
                        case "name":
                            xe.InnerText = dr["name"].ToString();
                            break;
                        case "specs":
                            xe.InnerText = dr["specification"].ToString();
                            break;
                        case "sort_code":
                            xe.InnerText = dr["inventoryclass"].ToString();
                            break;
                        case "main_measure":
                            xe.InnerText = dr["unit"].ToString();
                            break;
                        //case "inv_position":
                        //    xe.InnerText = dr["idinvlocation"].ToString();  //货位编码
                        //    break;
                        //sale_flag 是否内销
                        //case "title":
                        //    xe.InnerText = dr["title"].ToString();
                        //    break;
                        case "purchase_flag":
                            xe.InnerText = dr["isPurchase"].ToString();
                            break;
                        case "selfmake_flag":
                            xe.InnerText = dr["isMadeSelf"].ToString();
                            break;
                        case "prod_consu_flag":
                            xe.InnerText = dr["isMaterial"].ToString();
                            break;
                        case "in_making_flag":
                            xe.InnerText = "0";
                            break;
                        case "tax_serv_flag":
                            xe.InnerText = "0";
                            break;
                        case "suit_flag":
                            xe.InnerText = "0";
                            break;
                        case "qlty_guarantee_flag":
                            xe.InnerText = "0";
                            break;
                        case "batch_flag":
                            xe.InnerText = "0";
                            break;
                        case "entrust_flag":
                            xe.InnerText = "0";
                            break;
                        case "backlog_flag":
                            xe.InnerText = "0";
                            break;
                        case "free_item1":
                            xe.InnerText = "0";
                            break;
                        case "free_item2":
                            xe.InnerText = "0";
                            break;
                        case "btrack":
                            xe.InnerText = "0";
                            break;
                        case "bserial":
                            xe.InnerText = "0";
                            break;
                        case "bbarcode":
                            xe.InnerText = "0";
                            break;
                        case "unit_weight":
                            xe.InnerText = dr["IsWeigh"].ToString();
                            break;
                        case "CreatePerson":
                            xe.InnerText = dr["Creater"].ToString();
                            break;
                        case "ModifyPerson":
                            xe.InnerText = dr["updatedBy"].ToString();
                            break;
                        case "ModifyDate":
                            {
                                if (DBNull.Value.Equals(dr["updated"]))
                                {
                                    xe.InnerText = null;
                                }
                                else
                                {
                                    xe.InnerText = Convert.ToDateTime(dr["updated"]).ToShortDateString();
                                }
                            }
                            break;
                        case "self_define1":
                            xe.InnerText = dr["priuserdefnvc1"].ToString();
                            break;
                        case "self_define2":
                            xe.InnerText = dr["priuserdefnvc2"].ToString();
                            break;
                        case "pricetype":
                            xe.InnerText = "全月平均法";
                            break;
                        case "unitgroup_type":
                            xe.InnerText = "1";
                            break;
                        case "unitgroup_code":
                            xe.InnerText = dr["Gcode"].ToString();
                            break;
                        case "unitgroup_name":
                            xe.InnerText = dr["Gname"].ToString();
                            break;
                        case "puunit_code":
                            xe.InnerText = dr["puunit_code"].ToString();
                            break;
                        case "saunit_code":
                            xe.InnerText = dr["saunit_code"].ToString();
                            break;
                        case "stunit_code":
                            xe.InnerText = dr["stunit_code"].ToString();
                            break;
                        case "caunit_code":
                            xe.InnerText = dr["caunit_code"].ToString();
                            break;
                        case "cshopunit":
                            xe.InnerText = dr["cshopunit"].ToString();
                            break;
                        case "cProductUnit":
                            xe.InnerText = dr["cProductUnit"].ToString();
                            break;
                        default:
                            break;

                    }
                }
                root.AppendChild(xe_child);
            }
            xmlDoc.Save(url);
        }

        /// <summary>
        /// 销售订单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdateSaleOrderXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender)
        {
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList saleorders = xmlDoc.SelectNodes("ufinterface/saleorder");
            XmlNode saleorder = saleorders[0].CloneNode(true);
            //删除结点saleorder：
            for (int i = saleorders.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = saleorders[i];
                    xmln.ParentNode.RemoveChild(xmln);
            }

            XmlNode saleorderCopy = saleorder.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = saleorderCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                 
                    case "saletype": //销售类型编码
                        xe.InnerText = "01";    //01采购入库单
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                        break;
                    case "code":
                        xe.InnerText = drhead["code"].ToString();
                        break;
                    case "custcode":
                        xe.InnerText = drhead["custcode"].ToString();        //往来单位编码
                        break;
                    case "deptcode"://部门编码
                        xe.InnerText = drhead["deptcode"].ToString();
                        break;
                    case "personcode"://职员编号
                        xe.InnerText = drhead["personcode"].ToString();
                        break;
                    case "maker":
                        xe.InnerText = "demo";
                        break;
                    case "currency":
                        xe.InnerText = "人民币";
                        break;
                    case "currencyrate":
                        xe.InnerText = "1";
                        break;
                    case "taxrate"://税率
                        xe.InnerText = String.Empty;
                        break;
                    case "status":
                        xe.InnerText = "1";
                        break;
                    case "businesstype":
                        xe.InnerText = "普通销售";
                        break;
                    case "disflag":
                        xe.InnerText = "0";  //全部结算完毕标志bit
                        break;
                    case "cusname":
                        xe.InnerText = drhead["cusname"].ToString();
                        break;
                    case "arnest":
                        xe.InnerText = drhead["arnest"].ToString();
                        break;
                    case "memo":
                        xe.InnerText = drhead["memo"].ToString();
                        break;
                    case "define1":
                        xe.InnerText = drhead["define1"].ToString();
                        break;
                    case "define5":
                        xe.InnerText = drhead["define5"].ToString();
                        break;
                    case "define7":
                        xe.InnerText = drhead["define7"].ToString();
                        break;
                    case "define8":
                        xe.InnerText = drhead["define8"].ToString();
                        break;
                    case "define9":
                        xe.InnerText = drhead["define9"].ToString();
                        break;
                    case "define10":
                        xe.InnerText = drhead["define10"].ToString();
                        break;
                    case "define11":
                        xe.InnerText = drhead["define11"].ToString();
                        break;
                    case "define12":
                        xe.InnerText = drhead["define12"].ToString();
                        break;
                    case "define13":
                        xe.InnerText = drhead["define13"].ToString();
                        break;
                    case "define14":
                        xe.InnerText = drhead["define14"].ToString();
                        break;
                    case "define15":
                        xe.InnerText = drhead["define15"].ToString();
                        break;
                    default:
                        break;
                }
            }

            XmlNode body = saleorderCopy.SelectSingleNode("body");
            XmlNodeList root_node_in = body.ChildNodes;
            XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
            //删除结点body子节点：
            for (int i = root_node_in.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = root_node_in[i];
                xmln.ParentNode.RemoveChild(xmln);
            }

            if (dtBodys.Length > 0) {
                foreach (DataRow drbody in dtBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                            case "inventorycode":
                                xe.InnerText = drbody["inventorycode"].ToString();
                                break;
                            case "preparedate":
                                xe.InnerText = drbody["deliveryDate"] != null ? Convert.ToDateTime(drbody["deliveryDate"]).ToShortDateString() : Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                                break;
                            case "quantity":
                                xe.InnerText = drbody["quantity"].ToString();
                                break;
                            case "num":
                                xe.InnerText = drbody["quantity2"].ToString();
                                break;
                            case "discount":
                                xe.InnerText = drbody["origDiscount"].ToString();
                                break;
                            case "natdiscount":
                                xe.InnerText = drbody["discount"].ToString();
                                break;
                            case "sum":
                                xe.InnerText = drbody["origTaxAmount"].ToString();
                                break;
                            case "natsum":
                                xe.InnerText = drbody["taxAmount"].ToString();
                                break;
                            case "unitprice":
                                xe.InnerText = drbody["price"].ToString();
                                break;
                            case "natunitprice":
                                xe.InnerText = drbody["price"].ToString();
                                break;
                            case "money":
                                xe.InnerText = drbody["amount"].ToString();
                                break;
                            case "natmoney":
                                xe.InnerText = drbody["amount"].ToString();
                                break;
                            case "tax":
                                xe.InnerText = drbody["tax"].ToString();
                                break;
                            case "taxrate":
                                xe.InnerText = drbody["taxRate"].ToString();
                                break;
                            case "nattax":
                                xe.InnerText = drbody["tax"].ToString();
                                break;
                            case "dpremodate":
                                xe.InnerText = drbody["deliveryDate"]!=null?Convert.ToDateTime(drbody["deliveryDate"]).ToShortDateString():Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                                break;
                            case "memo":
                                xe.InnerText = drbody["DetailMemo"].ToString();
                                break;
                            case "taxunitprice":
                                xe.InnerText = drbody["taxPrice"].ToString();
                                break;
                            case "quotedprice":
                                xe.InnerText = drbody["taxPrice"].ToString();
                                break;
                            case "discountrate":
                                xe.InnerText = drbody["discountRate"].ToString();
                                break;
                            case "discountrate2":
                                xe.InnerText ="100";
                                break;
                            case "demandtype":
                                xe.InnerText = "5";
                                break;
                            case "bsaleprice":
                                xe.InnerText = "1";
                                break;
                            case "bgift":
                                xe.InnerText = "0";
                                break;
                            case "dreleasedate":
                                xe.InnerText =drbody["deliveryDate"]!=null?Convert.ToDateTime(drbody["deliveryDate"]).ToShortDateString():Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                                break;
                            case "dpredate":
                                xe.InnerText = drbody["deliveryDate"]!=null?Convert.ToDateTime(drbody["deliveryDate"]).ToShortDateString():Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                                break;
                            case "assitantunit":
                                xe.InnerText = drbody["subUnit"].ToString();
                                break;
                            case "assitantunitname":
                                xe.InnerText = drbody["subUnitName"].ToString();
                                break;
                            case "unitrate":
                                xe.InnerText = drbody["changeRate"].ToString();
                                break;
                            case "unitcode":
                                xe.InnerText = drbody["uint"].ToString();
                                break;
                            default:
                                break;
                               

                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(saleorderCopy);
                xmlDoc.Save(url);
            }

        }


         /// <summary>
        /// 发货单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdateConsignmentXML(DataRow drHead, string url, string operate, DataRow[] drBodys, string sender,int returnflag=0)
        {
            string date = string.Empty;
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList consignments = xmlDoc.SelectNodes("ufinterface/consignment");
            XmlNode consignment = consignments[0].CloneNode(true);
            //删除结点storein：
            for (int i = consignments.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = consignments[i];
                xmln.ParentNode.RemoveChild(xmln);
            }


            XmlNode storeinCopy = consignment.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = storeinCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                 
                    case "code":
                        xe.InnerText = drHead["code"].ToString();
                        break;
                    case "vouchertype":
                        xe.InnerText = "05";
                        break;
                    case "saletype":
                        xe.InnerText = "01";//要给予销售类型编码
                        break;
                    case "warehouse":
                        {
                           xe.InnerText = drHead["warehouse"].ToString();//仓库编码
                        }
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drHead["voucherdate"]).ToString("yyyy-MM-dd");
                        break;
                    case "deptcode":
                        {

                            xe.InnerText = drHead["deptcode"].ToString();    //t+部门不是必填项 U8 必填 t+为空默认为U8采购部
                        }
                        break;
                    case "personcode":
                        xe.InnerText = drHead["personCode"].ToString();
                        break;
                    case "custcode":
                        xe.InnerText = drHead["CstCode"].ToString();
                        break;
                    //case "address":
                    //    xe.InnerText = drHead["address"].ToString();
                    //    break;
                    case "currency_rate":
                        xe.InnerText = "1";
                        break;
                    case "currency_name":
                        xe.InnerText = "人民币";
                        break;
                    case "beginflag":
                        xe.InnerText = "0";  //期初标志
                        break;
                    case "returnflag":
                        xe.InnerText = returnflag.ToString();   //退货标志
                        break;
                    case "balanceall":
                        xe.InnerText = "1";  //全部结算完毕标志bit
                        break;
                    case "remark":
                        xe.InnerText = drHead["memo"].ToString();
                        break;
                    case "maker":
                        xe.InnerText = drHead["maker"].ToString();
                        break;
                    case "sale_cons_flag":
                        xe.InnerText = "0";   //1：先开票；0：先发货
                        break;
                    case "operation_type":
                        xe.InnerText = "普通销售";    //业务类型（普通/委托代销/分期收款)
                        break;
                    case "define1":
                        xe.InnerText = drHead["define1"].ToString();
                        break;
                    case "define2":
                        xe.InnerText = drHead["define2"].ToString();
                        break;
                    case "define3":
                        xe.InnerText = drHead["define3"].ToString();
                        break;
                    case "define4":
                        xe.InnerText =string.IsNullOrWhiteSpace(drHead["define4"].ToString())?string.Empty: Convert.ToDateTime(drHead["define4"]).ToString("yyyy-MM-dd");
                        break;
                    case "define5":
                        xe.InnerText = drHead["define5"].ToString();
                        break;
                    case "define8":
                        xe.InnerText = drHead["define8"].ToString();
                        break;
                    case "define9":
                        xe.InnerText = drHead["define9"].ToString();
                        break;
                    case "define10":
                        xe.InnerText = drHead["define10"].ToString();
                        break;
                    case "define12":
                        xe.InnerText = drHead["define12"].ToString();
                        break;
                    case "define13":
                        xe.InnerText = drHead["define13"].ToString();
                        break;
                    case "define14":
                        xe.InnerText = drHead["define14"].ToString();
                        break;
                    default:
                        break;
                        //现存数量(fstockquan) ,可用数量(fcanusequan)无字段
                }
            }

            if (drBodys.Length > 0)
            {
                XmlNode body = storeinCopy.SelectSingleNode("body");
                XmlNodeList root_node_in = body.ChildNodes;
                XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
                //删除结点body子节点：
                for (int i = root_node_in.Count - 1; i >= 0; i--)
                {
                    XmlNode xmln = root_node_in[i];
                    xmln.ParentNode.RemoveChild(xmln);
                }
                foreach (DataRow drbody in drBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                            //case "headID":
                            //    xe.InnerText = drbody["idSaleDeliveryDTO"].ToString();
                            //    break;
                            //存货名称(cinvname)，UDI(cinvdefine10)，规格(cinvstd)，单位(cinvm_unit)不存在
                            case "warehouse_code":
                                {
                                    xe.InnerText = drbody["WarhouseCode"].ToString();//仓库编码
                                }
                                break;
                            case "inventory_code":
                                xe.InnerText = drbody["inventorycode"].ToString();
                                break;
                            //case "unitquantity":
                            //    xe.InnerText = drbody["quantity"].ToString();
                            //    break;
                            case "quantity":
                                xe.InnerText = drbody["quantity"].ToString();
                                break;
                            case "num":
                                {
                                    if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                    {
                                        xe.InnerText = drbody["quantity2"].ToString();     //件数
                                    }
                                    else
                                    {
                                        xe.InnerText = string.Empty;
                                    }
                                }
                                break;
                            case "unitrate":
                                xe.InnerText = drbody["changeRate"].ToString();
                                break;
                            case "ccomunitcode":
                                xe.InnerText = drbody["uint"].ToString();
                                break;
                            case "cinvm_unit":
                                xe.InnerText = drbody["name"].ToString();
                                break;
                            case "cinva_unit":
                                xe.InnerText = drbody["subUnitName"].ToString();
                                break;
                            case "quotedprice":
                                //xe.InnerText = "";
                               xe.InnerText = drbody["taxPrice"].ToString();
                                break;
                            case "sum":
                                xe.InnerText = drbody["taxmount"].ToString();
                                break;
                            case "backquantity":
                                xe.InnerText = drbody["quantity"].ToString();
                                break;
                            case "backnum":
                                {
                                    if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                    {
                                        xe.InnerText = drbody["quantity2"].ToString();     //件数
                                    }
                                    else
                                    {
                                        xe.InnerText = string.Empty;
                                    }
                                }
                                break;
                            case "unit_code":
                                xe.InnerText = drbody["subUnit"].ToString();
                                break;
                            case "price":   //无税单价
                                xe.InnerText = drbody["price"].ToString();
                                break;
                            case "taxprice":  //含税单价
                                xe.InnerText = drbody["taxPrice"].ToString();
                                break;
                            case "tax":
                                xe.InnerText = drbody["tax"].ToString();
                                break;
                            case "money":
                                xe.InnerText = drbody["amount"].ToString();
                                break;
                            case "taxrate":
                                xe.InnerText = drbody["taxRate"].ToString();
                                break;
                            case "bQANeedCheck":
                                xe.InnerText = "1"; //是否质检 bit 非空  1、0
                                break;
                            case "bQAUrgency":
                                xe.InnerText = "1";// 是否急料
                                break;
                            case "bsaleprice":
                                xe.InnerText = "1"; //报价是否含税
                                break;
                            case "backflag":
                                xe.InnerText = "正常"; //1：退补发票	tinyint 	可空	非空时为1和0
                                break;
                            case "bgift":
                                xe.InnerText = "0";
                                break;
                            case "define24":
                                xe.InnerText = drbody["define24"].ToString();
                                break;
                            case "define25":
                                xe.InnerText = drbody["define25"].ToString();
                                break;
                            case "define28":
                                xe.InnerText = drbody["define28"].ToString();
                                break;
                            case "define29":
                                xe.InnerText = drbody["define29"].ToString();
                                break;
                            case "define30":
                                xe.InnerText = drbody["define30"].ToString();
                                break;
                            case "define31":
                                xe.InnerText = drbody["define31"].ToString();
                                break;
                            case "define33":
                                xe.InnerText = drbody["define33"].ToString();
                                break;
                            case "natprice":
                                xe.InnerText = drbody["price"].ToString();
                                break;
                            case "natmoney":
                                xe.InnerText = drbody["amount"].ToString();
                                break;
                            case "nattax":
                                xe.InnerText = drbody["tax"].ToString();
                                break;
                            case "natsum":
                                xe.InnerText = drbody["taxmount"].ToString();
                                break;
                            case "discount1":
                                xe.InnerText = "100";
                                break;
                            case "discount2":
                                xe.InnerText = "100";
                                break;
                            case "discount":
                                xe.InnerText = "0";
                                break;
                            case "natdiscount":
                                xe.InnerText = "0";
                                break;
                            case "retail_price":
                                xe.InnerText = "0";
                                break;
                            case "retail_money":
                                xe.InnerText = "0";
                                break;
                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(storeinCopy);
            }
            xmlDoc.Save(url);

        }


         /// <summary>
        /// 采购入库单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdateStoreinlXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender,int returnflag=1)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            try
            {
                XmlNodeList storeins = xmlDoc.SelectNodes("ufinterface/storein");
                XmlNode storein = storeins[0].CloneNode(true);
                //删除结点storein：
                for (int i = storeins.Count - 1; i >= 0; i--)
                {
                    XmlNode xmln = storeins[i];
                    xmln.ParentNode.RemoveChild(xmln);
                }

                XmlNode storeinCopy = storein.CloneNode(true);
                //修改节点
                XmlNodeList nodeList = storeinCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
                foreach (XmlNode xn in nodeList)//遍历所有子节点 
                {
                    XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                    switch (xe.Name)
                    {
                        case "receiveflag":    //到货标志
                            xe.InnerText = "1";
                            break;
                        case "vouchtype":
                            xe.InnerText = "01";    //单据类型	varchar	2	非空	01：采购入库单08：其他入库单09：其他出库单10：产成品入库单11：材料出库单32：销售出库单
                            break;
                        case "businesstype":
                            xe.InnerText = "普通采购";
                            break;
                        case "source":
                            xe.InnerText = "库存";
                            break;
                        case "receivecode":
                            xe.InnerText = "11";//入库类别编码
                            break;
                        case "purchasetypecode":
                            xe.InnerText = "01";//采购类型编码
                            break;
                        case "saletypecode":
                            xe.InnerText = "";//销售类型编码
                            break;
                        case "warehousecode":
                            {
                                xe.InnerText = drhead["warhousecode"].ToString();
                            }
                            break;
                        case "date":
                            xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                            break;
                        case "code":
                            xe.InnerText = drhead["code"].ToString();
                            break;
                        case "vendorcode":
                            xe.InnerText = drhead["vendor"].ToString();        //往来单位编码
                            break;
                        case "departmentcode":
                            xe.InnerText = drhead["deptcode"].ToString();        //部门编码
                            break;
                        case "maker":
                            xe.InnerText = drhead["maker"].ToString();
                            break;
                        case "exchname":
                            xe.InnerText = "人民币";
                            break;
                        case "exchrate":
                            xe.InnerText = "1";
                            break;
                        case "bomfirst":
                            xe.InnerText = "0";
                            break;
                        case "bpufirst":
                            xe.InnerText = "0";
                            break;
                        case "bcredit":
                            xe.InnerText = "0";  //全部结算完毕标志bit
                            break;
                        case "discounttaxtype":
                            xe.InnerText = "0";
                            break;
                        case "memory":
                            xe.InnerText = drhead["memo"].ToString();
                            break;
                        case "define12":
                            xe.InnerText = drhead["define12"].ToString();
                            break;
                        case "define11":
                            xe.InnerText = "";
                            break;
                        //case "closer":
                        //    xe.InnerText = dr["memo"].ToString();
                        //    break;
                        //case "verifier":
                        //    xe.InnerText = dr["审核人(cverifier)"].ToString();
                        //    break;
                        default:
                            break;
                    }
                }

                XmlNode body = storeinCopy.SelectSingleNode("body");
                XmlNodeList root_node_in = body.ChildNodes;
                XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
                //删除结点body子节点：
                for (int i = root_node_in.Count - 1; i >= 0; i--)
                {
                    XmlNode xmln = root_node_in[i];
                    xmln.ParentNode.RemoveChild(xmln);
                }

                if (dtBodys.Length > 0)
                {
                    foreach (DataRow drbody in dtBodys)
                    {
                        //XmlDocument doc_in = new XmlDocument();
                        //doc_in.Load(url2);
                        XmlNode entrysopy = xml_in.CloneNode(true);
                        XmlElement xe_child = (XmlElement)entrysopy;
                        XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                        foreach (XmlNode xn1 in nls1)//遍历
                        {
                            XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                            switch (xe.Name)
                            {
                                //case "autoid":
                                //    xe.InnerText = drbody["idRDRecordDTO"].ToString();
                                //    break;
                                //存货名称(cinvname)，UDI(cinvdefine10)，规格(cinvstd)，单位(cinvm_unit)不存在
                                case "inventorycode":
                                    xe.InnerText = drbody["inventorycode"].ToString();   //调拨单 子表 没有仓库id
                                    break;
                                case "invname":
                                    xe.InnerText = drbody["invname"].ToString();      //连接查询存货表
                                    break;
                                case "quantity":
                                    xe.InnerText = (Convert.ToDouble(drbody["quantity"]) * returnflag).ToString();     //数量
                                    break;
                                case "shouldquantity":
                                    xe.InnerText = string.Empty;
                                    break;
                                case "irate":
                                    xe.InnerText = drbody["changeRate"].ToString();
                                    break;
                                case "shouldnumber":
                                    xe.InnerText = string.Empty;
                                    break;
                                case "number":
                                    {
                                        if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                        {
                                            xe.InnerText = (Convert.ToDouble(drbody["quantity2"]) * returnflag).ToString();     //件数
                                        }
                                        else
                                        {
                                            xe.InnerText = string.Empty;
                                        }
                                    }
                                    break;
                                case "cmassunitname":
                                    xe.InnerText = drbody["name"].ToString(); //连接计量单位表查询
                                    break;
                                case "define32":
                                    xe.InnerText =  string.Empty;
                                    break;
                                case "define33":
                                    xe.InnerText = drbody["define12"].ToString();
                                    break;
                                case "define24":
                                    xe.InnerText = drbody["define3"].ToString();
                                    break;
                                case "define28":
                                    xe.InnerText = drbody["define7"].ToString();
                                    break;
                                case "define30":
                                    xe.InnerText = drbody["define9"].ToString();
                                    break;
                                case "define31":
                                    xe.InnerText = drbody["define10"].ToString();
                                    break;
                                case "price":
                                        xe.InnerText = drbody["price"].ToString();    //不含税单价
                                    break;
                                case "cost":
                                    xe.InnerText = (Convert.ToDouble(drbody["amount"]) * returnflag).ToString(); //数量* 单价
                                    break;
                                case "facost":
                                    xe.InnerText = drbody["price"].ToString();   //不含税单价
                                    break;
                                case "iaprice":
                                    {
                                        xe.InnerText = (Convert.ToDouble(drbody["amount"]) * returnflag).ToString(); //数量* 单价
                                    }
                                    break;
                                case "ioritaxcost":
                                    {
                                        xe.InnerText = drbody["taxPrice"].ToString();   //含税单价
                                    }

                                    break;
                                case "ioricost":
                                    {
                                        xe.InnerText = drbody["price"].ToString();   //不含税
                                    }

                                    break;
                                case "iorimoney":
                                    {
                                        xe.InnerText = (Convert.ToDouble(drbody["amount"]) * returnflag).ToString();   //不含税
                                    }

                                    break;
                                case "ioritaxprice":
                                    {
                                        xe.InnerText = (Convert.ToDouble(drbody["tax"]) * returnflag).ToString();   //税额
                                    }


                                    break;
                                case "iorisum":
                                    {
                                        xe.InnerText = (Convert.ToDouble(drbody["taxmount"]) * returnflag).ToString();   //含税金额 
                                    }

                                    break;
                                case "taxprice":
                                    {
                                        xe.InnerText = (Convert.ToDouble(drbody["tax"]) * returnflag).ToString();   //税额
                                    }

                                    break;
                                case "isum":
                                    {
                                        xe.InnerText = (Convert.ToDouble(drbody["taxmount"]) * returnflag).ToString();   //含税金额 
                                    }
                                    break;
                                case "taxrate":
                                    {
                                        xe.InnerText = drbody["taxRate"].ToString();
                                    }

                                    break;
                                case "iexpiratdatecalcu":
                                    xe.InnerText = "";
                                    break;
                                case "assitantunit":
                                    xe.InnerText = drbody["subUnit"].ToString();
                                    break;
                                case "assitantunitname":
                                    xe.InnerText = drbody["subUnitName"].ToString();
                                    break;
                                    //	关闭日期(dbclosedate) 行关闭人(cscloser)，基准交期(cinvdefine11)。预留数量(iprekeepquantity)不存在

                            }
                        }
                        body.AppendChild(xe_child);
                    }
                    ufinterface.AppendChild(storeinCopy);
                    xmlDoc.Save(url);
                }

            }
            catch (Exception EX)
            {

                throw EX;
            }

        }


         /// <summary>
        /// 采购订单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdatePurchaseOrderXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender)
        {
            string date = string.Empty;
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList purchaseorders = xmlDoc.SelectNodes("ufinterface/purchaseorder");
            XmlNode purchaseorder = purchaseorders[0].CloneNode(true);
            //删除结点purchaseorder：
            for (int i = purchaseorders.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = purchaseorders[i];
                    xmln.ParentNode.RemoveChild(xmln);
            }

            XmlNode purchaseorderCopy = purchaseorder.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = purchaseorderCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                    case "purchase_type_code":
                        xe.InnerText = "01";    //01采购入库单
                        break;
                    case "operation_type_code":
                        xe.InnerText = "普通采购";
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                        break;
                    case "code":
                        xe.InnerText = drhead["code"].ToString();
                        break;
                    case "vendorcode":
                        xe.InnerText = drhead["vendorcode"].ToString();        //往来单位编码
                        break;
                    case "deptcode":
                        xe.InnerText = drhead["deptcode"].ToString();   
                        break;
                    case "maker":
                        xe.InnerText = "demo";
                        break;
                    case "currency_name":
                        xe.InnerText = "人民币";
                        break;
                    case "currency_rate":
                        xe.InnerText = "1";
                        break;
                    case "traffic_money":
                        xe.InnerText = "0";
                        break;
                    case "bargain":
                        xe.InnerText = "0";
                        break;
                    case "remark":
                        xe.InnerText = drhead["memo"].ToString();
                        break;
                    case "personcode"://职员编号
                        xe.InnerText = drhead["personcode"].ToString();
                        break;
                    case "define10":
                        xe.InnerText = drhead["define10"].ToString();
                        break;
                    case "define6":
                        xe.InnerText = drhead["define6"].ToString();
                        break;
                    case "define3":
                        xe.InnerText = drhead["define3"].ToString();
                        break;
                    //case "define14":
                    //    xe.InnerText = drhead["define14"].ToString();
                    //    break;
                    case "define1":
                        xe.InnerText = drhead["define1"].ToString();
                        break;
                    case "idiscounttaxtype":
                        xe.InnerText = "1"; //扣税类别(0应税外加, 1应税内含)
                        break;
                    default:
                        break;
                }
            }

            XmlNode body = purchaseorderCopy.SelectSingleNode("body");
            XmlNodeList root_node_in = body.ChildNodes;
            XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
            //删除结点body子节点：
            for (int i = root_node_in.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = root_node_in[i];
                xmln.ParentNode.RemoveChild(xmln);
            }

            if (dtBodys.Length > 0) {
                foreach (DataRow drbody in dtBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                           
                            case "inventorycode":
                                xe.InnerText = drbody["inventorycode"].ToString();  
                                break;
                            case "checkflag":
                                xe.InnerText = "0";      //连接查询存货表
                                break;
                            case "quantity":
                                xe.InnerText = drbody["quantity"].ToString();  //数量
                                break;
                            case "unitcode":
                                xe.InnerText = drbody["uint"].ToString(); ;     //数量
                                break;
                            case "num":
                                xe.InnerText = drbody["quantity2"].ToString();   //数量
                                break;
                            case "quotedprice":
                                xe.InnerText = drbody["taxPrice"].ToString();  //报价
                                break;
                            case "money":
                                xe.InnerText = drbody["amount"].ToString();
                                break;
                            case "price":
                                xe.InnerText = drbody["price"].ToString();  //单价
                                break;
                            case "tax":
                                xe.InnerText = drbody["tax"].ToString(); //税额（原币）
                                break;
                            case "taxprice":
                                xe.InnerText = drbody["taxPrice"].ToString();   //单价
                                break;
                            case "sum":
                                xe.InnerText = drbody["taxmount"].ToString();   //价税合计（原币）
                                break;
                            case "discount":
                               xe.InnerText = drbody["discount"].ToString();   //不含税
                                break;
                            case "taxrate":
                               xe.InnerText = drbody["taxRate"].ToString(); ;  
                                break;
                            case "assistantunit":
                                xe.InnerText = drbody["subUnit"].ToString();
                                break;
                            case "arrivedate":
                                xe.InnerText = drbody["acceptDate"].ToString();
                                break;
                            case "define33":
                                xe.InnerText = drbody["define33"].ToString();
                                break;
                         



                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(purchaseorderCopy);
                xmlDoc.Save(url);
            }

        }


         /// <summary>
        /// 其他入库单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdateOStoreinlXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender)
        {
          
            string date = string.Empty;
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList storeins = xmlDoc.SelectNodes("ufinterface/storein");
            XmlNode storein = storeins[0].CloneNode(true);
            //删除结点storein：
            for (int i = storeins.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = storeins[i];
                    xmln.ParentNode.RemoveChild(xmln);
            }

            XmlNode storeinCopy = storein.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = storeinCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                    case "receiveflag":    //到货标志
                        xe.InnerText = "1";
                        break;
                    case "vouchtype":
                        xe.InnerText = "08";    //08：其他入库单
                        break;
                    case "businesstype":
                        xe.InnerText = "其他入库";
                        break;
                    case "source":
                        xe.InnerText = "库存";
                        break;
                    case "receivecode":
                        xe.InnerText = "19";//入库类别编码
                        break;
                    case "purchasetypecode":
                        xe.InnerText = "";//采购类型编码
                        break;
                    case "saletypecode":
                        xe.InnerText = "";//销售类型编码
                        break;
                    case "warehousecode":
                        {
                            xe.InnerText = drhead["warhousecode"].ToString();
                        }
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                        break;
                    case "code":
                        xe.InnerText = drhead["code"].ToString();
                        break;
                    case "vendorcode":
                        xe.InnerText = drhead["Vendor"].ToString();        //往来单位编码
                        break;
                    case "departmentcode":
                        xe.InnerText =  drhead["deptcode"].ToString();        //往来单位编码
                        break;
                    case "maker":
                        xe.InnerText = drhead["maker"].ToString();
                        break;
                    case "exchname":
                        xe.InnerText = "人民币";
                        break;
                    case "exchrate":
                        xe.InnerText = "1";
                        break;
                    case "bomfirst":
                        xe.InnerText = "0";
                        break;
                    case "bpufirst":
                        xe.InnerText = "0";
                        break;
                    case "bcredit":
                        xe.InnerText = "0";  //全部结算完毕标志bit
                        break;
                    case "discounttaxtype":
                        xe.InnerText = "0";
                        break;
                    case "memory":
                        xe.InnerText = drhead["memo"].ToString();
                        break;
                    case "define11":
                        xe.InnerText = "";
                        break;
                    case "define12":
                        xe.InnerText = "";
                        break;

                    default:
                        break;
                }
            }

            XmlNode body = storeinCopy.SelectSingleNode("body");
            XmlNodeList root_node_in = body.ChildNodes;
            XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
            //删除结点body子节点：
            for (int i = root_node_in.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = root_node_in[i];
                xmln.ParentNode.RemoveChild(xmln);
            }

            if (dtBodys.Length > 0) {
                foreach (DataRow drbody in dtBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                           
                            case "inventorycode":
                                xe.InnerText = drbody["Invcode"].ToString();   //调拨单 子表 没有仓库id
                                break;
                            case "invname":
                                xe.InnerText = drbody["InvName"].ToString();      //连接查询存货表
                                break;
                            case "quantity":
                                xe.InnerText = drbody["quantity"].ToString();     //数量
                                break;
                            case "shouldquantity":
                                xe.InnerText = string.Empty;
                                break;
                            case "shouldnumber":
                                 xe.InnerText = string.Empty;
                                break;
                            case "number":
                                {
                                    if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                    {
                                        xe.InnerText = drbody["quantity2"].ToString();     //件数
                                    }
                                    else
                                    {
                                        xe.InnerText = string.Empty;
                                    }
                                }
                                break;
                            case "irate":
                                xe.InnerText = xe.InnerText = drbody["changeRate"].ToString();     //数量
                                break;
                            case "cmassunitname":
                                xe.InnerText = drbody["name"].ToString(); //连接计量单位表查询
                                break;
                            case "define32":
                                xe.InnerText = string.Empty;
                                break;
                            case "define33":
                                xe.InnerText = drbody["define11"].ToString();
                                break;
                            case "define24":
                                xe.InnerText =string.Empty;
                                break;
                            case "define28":
                                xe.InnerText = string.Empty;
                                break;
                            case "define30":
                                xe.InnerText = string.Empty;
                                break;
                            case "define31":
                                xe.InnerText = string.Empty;
                                break;
                            case "price":
                                {
                                    // xe.InnerText = Convert.ToDouble(drbody["price"]).ToString();    //单价
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "cost":
                                {
                                    //xe.InnerText = Convert.ToDouble(drbody["amount"]).ToString(); //数量* 单价
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "facost":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "iaprice":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "ioritaxcost":
                                {
                                    xe.InnerText =string.Empty;
                                }
                                break;
                            case "ioricost":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "iorimoney":
                                {
                                    xe.InnerText =string.Empty;
                                }
                                break;
                            case "ioritaxprice":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "iorisum":
                                {
                                    xe.InnerText =string.Empty;
                                }
                                break;
                            case "taxprice":
                                {
                                    xe.InnerText =string.Empty;
                                }
                                break;
                            case "isum":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "taxrate":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "iexpiratdatecalcu":
                                xe.InnerText = "";
                                break;
                            case "assitantunit":
                                xe.InnerText = drbody["subUnit"].ToString();
                                break;
                            case "assitantunitname":
                                xe.InnerText = drbody["subUnitName"].ToString();
                                break;
                                //	关闭日期(dbclosedate) 行关闭人(cscloser)，基准交期(cinvdefine11)。预留数量(iprekeepquantity)不存在

                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(storeinCopy);
                xmlDoc.Save(url);
            }

        }



         /// <summary>
        /// 其他出库单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdateOStoreoutlXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender)
        {
          
            string date = string.Empty;
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList storeouts = xmlDoc.SelectNodes("ufinterface/storeout");
            XmlNode storeout = storeouts[0].CloneNode(true);
            //删除结点storeout：
            for (int i = storeouts.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = storeouts[i];
                    xmln.ParentNode.RemoveChild(xmln);
            }

            XmlNode storeoutCopy = storeout.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = storeoutCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                    case "receiveflag":    //收发标志
                        xe.InnerText = "0";
                        break;
                    case "vouchtype":
                        xe.InnerText = "09";    //09：其他出库单
                        break;
                    case "businesstype":
                        xe.InnerText = "其他出库";
                        break;
                    case "source":
                        xe.InnerText = "库存";
                        break;
                    case "warehousecode":
                        {
                            xe.InnerText = drhead["warhousecode"].ToString();
                        }
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                        break;
                    case "code":
                        xe.InnerText = drhead["code"].ToString();
                        break;
                    case "vendorcode":
                        xe.InnerText = drhead["Vendor"].ToString();        //往来单位编码
                        break;
                    case "departmentcode":
                        xe.InnerText =  drhead["deptcode"].ToString();        //部门
                        break;
                    case "maker":
                        xe.InnerText =  drhead["maker"].ToString();
                        break;
                    case "exchname":
                        xe.InnerText = "人民币";
                        break;
                    case "exchrate":
                        xe.InnerText = "1";
                        break;
                    case "bomfirst":
                        xe.InnerText = "0";
                        break;
                    case "bpufirst":
                        xe.InnerText = "0";
                        break;
                    case "bcredit":
                        xe.InnerText = "0";  //全部结算完毕标志bit
                        break;
                    case "discounttaxtype":
                        xe.InnerText = "0";
                        break;
                    case "memory":
                        xe.InnerText = drhead["memo"].ToString();
                        break;
                    case "receivecode":
                        xe.InnerText = "29";//出库类别
                        break;
                    default:
                        break;
                }
            }

            XmlNode body = storeoutCopy.SelectSingleNode("body");
            XmlNodeList root_node_in = body.ChildNodes;
            XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
            //删除结点body子节点：
            for (int i = root_node_in.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = root_node_in[i];
                xmln.ParentNode.RemoveChild(xmln);
            }

            if (dtBodys.Length > 0) {
                foreach (DataRow drbody in dtBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                           
                            case "inventorycode":
                                xe.InnerText = drbody["Invcode"].ToString();   //调拨单 子表 没有仓库id
                                break;
                            case "invname":
                                xe.InnerText = drbody["InvName"].ToString();      //连接查询存货表
                                break;
                            case "quantity":
                                xe.InnerText =Math.Round(Convert.ToDouble(drbody["quantity"]),2).ToString();     //数量
                                break;
                            case "shouldquantity":
                                xe.InnerText = string.Empty;
                                break;
                            case "shouldnumber":
                                 xe.InnerText = string.Empty;
                                break;
                            case "number":
                                {
                                    if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                    {
                                        xe.InnerText = drbody["quantity2"].ToString();     //件数
                                    }
                                    else
                                    {
                                        xe.InnerText = string.Empty;
                                    }
                                }
                                break;
                            case "cmassunitname":
                                xe.InnerText = drbody["name"].ToString(); //连接计量单位表查询
                                break;
                            case "irate":
                                xe.InnerText = drbody["changeRate"].ToString(); //连接计量单位表查询
                                break;
                            case "price":
                                {
                                   // xe.InnerText =drbody["price"].ToString();    //单价
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "cost":
                                {
                                    //xe.InnerText = drbody["amount"].ToString();    //金额
                                    xe.InnerText = string.Empty;
                                }
                               
                                break;
                            case "taxrate":
                                {
                                   xe.InnerText = drbody["taxRate"].ToString();   
                                }
                             
                                break;
                            case "taxprice":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                          
                            case "isum":
                                {
                                    xe.InnerText =  string.Empty;
                                }
                                break;
                            case "define32":
                                xe.InnerText = drbody["define11"].ToString();
                                break;
                            case "iexpiratdatecalcu":
                                xe.InnerText = "";
                                break;
                            case "assitantunit":
                                xe.InnerText = drbody["subUnit"].ToString(); 
                                break;
                            case "assitantunitname":
                                xe.InnerText = drbody["subUnitName"].ToString(); 
                                break;
                                //	关闭日期(dbclosedate) 行关闭人(cscloser)，基准交期(cinvdefine11)。预留数量(iprekeepquantity)不存在

                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(storeoutCopy);
                xmlDoc.Save(url);
            }

        }


         /// <summary>
        /// 产成品入库单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdatePStoreinlXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender)
        {
          
            string date = string.Empty;
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList storeins = xmlDoc.SelectNodes("ufinterface/storein");
            XmlNode storein = storeins[0].CloneNode(true);
            //删除结点storein：
            for (int i = storeins.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = storeins[i];
                    xmln.ParentNode.RemoveChild(xmln);
            }

            XmlNode storeinCopy = storein.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = storeinCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                    case "receiveflag":    //到货标志
                        xe.InnerText = "1";
                        break;
                    case "vouchtype":
                        xe.InnerText = "10";    //10：产成品入库单
                        break;
                    case "businesstype":
                        xe.InnerText = "成品入库";
                        break;
                    case "businesscode":
                        xe.InnerText = ""; 
                        break;
                    case "receivecode":
                        xe.InnerText = "12";//入库类别编码
                        break;
                    case "purchasetypecode":
                        xe.InnerText = "";//采购类型编码
                        break;
                    case "saletypecode":
                        xe.InnerText = "";//销售类型编码
                        break;
                    case "source":
                        xe.InnerText = "库存";
                        break;
                    case "warehousecode":
                        {
                            xe.InnerText = drhead["warhousecode"].ToString();
                        }
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                        break;
                    case "code":
                        xe.InnerText = drhead["code"].ToString();
                        break;
                    case "vendorcode":
                        xe.InnerText = drhead["Vendor"].ToString();        //往来单位编码
                        break;
                    case "departmentcode":
                        xe.InnerText =  drhead["deptcode"].ToString();        //往来单位编码
                        break;
                    case "maker":
                        xe.InnerText = drhead["maker"].ToString();
                        break;
                    case "exchname":
                        xe.InnerText = "人民币";
                        break;
                    case "exchrate":
                        xe.InnerText = "1";
                        break;
                    case "bomfirst":
                        xe.InnerText = "0";
                        break;
                    case "bpufirst":
                        xe.InnerText = "0";
                        break;
                    case "bcredit":
                        xe.InnerText = "0";  //全部结算完毕标志bit
                        break;
                    case "discounttaxtype":
                        xe.InnerText = "0";
                        break;
                    case "memory":
                        xe.InnerText = drhead["memo"].ToString();
                        break;
                    case "define11":
                        xe.InnerText = drhead["define11"].ToString();
                        break;

                    default:
                        break;
                }
            }

            XmlNode body = storeinCopy.SelectSingleNode("body");
            XmlNodeList root_node_in = body.ChildNodes;
            XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
            //删除结点body子节点：
            for (int i = root_node_in.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = root_node_in[i];
                xmln.ParentNode.RemoveChild(xmln);
            }

            if (dtBodys.Length > 0) {
                foreach (DataRow drbody in dtBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                           
                            case "inventorycode":
                                xe.InnerText = drbody["Invcode"].ToString();   //调拨单 子表 没有仓库id
                                break;
                            case "invname":
                                xe.InnerText = drbody["InvName"].ToString();      //连接查询存货表
                                break;
                            case "quantity":
                                xe.InnerText = drbody["quantity"].ToString();     //数量
                                break;
                            case "shouldquantity":
                                xe.InnerText = string.Empty;
                                break;
                            case "shouldnumber":
                                 xe.InnerText = string.Empty;
                                break;
                            case "number":
                                {
                                    if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                    {
                                        xe.InnerText = drbody["quantity2"].ToString();     //件数
                                    }
                                    else
                                    {
                                        xe.InnerText = string.Empty;
                                    }

                                }
                                break;
                            case "irate":
                                xe.InnerText = drbody["changeRate"].ToString();     //数量
                                break;
                            case "cmassunitname":
                                xe.InnerText = drbody["name"].ToString(); //连接计量单位表查询
                                break;
                            case "define32":
                                xe.InnerText = string.Empty;
                                break;
                            case "define33":
                                xe.InnerText = drbody["define11"].ToString();
                                break;
                            case "define24":
                                xe.InnerText = drbody["define3"].ToString();
                                break;
                            case "define28":
                                xe.InnerText = drbody["define7"].ToString();
                                break;
                            case "define30":
                                xe.InnerText = string.Empty;
                                break;
                            case "define31":
                                xe.InnerText = string.Empty;
                                break;
                            case "price":
                                {
                                    //  xe.InnerText = Convert.ToDouble(drbody["price"]).ToString();    //单价
                                    xe.InnerText =string.Empty;    //单价
                                }

                                break;
                            case "cost":
                                {
                                    //xe.InnerText = Convert.ToDouble(drbody["amount"]).ToString(); //数量* 单价
                                    xe.InnerText = string.Empty;    //数量* 单价
                                }
                                break;
                            case "facost":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "iaprice":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "ioritaxcost":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "ioricost":
                                {
                                    xe.InnerText =string.Empty;
                                }
                                break;
                            case "iorimoney":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "ioritaxprice":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "iorisum":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "taxprice":
                                {
                                    xe.InnerText =  string.Empty;
                                }
                                break;
                            case "isum":
                                {
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "taxrate":
                                {
                                    xe.InnerText = drbody["taxRate"].ToString();
                                }
                                break;
                            case "iexpiratdatecalcu":
                                xe.InnerText = "";
                                break;
                            case "assitantunit":
                                xe.InnerText = drbody["subUnit"].ToString();
                                break;
                            case "assitantunitname":
                                xe.InnerText = drbody["subUnitName"].ToString();
                                break;
                                //	关闭日期(dbclosedate) 行关闭人(cscloser)，基准交期(cinvdefine11)。预留数量(iprekeepquantity)不存在

                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(storeinCopy);
                xmlDoc.Save(url);
            }

        }

        /// <summary>
        /// 调拨单修改
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="url"></param>
        public static void UpdateAllocationXML(DataRow drhead, string url, string operate, DataRow[] dtBodys, string sender)
        {
          
            string date = string.Empty;
            string strcode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(url);
            XmlNode ufinterface = xmlDoc.SelectSingleNode("ufinterface");
            XmlElement ufinterfacexe = (XmlElement)ufinterface;
            ufinterfacexe.SetAttribute("proc", operate);
            ufinterfacexe.SetAttribute("sender", sender);

            XmlNodeList transvouchs = xmlDoc.SelectNodes("ufinterface/transvouch");
            XmlNode transvouch = transvouchs[0].CloneNode(true);
            //删除结点transvouch：
            for (int i = transvouchs.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = transvouchs[i];
                    xmln.ParentNode.RemoveChild(xmln);
            }

            XmlNode transvouchCopy = transvouch.CloneNode(true);
            //修改节点
            XmlNodeList nodeList = transvouchCopy.SelectSingleNode("header").ChildNodes;//获取Employees节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点 
            {
                XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型 
                switch (xe.Name)
                {
                    case "btransflag":    //收发标志
                        xe.InnerText = "0";
                        break;
                    case "idepcode":
                        xe.InnerText = drhead["indepartment"].ToString();//转入部门编码
                        break;
                    case "odepcode":
                        xe.InnerText =  drhead["department"].ToString();//转出部门编码  
                        break;
                    case "iwhcode":
                        xe.InnerText =drhead["inWarehouseCode"].ToString();//转入仓库编码  
                        break;
                    case "owhcode":
                        {
                            xe.InnerText = drhead["outWarehouseCode"].ToString();//转出仓库编码  
                        }
                        break;
                    case "date":
                        xe.InnerText = Convert.ToDateTime(drhead["voucherdate"]).ToShortDateString();
                        break;
                    case "tvcode":
                        xe.InnerText = drhead["code"].ToString();
                        break;
                    case "maker":
                        xe.InnerText = drhead["maker"].ToString();
                        break;
                    case "exchname":
                        xe.InnerText = "人民币";
                        break;
                    case "exchrate":
                        xe.InnerText = "1";
                        break;
                    case "memory":
                        xe.InnerText = drhead["memo"].ToString();
                        break;
                    case "define12":
                        {
                            xe.InnerText = drhead["define12"].ToString();
                        }
                        break;
                    default:
                        break;
                }
            }

            XmlNode body = transvouchCopy.SelectSingleNode("body");
            XmlNodeList root_node_in = body.ChildNodes;
            XmlNode xml_in = root_node_in.Item(0).CloneNode(true);
            //删除结点body子节点：
            for (int i = root_node_in.Count - 1; i >= 0; i--)
            {
                XmlNode xmln = root_node_in[i];
                xmln.ParentNode.RemoveChild(xmln);
            }

            if (dtBodys.Length > 0) {
                foreach (DataRow drbody in dtBodys)
                {
                    //XmlDocument doc_in = new XmlDocument();
                    //doc_in.Load(url2);
                    XmlNode entrysopy = xml_in.CloneNode(true);
                    XmlElement xe_child = (XmlElement)entrysopy;
                    XmlNodeList nls1 = xe_child.ChildNodes;//继续获取xe_in子节点的所有子节点 
                    foreach (XmlNode xn1 in nls1)//遍历
                    {
                        XmlElement xe = (XmlElement)xn1;//将子节点类型转换为XmlElement类型 
                        switch (xe.Name)
                        {
                           
                            case "inventorycode":
                                xe.InnerText = drbody["inventorycode"].ToString();   //调拨单 子表 没有仓库id
                                break;
                            case "invname":
                                xe.InnerText = drbody["invname"].ToString();      //连接查询存货表
                                break;
                            case "quantity":
                                xe.InnerText = drbody["quantity"].ToString();     //数量
                                break;
                            case "number":
                                {
                                    if (!string.IsNullOrWhiteSpace(drbody["subUnit"].ToString()))
                                    {
                                        xe.InnerText = drbody["quantity2"].ToString();     //件数
                                    }
                                    else {
                                        xe.InnerText = string.Empty;
                                    }
                                }
                                
                                break;
                            case "irate":
                                xe.InnerText = drbody["changeRate"].ToString();     //数量
                                break;
                            case "cmassunitname":
                                xe.InnerText = drbody["name"].ToString(); //主计量单位名称
                                break;
                            case "actualprice":
                                {
                                    //xe.InnerText =drbody["price"].ToString(); 
                                    xe.InnerText = string.Empty;
                                }
                               
                                break;
                            case "actualcost":
                                {
                                    //xe.InnerText = drbody["amount"].ToString(); 
                                    xe.InnerText = string.Empty;
                                }
                               
                                break;
                            case "price":
                                {
                                    //xe.InnerText = drbody["price"].ToString(); 
                                    xe.InnerText = string.Empty;
                                }
                                break;
                            case "tvcode":
                                {
                                     xe.InnerText = drhead["code"].ToString();
                                }
                                break;
                            case "define31":
                                {
                                    xe.InnerText = drbody["define10"].ToString();
                                }
                                break;
                            case "define32":
                                {
                                    xe.InnerText = drbody["define11"].ToString();
                                }
                                break;
                            case "define33":
                                {
                                    xe.InnerText = drbody["define12"].ToString();
                                }
                                break;
                            case "assitantunit":
                                xe.InnerText = drbody["subUnit"].ToString();//辅记量单位   
                                break;
                            case "assitantunitname":
                                xe.InnerText = drbody["subUnitName"].ToString();// 辅计量单位名称       
                                break;
                        }
                    }
                    body.AppendChild(xe_child);
                }
                ufinterface.AppendChild(transvouchCopy);
                xmlDoc.Save(url);
            }

        }
    }
}
