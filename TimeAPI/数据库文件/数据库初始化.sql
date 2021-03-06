USE [master]
GO
/****** Object:  Database [TIMEAPI]    Script Date: 04/22/2019 10:40:47 ******/
CREATE DATABASE [TIMEAPI] 
GO

USE [TIMEAPI]
GO
/****** Object:  Table [dbo].[tb_Vendor]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Vendor](
	[code] [varchar](50) NULL,
	[updated] [datetime] NULL,
	[reg] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_StoreinToPStorein]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_StoreinToPStorein](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_StoreinToInvoice]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_StoreinToInvoice](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_SaleOrder]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_SaleOrder](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_SaleDelivery]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_SaleDelivery](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_PurChaseStorein]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_PurChaseStorein](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_PurchaseOrder]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_PurchaseOrder](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_PStoreinToPStorein]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_PStoreinToPStorein](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_OStoreoutToOStoreout]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_OStoreoutToOStoreout](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_OStoreinToOStorein]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_OStoreinToOStorein](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Inventory]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Inventory](
	[code] [varchar](20) NULL,
	[updated] [datetime] NULL,
	[reg] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Customer]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Customer](
	[code] [varchar](50) NULL,
	[updated] [datetime] NULL,
	[reg] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_AllocationToPurchase]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_AllocationToPurchase](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_AllocationToDelivery]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_AllocationToDelivery](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_AllocationToAllocation]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_AllocationToAllocation](
	[code] [varchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logdetail]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logdetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[busId] [int] NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[date] [nvarchar](50) NOT NULL,
	[url] [nvarchar](100) NULL,
	[postData] [text] NULL,
	[postType] [nvarchar](50) NULL,
	[returnData] [text] NULL,
	[result] [nvarchar](50) NULL,
	[description] [text] NULL,
	[mark] [text] NULL,
 CONSTRAINT [PK_Logdetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'busId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'相关编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调用的地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抛送的参数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'postData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抛送数据类型（API接口，数据库链接）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'postType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'返回的数据' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'returnData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'调用结果' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'result'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'description'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Logdetail', @level2type=N'COLUMN',@level2name=N'mark'
GO
/****** Object:  Table [dbo].[Count]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Count](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BusId] [int] NOT NULL,
	[sucsum] [int] NOT NULL,
	[failsum] [int] NULL,
	[lattime] [datetime] NULL,
	[succount] [int] NULL,
	[failcount] [int] NULL,
 CONSTRAINT [PK_Count] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Count', @level2type=N'COLUMN',@level2name=N'BusId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'成功的次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Count', @level2type=N'COLUMN',@level2name=N'sucsum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'失败的次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Count', @level2type=N'COLUMN',@level2name=N'failsum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下次执行时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Count', @level2type=N'COLUMN',@level2name=N'lattime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次成功次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Count', @level2type=N'COLUMN',@level2name=N'succount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上次失败次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Count', @level2type=N'COLUMN',@level2name=N'failcount'
GO
/****** Object:  Table [dbo].[Business]    Script Date: 04/22/2019 10:40:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Business](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CODE] [varchar](50) NULL,
	[NAME] [varchar](255) NULL,
	[TYPE] [varchar](255) NULL,
	[ISAUTO] [bit] NULL,
	[STARTDATE] [datetime] NULL,
	[ENDDATE] [datetime] NULL,
	[VALUE] [decimal](18, 2) NULL,
	[FREQUENCY] [varchar](50) NULL,
 CONSTRAINT [PK_tb_Business] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'ID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'CODE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'NAME'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同步类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'TYPE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否执行' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'ISAUTO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始同步时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'STARTDATE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截止同步日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'ENDDATE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'VALUE'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同步频率单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Business', @level2type=N'COLUMN',@level2name=N'FREQUENCY'
GO
SET IDENTITY_INSERT [dbo].[Business] ON
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (1, N'Inventory', N'存货档案同步', N'间隔', 0, CAST(0x0000A9E800000000 AS DateTime), CAST(0x0000B84900000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (2, N'Customer', N'客户档案同步', N'间隔', 0, CAST(0x0000AA2300000000 AS DateTime), CAST(0x0000B12700000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (3, N'Vendor', N'供应商档案同步', N'间隔', 0, CAST(0x0000AA0300000000 AS DateTime), CAST(0x0000B12700000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (4, N'SaleOrder', N'销售订单同步到销售订单', N'间隔', 0, CAST(0x0000AA0300000000 AS DateTime), CAST(0x0000B29400000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (5, N'PurChaseStorein', N'采购入库单同步到采购入库单', N'间隔', 0, CAST(0x0000AA0300000000 AS DateTime), CAST(0x0000B12700000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (6, N'OStoreinToOStorein', N'其他入库单同步到其他入库单', N'间隔', 0, CAST(0x0000AA1200000000 AS DateTime), CAST(0x0000BC9F00000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (7, N'OStoreoutToOStoreout', N'其他出库单同步到其他出库单', N'间隔', 0, CAST(0x0000AA1200000000 AS DateTime), CAST(0x0000B6EA00000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (8, N'PStoreinToPStorein', N'产成品入库单同步到产成品入库单', N'间隔', 0, CAST(0x0000AA1200000000 AS DateTime), CAST(0x0000B6EA00000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (9, N'AllocationToAllocation', N'调拨单入库单同步到调拨单', N'间隔', 0, CAST(0x0000AA1200000000 AS DateTime), CAST(0x0000B2A200000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (10, N'AllocationToPurchase', N'调拨单同步到采购入库单', N'间隔', 0, CAST(0x0000AA1200000000 AS DateTime), CAST(0x0000B58300000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (11, N'AllocationToDelivery', N'调拨单同步到发货单', N'间隔', 0, CAST(0x0000AA1900000000 AS DateTime), CAST(0x0000B85E00000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (12, N'SaleDelivery', N'发货单同步发货单', N'间隔', 0, CAST(0x0000AA1900000000 AS DateTime), CAST(0x0000B85E00000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
INSERT [dbo].[Business] ([ID], [CODE], [NAME], [TYPE], [ISAUTO], [STARTDATE], [ENDDATE], [VALUE], [FREQUENCY]) VALUES (13, N'StoreinToPStorein', N'采购入库单同步到产成品入库单', N'间隔', 0, CAST(0x0000AA1900000000 AS DateTime), CAST(0x0000B58300000000 AS DateTime), CAST(5.00 AS Decimal(18, 2)), N'分钟')
SET IDENTITY_INSERT [dbo].[Business] OFF
