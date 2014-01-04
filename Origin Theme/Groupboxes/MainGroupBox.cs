using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Origin_Theme.Groupboxes
{
    public class MainGroupBox : ContainerControl
    {
        private Color _borderColor = Color.Silver;
        private Color _textColor = Color.White;
        private Color _headerColor = Color.Gray;

        private Font _textFont = new Font("Verdana", 8);

        public Color BoderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; Invalidate(); }
        }

        public Color HeaderColor
        {
            get { return _headerColor; }
            set { _headerColor = value; Invalidate(); }
        }

        public override Font Font
        {
            get { return _textFont; }
            set { _textFont = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);

            // Clear Control
            paintEvent.Graphics.Clear(BackColor);

            // Draw Borders
            paintEvent.Graphics.DrawRectangle(new Pen(_borderColor), new Rectangle(0, 0, Width - 1, Height - 1));

            // Draw Groupbox Header
            paintEvent.Graphics.FillRectangle(new SolidBrush(_headerColor), new Rectangle(0, 0, Width, 25));

            // Draw Groupbox Container
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.Percent80, Color.FromArgb(45, Color.FromArgb(39, 38, 38)), Color.Transparent);
            paintEvent.Graphics.FillRectangle(hatchBrush, new Rectangle(0, 0, Width, Height));

            // Draw Groupbox Title
            paintEvent.Graphics.DrawString(this.Text, _textFont, new SolidBrush(_textColor), new PointF(5, 5));

            // Dispose Of Brushes
            hatchBrush.Dispose();
        }
    }
}
