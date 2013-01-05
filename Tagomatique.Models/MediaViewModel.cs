using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Media.Imaging;
using Tagomatique.Data;
using Tagomatique.Models.Abstract;
using Tagomatique.Resources;
using Tagomatique.Resources.Enums;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
	public class MediaViewModel : DataViewModelBase, IEqualityComparer<MediaViewModel>, IDisposable
	{
		private static class InternalTools
		{
			#region Thumbnail Tools

			public static void GenerateThumbnail(MediaViewModel mediaViewModel)
			{
				switch (mediaViewModel.MediaType)
				{
					case MediaType.Photo:
						ThreadPool.QueueUserWorkItem(delegate { PictureTools.GenerateThumbnailOfPicture(mediaViewModel.AbsoluteURL, mediaViewModel.ID_Media, setImage); });
						break;

					case MediaType.Video:
						ThreadPool.QueueUserWorkItem(delegate { VideoTools.CaptureScreen(mediaViewModel.AbsoluteURL, mediaViewModel.ID_Media, new TimeSpan(0, 0, 30, 0), "Thumbnail", 0.5, null, setImage); });
						break;

					case MediaType.Musique:
						ThreadPool.QueueUserWorkItem(delegate { AudioTools.ReadMP3CoverArt(mediaViewModel.AbsoluteURL, mediaViewModel.ID_Media, setImage); });
						break;

					case MediaType.Autre:
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			private static void setImage(BitmapSource frame, Guid mediaViewModelId, object timeSpanId)
			{
				MediaViewModel media = GetByKey(mediaViewModelId);

				media._thumbnail = frame;
				media.OnPropertyChanged("Thumbnail");
			}

			#endregion
		}

		#region Champs

		public Guid ID_Media { get; private set; }

		public string Nom { get; set; }
		public string RelativeURL { get; set; }

		public Guid FK_ID_Dossier { get; set; }

		#endregion Champs

		#region Proprietes

		public DossierViewModel Dossier
		{
			get { return DossierViewModel.GetByKey(FK_ID_Dossier); }
		}

		public List<TagViewModel> Tags
		{
			get { return TagViewModel.GetByMediaKey(ID_Media); }
		}
		public List<SignetViewModel> Signets
		{
			get { return SignetViewModel.GetByMediaKey(ID_Media); }
		}
		public List<ChapitreViewModel> Chapitres
		{
			get { return ChapitreViewModel.GetByMediaKey(ID_Media); }
		}

		public string AbsoluteURL
		{
			get { return RelativeURL.Replace("~", Dossier.Chemin); }
		}

		public bool IsValid
		{
			get
			{
				string extension = Path.GetExtension(RelativeURL);

				if (!String.IsNullOrEmpty(extension))
					extension = extension.ToUpper();

				return File.Exists(AbsoluteURL) && (Parametres.ValidExtensionMusique.Contains(extension)
													|| Parametres.ValidExtensionPhoto.Contains(extension)
													|| Parametres.ValidExtensionVideo.Contains(extension));
			}
		}

		public bool IsMusique
		{
			get { return Path.HasExtension(RelativeURL) && Parametres.ValidExtensionMusique.Contains(Path.GetExtension(RelativeURL).ToUpper()); }
		}
		public bool IsPhoto
		{
			get { return Path.HasExtension(RelativeURL) && Parametres.ValidExtensionPhoto.Contains(Path.GetExtension(RelativeURL).ToUpper()); }
		}
		public bool IsVideo
		{
			get { return Path.HasExtension(RelativeURL) && Parametres.ValidExtensionVideo.Contains(Path.GetExtension(RelativeURL).ToUpper()); }
		}

		public MediaType MediaType
		{
			get
			{
				if (IsValid)
				{
					if (IsPhoto)
						return MediaType.Photo;

					if (IsMusique)
						return MediaType.Musique;

					if (IsVideo)
						return MediaType.Video;
				}

				return MediaType.Autre;
			}
		}

		private BitmapSource _thumbnail;
		/// <summary>
		/// Obtient le thumbnail de l'image source
		/// </summary>
		public BitmapSource Thumbnail
		{
			get
			{
				if (_thumbnail == null)
				{
					InternalTools.GenerateThumbnail(this);
				}

				return _thumbnail;
			}
		}

		#endregion Proprietes

		#region DataBase

		public static List<MediaViewModel> GetAll()
		{
			return TagomatiqueCache.GetAll(GetAllFromDB);
		}

		private static List<MediaViewModel> GetAllFromDB()
		{
			return AbstractDatabase.DataBase.GetAllMedia().Select(item => new MediaViewModel
															 {
																 ID_Media = item.ID_Media,
																 Nom = item.Nom,
																 RelativeURL = item.RelativeURL,
																 FK_ID_Dossier = item.FK_ID_Dossier,
																 State = DataModelState.Unchanged
															 }).ToList();
		}

		public static MediaViewModel GetByKey(Guid idMedia)
		{
			return TagomatiqueCache.GetElement(m => m.ID_Media == idMedia, GetAllFromDB);
		}
		public static MediaViewModel GetByProperties(string nom, string relativeUrl)
		{
			return TagomatiqueCache.GetElement(m => m.Nom == nom && m.RelativeURL == relativeUrl, GetAllFromDB);
		}

		public static List<MediaViewModel> GetByDossierKey(Guid idDossier)
		{
			return TagomatiqueCache.GetAll(GetAllFromDB).Where(m => m.FK_ID_Dossier == idDossier).ToList();
		}

		public static List<MediaViewModel> GetByTagLibelle(IEnumerable<string> tagsLibelle)
		{
			switch (Parametres.TagOperator)
			{
				case TagOperator.AND:
					return GetAll().Where(m => tagsLibelle.All(t => m.Tags.Select(tag => tag.Libelle).Contains(t))).ToList();

				case TagOperator.OR:
					return GetAll().Where(m => m.Tags.Any(t => tagsLibelle.Contains(t.Libelle))).ToList();

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion

		protected override void insert()
		{
			ID_Media = AbstractDatabase.DataBase.AjouterMedia(Nom, RelativeURL, FK_ID_Dossier);

			TagomatiqueCache.MarkAsDirty<MediaViewModel>();
		}
		protected override void update()
		{
			AbstractDatabase.DataBase.ModifierMedia(ID_Media, Nom, RelativeURL, FK_ID_Dossier);
		}
		protected override void delete()
		{
			AbstractDatabase.DataBase.SupprimerMedia(ID_Media);

			TagomatiqueCache.MarkAsDirty<MediaViewModel>();
		}

		#region Implementation of IEqualityComparer<in MediaViewModel>

		public bool Equals(MediaViewModel x, MediaViewModel y)
		{
			return x.FK_ID_Dossier == y.FK_ID_Dossier && x.ID_Media == y.ID_Media;
		}

		public int GetHashCode(MediaViewModel obj)
		{
			return base.GetHashCode();
		}

		#endregion

		#region Implementation of IDisposable

		public void Dispose()
		{
			_thumbnail = null;
		}

		#endregion
	}
}