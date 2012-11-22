using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Models.Abstract;
using Tagomatique.Resources;
using Tagomatique.Resources.Enums;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
	public class MediaViewModel : DataViewModelBase
	{
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

		public bool IsValid
		{
			get
			{
				return File.Exists(RelativeURL) && (Parametres.ValidExtensionMusique.Contains(Path.GetExtension(RelativeURL))
				                                    || Parametres.ValidExtensionPhoto.Contains(Path.GetExtension(RelativeURL))
				                                    || Parametres.ValidExtensionVideo.Contains(Path.GetExtension(RelativeURL)));
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
	}
}