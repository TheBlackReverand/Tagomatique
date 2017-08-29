using System;

namespace Tagomatique.Supplies.DTO
{
	public class Signet
	{
		public Guid ID_Signet { get; set; }

		public string Duree { get; set; }
		public string Libelle { get; set; }

		public Guid FK_ID_Media { get; set; }
	}
}