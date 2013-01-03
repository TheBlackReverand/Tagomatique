using System;
using System.Windows.Media.Imaging;

namespace Tagomatique.Tools
{
	public static class PictureTools
	{
		public static void GenerateThumbnailOfPicture(string source, Guid mediaViewModelId, Delegates.GenerateThumbnailDone setter)
		{
			BitmapSource ret;
			BitmapMetadata meta = null;

			//tentative de creation du thumbnail via Bitmap frame : très rapide et très peu couteux en mémoire !
			BitmapFrame frame = BitmapFrame.Create(new Uri(source), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);

			if (frame.Thumbnail == null) //echec, on tente avec BitmapImage (plus lent et couteux en mémoire)
			{
				BitmapImage image = new BitmapImage();
				image.DecodePixelHeight = 90; //on limite à 90px de hauteur
				image.BeginInit();
				image.UriSource = new Uri(source);
				image.CacheOption = BitmapCacheOption.None;
				image.CreateOptions = BitmapCreateOptions.DelayCreation; //important pour les performances
				image.EndInit();

				if (image.CanFreeze) //pour éviter les memory leak
					image.Freeze();

				ret = image;
			}
			else
			{
				//récupération des métas de l'image
				meta = frame.Metadata as BitmapMetadata;
				ret = frame.Thumbnail;
			}

			#region   TheBlackReverand : on deactive la gestion de l'orientation dans 1 premier temps

			//    public enum ExifOrientations
			//    {
			//        None = 0,
			//        Normal = 1,
			//        HorizontalFlip = 2,
			//        Rotate180 = 3,
			//        VerticalFlip = 4,
			//        Transpose = 5,
			//        Rotate270 = 6,
			//        Transverse = 7,
			//        Rotate90 = 8
			//    }

			//if ((meta != null) && (ret != null)) //si on a des meta, tentative de récupération de l'orientation
			//{

			//    if (meta.GetQuery("/app1/ifd/{ushort=274}") != null)
			//    {
			//        orientation = (ExifOrientations)Enum.Parse(typeof(ExifOrientations), meta.GetQuery("/app1/ifd/{ushort=274}").ToString());
			//    }

			//    double angle = 0;
			//    switch (orientation)
			//    {
			//        case ExifOrientations.Rotate90:
			//            angle = -90;
			//            break;
			//        case ExifOrientations.Rotate180:
			//            angle = 180;
			//            break;
			//        case ExifOrientations.Rotate270:
			//            angle = 90;
			//            break;
			//    }

			//    if (angle != 0) //on doit effectuer une rotation de l'image
			//    {
			//        ret = new TransformedBitmap(ret.Clone(), new RotateTransform(angle));
			//        ret.Freeze();
			//    }					
			//}

			#endregion

			if (ret != null)
				setter((BitmapSource)ret.GetCurrentValueAsFrozen(), mediaViewModelId, null);
		}
	}
}