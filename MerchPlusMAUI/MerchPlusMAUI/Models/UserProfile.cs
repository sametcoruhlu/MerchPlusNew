using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchPlusMAUI.Models
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfileImagePath { get; set; }
        public string Role { get; set; }
        
        // Tam adı döndüren property
        public string FullName => $"{FirstName} {LastName}";
        
        // Profil resmi yoksa varsayılan resim döndüren property
        public string ProfileImage => string.IsNullOrEmpty(ProfileImagePath) 
            ? "default_profile.png" 
            : ProfileImagePath;
    }
} 