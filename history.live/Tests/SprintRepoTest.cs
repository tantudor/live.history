using DreamSongs.MongoRepository;
using NUnit.Framework;
using LiveCalendar.BusinessLogic;

namespace LiveCalendar.Tests
{
    public class SprintRepoTest : BaseTest
    {
        [Test]
        public void AddSprint()
        {
            var repo = new MongoRepository<Concert>();

            var newSprint = new Concert
                {
                    Artist = new Artist
                    {
                        Name = "Eddie Meduza"
                    }
                };

            repo.Add(newSprint);

            var sprint = repo.All(c => c.Artist.Name == "Eddie Meduza");

            Assert.That(sprint, Is.EqualTo(newSprint));
        }
    }
}