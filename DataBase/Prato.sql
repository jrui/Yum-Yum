USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[Prato]    Script Date: 15/05/2017 16:54:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Prato](
	[id_prato] [int] IDENTITY(1,1) NOT NULL,
	[preco] [real] NOT NULL,
	[nome] [varchar](255) NOT NULL,
	[restaurante] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Prato] PRIMARY KEY CLUSTERED 
(
	[id_prato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Prato]  WITH CHECK ADD  CONSTRAINT [FK_Prato_Restaurante] FOREIGN KEY([restaurante])
REFERENCES [dbo].[Restaurante] ([nome])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Prato] CHECK CONSTRAINT [FK_Prato_Restaurante]
GO

