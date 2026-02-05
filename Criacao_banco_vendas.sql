/*
  SISTEMA DE CONTROLE DE VENDAS E PRODUÇÃO - COMUNICAÇÃO VISUAL
  Autor: [Jose Porcellani/GitHub]
  Data: Fevereiro 2026
*/

-- 1. CRIAÇÃO DO BANCO DE DADOS

CREATE DATABASE Controle_Vendas;

USE Controle_Vendas;

CREATE TABLE Cliente_PJ(
Cliente_PJ_ID INT IDENTITY(1,1) PRIMARY KEY,
CNPJ NVARCHAR (14) NOT NULL UNIQUE,
Data_Cadastro DATETIME DEFAULT GETDATE() NOT NULL
);

CREATE TABLE Cliente_PF(
Cliente_PF_ID INT IDENTITY(1,1) PRIMARY KEY,
CPF NVARCHAR (11) NOT NULL UNIQUE,
Data_Cadastro DATETIME DEFAULT GETDATE() NOT NULL
);


CREATE TABLE Endereco(
Endereco_ID INT IDENTITY(1,1) PRIMARY KEY,
Cidade NVARCHAR (50) NOT NULL,
CEP NVARCHAR (8) NOT NULL,
Bairro NVARCHAR (60) NOT NULL,
Rua NVARCHAR (100) NOT NULL,
Numero INT NULL,
Referencia NVARCHAR (255)
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
CONSTRAINT FK_Arquivo_Arte_Status_Arte_ID FOREIGN KEY (Status_Arte_ID) REFERENCES Status_Arte(Status_Arte_ID)
);
CREATE TABLE Usuario(
Usuario_ID INT IDENTITY (1,1) PRIMARY KEY,
Nome NVARCHAR(255) NOT NULL,
Funcao NVARCHAR (50) NOT NULL
);

