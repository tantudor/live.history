using System;
using DreamSongs.MongoRepository;

namespace LiveCalendar.BusinessLogic
{
    public class Concert : Entity
    {
        public Artist Artist { get; set; }
        public Venue Venue { get; set; }
        public DateTime Date { get; set; }
        public Event Event { get; set; }
    }
}