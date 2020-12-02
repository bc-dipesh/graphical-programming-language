using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphical_programming_language
{
    internal class Line : Shape
    {
        private float fromXPos;
        private float fromYPos;
        private float toXPos;
        private float toYPos;

        public Line() : base()
        {
            fromXPos = x;
            fromYPos = y;
            toXPos = 100;
            toYPos = 100;
        }

        public Line(float fromXPos, float fromYPos, float toXPos, float toYPos)
        {
            this.fromXPos = fromXPos;
            this.fromYPos = fromYPos;
            this.toXPos = toXPos;
            this.toYPos = toYPos;
        }

        public override void Set(Color color, params int[] list)
        {
            base.Set(color, list);
            toXPos = list[2];
            toYPos = list[3];
        }

        public override void Draw(Graphics graphics, Pen pen)
        {
            graphics.DrawLine(pen, fromXPos, fromYPos, toXPos, toYPos);

            graphics.Dispose();
        }
    }
}
