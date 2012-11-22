using System;

namespace Tagomatique.Data.Interface
{
	public interface ITag
	{
		Guid ID_Tag { get; set; }

		string Libelle { get; set; }

		Guid? FK_ID_Media { get; set; }
		Guid? FK_ID_Chapitre { get; set; }
	}
}
