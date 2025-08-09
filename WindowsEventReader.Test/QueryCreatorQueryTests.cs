namespace WindowsEventReader.Test;

[TestClass]
public class QueryCreatorQueryTests
{
    #region Startdate
    [TestMethod]
    public void StartDateQuery_ValidDateTrue_ShouldReturnCorrectQuery()
    {
        // Arrange
        DateTime startDate = new DateTime(2023, 10, 1, 12, 0, 0);
        string expectedQuery = "*[System[TimeCreated[@SystemTime >= '2023-10-01T12:00:00.000Z']]]";
        // Act
        string actualQuery = QueryCreator.StartDateQuery(startDate);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    [TestMethod]
    public void StartDateQuery_ValidDateFalse_ShouldReturnCorrectQuery()
    {
        // Arrange
        DateTime startDate = new DateTime(2023, 10, 1, 12, 0, 0);
        string expectedQuery = "[System[TimeCreated[@SystemTime >= '2023-10-01T12:00:00.000Z']]]";
        // Act
        string actualQuery = QueryCreator.StartDateQuery(startDate, false);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    #endregion

    #region EventID (string)
    [TestMethod]
    public void EventIdQuery_ValidIdTrue_ShouldReturnCorrectQuery()
    {
        // Arrange
        string eventId = "1000";
        string expectedQuery = "*[System[EventID=1000]]";
        // Act
        string actualQuery = QueryCreator.EventIdQuery(eventId);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    [TestMethod]
    public void EventIdQuery_ValidIdFalse_ShouldReturnCorrectQuery()
    {
        // Arrange
        string eventId = "1000";
        string expectedQuery = "[System[EventID=1000]]";
        // Act
        string actualQuery = QueryCreator.EventIdQuery(eventId, false);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    [TestMethod]
    public void EventIdQuery_EmptyId_Exception()
    {
        // Arrange
        string eventId = "";
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => QueryCreator.EventIdQuery(eventId));
    }
    #endregion
    #region EventID (IEnumerable<string>)
    [TestMethod]
    public void EventIdQuery_MultipleIdsTrue_ShouldReturnCorrectQuery()
    {
        // Arrange
        var eventIds = new List<string> { "1000", "2000", "3000" };
        string expectedQuery = "*[System[EventID=1000 or EventID=2000 or EventID=3000]]";
        // Act
        string actualQuery = QueryCreator.EventIdQuery(eventIds);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    [TestMethod]
    public void EventIdQuery_MultipleIdsFalse_ShouldReturnCorrectQuery()
    {
        // Arrange
        var eventIds = new List<string> { "1000", "2000", "3000" };
        string expectedQuery = "[System[EventID=1000 or EventID=2000 or EventID=3000]]";
        // Act
        string actualQuery = QueryCreator.EventIdQuery(eventIds, false);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }

    [TestMethod]
    public void EventIdQuery_EmptyIds_Exception()
    {
        // Arrange
        var eventIds = new List<string>();
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => QueryCreator.EventIdQuery(eventIds));
    }
    [TestMethod]
    public void EventIdQuery_NullIds_Exception()
    {
        // Arrange
        List<string> eventIds = null;
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => QueryCreator.EventIdQuery(eventIds));
    }
    #endregion
    #region Provider and EventID
    [TestMethod]
    public void EventProviderAndIdQuery_ValidIdProviderTrue_ShouldReturnCorrectQuery()
    {
        // Arrange
        string providerName = "Application";
        string eventId = "1000";
        string expectedQuery = "*[System[Provider[@Name='Application'] and (EventID=1000)]]";
        // Act
        string actualQuery = QueryCreator.EventProviderAndIdQuery(eventId, providerName);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    [TestMethod]
    public void EventProviderAndIdQuery_ValidIdProviderFalse_ShouldReturnCorrectQuery()
    {
        // Arrange
        string providerName = "Application";
        string eventId = "1000";
        string expectedQuery = "[System[Provider[@Name='Application'] and (EventID=1000)]]";
        // Act
        string actualQuery = QueryCreator.EventProviderAndIdQuery(eventId, providerName, false);
        // Assert
        Assert.AreEqual(expectedQuery, actualQuery);
    }
    [TestMethod]
    public void EventProviderAndIdQuery_EmptyId_Exception()
    {
        // Arrange
        string providerName = "Application";
        string eventId = "";
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => QueryCreator.EventProviderAndIdQuery(eventId, providerName));
    }
    [TestMethod]
    public void EventProviderAndIdQuery_EmptyProvider_Exception()
    {
        // Arrange
        string providerName = "";
        string eventId = "1000";
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => QueryCreator.EventProviderAndIdQuery(eventId, providerName));
    }
    [TestMethod]
    public void EventProviderAndIdQuery_SwapIdProvider_ShouldFail()
    {
        // Arrange
        string providerName = "1000";
        string eventId = "Application";
        // Act + Assert
        Assert.ThrowsException<ArgumentException>(() => QueryCreator.EventProviderAndIdQuery(eventId, providerName));
    }
    #endregion
}