using LaManuAuto.Models;

namespace LaManuAuto.Services
{
    public interface IDataService
    {
        List<Tutoriel> GetTutorials(string tuto);
    }
}
