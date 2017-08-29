namespace Tagomatique.Tools
{
	public static class WPFTools
	{
		public static bool IsDesignMode { get { return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()); } }
	}
}