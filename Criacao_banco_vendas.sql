/*
  SISTEMA DE CONTROLE DE VENDAS E PRODUÇÃO - COMUNICAÇÃO VISUAL
  Autor: [Jose Porcellani/GitHub]
  Data: Fevereiro 2026
*/

-- 1. CRIAÇÃO DO BANCO DE DADOS
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Controle_Vendas')
BEGIN
    CREATE DATABASE Controle_Vendas;
END


USE Controle_Vendas;


-- 2. CRIAÇÃO DAS TABELAS BASE
CREATE TABLE Cliente_PJ (
    Cliente_PJ_ID INT IDENTITY(1,1) PRIMARY KEY,
    CNPJ NVARCHAR(14) NOT NULL UNIQUE,
    Data_Cadastro DATETIME DEFAULT GETDATE() NOT NULL
);

CREATE TABLE Cliente_PF (
    Cliente_PF_ID INT IDENTITY(1,1) PRIMARY KEY,
    CPF NVARCHAR(11) NOT NULL UNIQUE,
    Data_Cadastro DATETIME DEFAULT GETDATE() NOT NULL
);

CREATE TABLE Endereco (
    Endereco_ID INT IDENTITY(1,1) PRIMARY KEY,
    Cidade NVARCHAR(50) NOT NULL,
    CEP NVARCHAR(8) NOT NULL,
    Bairro NVARCHAR(60) NOT NULL,
    Rua NVARCHAR(100) NOT NULL,
    Numero INT NULL,
    Referencia NVARCHAR(255)
);

CREATE TABLE Telefone(
    Telefone_ID INT IDENTITY (1,1) PRIMARY KEY,
    DDD NVARCHAR(3) NOT NULL,
    Numero NVARCHAR (9) NOT NULL
);

CREATE TABLE Clientes (
    Cliente_id INT IDENTITY (1,1) PRIMARY KEY,
    Endereco_ID INT NOT NULL,
    Telefone_ID INT NOT NULL,
    PF_ID INT NULL,
    PJ_ID INT NULL,
    Email NVARCHAR (100) NOT NULL,
    Nome NVARCHAR (50) NOT NULL,
    Ativo bit DEFAULT (1) NOT NULL,
    CONSTRAINT FK_Cliente_Endereco FOREIGN KEY (Endereco_ID) REFERENCES Endereco(Endereco_ID),
    CONSTRAINT FK_Cliente_Telefone FOREIGN KEY (Telefone_ID) REFERENCES Telefone(Telefone_ID),
    CONSTRAINT FK_Cliente_PJ FOREIGN KEY (PJ_ID) REFERENCES Cliente_PJ(Cliente_PJ_ID),
    CONSTRAINT FK_Cliente_PF FOREIGN KEY (PF_ID) REFERENCES Cliente_PF(Cliente_PF_ID),
    CONSTRAINT CK_PJ_PF CHECK ((PJ_ID IS NOT NULL AND PF_ID IS NULL) OR (PJ_ID IS NULL AND PF_ID IS NOT NULL))
);

