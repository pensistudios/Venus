using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Venus
{
    class ScrollEvent
    {
        public bool isScrolling;
        public bool scrollingXPositive;
        public bool scrollingYPositive;

        public ScrollEvent()
        {
            isScrolling = false;
            scrollingXPositive = false;
            scrollingYPositive = false;
        }

        public void scrollLeft()
        {
            isScrolling = true;
            scrollingXPositive = false;
        }
        public void scrollRight()
        {
            isScrolling = true;
            scrollingXPositive = true;
        }
        public void scrollDown()
        {
            isScrolling = true;
            scrollingYPositive = true;
        }
        public void scrollUp()
        {
            isScrolling = true;
            scrollingYPositive = false;
        }
        public void stopScroll()
        {
            isScrolling = false;
        }

        
    }
}
