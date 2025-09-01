using Zadanie.Core.Entities;
using Zadanie.Core.Enums;
using Zadanie.UseCases.Configuration;

namespace UnitTests
{
    public class ActionRulesTests
    {
        [Fact]
        public void Rules_ContainsAllActions()
        {
            var expectedActions = Enum.GetValues<CardAction>();
            var actualActions = ActionRules.Rules.Keys;

            Assert.Equal(expectedActions.Length, actualActions.Count);
            foreach (var action in expectedActions)
            {
                Assert.Contains(action, actualActions);
            }
        }

        [Fact]
        public void Rules_AllRulesAreNotNull()
        {
            foreach (var rule in ActionRules.Rules.Values)
            {
                Assert.NotNull(rule);
            }
        }

        [Theory]
        [InlineData(CardAction.ACTION1, CardStatus.Active, true)]
        [InlineData(CardAction.ACTION1, CardStatus.Inactive, false)]
        [InlineData(CardAction.ACTION1, CardStatus.Ordered, false)]
        [InlineData(CardAction.ACTION1, CardStatus.Restricted, false)]
        [InlineData(CardAction.ACTION1, CardStatus.Blocked, false)]
        [InlineData(CardAction.ACTION1, CardStatus.Expired, false)]
        [InlineData(CardAction.ACTION1, CardStatus.Closed, false)]
        public void Action1_Rule_ReturnsCorrectResult(CardAction action, CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[action];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardAction.ACTION2, CardStatus.Active, false)]
        [InlineData(CardAction.ACTION2, CardStatus.Inactive, true)]
        [InlineData(CardAction.ACTION2, CardStatus.Ordered, false)]
        [InlineData(CardAction.ACTION2, CardStatus.Restricted, false)]
        [InlineData(CardAction.ACTION2, CardStatus.Blocked, false)]
        [InlineData(CardAction.ACTION2, CardStatus.Expired, false)]
        [InlineData(CardAction.ACTION2, CardStatus.Closed, false)]
        public void Action2_Rule_ReturnsCorrectResult(CardAction action, CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[action];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardAction.ACTION3, CardStatus.Active, true)]
        [InlineData(CardAction.ACTION3, CardStatus.Inactive, true)]
        [InlineData(CardAction.ACTION3, CardStatus.Ordered, true)]
        [InlineData(CardAction.ACTION3, CardStatus.Restricted, true)]
        [InlineData(CardAction.ACTION3, CardStatus.Blocked, true)]
        [InlineData(CardAction.ACTION3, CardStatus.Expired, true)]
        [InlineData(CardAction.ACTION3, CardStatus.Closed, true)]
        public void Action3_Rule_ReturnsCorrectResult(CardAction action, CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[action];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardAction.ACTION4, CardStatus.Active, true)]
        [InlineData(CardAction.ACTION4, CardStatus.Inactive, true)]
        [InlineData(CardAction.ACTION4, CardStatus.Ordered, true)]
        [InlineData(CardAction.ACTION4, CardStatus.Restricted, true)]
        [InlineData(CardAction.ACTION4, CardStatus.Blocked, true)]
        [InlineData(CardAction.ACTION4, CardStatus.Expired, true)]
        [InlineData(CardAction.ACTION4, CardStatus.Closed, true)]
        public void Action4_Rule_ReturnsCorrectResult(CardAction action, CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[action];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardAction.ACTION5, CardType.Prepaid, false)]
        [InlineData(CardAction.ACTION5, CardType.Debit, false)]
        [InlineData(CardAction.ACTION5, CardType.Credit, true)]
        public void Action5_Rule_ReturnsCorrectResult(CardAction action, CardType cardType, bool expected)
        {
            var card = new Card("123", cardType, CardStatus.Active, false);
            var rule = ActionRules.Rules[action];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true, true)]
        [InlineData(CardStatus.Ordered, false, false)]
        [InlineData(CardStatus.Inactive, true, true)]
        [InlineData(CardStatus.Inactive, false, false)]
        [InlineData(CardStatus.Active, true, true)]
        [InlineData(CardStatus.Active, false, false)]
        [InlineData(CardStatus.Restricted, true, false)]
        [InlineData(CardStatus.Restricted, false, false)]
        [InlineData(CardStatus.Blocked, true, true)]
        [InlineData(CardStatus.Blocked, false, false)]
        [InlineData(CardStatus.Expired, true, false)]
        [InlineData(CardStatus.Expired, false, false)]
        [InlineData(CardStatus.Closed, true, false)]
        [InlineData(CardStatus.Closed, false, false)]
        public void Action6_Rule_ReturnsCorrectResult(CardStatus status, bool isPinSet, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, isPinSet);
            var rule = ActionRules.Rules[CardAction.ACTION6];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true, false)]
        [InlineData(CardStatus.Ordered, false, true)]
        [InlineData(CardStatus.Inactive, true, false)]
        [InlineData(CardStatus.Inactive, false, true)]
        [InlineData(CardStatus.Active, true, false)]
        [InlineData(CardStatus.Active, false, true)]
        [InlineData(CardStatus.Restricted, true, false)]
        [InlineData(CardStatus.Restricted, false, false)]
        [InlineData(CardStatus.Blocked, true, true)]
        [InlineData(CardStatus.Blocked, false, false)]
        [InlineData(CardStatus.Expired, true, false)]
        [InlineData(CardStatus.Expired, false, false)]
        [InlineData(CardStatus.Closed, true, false)]
        [InlineData(CardStatus.Closed, false, false)]
        public void Action7_Rule_ReturnsCorrectResult(CardStatus status, bool isPinSet, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, isPinSet);
            var rule = ActionRules.Rules[CardAction.ACTION7];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true)]
        [InlineData(CardStatus.Inactive, true)]
        [InlineData(CardStatus.Active, true)]
        [InlineData(CardStatus.Restricted, false)]
        [InlineData(CardStatus.Blocked, true)]
        [InlineData(CardStatus.Expired, false)]
        [InlineData(CardStatus.Closed, false)]
        public void Action8_Rule_ReturnsCorrectResult(CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[CardAction.ACTION8];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true)]
        [InlineData(CardStatus.Inactive, true)]
        [InlineData(CardStatus.Active, true)]
        [InlineData(CardStatus.Restricted, true)]
        [InlineData(CardStatus.Blocked, true)]
        [InlineData(CardStatus.Expired, true)]
        [InlineData(CardStatus.Closed, true)]
        public void Action9_Rule_ReturnsCorrectResult(CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[CardAction.ACTION9];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true)]
        [InlineData(CardStatus.Inactive, true)]
        [InlineData(CardStatus.Active, true)]
        [InlineData(CardStatus.Restricted, false)]
        [InlineData(CardStatus.Blocked, false)]
        [InlineData(CardStatus.Expired, false)]
        [InlineData(CardStatus.Closed, false)]
        public void Action10_Rule_ReturnsCorrectResult(CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[CardAction.ACTION10];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, false)]
        [InlineData(CardStatus.Inactive, true)]
        [InlineData(CardStatus.Active, true)]
        [InlineData(CardStatus.Restricted, false)]
        [InlineData(CardStatus.Blocked, false)]
        [InlineData(CardStatus.Expired, false)]
        [InlineData(CardStatus.Closed, false)]
        public void Action11_Rule_ReturnsCorrectResult(CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[CardAction.ACTION11];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true)]
        [InlineData(CardStatus.Inactive, true)]
        [InlineData(CardStatus.Active, true)]
        [InlineData(CardStatus.Restricted, false)]
        [InlineData(CardStatus.Blocked, false)]
        [InlineData(CardStatus.Expired, false)]
        [InlineData(CardStatus.Closed, false)]
        public void Action12_Rule_ReturnsCorrectResult(CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[CardAction.ACTION12];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(CardStatus.Ordered, true)]
        [InlineData(CardStatus.Inactive, true)]
        [InlineData(CardStatus.Active, true)]
        [InlineData(CardStatus.Restricted, false)]
        [InlineData(CardStatus.Blocked, false)]
        [InlineData(CardStatus.Expired, false)]
        [InlineData(CardStatus.Closed, false)]
        public void Action13_Rule_ReturnsCorrectResult(CardStatus status, bool expected)
        {
            var card = new Card("123", CardType.Prepaid, status, false);
            var rule = ActionRules.Rules[CardAction.ACTION13];
            var result = rule(card);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Rules_AllActionsHaveValidRules()
        {
            foreach (var kvp in ActionRules.Rules)
            {
                var action = kvp.Key;
                var rule = kvp.Value;

                var testCard = new Card("123", CardType.Prepaid, CardStatus.Active, true);
                var result = rule(testCard);

                Assert.IsType<bool>(result);
            }
        }

        [Fact]
        public void Rules_ConsistentWithBusinessLogic()
        {
            var prepaidClosedCard = new Card("123", CardType.Prepaid, CardStatus.Closed, false);
            var creditBlockedCard = new Card("456", CardType.Credit, CardStatus.Blocked, true);

            var prepaidClosedActions = ActionRules.Rules
                .Where(rule => rule.Value(prepaidClosedCard))
                .Select(rule => rule.Key)
                .ToList();

            var creditBlockedActions = ActionRules.Rules
                .Where(rule => rule.Value(creditBlockedCard))
                .Select(rule => rule.Key)
                .ToList();

            Assert.Contains(CardAction.ACTION3, prepaidClosedActions);
            Assert.Contains(CardAction.ACTION4, prepaidClosedActions);
            Assert.Contains(CardAction.ACTION9, prepaidClosedActions);
            Assert.Contains(CardAction.ACTION3, creditBlockedActions);
            Assert.Contains(CardAction.ACTION4, creditBlockedActions);
            Assert.Contains(CardAction.ACTION6, creditBlockedActions);
            Assert.Contains(CardAction.ACTION7, creditBlockedActions);
            Assert.Contains(CardAction.ACTION8, creditBlockedActions);
            Assert.Contains(CardAction.ACTION9, creditBlockedActions);
        }
    }
}