using System;

namespace Tagomatique.Resources.DTO
{
	public class Media
	{
		public Guid ID_Media { get; set; }

		public string Nom { get; set; }
		public string RelativeURL { get; set; }

		public Guid FK_ID_Dossier { get; set; }
	}
}
