USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[RestaurantesVisitados]    Script Date: 15/05/2017 16:55:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RestaurantesVisitados](
	[data] [date] NOT NULL,
	[utilizador] [int] NOT NULL,
	[restaurante] [varchar](255) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RestaurantesVisitados]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantesVisitados_Restaurante] FOREIGN KEY([restaurante])
REFERENCES [dbo].[Restaurante] ([nome])
GO

ALTER TABLE [dbo].[RestaurantesVisitados] CHECK CONSTRAINT [FK_RestaurantesVisitados_Restaurante]
GO

ALTER TABLE [dbo].[RestaurantesVisitados]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantesVisitados_Utilizador] FOREIGN KEY([utilizador])
REFERENCES [dbo].[Utilizador] ([id_utilizador])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[RestaurantesVisitados] CHECK CONSTRAINT [FK_RestaurantesVisitados_Utilizador]
GO

