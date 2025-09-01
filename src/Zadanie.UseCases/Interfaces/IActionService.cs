using Zadanie.Core.Entities;
using Zadanie.Core.Enums;

namespace Zadanie.UseCases.Interfaces
{
    public interface IActionService
    {
        IEnumerable<CardAction> GetAllowedActions(Card card);
        int GetAllowedActionsCount(Card card);
        bool IsActionAllowed(Card card, CardAction action);
    }
}