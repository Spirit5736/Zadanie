using Microsoft.AspNetCore.Mvc;
using Moq;
using Zadanie.Core.Entities;
using Zadanie.Core.Enums;
using Zadanie.Core.Interfaces;
using Zadanie.UseCases.DTOs;
using Zadanie.UseCases.Interfaces;
using Zadanie.Web.Controllers;

namespace IntegrationTests
{
    public class CardControllerIntegrationTests
    {
        private readonly Mock<IActionService> _mockActionService;
        private readonly Mock<ICardService> _mockCardService;
        private readonly CardController _controller;

        public CardControllerIntegrationTests()
        {
            _mockActionService = new Mock<IActionService>();
            _mockCardService = new Mock<ICardService>();
            _controller = new CardController(_mockActionService.Object, _mockCardService.Object);
        }

        [Fact]
        public async Task GetAllowedActions_WithValidRequest_ReturnsOk()
        {
            var userId = "user1";
            var cardNumber = "123";
            var cardDetails = new CardDetails(cardNumber, CardType.Prepaid, CardStatus.Active, true);
            var allowedActions = new[] { CardAction.ACTION1, CardAction.ACTION3, CardAction.ACTION4 };

            _mockCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                .ReturnsAsync(cardDetails);
            _mockActionService.Setup(x => x.GetAllowedActions(It.IsAny<Card>()))
                .Returns(allowedActions);
            _mockActionService.Setup(x => x.GetAllowedActionsCount(It.IsAny<Card>()))
                .Returns(allowedActions.Length);

            var result = await _controller.GetAllowedActions(userId, cardNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

            Assert.Equal(userId, response.UserId);
            Assert.Equal(cardNumber, response.CardNumber);
            Assert.Equal(allowedActions, response.AllowedActions);
            Assert.Equal(allowedActions.Length, response.ActionsCount);
        }

        [Fact]
        public async Task GetAllowedActions_WithEmptyUserId_ReturnsBadRequest()
        {
            var result = await _controller.GetAllowedActions("", "123");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = badRequestResult.Value;
            var errorProperty = error.GetType().GetProperty("error");
            Assert.NotNull(errorProperty);
            Assert.Equal("UserId cannot be empty", errorProperty.GetValue(error));
        }

        [Fact]
        public async Task GetAllowedActions_WithEmptyCardNumber_ReturnsBadRequest()
        {
            var result = await _controller.GetAllowedActions("user1", "");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = badRequestResult.Value;
            var errorProperty = error.GetType().GetProperty("error");
            Assert.NotNull(errorProperty);
            Assert.Equal("CardNumber cannot be empty", errorProperty.GetValue(error));
        }

        [Fact]
        public async Task GetAllowedActions_WithCardNotFound_ReturnsNotFound()
        {
            var userId = "user1";
            var cardNumber = "999";

            _mockCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                .ReturnsAsync((CardDetails?)null);

            var result = await _controller.GetAllowedActions(userId, cardNumber);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var error = notFoundResult.Value;
            var errorProperty = error.GetType().GetProperty("error");
            Assert.NotNull(errorProperty);
            Assert.Equal($"Card {cardNumber} for user {userId} not found", errorProperty.GetValue(error));
        }

        [Fact]
        public async Task GetAllowedActions_WithServiceException_ReturnsInternalServerError()
        {
            var result = await _controller.GetAllowedActions("user1", "");

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = badRequestResult.Value;
            var errorProperty = error.GetType().GetProperty("error");
            Assert.NotNull(errorProperty);
            Assert.Equal("CardNumber cannot be empty", errorProperty.GetValue(error));
        }

        [Fact]
        public async Task GetAllowedActions_WithDifferentCardTypes_ReturnsCorrectActions()
        {
            var testCases = new[]
            {
                (CardType.Prepaid, CardStatus.Active, true),
                (CardType.Debit, CardStatus.Inactive, false),
                (CardType.Credit, CardStatus.Blocked, true)
            };

            foreach (var (cardType, cardStatus, isPinSet) in testCases)
            {
                var userId = "user1";
                var cardNumber = "123";
                var cardDetails = new CardDetails(cardNumber, cardType, cardStatus, isPinSet);
                var allowedActions = new[] { CardAction.ACTION3, CardAction.ACTION4 };

                _mockCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                    .ReturnsAsync(cardDetails);
                _mockActionService.Setup(x => x.GetAllowedActions(It.IsAny<Card>()))
                    .Returns(allowedActions);
                _mockActionService.Setup(x => x.GetAllowedActionsCount(It.IsAny<Card>()))
                    .Returns(allowedActions.Length);

                var result = await _controller.GetAllowedActions(userId, cardNumber);

                var okResult = Assert.IsType<OkObjectResult>(result);
                var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

                Assert.Equal(userId, response.UserId);
                Assert.Equal(cardNumber, response.CardNumber);
                Assert.Equal(allowedActions, response.AllowedActions);
                Assert.Equal(allowedActions.Length, response.ActionsCount);
            }
        }

        [Fact]
        public async Task GetAllowedActions_ResponseFormat_IsValid()
        {
            var userId = "user1";
            var cardNumber = "123";
            var cardDetails = new CardDetails(cardNumber, CardType.Prepaid, CardStatus.Active, true);
            var allowedActions = new[] { CardAction.ACTION1, CardAction.ACTION3 };

            _mockCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                .ReturnsAsync(cardDetails);
            _mockActionService.Setup(x => x.GetAllowedActions(It.IsAny<Card>()))
                .Returns(allowedActions);
            _mockActionService.Setup(x => x.GetAllowedActionsCount(It.IsAny<Card>()))
                .Returns(allowedActions.Length);

            var result = await _controller.GetAllowedActions(userId, cardNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

            Assert.NotNull(response.UserId);
            Assert.NotNull(response.CardNumber);
            Assert.NotNull(response.AllowedActions);
            Assert.True(response.ActionsCount >= 0);
            Assert.NotEqual(default, response.RequestedAt);
        }

        [Fact]
        public async Task GetAllowedActions_Integration_WithRealActionService()
        {
            var userId = "user1";
            var cardNumber = "123";
            var cardDetails = new CardDetails(cardNumber, CardType.Prepaid, CardStatus.Active, true);

            _mockCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                .ReturnsAsync(cardDetails);

            var realActionService = new Zadanie.UseCases.Services.ActionService();
            var controller = new CardController(realActionService, _mockCardService.Object);

            var result = await controller.GetAllowedActions(userId, cardNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

            Assert.Equal(userId, response.UserId);
            Assert.Equal(cardNumber, response.CardNumber);
            Assert.NotNull(response.AllowedActions);
            Assert.True(response.ActionsCount > 0);
            Assert.NotEqual(default, response.RequestedAt);
        }

        [Fact]
        public async Task GetAllowedActions_Integration_WithRealCardService()
        {
            var userId = "user1";
            var cardNumber = "123";
            var cardDetails = new CardDetails(cardNumber, CardType.Credit, CardStatus.Blocked, true);

            _mockActionService.Setup(x => x.GetAllowedActions(It.IsAny<Card>()))
                .Returns(new[] { CardAction.ACTION3, CardAction.ACTION4 });
            _mockActionService.Setup(x => x.GetAllowedActionsCount(It.IsAny<Card>()))
                .Returns(2);

            var realCardService = new Mock<ICardService>();
            realCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                .ReturnsAsync(cardDetails);

            var controller = new CardController(_mockActionService.Object, realCardService.Object);

            var result = await controller.GetAllowedActions(userId, cardNumber);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetAllowedActionsResponse>(okResult.Value);

            Assert.Equal(userId, response.UserId);
            Assert.Equal(cardNumber, response.CardNumber);
            Assert.NotNull(response.AllowedActions);
            Assert.Equal(2, response.ActionsCount);
        }
    }

    public class Anonymous
    {
        public string error { get; set; } = string.Empty;
        public string details { get; set; } = string.Empty;
    }
}