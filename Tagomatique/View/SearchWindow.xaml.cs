namespace Tagomatique.View
{
    /// <summary>
    /// Logique d'interaction pour SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow
    {
        public SearchWindow()
        {
            InitializeComponent();

            DataContext = new SearchWindowContext();
        }
    }
}