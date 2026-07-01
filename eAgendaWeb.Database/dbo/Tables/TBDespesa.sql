CREATE TABLE [dbo].[TBDespesa] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [Descricao]        NVARCHAR (100)   NOT NULL,
    [DataDeOcorrencia] DATE             NULL,
    [Valor]            DECIMAL (18)     NOT NULL,
    [FormaDePagamento] INT              NOT NULL
);
GO

ALTER TABLE [dbo].[TBDespesa]
    ADD CONSTRAINT [PK_TBDespesa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

