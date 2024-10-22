USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[TipoCozinha]    Script Date: 15/05/2017 16:55:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TipoCozinha](
	[id_cozinha] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](255) NOT NULL,
 CONSTRAINT [PK_TipoCozinha] PRIMARY KEY CLUSTERED 
(
	[id_cozinha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

