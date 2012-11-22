using System;

namespace Tagomatique.Data.Interface
{
	public interface IMedia
	{
		Guid ID_Media { get; set; }
		string Nom { get; set; }
		string RelativeURL { get; set; }
		Guid FK_ID_Dossier { get; set; }
	}
}