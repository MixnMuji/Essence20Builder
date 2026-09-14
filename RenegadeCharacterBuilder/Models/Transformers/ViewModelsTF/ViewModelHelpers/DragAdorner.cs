using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace RenegadeCharacterBuilder.Models.Transformers.ViewModelsTF.ViewModelHelpers
{
    public class DragAdorner : Adorner
    {
        private readonly TextBlock _textBlock;

        private double _left;
        private double _top;


        public DragAdorner(UIElement adronedElement,string text) : base(adronedElement)
        {

            _textBlock = new TextBlock
            {
                IsHitTestVisible = false,
                Text = text,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                Background = Brushes.White,
                Foreground = Brushes.Black,
                Padding = new Thickness(8),
                Opacity = 0.8
            };

            AddVisualChild(_textBlock);

        }

        public void SetPosition(double left, double top)
        {
            _left = left;
            _top = top;
            AdornerLayer.GetAdornerLayer(this)?.Update(this);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            return _textBlock;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _textBlock.Measure(constraint);
            return _textBlock.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _textBlock.Arrange(
                new Rect(
                    _left,
                    _top,
                    _textBlock.DesiredSize.Width,
                    _textBlock.DesiredSize.Height
                )
            );

            return finalSize;
        }
    }
}
