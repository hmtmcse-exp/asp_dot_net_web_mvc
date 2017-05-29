using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scaffold.Models
{
    
    public enum OrganizerType
    {
        Administrator, Merchant, Client, Support
    }
    
    
    public enum UsageInterface
    {
        WEB, API
    }
    
    
    public class Organizer
    {
        public int Id { get; set; }
        
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Required(ErrorMessage = "Please Enter Name.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter a Valid Email Address.")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter Password.")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        
        [Required(ErrorMessage = "Please Enter Confirm Password.")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not Matched.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Organizer Type")]
        public OrganizerType OrganizerType { get; set; } = OrganizerType.Client;
        
        
        [Display(Name = "Usage Interface")]
        public UsageInterface UsageInterface { get; set; } = UsageInterface.WEB;
        
        
        [Display(Name = "Is Enable")]
        public bool IsEnable { get; set; } = true;
        
        

    }
}