using System;

namespace WindowsEventReader.ComputerPowerOnRanges
{
    public class PowerOnRange
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public PowerOnRange(DateTime startTime)
        {
            StartTime = startTime;
        }

        public TimeSpan Duration
        {
            get
            {
                if (EndTime.HasValue)
                {
                    return EndTime.Value - StartTime;
                }
                return DateTime.Now - StartTime;
            }
        }

        public override string ToString()
        {
            var endTimeStr = EndTime != null ? EndTime.Value.ToString() : "unknown";
            return $"{StartTime} - {endTimeStr}";
        }

    }
}
