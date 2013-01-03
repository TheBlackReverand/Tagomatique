using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tagomatique.Tools
{
	public static class VideoTools
	{
		public static void CaptureScreen(Uri source, Guid mediaViewModelId, TimeSpan timeSpan, object timeSpanId, Delegates.GenerateThumbnailDone finalWorkerPrimary)
		{
			CaptureScreen(source, mediaViewModelId, timeSpan, timeSpanId, -1, finalWorkerPrimary, null);
		}
		public static void CaptureScreen(Uri source, Guid mediaViewModelId, Dictionary<TimeSpan, object> captureList, Delegates.GenerateThumbnailDone finalWorkerPrimary)
		{
			CaptureScreen(source, mediaViewModelId, captureList, -1, finalWorkerPrimary, null);
		}
		public static void CaptureScreen(Uri source, Guid mediaViewModelId, TimeSpan timeSpan, object timeSpanId, double scale, Delegates.GenerateThumbnailDone finalWorkerPrimary, Delegates.GenerateThumbnailDone finalWorkerThumbnail)
		{
			CaptureScreen(source, mediaViewModelId, new Dictionary<TimeSpan, object> { { timeSpan, timeSpanId } }, scale, finalWorkerPrimary, finalWorkerThumbnail);
		}
		public static void CaptureScreen(string source, Guid mediaViewModelId, TimeSpan timeSpan, object timeSpanId, double scale, Delegates.GenerateThumbnailDone finalWorkerPrimary, Delegates.GenerateThumbnailDone finalWorkerThumbnail)
		{
			CaptureScreen(new Uri(source), mediaViewModelId, new Dictionary<TimeSpan, object> { { timeSpan, timeSpanId } }, scale, finalWorkerPrimary, finalWorkerThumbnail);
		}

		public static void CaptureScreen(Uri source, Guid mediaViewModelId, Dictionary<TimeSpan, object> captureList, double scale, Delegates.GenerateThumbnailDone finalWorkerPrimary, Delegates.GenerateThumbnailDone finalWorkerThumbnail)
		{
			Mutex mutexLock = new Mutex(false, source.GetHashCode().ToString());
			mutexLock.WaitOne();

			MediaPlayer player = new MediaPlayer { IsMuted = true, ScrubbingEnabled = true };
			player.Open(source);
			player.Pause();

			foreach (var pair in captureList)
			{
				TimeSpan timeSpan = pair.Key;
				object timeSpanId = pair.Value;

				player.Position = timeSpan;

				int width = 0, height = 0, cpt = 0;
				// Afin de compenser les temps d'acces on accord 10 essai avec 1s entre chaque essai
				while (width == 0 || height == 0 && cpt < 10)
				{
					Thread.Sleep(1000);
					width = player.NaturalVideoWidth;
					height = player.NaturalVideoHeight;

					cpt++;
				}

				RenderTargetBitmap rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
				DrawingVisual dv = new DrawingVisual();

				using (DrawingContext dc = dv.RenderOpen())
				{
					dc.DrawVideo(player, new Rect(0, 0, width, height));
				}

				rtb.Render(dv);
				var frame = BitmapFrame.Create(rtb).GetCurrentValueAsFrozen();

				if (finalWorkerPrimary != null)
				{
					finalWorkerPrimary(frame as BitmapFrame, mediaViewModelId, timeSpanId);
				}

				if (scale > 0 && finalWorkerThumbnail != null && frame is BitmapSource)
				{
					var thumbnailFrame = BitmapFrame.Create(new TransformedBitmap(frame as BitmapSource, new ScaleTransform(scale, scale))).GetCurrentValueAsFrozen();

					finalWorkerThumbnail(thumbnailFrame as BitmapFrame, mediaViewModelId, timeSpanId);
				}
			}

			player.Close();
			mutexLock.ReleaseMutex();
		}
	}
}