using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiveCalendar.BusinessLogic;
using LiveCalendar.DataAccess;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LiveCalendar.Controllers
{
    public class LiveController : Controller
    {
        private readonly ShowRepository _showRepository;
        private EventRepository _eventRepository;

        public LiveController()
        {
            _showRepository = new ShowRepository();
            _eventRepository = new EventRepository();
        }

        public string GetShows()
        {
            var model = _showRepository.GetAllShows().OrderByDescending(x => x.Date).Select(new ShowMapper().GetModelFromSprint);
            return JsonConvert.SerializeObject(model, new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy-MM-dd"
                });
        }

        public void Delete(string id)
        {
            _showRepository.Delete(id);
        }

        [HttpPost]
        public ActionResult Add(string artist, string venue, string date, string atEvent)
        {
            var show = new Concert
            {
                Artist = new Artist { Name = artist },
                Venue = new Venue { Name = venue },
                Date = DateTime.Parse(date).AddHours(20),
                Event = GetOrCreateEvent(atEvent)
            };

            _showRepository.Add(show);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddFromFile()
        {
            return View("AddFromFile");
        }

        private Event GetOrCreateEvent(string atEvent)
        {
            var theEvent = _eventRepository.GetEvent(atEvent);
            if (theEvent == null)
            {
                theEvent = new Event
                {
                    Name = atEvent
                };
            }
            return theEvent;
        }

        [HttpPost]
        public void Read(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var reader = new BinaryReader(file.InputStream);
                byte[] binData = reader.ReadBytes((int)file.InputStream.Length);
                
                string result = System.Text.Encoding.UTF7.GetString(binData);
                var strings = result.Split('\r','\n').Where(x => !string.IsNullOrWhiteSpace(x));

                foreach (var s in strings)
                {
                    var show = s.Trim();

                    var indexOfFirstSpace = show.IndexOf(' ');

                    var dateStr = show.Substring(0, indexOfFirstSpace);

                    if (dateStr.Length == 6)
                    {
                        dateStr = "20" + dateStr;
                    }
                    var date = DateTime.ParseExact(dateStr, "yyyyMMdd", CultureInfo.InvariantCulture);

                    var showStr = show.Substring(indexOfFirstSpace, show.Length - indexOfFirstSpace).Trim();

                    string artist;
                    if (!showStr.Contains('-'))
                    {
                        artist = showStr;
                        CreateShow(date, artist);
                    }
                    else
                    {
                        var placeAndArtist = showStr.Split('-');
                        artist = placeAndArtist[1].Trim();

                        Venue venue;
                        if (placeAndArtist[0].Contains(','))
                        {
                            var venueAndCity = placeAndArtist[0].Split(',');
                            venue = new Venue
                            {
                                Name = venueAndCity[0],
                                City = venueAndCity[1]
                            };
                        }
                        else
                        {
                            venue = new Venue
                            {
                                Name = placeAndArtist[0].Trim()
                            };
                        }
                        CreateShow(date, artist, venue);    
                    }
                }
            }

        }

        private void CreateShow(DateTime date, string artist, Venue venue = null)
        {
            var show = new Concert
            {
                Artist = new Artist { Name = artist },
                Date = date,
                Venue = venue
            };
            _showRepository.Add(show);
        }
    }
}