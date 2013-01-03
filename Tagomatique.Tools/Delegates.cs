using System;
using System.Windows.Media.Imaging;

namespace Tagomatique.Tools
{
	public static class Delegates
	{
		/// <summary>
		/// Delegate utilisé lors de la génération asynchrone des Thumbnails
		/// </summary>
		/// <param name="thumbnail">BitmapSource généré</param>
		/// <param name="mediaViewModelId">Guid du Media correspondant</param>
		/// <param name="timeSpanId">TimeSpan correspondant à l'instant du clichée (pour média vidéo uniquement)</param>
		public delegate void GenerateThumbnailDone(BitmapSource thumbnail, Guid mediaViewModelId, object timeSpanId);
	}
}