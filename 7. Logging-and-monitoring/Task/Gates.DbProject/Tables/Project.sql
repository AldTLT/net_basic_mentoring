IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Project' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Project] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (40) NOT NULL,
    [AuthorID]   NVARCHAR (40) NOT NULL,
    [CreateDate] DATETIME      CONSTRAINT [DF_Project_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Account_Project] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Account] ([Login])
);

