using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PizzaMaster.PowerShell.UI
{
    public class CenteredScrollViewer : Decorator
    {
        public static readonly DependencyProperty IsVerticalProperty = DependencyProperty.Register(
                                                                                                   "IsVertical",
                                                                                                   typeof(bool),
                                                                                                   typeof(
                                                                                                       CenteredScrollViewer
                                                                                                   ),
                                                                                                   new
                                                                                                       PropertyMetadata(default
                                                                                                                        (bool
                                                                                                                        )))
            ;

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
                                                                                               "Offset", typeof(double),
                                                                                               typeof(
                                                                                                   CenteredScrollViewer
                                                                                               ),
                                                                                               new
                                                                                                   FrameworkPropertyMetadata(default(double),
                                                                                                                             FrameworkPropertyMetadataOptions
                                                                                                                                 .AffectsArrange))
            ;

        public CenteredScrollViewer()
        {
            this.Loaded += (o, e) => this.SetOffset();
            this.DataContextChanged += (o, e) => this.OnDataContextChanged(e.OldValue, e.NewValue);
            this.Visibility = Visibility.Hidden;
        }

        public bool IsVertical
        {
            get => (bool) this.GetValue(IsVerticalProperty);
            set => this.SetValue(IsVerticalProperty, value);
        }

        public double Offset
        {
            get => (double) this.GetValue(OffsetProperty);
            set => this.SetValue(OffsetProperty, value);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            UIElement child = this.Child;
            child?.Arrange(this.IsVertical
                               ? new Rect(-this.Offset, 0, child.DesiredSize.Width, arrangeSize.Height)
                               : new Rect(0, -this.Offset, arrangeSize.Width, child.DesiredSize.Height));
            return arrangeSize;
        }

        private void ListBoxOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            this.SetOffset(animate: true);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            UIElement child = this.Child;
            if (child == null)
                return new Size();

            child.Measure(this.IsVertical
                              ? new Size(double.PositiveInfinity, constraint.Height)
                              : new Size(constraint.Width, double.PositiveInfinity));

            return constraint;
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            base.OnChildDesiredSizeChanged(child);

            this.SetOffset(animate: true);
        }

        private void OnDataContextChanged(object oldContext, object newContext)
        {
            if (oldContext is ArtikelViewModel oldArtikel)
            {
                oldArtikel.FilterChanged -= this.OnFilterChanged;
            }

            if (newContext is ArtikelViewModel newArtikel)
            {
                newArtikel.FilterChanged += this.OnFilterChanged;
            }
        }

        private void OnFilterChanged(object o, EventArgs e)
        {
            this.SetOffset();
        }

        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            if (visualRemoved is ListBox removed)
            {
                removed.SelectionChanged -= this.ListBoxOnSelectionChanged;
            }

            if (visualAdded is ListBox added)
            {
                added.SelectionChanged += this.ListBoxOnSelectionChanged;
            }
        }

        private void SetOffset(bool animate = false)
        {
            if (!(this.Child is ListBox listBox)) return;
            if (listBox.SelectedIndex < 0) return;

            var selection = (ListBoxItem) listBox.ItemContainerGenerator.ContainerFromIndex(listBox.SelectedIndex);
            if (selection == null) return;

            var point = selection.TranslatePoint(new Point(), listBox);
            var offset = this.IsVertical ? point.X : point.Y;
            offset -= this.IsVertical ? this.ActualWidth / 2 : this.ActualHeight / 2;
            offset += this.IsVertical ? selection.ActualWidth / 2 : selection.ActualHeight / 2;

            if (animate)
            {
                var duration = new Duration(TimeSpan.FromMilliseconds(200));
                var animation = new DoubleAnimation(offset, duration);
                animation.DecelerationRatio = 1;
                animation.AccelerationRatio = 0;
                this.BeginAnimation(OffsetProperty, animation);
            }
            else
            {
                this.BeginAnimation(OffsetProperty, null);
                this.Offset = offset;
                this.Visibility = Visibility.Visible;
                if (this.IsVertical)
                    selection.Focus();
            }
        }
    }
}