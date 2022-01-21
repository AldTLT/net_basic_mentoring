IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ProjectTaskRelationShip' AND TABLE_SCHEMA = 'dbo')
CREATE TABLE [dbo].[ProjectTaskRelationship] (
    [TaskID]    INT NOT NULL,
    [ProjectID] INT NOT NULL,
    CONSTRAINT [PK_ProjectTaskRelationship_1] PRIMARY KEY CLUSTERED ([TaskID] ASC, [ProjectID] ASC),
    CONSTRAINT [FK_Project_ProjectTaskRelationship] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Project] ([Id]),
    CONSTRAINT [FK_Task_ProjectTaskRelationship] FOREIGN KEY ([TaskID]) REFERENCES [dbo].[Task] ([Id])
);

