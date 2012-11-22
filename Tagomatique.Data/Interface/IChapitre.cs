using System;

namespace Tagomatique.Data.Interface
{
	public interface IChapitre
	{
		Guid ID_Chapitre { get; set; }
		string Debut { get; set; }
		string Fin { get; set; }
		string Description { get; set; }
		Guid FK_ID_Media { get; set; }
	}
}