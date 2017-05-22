using System.ComponentModel.DataAnnotations;

namespace Scaffold.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage = "Please Enter Name.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please Enter Password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter Confirm Password.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not Matched.")]
        public string ConfirmPassword { get; set; }
    }
}