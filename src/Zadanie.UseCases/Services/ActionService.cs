using Zadanie.Core.Entities;
using Zadanie.Core.Enums;
using Zadanie.UseCases.Configuration;
using Zadanie.UseCases.Interfaces;

namespace Zadanie.UseCases.Services
{
    public class ActionService : IActionService
    {
        public IEnumerable<CardAction> GetAllowedActions(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

                var rulesList = ActionRules.Rules
                .Where(rule => rule.Value(card))
                .Select(rule => rule.Key)
                .ToList();

            return rulesList;
        }

        public int GetAllowedActionsCount(Card card)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            var rulesCount = ActionRules.Rules.Count(rule => rule.Value(card));
            return rulesCount;
        }

        public bool IsActionAllowed(Card card, CardAction action)
        {
            if (card == null)
                throw new ArgumentNullException(nameof(card));

            var isAllowed = ActionRules.Rules.TryGetValue(action, out var rule) && rule(card);
            return isAllowed;
        }
    }
}