USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[Filtros]    Script Date: 15/05/2017 16:54:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Filtros](
	[id_filtro] [int] IDENTITY(1,1) NOT NULL,
	[preco_max] [real] NULL,
	[distancia_max] [real] NULL,
	[rating_min] [real] NULL,
 CONSTRAINT [PK_Filtros] PRIMARY KEY CLUSTERED 
(
	[id_filtro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

