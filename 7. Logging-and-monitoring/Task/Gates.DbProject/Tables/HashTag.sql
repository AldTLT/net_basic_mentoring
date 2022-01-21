IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HashTag' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[HashTag] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_HashTag] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF NOT EXISTS (select * from sys.indexes WHERE name = 'Uniq_Title_HashTag')
CREATE UNIQUE NONCLUSTERED INDEX [Uniq_Title_HashTag]
    ON [dbo].[HashTag]([Title] ASC);