using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shuffle;
using NUnit.Framework;

namespace Shuffle.Tests
{
    /// <summary>
    /// Tests for the Shuffler class.
    /// </summary>
    [TestFixture]
    public class ShufflerTests
    {
        /// <summary>
        /// Checks that a null deck is handled correctly.
        /// </summary>
        [Test]
        public void Constructor_NullDeck()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => new Shuffler(null));

            StringAssert.Contains("Deck cannot be null", ex.Message);
        }

        /// <summary>
        /// Checks that an empty deck is handled correctly.
        /// </summary>
        [Test]
        public void Constructor_EmptyDeck()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Shuffler(new List<string>()));

            StringAssert.Contains("Deck cannot be empty", ex.Message);
        }

        /// <summary>
        /// Checks that a deck with a null card is handled correctly.
        /// </summary>
        [Test]
        public void Constructor_NullCard()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new Shuffler(new List<string>() { "Ace", null, "Queen" }));

            StringAssert.Contains("Deck cannot contain any null values", ex.Message);
        }

        /// <summary>
        /// Checks that the first card is returned after shuffling the deck.
        /// </summary>
        [Test]
        public void Start_FirstCard()
        {
            Shuffler shuffler = new Shuffler(new List<string>() { "Two", "Jack", "Queen" });

            Assert.IsNotNull(shuffler.Start());
        }

        /// <summary>
        /// Checks that the next card is returned from the deck.
        /// </summary>
        [Test]
        public void NextCard_Card()
        {
            Shuffler shuffler = new Shuffler(new List<string>() { "Two", "Jack", "Queen" });

            shuffler.Start();

            Assert.IsNotNull(shuffler.NextCard());
        }

        /// <summary>
        /// Checks that null is returned once the last card is used.
        /// </summary>
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
