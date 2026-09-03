using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp.UI
{
    //Central definition of the application's colour scheme and typography.
    public static class Theme
    {
        // Colour palette
        public static readonly Color Primary = Color.FromArgb(0, 87, 146);
        public static readonly Color PrimaryHover = Color.FromArgb(0, 105, 173);
        public static readonly Color Accent = Color.FromArgb(0, 133, 103);
        public static readonly Color AccentHover = Color.FromArgb(0, 156, 121);
        public static readonly Color Surface = Color.FromArgb(245, 247, 250);
        public static readonly Color Border = Color.FromArgb(214, 220, 229);
        public static readonly Color TextPrimary = Color.FromArgb(33, 37, 41);
        public static readonly Color TextMuted = Color.FromArgb(108, 117, 125);
        public static readonly Color TextOnPrimary = Color.White;
        public static readonly Color DisabledFill = Color.FromArgb(233, 236, 239);
        public static readonly Color DisabledText = Color.FromArgb(140, 148, 158);

        // Typography
        public static readonly Font TitleFont = new Font("Segoe UI Semibold", 16F);
        public static readonly Font SubtitleFont = new Font("Segoe UI", 9.75F);
        public static readonly Font BodyFont = new Font("Segoe UI", 9.75F);
        public static readonly Font ButtonFont = new Font("Segoe UI Semibold", 10.5F);
        public static readonly Font SectionFont = new Font("Segoe UI Semibold", 11.25F);
        public static readonly Font SmallFont = new Font("Segoe UI", 8.75F);

        public static void StyleActionButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = PrimaryHover;
            button.BackColor = Primary;
            button.ForeColor = TextOnPrimary;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleCenter;
        }

        public static void StyleSecondaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Border;
            button.FlatAppearance.MouseOverBackColor = DisabledFill;
            button.BackColor = Color.White;
            button.ForeColor = TextPrimary;
            button.Font = ButtonFont;
            button.Cursor = Cursors.Hand;
        }

        public static void StyleDisabledButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Border;
            button.BackColor = DisabledFill;
            button.ForeColor = DisabledText;
            button.Font = ButtonFont;
            button.Enabled = false;
            button.Cursor = Cursors.Default;
        }
    }
}