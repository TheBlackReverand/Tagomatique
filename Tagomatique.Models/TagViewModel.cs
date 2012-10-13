using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data;
using Tagomatique.Tools;

namespace Tagomatique.Models
{
    public class TagViewModel : AbstractLibelleViewModel
    {
        #region Champs

        public Guid ID_Media;

        #endregion Champs

        #region Constructeur

        protected TagViewModel() { }

        #endregion Constructeur

        #region DataBase

        public static List<TagViewModel> GetAll()
        {
            return TagomatiqueCache.GetAll(GetAllFromDB);
        }
        private static List<TagViewModel> GetAllFromDB()
        {
            return DataBase.GetAllTag().Select(item => new TagViewModel
            {
                ID_Media = item.ID_Media,
                ID_Libelle = item.ID_Libelle,
                LibelleTexte = DataBase.GetLibelleByKey(item.ID_Libelle).LibelleTexte
            }).ToList();
        }

        public static TagViewModel GetByKey(Guid idMedia, Guid idLibelle)
        {
            return TagomatiqueCache.GetElement(t => t.ID_Media == idMedia && t.ID_Libelle == idLibelle, GetAllFromDB);
        }
        public static List<TagViewModel> GetByMediaKey(Guid idMedia)
        {
            return TagomatiqueCache.GetAll(GetAllFromDB).Where(t => t.ID_Media == idMedia).ToList();
        }

        public static void AjouterTag(Guid idMedia, string libelleTexte)
        {
            Libelle libelle = GetLibelleByTexte(libelleTexte);

            AjouterTag(idMedia, libelle.ID_Libelle);
        }
        public static void AjouterTag(Guid idMedia, Guid idLibelle)
        {
            DataBase.AjouterTag(idMedia, idLibelle);

            TagomatiqueCache.MarkAsDirty<TagViewModel>();
        }
        public static void SupprimerTag(Guid idMedia, Guid idLibelle)
        {
            DataBase.SupprimerTag(idMedia, idLibelle);

            TagomatiqueCache.MarkAsDirty<TagViewModel>();
        }

        #endregion
    }
}