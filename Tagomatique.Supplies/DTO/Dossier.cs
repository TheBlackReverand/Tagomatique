using System;

namespace Tagomatique.Supplies.DTO
{
	public class Dossier
	{
		public Guid ID_Dossier { get; set; }

		public string Nom { get; set; }
		public string Chemin { get; set; }
	}
}