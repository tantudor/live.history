using DreamSongs.MongoRepository;
using LiveCalendar.BusinessLogic;

namespace LiveCalendar.DataAccess
{
    public class EventRepository : MongoRepository<Event>
    {
        public Event GetEvent(string atEvent)
        {
            return GetSingle(x => x.Name == atEvent);
        }
    }
}