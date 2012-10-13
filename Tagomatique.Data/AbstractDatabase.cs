using System;
using System.Collections.Generic;

namespace Tagomatique.Data
{
    public abstract class AbstractDatabase
    {
        #region Infos

        public abstract string GetVersion();

        #endregion Infos


        #region Dossiers

        public abstract List<Dossier> GetAllDossier();
        public abstract Dossier GetDossierByKey(Guid idDossier);
        public abstract Dossier GetDossierByKey(Guid idDossier, Tagomatique context);

        public abstract void AjouterDossier(string nom, string chemin);
        public abstract void SupprimerDossier(Guid idDossier);

        #endregion Dosssiers

        #region Medias

        public abstract List<Media> GetAllMedia();
        public abstract Media GetMediaByKey(Guid idMedia);
        public abstract Media GetMediaByKey(Guid idMedia, Tagomatique context);

        public abstract void AjouterMedia(string nom, string relativeURL, Guid idDossier);
        public abstract void SupprimerMedia(Guid idMedia);

        #endregion Medias


        #region Libelles

        public abstract List<Libelle> GetAllLibelle();
        public abstract Libelle GetLibelleByKey(Guid idLibelle);
        public abstract Libelle GetLibelleByKey(Guid idLibelle, Tagomatique context);
        public abstract Libelle GetLibelleByText(string libelle);
        public abstract Libelle GetLibelleByText(string libelle, Tagomatique context);

        public abstract void AjouterLibelle(string libelle);
        public abstract void SupprimerLibelle(Guid idLibelle);

        #endregion Libelles

        #region Tags

        public abstract List<Tag> GetAllTag();
        public abstract Tag GetTagByKey(Guid idMedia, Guid idLibelle);
        public abstract Tag GetTagByKey(Guid idMedia, Guid idLibelle, Tagomatique context);
        public abstract List<Tag> GetTagOfMedia(Guid idMedia);
        public abstract List<Tag> GetTagOfMedia(Guid idMedia, Tagomatique context);

        public abstract void AjouterTag(Guid idMedia, Guid idLibelle);
        public abstract void SupprimerTag(Guid idMedia, Guid idLibelle);

        #endregion Tags

        #region Signets

        public abstract List<Signet> GetAllSignet();
        public abstract Signet GetSignetByKey(Guid idMedia, Guid idLibelle);
        public abstract Signet GetSignetByKey(Guid idMedia, Guid idLibelle, Tagomatique context);
        public abstract List<Signet> GetSignetOfMedia(Guid idMedia);
        public abstract List<Signet> GetSignetOfMedia(Guid idMedia, Tagomatique context);

        public abstract void AjouterSignet(Guid idMedia, Guid idLibelle, TimeSpan duree);
        public abstract void SupprimerSignet(Guid idMedia, Guid idLibelle);

        #endregion Signets
    }
}