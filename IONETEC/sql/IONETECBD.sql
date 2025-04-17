IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE SEQUENCE [MinhaSequencia] AS int START WITH 1 INCREMENT BY 1 NO MINVALUE NO MAXVALUE NO CYCLE;
GO

CREATE TABLE [Categorias] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(50) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Clientes] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Nif] varchar(14) NOT NULL,
    [TipoCliente] int NOT NULL,
    [Email] varchar(254) NULL,
    [Telefone] varchar(16) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Fabricantes] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Fabricantes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Fornecedores] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Documento] varchar(16) NOT NULL,
    [TipoFornecedor] int NOT NULL,
    [Email] varchar(254) NULL,
    [Telefone] varchar(16) NOT NULL,
    [Endereco] varchar(250) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NotaEntregaNotaRecebidas] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL DEFAULT (NEXT VALUE FOR MinhaSequencia),
    [DocumentoPdf] varchar(250) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [DataEmissao] datetime2 NOT NULL,
    [TipoNota] int NOT NULL,
    [Emitente] varchar(100) NULL,
    [Destinatario] varchar(100) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_NotaEntregaNotaRecebidas] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Vendedores] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    [Email] varchar(254) NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Vendedores] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Contatos] (
    [Id] uniqueidentifier NOT NULL,
    [ClienteId] uniqueidentifier NOT NULL,
    [Telefone] varchar(16) NOT NULL,
    [Email] varchar(250) NOT NULL,
    CONSTRAINT [PK_Contatos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Contatos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id])
);
GO

CREATE TABLE [Enderecos] (
    [Id] uniqueidentifier NOT NULL,
    [ClienteId] uniqueidentifier NOT NULL,
    [Bairro] varchar(200) NOT NULL,
    [Municipio] varchar(50) NOT NULL,
    [Provincia] varchar(30) NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Enderecos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id])
);
GO

CREATE TABLE [Pedidos] (
    [Id] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL DEFAULT (NEXT VALUE FOR MinhaSequencia),
    [ClienteId] uniqueidentifier NOT NULL,
    [ValorUnitario] decimal(18,2) NOT NULL,
    [PercentualDesconto] decimal(18,2) NOT NULL,
    [ValorDesconto] decimal(18,2) NOT NULL,
    [ValorTotal] decimal(18,2) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [NumeroDeSerie] varchar(30) NOT NULL,
    [OperacaoPedidos] int NOT NULL,
    [Situacao] int NOT NULL,
    [TipoPagamento] int NOT NULL,
    [NumeroDeTransacaoDePagamento] varchar(150) NOT NULL,
    CONSTRAINT [PK_Pedidos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Pedidos_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id])
);
GO

CREATE TABLE [Produtos] (
    [Id] uniqueidentifier NOT NULL,
    [FornecedorId] uniqueidentifier NOT NULL,
    [FabricanteId] uniqueidentifier NOT NULL,
    [CategoriaId] uniqueidentifier NOT NULL,
    [Codigo] int NOT NULL DEFAULT (NEXT VALUE FOR MinhaSequencia),
    [Nome] varchar(250) NOT NULL,
    [Descricao] varchar(500) NOT NULL,
    [Imagem] varchar(250) NOT NULL,
    [ValorCompra] decimal(18,2) NOT NULL,
    [ValorVenda] decimal(18,2) NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [QuantidadeEstoque] int NOT NULL,
    [Filial] varchar(250) NOT NULL,
    [Ativo] bit NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produtos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]),
    CONSTRAINT [FK_Produtos_Fabricantes_FabricanteId] FOREIGN KEY ([FabricanteId]) REFERENCES [Fabricantes] ([Id]),
    CONSTRAINT [FK_Produtos_Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [Fornecedores] ([Id])
);
GO

CREATE TABLE [PedidoItems] (
    [Id] uniqueidentifier NOT NULL,
    [PedidoId] uniqueidentifier NOT NULL,
    [ProdutoId] uniqueidentifier NOT NULL,
    [Quantidade] int NOT NULL,
    [ProdutoNome] varchar(250) NOT NULL,
    [ValorUnitario] decimal(18,2) NOT NULL,
    [VendedorId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_PedidoItems] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PedidoItems_Pedidos_PedidoId] FOREIGN KEY ([PedidoId]) REFERENCES [Pedidos] ([Id]),
    CONSTRAINT [FK_PedidoItems_Produtos_ProdutoId] FOREIGN KEY ([ProdutoId]) REFERENCES [Produtos] ([Id]),
    CONSTRAINT [FK_PedidoItems_Vendedores_VendedorId] FOREIGN KEY ([VendedorId]) REFERENCES [Vendedores] ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Contatos_ClienteId] ON [Contatos] ([ClienteId]);
GO

CREATE UNIQUE INDEX [IX_Enderecos_ClienteId] ON [Enderecos] ([ClienteId]);
GO

CREATE INDEX [IX_PedidoItems_PedidoId] ON [PedidoItems] ([PedidoId]);
GO

CREATE INDEX [IX_PedidoItems_ProdutoId] ON [PedidoItems] ([ProdutoId]);
GO

CREATE INDEX [IX_PedidoItems_VendedorId] ON [PedidoItems] ([VendedorId]);
GO

CREATE INDEX [IX_Pedidos_ClienteId] ON [Pedidos] ([ClienteId]);
GO

CREATE INDEX [IX_Produtos_CategoriaId] ON [Produtos] ([CategoriaId]);
GO

CREATE INDEX [IX_Produtos_FabricanteId] ON [Produtos] ([FabricanteId]);
GO

CREATE INDEX [IX_Produtos_FornecedorId] ON [Produtos] ([FornecedorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230909143302_Initial', N'7.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Fornecedores]') AND [c].[name] = N'Email');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Fornecedores] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [Fornecedores] SET [Email] = '' WHERE [Email] IS NULL;
ALTER TABLE [Fornecedores] ALTER COLUMN [Email] varchar(254) NOT NULL;
ALTER TABLE [Fornecedores] ADD DEFAULT '' FOR [Email];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Email');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [Clientes] SET [Email] = '' WHERE [Email] IS NULL;
ALTER TABLE [Clientes] ALTER COLUMN [Email] varchar(254) NOT NULL;
ALTER TABLE [Clientes] ADD DEFAULT '' FOR [Email];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230909161056_Initial_1', N'7.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Vendedores]') AND [c].[name] = N'Email');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Vendedores] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [Vendedores] SET [Email] = '' WHERE [Email] IS NULL;
ALTER TABLE [Vendedores] ALTER COLUMN [Email] varchar(254) NOT NULL;
ALTER TABLE [Vendedores] ADD DEFAULT '' FOR [Email];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230909162400_Initial_2', N'7.0.10');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Email');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Clientes] DROP COLUMN [Email];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clientes]') AND [c].[name] = N'Telefone');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Clientes] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Clientes] DROP COLUMN [Telefone];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230911142027_Initial_3', N'7.0.10');
GO

COMMIT;
GO

