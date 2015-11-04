using NUnit.Framework;

namespace LiveCalendar.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        protected virtual void SetUpFixtureBaseTest()
        {
        }

        public virtual void SetUpFixture()
        {
        }

        [SetUp]
        public void SetUpBase_DoNotCall()
        {
            SetUpBaseTest();
            SetUp();
        }

        public virtual void SetUp()
        {
        }

        protected virtual void SetUpBaseTest()
        {
        }
    }
}
