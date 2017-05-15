USE [Yum-Yum]
GO

/****** Object:  Table [dbo].[Comentario]    Script Date: 15/05/2017 16:53:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comentario](
	[id_comentario] [int] IDENTITY(1,1) NOT NULL,
	[descricao] [varchar](255) NOT NULL,
	[restaurante] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Comentario] PRIMARY KEY CLUSTERED 
(
	[id_comentario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Comentario]  WITH CHECK ADD  CONSTRAINT [FK_Comentario_Restaurante] FOREIGN KEY([restaurante])
REFERENCES [dbo].[Restaurante] ([nome])
ON UPDATE CASCADE
GO

ALTER TABLE [dbo].[Comentario] CHECK CONSTRAINT [FK_Comentario_Restaurante]
GO

