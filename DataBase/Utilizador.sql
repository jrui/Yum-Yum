USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[Utilizador]    Script Date: 15/05/2017 16:55:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Utilizador](
	[id_utilizador] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[filtro] [int] NOT NULL,
 CONSTRAINT [PK_Utilizador] PRIMARY KEY CLUSTERED 
(
	[id_utilizador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Utilizador]  WITH CHECK ADD  CONSTRAINT [FK_Utilizador_Filtros] FOREIGN KEY([filtro])
REFERENCES [dbo].[Filtros] ([id_filtro])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Utilizador] CHECK CONSTRAINT [FK_Utilizador_Filtros]
GO

