using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shuffle;
using NUnit.Framework;

namespace Shuffle.Tests
{
    [TestFixture]
    public class ShufflerTests
    {
        [Test]
        public void Constructor_NullDeck()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new Shuffler(null));

            StringAssert.Contains("Deck cannot be null", ex.Message);
        }

        [Test]
        public void Constructor_EmptyDeck()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Shuffler(new List<string>()));

            StringAssert.Contains("Deck cannot be empty", ex.Message);
        }

        [Test]
        public void Constructor_NullCard()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Shuffler(new List<string>() { "Ace", null, "Queen" }));

            StringAssert.Contains("Deck cannot contain any null values", ex.Message);
        }

        [Test]
        public void Start_FirstCard()
        {
            Shuffler shuffler = new Shuffler(new List<string>() { "Two", "Jack", "Queen" });

            Assert.IsNotNull(shuffler.Start());
        }

        [Test]
        public void NextCard_Card()
        {
            Shuffler shuffler = new Shuffler(new List<string>() { "Two", "Jack", "Queen" });

            shuffler.Start();

            Assert.IsNotNull(shuffler.NextCard());
        }

        [Test]
        public void NextCard_LastCard()
        {
            Shuffler shuffler = new Shuffler(new List<string>() { "Two", "Jack", "Queen" });

            shuffler.Start();
            shuffler.NextCard();
            shuffler.NextCard();

            Assert.IsNull(shuffler.NextCard());
        }
    }
}
