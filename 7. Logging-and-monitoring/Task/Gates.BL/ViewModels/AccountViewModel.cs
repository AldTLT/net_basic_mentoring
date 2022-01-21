using System.ComponentModel.DataAnnotations;

namespace Gates.BL.ViewModels
{
    public class AccountViewModel
    {
        [StringLength(20)]
        public string UserName;

        [StringLength(20)]
        public string Role;


        [StringLength(20)]
        public string Password;
    }
}
