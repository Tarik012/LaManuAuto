using LaManuAuto.Data;
using LaManuAuto.Models;

namespace LaManuAuto.Services
{
    public class DataService : IDataService
    {
       private readonly LAMANU_AUTOContext _Context;

        public DataService(LAMANU_AUTOContext context)
        {
            _Context = context;
        }
        public List<Tutoriel> GetTutorials(string tuto)
        {
            var tutoriels = _Context.Tutoriels
                .Where(x => x.Id == 1);
            var users = _Context.Users
                .Where(x => x.Email.EndsWith("gmail.com"));
       
        if(users.Any())
            return tutoriels.ToList();
        return new List<Tutoriel>();
        }

    }
}
