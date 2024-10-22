USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[RestaurantesVisitados]    Script Date: 07/06/2017 15:33:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RestaurantesVisitados](
	[data] [date] NOT NULL,
	[utilizador] [int] NOT NULL,
	[restaurante] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[utilizador] ASC,
	[restaurante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

