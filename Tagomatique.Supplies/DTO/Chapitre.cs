using System;

namespace Tagomatique.Supplies.DTO
{
	public class Chapitre
	{
		public Guid ID_Chapitre { get; set; }

		public string Debut { get; set; }
		public string Fin { get; set; }
		public string Description { get; set; }

		public Guid FK_ID_Media { get; set; }
	}
}