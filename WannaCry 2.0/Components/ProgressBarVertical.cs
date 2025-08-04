using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EgoldsUI;

namespace yt_DesignUI
{
    [ToolboxItem(true)]
    public class ProgressBarVertical : Control
    {
        #region -- Переменные --

        Animation ProgressAnim = new Animation();

        #endregion

        #region -- Свойства --

        public Color BorderColor { get; set; } = FlatColors.GrayDark;

        public Color StartColorTop { get; set; } = FlatColors.Green;

        public Color StartColorBottom { get; set; } = FlatColors.GreenSea;

        public Color EndColorTop { get; set; } = Color.Red;

        public Color EndColorBottom { get; set; } = Color.OrangeRed;

        private int _value = 0;
        public int Value
        {
            get => _value;
            set
            {
                if (value >= ValueMinimum && value <= ValueMaximum)
                {
                    _value = value;

                    if (Animator.IsWork == false)
                    {
                        ProgressAnim.Value = _value;
                        Invalidate();
                    }
                    else
                    {
                        ProgressAction(_value);
                    }
                }
                else
                {
                    value = _value;
                }
            }
        }

        private int _valueMinimum = 0;
        public int ValueMinimum
        {
            get => _valueMinimum;
            set
            {
                if (value < ValueMaximum)
                {
                    _valueMinimum = value;

                    if (_valueMinimum > Value)
                    {
                        Value = _valueMinimum;
                        Invalidate();
                    }
                }
                else
                {
                    value = _valueMinimum;
                }
            }
        }

        private int _valueMaximum = 100;
        public int ValueMaximum
        {
            get => _valueMaximum;
            set
            {
                if (value > ValueMinimum)
                {
                    _valueMaximum = value;
                    Invalidate();
                }
                else
                {
                    value = _valueMaximum;
                }
            }
        }

        public int Step { get; set; } = 10;

        
        public TimeSpan TotalTime { get; set; } = TimeSpan.FromMinutes(3); // Default to 3 minutes
        
        public TimeSpan RemainingTime { get; set; }

        #endregion

        public ProgressBarVertical()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(30, 200);

            BackColor = FlatColors.GrayLight;

            
            Value = ValueMaximum;
            RemainingTime = TotalTime;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rectBase = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectProgress = new Rectangle(
                rectBase.X,
                CalculateProgressRectSize(rectBase),
                rectBase.Width,
                CalculateProgressRectHeight(rectBase) + 1);

            int Rad = 1;
            GraphicsPath gpathBase = Drawer.RoundedRectangle(rectBase, Rad);
            GraphicsPath gpathProgress = Drawer.RoundedRectangle(rectProgress, Rad);

            
            DrawBase(graph, gpathBase);

            
            DrawProgress(graph, rectProgress, gpathProgress);

            
            DrawBorder(graph, gpathBase);
        }

        
        private int CalculateProgressRectSize(Rectangle rect)
        {
            double progress = (RemainingTime.TotalSeconds > 0) ? (RemainingTime.TotalSeconds / TotalTime.TotalSeconds) : 0;
            return (int)(rect.Height * (1 - progress));  // Inverted to start from top
        }

        private int CalculateProgressRectHeight(Rectangle rect)
        {
            double progress = (RemainingTime.TotalSeconds > 0) ? (RemainingTime.TotalSeconds / TotalTime.TotalSeconds) : 0;
            return (int)(rect.Height * progress);
        }


        #region -- Рисование объектов --

        private void DrawBase(Graphics graph, GraphicsPath gpath)
        {
            graph.FillPath(new SolidBrush(BackColor), gpath);
        }

        private void DrawBorder(Graphics graph, GraphicsPath gpath)
        {
            graph.DrawPath(new Pen(BorderColor), gpath);
        }

        private void DrawProgress(Graphics graph, Rectangle rect, GraphicsPath gpath)
        {
            if (rect.Height > 0)
            {
                
                double fraction = 1 - (double)RemainingTime.TotalSeconds / TotalTime.TotalSeconds;
                fraction = Math.Max(0, Math.Min(1, fraction));

                Color currentColorTop = InterpolateColors(StartColorTop, EndColorTop, (float)fraction);
                Color currentColorBottom = InterpolateColors(StartColorBottom, EndColorBottom, (float)fraction);

                
                LinearGradientBrush LGB = new LinearGradientBrush(rect, currentColorTop, currentColorBottom, LinearGradientMode.Vertical);

                graph.DrawPath(new Pen(LGB), gpath);
                graph.FillPath(LGB, gpath);
            }
        }

        
        private Color InterpolateColors(Color color1, Color color2, float fraction)
        {
            fraction = Math.Max(0, Math.Min(1, fraction)); // Ensure fraction is between 0 and 1
            float r = color1.R + (color2.R - color1.R) * fraction;
            float g = color1.G + (color2.G - color1.G) * fraction;
            float b = color1.B + (color2.B - color1.B) * fraction;

            return Color.FromArgb((int)Math.Round(r), (int)Math.Round(g), (int)Math.Round(b));
        }

        #endregion

        #region -- Запуск анимаций --

        private void ProgressAction(int PIXELS)
        {
            ProgressAnim = new Animation("ProgressBar_" + Handle, Invalidate, ProgressAnim.Value, PIXELS);

            ProgressAnim.StepDivider = 8;
            Animator.Request(ProgressAnim, true);
        }

        #endregion

        #region -- Public методы --

        public bool PerformStep()
        {
            if (Value < ValueMaximum)
            {
                if (Value + Step >= ValueMaximum)
                {
                    Value = ValueMaximum;
                    return false;
                }
                else
                {
                    Value += Step;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void ResetProgress()
        {
            Value = ValueMinimum;
        }

        #endregion
    }
}