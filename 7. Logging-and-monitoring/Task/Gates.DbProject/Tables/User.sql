IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'User' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[User] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (40) NULL,
    [LastName]  NVARCHAR (40) NULL,
    [AccountId]  NVARCHAR (40)  NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Account_User] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([Login]),
);

