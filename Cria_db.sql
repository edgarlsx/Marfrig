/*

---- Teste Marfrig ----

Script para criar o Banco de Dados, Tabelas e inserir dados iniciais do projeto.

Observações.
* Esse script não funciona caso haja um banco de dados com o mesmo nome.
* Será necessario alterar o caminho do Banco.
* Para inserir os dados iniciais descomentar a Sessão Insert.

*/


USE [master]
GO
/****** Object:  Database [Marfrig]    Script Date: 04/12/2021 18:27:32 ******/
CREATE DATABASE [Marfrig]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Marfrig', FILENAME = N'F:\Desenvolvimentos\DATA\Marfrig.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Marfrig_log', FILENAME = N'F:\Desenvolvimentos\DATA\Marfrig_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Marfrig] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Marfrig].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Marfrig] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Marfrig] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Marfrig] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Marfrig] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Marfrig] SET ARITHABORT OFF 
GO
ALTER DATABASE [Marfrig] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Marfrig] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Marfrig] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Marfrig] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Marfrig] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Marfrig] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Marfrig] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Marfrig] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Marfrig] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Marfrig] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Marfrig] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Marfrig] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Marfrig] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Marfrig] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Marfrig] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Marfrig] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Marfrig] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Marfrig] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Marfrig] SET  MULTI_USER 
GO
ALTER DATABASE [Marfrig] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Marfrig] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Marfrig] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Marfrig] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Marfrig] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Marfrig] SET QUERY_STORE = OFF
GO
USE [Marfrig]
GO
/****** Object:  Table [dbo].[Animal]    Script Date: 04/12/2021 18:27:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Animal](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](100) NULL,
	[Preco] [decimal](15, 2) NULL,
 CONSTRAINT [PK_Animal] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompraGado]    Script Date: 04/12/2021 18:27:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompraGado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPecuarista] [int] NOT NULL,
	[DataEntrega] [date] NULL,
 CONSTRAINT [PK_CompraGado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompraGadoItem]    Script Date: 04/12/2021 18:27:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompraGadoItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCompraGado] [int] NOT NULL,
	[IdAnimal] [int] NOT NULL,
	[Quantidade] [int] NULL,
 CONSTRAINT [PK_CompraGadoItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pecuarista]    Script Date: 04/12/2021 18:27:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pecuarista](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](100) NULL,
 CONSTRAINT [PK_Pecuarista] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompraGado]  WITH CHECK ADD  CONSTRAINT [FK_CompraGado_Pecuarista] FOREIGN KEY([IdPecuarista])
REFERENCES [dbo].[Pecuarista] ([Id])
GO
ALTER TABLE [dbo].[CompraGado] CHECK CONSTRAINT [FK_CompraGado_Pecuarista]
GO
ALTER TABLE [dbo].[CompraGadoItem]  WITH CHECK ADD  CONSTRAINT [FK_CompraGadoItem_Animal] FOREIGN KEY([IdAnimal])
REFERENCES [dbo].[Animal] ([Id])
GO
ALTER TABLE [dbo].[CompraGadoItem] CHECK CONSTRAINT [FK_CompraGadoItem_Animal]
GO
ALTER TABLE [dbo].[CompraGadoItem]  WITH CHECK ADD  CONSTRAINT [FK_CompraGadoItem_CompraGado] FOREIGN KEY([IdCompraGado])
REFERENCES [dbo].[CompraGado] ([Id])
GO
ALTER TABLE [dbo].[CompraGadoItem] CHECK CONSTRAINT [FK_CompraGadoItem_CompraGado]
GO
USE [master]
GO
ALTER DATABASE [Marfrig] SET  READ_WRITE 
GO

use [Marfrig] 
go

--- Sessão insert ---
/**** Popula as tabelas Pecuarista / Animal ****

insert into [dbo].[Pecuarista] Values ('Pecuarista 01')
insert into [dbo].[Pecuarista] Values ('Pecuarista 02')
insert into [dbo].[Pecuarista] Values ('Pecuarista 03')
insert into [dbo].[Pecuarista] Values ('Pecuarista 04')
insert into [dbo].[Pecuarista] Values ('Pecuarista 05')
insert into [dbo].[Pecuarista] Values ('Pecuarista 06')
insert into [dbo].[Pecuarista] Values ('Pecuarista 07')
insert into [dbo].[Pecuarista] Values ('Pecuarista 08')
insert into [dbo].[Pecuarista] Values ('Pecuarista 09')
insert into [dbo].[Pecuarista] Values ('Pecuarista 10')
insert into [dbo].[Pecuarista] Values ('Pecuarista 11')

insert into [dbo].[Animal] Values ('Boi', 5000.00)
insert into [dbo].[Animal] Values ('Vaca', 5000.00)
insert into [dbo].[Animal] Values ('Porco', 5000.00)
insert into [dbo].[Animal] Values ('Porca', 5000.00)
insert into [dbo].[Animal] Values ('Ovelha', 5000.00)
insert into [dbo].[Animal] Values ('Carneiro', 5000.00)
insert into [dbo].[Animal] Values ('Cabra', 5000.00)
insert into [dbo].[Animal] Values ('Bode', 5000.00)
insert into [dbo].[Animal] Values ('Cavalo', 5000.00)
insert into [dbo].[Animal] Values ('Bufalo', 5000.00)

insert into CompraGado Values(2, '2021-12-10')
insert into CompraGado Values(4, '2021-12-11')
insert into CompraGado Values(5, '2021-12-12')

insert into CompraGadoItem Values(1, 1, 100)
insert into CompraGadoItem Values(1, 6, 100)
insert into CompraGadoItem Values(2, 3, 100)
insert into CompraGadoItem Values(3, 5, 100)


*/