CREATE TABLE [dbo].[TBTarefa] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Prioridade]          INT              NOT NULL,
    [DataDeCriacao]       DATE             NULL,
    [DataDeConclusao]     DATE             NULL,
    [StatusDeConclusao]   INT              NOT NULL,
    [PercentualConcluido] DECIMAL (18)     NOT NULL,
    [Titulo]              NVARCHAR (100)   NOT NULL
);
GO

ALTER TABLE [dbo].[TBTarefa]
    ADD CONSTRAINT [PK_TBTarefa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

