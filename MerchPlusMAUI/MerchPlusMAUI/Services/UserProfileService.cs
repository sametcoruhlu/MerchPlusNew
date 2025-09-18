using System;
using System.Collections.Generic;
using MerchPlusMAUI.Models;

namespace MerchPlusMAUI.Services
{
    public class UserProfileService
    {
        // Singleton pattern için instance
        private static UserProfileService _instance;
        public static UserProfileService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserProfileService();
                }
                return _instance;
            }
        }
        
        // Mevcut kullanıcı
        private UserProfile _currentUser;
        
        // Private constructor for singleton
        private UserProfileService()
        {
            // Real user data will be loaded from GlobalVariables.CurrentMember
            _currentUser = null;
        }
        
        /// <summary>
        /// Mevcut giriş yapmış kullanıcının profilini döndürür
        /// </summary>
        public UserProfile GetCurrentUser()
        {
            // If user profile is not loaded yet, create it from GlobalVariables.CurrentMember
            if (_currentUser == null && GlobalVariables.CurrentMember != null)
            {
                _currentUser = new UserProfile
                {
                    Id = GlobalVariables.CurrentMember.Id,
                    FirstName = GlobalVariables.CurrentMember.NameSurname?.Split(' ').FirstOrDefault() ?? "Kullanıcı",
                    LastName = GlobalVariables.CurrentMember.NameSurname?.Split(' ').Skip(1).FirstOrDefault() ?? "",
                    Role = "Satış Temsilcisi", // This could be determined from member data
                    Email = GlobalVariables.CurrentMember.Email ?? "",
                    PhoneNumber = GlobalVariables.CurrentMember.PhoneNumber ?? "",
                    ProfileImagePath = !string.IsNullOrEmpty(GlobalVariables.CurrentMember.ProfilePicturePath) 
                        ? GlobalVariables.CurrentMember.ProfilePicturePath 
                        : "dotnet_bot.png"
                };
            }
            
            return _currentUser;
        }
        
        /// <summary>
        /// Kullanıcı profil bilgilerini günceller
        /// </summary>
        public void UpdateUserProfile(UserProfile updatedProfile)
        {
            _currentUser = updatedProfile;
        }
        
        /// <summary>
        /// Kullanıcı profil resmini günceller
        /// </summary>
        public void UpdateProfileImage(string newImagePath)
        {
            if (_currentUser != null)
            {
                _currentUser.ProfileImagePath = newImagePath;
            }
        }
        
        /// <summary>
        /// Refresh user profile from GlobalVariables.CurrentMember
        /// </summary>
        public void RefreshFromCurrentMember()
        {
            _currentUser = null; // Force reload on next GetCurrentUser call
        }
        
        // TODO: Bu servisi gerçek bir API veya veritabanı ile entegre et
        // Şu anda dummy veri kullanılıyor, gerçek implementasyon için:
        // - Kullanıcı bilgilerini API'den al
        // - Kullanıcı giriş işlemlerini yönet (Token, vb.)
        // - Profil bilgisi değişikliklerini sunucuya gönder
    }
} 