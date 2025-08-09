using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace ReadWindowsEvent
{
    public class EventLoader
    {
        public List<EventRecord> LoadEvents(string query)
        {
            List<EventRecord> events = new List<EventRecord>();

            EventLogQuery eventLogQuery = new EventLogQuery("System", PathType.LogName, query);
            using (EventLogReader eventLogReader = new EventLogReader(eventLogQuery))
            {
                try
                {
                    EventRecord eventRecord;
                    while ((eventRecord = eventLogReader.ReadEvent()) != null)
                    {
                        events.Add(eventRecord);
                    }
                }
                catch (EventLogException ex)
                {
                    throw;
                }
            }
            return events;
        }
    }
}
