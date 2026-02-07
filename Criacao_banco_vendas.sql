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
 Valor_Total DECIMAL(10,2) DEFAULT 0,
Forma_Pagamento NVARCHAR(50),
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
Funcao NVARCHAR (50) NOT NULL,
Login NVARCHAR(50) UNIQUE,
Senha NVARCHAR(255)
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

INSERT INTO Usuario (Nome, Funcao, Login, Senha) VALUES
('Administrador', 'Gestão', 'admin', 'admin123'),
('Jose Porcellani', 'Produção', 'jose', '123'),
('Vendedor Teste', 'Comercial', 'venda', 'venda123'),
('Designer Teste', 'Arte', 'arte', 'arte123');

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
    DATEDIFF(day, P.Data_Pedido, GETDATE()) AS Dias_Aguardando_Arte,
    PI.Largura, PI.Altura, PI.Quantidade, 
    PI.Observacao_Tecnica, 
    P.Observacao_Geral,
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
    PI.Observacao_Tecnica,
    P.Observacao_Geral
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID 
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
LEFT JOIN Arquivo_Arte AA ON PI.Item_ID = AA.Item_ID -- Mudado para LEFT JOIN
WHERE P.Status_ID IN (4, 5); 


-- VIEW: FILA DE IMPRESSÃO
CREATE OR ALTER VIEW VW_Fila_Impressao AS 
SELECT 
    P.OS_Externa AS OS,
    C.Nome AS Cliente,
    TP.Nome AS Produto,
    DATEDIFF(day, P.Data_Pedido, GETDATE()) AS Dias_em_Impressao,
    (SELECT STRING_AGG(M.Nome, ' / ') FROM 
    Tipo_Produto_Material TPM 
    JOIN Material M  ON TPM.Material_ID = M.Material_ID
    WHERE TPM.Tipo_Produto_ID = TP.Tipo_Produto_ID) AS Material_Base,
    PI.Largura, PI.Altura, PI.Quantidade,
    AA.Caminho_Arquivo AS Link_Arte,
    P.Observacao_Geral,
    PI.Observacao_Tecnica
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
CREATE OR ALTER VIEW VW_Em_Producao AS 
SELECT
   P.OS_Externa AS OS,
   C.Nome AS Cliente,
   TP.Nome AS Produto,
   DATEDIFF(day, P.Data_Pedido, GETDATE()) AS Dias_em_Producao,
   PI.Largura, PI.Altura, PI.Quantidade,
   SP.Nome AS Etapa_Atual,
   P.Observacao_Geral
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
WHERE P.Status_ID IN (5, 6) -- 5: Em Produção (Acabamento), 6: Finalizado (Aguardando Retirada)
GROUP BY P.OS_Externa, C.Nome, TP.Nome, P.Data_Pedido, PI.Largura, PI.Altura, PI.Quantidade, SP.Nome, P.Observacao_Geral;

-- VIEW: DASHBOARD GESTÃO
CREATE VIEW VW_Dashboard_Gestao AS 
SELECT 
    SP.Nome AS Etapa,
    COUNT(DISTINCT P.Pedido_ID) AS Total_Pedidos 
FROM Status_Producao SP
LEFT JOIN Pedido P ON SP.Status_ID = P.Status_ID
GROUP BY SP.Nome, SP.Ordem;


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


CREATE OR ALTER VIEW VW_Busca_Rapida_Pedido AS
SELECT
P.OS_Externa AS OS,
C.Nome AS Nome,
P.Data_Pedido,
SP.Nome AS Satus_Atual,
TP.Nome AS Produto,
PI.Altura,
PI.Largura,
PI.Quantidade
FROM Pedido P
JOIN Clientes C ON P.Cliente_ID = C.Cliente_id
JOIN Status_Producao SP ON P.Status_ID = SP.Status_ID
JOIN Pedido_Item PI ON P.Pedido_ID = PI.Pedido_ID
JOIN Tipo_Produto TP ON PI.Tipo_Produto_ID = TP.Tipo_Produto_ID;

CREATE OR ALTER VIEW VW_Dashboard_Financeiro AS 
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


CREATE OR ALTER PROCEDURE SP_Validar_Login
    @Login NVARCHAR(50),
    @Senha NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON; -- Melhora a performance ao não retornar mensagens de "linhas afetadas"

    -- Verifica se existe o par Login/Senha no banco
    IF EXISTS (SELECT 1 FROM Usuario WHERE Login = @Login AND Senha = @Senha)
    BEGIN
        -- Se estiver tudo certo, o banco "entrega" os dados do funcionário para o sistema
        SELECT 
            Usuario_ID, 
            Nome, 
            Funcao 
        FROM Usuario 
        WHERE Login = @Login AND Senha = @Senha;
    END
    ELSE
    BEGIN
        -- Se o login ou a senha estiverem errados, o banco gera um erro para avisar a tela
        RAISERROR('Usuário ou Senha inválidos. Tente novamente.', 16, 1);
    END
