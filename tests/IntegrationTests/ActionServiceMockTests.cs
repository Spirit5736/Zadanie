using Moq;
using Zadanie.Core.Entities;
using Zadanie.Core.Enums;
using Zadanie.UseCases.Interfaces;
using Zadanie.UseCases.Services;

namespace IntegrationTests
{
    public class ActionServiceMockTests
    {
        private readonly Mock<IActionService> _mockActionService;
        private readonly ActionService _realActionService;

        public ActionServiceMockTests()
        {
            _mockActionService = new Mock<IActionService>();
            _realActionService = new ActionService();
        }

        [Fact]
        public void GetAllowedActions_WithValidCard_ReturnsExpectedActions()
        {
            var card = new Card("123", CardType.Prepaid, CardStatus.Active, true);
            var expectedActions = new[] { CardAction.ACTION1, CardAction.ACTION3, CardAction.ACTION4 };

            _mockActionService.Setup(x => x.GetAllowedActions(card))
                .Returns(expectedActions);

            var result = _mockActionService.Object.GetAllowedActions(card);

            Assert.Equal(expectedActions, result);
            _mockActionService.Verify(x => x.GetAllowedActions(card), Times.Once);
        }

        [Fact]
        public void GetAllowedActionsCount_WithValidCard_ReturnsExpectedCount()
        {
            var card = new Card("123", CardType.Credit, CardStatus.Blocked, false);
            var expectedCount = 5;

            _mockActionService.Setup(x => x.GetAllowedActionsCount(card))
                .Returns(expectedCount);

            var result = _mockActionService.Object.GetAllowedActionsCount(card);

            Assert.Equal(expectedCount, result);
            _mockActionService.Verify(x => x.GetAllowedActionsCount(card), Times.Once);
        }

