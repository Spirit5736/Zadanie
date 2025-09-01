using Zadanie.Core.Enums;

namespace Zadanie.UseCases.DTOs
{
    public record GetAllowedActionsResponse
    {
        public string UserId { get; init; } = string.Empty;
        public string CardNumber { get; init; } = string.Empty;
        public IEnumerable<CardAction> AllowedActions { get; init; } = [];
        public int ActionsCount { get; init; }
        public DateTime RequestedAt { get; init; } = DateTime.UtcNow;
    }
}