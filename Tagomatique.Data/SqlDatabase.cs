using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Resources;

namespace Tagomatique.Data
{
    public class SqlDatabase : AbstractDatabase
    {
        private static Tagomatique GetContext()
        {
            return new Tagomatique("Data Source=|DataDirectory|\\Tagomatique.sdf");
        }

        #region Infos

        public override string GetVersion()
        {
            try
            {
                using (Tagomatique context = GetContext())
                {
                    return context.Infos.First().Version;
                }
            }
            catch (Exception)
            {
                return Erreurs.RecuperationVersion;
            }
        }

        #endregion Infos


        #region Dossiers

        public override List<Dossier> GetAllDossier()
        {
            using (Tagomatique context = GetContext())
            {
                return context.Dossiers.ToList();
            }
        }

        public override Dossier GetDossierByKey(Guid idDossier)
        {
            using (Tagomatique context = GetContext())
            {
                return GetDossierByKey(idDossier, context);
            }
        }
        public override Dossier GetDossierByKey(Guid idDossier, Tagomatique context)
        {
            return context.Dossiers.SingleOrDefault(d => d.ID_Dossier == idDossier);
        }

        public override void AjouterDossier(string nom, string chemin)
        {
            using (Tagomatique context = GetContext())
            {
                Dossier newDossier = new Dossier
                                      {
                                          ID_Dossier = Guid.NewGuid(),
                                          Nom = nom,
                                          Chemin = chemin
                                      };

                context.Dossiers.InsertOnSubmit(newDossier);
                context.SubmitChanges();
            }
        }

        public override void SupprimerDossier(Guid idDossier)
        {
            using (Tagomatique context = GetContext())
            {
                Dossier dossierForDelete = GetDossierByKey(idDossier, context);

                context.Dossiers.DeleteOnSubmit(dossierForDelete);
                context.SubmitChanges();
            }
        }

        #endregion Dossiers

        #region Medias

        public override List<Media> GetAllMedia()
        {
            using (Tagomatique context = GetContext())
            {
                return context.Medias.ToList();
            }
        }

        public override Media GetMediaByKey(Guid idMedia)
        {
            using (Tagomatique context = GetContext())
            {
                return GetMediaByKey(idMedia, context);
            }
        }
        public override Media GetMediaByKey(Guid idMedia, Tagomatique context)
        {
            return context.Medias.SingleOrDefault(m => m.ID_Media == idMedia);
        }

        public override void AjouterMedia(string nom, string relativeURL, Guid idDossier)
        {
            using (Tagomatique context = GetContext())
            {
                Media newMedia = new Media
                {
                    ID_Media = Guid.NewGuid(),
                    Nom = nom,
                    RelativeURL = relativeURL,
                    ID_Dossier = idDossier
                };

                context.Medias.InsertOnSubmit(newMedia);
                context.SubmitChanges();
            }
        }

        public override void SupprimerMedia(Guid idMedia)
        {
            using (Tagomatique context = GetContext())
            {
                Media mediaForDelete = GetMediaByKey(idMedia, context);

                context.Medias.DeleteOnSubmit(mediaForDelete);
                context.SubmitChanges();
            }
        }

        #endregion Medias


        #region Libelles

        public override List<Libelle> GetAllLibelle()
        {
            using (Tagomatique context = GetContext())
            {
                return context.Libelles.ToList();
            }
        }

        public override Libelle GetLibelleByKey(Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                return GetLibelleByKey(idLibelle, context);
            }
        }
        public override Libelle GetLibelleByKey(Guid idLibelle, Tagomatique context)
        {
            return context.Libelles.SingleOrDefault(t => t.ID_Libelle == idLibelle);
        }

        public override Libelle GetLibelleByText(string libelle)
        {
            using (Tagomatique context = GetContext())
            {
                return GetLibelleByText(libelle, context);
            }
        }
        public override Libelle GetLibelleByText(string libelle, Tagomatique context)
        {
            return context.Libelles.SingleOrDefault(t => t.LibelleTexte == libelle);
        }

