using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Origin_Theme.Controlboxes
{
    public class PrimaryControlBox : Control
    {
        private Color _backgroundColor = Color.FromArgb(39, 38, 38);
        private Color _borderColor = Color.FromArgb(94, 94, 94);
        private Color _textColor = Color.FromArgb(168, 168, 168);

        private bool _isMinimizedHighlighted = false;
        private bool _isMaximizedHighlighted = false;
        private bool _isCloseHighlighted = false;

        private readonly Font _font = new Font("Marlett", 8);

        public override Color BackColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs paintEvent)
        {
            base.OnPaint(paintEvent);

            _isCloseHighlighted = false;
            _isMaximizedHighlighted = false;
            _isMinimizedHighlighted = false;


            // Control defaults
            this.Height = 18;
            this.Width = 88;
            
            // Draw borders between controls
            Brush hatchBrush = new HatchBrush(HatchStyle.Percent50, _borderColor);
            paintEvent.Graphics.FillRectangle(hatchBrush, new Rectangle(4, 3, 1, 12));
            paintEvent.Graphics.FillRectangle(hatchBrush, new Rectangle(32, 3, 1, 12));
            paintEvent.Graphics.FillRectangle(hatchBrush, new Rectangle(60, 3, 1, 12));

            // Draw icon buttons
            paintEvent.Graphics.DrawString("0", _font, new SolidBrush(_textColor), new PointF(12, 2));
            paintEvent.Graphics.DrawString("2", _font, new SolidBrush(_textColor), new PointF(40, 4));
            paintEvent.Graphics.DrawString("r", _font, new SolidBrush(_textColor), new PointF(68, 4));

            // Dispose of brushes
            hatchBrush.Dispose();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.HighSpeed;

                if (e.X > 4 && e.X < 32)
                {
                    if (_isMaximizedHighlighted || _isCloseHighlighted)
                    {
                        Invalidate();
                    }

                    if (!_isMinimizedHighlighted)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), new Rectangle(8, 1, 21, 18));
                        _isMinimizedHighlighted = true;
                    }
                }
                else if (e.X > 32 && e.X < 60)
                {
                    if (_isMinimizedHighlighted || _isCloseHighlighted)
                    {
                        Invalidate();
                    }

                    if (!_isMaximizedHighlighted)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), new Rectangle(36, 1, 21, 18));
                        _isMaximizedHighlighted = true;
                    }
                }
                else if (e.X > 60)
                {
                    if (_isMaximizedHighlighted || _isMinimizedHighlighted)
                    {
                        Invalidate();
                    }

                    if (!_isCloseHighlighted)
                    {
                        graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), new Rectangle(64, 1, 21, 18));
                        _isCloseHighlighted = true;
                    }
                }

            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.X > 4 && e.X < 32)
            {
                FindForm().WindowState = FormWindowState.Minimized;
            }
            else if (e.X > 32 && e.X < 60)
            {
                if (FindForm().WindowState == FormWindowState.Maximized)
                {
                    FindForm().WindowState = FormWindowState.Normal;
                }
                else
                {
                    FindForm().WindowState = FormWindowState.Maximized;
                }
            }
            else if (e.X > 60)
            {
                FindForm().Close();
            }

            FindForm().Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Invalidate();
            base.OnMouseLeave(e);
        }
    }
}
