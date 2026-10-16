using System;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// An event suggested to the resident, with its score and a short,
    /// readable reason for the suggestion.
    /// </summary>
    public class Recommendation
    {
        public LocalEvent Event { get; }

        public double Score { get; }

        public string Reason { get; }

        /// <summary>
        /// True when the suggestion is based on the resident's searches or the
        /// events they opened; false when it only fills a gap with a soon event.
        /// </summary>
        public bool IsPersonalised { get; }

        public Recommendation(LocalEvent localEvent, double score, string reason, bool isPersonalised)
        {
            Event = localEvent ?? throw new ArgumentNullException(nameof(localEvent));
            Score = score;
            Reason = reason ?? string.Empty;
            IsPersonalised = isPersonalised;
        }

        public override string ToString()
        {
            return string.Format("{0:0.00}  {1}  ({2})", Score, Event.Title, Reason);
        }
    }
}
