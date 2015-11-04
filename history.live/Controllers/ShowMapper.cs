using LiveCalendar.BusinessLogic;
using LiveCalendar.Models;

namespace LiveCalendar.Controllers
{
    public class ShowMapper
    {
        public SprintModel GetModelFromSprint(Concert concert)
        {
            return new SprintModel
            {
                Artist = concert.Artist.Name,
                Venue = concert.Venue != null ? concert.Venue.Name : null,
                Date = concert.Date.ToShortDateString(),
                Event = concert.Event != null ? concert.Event.Name : string.Empty
            };
        }
    }
}