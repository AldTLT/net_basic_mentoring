IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Task' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[Task] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (20)  NOT NULL,
    [Description] NVARCHAR (200) NOT NULL,
    [DeadLine]    DATETIME2       NOT NULL,
    [CreateDate]  DATETIME2       CONSTRAINT [DF_Task_CreateDate] DEFAULT (getdate()) NOT NULL,
    [StatusID]    INT            NOT NULL,
    [PriorityID]  INT            NOT NULL,
    [ExecutorID]  NVARCHAR (40)  NULL,
    [AuthorID]    NVARCHAR (40)  NULL,
    [ProjectID]   INT            DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountAuthor_Task] FOREIGN KEY ([AuthorID]) REFERENCES [dbo].[Account] ([Login]),
    CONSTRAINT [FK_AccountExecutor_Task] FOREIGN KEY ([ExecutorID]) REFERENCES [dbo].[Account] ([Login]),
    CONSTRAINT [FK_Priority_Task] FOREIGN KEY ([PriorityID]) REFERENCES [dbo].[Priority] ([Id]),
    CONSTRAINT [FK_Status_Task] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[Status] ([Id]),
    CONSTRAINT [FK_Project_Task] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([Id])
);

