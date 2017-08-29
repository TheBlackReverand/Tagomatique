using System.Windows;
using System.Windows.Controls;
using Tagomatique.Models;
using Tagomatique.Supplies.Enums;

namespace Tagomatique.View.TemplateSelector
{
	public class MediaTemplateSelector : DataTemplateSelector
	{
		public DataTemplate PictureTemplate { get; set; }
		public DataTemplate StringTemplate { get; set; }

		public DataTemplate OtherTemplate { get; set; }

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			MediaViewModel media = item as MediaViewModel;

			if (media == null)
			{
				return OtherTemplate;
			}

			switch (media.MediaType)
			{
				case MediaType.Photo:
				case MediaType.Video:
				case MediaType.Musique:
					return PictureTemplate;

				default:
				case MediaType.Autre:
					return StringTemplate;
			}
		}
	}
}