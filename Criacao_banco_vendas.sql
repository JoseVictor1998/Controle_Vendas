USE [master]
GO
/****** Object:  Database [Controle_Vendas]    Script Date: 10/03/2026 00:26:02 ******/
CREATE DATABASE [Controle_Vendas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Controle_Vendas', FILENAME = N'/var/opt/mssql/data/Controle_Vendas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Controle_Vendas_log', FILENAME = N'/var/opt/mssql/data/Controle_Vendas_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Controle_Vendas] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Controle_Vendas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Controle_Vendas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Controle_Vendas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Controle_Vendas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Controle_Vendas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Controle_Vendas] SET ARITHABORT OFF 
GO
ALTER DATABASE [Controle_Vendas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Controle_Vendas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Controle_Vendas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Controle_Vendas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Controle_Vendas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Controle_Vendas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Controle_Vendas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Controle_Vendas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Controle_Vendas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Controle_Vendas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Controle_Vendas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Controle_Vendas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Controle_Vendas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Controle_Vendas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Controle_Vendas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Controle_Vendas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Controle_Vendas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Controle_Vendas] SET RECOVERY FULL 
GO
ALTER DATABASE [Controle_Vendas] SET  MULTI_USER 
GO
ALTER DATABASE [Controle_Vendas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Controle_Vendas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Controle_Vendas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Controle_Vendas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Controle_Vendas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Controle_Vendas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Controle_Vendas', N'ON'
GO
ALTER DATABASE [Controle_Vendas] SET QUERY_STORE = ON
GO
ALTER DATABASE [Controle_Vendas] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Controle_Vendas]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Cliente_id] [int] IDENTITY(1,1) NOT NULL,
	[Endereco_ID] [int] NOT NULL,
	[Telefone_ID] [int] NOT NULL,
	[PF_ID] [int] NULL,
	[PJ_ID] [int] NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Cliente_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status_Producao]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status_Producao](
	[Status_ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Ordem] [int] NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Status_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[Pedido_ID] [int] IDENTITY(1,1) NOT NULL,
	[Cliente_ID] [int] NOT NULL,
	[OS_Externa] [nvarchar](6) NOT NULL,
	[Data_Pedido] [datetime] NULL,
	[Status_ID] [int] NOT NULL,
	[Vendedor_ID] [int] NULL,
	[Observacao_Geral] [nvarchar](255) NOT NULL,
	[Valor_Total] [decimal](10, 2) NULL,
	[Forma_Pagamento] [nvarchar](50) NULL,
	[Parcelas] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Pedido_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Dashboard_Financeiro]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Dashboard_Financeiro] AS 
SELECT 
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    P.Valor_Total,
    P.Forma_Pagamento,
    FORMAT(P.Data_Pedido, 'dd/MM/yyyy HH:mm') AS Data_Venda,
    SP.Nome AS Status_Atual
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID;

