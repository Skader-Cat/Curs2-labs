using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Laba5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private bool isDragging = false;
        private UIElement draggedElement = null;
        private Point offset;

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is UIElement)
            {
                draggedElement = (UIElement)e.OriginalSource;
                isDragging = true;
                offset = e.GetPosition(draggedElement);
                draggedElement.CaptureMouse();
            }
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && draggedElement != null)
            {
                Point position = e.GetPosition(canvas);
                Canvas.SetLeft(draggedElement, position.X - offset.X);
                Canvas.SetTop(draggedElement, position.Y - offset.Y);
            }
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging && draggedElement != null)
            {
                draggedElement.ReleaseMouseCapture();
                draggedElement = null;
                isDragging = false;

                // TODO: Check if the element is close enough to connect with another element and draw a line between them
            }
        }
    }

}
