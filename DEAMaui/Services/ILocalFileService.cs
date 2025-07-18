
using DEAModels;

namespace DEAMaui.Services
{
    public interface ILocalFileService
    {
        Task LogMatchActionAsync(string action, Match match);
    }
}