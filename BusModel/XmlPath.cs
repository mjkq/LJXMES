using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusModel
{
    public class XmlPath
    {
        /// <summary>
        /// 客户档案路径
        /// </summary>
        public static string Customer
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\Customer.xml"; }
        }

        /// <summary>
        /// 存货档案路径
        /// </summary>
        public static string Inventory {
             get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\Inventory.xml"; }
        }

        /// <summary>
        /// 供应商档案路径
        /// </summary>
        public static string Vendor
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\Vendor.xml"; }
        }

        /// <summary>
        /// 销售订单路径
        /// </summary>
        public static string SaleOrder
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\SaleOrder.xml"; }
        }

        /// <summary>
        /// 发货单路径
        /// </summary>
        public static string Consignment
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\Consignment.xml"; }
        }

        /// <summary>
        /// 采购入库单路径
        /// </summary>
        public static string Storein
        {
            get {  return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\Storein.xml"; }
        }

        /// <summary>
        /// 采购订单单路径
        /// </summary>
        public static string PurchaseOrder
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\PurchaseOrder.xml"; }
        }

        /// <summary>
        /// 出库单路径
        /// </summary>
        public static string StoreOut
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\storeout.xml"; }
        }


        /// <summary>
        /// 调拨单路径
        /// </summary>
        public static string Allocation
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "XmlFile\\transvouch.xml"; }
        }

        /// <summary>
        /// T+密钥文件路径
        /// </summary>
        public static string PemPath
        {
            get { return System.AppDomain.CurrentDomain.BaseDirectory + "Pem\\cjet_pri.pem"; }
        }


    }
}
