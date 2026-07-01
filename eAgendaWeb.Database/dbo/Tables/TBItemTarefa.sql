CREATE TABLE [dbo].[TBItemTarefa] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [TarefaId] UNIQUEIDENTIFIER NULL,
    [ItemId]   UNIQUEIDENTIFIER NULL
);
GO

ALTER TABLE [dbo].[TBItemTarefa]
    ADD CONSTRAINT [FK_TBItemTarefa_Tarefa] FOREIGN KEY ([TarefaId]) REFERENCES [dbo].[TBTarefa] ([Id]);
GO

ALTER TABLE [dbo].[TBItemTarefa]
    ADD CONSTRAINT [FK_TBItemTarefa_Item] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[TBItem] ([Id]);
GO

ALTER TABLE [dbo].[TBItemTarefa]
    ADD CONSTRAINT [PK_TBItemTarefa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

