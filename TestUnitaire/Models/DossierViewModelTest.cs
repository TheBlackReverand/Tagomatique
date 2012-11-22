using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tagomatique.Models;

namespace TestUnitaire.Models
{
	[TestClass]
	public class DossierViewModelTest
	{
		[TestMethod]
		public void Test1()
		{
			DossierViewModel dossier = new DossierViewModel();
			dossier.Nom = "Dossier 1";
			dossier.Chemin = @"C:\";

		}
	}
}