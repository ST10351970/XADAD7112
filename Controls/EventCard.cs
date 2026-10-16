using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.Models;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Controls
{
    /// <summary>
    /// One event in the results list. Shows the category and time, the title,
    /// where it takes place and a short description. Clicking it raises EventSelected.
    /// </summary>
    public class EventCard : CardBase
    {
        public LocalEvent Item { get; }

        public event EventHandler<LocalEvent>? EventSelected;

        public EventCard(LocalEvent localEvent)
        {
            Item = localEvent ?? throw new ArgumentNullException(nameof(localEvent));

            SetClickable(true);
            AccessibleRole = AccessibleRole.PushButton;
            AccessibleName = localEvent.Title;
            AccessibleDescription = string.Format("{0}, {1}, {2}",
                localEvent.Category, localEvent.TimeRangeText, localEvent.LocationText);
        }

        protected override Color StripeColour
        {
            get { return Theme.GetCategoryColour(Item.Category); }
        }

        protected override int MeasureContentHeight(int contentWidth)
        {
            int height = MeasureLine(Theme.SmallBoldFont);
            height += LineGap + MeasureWrapped(Item.Title, Theme.CardTitleFont, contentWidth);
            height += LineGap + MeasureWrapped(Item.LocationText, Theme.SmallFont, contentWidth);

            if (Item.Description.Length > 0)
            {
                height += LineGap + 2 + MeasureWrapped(Item.Description, Theme.BodyFont, contentWidth);
            }

            return height;
        }

        protected override void PaintContent(Graphics graphics, Rectangle bounds)
        {
            int x = bounds.Left;
            int y = bounds.Top;
            int width = bounds.Width;

            y += DrawSplitRow(graphics,
                Item.Category.ToUpperInvariant(), StripeColour,
                Item.TimeRangeText, Theme.TextMuted,
                Theme.SmallBoldFont, x, y, width) + LineGap;

            y += DrawWrapped(graphics, Item.Title, Theme.CardTitleFont, Theme.TextPrimary, x, y, width) + LineGap;
            y += DrawWrapped(graphics, Item.LocationText, Theme.SmallFont, Theme.TextMuted, x, y, width);

            if (Item.Description.Length > 0)
            {
                y += LineGap + 2;
                DrawWrapped(graphics, Item.Description, Theme.BodyFont, Theme.TextPrimary, x, y, width);
            }
        }

        protected override void OnActivated()
        {
            EventSelected?.Invoke(this, Item);
        }
    }
}
