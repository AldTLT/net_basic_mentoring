IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TaskHashRelationship' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[TaskHashRelationship] (
    [TaskID]    INT NOT NULL,
    [HashTagID] INT NOT NULL,
    CONSTRAINT [PK_TaskHashRelatinship] PRIMARY KEY CLUSTERED ([TaskID] ASC, [HashTagID] ASC),
    CONSTRAINT [FK_HashTag_TaskHashRelationship] FOREIGN KEY ([HashTagID]) REFERENCES [dbo].[HashTag] ([Id]),
    CONSTRAINT [FK_Task_TaskHashRelationship] FOREIGN KEY ([TaskID]) REFERENCES [dbo].[Task] ([Id])
);

