using System.Windows;
using System.Windows.Controls;

namespace ReminderApp
{
    /// <summary>
    /// Interaction logic for ListOfCurrentTasks.xaml
    /// </summary>
    public partial class ListOfCurrentTasks : Window
    {
        public ListOfCurrentTasks()
        {
            InitializeComponent();
            Menu ThisMenu = (Content as DockPanel).Children[0] as Menu;
            Methods.MakeMenu(ref ThisMenu);
        }

        
    }
}
