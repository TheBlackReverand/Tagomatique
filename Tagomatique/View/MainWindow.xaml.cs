﻿namespace Tagomatique.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
  
            DataContext = new MainWindowContext(this);
        }
    }
}