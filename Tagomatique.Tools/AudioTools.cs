using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Tagomatique.Tools
{
	public static class AudioTools
	{
		public static void ReadMP3CoverArt(string source, Guid mediaViewModelId, Delegates.GenerateThumbnailDone setter)
		{
			TagLib.File file = TagLib.File.Create(source);

			if (file.Tag.Pictures.Length >= 1)
			{
				byte[] bytes = file.Tag.Pictures[0].Data.Data;

				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = new MemoryStream(bytes);
				bitmapImage.EndInit();

				setter((BitmapSource)bitmapImage.GetCurrentValueAsFrozen(), mediaViewModelId, null);
			}
		}
	}
}