using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ImageChecker_2.ViewModels;
using Microsoft.Xaml.Behaviors;

namespace ImageChecker_2.Models
{
    public class DragBehavior : Behavior<Canvas>
    {
        private Point lastPoint = new Point(0, 0);
        private MainWindowViewModel vm;

        protected override void OnAttached()
        {
            base.OnAttached();
            vm = AssociatedObject.DataContext as MainWindowViewModel;
            AssociatedObject.MouseMove += AssociatedObjectOnMouseMove;
            AssociatedObject.MouseUp += AssociatedObjectOnMouseUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseMove -= AssociatedObjectOnMouseMove;
            AssociatedObject.MouseUp -= AssociatedObjectOnMouseUp;
        }

        private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (lastPoint == new Point(0, 0))
                {
                    // マウスのボタンを押した一発目の状態。この場合は現在の値を入れて、次からは一つ前の座標に基づき移動の処理をする。
                    lastPoint = e.GetPosition(AssociatedObject);
                    return;
                }

                var distance = lastPoint - e.GetPosition(AssociatedObject);
                vm.PreviewContainer.X -= (int)distance.X / vm.PreviewContainer.PreviewScreenScale;
                vm.PreviewContainer.Y -= (int)distance.Y / vm.PreviewContainer.PreviewScreenScale;

                lastPoint = e.GetPosition(AssociatedObject);
            }

            if (e.LeftButton == MouseButtonState.Released)
            {
                lastPoint = new Point(0, 0);
            }
        }

        private void AssociatedObjectOnMouseUp(object sender, MouseEventArgs e)
        {
            vm.PreviewContainer.X = Math.Round(vm.PreviewContainer.X / 10) * 10;
            vm.PreviewContainer.Y = Math.Round(vm.PreviewContainer.Y / 10) * 10;
        }
    }
}