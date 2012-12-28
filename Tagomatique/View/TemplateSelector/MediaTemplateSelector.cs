using System.Windows;
using System.Windows.Controls;
using Tagomatique.Models;

namespace Tagomatique.View.TemplateSelector
{
	public class MediaTemplateSelector : DataTemplateSelector
	{
		public DataTemplate PhotoTemplate { get; set; }
		public DataTemplate StringTemplate { get; set; }

		public DataTemplate OtherTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			MediaViewModel media = item as MediaViewModel;

			if (media != null)
			{
				if (media.IsPhoto)
					return PhotoTemplate;

				return StringTemplate;
			}

			return OtherTemplate;
		}
	}
}