namespace Tagomatique.Data.Context
{
    internal partial class Chapitre
    {
        internal Tagomatique.Supplies.DTO.Chapitre GetGeneriqueDataObject()
        {
            return new Tagomatique.Supplies.DTO.Chapitre()
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
        internal Tagomatique.Supplies.DTO.Dossier GetGeneriqueDataObject()
        {
            return new Tagomatique.Supplies.DTO.Dossier()
            {
                ID_Dossier = this.ID_Dossier,

                Nom = this.Nom,
                Chemin = this.Chemin,
            };
        }
    }

    internal partial class Infos
    {
        internal Tagomatique.Supplies.DTO.Infos GetGeneriqueDataObject()
        {
            return new Tagomatique.Supplies.DTO.Infos()
            {
                Version = this.Version,
            };
        }
    }

    internal partial class Media
    {
        internal Tagomatique.Supplies.DTO.Media GetGeneriqueDataObject()
        {
            return new Tagomatique.Supplies.DTO.Media()
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
        internal Tagomatique.Supplies.DTO.Signet GetGeneriqueDataObject()
        {
            return new Tagomatique.Supplies.DTO.Signet()
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
        internal Tagomatique.Supplies.DTO.Tag GetGeneriqueDataObject()
        {
            return new Tagomatique.Supplies.DTO.Tag()
            {
                ID_Tag = this.ID_Tag,

                Libelle = this.Libelle,

                FK_ID_Chapitre = this.FK_ID_Chapitre,
                FK_ID_Media = this.FK_ID_Media,
            };
        }
    }
}