        public override void AjouterLibelle(string libelle)
        {
            using (Tagomatique context = GetContext())
            {
                Libelle newLibelle = new Libelle
                                  {
                                      ID_Libelle = Guid.NewGuid(),
                                      LibelleTexte = libelle
                                  };

                context.Libelles.InsertOnSubmit(newLibelle);
                context.SubmitChanges();
            }
        }

        public override void SupprimerLibelle(Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                Libelle libelleForDelete = GetLibelleByKey(idLibelle, context);

                context.Libelles.DeleteOnSubmit(libelleForDelete);
                context.SubmitChanges();
            }
        }

        #endregion Libelles

        #region Tags

        public override List<Tag> GetAllTag()
        {
            using (Tagomatique context = GetContext())
            {
                return context.Tags.ToList();
            }
        }

        public override Tag GetTagByKey(Guid idMedia, Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                return GetTagByKey(idMedia, idLibelle, context);
            }
        }
        public override Tag GetTagByKey(Guid idMedia, Guid idLibelle, Tagomatique context)
        {
            return context.Tags.SingleOrDefault(tom => tom.ID_Media == idMedia && tom.ID_Libelle == idLibelle);
        }

        public override List<Tag> GetTagOfMedia(Guid idMedia)
        {
            using (Tagomatique context = GetContext())
            {
                return GetTagOfMedia(idMedia, context);
            }
        }
        public override List<Tag> GetTagOfMedia(Guid idMedia, Tagomatique context)
        {
            return context.Tags.Where(tom => tom.ID_Media == idMedia).ToList();
        }

        public override void AjouterTag(Guid idMedia, Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                Tag newTag = new Tag
                                 {
                                     ID_Media = idMedia,
                                     ID_Libelle = idLibelle
                                 };

                context.Tags.InsertOnSubmit(newTag);
                context.SubmitChanges();
            }
        }

        public override void SupprimerTag(Guid idMedia, Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                Tag tagForDelete = GetTagByKey(idMedia, idLibelle, context);

                context.Tags.DeleteOnSubmit(tagForDelete);
                context.SubmitChanges();
            }
        }

        #endregion TagsOfMedia

        #region Signets

        public override List<Signet> GetAllSignet()
        {
            using (Tagomatique context = GetContext())
            {
                return context.Signets.ToList();
            }
        }

        public override Signet GetSignetByKey(Guid idMedia, Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                return GetSignetByKey(idMedia, idLibelle, context);
            }
        }
        public override Signet GetSignetByKey(Guid idMedia, Guid idLibelle, Tagomatique context)
        {
            return context.Signets.SingleOrDefault(tom => tom.ID_Media == idMedia && tom.ID_Libelle == idLibelle);
        }

        public override List<Signet> GetSignetOfMedia(Guid idMedia)
        {
            using (Tagomatique context = GetContext())
            {
                return GetSignetOfMedia(idMedia, context);
            }
        }
        public override List<Signet> GetSignetOfMedia(Guid idMedia, Tagomatique context)
        {
            return context.Signets.Where(tom => tom.ID_Media == idMedia).ToList();
        }

        public override void AjouterSignet(Guid idMedia, Guid idLibelle, TimeSpan duree)
        {
            using (Tagomatique context = GetContext())
            {
                Signet newSignet = new Signet
                {
                    ID_Media = idMedia,
                    ID_Libelle = idLibelle,
                    // Format = hh:mm:ss
                    Duree = duree.ToString()
                };

                context.Signets.InsertOnSubmit(newSignet);
                context.SubmitChanges();
            }
        }

        public override void SupprimerSignet(Guid idMedia, Guid idLibelle)
        {
            using (Tagomatique context = GetContext())
            {
                Signet signetForDelete = GetSignetByKey(idMedia, idLibelle, context);

                context.Signets.DeleteOnSubmit(signetForDelete);
                context.SubmitChanges();
            }
        }

        #endregion SignetsOfMedia
    }
}