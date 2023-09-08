using System.IO;
using System.Linq;
using System.Windows;
using ImageChecker_2.ViewModels;
using Microsoft.Xaml.Behaviors;

namespace ImageChecker_2.Models
{
    public class DragAndDropBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            // ファイルをドラッグしてきて、コントロール上に乗せた際の処理
            AssociatedObject.PreviewDragOver += AssociatedObject_PreviewDragOver;

            // ファイルをドロップした際の処理
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewDragOver -= AssociatedObject_PreviewDragOver;
            AssociatedObject.Drop -= AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            // ファイルパスの一覧の配列
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            var vm = ((Window)sender).DataContext as MainWindowViewModel;

            if (files is { Length: 1, } && Directory.Exists(files.First()))
            {
                // files の要素数が 1 でドロップアイテムがディレクトリならば、画像用ディレクトリがドロップされたということなので
                // ディレクトリの内容をロードして渡す。
                vm?.LoadImages(Directory.GetFiles(files.First()));
                return;
            }
            
            // 上記の条件を満たさない場合は、画像ファイルが一つ以上直接ドロップされているはずなので、そのまま渡す。
            vm?.LoadImages(files);
        }

        private void AssociatedObject_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
        }
    }
}