using System;
using System.ComponentModel.DataAnnotations;

namespace Gates.BL.ViewModels
{
    /// <summary>
    /// Модель представляет Task для связи с фронтендом.
    /// </summary>
    public class TaskViewModel
    {
        public int TaskId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40)]
        //public string AuthorLogin { get; set; }
        public string AuthorLogin { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40)]
        //public string ExecutorLogin { get; set; }
        public string ExecutorLogin { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(200, MinimumLength = 6)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeadlineDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        public int? ProjectId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Priority { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Status { get; set; }
    }
}