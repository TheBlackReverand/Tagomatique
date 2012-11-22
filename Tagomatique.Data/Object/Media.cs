using System;
using Tagomatique.Data.Interface;

namespace Tagomatique.Data.Object
{
	public class Media : IMedia
	{
		public Guid ID_Media { get; set; }

		public string Nom { get; set; }
		public string RelativeURL { get; set; }

		public Guid FK_ID_Dossier { get; set; }
	}
}
