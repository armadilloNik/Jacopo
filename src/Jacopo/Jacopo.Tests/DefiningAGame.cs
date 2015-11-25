using System;
using System.Collections.Generic;
using System.Linq;
using Should;

namespace Jacopo.Tests
{
    /// <summary>
    ///  it seems like i don't really need all the horsepower and specificity 
    ///  lisp may actually be more appropriate in this case. so what does that mean for the rest of things?
    /// </summary>

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

        public void PlayersShouldHaveTokens()
        {
            var noThanks = new NoThanksGame();

            var players = noThanks.Components.OfType<Player>();

            var tokenCounts = players.Select(p => p.Components.OfType<Token>().Count());

            tokenCounts.Any(c => c != 11).ShouldBeFalse();

        }

        public void StartingAGame()
        {
            var noThanks = new NoThanksGame();

            noThanks.Start();

            var deck = noThanks.Components.OfType<CardDeck>().Single();

            var card = deck.ActiveCard;

            card.ShouldNotBeNull();

        }
    }

    public class Token : IGameComponent
    {
    }

    public class Player : IGameComponent
    {
        private readonly List<IGameComponent> _components;
        public IEnumerable<IGameComponent> Components => _components;

        public Player(IEnumerable<IGameComponent> components = null)
        {
            _components = new List<IGameComponent>();

            if(components.Any())
                _components.AddRange(components);
        }

        public void GiveGomponent(IGameComponent component)
        {
            _components.Add(component);
        }

    }

    public class NoThanksGame
    {
        private readonly List<IGameComponent> _components;
        public IEnumerable<IGameComponent> Components => _components;

        public NoThanksGame()
        {
            _components = new List<IGameComponent> {new CardDeck(() => Enumerable.Range(2, 33))};

            _components.AddRange(Enumerable.Range(1,3).Select(p => new Player(Enumerable.Range(1,11).Select(t => new Token()))));
        }

        public void Start()
        {

        }
    }

    public class CardDeck : IGameComponent
    {
        public IEnumerable<Card> Cards { get; set; }

        public Card ActiveCard => _activeCard;

        private Card _activeCard;

        public CardDeck(Func<IEnumerable<int>> cardValues)
        {
            Cards = cardValues().Select(v => new Card(v));

            _activeCard = Cards.First();
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
