﻿using System;

namespace Tagomatique.Supplies.DTO
{
	public class Tag
	{
		public Guid ID_Tag { get; set; }

		public string Libelle { get; set; }

		public Guid? FK_ID_Media { get; set; }
		public Guid? FK_ID_Chapitre { get; set; }
	}
}