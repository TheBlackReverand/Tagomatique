﻿using System;
using Tagomatique.Data.Interface;

namespace Tagomatique.Data.Object
{
	public class Tag : ITag
	{
		public Guid ID_Tag { get; set; }

		public string Libelle { get; set; }

		public Guid? FK_ID_Media { get; set; }
		public Guid? FK_ID_Chapitre { get; set; }
	}
}