USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[CozinhaRestaurante]    Script Date: 15/05/2017 16:53:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CozinhaRestaurante](
	[restaurante] [varchar](255) NOT NULL,
	[cozinha] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CozinhaRestaurante]  WITH CHECK ADD  CONSTRAINT [FK_CozinhaRestaurante_Restaurante] FOREIGN KEY([restaurante])
REFERENCES [dbo].[Restaurante] ([nome])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CozinhaRestaurante] CHECK CONSTRAINT [FK_CozinhaRestaurante_Restaurante]
GO

ALTER TABLE [dbo].[CozinhaRestaurante]  WITH CHECK ADD  CONSTRAINT [FK_CozinhaRestaurante_TipoCozinha] FOREIGN KEY([cozinha])
REFERENCES [dbo].[TipoCozinha] ([id_cozinha])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[CozinhaRestaurante] CHECK CONSTRAINT [FK_CozinhaRestaurante_TipoCozinha]
GO