GO
/****** Object:  Table [dbo].[Custos_Fixos]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Custos_Fixos](
	[Custo_ID] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [nvarchar](100) NOT NULL,
	[Valor] [decimal](10, 2) NOT NULL,
	[Data_Vencimento] [date] NOT NULL,
	[Status_Pagamento] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Custo_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Dashboard_BI_Gerencial]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [dbo].[VW_Dashboard_BI_Gerencial] AS
SELECT 
    (
        SELECT ISNULL(SUM(Valor_Total), 0)
        FROM Pedido
        WHERE Data_Pedido >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
          AND Data_Pedido < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
    ) AS Total_Vendas_Mes,
    (
        SELECT ISNULL(SUM(Valor), 0)
        FROM Custos_Fixos
        WHERE Data_Vencimento >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
          AND Data_Vencimento < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
    ) AS Total_Custos_Mes,
    (
        (
            SELECT ISNULL(SUM(Valor_Total), 0)
            FROM Pedido
            WHERE Data_Pedido >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
              AND Data_Pedido < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
        ) -
        (
            SELECT ISNULL(SUM(Valor), 0)
            FROM Custos_Fixos
            WHERE Data_Vencimento >= DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1)
              AND Data_Vencimento < DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1))
        )
    ) AS Lucro_Estimado;
GO
/****** Object:  Table [dbo].[Historico_Status]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Historico_Status](
	[Historico_ID] [int] IDENTITY(1,1) NOT NULL,
	[Pedido_ID] [int] NOT NULL,
	[Status_ID] [int] NOT NULL,
	[Data_Mudanca] [datetime] NULL,
	[Usuario_ID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Historico_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Alertas_SLA]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Alertas_SLA] AS
WITH UltimoStatus AS (
    SELECT
        H.Pedido_ID,
        H.Status_ID,
        H.Data_Mudanca,
        ROW_NUMBER() OVER (PARTITION BY H.Pedido_ID ORDER BY H.Historico_ID DESC) AS rn
    FROM Historico_Status H
),
DataAprovacao AS (
    SELECT
        Pedido_ID,
        MAX(Data_Mudanca) AS Data_Aprovacao
    FROM Historico_Status
    WHERE Status_ID = 4
    GROUP BY Pedido_ID
)
SELECT
    P.OS_Externa,
    C.Nome AS Cliente,
    SP.Nome AS Status_Atual,
    DATEDIFF(HOUR, U.Data_Mudanca, GETDATE()) AS Horas_No_Status,
    DATEDIFF(HOUR, DA.Data_Aprovacao, GETDATE()) AS Horas_Desde_Aprovacao,

    CASE
        WHEN P.Status_ID IN (2,3) AND DATEDIFF(HOUR, U.Data_Mudanca, GETDATE()) > 24
            THEN 'ATRASADO NA ARTE (24h)'
        WHEN P.Status_ID = 4 AND DATEDIFF(HOUR, U.Data_Mudanca, GETDATE()) > 24
            THEN 'ATRASADO NA IMPRESSÃO (24h)'
        WHEN P.Status_ID IN (4,5) AND DA.Data_Aprovacao IS NOT NULL AND DATEDIFF(HOUR, DA.Data_Aprovacao, GETDATE()) > 168
            THEN 'ATRASADO NA PRODUÇÃO (7 dias após aprovação)'
        ELSE 'No Prazo'
    END AS Alerta_Prazo
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
JOIN UltimoStatus U ON P.Pedido_ID = U.Pedido_ID AND U.rn = 1
LEFT JOIN DataAprovacao DA ON P.Pedido_ID = DA.Pedido_ID;

GO
/****** Object:  Table [dbo].[Tipo_Produto]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Produto](
	[Tipo_Produto_ID] [int] IDENTITY(1,1) NOT NULL,
	[Categoria_ID] [int] NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Descricao_Tecnica] [nvarchar](255) NOT NULL,
	[Usa_Adesivo] [bit] NULL,
	[Usa_Mascara] [bit] NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Tipo_Produto_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido_Item]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido_Item](
	[Item_ID] [int] IDENTITY(1,1) NOT NULL,
	[Pedido_ID] [int] NOT NULL,
	[Tipo_Produto_ID] [int] NOT NULL,
	[Largura] [decimal](10, 2) NOT NULL,
	[Altura] [decimal](10, 2) NOT NULL,
	[Quantidade] [int] NOT NULL,
	[Observacao_Tecnica] [varchar](max) NULL,
	[Caminho_Foto] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Item_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status_Arte]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status_Arte](
	[Status_Arte_ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Status_Arte_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Arquivo_Arte]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Arquivo_Arte](
	[Arquivo_ID] [int] IDENTITY(1,1) NOT NULL,
	[Item_ID] [int] NOT NULL,
	[Nome_Arquivo] [nvarchar](100) NOT NULL,
	[Caminho_Arquivo] [nvarchar](500) NOT NULL,
	[Status_Arte_ID] [int] NOT NULL,
	[Caminho_Fisico] [nvarchar](500) NULL,
	[ContentType] [nvarchar](100) NULL,
	[TamanhoBytes] [bigint] NULL,
	[DataUpload] [datetime2](7) NOT NULL,
	[UsuarioUpload] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Arquivo_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Fila_Arte]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Fila_Arte] AS 
SELECT 
    PI.Item_ID, -- OBRIGATÓRIO: Para o upload saber onde gravar
    AA.Arquivo_ID, -- OBRIGATÓRIO: Para downloads
    P.Os_Externa AS OS, 
    C.Nome AS Cliente, 
    TP.Nome AS Produto,
    DATEDIFF(day, P.Data_Pedido, GETDATE()) AS Dias_Aguardando_Arte,
    PI.Largura, PI.Altura, PI.Quantidade, 
    PI.Observacao_Tecnica, 
    P.Observacao_Geral,
    ISNULL(SA.Nome, 'Pendente / Sem Arquivo') AS Status_Arte,
    -- 🚀 O QUE FALTAVA: 
    PI.Caminho_Foto -- Adicionado para mostrar a miniatura e permitir o zoom
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID 
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID 
LEFT JOIN Status_Arte SA ON AA.Status_Arte_ID = SA.Status_Arte_ID
WHERE P.Status_ID IN (1, 2, 3); -- Status de criação e aprovação de arte

GO
/****** Object:  View [dbo].[VW_Fila_Arte_Finalista_Full]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_Fila_Arte_Finalista_Full] AS
SELECT 
    PI.Item_ID,
    P.Pedido_ID,
    AA.Arquivo_ID AS ArquivoId, -- 🚀 É SÓ ADICIONAR ESSA LINHA AQUI!
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    TP.Nome AS Produto,
    PI.Largura, 
    PI.Altura, 
    PI.Quantidade,
    PI.Observacao_Tecnica,
    AA.Caminho_Arquivo AS Caminho_Arte,
    SA.Nome AS Status_Arte,
    
    -- 🚀 ADICIONADO: Traz o número exato do status no banco para facilitar auditoria
    P.Status_ID AS Pedido_Status_ID, 
    
    -- 🚀 O NOVO SEGREDO: Traduz o número do status para o nome real do setor
    CASE 
        WHEN P.Status_ID = 1 THEN 'Orçamento / Novo'
        WHEN P.Status_ID = 2 THEN 'Fila de Arte'
        WHEN P.Status_ID = 3 THEN 'Arte em Correção'
        WHEN P.Status_ID = 4 THEN 'Fila de Impressão'
        WHEN P.Status_ID = 5 THEN 'Fila de Produção'
        WHEN P.Status_ID = 6 THEN 'Acabamento / Pronto'
        WHEN P.Status_ID = 7 THEN 'Entregue'
        WHEN P.Status_ID = 8 THEN 'Arquivado / Reprovado'
        ELSE 'Status Desconhecido (' + CAST(P.Status_ID AS VARCHAR) + ')'
    END AS Setor_Fila

FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID
LEFT JOIN Status_Arte SA ON AA.Status_Arte_ID = SA.Status_Arte_ID;

GO
/****** Object:  Table [dbo].[Material]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[Material_ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Descricao] [nvarchar](255) NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Material_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Produto_Material]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Produto_Material](
	[Tipo_Produto_ID] [int] NOT NULL,
	[Material_ID] [int] NOT NULL,
 CONSTRAINT [PK_Tipo_Produto_Material] PRIMARY KEY CLUSTERED 
(
	[Tipo_Produto_ID] ASC,
	[Material_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Fila_Impressao]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Fila_Impressao] AS 
SELECT 
    PI.Item_ID,
    AA.Arquivo_ID,
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    TP.Nome AS Produto,
    DATEDIFF(day, P.Data_Pedido, GETDATE()) AS Dias_em_Impressao,
    -- Ajuste no Material: Se for nulo, avisa que está sem vínculo
    ISNULL((SELECT STRING_AGG(M.Nome, ' / ') FROM 
            Tipo_Produto_Material TPM 
            JOIN Material M ON TPM.Material_ID = M.Material_ID
            WHERE TPM.Tipo_Produto_ID = TP.Tipo_Produto_ID), 'Sem Material Definido') AS Material_Base,
    PI.Largura, PI.Altura, PI.Quantidade,
    PI.Caminho_Foto, -- 👈 FALTAVA ISSO: Para a foto aparecer na miniatura
    ISNULL(AA.Caminho_Arquivo, '') AS Link_Arte, 
    P.Observacao_Geral,
    PI.Observacao_Tecnica
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID 
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID 
WHERE P.Status_ID = 4;
GO
/****** Object:  View [dbo].[VW_Monitoramento_Global]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 1. MONITORAMENTO GLOBAL: O "Onde está meu pedido?" (Visão para o Vendedor/Dono)
CREATE VIEW [dbo].[VW_Monitoramento_Global] AS 
SELECT P.OS_Externa AS OS, C.Nome AS Cliente, TP.Nome AS Produto, SP.Nome AS Status_Producao, FORMAT(P.Data_Pedido, 'dd/MM/yyyy HH:mm') AS Data_Entrada
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID;

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Usuario_ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Funcao] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NULL,
	[Senha] [nvarchar](255) NULL,
	[Nivel_Acesso] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Usuario_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Fila_Producao_Completa]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE    VIEW [dbo].[VW_Fila_Producao_Completa] AS 
SELECT 
    PI.Item_ID AS ItemId,
    P.OS_Externa AS Os, 
    C.Nome AS Cliente, 
    TP.Nome AS Produto,
    (SELECT STRING_AGG(M.Nome, ' / ') FROM 
        Tipo_Produto_Material TPM 
        JOIN Material M ON TPM.Material_ID = M.Material_ID
        WHERE TPM.Tipo_Produto_ID = TP.Tipo_Produto_ID) AS MaterialBase,
    PI.Largura, 
    PI.Altura, 
    PI.Quantidade, 
    PI.Caminho_Foto AS CaminhoFoto, 
    DATEDIFF(HOUR, P.Data_Pedido, GETDATE()) AS HorasDesdeAbertura,
    U.Nome AS VendedorResponsavel,
    P.Observacao_Geral AS ObservacaoGeral,
    PI.Observacao_Tecnica AS ObservacaoTecnica
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID 
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Usuario U ON P.Vendedor_ID = U.Usuario_ID
WHERE P.Status_ID IN (5);
GO
/****** Object:  View [dbo].[VW_Dashboard_Gestao_Ativa]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Dashboard_Gestao_Ativa] AS 
SELECT 
    SP.Nome AS Etapa,
    COUNT(P.Pedido_ID) AS Total_Pedidos 
FROM Status_Producao SP
LEFT JOIN Pedido P ON SP.Status_ID = P.Status_ID
-- Filtro para remover os Arquivados da visão de gestão diária
WHERE SP.Status_ID <> 8
GROUP BY SP.Nome, SP.Ordem;

GO
/****** Object:  View [dbo].[VW_Meus_Pedidos_Vendedor]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Meus_Pedidos_Vendedor] AS
SELECT 
    P.Pedido_ID,
    P.Vendedor_ID, 
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    SP.Status_ID,
    SP.Nome AS Status_Atual,
    P.Valor_Total,
    P.Forma_Pagamento,
    P.Parcelas,
    P.Data_Pedido,
    -- Conta quantos materiais tem dentro dessa OS
    (SELECT COUNT(*) FROM Pedido_Item WHERE Pedido_ID = P.Pedido_ID) AS Qtd_Itens
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
GO
/****** Object:  Table [dbo].[Cliente_PJ]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente_PJ](
	[Cliente_PJ_ID] [int] IDENTITY(1,1) NOT NULL,
	[CNPJ] [nvarchar](14) NOT NULL,
	[Data_Cadastro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Cliente_PJ_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente_PF]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente_PF](
	[Cliente_PF_ID] [int] IDENTITY(1,1) NOT NULL,
	[CPF] [nvarchar](11) NOT NULL,
	[Data_Cadastro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Cliente_PF_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Endereco]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Endereco](
	[Endereco_ID] [int] IDENTITY(1,1) NOT NULL,
	[Cidade] [nvarchar](50) NOT NULL,
	[CEP] [nvarchar](8) NOT NULL,
	[Bairro] [nvarchar](60) NOT NULL,
	[Rua] [nvarchar](100) NOT NULL,
	[Numero] [int] NULL,
	[Referencia] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Endereco_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefone]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefone](
	[Telefone_ID] [int] IDENTITY(1,1) NOT NULL,
	[DDD] [nvarchar](3) NOT NULL,
	[Numero] [nvarchar](9) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Telefone_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VW_Pesquisa_Clientes_Vendas]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[VW_Pesquisa_Clientes_Vendas] AS
SELECT 
    C.Cliente_id AS ID,
    C.Nome,
    ISNULL(PF.CPF, PJ.CNPJ) AS Documento,
    CASE 
        WHEN C.PF_ID IS NOT NULL THEN 'Pessoa Física'
        WHEN C.PJ_ID IS NOT NULL THEN 'Pessoa Jurídica'
    END AS Tipo_Cliente,
    T.DDD + ' ' + T.Numero AS Telefone,
    C.Email,
    E.Cidade + ' - ' + E.Bairro AS Localidade,
    C.Ativo
FROM Clientes C
LEFT JOIN Cliente_PF PF ON C.PF_ID = PF.Cliente_PF_ID
LEFT JOIN Cliente_PJ PJ ON C.PJ_ID = PJ.Cliente_PJ_ID
JOIN Telefone T ON C.Telefone_ID = T.Telefone_ID
JOIN Endereco E ON C.Endereco_ID = E.Endereco_ID;

GO
/****** Object:  View [dbo].[VW_Historico_Pedidos_Cliente]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   VIEW [dbo].[VW_Historico_Pedidos_Cliente] AS
SELECT 
    C.Nome AS Cliente,
    P.OS_Externa AS OS,
    P.Data_Pedido,
    TP.Nome AS Produto,
    PI.Largura,
    PI.Altura,
    PI.Quantidade,
    S.Nome AS Status_Atual,
    PI.Observacao_Tecnica
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Status_Producao S ON P.Status_ID = S.Status_ID;

GO
/****** Object:  View [dbo].[VW_Busca_Rapida_Pedido]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[VW_Busca_Rapida_Pedido] AS
SELECT
P.OS_Externa AS OS,
C.Nome AS Nome,
P.Data_Pedido,
SP.Nome AS Status_Atual,
TP.Nome AS Produto,
PI.Altura,
PI.Largura,
PI.Quantidade
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID;

GO
/****** Object:  Table [dbo].[Categoria_Produtos]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria_Produtos](
	[Categoria_ID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Descricao] [nvarchar](255) NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Categoria_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Arquivo_Arte] ON 

INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (1, 1, N'arquivo vovo ', N'C:\Users\porce\OneDrive\Imagens', 5, NULL, NULL, NULL, CAST(N'2026-02-28T20:15:16.8973519' AS DateTime2), NULL)
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (2, 2, N'134118341497216055.jpg', N'/api/Producao/DownloadArte/2', 4, N'/data/artes/Item-2/2_20260301_035259_68631aef5a5e4a69b0865f1e97678a5a.jpg', N'image/jpeg', 2042607, CAST(N'2026-03-01T03:52:59.1788923' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (2003, 1002, N'134118341497216055.jpg', N'/api/Producao/DownloadArte/2003', 4, N'/data/artes/Item-1002/1002_20260301_163527_f388c23dc73d40d18430a0d2dedb0bfd.jpg', N'image/jpeg', 2042607, CAST(N'2026-03-01T16:35:27.7815876' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (3003, 2002, N'134107235287797445.jpg', N'/api/Producao/DownloadArte/3003', 4, N'/data/artes/Item-2002/2002_20260301_164249_172b6d0d49184fc9a8b4062213c6f9de.jpg', N'image/jpeg', 2204272, CAST(N'2026-03-01T16:42:49.9853023' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (4003, 3002, N'teste.pdf', N'/api/Producao/DownloadArte/4003', 4, N'/data/artes/Item-3002/3002_20260301_192120_cccb3532ca7b412babfb75cbb07d689e.pdf', N'application/pdf', 25834, CAST(N'2026-03-01T19:21:21.0038535' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (5003, 4002, N'teste.pdf', N'/api/Producao/DownloadArte/5003', 4, N'/data/artes/Item-4002/4002_20260301_193228_66dae0b759ba4435bca4fd470f8a9684.pdf', N'application/pdf', 25834, CAST(N'2026-03-01T19:32:28.4026925' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (6003, 5002, N'teste.pdf', N'/api/Producao/DownloadArte/6003', 4, N'/data/artes/Item-5002/5002_20260301_195243_2db1341e3b73424c9058dd16ed7e2a58.pdf', N'application/pdf', 25834, CAST(N'2026-03-01T19:52:44.0159792' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (6004, 5003, N'teste.pdf', N'/api/Producao/DownloadArte/6004', 4, N'/data/artes/Item-5003/5003_20260301_200056_5bcf8c65bf0d42e79864cbbd11371c54.pdf', N'application/pdf', 25834, CAST(N'2026-03-01T20:00:56.8066950' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (6005, 5004, N'teste.pdf', N'/api/Producao/DownloadArte/6005', 4, N'/data/artes/Item-5004/5004_20260301_200102_d00c50f9a68246a684a184eec1362bfb.pdf', N'application/pdf', 25834, CAST(N'2026-03-01T20:01:02.1260460' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (7003, 6002, N'teste.pdf', N'/api/Producao/DownloadArte/7003', 4, N'/data/artes/Item-6002/6002_20260302_030824_5ffdc62622bd48a89287cf32922e2e0a.pdf', N'application/pdf', 25834, CAST(N'2026-03-02T03:08:24.1243559' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (7004, 6003, N'teste.pdf', N'/api/Producao/DownloadArte/7004', 5, N'/data/artes/Item-6003/6003_20260302_032532_3972f5f836e64a8a8fe06d82130fab9d.pdf', N'application/pdf', 25834, CAST(N'2026-03-02T03:25:32.9625247' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (8003, 7002, N'teste.pdf', N'/api/Producao/DownloadArte/8003', 4, N'/data/artes/Item-7002/7002_20260302_035848_29d1e1f873e3475da433a8a22b52b846.pdf', N'application/pdf', 25834, CAST(N'2026-03-02T03:58:48.4090217' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (8004, 7003, N'teste.pdf', N'/api/Producao/DownloadArte/8004', 4, N'/data/artes/Item-7003/7003_20260302_040313_4291f4d563514e909db8235ff7b2747b.pdf', N'application/pdf', 25834, CAST(N'2026-03-02T04:03:13.2670075' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (9003, 9004, N'134107235287797445.jpg', N'/api/Producao/DownloadArte/9003', 4, N'/data/artes/Item-9004/9004_20260307_021315_6c76510c00bf4eab809e46e0d233cfe1.jpg', N'image/jpeg', 2204272, CAST(N'2026-03-07T02:13:15.4081196' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (9004, 9005, N'042c0d68454c48a4922a4866735a5e28.jpg', N'http://localhost:5001/uploads/042c0d68454c48a4922a4866735a5e28.jpg', 1, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (10003, 10004, N'134118341497216055.jpg', N'/api/Producao/DownloadArte/10003', 4, N'/data/artes/Item-10004/10004_20260307_021326_8ed2bc558aa341acbb350fa5455de201.jpg', N'image/jpeg', 2042607, CAST(N'2026-03-07T02:13:26.6100405' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (10004, 10005, N'3ea13f6881444fd780673cd9dd0281a6.jpg', N'http://localhost:5001/uploads/3ea13f6881444fd780673cd9dd0281a6.jpg', 1, NULL, NULL, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL)
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (11003, 11004, N'teste.pdf', N'/api/Producao/DownloadArte/11003', 2, N'/data/artes/Item-11004/11004_20260310_013826_102bfaf504fb4bc6845b5e20b860bb2f.pdf', N'application/pdf', 25834, CAST(N'2026-03-10T01:38:26.3722670' AS DateTime2), N'admin')
INSERT [dbo].[Arquivo_Arte] ([Arquivo_ID], [Item_ID], [Nome_Arquivo], [Caminho_Arquivo], [Status_Arte_ID], [Caminho_Fisico], [ContentType], [TamanhoBytes], [DataUpload], [UsuarioUpload]) VALUES (12003, 12004, N'arte_os_2 (2).pdf', N'/api/Producao/DownloadArte/12003', 2, N'/data/artes/Item-12004/12004_20260310_020027_a034aad11577406c8f7f008a01b52479.pdf', N'application/pdf', 2042607, CAST(N'2026-03-10T02:00:27.4130211' AS DateTime2), N'admin')
SET IDENTITY_INSERT [dbo].[Arquivo_Arte] OFF
GO
SET IDENTITY_INSERT [dbo].[Categoria_Produtos] ON 

INSERT [dbo].[Categoria_Produtos] ([Categoria_ID], [Nome], [Descricao], [Ativo]) VALUES (1, N'Adesivo', N'Produtos baseados em adesivo', 1)
INSERT [dbo].[Categoria_Produtos] ([Categoria_ID], [Nome], [Descricao], [Ativo]) VALUES (2, N'Placas', N'Placas de sinalização e comunicação visual', 1)
INSERT [dbo].[Categoria_Produtos] ([Categoria_ID], [Nome], [Descricao], [Ativo]) VALUES (3, N'Acrilico', N'Produtos fabricados em acrilico', 1)
INSERT [dbo].[Categoria_Produtos] ([Categoria_ID], [Nome], [Descricao], [Ativo]) VALUES (4, N'Metal', N'Produtos fabricados em metal', 1)
INSERT [dbo].[Categoria_Produtos] ([Categoria_ID], [Nome], [Descricao], [Ativo]) VALUES (5, N'UV', N'Produtos fabricados em impressão UV', 1)
SET IDENTITY_INSERT [dbo].[Categoria_Produtos] OFF
GO
SET IDENTITY_INSERT [dbo].[Cliente_PF] ON 

INSERT [dbo].[Cliente_PF] ([Cliente_PF_ID], [CPF], [Data_Cadastro]) VALUES (1, N'03745466279', CAST(N'2026-02-22T17:09:27.507' AS DateTime))
INSERT [dbo].[Cliente_PF] ([Cliente_PF_ID], [CPF], [Data_Cadastro]) VALUES (2, N'00020202020', CAST(N'2026-02-28T04:58:36.937' AS DateTime))
SET IDENTITY_INSERT [dbo].[Cliente_PF] OFF
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([Cliente_id], [Endereco_ID], [Telefone_ID], [PF_ID], [PJ_ID], [Email], [Nome], [Ativo]) VALUES (1, 1, 1, 1, NULL, N'porcellani303@gmail.com', N'Jose', 1)
INSERT [dbo].[Clientes] ([Cliente_id], [Endereco_ID], [Telefone_ID], [PF_ID], [PJ_ID], [Email], [Nome], [Ativo]) VALUES (2, 2, 2, 2, NULL, N'ghyuftgf@gmail.com', N'Giovani Thofani', 1)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[Endereco] ON 

INSERT [dbo].[Endereco] ([Endereco_ID], [Cidade], [CEP], [Bairro], [Rua], [Numero], [Referencia]) VALUES (1, N'maringa', N'87023621', N'jardim vitoria', N'pioneiro jaoao zavatini', 723, NULL)
INSERT [dbo].[Endereco] ([Endereco_ID], [Cidade], [CEP], [Bairro], [Rua], [Numero], [Referencia]) VALUES (2, N'paudalho', N'87696969', N'ate enjua', N'ate chora', 7969, NULL)
SET IDENTITY_INSERT [dbo].[Endereco] OFF
GO
SET IDENTITY_INSERT [dbo].[Historico_Status] ON 

INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (1, 1, 1, CAST(N'2026-02-22T17:46:33.110' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (2, 1, 4, CAST(N'2026-02-22T17:56:03.453' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (3, 1, 5, CAST(N'2026-02-22T17:56:38.653' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (4, 1, 4, CAST(N'2026-02-22T17:57:54.650' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (1002, 3, 1, CAST(N'2026-02-24T01:18:15.910' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (2011, 1, 3, CAST(N'2026-02-26T02:57:58.000' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (3002, 1, 4, CAST(N'2026-02-27T03:13:54.067' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (4002, 1, 8, CAST(N'2026-02-27T04:00:06.840' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (4003, 1, 5, CAST(N'2026-02-27T04:05:52.660' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (5002, 1002, 1, CAST(N'2026-02-28T05:17:41.950' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (6002, 2002, 1, CAST(N'2026-03-01T16:36:40.157' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (7002, 3, 4, CAST(N'2026-03-01T18:55:43.637' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (8002, 1002, 4, CAST(N'2026-03-01T19:19:12.367' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (8003, 2002, 4, CAST(N'2026-03-01T19:19:29.040' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (8004, 3002, 1, CAST(N'2026-03-01T19:21:09.617' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (8005, 3002, 4, CAST(N'2026-03-01T19:21:24.433' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (9002, 4002, 1, CAST(N'2026-03-01T19:32:15.920' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (9003, 4002, 4, CAST(N'2026-03-01T19:32:32.767' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (10002, 5002, 1, CAST(N'2026-03-01T19:52:34.987' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (10003, 5002, 4, CAST(N'2026-03-01T19:52:50.477' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (10004, 5003, 1, CAST(N'2026-03-01T19:54:11.233' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (10005, 5004, 1, CAST(N'2026-03-01T20:00:44.743' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (10006, 5003, 4, CAST(N'2026-03-01T20:01:04.300' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (10007, 5004, 4, CAST(N'2026-03-01T20:01:10.400' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (11002, 3002, 5, CAST(N'2026-03-01T20:27:36.097' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (12002, 6002, 1, CAST(N'2026-03-02T02:44:53.423' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (12003, 6002, 2, CAST(N'2026-03-02T03:19:53.550' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (12004, 6002, 3, CAST(N'2026-03-02T03:19:53.553' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (12005, 6002, 2, CAST(N'2026-03-02T03:20:00.503' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (12006, 6002, 3, CAST(N'2026-03-02T03:20:00.507' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (12007, 6003, 1, CAST(N'2026-03-02T03:23:34.430' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13002, 6002, 4, CAST(N'2026-03-02T03:32:05.230' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13003, 7002, 1, CAST(N'2026-03-02T03:43:49.620' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13004, 7003, 1, CAST(N'2026-03-02T03:51:16.343' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13005, 6003, 2, CAST(N'2026-03-02T03:59:09.530' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13006, 6003, 3, CAST(N'2026-03-02T03:59:09.537' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13007, 6003, 8, CAST(N'2026-03-02T03:59:15.410' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13008, 7002, 4, CAST(N'2026-03-02T03:59:20.287' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13009, 7003, 4, CAST(N'2026-03-02T04:03:20.477' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13010, 1002, 5, CAST(N'2026-03-02T04:04:15.067' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13011, 2002, 5, CAST(N'2026-03-02T04:07:08.317' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13012, 4002, 5, CAST(N'2026-03-02T04:07:10.893' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (13013, 5002, 5, CAST(N'2026-03-02T04:07:13.967' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (14002, 1002, 6, CAST(N'2026-03-05T02:10:25.897' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (14003, 4002, 6, CAST(N'2026-03-05T02:10:40.700' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (15002, 5003, 5, CAST(N'2026-03-05T02:42:40.703' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (15003, 1, 6, CAST(N'2026-03-05T03:46:43.193' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (16002, 5003, 6, CAST(N'2026-03-05T04:13:28.287' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (16003, 5002, 6, CAST(N'2026-03-05T04:13:30.370' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (16004, 3002, 6, CAST(N'2026-03-05T04:13:32.360' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (16005, 2002, 6, CAST(N'2026-03-05T04:13:34.400' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (18002, 9002, 1, CAST(N'2026-03-06T03:05:49.237' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (19002, 10002, 1, CAST(N'2026-03-06T03:23:34.523' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20002, 5004, 5, CAST(N'2026-03-07T02:12:03.550' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20003, 9002, 2, CAST(N'2026-03-07T02:13:20.010' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20004, 9002, 3, CAST(N'2026-03-07T02:13:20.013' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20005, 9002, 4, CAST(N'2026-03-07T02:13:21.530' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20006, 10002, 4, CAST(N'2026-03-07T02:13:30.213' AS DateTime), 2)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20007, 6002, 5, CAST(N'2026-03-07T02:13:45.793' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20008, 7002, 5, CAST(N'2026-03-07T02:13:48.030' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (20009, 7003, 5, CAST(N'2026-03-07T02:13:49.990' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (21002, 3, 5, CAST(N'2026-03-07T02:26:14.657' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (21003, 9002, 5, CAST(N'2026-03-07T02:26:20.503' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (21004, 10002, 5, CAST(N'2026-03-07T02:26:22.767' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (22002, 1, 7, CAST(N'2026-03-10T01:37:16.747' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (22003, 11002, 1, CAST(N'2026-03-10T01:38:13.630' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (23002, 12002, 1, CAST(N'2026-03-10T02:00:18.347' AS DateTime), 1)
INSERT [dbo].[Historico_Status] ([Historico_ID], [Pedido_ID], [Status_ID], [Data_Mudanca], [Usuario_ID]) VALUES (23003, 12002, 3, CAST(N'2026-03-10T02:00:27.453' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Historico_Status] OFF
GO
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (1, N'Adesivo Vinil Fosco', N'Vinil fosco para impressão', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (2, N'Adesivo Refletivo GTP Branco', N'Refletivo branco GTP', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (3, N'Adesivo GTP Amarelo', N'Refletivo amarelo GTP', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (4, N'Acrílico 2mm', N'Acrílico espessura 2mm', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (5, N'Acrílico 3mm', N'Acrílico espessura 3mm', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (6, N'Acrílico 6mm Branco', N'Acrílico branco 6mm', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (7, N'Acrílico 6mm Cristal', N'Acrílico cristal 6mm', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (8, N'Acrílico 6mm Preto', N'Acrílico preto 6mm', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (9, N'Acrílico Espelhado Dourado 2mm', N'Acrílico espelhado dourado', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (10, N'PVC 3mm', N'Placa em PVC expandido', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (11, N'Aço Inox Escovado', N'Inox acabamento escovado', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (12, N'Chapa Galvanizada', N'Chapa galvanizada', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (13, N'Chapa 18', N'Chapa metálica nº 18', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (14, N'ACM 3mm', N'Alumínio composto 3mm', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (15, N'Alumínio', N'Chapa de alumínio', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (16, N'Tubo 2"', N'Tubo metálico 2 polegadas', 1)
INSERT [dbo].[Material] ([Material_ID], [Nome], [Descricao], [Ativo]) VALUES (17, N'Tubo 2.5"', N'Tubo metálico 2.5 polegadas', 1)
SET IDENTITY_INSERT [dbo].[Material] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedido] ON 

INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (1, 1, N'OS-202', CAST(N'2026-02-22T17:46:33.100' AS DateTime), 7, 2, N'Banner para fachada', CAST(1560.00 AS Decimal(10, 2)), N'Cartão de Crédito', 3)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (3, 1, N'002 25', CAST(N'2026-02-24T01:18:15.910' AS DateTime), 5, 1, N'ghfdesdgfvg nuiedfhapi87fgey nj[fhnbrweupibghf', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (1002, 2, N'069706', CAST(N'2026-02-28T05:17:41.947' AS DateTime), 6, 2, N'fazer com 25 cm e grossura de 25cm', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (2002, 2, N'001564', CAST(N'2026-03-01T16:36:40.153' AS DateTime), 6, 2, N'jose ', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (3002, 2, N'20584', CAST(N'2026-03-01T19:21:09.613' AS DateTime), 6, 2, N'dfswfegegsvf', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (4002, 1, N'423423', CAST(N'2026-03-01T19:32:15.913' AS DateTime), 6, 2, N'cdsafcascd', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (5002, 2, N'002526', CAST(N'2026-03-01T19:52:34.980' AS DateTime), 6, 2, N'paca de sinalizacao com poste', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (5003, 1, N'34342', CAST(N'2026-03-01T19:54:11.233' AS DateTime), 6, 2, N'32e', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (5004, 1, N'32423', CAST(N'2026-03-01T20:00:44.743' AS DateTime), 5, 2, N'teste', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (6002, 2, N'1234', CAST(N'2026-03-02T02:44:53.420' AS DateTime), 5, 2, N'vsrgf', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (6003, 1, N'42343', CAST(N'2026-03-02T03:23:34.430' AS DateTime), 8, 2, N'wfe', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (7002, 2, N'453534', CAST(N'2026-03-02T03:43:49.617' AS DateTime), 5, 2, N'grsATRAESHGaeh', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (7003, 2, N'324', CAST(N'2026-03-02T03:51:16.343' AS DateTime), 5, 2, N'fgrewg', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (9002, 2, N'026699', CAST(N'2026-03-06T03:05:49.233' AS DateTime), 5, 2, N'pegar e fazer essa porra ', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (10002, 2, N'123445', CAST(N'2026-03-06T03:23:34.517' AS DateTime), 5, 2, N'poedfhoutyrdsgvctrgfviyuedhgosdfhbgvao8udebghahgfriouygvarg', CAST(0.00 AS Decimal(10, 2)), NULL, NULL)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (11002, 2, N'025242', CAST(N'2026-03-10T01:38:13.630' AS DateTime), 1, 2, N'ntshrga', CAST(0.00 AS Decimal(10, 2)), NULL, 1)
INSERT [dbo].[Pedido] ([Pedido_ID], [Cliente_ID], [OS_Externa], [Data_Pedido], [Status_ID], [Vendedor_ID], [Observacao_Geral], [Valor_Total], [Forma_Pagamento], [Parcelas]) VALUES (12002, 2, N'3242', CAST(N'2026-03-10T02:00:18.340' AS DateTime), 3, 2, N'ADTJHKGVBrfBVLIsrwbgfviirewGFBHYSEA7YHG8EUJhl;d,typnj', CAST(0.00 AS Decimal(10, 2)), NULL, 1)
SET IDENTITY_INSERT [dbo].[Pedido] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedido_Item] ON 

INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (1, 1, 1, CAST(1.50 AS Decimal(10, 2)), CAST(0.80 AS Decimal(10, 2)), 1, N'Lona 440g com ilhós', N'uploads/banner_cliente1.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (2, 3, 25, CAST(111.00 AS Decimal(10, 2)), CAST(1525.00 AS Decimal(10, 2)), 155, N'hjkrefgvbtyuwesgvfc', N'')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (1002, 1002, 19, CAST(25.00 AS Decimal(10, 2)), CAST(25.00 AS Decimal(10, 2)), 25, N'impressao uv urgente', N'http://localhost:5001/uploads/0eaf29ae224947b9aeeb84aeba10d86d.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (2002, 2002, 19, CAST(20.00 AS Decimal(10, 2)), CAST(20.00 AS Decimal(10, 2)), 1, N'aco inox gravado com impressao uv e verniz ', N'http://localhost:5001/uploads/83596f21a62e457d921d41dfb432b0d7.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (3002, 3002, 3, CAST(25.00 AS Decimal(10, 2)), CAST(33.00 AS Decimal(10, 2)), 1, N'fgWEFwwgrg', N'http://localhost:5001/uploads/e5602582df24415ab0c160768880bc86.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (4002, 4002, 15, CAST(3.00 AS Decimal(10, 2)), CAST(22.00 AS Decimal(10, 2)), 1, N'casfdvwv', N'http://localhost:5001/uploads/25ad78be4d8043e39f90fbea8b3a2a39.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (5002, 5002, 12, CAST(25.00 AS Decimal(10, 2)), CAST(30.00 AS Decimal(10, 2)), 15, N'poste com 3 metos de coprimento e reforco ', N'http://localhost:5001/uploads/97b283458ed94502879346b49583adee.webp')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (5003, 5003, 15, CAST(324.00 AS Decimal(10, 2)), CAST(23.00 AS Decimal(10, 2)), 132, N'32er', N'http://localhost:5001/uploads/155ae0c953aa4a40b9a298ab5af8480e.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (5004, 5004, 34, CAST(222.00 AS Decimal(10, 2)), CAST(22.00 AS Decimal(10, 2)), 1, N'teste', NULL)
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (6002, 6002, 3, CAST(32.00 AS Decimal(10, 2)), CAST(23.00 AS Decimal(10, 2)), 1, N'erwfgwfbg', N'/uploads/artes/6002_3729e2e4-61ae-4f92-ae1b-a3c7f17366e8')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (6003, 6003, 19, CAST(4.00 AS Decimal(10, 2)), CAST(3.00 AS Decimal(10, 2)), 1, N'WFef', N'/uploads/fotos/foto_6003.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (7002, 7002, 26, CAST(3.20 AS Decimal(10, 2)), CAST(2.20 AS Decimal(10, 2)), 1, N'SGFRaegbghrARGEG', N'http://localhost:5001/uploads/2965571f7af14a50907a27cae50096a7.webp')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (7003, 7003, 13, CAST(0.30 AS Decimal(10, 2)), CAST(0.30 AS Decimal(10, 2)), 1, N'ger', N'/uploads/fotos/foto_7003.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (9004, 9002, 18, CAST(0.35 AS Decimal(10, 2)), CAST(0.35 AS Decimal(10, 2)), 125, N'vai caralhoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo', NULL)
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (9005, 9002, 26, CAST(30.00 AS Decimal(10, 2)), CAST(45.00 AS Decimal(10, 2)), 132, N'vai se fudeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee', NULL)
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (10004, 10002, 15, CAST(23.00 AS Decimal(10, 2)), CAST(12.00 AS Decimal(10, 2)), 121, N'adsefvgaerbgtagbetgv', N'http://localhost:5001/uploads/6bb2e55652544957a935c1e1ba11a0c1.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (10005, 10002, 13, CAST(32.00 AS Decimal(10, 2)), CAST(23.00 AS Decimal(10, 2)), 132, N'efdssvfsdvadfbdfgvfdgvaDFga', N'http://localhost:5001/uploads/3ea13f6881444fd780673cd9dd0281a6.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (11004, 11002, 15, CAST(23.00 AS Decimal(10, 2)), CAST(32.00 AS Decimal(10, 2)), 132, N'efdw', N'http://localhost:5001/uploads/be48e73a44374f519bb01e06b22e68d7.jpg')
INSERT [dbo].[Pedido_Item] ([Item_ID], [Pedido_ID], [Tipo_Produto_ID], [Largura], [Altura], [Quantidade], [Observacao_Tecnica], [Caminho_Foto]) VALUES (12004, 12002, 39, CAST(45.00 AS Decimal(10, 2)), CAST(34.00 AS Decimal(10, 2)), 69, N'ewqgyrfdtyiu3q4fg6rgvco7WUERGF', N'http://localhost:5001/uploads/040a714acc034919962bb2e60caf8487.jpg')
SET IDENTITY_INSERT [dbo].[Pedido_Item] OFF
GO
SET IDENTITY_INSERT [dbo].[Status_Arte] ON 

INSERT [dbo].[Status_Arte] ([Status_Arte_ID], [Nome]) VALUES (1, N'Não Enviada')
INSERT [dbo].[Status_Arte] ([Status_Arte_ID], [Nome]) VALUES (2, N'Enviada')
INSERT [dbo].[Status_Arte] ([Status_Arte_ID], [Nome]) VALUES (3, N'Em Correção')
INSERT [dbo].[Status_Arte] ([Status_Arte_ID], [Nome]) VALUES (4, N'Aprovada')
INSERT [dbo].[Status_Arte] ([Status_Arte_ID], [Nome]) VALUES (5, N'Reprovada')
SET IDENTITY_INSERT [dbo].[Status_Arte] OFF
GO
SET IDENTITY_INSERT [dbo].[Status_Producao] ON 

INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (1, N'Pedido Criado', 1, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (2, N'Aguardando Arte', 2, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (3, N'Arte em Análise', 3, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (4, N'Arte Aprovada', 4, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (5, N'Em Producao', 5, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (6, N'Finalizada', 6, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (7, N'Entregue', 7, 1)
INSERT [dbo].[Status_Producao] ([Status_ID], [Nome], [Ordem], [Ativo]) VALUES (8, N'Arquivado', 8, 1)
SET IDENTITY_INSERT [dbo].[Status_Producao] OFF
GO
SET IDENTITY_INSERT [dbo].[Telefone] ON 

INSERT [dbo].[Telefone] ([Telefone_ID], [DDD], [Numero]) VALUES (1, N'44', N'997599415')
INSERT [dbo].[Telefone] ([Telefone_ID], [DDD], [Numero]) VALUES (2, N'016', N'996999699')
SET IDENTITY_INSERT [dbo].[Telefone] OFF
GO
SET IDENTITY_INSERT [dbo].[Tipo_Produto] ON 

INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (1, 1, N'Adesivo Vinil Impresso', N'Impressão digital em vinil fosco', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (2, 1, N'Adesivo Refletivo Impresso', N'Impressão digital em adesivo refletivo', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (3, 2, N'Placa ACM + Vinil Fosco', N'ACM com adesivação comum', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (4, 2, N'Placa ACM + Refletivo Branco', N'ACM com refletivo branco', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (5, 2, N'Placa ACM + Refletivo Amarelo', N'ACM com refletivo amarelo', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (6, 2, N'Placa Galvanizado + Vinil Fosco', N'Galvanizado com adesivação comum', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (7, 2, N'Placa Galvanizado + Refletivo Branco', N'Galvanizado com refletivo branco', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (8, 2, N'Placa Galvanizado + Refletivo Amarelo', N'Galvanizado com refletivo amarelo', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (9, 2, N'Placa Chapa 18 + Vinil Fosco', N'Chapa 18 com adesivação comum', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (10, 2, N'Placa Chapa 18 + Refletivo Branco', N'Chapa 18 com refletivo branco', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (11, 2, N'Placa Chapa 18 + Refletivo Amarelo', N'Chapa 18 com refletivo amarelo', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (12, 2, N'Placa de Sinalização com Poste', N'Placa com tubo para fixação', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (13, 3, N'Acrílico Impressão UV', N'Impressão UV direta em acrilico', 0, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (14, 3, N'Troféu de Acrílico', N'Acrílico recortado com impressão UV', 0, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (15, 3, N'Acrílico com Sobreposição', N'Acrílico com camadas sobrepostas', 0, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (16, 4, N'Alumínio Impressão UV', N'Impressão UV em chapa de alumínio', 0, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (17, 4, N'Alumínio Adesivado', N'Alumínio com aplicação de adesivo', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (18, 4, N'Aço Inox Gravado', N'Gravação em baixo relevo em inox', 0, 1, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (19, 4, N'Aço Inox Impressão UV', N'Impressão UV direta em inox', 0, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (20, 2, N'Placa PVC 3mm + Adesivo', N'PVC com aplicação de adesivo', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (21, 2, N'Placa PVC 3mm Impressão UV', N'Impressão direta no PVC', 0, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (22, 2, N'Placa ACM + Poste 2" + Vinil Fosco', N'Placa Informativa', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (23, 2, N'Placa ACM + Poste 2,5" + Vinil Fosco', N'Placa Informativa', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (24, 2, N'Placa Galvanizada + Poste 2" + Vinil Fosco', N'Placa Informativa ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (25, 2, N'Placa Galvanizada + Poste 2,5" + Vinil Fosco', N'Placa Informativa ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (26, 2, N'Placa Chapa 18 + Poste 2" + Vinil Fosco', N'Placa Informativa ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (27, 2, N'Placa Chapa 18 + Poste 2,5" + Vinil Fosco', N'Placa Informativa ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (28, 2, N'Placa ACM + Poste 2" + Refletivo Branco', N'Sinalização padrão (Ex: Pare, Sentido Proibido)', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (29, 2, N'Placa ACM + Poste 2.5" + Refletivo Branco', N'Sinalização padrão (Ex: Pare, Sentido Proibido)', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (30, 2, N'Placa Galvanizada + Poste 2" + Refletivo Branco', N'Sinalização  branca', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (31, 2, N'Placa Galvanizada + Poste 2,5" + Refletivo Branco', N'Sinalização  branca', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (32, 2, N'Placa Chapa 18 + Poste 2" + Refletivo Branco', N'Sinalização  branca', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (33, 2, N'Placa Chapa 18+ Poste 2,5" + Refletivo Branco', N'Sinalização  branca', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (34, 2, N'Placa ACM + Poste 2" + Refletivo Amarelo', N'Sinalização advertência (Ex: Curva, Pare à frente)', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (35, 2, N'Placa ACM + Poste 2.5" + Refletivo Amarelo', N'Sinalização advertência (Ex: Curva, Pare à frente)', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (36, 2, N'Placa Galvanizada + Poste 2" + Refletivo Amarelo', N'Sinalização advertência ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (37, 2, N'Placa Galvanizada + Poste 2,5" + Refletivo Amarelo', N'Sinalização advertência ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (38, 2, N'Placa Chapa 18 + Poste 2" + Refletivo Amarelo', N'Sinalização advertência ', 1, 0, 1)
INSERT [dbo].[Tipo_Produto] ([Tipo_Produto_ID], [Categoria_ID], [Nome], [Descricao_Tecnica], [Usa_Adesivo], [Usa_Mascara], [Ativo]) VALUES (39, 2, N'Placa Chapa 18 + Poste 2,5" + Refletivo Amarelo', N'Sinalização advertência ', 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Tipo_Produto] OFF
GO
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (1, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (2, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (3, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (3, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (4, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (4, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (5, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (5, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (6, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (6, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (7, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (7, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (8, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (8, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (9, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (9, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (10, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (10, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (11, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (11, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (12, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (12, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (13, 5)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (14, 6)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (15, 4)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (15, 9)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (19, 11)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (20, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (20, 10)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (21, 10)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (22, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (22, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (22, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (23, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (23, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (23, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (24, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (24, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (24, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (25, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (25, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (25, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (26, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (26, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (26, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (27, 1)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (27, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (27, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (28, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (28, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (28, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (29, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (29, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (29, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (30, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (30, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (30, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (31, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (31, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (31, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (32, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (32, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (32, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (33, 2)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (33, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (33, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (34, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (34, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (34, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (35, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (35, 14)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (35, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (36, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (36, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (36, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (37, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (37, 12)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (37, 17)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (38, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (38, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (38, 16)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (39, 3)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (39, 13)
INSERT [dbo].[Tipo_Produto_Material] ([Tipo_Produto_ID], [Material_ID]) VALUES (39, 17)
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([Usuario_ID], [Nome], [Funcao], [Login], [Senha], [Nivel_Acesso]) VALUES (1, N'God', N'God', N'God', N'HIqZPFh1CXELeez3lXTi', N'God')
INSERT [dbo].[Usuario] ([Usuario_ID], [Nome], [Funcao], [Login], [Senha], [Nivel_Acesso]) VALUES (2, N'Administrador', N'Gestão', N'admin', N'admin123', N'Admin')
INSERT [dbo].[Usuario] ([Usuario_ID], [Nome], [Funcao], [Login], [Senha], [Nivel_Acesso]) VALUES (3, N'Jose Porcellani', N'Produção', N'jose', N'123', N'Producao')
INSERT [dbo].[Usuario] ([Usuario_ID], [Nome], [Funcao], [Login], [Senha], [Nivel_Acesso]) VALUES (4, N'Vendedor Teste', N'Comercial', N'venda', N'venda123', N'Vendedor')
INSERT [dbo].[Usuario] ([Usuario_ID], [Nome], [Funcao], [Login], [Senha], [Nivel_Acesso]) VALUES (5, N'Designer Teste', N'Arte', N'arte', N'arte123', N'Arte')
INSERT [dbo].[Usuario] ([Usuario_ID], [Nome], [Funcao], [Login], [Senha], [Nivel_Acesso]) VALUES (1002, N'Matheus', N'Impressao', N'Mat', N'mat123', N'Vendedor')
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Cliente___C1F897319EEFD529]    Script Date: 10/03/2026 00:26:02 ******/
ALTER TABLE [dbo].[Cliente_PF] ADD UNIQUE NONCLUSTERED 
(
	[CPF] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Cliente___AA57D6B4F6A8D13D]    Script Date: 10/03/2026 00:26:02 ******/
