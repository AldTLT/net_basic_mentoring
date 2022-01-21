IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UnderTask' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[UnderTask] (
    [ParrentTaskID] INT NOT NULL,
    [ChildTaskID]   INT NOT NULL,
    CONSTRAINT [PK_UnderTask] PRIMARY KEY CLUSTERED ([ParrentTaskID] ASC, [ChildTaskID] ASC),
    CONSTRAINT [FK_ChildTask_UnderTask] FOREIGN KEY ([ChildTaskID]) REFERENCES [dbo].[Task] ([Id]),
    CONSTRAINT [FK_ParrentTask_UnderTask] FOREIGN KEY ([ParrentTaskID]) REFERENCES [dbo].[Task] ([Id])
);

IF NOT EXISTS (select * from sys.indexes WHERE name = 'FK_UnderTask')
CREATE UNIQUE NONCLUSTERED INDEX [FK_UnderTask]
    ON [dbo].[UnderTask]([ChildTaskID] ASC);

