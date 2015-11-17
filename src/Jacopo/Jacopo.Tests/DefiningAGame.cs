using System;
using System.Collections.Generic;
using System.Linq;
using Should;

namespace Jacopo.Tests
{
    public class DefiningNoThanksTests
    {
        public void ShouldHaveOneAndOnlyOneDeck()
        {
            var noThanks = new NoThanksGame();

            var decks = noThanks.Components.OfType<CardDeck>();

            decks.Count().ShouldEqual(1);
        }

        public void ShouldHaveCards2To34()
        {
            var expectedRange = Enumerable.Range(2, 33).ToList();

            var noThanks = new NoThanksGame();

            var deck = noThanks.Components.OfType<CardDeck>().Single();

            var actualRange = deck.Cards.Select(c => c.NumericValue).OrderBy(i => i);


            expectedRange.Except(actualRange).Count().ShouldEqual(0);
            actualRange.Except(expectedRange).Count().ShouldEqual(0);
        }

        public void ShouldHavePlayers()
        {
            var noThanks = new NoThanksGame();

            var players = noThanks.Components.OfType<Player>();

            players.Count().ShouldBeInRange(2, 5);
        }
    }

    public class Player : IGameComponent 
    {

    }

    public class NoThanksGame
    {
        private readonly List<IGameComponent> _components;
        public IEnumerable<IGameComponent> Components => _components;

        public NoThanksGame()
        {
            _components = new List<IGameComponent> {new CardDeck(() => Enumerable.Range(2, 33))};

            _components.AddRange(Enumerable.Range(1,3).Select(p => new Player()));

        }
    }

    public class CardDeck : IGameComponent
    {
        public IEnumerable<Card> Cards { get; set; }

        public CardDeck(Func<IEnumerable<int>> cardValues)
        {
            Cards = cardValues().Select(v => new Card(v));
        }
    }

    public class Card
    {
        public int NumericValue { get; private set; }

        public Card(int value)
        {
            NumericValue = value;
        }
    }

    public interface IGameComponent
    {
    }
}
