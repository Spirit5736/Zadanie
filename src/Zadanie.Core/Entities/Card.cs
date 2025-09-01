using Zadanie.Core.Enums;

namespace Zadanie.Core.Entities
{
    public record Card(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);
}