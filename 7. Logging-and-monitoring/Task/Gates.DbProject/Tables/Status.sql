IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Status' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Status] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF NOT EXISTS (select * from sys.indexes WHERE name = 'Uniq_Title_Status')
CREATE UNIQUE NONCLUSTERED INDEX [Uniq_Title_Status]
    ON [dbo].[Status]([Title] ASC);

