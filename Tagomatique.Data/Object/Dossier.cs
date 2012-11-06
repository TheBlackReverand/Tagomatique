using System;

namespace Tagomatique.Data.Objects
{
	public class Dossier
	{
		public Guid ID_Dossier { get; set; }

		public string Nom { get; set; }
		public string Chemin { get; set; }
	}
}