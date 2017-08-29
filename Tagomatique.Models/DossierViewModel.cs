using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Models.Abstract;
using Tagomatique.Supplies.Enums;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
	public class DossierViewModel : DataViewModelBase
	{
		#region Champs

		public Guid ID_Dossier { get; private set; }

		public string Nom { get; set; }
		public string Chemin { get; set; }

		#endregion Champs

		#region Proprietes

		public DirectoryInfo DirectoryInfo
		{
			get { return new DirectoryInfo(Chemin); }
			set { Chemin = value.FullName; }
		}

		public List<MediaViewModel> Medias
		{
			get { return MediaViewModel.GetByDossierKey(ID_Dossier); }
		}

		#endregion Proprietes

		#region DataBase

		public static List<DossierViewModel> GetAll()
		{
			return TagomatiqueCache.GetAll(GetAllFromDB);
		}
		private static List<DossierViewModel> GetAllFromDB()
		{
#if DEBUG
			if (WPFTools.IsDesignMode)
			{
				return new List<DossierViewModel>()
				{
					new DossierViewModel(){ Nom ="Dossier 1", Chemin = @"C:\Tagomatique\Dossier1" },
					new DossierViewModel(){ Nom ="Dossier 2", Chemin = @"C:\Tagomatique\Dossier2" },
					new DossierViewModel(){ Nom ="Dossier 3", Chemin = @"C:\Tagomatique\Dossier3" },
					new DossierViewModel(){ Nom ="Dossier 4", Chemin = @"C:\Tagomatique\Dossier4" },
					new DossierViewModel(){ Nom ="Dossier 5", Chemin = @"C:\Tagomatique\Dossier5" },
				};
			}
#endif

			return AbstractDatabase.DataBase.GetAllDossier().Select(item => new DossierViewModel
																				{
																					ID_Dossier = item.ID_Dossier,
																					Nom = item.Nom,
																					Chemin = item.Chemin,
																					State = DataModelState.Unchanged
																				}).ToList();
		}

		public static DossierViewModel GetByKey(Guid idDossier)
		{
			return TagomatiqueCache.GetElement(d => d.ID_Dossier == idDossier, GetAllFromDB);
		}
		public static DossierViewModel GetByProperties(string nom, string chemin)
		{
			return TagomatiqueCache.GetElement(d => d.Nom == nom && d.Chemin == chemin, GetAllFromDB);
		}

		#endregion

		public override void markedAsToRemove()
		{
			base.markedAsToRemove();

			List<MediaViewModel> lst = MediaViewModel.GetByDossierKey(ID_Dossier);
			foreach (MediaViewModel mediaViewModel in lst)
			{
				mediaViewModel.markedAsToRemove();
			}
		}

		protected override void insert()
		{
			ID_Dossier = AbstractDatabase.DataBase.AjouterDossier(Nom, Chemin);

			TagomatiqueCache.MarkAsDirty<DossierViewModel>();
		}
		protected override void update()
		{
			AbstractDatabase.DataBase.ModifierDossier(ID_Dossier, Nom, Chemin);
		}
		protected override void delete()
		{
			AbstractDatabase.DataBase.SupprimerDossier(ID_Dossier);

			TagomatiqueCache.MarkAsDirty<DossierViewModel>();
		}
	}
}