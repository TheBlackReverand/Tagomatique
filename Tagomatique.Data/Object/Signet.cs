using System;
using Tagomatique.Data.Interface;

namespace Tagomatique.Data.Object
{
	public class Signet : ISignet
	{
		public Guid ID_Signet { get; set; }

		public string Duree { get; set; }
		public string Libelle { get; set; }

		public Guid FK_ID_Media { get; set; }
	}
}