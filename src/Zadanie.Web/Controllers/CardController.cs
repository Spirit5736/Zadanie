using Microsoft.AspNetCore.Mvc;
using Zadanie.Core.Entities;
using Zadanie.Core.Interfaces;
using Zadanie.UseCases.DTOs;
using Zadanie.UseCases.Interfaces;

namespace Zadanie.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IActionService _actionService;
        private readonly ICardService _cardService;

        public CardController(IActionService actionService, ICardService cardService)
        {
            _actionService = actionService ?? throw new ArgumentNullException(nameof(actionService));
            _cardService = cardService ?? throw new ArgumentNullException(nameof(cardService));
        }

        [HttpGet("actions")]
        public async Task<IActionResult> GetAllowedActions(
            [FromQuery] string userId,
            [FromQuery] string cardNumber)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return BadRequest(new { error = "UserId cannot be empty" });
                }

                if (string.IsNullOrWhiteSpace(cardNumber))
                {
                    return BadRequest(new { error = "CardNumber cannot be empty" });
                }

                var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
                
                if (cardDetails == null)
                {
                    return NotFound(new { error = $"Card {cardNumber} for user {userId} not found" });
                }

                var card = new Card(
                    cardDetails.CardNumber,
                    cardDetails.CardType,
                    cardDetails.CardStatus,
                    cardDetails.IsPinSet
                );

                var allowedActions = _actionService.GetAllowedActions(card);
                var actionsCount = _actionService.GetAllowedActionsCount(card);

                var response = new GetAllowedActionsResponse
                {
                    UserId = userId,
                    CardNumber = cardNumber,
                    AllowedActions = allowedActions,
                    ActionsCount = actionsCount,
                    RequestedAt = DateTime.UtcNow
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal server error", details = ex.Message });
            }
        }
    }
}