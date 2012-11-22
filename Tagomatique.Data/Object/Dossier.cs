using System;
using Tagomatique.Data.Interface;

namespace Tagomatique.Data.Object
{
	public class Dossier : IDossier
	{
		public Guid ID_Dossier { get; set; }

		public string Nom { get; set; }
		public string Chemin { get; set; }
	}
}