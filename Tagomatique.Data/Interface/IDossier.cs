using System;

namespace Tagomatique.Data.Interface
{
	public interface IDossier
	{
		Guid ID_Dossier { get; set; }
		string Nom { get; set; }
		string Chemin { get; set; }
	}
}