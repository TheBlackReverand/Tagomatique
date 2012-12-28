using System;
using System.Linq;
using System.Threading;
using Tagomatique.Models;

namespace Tagomatique
{
	public partial class App
	{
		public App()
		{
			INITIALISER_DOSSIER_MEDIA();
			INITIALISER_TAG();
			INITIALISER_SIGNET();

			Thread.Sleep(1000);
		}

		private static void INITIALISER_DOSSIER_MEDIA()
		{
			if (DossierViewModel.GetAll().Count == 0)
			{
				DossierViewModel dossier = new DossierViewModel();
				dossier.Nom = "Dossier1";
				dossier.Chemin = @"D:\img\evil";
				dossier.save();

				dossier = new DossierViewModel();
				dossier.Nom = "Dossier2";
				dossier.Chemin = @"D:\img\hadopipi";
				dossier.save();
			}

			if (MediaViewModel.GetAll().Count == 0)
			{
				MediaViewModel media = new MediaViewModel();
				media.Nom = "Evil Dead";
				media.RelativeURL = @"~\25222.gif";
				media.FK_ID_Dossier = DossierViewModel.GetAll().First(d => d.Chemin == @"D:\img\evil").ID_Dossier;
				media.save();

				media = new MediaViewModel();
				media.Nom = "Hadopi 1";
				media.RelativeURL = @"~\hadop1.png";
				media.FK_ID_Dossier = DossierViewModel.GetAll().First(d => d.Chemin == @"D:\img\hadopipi").ID_Dossier;
				media.save();

				media = new MediaViewModel();
				media.Nom = "Hadopi 2";
				media.RelativeURL = @"~\hadop2.jpg";
				media.FK_ID_Dossier = DossierViewModel.GetAll().First(d => d.Chemin == @"D:\img\hadopipi").ID_Dossier;
				media.save();
			}
		}
		private static void INITIALISER_TAG()
		{
			Guid idEvilDead = MediaViewModel.GetAll().First(d => d.Nom == "Evil Dead").ID_Media;
			Guid idH1 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 1").ID_Media;
			Guid idH2 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 2").ID_Media;

			if (TagViewModel.GetAll().Count == 0)
			{
				TagViewModel tag = new TagViewModel();
				tag.Libelle = "Evil Dead";
				tag.FK_ID_Media = idEvilDead;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Humour";
				tag.FK_ID_Media = idEvilDead;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Jumeaux";
				tag.FK_ID_Media = idEvilDead;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Film";
				tag.FK_ID_Media = idEvilDead;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Humour";
				tag.FK_ID_Media = idH1;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Hadopi";
				tag.FK_ID_Media = idH1;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "DTC";
				tag.FK_ID_Media = idH1;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Humour";
				tag.FK_ID_Media = idH2;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Hadopi";
				tag.FK_ID_Media = idH2;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Cortex";
				tag.FK_ID_Media = idH2;
				tag.save();
			}
		}
		private static void INITIALISER_SIGNET()
		{
			Guid idEvilDead = MediaViewModel.GetAll().First(d => d.Nom == "Evil Dead").ID_Media;
			Guid idH1 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 1").ID_Media;
			Guid idH2 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 2").ID_Media;

			if (SignetViewModel.GetAll().Count == 0)
			{
				SignetViewModel signet = new SignetViewModel();
				signet.Libelle = "Hadopi";
				signet.Duree = new TimeSpan(1, 43, 55);
				signet.FK_ID_Media = idH1;
				signet.save();

				signet = new SignetViewModel();
				signet.Libelle = "Hadopi";
				signet.Duree = new TimeSpan(1, 43, 55);
				signet.FK_ID_Media = idH2;
				signet.save();
			}
		}
	}
}