using System.Windows;
using ImageChecker_2.Models;

namespace ImageChecker_2.Views
{
    public partial class ImageContainerSelector 
    {
        public static readonly DependencyProperty ImageContainerProperty =
            DependencyProperty.Register(
                nameof(ImageContainer),                               
                typeof(ImageContainer),                         
                typeof(ImageContainerSelector),              
                new PropertyMetadata(null));    
        
        public ImageContainerSelector()
        {
            InitializeComponent();
        }
        
        public ImageContainer ImageContainer
        {
            get => (ImageContainer)GetValue(ImageContainerProperty);
            set => SetValue(ImageContainerProperty, value);
        }
    }
}