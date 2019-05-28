using System;

namespace Patterns.ChainOfResponsability
{
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query query) => Queries?.Invoke(sender, query);
    }

    public class Query
    {
        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName ?? throw new ArgumentNullException(nameof(creatureName));
            WhatToQuery = whatToQuery;
            Value = value;
        }

        public string CreatureName { get; }
        public Argument WhatToQuery { get; }
        public int Value { get; set; }

        public enum Argument
        {
            Attack,
            Defense
        }
    }

    public class Creature
    {
        private readonly Game game;
        private readonly int attack;
        private readonly int defense;

        public Creature(Game game, int attack, int defense, string name)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.attack = attack;
            this.defense = defense;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public string Name { get; }
        public int Attack
        {
            get
            {
                var query = new Query(Name, Query.Argument.Attack, attack);
                game.PerformQuery(this, query);

                return query.Value;
            }
        }
        public int Defense
        {
            get
            {
                var query = new Query(Name, Query.Argument.Defense, defense);
                game.PerformQuery(this, query);

                return query.Value;
            }
        }

        public override string ToString()
        {
            return $"{{{nameof(Name)}={Name}, {nameof(Attack)}={Attack}, {nameof(Defense)}={Defense}}}";
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected Game game;
        protected Creature creature;

        protected CreatureModifier(Game game, Creature creature)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            this.creature = creature ?? throw new ArgumentNullException(nameof(creature));

            game.Queries += Handle;
        }

        public abstract void Handle(object sender, Query query);

        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        {

        }

        public override void Handle(object sender, Query query)
        {
            if (query.CreatureName == creature.Name && query.WhatToQuery == Query.Argument.Attack)
                query.Value *= 2;
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        {

        }

        public override void Handle(object sender, Query query)
        {
            if (query.CreatureName == creature.Name && query.WhatToQuery == Query.Argument.Defense)
                query.Value += 2;
        }
    }

    public static class BrokerChain
    {
        public static void Start()
        {
            var game = new Game();
            var goblin = new Creature(game, 3, 3, "Goblin");
            Console.WriteLine(goblin);

            using (new DoubleAttackModifier(game, goblin))
            using (new IncreaseDefenseModifier(game, goblin))
                Console.WriteLine(goblin);


            Console.WriteLine(goblin);
        }
    }
}
