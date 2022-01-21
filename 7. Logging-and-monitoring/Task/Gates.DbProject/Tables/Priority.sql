IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Priority' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Priority] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF NOT EXISTS (select * from sys.indexes WHERE name = 'Uniq_Title_Priority')
CREATE UNIQUE NONCLUSTERED INDEX [Uniq_Title_Priority]
    ON [dbo].[Priority]([Title] ASC);

