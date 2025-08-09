using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsEventReader.ComputerPowerOnRanges
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dateTimePicker1.Value = DateTime.Today.AddDays(-7);
            panel1.AutoScroll = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var startTime = dateTimePicker1.Value;
            string powerOnId = "1";
            string sleepId = "42";

            string queryTimeExp = QueryCreator.StartDateExpression(startTime);
            string powerOnQuery = QueryCreator.EventProviderAndIdExpression(powerOnId, "Microsoft-Windows-Power-Troubleshooter");
            string sleepQuery = QueryCreator.EventIdExpression(sleepId);
            string queryString = $"*[System[(({powerOnQuery}) or ({sleepQuery})) and {queryTimeExp}]]";

            var events = new EventLoader().LoadEvents(queryString);

            var powerOnRanges = new List<PowerOnRange>();
            PowerOnRange currentRange = null;
            foreach (var eventRecord in events)
            {
                DateTime eventTime = eventRecord.TimeCreated.Value;
                string eventId = eventRecord.Id.ToString();

                if (eventId == sleepId)
                {
                    if (currentRange == null)
                    {
                        currentRange = new PowerOnRange(DateTime.MinValue);
                        powerOnRanges.Add(currentRange);
                    }
                    currentRange.EndTime = eventTime;
                }
                else if (eventId == powerOnId)
                {
                    currentRange = new PowerOnRange(eventTime);
                    powerOnRanges.Add(currentRange);
                }
            }

            var sb = new StringBuilder();
            foreach (var range in powerOnRanges)
            {
                sb.AppendLine($"{range.ToString()} ({range.Duration.ToString(@"hh\:mm")})");
            }
            outputLabel.Text = sb.ToString();
        }

    }
}
