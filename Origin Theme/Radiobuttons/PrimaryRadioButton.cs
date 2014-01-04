using Origin_Theme.Enums;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Origin_Theme.Checkboxes
{
    public class PrimaryRadioButton : RadioButton
    {
        private Color _textColor = Color.Black;
        private Color _bulletColor = Color.Orange;

        private Color _backgroundColor = Color.FromArgb(245, 245, 245);
        private Color _borderColor = Color.FromArgb(170, 170, 170);
        private Color _primaryGradientColor = Color.FromArgb(245, 245, 245);
        private Color _secondaryGradientColor = Color.FromArgb(225, 225, 225);

        private Font _font = new Font("Verdana", 8);

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; Invalidate(); }
        }

        public override Color BackColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; Invalidate(); }
        }

        public override Font Font
        {
            get { return _font; }
            set { _font = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);
            paintEvent.Graphics.Clear(_backgroundColor);

            // High Rendering For Radiobutton Circle
            paintEvent.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            // Draw Cirle & Border
            Brush gradientBrush = new LinearGradientBrush(new Rectangle(new Point(0, 0), new Size(14, 14)),
                _primaryGradientColor, _secondaryGradientColor, 90.0F);
            paintEvent.Graphics.FillEllipse(gradientBrush, new Rectangle(0, 0, 14, 14));
            paintEvent.Graphics.DrawEllipse(new Pen(_borderColor), new Rectangle(0, 0, 14, 14));

            // Draw Text
            paintEvent.Graphics.DrawString(this.Text, _font, new SolidBrush(_textColor), new PointF(19, 0));
            

            if (this.Checked)
            {
                paintEvent.Graphics.FillEllipse(new SolidBrush(_bulletColor), new Rectangle(4, 4, 6, 6));
            }

            // Dispose Of Brushes & Pens
            gradientBrush.Dispose();
        }
    }
}
