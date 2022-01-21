using System;
using System.ComponentModel.DataAnnotations;

namespace Gates.BL.ViewModels
{
    /// <summary>
    /// Модель представляет Comment для связи с фронтендом
    /// </summary>
    public class CommentViewModel
    {
        public int CommentId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40)]
        public string Login { get; set; }

        public int TaskId { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(400, MinimumLength = 6)]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }
}
