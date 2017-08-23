namespace Tagomatique.Data.Context
{
    internal partial class Chapitre
    {
        internal Tagomatique.Resources.DTO.Chapitre GetGeneriqueDataObject()
        {
            return new Tagomatique.Resources.DTO.Chapitre()
            {
                ID_Chapitre = this.ID_Chapitre,

                Debut = this.Debut,
                Fin = this.Fin,
                Description = this.Description,

                FK_ID_Media = this.FK_ID_Media,
            };
        }
    }

    internal partial class Dossier
    {
        internal Tagomatique.Resources.DTO.Dossier GetGeneriqueDataObject()
        {
            return new Tagomatique.Resources.DTO.Dossier()
            {
                ID_Dossier = this.ID_Dossier,

                Nom = this.Nom,
                Chemin = this.Chemin,
            };
        }
    }

    internal partial class Infos
    {
        internal Tagomatique.Resources.DTO.Infos GetGeneriqueDataObject()
        {
            return new Tagomatique.Resources.DTO.Infos()
            {
                Version = this.Version,
            };
        }
    }

    internal partial class Media
    {
        internal Tagomatique.Resources.DTO.Media GetGeneriqueDataObject()
        {
            return new Tagomatique.Resources.DTO.Media()
            {
                ID_Media = this.ID_Media,

                Nom = this.Nom,
                RelativeURL = this.RelativeURL,

                FK_ID_Dossier = this.FK_ID_Dossier,
            };
        }
    }

    internal partial class Signet
    {
        internal Tagomatique.Resources.DTO.Signet GetGeneriqueDataObject()
        {
            return new Tagomatique.Resources.DTO.Signet()
            {
                ID_Signet = this.ID_Signet,

                Libelle = this.Libelle,
                Duree = this.Duree,

                FK_ID_Media = this.FK_ID_Media,
            };
        }
    }

    internal partial class Tag
    {
        internal Tagomatique.Resources.DTO.Tag GetGeneriqueDataObject()
        {
            return new Tagomatique.Resources.DTO.Tag()
            {
                ID_Tag = this.ID_Tag,

                Libelle = this.Libelle,

                FK_ID_Chapitre = this.FK_ID_Chapitre,
                FK_ID_Media = this.FK_ID_Media,
            };
        }
    }
}