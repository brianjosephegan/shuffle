using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Shuffle
{
    /// <summary>
    /// The logic for shuffling a deck of cards
    /// </summary>
    internal class Shuffler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Kings.Game"/> class.
        /// </summary>
        /// <param name="deck">Deck to use for the game</param>
        public Shuffler(IEnumerable<string> deck)
        {
            if (deck == null)
            {
                throw new ArgumentNullException(nameof(deck), "Deck cannot be null.");
            }

            if (!deck.Any())
            {
                throw new ArgumentException("Deck cannot be empty.", nameof(deck));
            }

            if (deck.Any(x => x == null))
            {
                throw new ArgumentException("Deck cannot contain any null values.", nameof(deck));
            }

            this.deck = deck;
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        /// <returns>The first card in the deck</returns>
        public string Start()
        {
            currentDeck = ShuffleDeck(deck);
            currentDeck.MoveNext();
            return currentDeck.Current;
        }

        /// <summary>
        /// Returns the next card in the deck
        /// </summary>
        /// <returns>The next card or null if no cards are left</returns>
        public string NextCard()
        {
            if (currentDeck.MoveNext())
            {
                return currentDeck.Current;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Shuffles the specified deck
        /// </summary>
        /// <param name="deckToShuffle">The deck to shuffle</param>
        /// <returns>A shuffled deck</returns>
        private IEnumerator<string> ShuffleDeck(IEnumerable<string> deckToShuffle)
        {
            return deckToShuffle.OrderBy(x => random.Next()).ToList().GetEnumerator();
        }

        /// <summary>
        /// Current deck being used in the game
        /// </summary>
        private IEnumerator<string> currentDeck;

        /// <summary>
        /// The deck of cards to use for a game
        /// </summary>
        private readonly IEnumerable<string> deck;

        /// <summary>
        /// Random object used for shuffling the cards
        /// </summary>
        private readonly Random random = new Random();
    }
}