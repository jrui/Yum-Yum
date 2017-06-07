USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[FiltroCozinha]    Script Date: 07/06/2017 15:33:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FiltroCozinha](
	[cozinha] [int] NOT NULL,
	[filtro] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cozinha] ASC,
	[filtro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FiltroCozinha]  WITH CHECK ADD  CONSTRAINT [FK_FiltroCozinha_Filtros] FOREIGN KEY([filtro])
REFERENCES [dbo].[Filtros] ([id_filtro])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[FiltroCozinha] CHECK CONSTRAINT [FK_FiltroCozinha_Filtros]
GO

ALTER TABLE [dbo].[FiltroCozinha]  WITH CHECK ADD  CONSTRAINT [FK_FiltroCozinha_TipoCozinha] FOREIGN KEY([cozinha])
REFERENCES [dbo].[TipoCozinha] ([id_cozinha])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[FiltroCozinha] CHECK CONSTRAINT [FK_FiltroCozinha_TipoCozinha]
GO

