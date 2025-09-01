using Zadanie.Core.Enums;

namespace Zadanie.Core.Entities
{
    public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);
}