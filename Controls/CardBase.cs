using System;
using System.Drawing;
using System.Windows.Forms;
using MunicipalServicesApp.UI;

namespace MunicipalServicesApp.Controls
{
    /// <summary>
    /// Shared look and behaviour for the cards on the Local Events page: a white
    /// panel with a coloured stripe on the left, a highlight on hover and keyboard
    /// focus, and a height that grows to fit its text at the current width.
    /// Cards draw their own text instead of hosting Label controls, which keeps
    /// a page of 30 cards light and makes the height easy to calculate.
    /// </summary>
    public abstract class CardBase : Control
    {
        protected const int StripeWidth = 5;
        protected const int ContentPaddingX = 14;
        protected const int ContentPaddingY = 10;
        protected const int LineGap = 4;

        // NoPrefix stops "&" in names such as "Water & Sanitation" being treated
        // as a keyboard shortcut marker and disappearing.
        protected const TextFormatFlags WrapFlags =
            TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl | TextFormatFlags.NoPrefix;

        protected const TextFormatFlags LineFlags =
            TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix;

        private bool _isHovered;

        protected CardBase()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.UserPaint
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.Selectable, true);

            BackColor = Color.White;
            Margin = new Padding(0, 0, 0, 8);
        }

        /// <summary>
        /// Colour of the stripe down the left edge of the card.
        /// </summary>
        protected abstract Color StripeColour { get; }

        protected virtual Color CardBackColour
        {
            get { return Color.White; }
        }

        /// <summary>
        /// Clickable cards show a hand cursor, highlight on hover and take keyboard focus.
        /// </summary>
        protected bool IsClickable { get; private set; }

        protected void SetClickable(bool clickable)
        {
            IsClickable = clickable;
            TabStop = clickable;
            Cursor = clickable ? Cursors.Hand : Cursors.Default;
            SetStyle(ControlStyles.Selectable, clickable);
        }

        /// <summary>
        /// Height of the text content for the given content width (excludes padding).
        /// </summary>
        protected abstract int MeasureContentHeight(int contentWidth);

        protected abstract void PaintContent(Graphics graphics, Rectangle contentBounds);

        /// <summary>
        /// Called when a clickable card is clicked or activated with Enter or Space.
        /// </summary>
        protected virtual void OnActivated()
        {
        }

        private int ContentWidth
        {
            get { return Width - StripeWidth - (2 * ContentPaddingX); }
        }

        /// <summary>
        /// Resizes the card to fit its content at the current width.
        /// </summary>
        public void UpdateHeight()
        {
            // Width is 0 until the page sets it, so nothing is measured during construction.
            if (ContentWidth <= 0) return;

            int desiredHeight = MeasureContentHeight(ContentWidth) + (2 * ContentPaddingY);
            if (Height != desiredHeight)
            {
                Height = desiredHeight;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateHeight();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            Color background = (_isHovered && IsClickable) ? Theme.CardHover : CardBackColour;

            using (SolidBrush backBrush = new SolidBrush(background))
            {
                graphics.FillRectangle(backBrush, ClientRectangle);
            }

            using (SolidBrush stripeBrush = new SolidBrush(StripeColour))
            {
                graphics.FillRectangle(stripeBrush, 0, 0, StripeWidth, Height);
            }

            Color borderColour = (Focused && IsClickable) ? Theme.Primary : Theme.Border;
            using (Pen borderPen = new Pen(borderColour))
            {
                graphics.DrawRectangle(borderPen, 0, 0, Width - 1, Height - 1);
            }

            if (ContentWidth > 0)
            {
                Rectangle contentBounds = new Rectangle(
                    StripeWidth + ContentPaddingX,
                    ContentPaddingY,
                    ContentWidth,
                    Math.Max(0, Height - (2 * ContentPaddingY)));

                PaintContent(graphics, contentBounds);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (IsClickable)
            {
                Focus();
                OnActivated();
            }
        }

        // Lets the card receive Enter and Space instead of the form treating
        // Enter as a click on its default (Search) button.
        protected override bool IsInputKey(Keys keyData)
        {
            if (IsClickable && (keyData == Keys.Enter || keyData == Keys.Space))
            {
                return true;
            }

            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (IsClickable && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space))
            {
                e.Handled = true;
                OnActivated();
            }
        }

        // ------------------------------------------------------------------
        // Text helpers shared by the card types
        // ------------------------------------------------------------------

        protected static int MeasureWrapped(string text, Font font, int width)
        {
            if (string.IsNullOrEmpty(text) || width <= 0) return 0;

            return TextRenderer.MeasureText(text, font, new Size(width, 10000), WrapFlags).Height;
        }

        protected static int MeasureLine(Font font)
        {
            return TextRenderer.MeasureText("Ag", font, new Size(10000, 10000), LineFlags).Height;
        }

        /// <summary>
        /// Draws word-wrapped text starting at y and returns the height it used.
        /// </summary>
        protected static int DrawWrapped(Graphics graphics, string text, Font font, Color colour,
                                         int x, int y, int width)
        {
            int height = MeasureWrapped(text, font, width);
            if (height == 0) return 0;

            TextRenderer.DrawText(graphics, text, font, new Rectangle(x, y, width, height), colour, WrapFlags);
            return height;
        }

        /// <summary>
        /// Draws one line of text on the left and another on the right of the same row,
        /// and returns the row height.
        /// </summary>
        protected static int DrawSplitRow(Graphics graphics, string leftText, Color leftColour,
                                          string rightText, Color rightColour, Font font,
                                          int x, int y, int width)
        {
            int height = MeasureLine(font);

            int rightWidth = string.IsNullOrEmpty(rightText)
                ? 0
                : TextRenderer.MeasureText(rightText, font, new Size(10000, 10000), LineFlags).Width;

            if (rightWidth > 0)
            {
                Rectangle rightBounds = new Rectangle(x + width - rightWidth, y, rightWidth, height);
                TextRenderer.DrawText(graphics, rightText, font, rightBounds, rightColour, LineFlags);
            }

            int leftWidth = Math.Max(0, width - rightWidth - (rightWidth > 0 ? 8 : 0));
            if (leftWidth > 0 && !string.IsNullOrEmpty(leftText))
            {
                Rectangle leftBounds = new Rectangle(x, y, leftWidth, height);
                TextRenderer.DrawText(graphics, leftText, font, leftBounds, leftColour, LineFlags);
            }

            return height;
        }
    }
}
