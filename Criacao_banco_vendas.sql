CREATE DATABASE Controle_Vendas;

USE Controle_Vendas;

CREATE DATABASE Controle_Vendas;
GO
USE Controle_Vendas;
GO

-- 1. Tabelas Base (Sem Dependências)
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

ALTER TABLE Endereco 
ALTER COLUMN CEP NVARCHAR(8) NOT NULL;

EXEC sp_rename 'Endereco.Bairo','Bairro','COLUMN';

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

--IDEIAS (NÃO IMPLEMENTAR AGORA)--
--estoque de adesivo--
--cálculo automático de metros--
--alerta de material--

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
OS_Externa NVARCHAR (6) NOT NULL,
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
USE Controle_Vendas;

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
('Metal', 'Produtos fabricados em metal');

INSERT INTO Material (Nome, Descricao)
VALUES 
('Adesivo Vinil Fosco', 'Vinil fosco para impressão'),
('Adesivo Refletivo GTP Branco', 'Refletivo branco GTP'),
('Adesivo GTP Amarelo', 'Refletivo amarelo GTP'),
('Acrílico 2mm', 'Acrílico espessura 2mm'),
('Acrílico 3mm', 'Acrílico espessura 3mm'),
('Acrílico 6mm Branco', 'Acrílico branco 6mm'),
('Acrílico 6mm Cristal', 'Acrílico cristal 6mm'),
('Acrílico 6mm Preto', 'Acrílico preto 6mm'),
('Acrílico Espelhado Dourado 2mm', 'Acrílico espelhado dourado'),
('Aço Inox Escovado', 'Inox acabamento escovado'),
('Chapa Galvanizada', 'Chapa galvanizada'),
('Chapa 18', 'Chapa metálica nº 18'),
('ACM 3mm', 'Alumínio composto 3mm'),
('Alumínio', 'Chapa de alumínio'),
('Tubo 2"', 'Tubo metálico 2 polegadas'),
('Tubo 2.5"', 'Tubo metálico 2.5 polegadas'),
('Tubo 3"', 'Tubo metálico 3 polegadas');


INSERT INTO Tipo_Produto (Categoria_ID, Nome, Descricao_Tecnica, Usa_adesivo, Usa_Mascara)
VALUES 
--Adesivo--
(1, 'Adesivo Vinil Impresso', 'Impressão digital em vinil fosco', 1, 0),
(1, 'Adesivo Refletivo Impresso', 'Impressão digital em adesivo refletivo', 1, 0),
--Placas--
(2, 'Placa de Sinalização','Placa refletiva em ACM, chapa 18 ou galvanizada', 1, 0),
(2, 'Placa de Sinalização com Poste', 'Placa refletiva com tubo para fixação', 1, 0),
--Acrilico--
(3, 'Acrílico Impressão UV', 'Impressão UV direta em acrilico', 0, 0),
(3, 'Troféu de Acrílico', 'Acrílico recortado com impressão UV', 0, 0),
(3, 'Acrílico com Sobreposição', 'Acrílico com camadas sobrepostas', 0, 0),
--Metal--
(4, 'Alumínio Impressão UV', 'Impressão UV em chapa de alumínio', 0, 0),
(4, 'Alumínio Adesivado', 'Alumínio com aplicação de adesivo', 1, 0),
(4, 'Aço Inox Gravado', 'Gravação em baixo relevo em inox', 0, 1),
(4, 'Aço Inox Impressão UV', 'Impressão UV direta em inox', 0, 0);


INSERT INTO Tipo_Produto_Material (Tipo_Produto_ID, Material_ID)
VALUES
-- Adesivo Vinil Impresso--
(1, 1),  -- Vinil Fosco

-- Adesivo Refletivo Impresso
(2, 2),  -- Refletivo Branco
(2, 3),  -- Refletivo Amarelo

-- Placa de Sinalização--
(3, 11), -- Chapa Galvanizada
(3, 12), -- Chapa 18
(3, 13), -- ACM 3mm

-- Placa de Sinalização com Poste--
(4, 11), -- Chapa Galvanizada
(4, 12), -- Chapa 18
(4, 13), -- ACM 3mm
(4, 15), -- Tubo 2"
(4, 16), -- Tubo 2.5"
(4, 17), -- Tubo 3"

-- Acrílico Impressão UV--
(5, 4),  -- Acrílico 2mm
(5, 5),  -- Acrílico 3mm
(5, 6),  -- Acrílico 6mm Branco
(5, 7),  -- Acrílico 6mm Cristal
(5, 8),  -- Acrílico 6mm Preto

-- Troféu de Acrílico--
(6, 4),  -- Acrílico 2mm
(6, 5),  -- Acrílico 3mm
(6, 6),  -- Acrílico 6mm Branco
(6, 7),  -- Acrílico 6mm Cristal

-- Acrílico com Sobreposição--
(7, 4),  -- Acrílico 2mm
(7, 5),  -- Acrílico 3mm
(7, 6),  -- Acrílico 6mm Branco
(7, 9),  -- Acrílico Espelhado Dourado

-- Alumínio Impressão UV--
(8, 14), -- Alumínio

-- Alumínio Adesivado--
(9, 14), -- Alumínio
(9, 1),  -- Adesivo Vinil Fosco

--  Aço Inox Gravado (usa máscara como processo)--
(10, 10), -- Aço Inox Escovado

--  Aço Inox Impressão UV--
(11, 10); -- Aço Inox Escovado


SELECT 
    cp.Nome AS Categoria,
    tp.Nome AS Produto,
    STRING_AGG(m.Nome, ', ') AS Materiais
FROM Tipo_Produto tp
JOIN Categoria_Produtos cp 
    ON cp.Categoria_ID = tp.Categoria_ID
JOIN Tipo_Produto_Material tpm 
    ON tpm.Tipo_Produto_ID = tp.Tipo_Produto_ID
JOIN Material m 
    ON m.Material_ID = tpm.Material_ID
GROUP BY cp.Nome, tp.Nome
ORDER BY cp.Nome, tp.Nome;

SELECT
    tp.Tipo_Produto_ID,
    tp.Nome AS Produto,
    m.Material_ID,
    m.Nome AS Material
FROM Tipo_Produto_Material tpm
JOIN Tipo_Produto tp 
    ON tp.Tipo_Produto_ID = tpm.Tipo_Produto_ID
JOIN Material m 
    ON m.Material_ID = tpm.Material_ID
ORDER BY tp.Tipo_Produto_ID, m.Nome;


SELECT 
    tp.Tipo_Produto_ID,
    tp.Nome AS Produto,
    cp.Nome AS Categoria,
    tp.Usa_Adesivo,
    tp.Usa_Mascara
FROM Tipo_Produto tp
JOIN Categoria_Produtos cp 
    ON cp.Categoria_ID = tp.Categoria_ID
ORDER BY cp.Nome, tp.Nome;
SELECT * FROM Status_Producao;
SELECT * FROM Status_Arte;
SELECT * FROM Categoria_Produtos;
SELECT * FROM Material;
SELECT * FROM Tipo_Produto;
SELECT * FROM Tipo_Produto_Material;


INSERT INTO Usuario (Nome, Funcao)
VALUES
('Administrador', 'Gestão'),
('Impressor', 'Produção'),
('Designer', 'Arte'),
('Vendedor', 'Comercial');


