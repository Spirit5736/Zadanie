using Zadanie.Core.Entities;

namespace Zadanie.Core.Interfaces
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
    }
}