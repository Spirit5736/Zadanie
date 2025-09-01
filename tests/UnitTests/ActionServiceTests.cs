using Zadanie.Core.Entities;
using Zadanie.Core.Enums;
using Zadanie.UseCases.Services;

namespace UnitTests
{
    public class ActionServiceTests
    {
        private readonly ActionService _actionService;

        public ActionServiceTests()
        {
            _actionService = new ActionService();
        }

        [Fact]
        public void GetAllowedActions_WithNullCard_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _actionService.GetAllowedActions(null!));
        }

        [Fact]
        public void GetAllowedActionsCount_WithNullCard_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _actionService.GetAllowedActionsCount(null!));
        }

        [Fact]
        public void IsActionAllowed_WithNullCard_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _actionService.IsActionAllowed(null!, CardAction.ACTION1));
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Inactive, false)]
        [InlineData(CardType.Debit, CardStatus.Inactive, false)]
        public void GetAllowedActions_InactiveStatus_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION2, allowedActions);
            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION7, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);
            Assert.Contains(CardAction.ACTION10, allowedActions);
            Assert.Contains(CardAction.ACTION11, allowedActions);
            Assert.Contains(CardAction.ACTION12, allowedActions);
            Assert.Contains(CardAction.ACTION13, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION1, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION5, allowedActions);

            Assert.Equal(10, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void GetAllowedActions_ActiveStatus_CreditCard_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION1, allowedActions);
            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION5, allowedActions);
            Assert.Contains(CardAction.ACTION6, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);
            Assert.Contains(CardAction.ACTION10, allowedActions);
            Assert.Contains(CardAction.ACTION11, allowedActions);
            Assert.Contains(CardAction.ACTION12, allowedActions);
            Assert.Contains(CardAction.ACTION13, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION7, allowedActions);


            Assert.Equal(11, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, false)]
        [InlineData(CardType.Debit, CardStatus.Active, false)]
        public void GetAllowedActions_ActiveStatus_NonCreditCard_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION1, allowedActions);
            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION5, allowedActions);
            Assert.Contains(CardAction.ACTION7, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);
            Assert.Contains(CardAction.ACTION10, allowedActions);
            Assert.Contains(CardAction.ACTION11, allowedActions);
            Assert.Contains(CardAction.ACTION12, allowedActions);
            Assert.Contains(CardAction.ACTION13, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);

            Assert.Equal(10, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Blocked, false)]
        [InlineData(CardType.Debit, CardStatus.Blocked, false)]
        public void GetAllowedActions_BlockedStatus_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION1, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION7, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION10, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION11, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION12, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION13, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION5, allowedActions);


            Assert.Equal(4, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Expired, false)]
        [InlineData(CardType.Debit, CardStatus.Expired, false)]
        public void GetAllowedActions_ExpiredStatus_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION1, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION7, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION8, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION10, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION11, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION12, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION13, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION5, allowedActions);

            Assert.Equal(3, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Credit, CardStatus.Closed, false)]
        public void GetAllowedActions_ClosedStatus_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION5, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION1, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION7, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION8, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION10, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION11, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION12, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION13, allowedActions);

            Assert.Equal(4, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Credit, CardStatus.Blocked, true)]
        public void GetAllowedActions_BlockedStatusWithPin_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION5, allowedActions);
            Assert.Contains(CardAction.ACTION6, allowedActions);
            Assert.Contains(CardAction.ACTION7, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION1, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION10, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION11, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION12, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION13, allowedActions);

            Assert.Equal(7, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        public void GetAllowedActions_ActiveStatusWithPin_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION1, allowedActions);
            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION6, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);
            Assert.Contains(CardAction.ACTION10, allowedActions);
            Assert.Contains(CardAction.ACTION11, allowedActions);
            Assert.Contains(CardAction.ACTION12, allowedActions);
            Assert.Contains(CardAction.ACTION13, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION5, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION7, allowedActions);

            Assert.Equal(10, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, false)]
        [InlineData(CardType.Debit, CardStatus.Active, false)]
        public void GetAllowedActions_ActiveStatusWithoutPin_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION1, allowedActions);
            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION7, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);
            Assert.Contains(CardAction.ACTION10, allowedActions);
            Assert.Contains(CardAction.ACTION11, allowedActions);
            Assert.Contains(CardAction.ACTION12, allowedActions);
            Assert.Contains(CardAction.ACTION13, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION5, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);

            Assert.Equal(10, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Credit, CardStatus.Blocked, false)]
        public void GetAllowedActions_BlockedStatusWithoutPin_ReturnsCorrectActions(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var allowedActions = _actionService.GetAllowedActions(card).ToList();

            Assert.Contains(CardAction.ACTION3, allowedActions);
            Assert.Contains(CardAction.ACTION4, allowedActions);
            Assert.Contains(CardAction.ACTION8, allowedActions);
            Assert.Contains(CardAction.ACTION9, allowedActions);
            Assert.Contains(CardAction.ACTION5, allowedActions);

            Assert.DoesNotContain(CardAction.ACTION1, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION2, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION6, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION7, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION10, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION11, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION12, allowedActions);
            Assert.DoesNotContain(CardAction.ACTION13, allowedActions);

            Assert.Equal(5, allowedActions.Count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        public void GetAllowedActionsCount_ActiveStatusWithPin_ReturnsCorrectCount(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var count = _actionService.GetAllowedActionsCount(card);
            Assert.Equal(10, count);
        }

        [Theory]
        [InlineData(CardType.Credit, CardStatus.Blocked, false)]
        public void GetAllowedActionsCount_BlockedStatusWithoutPin_ReturnsCorrectCount(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var count = _actionService.GetAllowedActionsCount(card);
            Assert.Equal(5, count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Restricted, false)]
        [InlineData(CardType.Debit, CardStatus.Restricted, false)]
        public void GetAllowedActionsCount_RestrictedStatus_ReturnsCorrectCount(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var count = _actionService.GetAllowedActionsCount(card);
            Assert.Equal(3, count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Closed, false)]
        [InlineData(CardType.Debit, CardStatus.Closed, false)]
        public void GetAllowedActionsCount_ClosedStatus_ReturnsCorrectCount(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var count = _actionService.GetAllowedActionsCount(card);
            Assert.Equal(3, count);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action1_ActiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION1);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Inactive, true)]
        [InlineData(CardType.Debit, CardStatus.Inactive, true)]
        [InlineData(CardType.Credit, CardStatus.Inactive, true)]
        public void IsActionAllowed_Action1_InactiveStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION1);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action2_ActiveStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION2);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Inactive, true)]
        [InlineData(CardType.Debit, CardStatus.Inactive, true)]
        [InlineData(CardType.Credit, CardStatus.Inactive, true)]
        public void IsActionAllowed_Action2_InactiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION2);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action3_AnyStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION3);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action4_AnyStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION4);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        public void IsActionAllowed_Action5_PrepaidDebit_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION5);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action5_Credit_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION5);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action6_ActiveStatusWithPin_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION6);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, false)]
        [InlineData(CardType.Debit, CardStatus.Active, false)]
        [InlineData(CardType.Credit, CardStatus.Active, false)]
        public void IsActionAllowed_Action6_ActiveStatusWithoutPin_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION6);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Blocked, true)]
        [InlineData(CardType.Debit, CardStatus.Blocked, true)]
        [InlineData(CardType.Credit, CardStatus.Blocked, true)]
        public void IsActionAllowed_Action6_BlockedStatusWithPin_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION6);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Blocked, false)]
        [InlineData(CardType.Debit, CardStatus.Blocked, false)]
        [InlineData(CardType.Credit, CardStatus.Blocked, false)]
        public void IsActionAllowed_Action6_BlockedStatusWithoutPin_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION6);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, false)]
        [InlineData(CardType.Debit, CardStatus.Active, false)]
        [InlineData(CardType.Credit, CardStatus.Active, false)]
        public void IsActionAllowed_Action7_ActiveStatusWithoutPin_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION7);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action7_ActiveStatusWithPin_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION7);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Blocked, true)]
        [InlineData(CardType.Debit, CardStatus.Blocked, true)]
        [InlineData(CardType.Credit, CardStatus.Blocked, true)]
        public void IsActionAllowed_Action7_BlockedStatusWithPin_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION7);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Blocked, false)]
        [InlineData(CardType.Debit, CardStatus.Blocked, false)]
        [InlineData(CardType.Credit, CardStatus.Blocked, false)]
        public void IsActionAllowed_Action7_BlockedStatusWithoutPin_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION7);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action8_ActiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION8);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Restricted, false)]
        [InlineData(CardType.Debit, CardStatus.Restricted, false)]
        [InlineData(CardType.Credit, CardStatus.Restricted, false)]
        public void IsActionAllowed_Action8_RestrictedStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION8);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action9_AnyStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION9);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action10_ActiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION10);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Restricted, false)]
        [InlineData(CardType.Debit, CardStatus.Restricted, false)]
        [InlineData(CardType.Credit, CardStatus.Restricted, false)]
        public void IsActionAllowed_Action10_RestrictedStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION10);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action11_ActiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION11);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Ordered, false)]
        [InlineData(CardType.Debit, CardStatus.Ordered, false)]
        [InlineData(CardType.Credit, CardStatus.Ordered, false)]
        public void IsActionAllowed_Action11_OrderedStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION11);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action12_ActiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION12);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Restricted, false)]
        [InlineData(CardType.Debit, CardStatus.Restricted, false)]
        [InlineData(CardType.Credit, CardStatus.Restricted, false)]
        public void IsActionAllowed_Action12_RestrictedStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION12);
            Assert.False(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Active, true)]
        [InlineData(CardType.Debit, CardStatus.Active, true)]
        [InlineData(CardType.Credit, CardStatus.Active, true)]
        public void IsActionAllowed_Action13_ActiveStatus_ReturnsTrue(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION13);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Ordered, true)]
        [InlineData(CardType.Debit, CardStatus.Ordered, true)]
        [InlineData(CardType.Credit, CardStatus.Ordered, true)]
        public void IsActionAllowed_Action13_OrderedStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION13);
            Assert.True(isAllowed);
        }

        [Theory]
        [InlineData(CardType.Prepaid, CardStatus.Closed, false)]
        [InlineData(CardType.Debit, CardStatus.Closed, false)]
        [InlineData(CardType.Credit, CardStatus.Closed, false)]
        public void IsActionAllowed_Action13_ClosedStatus_ReturnsFalse(CardType cardType, CardStatus cardStatus, bool isPinSet)
        {
            var card = new Card("123", cardType, cardStatus, isPinSet);
            var isAllowed = _actionService.IsActionAllowed(card, CardAction.ACTION13);
            Assert.False(isAllowed);
        }
    }
}