using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsEventReader.Test;

[TestClass]
[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
public class EventLoaderTests
{
    private EventLoader _eventLoader = new EventLoader();

    private const string powerOnId = "1";
    private const string powerOnProviderName = "Microsoft-Windows-Power-Troubleshooter";

    [TestMethod]
    public void LoadEvent_Today_Any()
    {
        // Arrange
        DateTime startDate = DateTime.Today;
        string query = QueryCreator.EventProviderAndIdQuery(powerOnId, powerOnProviderName) + QueryCreator.StartDateQuery(startDate,false);

        // Act
        List<EventRecord> events = _eventLoader.LoadEvents(query);

        // Assert
        Assert.IsTrue(events.Count > 0, "No events found for today with the specified provider and ID.");
    }
    [TestMethod]
    public void LoadEvent_Today_LessThanOlderDate()
    {
        // Arrange
        DateTime startDate = DateTime.Today;
        DateTime olderDate = startDate.AddDays(-1);
        string query = QueryCreator.EventProviderAndIdQuery(powerOnId, powerOnProviderName) + QueryCreator.StartDateQuery(startDate, false);
        string queryOlder = QueryCreator.EventProviderAndIdQuery(powerOnId, powerOnProviderName) + QueryCreator.StartDateQuery(olderDate, false);
        // Act
        var events = _eventLoader.LoadEvents(query);
        var olderEvents = _eventLoader.LoadEvents(queryOlder);
        // Assert
        Assert.IsTrue(events.Count > 0, "No events found for today with the specified provider and ID.");
        Assert.IsTrue(olderEvents.Count > events.Count);
    }
}
