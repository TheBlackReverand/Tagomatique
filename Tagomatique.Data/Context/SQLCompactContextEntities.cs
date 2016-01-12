namespace Tagomatique.Data.Context
{
    internal partial class Chapitre
    {
        internal Tagomatique.Data.Objects.Chapitre GetGeneriqueDataObject()
        {
            return new Objects.Chapitre()
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
        internal Tagomatique.Data.Objects.Dossier GetGeneriqueDataObject()
        {
            return new Objects.Dossier()
            {
                ID_Dossier = this.ID_Dossier,

                Nom = this.Nom,
                Chemin = this.Chemin,
            };
        }
    }

    internal partial class Infos
    {
        internal Tagomatique.Data.Objects.Infos GetGeneriqueDataObject()
        {
            return new Objects.Infos()
            {
                Version = this.Version,
            };
        }
    }

    internal partial class Media
    {
        internal Tagomatique.Data.Objects.Media GetGeneriqueDataObject()
        {
            return new Objects.Media()
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
        internal Tagomatique.Data.Objects.Signet GetGeneriqueDataObject()
        {
            return new Objects.Signet()
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
        internal Tagomatique.Data.Objects.Tag GetGeneriqueDataObject()
        {
            return new Objects.Tag()
            {
                ID_Tag = this.ID_Tag,

                Libelle = this.Libelle,

                FK_ID_Chapitre = this.FK_ID_Chapitre,
                FK_ID_Media = this.FK_ID_Media,
            };
        }
    }
}