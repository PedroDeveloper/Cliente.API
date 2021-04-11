---------------------------------------------------------------------------
CREATE TABLE Cliente (
	[ID] bigint IDENTITY(1,1),
	[Cnpj] VARCHAR(20) NULL,
	[Cpf] VARCHAR(20) NULL,
	[Nome] VARCHAR(250) NULL,
	[DtNas] DATE NULL,
	[RazaoSoci] NVARCHAR(50) NULL,    	
	[Email] VARCHAR(100) NOT NULL,
	[tel] varchar(50) NOT NULL,
	
);

ALTER TABLE Cliente ADD CONSTRAINT PK_Cliente PRIMARY KEY(ID)

---------------------------------------------------------------------------

CREATE TABLE Endereco
(
	[ID_end] int identity(1,1),	
	[ID_cliente] bigint NOT NULL,
	[Nome_end] varchar(250) NOT NULL,
	[Tel_end] varchar(15) NOT NULL,
	[Cep] varchar(9) NOT NULL,
	[Endereco] varchar(250) NOT NULL,
	[Numero] varchar(15) NOT NULL,
	[Complemento] varchar(100) NOT NULL,
	[Bairro] varchar(50) NOT NULL,
	[Cidade] varchar(50) NOT NULL,
	[Estado] char(2)NOT NULL,
);

ALTER TABLE Endereco Add Constraint PK_Enderecol Primary Key(ID)
ALTER TABLE Endereco Add Constraint FK_Endereco Foreign Key(ID_cliente) References Cliente(ID) ON DELETE CASCADE

---------------------------------------------------------------------------