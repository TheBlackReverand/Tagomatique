using System;
using System.IO;
using System.Linq;
using System.Threading;
using Tagomatique.Data;
using Tagomatique.Models;

namespace Tagomatique
{
    public partial class App
    {
        public App()
        {
            File.AppendAllText(@"C:\log.tago.txt", "App.Main()");

            INITIALISER_DOSSIER_MEDIA();
            INITIALISER_TAG();
            INITIALISER_SIGNET();

            Thread.Sleep(1000);
        }

        private static void INITIALISER_DOSSIER_MEDIA()
        {
            if (DossierViewModel.GetAll().Count == 0)
            {
                DossierViewModel.AjouterDossier("Dossier1", @"D:\img\evil");
                DossierViewModel.AjouterDossier("Dossier2", @"D:\img\hadopipi");
            }

            if (MediaViewModel.GetAll().Count == 0)
            {
                MediaViewModel.AjouterMedia("Evil Dead", @"~\25222.gif", DossierViewModel.GetAll().First(d => d.Chemin == @"D:\img\evil").ID_Dossier);
                MediaViewModel.AjouterMedia("Hadopi 1", @"~\hadop1.png", DossierViewModel.GetAll().First(d => d.Chemin == @"D:\img\hadopipi").ID_Dossier);
                MediaViewModel.AjouterMedia("Hadopi 2", @"~\hadop2.jpg", DossierViewModel.GetAll().First(d => d.Chemin == @"D:\img\hadopipi").ID_Dossier);
            }
        }
        private static void INITIALISER_TAG()
        {
            Guid idEvilDead = MediaViewModel.GetAll().First(d => d.Nom == "Evil Dead").ID_Media;
            Guid idH1 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 1").ID_Media;
            Guid idH2 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 2").ID_Media;

            if (TagViewModel.GetAll().Count == 0)
            {
                TagViewModel.AjouterTag(idEvilDead, "Evil Dead");
                TagViewModel.AjouterTag(idEvilDead, "Humour");
                TagViewModel.AjouterTag(idEvilDead, "Jumeaux");
                TagViewModel.AjouterTag(idEvilDead, "Film");

                TagViewModel.AjouterTag(idH1, "Humour");
                TagViewModel.AjouterTag(idH1, "Hadopi");
                TagViewModel.AjouterTag(idH1, "DTC");

                TagViewModel.AjouterTag(idH2, "Humour");
                TagViewModel.AjouterTag(idH2, "Hadopi");
                TagViewModel.AjouterTag(idH2, "Cortex");
            }
        }
        private static void INITIALISER_SIGNET()
        {
            Guid idEvilDead = MediaViewModel.GetAll().First(d => d.Nom == "Evil Dead").ID_Media;
            Guid idH1 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 1").ID_Media;
            Guid idH2 = MediaViewModel.GetAll().First(d => d.Nom == "Hadopi 2").ID_Media;

            if (SignetViewModel.GetAll().Count == 0)
            {
                SignetViewModel.AjouterSignet(idH1, "Hadopi", new TimeSpan(1, 43, 55));
                SignetViewModel.AjouterSignet(idH2, "Hadopi", new TimeSpan(1, 43, 55));
            }
        }
    }
}