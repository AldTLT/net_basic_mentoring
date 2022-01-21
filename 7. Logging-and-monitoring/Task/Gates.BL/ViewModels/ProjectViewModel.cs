using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Gates.BL.ViewModels
{
    /// <summary>
    /// Модель представляет Project для связи с фронтендом
    /// </summary>
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40)]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Text)]
        [StringLength(40)]
        public string AuthorId { get; set; }

        
    }
}
