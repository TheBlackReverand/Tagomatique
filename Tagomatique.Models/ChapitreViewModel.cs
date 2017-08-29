using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Models.Abstract;
using Tagomatique.Supplies.Enums;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
	public class ChapitreViewModel : DataViewModelBase
	{
		#region Champs

		public Guid ID_Chapitre { get; set; }

		public TimeSpan Debut { get; set; }
		public TimeSpan Fin { get; set; }
		public string Description { get; set; }

		public Guid FK_ID_Media { get; set; }

		#endregion Champs

		#region DataBase

		public static List<ChapitreViewModel> GetAll()
		{
			return TagomatiqueCache.GetAll(GetAllFromDB);
		}
		private static List<ChapitreViewModel> GetAllFromDB()
		{
			return AbstractDatabase.DataBase.GetAllChapitre().Select(item => new ChapitreViewModel
				                                                                 {
					                                                                 ID_Chapitre = item.ID_Chapitre,
					                                                                 Debut = TimeSpan.Parse(item.Debut),
					                                                                 Fin = TimeSpan.Parse(item.Fin),
					                                                                 Description = item.Description,
					                                                                 FK_ID_Media = item.FK_ID_Media,
					                                                                 State = DataModelState.Unchanged
				                                                                 }).ToList();
		}

		public static ChapitreViewModel GetByKey(Guid idChapitre)
		{
			return TagomatiqueCache.GetElement(c => c.ID_Chapitre == idChapitre, GetAllFromDB);
		}
		public static List<ChapitreViewModel> GetByMediaKey(Guid idMedia)
		{
            return TagomatiqueCache.GetElements(c => c.FK_ID_Media == idMedia, GetAllFromDB);
		}

		#endregion

		protected override void insert()
		{
			ID_Chapitre = AbstractDatabase.DataBase.AjouterChapitre(FK_ID_Media, Description, Debut.ToString("hh':'mm':'ss'.'ms"), Fin.ToString("hh':'mm':'ss'.'ms"));

			TagomatiqueCache.MarkAsDirty<SignetViewModel>();
		}
		protected override void update()
		{
			throw new NotImplementedException("Modification des Chapitre interdit");
		}
		protected override void delete()
		{
			AbstractDatabase.DataBase.SupprimerSignet(ID_Chapitre);

			TagomatiqueCache.MarkAsDirty<SignetViewModel>();
		}
	}
}
