using System;
using Tagomatique.Data;

namespace Tagomatique.Models
{
    public abstract class AbstractLibelleViewModel : ViewModelBase
    {
        #region Champs

        public Guid ID_Libelle;

        public string LibelleTexte { get; set; }

        #endregion Champs

        protected static Libelle GetLibelleByTexte(string libelleTexte)
        {
            Libelle libelle = DataBase.GetLibelleByText(libelleTexte);

            if (libelle == null)
            {
                DataBase.AjouterLibelle(libelleTexte);
                libelle = DataBase.GetLibelleByText(libelleTexte);
            }

            return libelle;
        }
    }
}