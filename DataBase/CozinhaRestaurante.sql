USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[CozinhaRestaurante]    Script Date: 07/06/2017 15:24:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CozinhaRestaurante](
	[restaurante] [varchar](255) NOT NULL,
	[cozinha] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[restaurante] ASC,
	[cozinha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

