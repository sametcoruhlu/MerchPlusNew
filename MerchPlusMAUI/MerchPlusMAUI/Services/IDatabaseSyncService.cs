using System.Threading.Tasks;

namespace MerchPlusMAUI.Services
{
    public interface IDatabaseSyncService
    {
        Task StartSync();
        Task StopSync();
    }
} 