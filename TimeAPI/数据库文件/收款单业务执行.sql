CREATE TABLE [dbo].[tb_Recept](
	[code] [nvarchar](50) NULL,
	[Reg] [nvarchar](25) NULL
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'同步的单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Recept', @level2type=N'COLUMN',@level2name=N'code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'U8注册码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_Recept', @level2type=N'COLUMN',@level2name=N'Reg'
GO
