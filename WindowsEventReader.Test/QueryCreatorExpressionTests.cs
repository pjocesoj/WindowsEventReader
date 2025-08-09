namespace WindowsEventReader.Test
{
    [TestClass]
    public class QueryCreatorExpressionTests
    {
        #region Startdate
        [TestMethod]
        public void StartDateExpression_ShouldReturnCorrectExpression()
        {
            // Arrange
            DateTime startDate = new DateTime(2023, 10, 1, 12, 0, 0);
            string expectedExpression = "TimeCreated[@SystemTime >= '2023-10-01T12:00:00.000Z']";
            // Act
            string actualExpression = QueryCreator.StartDateExpression(startDate);
            // Assert
            Assert.AreEqual(expectedExpression, actualExpression);
        }
        #endregion
        #region EventID
        [TestMethod]
        public void EventIdExpression_ShouldReturnCorrectExpression()
        {
            // Arrange
            string eventId = "1000";
            string expectedExpression = "EventID=1000";
            // Act
            string actualExpression = QueryCreator.EventIdExpression(eventId);
            // Assert
            Assert.AreEqual(expectedExpression, actualExpression);
        }
        #endregion
        #region EventID (IEnumerable)
        [TestMethod]
        public void EventIdExpression_MultipleIds_ShouldReturnCorrectExpression()
        {
            // Arrange
            IEnumerable<string> eventIds = new List<string> { "1000", "2000", "3000" };
            string expectedExpression = "EventID=1000 or EventID=2000 or EventID=3000";
            // Act
            string actualExpression = QueryCreator.EventIdExpression(eventIds);
            // Assert
            Assert.AreEqual(expectedExpression, actualExpression);
        }
        #endregion
        #region Provider and EventID
        [TestMethod]
        public void ProviderAndEventIdExpression_IdProvider_ShouldReturnCorrectExpression()
        {
            // Arrange
            string providerName = "Application";
            string eventId = "1000";
            string expectedExpression = "Provider[@Name='Application'] and EventID=1000";
            // Act
            string actualExpression = QueryCreator.EventProviderAndIdExpression(eventId, providerName);
            // Assert
            Assert.AreEqual(expectedExpression, actualExpression);
        }
        #endregion

    }
}
