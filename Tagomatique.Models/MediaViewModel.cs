using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
    public class MediaViewModel : ViewModelBase
    {
        #region Champs

        public Guid ID_Media;
        public Guid ID_Dossier;

        public string Nom { get; set; }
        public string RelativeURL { get; set; }

        public bool IsValid { get; private set; }

        #endregion Champs

        #region Constructeur

        private MediaViewModel() { }

        #endregion Constructeur

        #region Proprietes

        public DossierViewModel Dossier
        {
            get { return DossierViewModel.GetByKey(ID_Dossier); }
        }

        public List<TagViewModel> Tags
        {
            get { return TagViewModel.GetByMediaKey(ID_Media); }
        }
        public List<SignetViewModel> Signets
        {
            get { return SignetViewModel.GetByMediaKey(ID_Media); }
        }

        #endregion Proprietes

        #region DataBase

        public static List<MediaViewModel> GetAll()
        {
            return TagomatiqueCache.GetAll(GetAllFromDB);
        }
        private static List<MediaViewModel> GetAllFromDB()
        {
            return DataBase.GetAllMedia().Select(item => new MediaViewModel
            {
                ID_Media = item.ID_Media,
                Nom = item.Nom,
                RelativeURL = item.RelativeURL,
                ID_Dossier = item.ID_Dossier,
                IsValid = true
            }).ToList();
        }

        public static MediaViewModel GetByKey(Guid idMedia)
        {
            return TagomatiqueCache.GetElement(m => m.ID_Media == idMedia, GetAllFromDB, true);
        }
        public static MediaViewModel GetByProperties(string nom, string relativeUrl)
        {
            return TagomatiqueCache.GetElement(m => m.Nom == nom && m.RelativeURL == relativeUrl, GetAllFromDB, true);
        }

        public static List<MediaViewModel> GetByDossierKey(Guid idDossier)
        {
            return TagomatiqueCache.GetAll(GetAllFromDB).Where(m => m.ID_Dossier == idDossier).ToList();
        }

        public static MediaViewModel AjouterMedia(string nom, string relativeURL, Guid idDossier, bool inBase = true, bool isValid = true)
        {
            if (inBase)
            {
                DataBase.AjouterMedia(nom, relativeURL, idDossier);

                TagomatiqueCache.MarkAsDirty<MediaViewModel>();

                return GetByProperties(nom, relativeURL);
            }
            else
            {
                return new MediaViewModel
                 {
                     ID_Media = Guid.Empty,
                     ID_Dossier = Guid.Empty,
                     Nom = nom,
                     RelativeURL = relativeURL,
                     IsValid = isValid
                 };
            }
        }

        public static void SupprimerMedia(Guid idMedia)
        {
            DataBase.SupprimerMedia(idMedia);

            TagomatiqueCache.MarkAsDirty<MediaViewModel>();
        }

        #endregion
    }
}