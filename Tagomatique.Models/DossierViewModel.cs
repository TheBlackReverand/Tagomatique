using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
    public class DossierViewModel : ViewModelBase
    {
        #region Champs

        public Guid ID_Dossier;

        public string Nom { get; set; }
        public string Chemin { get; set; }

        #endregion Champs

        #region Constructeur

        private DossierViewModel() { }

        #endregion Constructeur

        #region Proprietes

        public DirectoryInfo DirectoryInfo
        {
            get { return new DirectoryInfo(Chemin); }
            set { Chemin = value.FullName; }
        }

        public List<MediaViewModel> Medias
        {
            get { return MediaViewModel.GetByDossierKey(ID_Dossier); }
        }

        #endregion Proprietes

        #region DataBase

        public static List<DossierViewModel> GetAll()
        {
            return TagomatiqueCache.GetAll(GetAllFromDB);
        }
        private static List<DossierViewModel> GetAllFromDB()
        {
            return DataBase.GetAllDossier().Select(item => new DossierViewModel
                                                                   {
                                                                       ID_Dossier = item.ID_Dossier,
                                                                       Nom = item.Nom,
                                                                       Chemin = item.Chemin
                                                                   }).ToList();
        }

        public static DossierViewModel GetByKey(Guid idDossier)
        {
            return TagomatiqueCache.GetElement(d => d.ID_Dossier == idDossier, GetAllFromDB, true);
        }

        public static void AjouterDossier(string nom, string chemin)
        {
            DataBase.AjouterDossier(nom, chemin);

            TagomatiqueCache.MarkAsDirty<DossierViewModel>();
        }
        public static void SupprimerDossier(Guid idDossier)
        {
            DataBase.SupprimerDossier(idDossier);

            TagomatiqueCache.MarkAsDirty<DossierViewModel>();
        }

        #endregion
    }
}