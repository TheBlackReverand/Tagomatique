using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Models.Abstract;
using Tagomatique.Supplies.Enums;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
	public class TagViewModel : DataViewModelBase
	{
		#region Champs

		public Guid ID_Tag { get; set; }

		public string Libelle { get; set; }

		public Guid? FK_ID_Media { get; set; }
		public Guid? FK_ID_Chapitre { get; set; }

		#endregion Champs

		#region DataBase

		public static List<TagViewModel> GetAll()
		{
			return TagomatiqueCache.GetAll(GetAllFromDB);
		}

		private static List<TagViewModel> GetAllFromDB()
		{
			return AbstractDatabase.DataBase.GetAllTag().Select(item => new TagViewModel
														   {
															   ID_Tag = item.ID_Tag,
															   Libelle = item.Libelle,
															   FK_ID_Media = item.FK_ID_Media,
															   FK_ID_Chapitre = item.FK_ID_Chapitre,
															   State = DataModelState.Unchanged
														   }).ToList();
		}

		public static TagViewModel GetByKey(Guid idTag)
		{
			return TagomatiqueCache.GetElement(t => t.ID_Tag == idTag, GetAllFromDB);
		}

		public static List<TagViewModel> GetByMediaKey(Guid idMedia)
		{
            return TagomatiqueCache.GetElements(t => t.FK_ID_Media == idMedia, GetAllFromDB);
		}
		public static List<TagViewModel> GetByChapitreKey(Guid idChapitre)
		{
            return TagomatiqueCache.GetElements(t => t.FK_ID_Chapitre == idChapitre, GetAllFromDB);
		}

		#endregion

		protected override void insert()
		{
			if (FK_ID_Media.HasValue && !FK_ID_Chapitre.HasValue)
				ID_Tag = AbstractDatabase.DataBase.AjouterTagForMedia(FK_ID_Media.Value, Libelle);
			else if (!FK_ID_Media.HasValue && FK_ID_Chapitre.HasValue)
				ID_Tag = AbstractDatabase.DataBase.AjouterTagForChapitre(FK_ID_Chapitre.Value, Libelle);
			else
				throw new Exception("Tag non assigné");

			TagomatiqueCache.MarkAsDirty<TagViewModel>();
		}

		protected override void update()
		{
			throw new NotImplementedException("Modification des Tag interdit");
		}

		protected override void delete()
		{
			AbstractDatabase.DataBase.SupprimerTag(ID_Tag);

			TagomatiqueCache.MarkAsDirty<TagViewModel>();
		}
	}
}