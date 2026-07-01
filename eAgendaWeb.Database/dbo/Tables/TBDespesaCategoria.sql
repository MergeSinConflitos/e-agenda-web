CREATE TABLE [dbo].[TBDespesaCategoria] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [DespesaId]   UNIQUEIDENTIFIER NOT NULL,
    [CategoriaId] UNIQUEIDENTIFIER NOT NULL
);
GO

ALTER TABLE [dbo].[TBDespesaCategoria]
    ADD CONSTRAINT [PK_TBDespesaCategoria] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[TBDespesaCategoria]
    ADD CONSTRAINT [FK_TBDespesaCategoria_Categoria] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[TBCategoria] ([Id]);
GO

ALTER TABLE [dbo].[TBDespesaCategoria]
    ADD CONSTRAINT [FK_TBDespesaCategoria_Despesa] FOREIGN KEY ([DespesaId]) REFERENCES [dbo].[TBDespesa] ([Id]);
GO

