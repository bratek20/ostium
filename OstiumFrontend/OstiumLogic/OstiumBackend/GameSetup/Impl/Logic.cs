using System;
using System.Collections.Generic;
using System.Linq;
using GameComponents.Api;
using GameSetup.Api;

namespace GameSetup.Impl
{
    public class GameSetupApiLogic : GameSetupApi
    {
        private List<CreatureCard> hand = new List<CreatureCard>
        {
            CreatureCard.Create(new CreatureCardId("Mouse1")),
            CreatureCard.Create(new CreatureCardId("Mouse2"))
        };

        private CreatureCard? attackRow = null;
        private CreatureCard? defenseRow = null;

        public Game StartGame()
        {
            return ToApiGame();
        }

        public Game PlayCard(CreatureCardId cardId, RowType row)
        {
            var card = hand.First(c => c.GetId().Equals(cardId));
            hand.Remove(card);

            switch (row)
            {
                case RowType.ATTACK:
                    attackRow = CreatureCard.Create(cardId);
                    break;
                case RowType.DEFENSE:
                    defenseRow = CreatureCard.Create(cardId);
                    break;
            }

            return ToApiGame();
        }

        public Game MoveCard(CreatureCardId cardId, RowType from, RowType to)
        {
            CreatureCard? card = from switch
            {
                RowType.ATTACK => attackRow,
                RowType.DEFENSE => defenseRow,
                _ => throw new InvalidOperationException($"No card in {from} row")
            };

            if (card == null)
            {
                throw new InvalidOperationException($"No card in {from} row");
            }

            switch (to)
            {
                case RowType.ATTACK:
                    attackRow = card;
                    break;
                case RowType.DEFENSE:
                    defenseRow = card;
                    break;
            }

            switch (from)
            {
                case RowType.ATTACK:
                    attackRow = null;
                    break;
                case RowType.DEFENSE:
                    defenseRow = null;
                    break;
            }

            return ToApiGame();
        }

        private Game ToApiGame()
        {
            return Game.Create(
                Table.Create(
                    GateDurabilityCard.Create(
                        new GateDurabilityMarker(15),
                        new GateDurabilityMarker(15)
                    ),
                    attackRow,
                    defenseRow,
                    GateCard.Create(false)
                ),
                Hand.Create(hand.ToList())
            );
        }
    }
}
