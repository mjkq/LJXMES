--清除单条数据
DELETE FROM  dbo.tb_AllocationToAllocation WHERE CODE='T+单据号'--调拨单到调拨单中间表
DELETE FROM  dbo.tb_AllocationToDelivery WHERE CODE='T+单据号'--调拨单到发货单中间表
DELETE FROM  dbo.tb_AllocationToPurchase WHERE CODE='T+单据号'--调拨单到采购入库单中间表
DELETE FROM  dbo.tb_Customer WHERE CODE='T+单据号' AND Reg='U8账套对应的注册号'--客户档案中间表
DELETE FROM  dbo.tb_Inventory WHERE CODE='T+单据号' AND Reg='U8账套对应的注册号'--存货档案中间表
DELETE FROM  dbo.tb_OStoreinToOStorein WHERE CODE='T+单据号'--其他入库到其他入库单中间表
DELETE FROM  dbo.tb_OStoreoutToOStoreout WHERE CODE='T+单据号'--其他出库到其他出库单中间表
DELETE FROM  dbo.tb_PStoreinToPStorein WHERE CODE='T+单据号'--产成品入库单到产成品入库单中间表
DELETE FROM  dbo.tb_PurchaseOrder WHERE CODE='T+单据号'--采购订单中间表
DELETE FROM  dbo.tb_PurChaseStorein WHERE CODE='T+单据号'--采购入库单到采购入库单中间表
DELETE FROM  dbo.tb_SaleDelivery WHERE CODE='T+单据号'--发货单到发货单中间表
DELETE FROM  dbo.tb_SaleOrder WHERE CODE='T+单据号'--销售订单中间表
DELETE FROM  dbo.tb_StoreinToInvoice WHERE CODE='T+单据号'-- 采购入库单到发货单中间表
DELETE FROM  dbo.tb_StoreinToPStorein WHERE CODE='T+单据号'-- 采购入库单到产成品入库单中间表
DELETE FROM  dbo.tb_Vendor WHERE CODE='T+单据号'--供应商中间表
DELETE FROM  dbo.tb_Recept WHERE CODE='T+单据号' AND Reg='U8账套对应的注册号'--收款单中间表