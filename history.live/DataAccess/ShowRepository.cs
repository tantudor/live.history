using System.Collections.Generic;
using System.Linq;
using DreamSongs.MongoRepository;
using LiveCalendar.BusinessLogic;

namespace LiveCalendar.DataAccess
{
    public class ShowRepository : MongoRepository<Concert>
    {
        public IEnumerable<Concert> GetAllShows()
        {
            return All().ToList();
        }
    }
}