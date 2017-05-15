USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[Restaurante]    Script Date: 15/05/2017 16:54:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Restaurante](
	[nome] [varchar](255) NOT NULL,
	[morada] [varchar](255) NOT NULL,
	[rating] [real] NOT NULL,
	[preco_medio] [real] NOT NULL,
	[contacto] [nchar](15) NOT NULL,
	[imagem] [varchar](255) NOT NULL,
	[gestor] [int] NOT NULL,
 CONSTRAINT [PK_Restaurante] PRIMARY KEY CLUSTERED 
(
	[nome] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

