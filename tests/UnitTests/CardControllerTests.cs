using Microsoft.AspNetCore.Mvc;
using Moq;
using Zadanie.Core.Entities;
using Zadanie.Core.Enums;
using Zadanie.Core.Interfaces;
using Zadanie.UseCases.DTOs;
using Zadanie.UseCases.Interfaces;
using Zadanie.Web.Controllers;

namespace UnitTests
{
    public class CardControllerTests
    {
        private readonly Mock<IActionService> _mockActionService;
        private readonly Mock<ICardService> _mockCardService;
        private readonly CardController _controller;

        public CardControllerTests()
        {
            _mockActionService = new Mock<IActionService>();
            _mockCardService = new Mock<ICardService>();
            _controller = new CardController(_mockActionService.Object, _mockCardService.Object);
        }

        [Fact]
        public void Constructor_WithNullActionService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CardController(null!, _mockCardService.Object));
        }

        [Fact]
        public void Constructor_WithNullCardService_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CardController(_mockActionService.Object, null!));
        }

        [Fact]
        public async Task GetAllowedActions_WithValidInputs_ReturnsOkResult()
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
            Assert.True(response.RequestedAt <= DateTime.UtcNow);
            Assert.True(response.RequestedAt > DateTime.UtcNow.AddMinutes(-1));
        }

        [Fact]
        public async Task GetAllowedActions_CreatesCorrectCardEntity()
        {
            var userId = "user1";
            var cardNumber = "123";
            var cardDetails = new CardDetails(cardNumber, CardType.Credit, CardStatus.Blocked, true);
            var allowedActions = new[] { CardAction.ACTION3, CardAction.ACTION4 };

            _mockCardService.Setup(x => x.GetCardDetails(userId, cardNumber))
                .ReturnsAsync(cardDetails);
            _mockActionService.Setup(x => x.GetAllowedActions(It.IsAny<Card>()))
                .Returns(allowedActions);
            _mockActionService.Setup(x => x.GetAllowedActionsCount(It.IsAny<Card>()))
                .Returns(allowedActions.Length);

            await _controller.GetAllowedActions(userId, cardNumber);

            _mockActionService.Verify(x => x.GetAllowedActions(It.Is<Card>(c =>
                c.CardNumber == cardNumber &&
                c.CardType == CardType.Credit &&
                c.CardStatus == CardStatus.Blocked &&
                c.IsPinSet == true)), Times.Once);

            _mockActionService.Verify(x => x.GetAllowedActionsCount(It.Is<Card>(c =>
                c.CardNumber == cardNumber &&
                c.CardType == CardType.Credit &&
                c.CardStatus == CardStatus.Blocked &&
                c.IsPinSet == true)), Times.Once);
        }

        [Fact]
        public async Task GetAllowedActions_ResponseContainsAllRequiredFields()
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
    }

    public class Anonymous
    {
        public string error { get; set; } = string.Empty;
        public string details { get; set; } = string.Empty;
    }
}