IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Comment' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Comment] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Contents]   NVARCHAR (400) NOT NULL,
    [CreateDate] DATETIME       CONSTRAINT [DF_Comment_CreateDate] DEFAULT (getdate()) NOT NULL,
    [AuthorID]   NVARCHAR (40)  NOT NULL,
    [TaskID]     INT            NOT NULL,
    CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Account_Comment] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Account] ([Login]),
    CONSTRAINT [FK_Task_Comment] FOREIGN KEY ([TaskID]) REFERENCES [dbo].[Task] ([Id])
);

