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
				dossier.Nom = "Divx_1";
				dossier.Chemin = @"\\SRVTBR\Divx_1";
				dossier.save();

				dossier = new DossierViewModel();
				dossier.Nom = "Divx_2";
				dossier.Chemin = @"\\SRVTBR\Divx_2";
				dossier.save();
			}

			if (MediaViewModel.GetAll().Count == 0)
			{
				MediaViewModel media = new MediaViewModel();
				media.Nom = "28 jours plus tard";
				media.RelativeURL = @"~\28 jours plus tard\28 semaines plus tard.avi";
				media.FK_ID_Dossier = DossierViewModel.GetAll().First(d => d.Chemin == @"\\SRVTBR\Divx_1").ID_Dossier;
				media.save();

				media = new MediaViewModel();
				media.Nom = "Massacre à la tronçonneuse 1";
				media.RelativeURL = @"~\Massacre à la tronçonneuse\Massacre à la tronçonneuse [2003].avi";
				media.FK_ID_Dossier = DossierViewModel.GetAll().First(d => d.Chemin == @"\\SRVTBR\Divx_2").ID_Dossier;
				media.save();

				media = new MediaViewModel();
				media.Nom = "Massacre à la tronçonneuse 2";
				media.RelativeURL = @"~\Massacre à la tronçonneuse\Massacre à la tronçonneuse 2 [1986].avi";
				media.FK_ID_Dossier = DossierViewModel.GetAll().First(d => d.Chemin == @"\\SRVTBR\Divx_2").ID_Dossier;
				media.save();
			}
		}
		private static void INITIALISER_TAG()
		{
			Guid id28 = MediaViewModel.GetAll().First(d => d.Nom == "28 jours plus tard").ID_Media;
			Guid idMassacre1 = MediaViewModel.GetAll().First(d => d.Nom == "Massacre à la tronçonneuse 1").ID_Media;
			Guid idMassacre2 = MediaViewModel.GetAll().First(d => d.Nom == "Massacre à la tronçonneuse 2").ID_Media;

			if (TagViewModel.GetAll().Count == 0)
			{
				TagViewModel tag = new TagViewModel();
				tag.Libelle = "Horreur";
				tag.FK_ID_Media = id28;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Film";
				tag.FK_ID_Media = id28;
				tag.save();


				tag = new TagViewModel();
				tag.Libelle = "Trash";
				tag.FK_ID_Media = idMassacre1;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Film";
				tag.FK_ID_Media = idMassacre1;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Massacre";
				tag.FK_ID_Media = idMassacre1;
				tag.save();


				tag = new TagViewModel();
				tag.Libelle = "Trash";
				tag.FK_ID_Media = idMassacre2;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Film";
				tag.FK_ID_Media = idMassacre2;
				tag.save();

				tag = new TagViewModel();
				tag.Libelle = "Massacre";
				tag.FK_ID_Media = idMassacre2;
				tag.save();
			}
		}
		private static void INITIALISER_SIGNET()
		{
			Guid idMassacre1 = MediaViewModel.GetAll().First(d => d.Nom == "Massacre à la tronçonneuse 1").ID_Media;

			if (SignetViewModel.GetAll().Count == 0)
			{
				SignetViewModel signet = new SignetViewModel();
				signet.Libelle = "Début film";
				signet.Duree = new TimeSpan(0, 0, 20, 55, 198);
				signet.FK_ID_Media = idMassacre1;
				signet.save();

				signet = new SignetViewModel();
				signet.Libelle = "Premier sang";
				signet.Duree = new TimeSpan(0, 1, 5, 47, 257);
				signet.FK_ID_Media = idMassacre1;
				signet.save();
			}
		}
	}
}