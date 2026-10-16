using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Controls
{
    /// <summary>
    /// One service notice. The stripe and label colour show the priority
    /// (red for urgent, amber for important), and urgent notices get a tinted
    /// background so they stand out at the top of the list.
    /// </summary>
    public class AnnouncementCard : CardBase
    {
        public Announcement Item { get; }

        public AnnouncementCard(Announcement announcement)
        {
            Item = announcement ?? throw new ArgumentNullException(nameof(announcement));

            // Notices show their full message already, so there is nothing to open.
            SetClickable(false);
            AccessibleRole = AccessibleRole.StaticText;
            AccessibleName = announcement.PriorityLabel + ": " + announcement.Title;
            AccessibleDescription = announcement.Message;
        }

        protected override Color StripeColour
        {
            get { return Theme.GetPriorityColour(Item.Priority); }
        }

        protected override Color CardBackColour
        {
            get { return Item.Priority == AnnouncementPriority.Critical ? Theme.CriticalTint : Color.White; }
        }

        private string AreaText
        {
            get { return Item.Area.Length > 0 ? Item.Area + "  |  " + Item.Category : Item.Category; }
        }

        protected override int MeasureContentHeight(int contentWidth)
        {
            int height = MeasureLine(Theme.SmallBoldFont);
            height += LineGap + MeasureWrapped(Item.Title, Theme.CardTitleFont, contentWidth);
            height += LineGap + MeasureWrapped(AreaText, Theme.SmallFont, contentWidth);

            if (Item.Message.Length > 0)
            {
                height += LineGap + MeasureWrapped(Item.Message, Theme.SmallFont, contentWidth);
            }

            return height;
        }

        protected override void PaintContent(Graphics graphics, Rectangle bounds)
        {
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;

            y += DrawSplitRow(graphics,
                Item.PriorityLabel.ToUpperInvariant(), StripeColour,
                Item.DateRangeText, Theme.TextMuted,
                Theme.SmallBoldFont, x, y, width) + LineGap;

            y += DrawWrapped(graphics, Item.Title, Theme.CardTitleFont, Theme.TextPrimary, x, y, width) + LineGap;
            y += DrawWrapped(graphics, AreaText, Theme.SmallFont, Theme.TextMuted, x, y, width);

            if (Item.Message.Length > 0)
            {
                y += LineGap;
                DrawWrapped(graphics, Item.Message, Theme.SmallFont, Theme.TextPrimary, x, y, width);
            }
        }
    }
}
