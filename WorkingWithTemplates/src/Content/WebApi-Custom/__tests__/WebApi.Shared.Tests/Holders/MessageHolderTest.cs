using FluentAssertions;
using System;
using System.Net;
using WebApi.Shared.Holders;
using WebApi.Shared.Tests.Fixtures;
using Xunit;

namespace WebApi.Shared.Tests.Holders
{
    public class MessageHolderTest
    {
        [Fact]
        public void ShouldHaveTestsOnListWhenAddMessage()
        {
            // Given
            var expectedErrorCode = Fixture.Get().Random.Enum<HttpStatusCode>();
            var expectedMessage = Fixture.Get().Lorem.Sentence();
            IMessageHolder messageHolder = new MessageHolder();
            messageHolder.AddMessage(expectedErrorCode, expectedMessage);

            // When
            var messages = messageHolder.All();

            // Then
            messages.Should().Contain(message =>
                message.Code == expectedErrorCode && message.Content.Equals(expectedMessage));
        }

        [Fact]
        public void ShouldStringifiedMessages()
        {
            // Given
            var expectedErrorCode = Fixture.Get().Random.Enum<HttpStatusCode>();
            var expectedMessage = Fixture.Get().Lorem.Sentence();
            IMessageHolder messageHolder = new MessageHolder();
            messageHolder.AddMessage(expectedErrorCode, expectedMessage);

            // When
            var stringifiedMessage = messageHolder.StringifyMessages();

            // Then
            stringifiedMessage.Should().BeEquivalentTo(expectedMessage);
        }

        [Fact]
        public void ShouldHaveNewLineOnStringifiedMessagesWhenHasMoreThanOneMessage()
        {
            // Given
            var expectedToken = Environment.NewLine;
            IMessageHolder messageHolder = new MessageHolder();
            messageHolder.AddMessage(
                Fixture.Get().Random.Enum<HttpStatusCode>(),
                Fixture.Get().Lorem.Sentence());
            messageHolder.AddMessage(
                Fixture.Get().Random.Enum<HttpStatusCode>(),
                Fixture.Get().Lorem.Sentence());

            // When
            var stringifiedMessage = messageHolder.StringifyMessages();

            // Then
            stringifiedMessage.Should().Contain(expectedToken);
        }

        [Fact]
        public void ShouldReturnFalseWhenThereIsNotMessages()
        {
            // Given
            IMessageHolder messageHolder = new MessageHolder();

            // When
            var hasAny = messageHolder.Any();

            // Then
            hasAny.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnTrueWhenThereIsNotMessages()
        {
            // Given
            IMessageHolder messageHolder = new MessageHolder();
            messageHolder.AddMessage(
                Fixture.Get().Random.Enum<HttpStatusCode>(),
                Fixture.Get().Lorem.Sentence());

            // When
            var hasAny = messageHolder.Any();

            // Then
            hasAny.Should().BeTrue();
        }

        [Fact]
        public void ShouldAddExceptions()
        {
            // Given
            var expectedMessage = Fixture.Get().Lorem.Sentence();
            IMessageHolder messageHolder = new MessageHolder();

            // When
            messageHolder.AddException(new Exception(expectedMessage));

            // Then
            messageHolder.Any().Should().BeTrue();
            messageHolder.All().Should().Contain(message => message.Code == HttpStatusCode.InternalServerError && message.Content.Equals(expectedMessage));
        }
    }
}