ALTER TABLE [dbo].[Cliente_PJ] ADD UNIQUE NONCLUSTERED 
(
	[CNPJ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Pedido__3FEC44085A5D2F43]    Script Date: 10/03/2026 00:26:02 ******/
ALTER TABLE [dbo].[Pedido] ADD UNIQUE NONCLUSTERED 
(
	[OS_Externa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuario__5E55825BEAAA9BFA]    Script Date: 10/03/2026 00:26:02 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Arquivo_Arte] ADD  CONSTRAINT [DF_Arquivo_Arte_CaminhoArquivo]  DEFAULT ('/api/Producao/DownloadArte/temp') FOR [Caminho_Arquivo]
GO
ALTER TABLE [dbo].[Arquivo_Arte] ADD  CONSTRAINT [DF_Arquivo_Arte_DataUpload]  DEFAULT (sysdatetime()) FOR [DataUpload]
GO
ALTER TABLE [dbo].[Categoria_Produtos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Cliente_PF] ADD  DEFAULT (getdate()) FOR [Data_Cadastro]
GO
ALTER TABLE [dbo].[Cliente_PJ] ADD  DEFAULT (getdate()) FOR [Data_Cadastro]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Custos_Fixos] ADD  DEFAULT ((0)) FOR [Status_Pagamento]
GO
ALTER TABLE [dbo].[Historico_Status] ADD  DEFAULT (getdate()) FOR [Data_Mudanca]
GO
ALTER TABLE [dbo].[Material] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT (getdate()) FOR [Data_Pedido]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT ((0)) FOR [Valor_Total]
GO
ALTER TABLE [dbo].[Pedido] ADD  DEFAULT ((1)) FOR [Parcelas]
GO
ALTER TABLE [dbo].[Status_Producao] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Tipo_Produto] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ('Vendedor') FOR [Nivel_Acesso]
GO
ALTER TABLE [dbo].[Arquivo_Arte]  WITH CHECK ADD  CONSTRAINT [FK_Arquivo_Arte_Item_ID] FOREIGN KEY([Item_ID])
REFERENCES [dbo].[Pedido_Item] ([Item_ID])
GO
ALTER TABLE [dbo].[Arquivo_Arte] CHECK CONSTRAINT [FK_Arquivo_Arte_Item_ID]
GO
ALTER TABLE [dbo].[Arquivo_Arte]  WITH CHECK ADD  CONSTRAINT [FK_Arquivo_Arte_Status_Arte_ID] FOREIGN KEY([Status_Arte_ID])
REFERENCES [dbo].[Status_Arte] ([Status_Arte_ID])
GO
ALTER TABLE [dbo].[Arquivo_Arte] CHECK CONSTRAINT [FK_Arquivo_Arte_Status_Arte_ID]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Endereco] FOREIGN KEY([Endereco_ID])
REFERENCES [dbo].[Endereco] ([Endereco_ID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Cliente_Endereco]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_PF] FOREIGN KEY([PF_ID])
REFERENCES [dbo].[Cliente_PF] ([Cliente_PF_ID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Cliente_PF]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_PJ] FOREIGN KEY([PJ_ID])
REFERENCES [dbo].[Cliente_PJ] ([Cliente_PJ_ID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Cliente_PJ]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_Telefone] FOREIGN KEY([Telefone_ID])
REFERENCES [dbo].[Telefone] ([Telefone_ID])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Cliente_Telefone]
GO
ALTER TABLE [dbo].[Historico_Status]  WITH CHECK ADD  CONSTRAINT [FK_Historico_Status_Pedido_ID] FOREIGN KEY([Pedido_ID])
REFERENCES [dbo].[Pedido] ([Pedido_ID])
GO
ALTER TABLE [dbo].[Historico_Status] CHECK CONSTRAINT [FK_Historico_Status_Pedido_ID]
GO
ALTER TABLE [dbo].[Historico_Status]  WITH CHECK ADD  CONSTRAINT [FK_Historico_Status_Status_ID] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[Status_Producao] ([Status_ID])
GO
ALTER TABLE [dbo].[Historico_Status] CHECK CONSTRAINT [FK_Historico_Status_Status_ID]
GO
ALTER TABLE [dbo].[Historico_Status]  WITH CHECK ADD  CONSTRAINT [FK_Historico_StatusT_Usuario_ID] FOREIGN KEY([Usuario_ID])
REFERENCES [dbo].[Usuario] ([Usuario_ID])
GO
ALTER TABLE [dbo].[Historico_Status] CHECK CONSTRAINT [FK_Historico_StatusT_Usuario_ID]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Cliente_ID] FOREIGN KEY([Cliente_ID])
REFERENCES [dbo].[Clientes] ([Cliente_id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Cliente_ID]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Status_ID] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[Status_Producao] ([Status_ID])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Status_ID]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Vendedor] FOREIGN KEY([Vendedor_ID])
REFERENCES [dbo].[Usuario] ([Usuario_ID])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Vendedor]
GO
ALTER TABLE [dbo].[Pedido_Item]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Item_Pedido_ID] FOREIGN KEY([Pedido_ID])
REFERENCES [dbo].[Pedido] ([Pedido_ID])
GO
ALTER TABLE [dbo].[Pedido_Item] CHECK CONSTRAINT [FK_Pedido_Item_Pedido_ID]
GO
ALTER TABLE [dbo].[Pedido_Item]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Item_Tipo_Produto_ID] FOREIGN KEY([Tipo_Produto_ID])
REFERENCES [dbo].[Tipo_Produto] ([Tipo_Produto_ID])
GO
ALTER TABLE [dbo].[Pedido_Item] CHECK CONSTRAINT [FK_Pedido_Item_Tipo_Produto_ID]
GO
ALTER TABLE [dbo].[Tipo_Produto]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Produto_Categoria_ID] FOREIGN KEY([Categoria_ID])
REFERENCES [dbo].[Categoria_Produtos] ([Categoria_ID])
GO
ALTER TABLE [dbo].[Tipo_Produto] CHECK CONSTRAINT [FK_Tipo_Produto_Categoria_ID]
GO
ALTER TABLE [dbo].[Tipo_Produto_Material]  WITH CHECK ADD  CONSTRAINT [FK_TPM_Material] FOREIGN KEY([Material_ID])
REFERENCES [dbo].[Material] ([Material_ID])
GO
ALTER TABLE [dbo].[Tipo_Produto_Material] CHECK CONSTRAINT [FK_TPM_Material]
GO
ALTER TABLE [dbo].[Tipo_Produto_Material]  WITH CHECK ADD  CONSTRAINT [FK_TPM_Tipo_Produto] FOREIGN KEY([Tipo_Produto_ID])
REFERENCES [dbo].[Tipo_Produto] ([Tipo_Produto_ID])
GO
ALTER TABLE [dbo].[Tipo_Produto_Material] CHECK CONSTRAINT [FK_TPM_Tipo_Produto]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [CK_PJ_PF] CHECK  (([PJ_ID] IS NOT NULL AND [PF_ID] IS NULL OR [PJ_ID] IS NULL AND [PF_ID] IS NOT NULL))
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [CK_PJ_PF]
GO
/****** Object:  StoredProcedure [dbo].[SP_Atualizar_Status_Arte]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Atualizar_Status_Arte]
    @Item_ID INT,
    @Novo_Status_Arte_ID INT,
    @Usuario_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    EXEC sp_set_session_context @key = N'UsuarioId', @value = @Usuario_ID;

    -- Atualiza a arte
    UPDATE Arquivo_Arte SET Status_Arte_ID = @Novo_Status_Arte_ID WHERE Item_ID = @Item_ID;

    -- Descobre de qual pedido é essa arte
    DECLARE @PedidoId INT;
    SELECT @PedidoId = Pedido_ID FROM Pedido_Item WHERE Item_ID = @Item_ID;

    -- 🚀 A MÁGICA AQUI: Se a arte for Enviada (2) ou Em Correção (3), a OS vai pra Análise (3)!
    IF @Novo_Status_Arte_ID IN (2, 3)
    BEGIN
        UPDATE Pedido SET Status_ID = 3 WHERE Pedido_ID = @PedidoId;
    END

    -- Se for Aprovada (4), a OS vai pra Fila de Impressão (4)
    IF @Novo_Status_Arte_ID = 4
    BEGIN
        UPDATE Pedido SET Status_ID = 4 WHERE Pedido_ID = @PedidoId;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Atualizar_Status_Pedido]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Atualizar_Status_Pedido]
   @Pedido_ID INT,
   @Novo_Status_ID INT,
   @Usuario_ID INT,
   @Valor_Total DECIMAL(10,2) = NULL,
   @Forma_Pagamento NVARCHAR(50) = NULL,
   @Parcelas INT = NULL -- 🚀 AVISAMOS A PROCEDURE SOBRE AS PARCELAS
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        DECLARE @ctx VARBINARY(128) = CONVERT(VARBINARY(128), CONVERT(INT, @Usuario_ID));
        SET CONTEXT_INFO @ctx;

        IF @Novo_Status_ID = 7 AND (@Valor_Total IS NULL OR @Valor_Total <= 0 OR @Forma_Pagamento IS NULL)
        BEGIN
            RAISERROR ('Erro Financeiro: Informe o Valor Total e a Forma de Pagamento para entregar o pedido.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        UPDATE Pedido
        SET Status_ID = @Novo_Status_ID,
            Valor_Total = ISNULL(@Valor_Total, Valor_Total),
            Forma_Pagamento = ISNULL(@Forma_Pagamento, Forma_Pagamento),
            Parcelas = ISNULL(@Parcelas, Parcelas) -- 🚀 SALVAMOS A PARCELA AQUI
        WHERE Pedido_ID = @Pedido_ID;

        COMMIT TRANSACTION;
        PRINT 'Status e Financeiro atualizados com sucesso!';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Cadastrar_Cliente_Completo]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_Cadastrar_Cliente_Completo]
    @Nome NVARCHAR(50),
    @Email NVARCHAR(100),
    @DDD NVARCHAR(3),
    @NumeroTelefone NVARCHAR(9),
    @Cidade NVARCHAR(50),
    @CEP NVARCHAR(8),
    @Bairro NVARCHAR(60),
    @Rua NVARCHAR(100),
    @NumeroEndereco INT,
    @Documento NVARCHAR(14), -- CPF ou CNPJ
    @Tipo CHAR(2) -- 'PF' ou 'PJ'
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Iniciamos uma transação para garantir que ou salva TUDO ou NADA
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Inserir o Endereço e capturar o ID
        INSERT INTO Endereco (Cidade, CEP, Bairro, Rua, Numero)
        VALUES (@Cidade, @CEP, @Bairro, @Rua, @NumeroEndereco);
        DECLARE @EndID INT = SCOPE_IDENTITY();

        -- 2. Inserir o Telefone e capturar o ID
        INSERT INTO Telefone (DDD, Numero)
        VALUES (@DDD, @NumeroTelefone);
        DECLARE @TelID INT = SCOPE_IDENTITY();

        -- 3. Lógica para PF ou PJ
        DECLARE @PF_ID INT = NULL;
        DECLARE @PJ_ID INT = NULL;

        IF @Tipo = 'PF'
        BEGIN
            INSERT INTO Cliente_PF (CPF) VALUES (@Documento);
            SET @PF_ID = SCOPE_IDENTITY();
        END
        ELSE IF @Tipo = 'PJ'
        BEGIN
            INSERT INTO Cliente_PJ (CNPJ) VALUES (@Documento);
            SET @PJ_ID = SCOPE_IDENTITY();
        END

        -- 4. Inserir na tabela principal de Clientes vinculando os IDs anteriores
        INSERT INTO Clientes (Endereco_ID, Telefone_ID, PF_ID, PJ_ID, Email, Nome)
        VALUES (@EndID, @TelID, @PF_ID, @PJ_ID, @Email, @Nome);

        -- Se chegou aqui sem erros, confirma a gravação
        COMMIT TRANSACTION;
        PRINT 'Cliente cadastrado com sucesso!';

    END TRY
    BEGIN CATCH
        -- Se der qualquer erro (ex: CPF duplicado), desfaz tudo o que foi feito acima
        ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR (@ErrorMessage, 16, 1);
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Cadastrar_Tipo_Produto_Completo]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_Cadastrar_Tipo_Produto_Completo]
    @Categoria_ID INT,
    @Nome NVARCHAR(100),
    @Descricao_Tecnica NVARCHAR(255),
    @Usa_Adesivo BIT,
    @Usa_Mascara BIT,
    @Material_ID_1 INT,           -- Obrigatório
    @Material_ID_2 INT = NULL,    -- Opcional
    @Material_ID_3 INT = NULL     -- Opcional
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Insere o Tipo de Produto
        INSERT INTO Tipo_Produto (Categoria_ID, Nome, Descricao_Tecnica, Usa_Adesivo, Usa_Mascara)
        VALUES (@Categoria_ID, @Nome, @Descricao_Tecnica, @Usa_Adesivo, @Usa_Mascara);

        -- Captura o ID do produto que acabou de ser criado
        DECLARE @NovoProdutoID INT = SCOPE_IDENTITY();

        -- 2. Vincula o primeiro material (Sempre existe)
        INSERT INTO Tipo_Produto_Material (Tipo_Produto_ID, Material_ID)
        VALUES (@NovoProdutoID, @Material_ID_1);

        -- 3. Vincula o segundo material (Se houver)
        IF @Material_ID_2 IS NOT NULL
        BEGIN
            INSERT INTO Tipo_Produto_Material (Tipo_Produto_ID, Material_ID)
            VALUES (@NovoProdutoID, @Material_ID_2);
        END

        -- 4. Vincula o terceiro material (Se houver)
        IF @Material_ID_3 IS NOT NULL
        BEGIN
            INSERT INTO Tipo_Produto_Material (Tipo_Produto_ID, Material_ID)
            VALUES (@NovoProdutoID, @Material_ID_3);
        END

        COMMIT TRANSACTION;
        PRINT 'Produto e vínculos de materiais cadastrados com sucesso!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Criar_Pedido_Com_Item]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[SP_Criar_Pedido_Com_Item]
    @Cliente_ID INT,
    @OS_Externa NVARCHAR(6),
    @Vendedor_ID INT, -- Novo parâmetro obrigatório
    @Observacao_Geral NVARCHAR(255),
    @Tipo_Produto_ID INT,
    @Largura DECIMAL(10,2),
    @Altura DECIMAL(10,2),
    @Quantidade INT,
    @Observacao_Tecnica NVARCHAR(255),
    @Caminho_Foto NVARCHAR(255) = NULL -- Novo parâmetro opcional
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        INSERT INTO Pedido (Cliente_ID, OS_Externa, Status_ID, Vendedor_ID, Observacao_Geral)
        VALUES (@Cliente_ID, @OS_Externa, 1, @Vendedor_ID, @Observacao_Geral);
        
        DECLARE @NovoPedidoID INT = SCOPE_IDENTITY();

        INSERT INTO Pedido_Item (Pedido_ID, Tipo_Produto_ID, Largura, Altura, Quantidade, Observacao_Tecnica, Caminho_Foto)
        VALUES (@NovoPedidoID, @Tipo_Produto_ID, @Largura, @Altura, @Quantidade, @Observacao_Tecnica, @Caminho_Foto);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Validar_Login]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_Validar_Login]
    @Login NVARCHAR(50),
    @Senha NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        Usuario_ID AS UsuarioId,      -- Alias para bater com a classe C#
        Nome, 
        Funcao,
        Login,
        Senha,
        Nivel_Acesso AS NivelAcesso   -- AQUI ESTÁ O SEGREDO!
    FROM Usuario 
    WHERE Login = @Login AND Senha = @Senha;
END

GO
/****** Object:  StoredProcedure [dbo].[SP_Vincular_Arquivo_Arte]    Script Date: 10/03/2026 00:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[SP_Vincular_Arquivo_Arte]
    @Item_ID INT,
    @Nome_Arquivo NVARCHAR(100), -- Novo campo obrigatório
    @Caminho_Arquivo NVARCHAR(255),
    @Usuario_ID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY 
        IF EXISTS (SELECT 1 FROM Arquivo_Arte WHERE Item_ID = @Item_ID)
        BEGIN
            UPDATE Arquivo_Arte
            SET Nome_Arquivo = @Nome_Arquivo,
                Caminho_Arquivo = @Caminho_Arquivo,
                Status_Arte_ID = 2 
            WHERE Item_ID = @Item_ID;
        END
        ELSE
        BEGIN
            INSERT INTO Arquivo_Arte (Item_ID, Nome_Arquivo, Caminho_Arquivo, Status_Arte_ID)
            VALUES (@Item_ID, @Nome_Arquivo, @Caminho_Arquivo, 2);
        END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH 
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

GO
USE [master]
GO
ALTER DATABASE [Controle_Vendas] SET  READ_WRITE 
GO
