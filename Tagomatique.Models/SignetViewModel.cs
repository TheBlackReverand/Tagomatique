using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Models.Abstract;
using Tagomatique.Resources.Enums;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
	public class SignetViewModel : DataViewModelBase
	{
		#region Champs

		public Guid ID_Signet { get; set; }

		public TimeSpan Duree { get; set; }
		public string Libelle { get; set; }

		public Guid FK_ID_Media { get; set; }

		#endregion Champs

		#region DataBase

		public static List<SignetViewModel> GetAll()
		{
			return TagomatiqueCache.GetAll(GetAllFromDB);
		}
		private static List<SignetViewModel> GetAllFromDB()
		{
			return AbstractDatabase.DataBase.GetAllSignet().Select(item => new SignetViewModel
															  {
																  ID_Signet = item.ID_Signet,
																  Duree = TimeSpan.Parse(item.Duree),
																  Libelle = item.Libelle,
																  FK_ID_Media = item.FK_ID_Media,
																  State = DataModelState.Unchanged
															  }).ToList();
		}

		public static SignetViewModel GetByKey(Guid idSignet)
		{
			return TagomatiqueCache.GetElement(s => s.ID_Signet == idSignet, GetAllFromDB);
		}
		public static List<SignetViewModel> GetByMediaKey(Guid idMedia)
		{
			return TagomatiqueCache.GetAll(GetAllFromDB).Where(s => s.FK_ID_Media == idMedia).ToList();
		}

		#endregion

		protected override void insert()
		{
			ID_Signet = AbstractDatabase.DataBase.AjouterSignet(FK_ID_Media, Libelle, Duree.ToString("hh:mm:ss.ms"));

			TagomatiqueCache.MarkAsDirty<SignetViewModel>();
		}
		protected override void update()
		{
			throw new NotImplementedException("Modification des Signet interdit");
		}
		protected override void delete()
		{
			AbstractDatabase.DataBase.SupprimerSignet(ID_Signet);

			TagomatiqueCache.MarkAsDirty<SignetViewModel>();
		}
	}
}