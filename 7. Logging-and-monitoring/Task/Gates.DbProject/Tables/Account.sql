IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Account' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Account] (
    [Login]        NVARCHAR (40)  NOT NULL,
    [PasswordHash] NVARCHAR (128) NOT NULL,
    [UserID]       INT            NULL,
    [RoleID]       INT            NOT NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Login] ASC),
    CONSTRAINT [FK_Role_Account] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([Id]),
    CONSTRAINT [FK_User_Account] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([Id])
);

IF NOT EXISTS (select * from sys.indexes WHERE name = 'FK_UserID')
CREATE UNIQUE NONCLUSTERED INDEX [FK_UserID]
    ON [dbo].[Account]([Login] ASC);