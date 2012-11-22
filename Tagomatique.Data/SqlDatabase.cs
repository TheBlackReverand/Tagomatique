using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data.Context;
using Tagomatique.Data.Interface;
using SQLCompactContext = Tagomatique.Data.Context.SQLCompactContext;

namespace Tagomatique.Data
{
	internal class SqlDatabase : AbstractDatabase
	{
		private static SQLCompactContext GetContext()
		{
			return new SQLCompactContext("Data Source=|DataDirectory|\\Tagomatique.sdf");
		}

		#region Infos

		public override IInfos GetInfos()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Infos.First();
			}
		}

		public override string GetVersion()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Infos.First().Version;
			}
		}

		#endregion Infos


		#region Dossiers

		public override List<IDossier> GetAllDossier()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Dossier.Cast<IDossier>().ToList();
			}
		}

		public override Guid AjouterDossier(string nom, string chemin)
		{
			Guid id = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				Dossier newDossier = new Dossier
				{
					ID_Dossier = id,
					Nom = nom,
					Chemin = chemin
				};

				context.Dossier.AddObject(newDossier);
				context.SaveChanges();
			}

			return id;
		}
		public override void ModifierDossier(Guid idDossier, string nom, string chemin)
		{
			using (SQLCompactContext context = GetContext())
			{
				var dossier = context.Dossier.SingleOrDefault(d => d.ID_Dossier == idDossier);

				if (dossier != null)
				{
					dossier.Nom = nom;
					dossier.Chemin = chemin;

					context.SaveChanges();
				}
			}
		}
		public override void SupprimerDossier(Guid idDossier)
		{
			using (SQLCompactContext context = GetContext())
			{
				var dossierForDelete = context.Dossier.SingleOrDefault(d => d.ID_Dossier == idDossier);

				if (dossierForDelete != null)
				{
					context.Dossier.DeleteObject(dossierForDelete);
					context.SaveChanges();
				}
			}
		}

		#endregion Dossiers

		#region Medias

		public override List<IMedia> GetAllMedia()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Media.Cast<IMedia>().ToList();
			}
		}

		public override Guid AjouterMedia(string nom, string relativeURL, Guid idDossier)
		{
			Guid id = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				Media newMedia = new Media
				{
					ID_Media = id,
					Nom = nom,
					RelativeURL = relativeURL,
					FK_ID_Dossier = idDossier
				};

				context.Media.AddObject(newMedia);
				context.SaveChanges();
			}

			return id;
		}
		public override void ModifierMedia(Guid idMedia, string nom, string relativeURL, Guid idDossier)
		{
			using (SQLCompactContext context = GetContext())
			{
				var media = context.Media.SingleOrDefault(m => m.ID_Media == idMedia);

				if (media != null)
				{
					media.Nom = nom;
					media.RelativeURL = relativeURL;
					media.FK_ID_Dossier = idDossier;

					context.SaveChanges();
				}
			}
		}
		public override void SupprimerMedia(Guid idMedia)
		{
			using (SQLCompactContext context = GetContext())
			{
				var mediaForDelete = context.Media.SingleOrDefault(m => m.ID_Media == idMedia);

				if (mediaForDelete != null)
				{
					context.Media.DeleteObject(mediaForDelete);
					context.SaveChanges();
				}
			}
		}

		#endregion Medias


		#region Tags

		public override List<ITag> GetAllTag()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Tag.Cast<ITag>().ToList();
			}
		}

		public override Guid AjouterTagForChapitre(Guid idChapitre, string libelleTexte)
		{
			Guid id = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				// TODO : Deplacer ce test dans la couche Model
				if (!context.Tag.Any(toc => toc.FK_ID_Chapitre == idChapitre && toc.Libelle == libelleTexte))
				{
					Tag newTag = new Tag
					             	{
					             		ID_Tag = id,
					             		Libelle = libelleTexte,
					             		FK_ID_Chapitre = idChapitre,
					             		FK_ID_Media = null
					             	};

					context.Tag.AddObject(newTag);
					context.SaveChanges();

					return id;
				}
			}

			return Guid.Empty;
		}
		public override Guid AjouterTagForMedia(Guid idMedia, string libelleTexte)
		{
			Guid idTag = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				if (!context.Tag.Any(tom => tom.FK_ID_Chapitre == idMedia && tom.Libelle == libelleTexte))
				{
					Tag newTag = new Tag
					{
						ID_Tag = idTag,
						Libelle = libelleTexte,
						FK_ID_Chapitre = null,
						FK_ID_Media = idMedia
					};

					context.Tag.AddObject(newTag);
					context.SaveChanges();

					return idTag;
				}
			}

			return Guid.Empty;
		}
		public override void SupprimerTag(Guid idTag)
		{
			using (SQLCompactContext context = GetContext())
			{
				Tag tagForDelete = context.Tag.SingleOrDefault(t => t.ID_Tag == idTag);

				if (tagForDelete != null)
				{
					context.Tag.DeleteObject(tagForDelete);
					context.SaveChanges();
				}
			}
		}

		#endregion TagsOfMedia

		#region Signets

		public override List<ISignet> GetAllSignet()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Signet.Cast<ISignet>().ToList();
			}
		}

		public override Guid AjouterSignet(Guid idMedia, string libelleTexte, string duree)
		{
			Guid idSignet = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				if (!context.Signet.Any(s => s.FK_ID_Media == idMedia && s.Libelle == libelleTexte && s.Duree == duree))
				{
					Signet newSignet = new Signet
					{
						ID_Signet = idSignet,
						Libelle = libelleTexte,
						Duree = duree,
						FK_ID_Media = idMedia
					};

					context.Signet.AddObject(newSignet);
					context.SaveChanges();

					return idSignet;
				}
			}

			return Guid.Empty;
		}
		public override void SupprimerSignet(Guid idSignet)
		{
			using (SQLCompactContext context = GetContext())
			{
				var signetForDelete = context.Signet.SingleOrDefault(s => s.ID_Signet == idSignet);

				if (signetForDelete != null)
				{
					context.Signet.DeleteObject(signetForDelete);
					context.SaveChanges();
				}
			}
		}

		#endregion Signets

		#region Chapitres

		public override List<IChapitre> GetAllChapitre()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Chapitre.Cast<IChapitre>().ToList();
			}
		}

		public override Guid AjouterChapitre(Guid idMedia, string description, string debut, string fin)
		{
			Guid idChapitre = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				// TODO : Deporter ce test dans la couche Models
				if (!context.Chapitre.Any(c => c.Debut == debut && c.Fin == fin && c.Description == description))
				{
					Chapitre newChapitre = new Chapitre
					{
						ID_Chapitre = idChapitre,
						Debut = debut,
						Fin = fin,
						Description = description,
						FK_ID_Media = idMedia
					};

					context.Chapitre.AddObject(newChapitre);
					context.SaveChanges();

					return idChapitre;
				}
			}

			return Guid.Empty;
		}
		public override void SupprimerChapitre(Guid idChapitre)
		{
			using (SQLCompactContext context = GetContext())
			{
				var chapitreForDelete = context.Chapitre.SingleOrDefault(s => s.ID_Chapitre == idChapitre);

				if (chapitreForDelete != null)
				{
					context.Chapitre.DeleteObject(chapitreForDelete);
					context.SaveChanges();
				}
			}
		}

		#endregion Chapitres
	}
}