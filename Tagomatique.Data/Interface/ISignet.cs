using System;

namespace Tagomatique.Data.Interface
{
	public interface ISignet
	{
		Guid ID_Signet { get; set; }
		string Duree { get; set; }
		string Libelle { get; set; }
		Guid FK_ID_Media { get; set; }
	}
}