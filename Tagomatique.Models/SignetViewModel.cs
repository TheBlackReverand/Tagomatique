using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
    public class SignetViewModel : AbstractLibelleViewModel
    {
        #region Champs

        public Guid ID_Media;

        public TimeSpan Duree { get; set; }

        #endregion Champs

        #region Constructeur

        protected SignetViewModel() { }

        #endregion Constructeur

        #region DataBase

        public static List<SignetViewModel> GetAll()
        {
            return TagomatiqueCache.GetAll(GetAllFromDB);
        }
        private static List<SignetViewModel> GetAllFromDB()
        {
            return DataBase.GetAllSignet().Select(item => new SignetViewModel
            {
                ID_Media = item.ID_Media,
                ID_Libelle = item.ID_Libelle,
                LibelleTexte = DataBase.GetLibelleByKey(item.ID_Libelle).LibelleTexte,
                Duree = TimeSpan.Parse(item.Duree)
            }).ToList();
        }

        public static SignetViewModel GetByKey(Guid idMedia, Guid idLibelle)
        {
            return TagomatiqueCache.GetElement(t => t.ID_Media == idMedia && t.ID_Libelle == idLibelle, GetAllFromDB);
        }
        public static List<SignetViewModel> GetByMediaKey(Guid idMedia)
        {
            return TagomatiqueCache.GetAll(GetAllFromDB).Where(t => t.ID_Media == idMedia).ToList();
        }

        public static void AjouterSignet(Guid idMedia, string libelleTexte, TimeSpan duree)
        {
            Libelle libelle = GetLibelleByTexte(libelleTexte);

            AjouterSignet(idMedia, libelle.ID_Libelle, duree);
        }
        public static void AjouterSignet(Guid idMedia, Guid idLibelle, TimeSpan duree)
        {
            DataBase.AjouterSignet(idMedia, idLibelle, duree);

            TagomatiqueCache.MarkAsDirty<SignetViewModel>();
        }
        public static void SupprimerSignet(Guid idMedia, Guid idLibelle)
        {
            DataBase.SupprimerSignet(idMedia, idLibelle);

            TagomatiqueCache.MarkAsDirty<SignetViewModel>();
        }

        #endregion
    }
}