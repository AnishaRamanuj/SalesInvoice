USE [SalesInvoice]
GO
/****** Object:  Table [dbo].[SalesInvoice]    Script Date: 7/27/2023 1:13:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesInvoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[Date] [date] NOT NULL,
	[DueDays] [int] NOT NULL,
	[DueDate] [date] NOT NULL,
	[Party] [nvarchar](max) NOT NULL,
	[Item] [nvarchar](max) NOT NULL,
	[QTY] [int] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Discount] [float] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SalesInvoice] ON 

INSERT [dbo].[SalesInvoice] ([Id], [Code], [Date], [DueDays], [DueDate], [Party], [Item], [QTY], [Rate], [Amount], [Discount], [TotalAmount]) VALUES (4, N'aaaa', CAST(N'2023-07-22' AS Date), 10, CAST(N'2023-08-01' AS Date), N'Party 4', N'Item-5', 100, CAST(100.00 AS Decimal(18, 2)), CAST(10000.00 AS Decimal(18, 2)), 20, CAST(8000.00 AS Decimal(18, 2)))
INSERT [dbo].[SalesInvoice] ([Id], [Code], [Date], [DueDays], [DueDate], [Party], [Item], [QTY], [Rate], [Amount], [Discount], [TotalAmount]) VALUES (5, N'sss', CAST(N'2001-10-10' AS Date), 2, CAST(N'2001-10-12' AS Date), N'Party 2', N'Item-2', 100, CAST(525.00 AS Decimal(18, 2)), CAST(52500.00 AS Decimal(18, 2)), 10, CAST(47250.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[SalesInvoice] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_AddInvoiceDetails]    Script Date: 7/27/2023 1:13:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Anisha Ramanuj
-- Create date: 26-07-2023
-- Description:	Create Invoice
-- =============================================
create PROCEDURE [dbo].[sp_AddInvoiceDetails]
@Id int,
@Code NVARCHAR(max),
@Date DATE,
@DueDays int,
@DueDate DATE,
@Party NVARCHAR(max),
@Item NVARCHAR(max),
@QTY INT,
@Rate DECIMAL(18,2),
@Amount DECIMAL(18,2),
@Discount FLOAT,
@TotalAmount DECIMAL(18,2)


AS
BEGIN 
SET NOCOUNT ON
BEGIN
   INSERT INTO [dbo].[SalesInvoice]
           ([Code]
		   ,[Date]
		   ,[DueDays]
           ,[DueDate]
           ,[Party]
		   ,[Item]
		   ,[Qty]
		   ,[Rate]
		   ,[Amount]
		   ,[Discount],
	       [TotalAmount]
		   )
     VALUES
           (@Code,
		   @Date,
		   @DueDays,
		   @DueDate
		   ,@Party,
		   @Item,
		   @Qty,
		   @Rate,
		   @Amount,
		   @Discount,
		   @TotalAmount
			)
   END
   END;
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteInvoice]    Script Date: 7/27/2023 1:13:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_DeleteInvoice]
   @Id INT
AS
BEGIN
   SET NOCOUNT ON;

   DELETE FROM SalesInvoice
   WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InvoiceList]    Script Date: 7/27/2023 1:13:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_InvoiceList]
AS
BEGIN
   SET NOCOUNT ON;

   SELECT *
   FROM SalesInvoice;
END
GO
