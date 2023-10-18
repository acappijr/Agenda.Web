CREATE DATABASE WebApp;
GO

USE WebApp;
GO

CREATE LOGIN WebAppUser
WITH PASSWORD = 'WebAppPw1!', DEFAULT_DATABASE = WebApp;
GO

CREATE USER WebAppUser FOR LOGIN WebAppUser;
GO

GRANT ALTER ANY SCHEMA to WebAppUser
GRANT EXECUTE to WebAppUser
GRANT CONTROL to WebAppUser
--EXEC sp_addrolemember N'db_datareader', N'WebAppUser'
--EXEC sp_addrolemember N'db_datawriter', N'WebAppUser'
--EXEC sp_addrolemember N'db_ddladmin', N'WebAppUser'

CREATE TABLE dbo.Cep(
    Id UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    Codigo CHAR(8) NOT NULL,
    Estado CHAR(2) NOT NULL,
    Cidade VARCHAR(50) NOT NULL,
    Bairro VARCHAR(90),
    Endereco VARCHAR(120)
);

ALTER TABLE dbo.Cep ADD CONSTRAINT PK_Cep PRIMARY KEY NONCLUSTERED (Id);
CREATE UNIQUE CLUSTERED INDEX CIX_Cep ON dbo.Cep(Codigo);

CREATE TABLE dbo.Contato(
    Id INT IDENTITY(1,1) NOT NULL,
    Nome VARCHAR(80) NOT NULL,
    Telefone VARCHAR(11),
    Email VARCHAR(255),
    CepId UNIQUEIDENTIFIER NOT NULL,
    NumeroEndereco VARCHAR(15),
    ComplementoEndereco VARCHAR(30)
)

ALTER TABLE dbo.Contato ADD CONSTRAINT PK_Contato PRIMARY KEY CLUSTERED (Id);
ALTER TABLE dbo.Contato ADD CONSTRAINT FK_Cep FOREIGN KEY (CepId) REFERENCES Cep(Id);