CREATE TABLE Categoria_Produtos(
    Categoria_ID INT IDENTITY (1,1) PRIMARY KEY,
    Nome NVARCHAR (100) NOT NULL,
    Descricao NVARCHAR (255) NOT NULL, 
    Ativo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Tipo_Produto(
    Tipo_Produto_ID INT IDENTITY (1,1) PRIMARY KEY,
    Categoria_ID INT NOT NULL,
    Nome NVARCHAR(100) NOT NULL,
    Descricao_Tecnica NVARCHAR(255) NOT NULL,
    Usa_Adesivo BIT,
    Usa_Mascara BIT,
    Ativo BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Tipo_Produto_Categoria_ID FOREIGN KEY (Categoria_ID) References Categoria_Produtos(Categoria_ID)
);

CREATE TABLE Status_Producao(
    Status_ID INT IDENTITY (1,1) PRIMARY KEY,
    Nome NVARCHAR (100) NOT NULL,
    Ordem INT NOT NULL,
    Ativo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Pedido(
    Pedido_ID INT IDENTITY(1,1) PRIMARY KEY,
    Cliente_ID INT NOT NULL,
    OS_Externa NVARCHAR (6) NOT NULL UNIQUE,
    Data_Pedido DATETIME DEFAULT GETDATE(),
    Status_ID INT NOT NULL,
    Observacao_Geral NVARCHAR (255) NOT NULL,
    CONSTRAINT FK_Pedido_Cliente_ID FOREIGN KEY (Cliente_ID) REFERENCES Clientes(Cliente_id),
    CONSTRAINT FK_Pedido_Status_ID FOREIGN KEY (Status_ID) REFERENCES Status_Producao(Status_ID)
);

CREATE TABLE Pedido_Item(
    Item_ID INT IDENTITY(1,1) PRIMARY KEY,
    Pedido_ID INT NOT NULL,
    Tipo_Produto_ID INT NOT NULL,
    Largura DECIMAL(10,2) NOT NULL,
    Altura DECIMAL (10,2) NOT NULL,
    Quantidade INT NOT NULL,
    Observacao_Tecnica NVARCHAR(255) NOT NULL,
    CONSTRAINT FK_Pedido_Item_Pedido_ID FOREIGN KEY (Pedido_ID) REFERENCES Pedido(Pedido_ID),
    CONSTRAINT FK_Pedido_Item_Tipo_Produto_ID FOREIGN KEY (Tipo_Produto_ID) REFERENCES Tipo_Produto(Tipo_Produto_ID)
);

CREATE TABLE Status_Arte(
    Status_Arte_ID INT IDENTITY (1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL
);

CREATE TABLE Arquivo_Arte(
    Arquivo_ID INT IDENTITY (1,1) PRIMARY KEY ,
    Item_ID INT NOT NULL,
    Nome_Arquivo NVARCHAR(100) NOT NULL,
    Caminho_Arquivo NVARCHAR (255) NOT NULL,
    Status_Arte_ID INT NOT NULL,
    CONSTRAINT FK_Arquivo_Arte_Item_ID FOREIGN KEY (Item_ID) REFERENCES Pedido_Item(Item_ID),
    CONSTRAINT FK_Arquivo_Arte_Status_Arte_ID FOREIGN KEY (Status_Arte_ID) REFERENCES Status_Arte(Status_Arte_ID),
    CONSTRAINT UQ_Arquivo_Unico UNIQUE (Item_ID, Caminho_Arquivo)
);

CREATE TABLE Material(
    Material_ID INT IDENTITY (1,1) PRIMARY KEY,
    Nome NVARCHAR(50) NOT NULL,
    Descricao NVARCHAR(255),
    Ativo BIT NOT NULL DEFAULT 1
);

CREATE TABLE Tipo_Produto_Material(
    Tipo_Produto_ID INT NOT NULL,
    Material_ID INT NOT NULL ,
    CONSTRAINT PK_Tipo_Produto_Material PRIMARY KEY (Tipo_Produto_ID, Material_ID),
    CONSTRAINT FK_TPM_Tipo_Produto FOREIGN KEY (Tipo_Produto_ID) REFERENCES Tipo_Produto(Tipo_Produto_ID),
    CONSTRAINT FK_TPM_Material FOREIGN KEY (Material_ID) REFERENCES Material(Material_ID)
);

CREATE TABLE Usuario(
    Usuario_ID INT IDENTITY (1,1) PRIMARY KEY,
    Nome NVARCHAR(255) NOT NULL,
    Funcao NVARCHAR (50) NOT NULL
);



-- 3. CARGA DE DADOS INICIAIS (CONFIGURAÇÕES)
INSERT INTO Status_Producao (Nome, Ordem) VALUES 
('Pedido Criado', 1), ('Aguardando Arte', 2), ('Arte em Análise', 3), 
('Arte Aprovada', 4), ('Em Producao', 5), ('Finalizada', 6), ('Entregue', 7);

INSERT INTO Status_Arte (Nome) VALUES 
('Não Enviada'), ('Enviada'), ('Em Correção'), ('Aprovada'), ('Reprovada');

INSERT INTO Categoria_Produtos (Nome, Descricao) VALUES
('Adesivo', 'Produtos baseados em adesivo'),
('Placas', 'Placas de sinalização e comunicação visual'),
('Acrilico', 'Produtos fabricados em acrilico'),
('Metal', 'Produtos fabricados em metal');

INSERT INTO Material (Nome, Descricao) VALUES 
('Adesivo Vinil Fosco', 'Vinil fosco para impressão'),
('Adesivo Refletivo GTP Branco', 'Refletivo branco GTP'),
('Acrílico 3mm', 'Acrílico espessura 3mm'),
('Aço Inox Escovado', 'Inox acabamento escovado'),
('Chapa Galvanizada', 'Chapa galvanizada'),
('ACM 3mm', 'Alumínio composto 3mm'),
('Alumínio', 'Chapa de alumínio'),
('Tubo 2"', 'Tubo metálico 2 polegadas'),
('PVC 3mm', 'Placa de PVC expandido 3mm');

INSERT INTO Tipo_Produto (Categoria_ID, Nome, Descricao_Tecnica, Usa_Adesivo, Usa_Mascara) VALUES 
(1, 'Adesivo Vinil Impresso', 'Impressão digital em vinil fosco', 1, 0),
(2, 'Placa de Sinalização','Placa em ACM, chapa ou galvanizada', 1, 0),
(2, 'Placa de Sinalização com Poste', 'Placa com tubo para fixação', 1, 0),
(3, 'Acrílico com Sobreposição', 'Acrílico com camadas sobrepostas', 0, 0),
(4, 'Alumínio Adesivado', 'Alumínio com aplicação de adesivo', 1, 0),
(4, 'Aço Inox Gravado', 'Gravação em baixo relevo em inox', 0, 1),
(2, 'Placa PVC 3mm + Adesivo', 'Placa de PVC com aplicação de vinil fosco', 1, 0);

-- VINCULANDO MATERIAIS AOS PRODUTOS (IDs seguindo a ordem de inserção acima)
INSERT INTO Tipo_Produto_Material (Tipo_Produto_ID, Material_ID) VALUES
(1, 1), (2, 5), (2, 6), (3, 5), (3, 8), (4, 3), (7, 9), (7, 1);

INSERT INTO Usuario (Nome, Funcao) VALUES ('Administrador', 'Gestão'), ('Designer', 'Arte');



-- 4. CRIAÇÃO DAS VIEWS (GENTE QUE PRODUZ)

-- VIEW: FILA DA ARTE
CREATE VIEW VW_Fila_Arte AS 
SELECT 
    P.Os_Externa AS OS, C.Nome AS Cliente, TP.Nome AS Produto,
    STRING_AGG(CAST(M.Nome AS VARCHAR(MAX)), ' / ') AS Material_Base, 
    PI.Largura, PI.Altura, PI.Quantidade,
    PI.Observacao_Tecnica AS Descricao_Tecnica, 
    ISNULL(SA.Nome, 'Sem Arte') AS Situacao_Arte 
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID 
LEFT JOIN Tipo_Produto_Material TPM ON TP.Tipo_Produto_ID = TPM.Tipo_Produto_ID
LEFT JOIN Material M ON TPM.Material_ID = M.Material_ID
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID
LEFT JOIN Status_Arte SA ON AA.Status_Arte_ID = SA.Status_Arte_ID
WHERE P.Status_ID IN (1, 2, 3)
GROUP BY P.Os_Externa, C.Nome, TP.Nome, PI.Largura, PI.Altura, PI.Quantidade, PI.Observacao_Tecnica, SA.Nome;


-- VIEW: FILA DE IMPRESSÃO
CREATE VIEW VW_Fila_Impressao AS 
SELECT 
    P.OS_Externa AS OS, TP.Nome AS Produto,
    STRING_AGG(CAST(M.Nome AS VARCHAR(MAX)), ' / ') AS Material_Base, 
    PI.Largura, PI.Altura, PI.Quantidade,
    AA.Caminho_Arquivo AS Link_Arte,
    PI.Observacao_Tecnica AS Descricao_Tecnica 
FROM Pedido P
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID 
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
LEFT JOIN Tipo_Produto_Material TPM ON TP.Tipo_Produto_ID = TPM.Tipo_Produto_ID
LEFT JOIN Material M ON TPM.Material_ID = M.Material_ID
JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID
WHERE P.Status_ID = 4
GROUP BY P.OS_Externa, TP.Nome, PI.Largura, PI.Altura, PI.Quantidade, AA.Caminho_Arquivo, PI.Observacao_Tecnica;


USE Controle_Vendas;
GO

-- 1. MONITORAMENTO GLOBAL: O "Onde está meu pedido?" (Visão para o Vendedor/Dono)
CREATE VIEW VW_Monitoramento_Global AS 
SELECT 
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    TP.Nome AS Produto,
    SP.Nome AS Status_Producao,
    FORMAT(P.Data_Pedido, 'dd/MM/yyyy HH:mm') AS Data_Entrada -- Data formatada Brasil
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
GROUP BY P.OS_Externa, C.Nome, TP.Nome, SP.Nome, P.Data_Pedido;


-- 2. FILA DE ACABAMENTO: O que já foi impresso e está sendo montado
CREATE VIEW VW_Em_Producao AS 
SELECT
   P.OS_Externa AS OS,
   C.Nome AS Cliente,
   TP.Nome AS Produto,
   PI.Largura, PI.Altura, PI.Quantidade,
   SP.Nome AS Etapa_Atual
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
WHERE P.Status_ID IN (5, 6) -- 5: Em Produção (Acabamento), 6: Finalizado (Aguardando Retirada)
GROUP BY P.OS_Externa, C.Nome, TP.Nome, PI.Largura, PI.Altura, PI.Quantidade, SP.Nome;


-- VIEW: DASHBOARD GESTÃO
CREATE VIEW VW_Dashboard_Gestao AS 
SELECT 
    SP.Nome AS Etapa,
    COUNT(DISTINCT P.Pedido_ID) AS Total_Pedidos 
FROM Status_Producao SP
LEFT JOIN Pedido P ON SP.Status_ID = P.Status_ID
GROUP BY SP.Nome, SP.Ordem;


