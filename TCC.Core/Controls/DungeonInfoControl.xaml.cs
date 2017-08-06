﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TCC.Controls
{
    /// <summary>
    /// Interaction logic for DungeonInfoControl.xaml
    /// </summary>
    public partial class DungeonInfoControl : UserControl
    {
        TimeSpan growDuration;
        DoubleAnimation scaleUp;
        DoubleAnimation moveUp;
        DoubleAnimation scaleDown;
        DoubleAnimation moveDown;

        public DungeonInfoControl()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            rootBorder.RenderTransform.BeginAnimation(TranslateTransform.XProperty, scaleUp);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            rootBorder.RenderTransform.BeginAnimation(TranslateTransform.XProperty, scaleDown);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            growDuration = TimeSpan.FromMilliseconds(150);
            scaleUp = new DoubleAnimation(2, growDuration) { EasingFunction = new QuadraticEase() };
            moveUp = new DoubleAnimation(10, growDuration) { EasingFunction = new QuadraticEase() };
            scaleDown = new DoubleAnimation(0, growDuration) { EasingFunction = new QuadraticEase() };
            moveDown = new DoubleAnimation(4, growDuration) { EasingFunction = new QuadraticEase() };

        }
        public void AnimateIn()
        {

            Dispatcher.Invoke(() =>
            {
                entriesBubble.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, new DoubleAnimation(.9, 1, TimeSpan.FromMilliseconds(1000)) { EasingFunction = new ElasticEase() });
                entriesBubble.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, new DoubleAnimation(.9, 1, TimeSpan.FromMilliseconds(1000)) { EasingFunction = new ElasticEase() });
                entriesBubble.Child.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200)));
            });

        }


    }
}