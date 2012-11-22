﻿using System;
using Tagomatique.Data.Interface;

namespace Tagomatique.Data.Object
{
	public class Chapitre : IChapitre
	{
		public Guid ID_Chapitre { get; set; }

		public string Debut { get; set; }
		public string Fin { get; set; }
		public string Description { get; set; }

		public Guid FK_ID_Media { get; set; }
	}
}