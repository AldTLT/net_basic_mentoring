using System;
using System.ComponentModel.DataAnnotations;

namespace Gates.BL.ViewModels
{
    /// <summary>
    /// Модель представляет Task для связи с фронтендом.
    /// </summary>
    public class BoardTaskViewModel
    {
        public int TaskId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 3)]
        public string AuthorLogin { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [StringLength(200)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeadlineDate { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40)]
        public string Project { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Priority { get; set; }

        [DataType(DataType.Text)]
        [StringLength(20)]
        public string Status { get; set; }
    }
}