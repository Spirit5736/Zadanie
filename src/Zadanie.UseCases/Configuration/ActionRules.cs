using Zadanie.Core.Entities;
using Zadanie.Core.Enums;

namespace Zadanie.UseCases.Configuration
{
    public static class ActionRules
    {
        public static readonly Dictionary<CardAction, Func<Card, bool>> Rules = new()
        {
            { CardAction.ACTION1, card => card.CardStatus == CardStatus.Active },
            { CardAction.ACTION2, card => card.CardStatus == CardStatus.Inactive },
            { CardAction.ACTION3, card => true },
            { CardAction.ACTION4, card => true },
            { CardAction.ACTION5, card => IsAction5Allowed(card) },
            { CardAction.ACTION6, IsAction6Allowed },
            { CardAction.ACTION7, IsAction7Allowed },
            { CardAction.ACTION8, IsAction8Allowed },
            { CardAction.ACTION9, card => true },
            { CardAction.ACTION10, IsAction10Allowed },
            { CardAction.ACTION11, IsAction11Allowed },
            { CardAction.ACTION12, IsAction12Allowed },
            { CardAction.ACTION13, IsAction13Allowed }
        };

        private static bool IsAction6Allowed(Card card)
        {
            var isAction6Allowed = card.CardStatus switch
            {
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active => card.IsPinSet,
                CardStatus.Blocked => card.IsPinSet,
                _ => false
            };

            return isAction6Allowed;
        }

        private static bool IsAction7Allowed(Card card)
        {
            var isAction7Allowed = card.CardStatus switch
            {
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active => !card.IsPinSet,
                CardStatus.Blocked => card.IsPinSet,
                _ => false
            };
            return isAction7Allowed;
        }

        private static bool IsAction8Allowed(Card card)
        {
            var isAction8Allowed = card.CardStatus switch
            {
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active or CardStatus.Blocked => true,
                _ => false
            };
            return isAction8Allowed;
        }

        private static bool IsAction10Allowed(Card card)
        {
            var isAction10Allowed = card.CardStatus switch
            {
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active => true,
                _ => false
            };
            return isAction10Allowed;
        }

        private static bool IsAction11Allowed(Card card)
        {
            var isAction11Allowed = card.CardStatus switch
            {
                CardStatus.Inactive or CardStatus.Active => true,
                _ => false
            };
            return isAction11Allowed;
        }

        private static bool IsAction12Allowed(Card card)
        {
            var isAction12Allowed = card.CardStatus switch
            {
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active => true,
                _ => false
            };
            return isAction12Allowed;
        }

        private static bool IsAction13Allowed(Card card)
        {
            var isAction13Allowed = card.CardStatus switch
            {
                CardStatus.Ordered or CardStatus.Inactive or CardStatus.Active => true,
                _ => false
            };
            return isAction13Allowed;
        }

        private static bool IsAction5Allowed(Card card)
        {
            return card.CardType == CardType.Credit;
        }
    }
}