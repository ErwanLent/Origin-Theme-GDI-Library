using Origin_Theme.Enums;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Origin_Theme.Buttons
{
    public class SecondaryButton : Button
    {
        private Color _buttonColorTop = Color.Silver;
        private Color _buttonColorBottom = Color.Gray;
        private Color _textColor = Color.White;
        private Color _borderColor = Color.White;

        private Font _font = new Font("Verdana", 8);

        private MouseState _mouseState = MouseState.None;

        public Color TopColor
        {
            get { return _buttonColorTop; }
            set { _buttonColorTop = value; Invalidate(); }
        }

        public Color BottomColor
        {
            get { return _buttonColorBottom; }
            set { _buttonColorBottom = value; Invalidate(); }
        }

        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        public override Font Font
        {
            get { return _font; }
            set { _font = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);

            // Clear Control
            paintEvent.Graphics.Clear(_buttonColorTop);

            Brush gradientBrush = new LinearGradientBrush(new Rectangle(0, 0, Width - 1, Height - 1), _buttonColorTop, _buttonColorBottom, 90.0F);
            paintEvent.Graphics.FillRectangle(gradientBrush, new Rectangle(0, 0, Width - 1, Height - 1));

            switch (_mouseState)
            {
                case MouseState.None:
                    paintEvent.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.Black)), new Rectangle(0, 0, Width - 1, Height - 1));
                    break;
                case MouseState.Over:
                    paintEvent.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), new Rectangle(0, 0, Width - 1, Height - 1));
                    break;
                case MouseState.Down:
                    paintEvent.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.DarkGray)), new Rectangle(0, 0, Width - 1, Height - 1));
                    break;
            }

            // Draw Border
            paintEvent.Graphics.DrawRectangle(new Pen(_borderColor), new Rectangle(0, 0, Width - 1, Height - 1));

            // Draw Button Text
            paintEvent.Graphics.DrawString(this.Text, _font, new SolidBrush(_textColor), CalculateTextPoint());
        }

        private PointF CalculateTextPoint()
        {
            Graphics graphics = this.CreateGraphics();

            // Measure Text And Center It
            Size textSize = graphics.MeasureString(this.Text, _font).ToSize();
            PointF measuredPoint = new PointF((Width / 2) - (textSize.Width / 2), (Height / 2) - (textSize.Height / 2));

            return measuredPoint;
        }

        protected override void OnMouseEnter(System.EventArgs e)
        {
            _mouseState = MouseState.Over;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            _mouseState = MouseState.Down;
            Invalidate();
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            _mouseState = MouseState.None;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            _mouseState = MouseState.Over;
            Invalidate();
            base.OnMouseClick(e);
        }
    }
}
