using System.Drawing;
using System.Windows.Forms;

namespace Origin_Theme
{
    public class MainForm : ContainerControl
    {
        private Color _backgroundColor = Color.FromArgb(245, 245, 245);
        private Color _borderColor = Color.FromArgb(39, 38, 38);

        private Brush _textBrush = new SolidBrush(Color.White);
        private Font _font = new Font("Segoe UI", 9);

        private Point _dragLocation;

        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        public Brush TextColor
        {
            get { return _textBrush; }
            set { _textBrush = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);

            // Default Form Settings
            Form form = FindForm();
            // If Statement Prevents Infinite RePaint Loop
            if (form.FormBorderStyle != FormBorderStyle.None) form.FormBorderStyle = FormBorderStyle.None;
            Width = form.Width;
            Height = form.Height;
            Location = new Point(0, 0);
            BackColor = _backgroundColor;
            form.TransparencyKey = Color.Fuchsia;

            // Clear Form And Fill Form With Border Color
            paintEvent.Graphics.Clear(_borderColor);

            // Fill Rectangle On Top Of Form
            paintEvent.Graphics.FillRectangle(new SolidBrush(_backgroundColor), new Rectangle(3, 26, Width - 6, Height - 29));

            // Add Title To Form
            paintEvent.Graphics.DrawString(form.Text, _font, _textBrush, new PointF(28, 4));

            // Add Icon To Form
            paintEvent.Graphics.DrawIcon(form.Icon, new Rectangle(10, 5, 16, 16));

            //Draw Rounded Corners
            DrawCorners(Color.Fuchsia);
          
        }

        private void DrawCorners(Color cornerColor)
        {
            Brush cornerBrush = new SolidBrush(cornerColor);
            Graphics graphics = this.CreateGraphics();

            graphics.FillRectangle(cornerBrush, 0, 0, 1, 1);
            graphics.FillRectangle(cornerBrush, Width - 1, 0, 1, 1);
            graphics.FillRectangle(cornerBrush, 0, Height - 1, 1, 1);
            graphics.FillRectangle(cornerBrush, Width - 1, Height - 1, 1, 1);

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _dragLocation = this.PointToScreen(e.Location);
                Point formLocation = FindForm().Location;
                _dragLocation.X -= formLocation.X;
                _dragLocation.Y -= formLocation.Y;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                Point newLocation = this.PointToScreen(e.Location);
                newLocation.X -= _dragLocation.X;
                newLocation.Y -= _dragLocation.Y;
                FindForm().Location = newLocation;
            }
        }
    }
}