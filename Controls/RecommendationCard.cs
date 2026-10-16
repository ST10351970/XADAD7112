using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Controls
{
    /// <summary>
    /// A compact card for the suggestions column: date and time, title, and a
    /// short reason explaining why the event is being suggested.
    /// </summary>
    public class RecommendationCard : CardBase
    {
        public LocalEvent Item { get; }

        public string Reason { get; }

        public event EventHandler<LocalEvent>? EventSelected;

        public RecommendationCard(LocalEvent localEvent, string reason)
        {
            Item = localEvent ?? throw new ArgumentNullException(nameof(localEvent));
            Reason = reason ?? string.Empty;

            SetClickable(true);
            AccessibleRole = AccessibleRole.PushButton;
            AccessibleName = localEvent.Title;
            AccessibleDescription = Reason;
        }

        protected override Color StripeColour
        {
            get { return Theme.GetCategoryColour(Item.Category); }
        }

        private string WhenText
        {
            get { return Item.StartDateTime.ToString("ddd dd MMM, HH:mm"); }
        }

        protected override int MeasureContentHeight(int contentWidth)
        {
            int height = MeasureLine(Theme.SmallBoldFont);
            height += LineGap + MeasureWrapped(Item.Title, Theme.CardTitleFont, contentWidth);

            if (Reason.Length > 0)
            {
                height += LineGap + MeasureWrapped(Reason, Theme.SmallFont, contentWidth);
            }

            return height;
        }

        protected override void PaintContent(Graphics graphics, Rectangle bounds)
        {
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;

            y += DrawSplitRow(graphics, WhenText, Theme.TextMuted, string.Empty, Theme.TextMuted,
                Theme.SmallBoldFont, x, y, width) + LineGap;

            y += DrawWrapped(graphics, Item.Title, Theme.CardTitleFont, Theme.TextPrimary, x, y, width);

            if (Reason.Length > 0)
            {
                y += LineGap;
                DrawWrapped(graphics, Reason, Theme.SmallFont, Theme.Accent, x, y, width);
            }
        }

        protected override void OnActivated()
        {
            EventSelected?.Invoke(this, Item);
        }
    }
}
