using System;
using System.Windows;

namespace ReminderAppReD.Views
{
    /// <summary>
    /// Interaction logic for AlertWindow.xaml
    /// </summary>
    public partial class AlertWindow : Window
    {
	    private static int _idFormer = 0;
	    public readonly string Id;

	    public AlertWindow()
        {
            InitializeComponent();
            Id = _idFormer++.ToString();
        }
    }
}