END

CREATE OR ALTER PROCEDURE SP_Cadastrar_Cliente_Completo
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


CREATE OR ALTER PROCEDURE SP_Criar_Pedido_Com_Item
    @Cliente_ID INT,
    @OS_Externa NVARCHAR(6),
    @Status_ID INT,
    @Observacao_Geral NVARCHAR(255),
    @Tipo_Produto_ID INT,
    @Largura DECIMAL(10,2),
    @Altura DECIMAL(10,2),
    @Quantidade INT,
    @Observacao_Tecnica NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Cria a cabeçalho do Pedido
        INSERT INTO Pedido (Cliente_ID, OS_Externa, Status_ID, Observacao_Geral)
        VALUES (@Cliente_ID, @OS_Externa, @Status_ID, @Observacao_Geral);
        
        -- 2. Recupera o ID do pedido que acabou de ser gerado
        DECLARE @NovoPedidoID INT = SCOPE_IDENTITY();

        -- 3. Insere o primeiro item obrigatório deste pedido
        INSERT INTO Pedido_Item (Pedido_ID, Tipo_Produto_ID, Largura, Altura, Quantidade, Observacao_Tecnica)
        VALUES (@NovoPedidoID, @Tipo_Produto_ID, @Largura, @Altura, @Quantidade, @Observacao_Tecnica);

        COMMIT TRANSACTION;
        PRINT 'Pedido e Item criados com sucesso!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; -- Repassa o erro para a interface (ex: OS duplicada)
    END CATCH
END

CREATE OR ALTER PROCEDURE SP_Atualizar_Status_Pedido
   @Pedido_ID INT,
   @Novo_Status_ID INT,
   @Usuario_ID INT,
   @Valor_Total DECIMAL(10,2) = NULL,   -- Novos campos opcionais
   @Forma_Pagamento NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- A TRAVA: Se tentar mudar para "Entregue" (7) sem dinheiro, o banco barra!
        IF @Novo_Status_ID = 7 AND (@Valor_Total IS NULL OR @Valor_Total <= 0 OR @Forma_Pagamento IS NULL)
        BEGIN
            RAISERROR ('Erro Financeiro: Informe o Valor Total e a Forma de Pagamento para entregar o pedido.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Atualiza o status e também os valores financeiros (se enviados)
        UPDATE Pedido
        SET Status_ID = @Novo_Status_ID,
            Valor_Total = ISNULL(@Valor_Total, Valor_Total),
            Forma_Pagamento = ISNULL(@Forma_Pagamento, Forma_Pagamento)
        WHERE Pedido_ID = @Pedido_ID;
   
        COMMIT TRANSACTION;
        PRINT 'Status e Financeiro atualizados com sucesso!';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
 
CREATE OR ALTER PROCEDURE SP_Vincular_Arquivo_Arte
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
  
CREATE OR ALTER TRIGGER TR_Gerar_Historico_Status
ON Pedido
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Verifica se a coluna Status_ID foi realmente alterada
    IF UPDATE(Status_ID)
    BEGIN
        INSERT INTO Historico_Status (Pedido_ID, Status_ID, Data_Mudanca, Usuario_ID)
        SELECT 
            i.Pedido_ID, 
            i.Status_ID, 
            GETDATE(), 
            -- Aqui o sistema captura o ID do usuário que você passou na Procedure
            -- Se por acaso não vier um ID, podemos definir um padrão (ex: 1 para Admin)
            ISNULL(CAST(CONTEXT_INFO() AS INT), 1) 
        FROM inserted i
        JOIN deleted d ON i.Pedido_ID = d.Pedido_ID
        WHERE i.Status_ID <> d.Status_ID; -- Só grava se o status for realmente diferente
    END
END

CREATE OR ALTER TRIGGER TR_Validar_Dimensoes_Item
ON Pedido_Item
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Se houver qualquer item com Largura ou Altura menor ou igual a zero
    IF EXISTS (
        SELECT 1 
        FROM inserted 
        WHERE Largura <= 0 OR Altura <= 0
    )
    BEGIN
        -- O SQL cancela a operação e envia um aviso claro para a tela
        ROLLBACK TRANSACTION;
        RAISERROR ('Erro de Segurança: Largura e Altura devem ser maiores que zero.', 16, 1);
    END
END

CREATE OR ALTER TRIGGER TR_Proibir_Delete_Pedido
ON Pedido
FOR DELETE
AS
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR ('Erro: Não é permitido excluir pedidos. Altere o status se necessário.', 16, 1);
END
