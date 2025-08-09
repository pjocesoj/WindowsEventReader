using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsEventReader
{
    public static class QueryCreator
    {
        #region Startdate
        /// <summary>
        /// create query string for filter events from date
        /// </summary>
        /// <param name="start">start date</param>
        /// <param name="star">* on beginning (whole query) or not (on end of another)</param>
        public static string StartDateQuery(DateTime start, bool star = true)
        {
            if (star)
            {
                return $"*[System[TimeCreated[@SystemTime >= '{start:yyyy-MM-ddTHH:mm:ss.fffZ}']]]";
            }
            return $"[System[TimeCreated[@SystemTime >= '{start:yyyy-MM-ddTHH:mm:ss.fffZ}']]]";
        }
        /// <summary>
        /// create expression for filter events from date which can be part of complex query string 
        /// </summary>
        /// <param name="start">start date</param>
        public static string StartDateExpression(DateTime start)
        {
            return $"TimeCreated[@SystemTime >= '{start:yyyy-MM-ddTHH:mm:ss.fffZ}']";
        }
        #endregion

        #region EventID
        /// <summary>
        /// create query string for filter events by EventID
        /// </summary>
        /// <param name="eventId">event ID</param>
        /// <param name="star">* on beginning (whole query) or not (on end of another)</param>
        /// <exception cref="ArgumentException">Thrown when eventId is null or empty.</exception>
        public static string EventIdQuery(string eventId, bool star = true)
        {
            Guard.NotNullOrEmpty(eventId, nameof(eventId));

            if (star)
            {
                return $"*[System[EventID={eventId}]]";
            }
            return $"[System[EventID={eventId}]]";
        }
        /// <summary>
        /// create expression for filter events by EventID which can be part of complex query string
        /// </summary>
        /// <param name="eventId">event ID</param>
        /// <exception cref="ArgumentException">Thrown when eventId is null or empty.</exception>
        public static string EventIdExpression(string eventId)
        {
            Guard.NotNullOrEmpty(eventId, nameof(eventId));

            return $"EventID={eventId}";
        }

        /// <summary>
        /// create query string for filter events by multiple EventIDs
        /// </summary>
        /// <param name="eventIds">collection of ids</param>
        /// <param name="star">* on beginning (whole query) or not (on end of another)</param>
        /// <exception cref="ArgumentException">Thrown when eventIds is null or empty.</exception>
        public static string EventIdQuery(IEnumerable<string> eventIds, bool star = true)
        {
            Guard.NotNullOrEmpty(eventIds, nameof(eventIds));

            var sb = new StringBuilder(star ? "*[System[" : "[System[");
            int i = 0;
            int count = eventIds.Count();
            foreach (var id in eventIds)
            {
                sb.Append($"EventID={id}");
                if (i < count - 1)
                {
                    sb.Append(" or ");
                }
                i++;
            }
            sb.Append("]]");
            return sb.ToString();
        }
        /// <summary>
        /// create expression for filter events by multiple EventIDs which can be part of complex query string
        /// </summary>
        /// <param name="eventIds">collection of ids</param>
        /// <exception cref="ArgumentException">Thrown when eventId is null or empty.</exception>
        public static string EventIdExpression(IEnumerable<string> eventIds)
        {
            Guard.NotNullOrEmpty(eventIds, nameof(eventIds));

            var sb = new StringBuilder();
            int i = 0;
            int count = eventIds.Count();
            foreach (var id in eventIds)
            {
                sb.Append($"EventID={id}");
                if (i < count - 1)
                {
                    sb.Append(" or ");
                }
                i++;
            }            
            return sb.ToString();
        }
        #endregion

        #region Provider and EventID
        /// <summary>
        /// create query string for filter events by EventID and Provider Name
        /// </summary>
        /// <param name="eventId">event ID</param>
        /// <param name="providerFullName">provider full name (including Microsoft-Windows and other prefixes)</param>
        /// <param name="star">* on beginning (whole query) or not (on end of another)</param>
        /// <exception cref="ArgumentException">Thrown when eventId is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when providerFullName is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when eventId is not number.</exception>
        public static string EventProviderAndIdQuery(string eventId, string providerFullName, bool star = true)
        {
            Guard.NotNullOrEmpty(eventId, nameof(eventId));
            Guard.NotNullOrEmpty(providerFullName, nameof(providerFullName));
            Guard.IsNumber(eventId, nameof(eventId));

            if (star)
            {
                return $"*[System[Provider[@Name='{providerFullName}'] and (EventID={eventId})]]";
            }
            return $"[System[Provider[@Name='{providerFullName}'] and (EventID={eventId})]]";
        }
        /// <summary>
        /// create expression for filter events by EventID and Provider Name which can be part of complex query string
        /// </summary>
        /// <param name="eventId">event ID</param>
        /// <param name="providerFullName">provider full name (including Microsoft-Windows and other prefixes)</param>
        /// <exception cref="ArgumentException">Thrown when eventId is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when providerFullName is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when eventId is not number.</exception>
        public static string EventProviderAndIdExpression(string eventId, string providerFullName)
        {
            Guard.NotNullOrEmpty(eventId, nameof(eventId));
            Guard.NotNullOrEmpty(providerFullName, nameof(providerFullName));
            Guard.IsNumber(eventId, nameof(eventId));

            return $"(Provider[@Name='{providerFullName}'] and EventID={eventId})";
        }
        #endregion
    }
}