        [Fact]
        public void IsActionAllowed_WithValidCardAndAction_ReturnsExpectedResult()
        {
            var card = new Card("123", CardType.Debit, CardStatus.Inactive, true);
            var action = CardAction.ACTION2;
            var expectedResult = true;

            _mockActionService.Setup(x => x.IsActionAllowed(card, action))
                .Returns(expectedResult);

            var result = _mockActionService.Object.IsActionAllowed(card, action);

            Assert.Equal(expectedResult, result);
            _mockActionService.Verify(x => x.IsActionAllowed(card, action), Times.Once);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithPrepaidActiveCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Prepaid, CardStatus.Active, true);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION1, result);
            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION6, result);
            Assert.Contains(CardAction.ACTION8, result);
            Assert.Contains(CardAction.ACTION9, result);
            Assert.Contains(CardAction.ACTION10, result);
            Assert.Contains(CardAction.ACTION11, result);
            Assert.Contains(CardAction.ACTION12, result);
            Assert.Contains(CardAction.ACTION13, result);

            Assert.DoesNotContain(CardAction.ACTION2, result);
            Assert.DoesNotContain(CardAction.ACTION7, result);
            Assert.DoesNotContain(CardAction.ACTION5, result);

            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithCreditBlockedCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Credit, CardStatus.Blocked, true);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION5, result);
            Assert.Contains(CardAction.ACTION6, result);
            Assert.Contains(CardAction.ACTION7, result);
            Assert.Contains(CardAction.ACTION8, result);
            Assert.Contains(CardAction.ACTION9, result);

            Assert.DoesNotContain(CardAction.ACTION1, result);
            Assert.DoesNotContain(CardAction.ACTION2, result);
            Assert.DoesNotContain(CardAction.ACTION10, result);
            Assert.DoesNotContain(CardAction.ACTION11, result);
            Assert.DoesNotContain(CardAction.ACTION12, result);
            Assert.DoesNotContain(CardAction.ACTION13, result);

            Assert.Equal(7, result.Count);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithDebitRestrictedCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Debit, CardStatus.Restricted, false);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION9, result);

            Assert.DoesNotContain(CardAction.ACTION1, result);
            Assert.DoesNotContain(CardAction.ACTION2, result);
            Assert.DoesNotContain(CardAction.ACTION6, result);
            Assert.DoesNotContain(CardAction.ACTION7, result);
            Assert.DoesNotContain(CardAction.ACTION8, result);
            Assert.DoesNotContain(CardAction.ACTION10, result);
            Assert.DoesNotContain(CardAction.ACTION11, result);
            Assert.DoesNotContain(CardAction.ACTION12, result);
            Assert.DoesNotContain(CardAction.ACTION13, result);
            Assert.DoesNotContain(CardAction.ACTION5, result);


            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithClosedCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Prepaid, CardStatus.Closed, false);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION9, result);

            Assert.DoesNotContain(CardAction.ACTION1, result);
            Assert.DoesNotContain(CardAction.ACTION2, result);
            Assert.DoesNotContain(CardAction.ACTION6, result);
            Assert.DoesNotContain(CardAction.ACTION7, result);
            Assert.DoesNotContain(CardAction.ACTION8, result);
            Assert.DoesNotContain(CardAction.ACTION10, result);
            Assert.DoesNotContain(CardAction.ACTION11, result);
            Assert.DoesNotContain(CardAction.ACTION12, result);
            Assert.DoesNotContain(CardAction.ACTION13, result);
            Assert.DoesNotContain(CardAction.ACTION5, result);


            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithExpiredCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Credit, CardStatus.Expired, true);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION5, result);
            Assert.Contains(CardAction.ACTION9, result);

            Assert.DoesNotContain(CardAction.ACTION1, result);
            Assert.DoesNotContain(CardAction.ACTION2, result);
            Assert.DoesNotContain(CardAction.ACTION6, result);
            Assert.DoesNotContain(CardAction.ACTION7, result);
            Assert.DoesNotContain(CardAction.ACTION8, result);
            Assert.DoesNotContain(CardAction.ACTION10, result);
            Assert.DoesNotContain(CardAction.ACTION11, result);
            Assert.DoesNotContain(CardAction.ACTION12, result);
            Assert.DoesNotContain(CardAction.ACTION13, result);

            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithOrderedCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Debit, CardStatus.Ordered, false);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION7, result);
            Assert.Contains(CardAction.ACTION8, result);
            Assert.Contains(CardAction.ACTION9, result);
            Assert.Contains(CardAction.ACTION10, result);
            Assert.Contains(CardAction.ACTION12, result);
            Assert.Contains(CardAction.ACTION13, result);

            Assert.DoesNotContain(CardAction.ACTION1, result);
            Assert.DoesNotContain(CardAction.ACTION2, result);
            Assert.DoesNotContain(CardAction.ACTION5, result);
            Assert.DoesNotContain(CardAction.ACTION6, result);
            Assert.DoesNotContain(CardAction.ACTION11, result);

            Assert.Equal(8, result.Count);
        }

        [Fact]
        public void GetAllowedActions_RealService_WithInactiveCard_ReturnsCorrectActions()
        {
            var card = new Card("123", CardType.Prepaid, CardStatus.Inactive, true);
            var result = _realActionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION2, result);
            Assert.Contains(CardAction.ACTION3, result);
            Assert.Contains(CardAction.ACTION4, result);
            Assert.Contains(CardAction.ACTION6, result);
            Assert.Contains(CardAction.ACTION8, result);
            Assert.Contains(CardAction.ACTION9, result);
            Assert.Contains(CardAction.ACTION10, result);
            Assert.Contains(CardAction.ACTION11, result);
            Assert.Contains(CardAction.ACTION12, result);
            Assert.Contains(CardAction.ACTION13, result);

            Assert.DoesNotContain(CardAction.ACTION1, result);
            Assert.DoesNotContain(CardAction.ACTION5, result);
            Assert.DoesNotContain(CardAction.ACTION7, result);



            Assert.Equal(10, result.Count);
        }

    }
}