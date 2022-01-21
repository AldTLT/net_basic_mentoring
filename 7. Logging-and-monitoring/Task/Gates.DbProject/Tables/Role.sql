IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Role' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Role] (
    [Id]    INT           IDENTITY (1, 1) NOT NULL,
    [Title] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF NOT EXISTS (select * from sys.indexes WHERE name = 'Uniq_Title_Role')
CREATE UNIQUE NONCLUSTERED INDEX [Uniq_Title_Role]
    ON [dbo].[Role]([Title] ASC);

