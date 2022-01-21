using Gates.BL.ViewModels;
using Gates.DAL.Entities;

namespace Gates.DAL.Map
{
    internal static class TaskMapper
    {
        public static TaskEntity Map(this TaskViewModel taskModel)
        {
            return new TaskEntity
            {
                ExecutorId = taskModel.ExecutorLogin,
                Title = taskModel.Title,
                Description = taskModel.Description,
                DeadlineDate = taskModel.DeadlineDate                
            };
        }

        public static TaskViewModel Map(this TaskEntity task)
        {
            return new TaskViewModel
            {               
                TaskId = task.Id,
                Title = task.Title,
                Description = task.Description,
                AuthorLogin = task.Author?.UserName,
                ExecutorLogin = task.Executor?.UserName,
                CreateDate = task.CreateDate,
                DeadlineDate = task.DeadlineDate,
                Status = task.Status?.Title,
                Priority = task.Priority?.Title,
                ProjectId = task.ProjectId
                
            };
        }

        public static BoardTaskViewModel MapToBoardTask(this TaskEntity taskEntity)
        {
            return new BoardTaskViewModel
            {
                AuthorLogin = taskEntity.Author?.UserName,
                TaskId = taskEntity.Id,
                Description = taskEntity.Description,
                Title = taskEntity.Title,
                DeadlineDate = taskEntity.DeadlineDate,
                Priority = taskEntity?.Priority?.Title,
                Status = taskEntity?.Status?.Title
            };
        }
    }
}