using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic; // Added for Dictionary
using MerchPlusMAUI.Views; // Added for StoreProductsPage reference

namespace MerchPlusMAUI.Controls
{
    public partial class FloatingBackButton : ContentView
    {
        public FloatingBackButton()
        {
            InitializeComponent();
            CheckVisibility();
        }

        private void CheckVisibility()
        {
            try
            {
                if (Shell.Current != null)
                {
                    var currentPage = Shell.Current.CurrentPage;
                    var pageName = currentPage?.GetType().Name ?? "";
                    
                    System.Diagnostics.Debug.WriteLine($"FloatingBackButton: Current page is {pageName}");
                    
                    // Ana sayfalarda gizle, diğer sayfalarda göster
                    bool shouldShow = pageName != "AboutMePage" && pageName != "HomePage";
                    
                    IsVisible = shouldShow;
                    System.Diagnostics.Debug.WriteLine($"FloatingBackButton: Visibility set to {shouldShow}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FloatingBackButton: CheckVisibility error: {ex.Message}");
                IsVisible = false;
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("FloatingBackButton: Back button clicked");
                
                // Animasyon efekti
                await BackButton.ScaleTo(0.9, 50);
                await BackButton.ScaleTo(1.0, 50);
                
                // Shell navigation ile geri git
                if (Shell.Current != null)
                {
                    var currentPage = Shell.Current.CurrentPage;
                    var pageName = currentPage?.GetType().Name ?? "";
                    
                    System.Diagnostics.Debug.WriteLine($"FloatingBackButton: Current page is {pageName}");
                    
                    // Sayfa tipine göre farklı navigation stratejisi
                    switch (pageName)
                    {
                        case "StoresPage":
                            // StoresPage'den AboutMePage'e git
                            System.Diagnostics.Debug.WriteLine("FloatingBackButton: Navigating from StoresPage to AboutMePage");
                            await Shell.Current.GoToAsync("///AboutMePage");
                            break;
                            
                        case "NotificationsPage":
                            // NotificationsPage'den AboutMePage'e git
                            System.Diagnostics.Debug.WriteLine("FloatingBackButton: Navigating from NotificationsPage to AboutMePage");
                            await Shell.Current.GoToAsync("///AboutMePage");
                            break;
                            
                        case "MerchDataEntryPage":
                            // MerchDataEntryPage'den StoreProductsPage'e git
                            System.Diagnostics.Debug.WriteLine("FloatingBackButton: Navigating from MerchDataEntryPage to StoreProductsPage");
                            await Shell.Current.GoToAsync("..");
                            break;
                            
                        case "StoreProductsPage":
                            // StoreProductsPage'den StoreReportPage'e git
                            System.Diagnostics.Debug.WriteLine("FloatingBackButton: Navigating from StoreProductsPage to StoreReportPage");
                            try
                            {
                                // StoreProductsPage'den StoreReportPage'e absolute routing ile git
                                // Parametreleri almaya çalış
                                if (currentPage is StoreProductsPage storeProductsPage)
                                {
                                    var parameters = new Dictionary<string, object>
                                    {
                                        ["entMemberRouteId"] = storeProductsPage.EntMemberRouteId,
                                        ["StoreName"] = storeProductsPage.StoreName
                                    };
                                    await Shell.Current.GoToAsync("///StoreReportPage", parameters);
                                }
                                else
                                {
                                    // Parametreler alınamazsa fallback olarak StoresPage'e git
                                    await Shell.Current.GoToAsync("///StoresPage");
                                }
                            }
                            catch (Exception navEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"FloatingBackButton: StoreProductsPage navigation error: {navEx.Message}");
                                // Fallback: StoresPage'e git
                                await Shell.Current.GoToAsync("///StoresPage");
                            }
                            break;
                            
                        default:
                            // Diğer sayfalar için generic back navigation
                            System.Diagnostics.Debug.WriteLine($"FloatingBackButton: Generic back navigation from {pageName}");
                            await Shell.Current.GoToAsync("..");
                            break;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("FloatingBackButton: Shell.Current is null");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FloatingBackButton: Navigation error: {ex.Message}");
                
                // Fallback: Ana sayfaya git
                try
                {
                    await Shell.Current.GoToAsync("///AboutMePage");
                }
                catch (Exception fallbackEx)
                {
                    System.Diagnostics.Debug.WriteLine($"FloatingBackButton: Fallback navigation error: {fallbackEx.Message}");
                }
            }
        }

        // Dışarıdan visibility kontrolü
        public void UpdateVisibility()
        {
            CheckVisibility();
        }

        // Manuel show/hide
        public void Show()
        {
            IsVisible = true;
        }

        public void Hide()
        {
            IsVisible = false;
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            if (Handler != null)
            {
                CheckVisibility();
            }
        }
    }
} 