CREATE TABLE Historico_Status(
Historico_ID INT IDENTITY (1,1) PRIMARY KEY ,
Pedido_ID INT NOT NULL,
Status_ID INT NOT NULL,
Data_Mudanca DATETIME DEFAULT GETDATE(),
Usuario_ID INT NOT NULL,
CONSTRAINT FK_Historico_Status_Pedido_ID FOREIGN KEY (Pedido_ID) REFERENCES Pedido(Pedido_ID),
CONSTRAINT FK_Historico_Status_Status_ID FOREIGN KEY (Status_ID) REFERENCES Status_Producao(Status_ID),
CONSTRAINT FK_Historico_StatusT_Usuario_ID FOREIGN KEY (Usuario_ID) REFERENCES Usuario(Usuario_ID)
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

-- 3. CARGA DE DADOS INICIAIS (CONFIGURAÇÕES)
INSERT INTO Usuario (Nome, Funcao) VALUES
('Administrador', 'Gestão'),
('Designer', 'Arte'),
('Impressor', 'Produção'),
('Vendedor', 'Comercial');

INSERT INTO Status_Producao (Nome, Ordem)
VALUES 
('Pedido Criado', 1),
('Aguardando Arte', 2),
('Arte em Análise', 3),
('Arte Aprovada', 4),
('Em Producao', 5),
('Finalizada', 6),
('Entregue', 7);

INSERT INTO Status_Arte (Nome)
Values
('Não Enviada'),
('Enviada'),
('Em Correção'),
('Aprovada'),
('Reprovada');

INSERT INTO Categoria_Produtos (Nome, Descricao)
VALUES
('Adesivo', 'Produtos baseados em adesivo'),
('Placas', 'Placas de sinalização e comunicação visual'),
('Acrilico', 'Produtos fabricados em acrilico'),
('Metal', 'Produtos fabricados em metal'),
('UV','Produtos fabricados em impressão UV');
 


INSERT INTO Material (Nome, Descricao)
VALUES 
('Adesivo Vinil Fosco', 'Vinil fosco para impressão'), --ID 1
('Adesivo Refletivo GTP Branco', 'Refletivo branco GTP'),--ID 2 
('Adesivo GTP Amarelo', 'Refletivo amarelo GTP'),--ID 3 
('Acrílico 2mm', 'Acrílico espessura 2mm'),--ID 4
('Acrílico 3mm', 'Acrílico espessura 3mm'),--ID 5
('Acrílico 6mm Branco', 'Acrílico branco 6mm'),--ID 6
('Acrílico 6mm Cristal', 'Acrílico cristal 6mm'),--ID 7
('Acrílico 6mm Preto', 'Acrílico preto 6mm'),--ID 8
('Acrílico Espelhado Dourado 2mm', 'Acrílico espelhado dourado'),--ID 9
('PVC 3mm', 'Placa em PVC expandido'),--ID 10
('Aço Inox Escovado', 'Inox acabamento escovado'),--ID 11
('Chapa Galvanizada', 'Chapa galvanizada'),--ID 12
('Chapa 18', 'Chapa metálica nº 18'),--ID 13
('ACM 3mm', 'Alumínio composto 3mm'),--ID 14
('Alumínio', 'Chapa de alumínio'),--ID 15
('Tubo 2"', 'Tubo metálico 2 polegadas'),--ID 16
('Tubo 2.5"', 'Tubo metálico 2.5 polegadas');--ID 17



INSERT INTO Tipo_Produto (Categoria_ID, Nome, Descricao_Tecnica, Usa_Adesivo, Usa_Mascara) VALUES 
(1, 'Adesivo Vinil Impresso', 'Impressão digital em vinil fosco', 1, 0), -- ID 1
(1, 'Adesivo Refletivo Impresso', 'Impressão digital em adesivo refletivo', 1, 0), -- ID 2
(2, 'Placa ACM + Vinil Fosco', 'ACM com adesivação comum', 1, 0), -- ID 3
(2, 'Placa ACM + Refletivo Branco', 'ACM com refletivo branco', 1, 0), -- ID 4
(2, 'Placa ACM + Refletivo Amarelo', 'ACM com refletivo amarelo', 1, 0), -- ID 5
(2, 'Placa Galvanizado + Vinil Fosco', 'Galvanizado com adesivação comum', 1, 0), -- ID 6
(2, 'Placa Galvanizado + Refletivo Branco', 'Galvanizado com refletivo branco', 1, 0), -- ID 7
(2, 'Placa Galvanizado + Refletivo Amarelo', 'Galvanizado com refletivo amarelo', 1, 0), --ID 8
(2, 'Placa Chapa 18 + Vinil Fosco', 'Chapa 18 com adesivação comum', 1, 0), -- ID 9
(2, 'Placa Chapa 18 + Refletivo Branco', 'Chapa 18 com refletivo branco', 1, 0), -- ID 10
(2, 'Placa Chapa 18 + Refletivo Amarelo', 'Chapa 18 com refletivo amarelo', 1, 0),-- ID 11
(2, 'Placa de Sinalização com Poste', 'Placa com tubo para fixação', 1, 0), -- ID 12
(3, 'Acrílico Impressão UV', 'Impressão UV direta em acrilico', 0, 0), -- ID 13
(3, 'Troféu de Acrílico', 'Acrílico recortado com impressão UV', 0, 0), -- ID 14
(3, 'Acrílico com Sobreposição', 'Acrílico com camadas sobrepostas', 0, 0), -- ID 15
(4, 'Alumínio Impressão UV', 'Impressão UV em chapa de alumínio', 0, 0), -- ID 16
(4, 'Alumínio Adesivado', 'Alumínio com aplicação de adesivo', 1, 0), -- ID 17
(4, 'Aço Inox Gravado', 'Gravação em baixo relevo em inox', 0, 1), -- ID 18
(4, 'Aço Inox Impressão UV', 'Impressão UV direta em inox', 0, 0), -- ID 19
(2, 'Placa PVC 3mm + Adesivo', 'PVC com aplicação de adesivo', 1, 0), -- ID 20
(2, 'Placa PVC 3mm Impressão UV', 'Impressão direta no PVC', 0, 0), -- ID 21
(2, 'Placa ACM + Poste 2" + Vinil Fosco', 'Placa Informativa', 1, 0), -- ID 22
(2, 'Placa ACM + Poste 2,5" + Vinil Fosco', 'Placa Informativa', 1, 0), -- ID 23
(2, 'Placa Galvanizada + Poste 2" + Vinil Fosco', 'Placa Informativa ', 1, 0), -- ID 24
(2, 'Placa Galvanizada + Poste 2,5" + Vinil Fosco', 'Placa Informativa ', 1, 0), -- ID 25
(2, 'Placa Chapa 18 + Poste 2" + Vinil Fosco', 'Placa Informativa ', 1, 0), -- ID 26
(2, 'Placa Chapa 18 + Poste 2,5" + Vinil Fosco', 'Placa Informativa ', 1, 0), -- ID 27
(2, 'Placa ACM + Poste 2" + Refletivo Branco', 'Sinalização padrão (Ex: Pare, Sentido Proibido)', 1, 0), -- ID 28
(2, 'Placa ACM + Poste 2.5" + Refletivo Branco', 'Sinalização padrão (Ex: Pare, Sentido Proibido)', 1, 0), -- ID 29
(2, 'Placa Galvanizada + Poste 2" + Refletivo Branco', 'Sinalização  branca', 1, 0), -- ID 30
(2, 'Placa Galvanizada + Poste 2,5" + Refletivo Branco', 'Sinalização  branca', 1, 0), --ID 31
(2, 'Placa Chapa 18 + Poste 2" + Refletivo Branco', 'Sinalização  branca', 1, 0), --ID 32
(2, 'Placa Chapa 18+ Poste 2,5" + Refletivo Branco', 'Sinalização  branca', 1, 0), --ID 33
(2, 'Placa ACM + Poste 2" + Refletivo Amarelo', 'Sinalização advertência (Ex: Curva, Pare à frente)', 1, 0),--ID 34
(2, 'Placa ACM + Poste 2.5" + Refletivo Amarelo', 'Sinalização advertência (Ex: Curva, Pare à frente)', 1, 0),-- ID 35
(2, 'Placa Galvanizada + Poste 2" + Refletivo Amarelo', 'Sinalização advertência ', 1, 0), -- ID 36
(2, 'Placa Galvanizada + Poste 2,5" + Refletivo Amarelo', 'Sinalização advertência ', 1, 0), -- ID 37
(2, 'Placa Chapa 18 + Poste 2" + Refletivo Amarelo', 'Sinalização advertência ', 1, 0), -- ID 38
(2, 'Placa Chapa 18 + Poste 2,5" + Refletivo Amarelo', 'Sinalização advertência ', 1, 0); -- ID 39


-- VINCULANDO MATERIAIS AOS PRODUTOS (IDs seguindo a ordem de inserção acima)
INSERT INTO Tipo_Produto_Material (Tipo_Produto_ID, Material_ID) VALUES
-- Adesivos e Placas Simples (IDs 1 a 21)
(1, 1), (2, 2), (3, 14), (3, 1), (4, 14), (4, 2), (5, 14), (5, 3),
(6, 12), (6, 1), (7, 12), (7, 2), (8, 12), (8, 3),
(9, 13), (9, 1), (10, 13), (10, 2), (11, 13), (11, 3),
(13, 5), (14, 6), (15, 4), (15, 9), (20, 10), (20, 1), (21, 10),
-- KITS CONDOMÍNIO (Vinil Fosco + Chapa + Tubo)
(22, 14), (22, 1), (22, 16), -- ACM + Poste 2" + Fosco
(23, 14), (23, 1), (23, 17), -- ACM + Poste 2.5" + Fosco
(24, 12), (24, 1), (24, 16), -- Galva + Poste 2" + Fosco
(25, 12), (25, 1), (25, 17), -- Galva + Poste 2.5" + Fosco
(26, 13), (26, 1), (26, 16), -- Chapa 18 + Poste 2" + Fosco
(27, 13), (27, 1), (27, 17), -- Chapa 18 + Poste 2.5" + Fosco
-- KITS TRÂNSITO BRANCO (Refletivo Branco + Chapa + Tubo)
(28, 14), (28, 2), (28, 16), -- ACM + Poste 2" + Branco
(29, 14), (29, 2), (29, 17), -- ACM + Poste 2.5" + Branco
(30, 12), (30, 2), (30, 16), -- Galva + Poste 2" + Branco
(31, 12), (31, 2), (31, 17), -- Galva + Poste 2.5" + Branco
(32, 13), (32, 2), (32, 16), -- Chapa 18 + Poste 2" + Branco
(33, 13), (33, 2), (33, 17), -- Chapa 18 + Poste 2.5" + Branco
-- KITS TRÂNSITO AMARELO (Refletivo Amarelo + Chapa + Tubo)
(34, 14), (34, 3), (34, 16), -- ACM + Poste 2" + Amarelo
(35, 14), (35, 3), (35, 17), -- ACM + Poste 2.5" + Amarelo
(36, 12), (36, 3), (36, 16), -- Galva + Poste 2" + Amarelo
(37, 12), (37, 3), (37, 17), -- Galva + Poste 2.5" + Amarelo
(38, 13), (38, 3), (38, 16), -- Chapa 18 + Poste 2" + Amarelo
(39, 13), (39, 3), (39, 17); -- Chapa 18 + Poste 2.5" + Amarelo


-- 4. CRIAÇÃO DAS VIEWS (GENTE QUE PRODUZ)
CREATE OR ALTER VIEW VW_Fila_Arte AS 
SELECT 
    P.Os_Externa AS OS, 
    C.Nome AS Cliente, 
    TP.Nome AS Produto,
    PI.Largura, PI.Altura, PI.Quantidade, 
    PI.Observacao_Tecnica, 
    ISNULL(SA.Nome, 'Pendente / Sem Arquivo') AS Status_Arte 
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID 
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID -- LEFT JOIN não "esconde" o pedido
LEFT JOIN Status_Arte SA ON AA.Status_Arte_ID = SA.Status_Arte_ID
WHERE P.Status_ID IN (1, 2, 3); 

-- 2. FILA DE PRODUÇÃO (Garante que tudo em produção apareça)
CREATE OR ALTER VIEW VW_Fila_Producao AS 
SELECT 
    P.OS_Externa AS OS, 
    C.Nome AS Cliente, 
    TP.Nome AS Produto,
    PI.Largura, PI.Altura, PI.Quantidade, 
    ISNULL(AA.Caminho_Arquivo, 'Arte não vinculada') AS Local_da_Arte, 
    PI.Observacao_Tecnica
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID 
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID -- Mudado para LEFT JOIN
WHERE P.Status_ID IN (4, 5); 
GO

-- VIEW: FILA DE IMPRESSÃO
CREATE OR ALTER VIEW VW_Fila_Impressao AS 
SELECT 
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    TP.Nome AS Produto,
    (SELECT STRING_AGG(M.Nome, ' / ') FROM 
    Tipo_Produto_Material TPM 
    JOIN Material M  ON TPM.Material_ID = M.Material_ID
    WHERE TPM.Tipo_Produto_ID = TP.Tipo_Produto_ID) AS Material_Base,
    PI.Largura, PI.Altura, PI.Quantidade, AA.Caminho_Arquivo AS Link_Arte, PI.Observacao_Tecnica
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_ID
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID 
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID
WHERE P.Status_ID = 4;

-- 1. MONITORAMENTO GLOBAL: O "Onde está meu pedido?" (Visão para o Vendedor/Dono)
CREATE VIEW VW_Monitoramento_Global AS 
SELECT P.OS_Externa AS OS, C.Nome AS Cliente, TP.Nome AS Produto, SP.Nome AS Status_Producao, FORMAT(P.Data_Pedido, 'dd/MM/yyyy HH:mm') AS Data_Entrada
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID;

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

USE Controle_Vendas;
GO
  --VIEW: Pesquisa de clientes
CREATE OR ALTER VIEW VW_Pesquisa_Clientes_Vendas AS
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
SELECT * FROM VW_Pesquisa_Clientes_Vendas;

  -- VIEW: Historico de Pedidos
CREATE OR ALTER VIEW VW_Historico_Pedidos_Cliente AS
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

