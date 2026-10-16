using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.Controls
{
    /// <summary>
    /// A label that wraps its text to its width and grows or shrinks its height to
    /// fit, so short notes inside the scrolling panels never get cut off.
    /// </summary>
    public class AutoHeightLabel : Label
    {
        private const TextFormatFlags MeasureFlags =
            TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl | TextFormatFlags.NoPrefix;

        public AutoHeightLabel()
        {
            AutoSize = false;
            UseMnemonic = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            FitHeightToText();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            FitHeightToText();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            FitHeightToText();
        }

        private void FitHeightToText()
        {
            int textWidth = Width - Padding.Horizontal;
            if (textWidth <= 0) return;

            int textHeight = string.IsNullOrEmpty(Text)
                ? 0
                : TextRenderer.MeasureText(Text, Font, new Size(textWidth, 10000), MeasureFlags).Height;

            // A few pixels of slack because Label draws with slightly different
            // spacing than TextRenderer measures.
            int desiredHeight = textHeight + Padding.Vertical + 4;

            if (Height != desiredHeight)
            {
                Height = desiredHeight;
            }
        }
    }
}
