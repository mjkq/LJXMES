--清除所有数据
truncate table dbo.Logdetail --清除日志
truncate table Count --清除同步记录
truncate table dbo.tb_AllocationToAllocation--调拨单到调拨单中间表
truncate table dbo.tb_AllocationToDelivery--调拨单到发货单中间表
truncate table dbo.tb_AllocationToPurchase--调拨单到采购入库单中间表
truncate table dbo.tb_Customer--客户档案中间表
truncate table dbo.tb_Inventory--存货档案中间表
truncate table dbo.tb_OStoreinToOStorein--其他入库到其他入库单中间表
truncate table dbo.tb_OStoreoutToOStoreout--其他出库到其他出库单中间表
truncate table dbo.tb_PStoreinToPStorein--产成品入库单到产成品入库单中间表
truncate table dbo.tb_PurchaseOrder--采购订单中间表
truncate table tb_PurChaseStorein--采购入库单到采购入库单中间表
truncate table dbo.tb_SaleDelivery--发货单到发货单中间表
truncate table dbo.tb_SaleOrder--销售订单中间表
truncate table dbo.tb_StoreinToInvoice-- 采购入库单到发货单中间表
truncate table dbo.tb_StoreinToPStorein-- 采购入库单到产成品入库单中间表
truncate table dbo.tb_Vendor--供应商中间表
truncate table dbo.tb_Recept--收款单中间表