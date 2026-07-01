CREATE TABLE [dbo].[TBItem] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [Titulo]            NVARCHAR (100)   NOT NULL,
    [StatusDeConclusao] INT              NOT NULL
);
GO

ALTER TABLE [dbo].[TBItem]
    ADD CONSTRAINT [PK_TBItem] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

