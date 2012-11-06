using System;
using System.Collections.Generic;
using System.Linq;
using Tagomatique.Data.Objects;
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

		public override Infos GetInfos()
		{
			using (SQLCompactContext context = GetContext())
			{
				var tmp = context.Infos.First();

				return new Infos { Version = tmp.Version };
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

		public override List<Dossier> GetAllDossier()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Dossier.Select(d => new Dossier
													   {
														   ID_Dossier = d.ID_Dossier,
														   Chemin = d.Chemin,
														   Nom = d.Nom
													   }).ToList();
			}
		}

		public override Dossier GetDossierByKey(Guid idDossier)
		{
			using (SQLCompactContext context = GetContext())
			{
				var dossier = context.Dossier.SingleOrDefault(d => d.ID_Dossier == idDossier);

				return dossier != null ? new Dossier { ID_Dossier = dossier.ID_Dossier, Chemin = dossier.Chemin, Nom = dossier.Nom } : null;
			}
		}

		public override Guid AjouterDossier(string nom, string chemin)
		{
			Guid id = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				Context.Dossier newDossier = new Context.Dossier
				{
					ID_Dossier = id,
					Nom = nom,
					Chemin = chemin
				};

				context.Dossier.InsertOnSubmit(newDossier);
				context.SubmitChanges();
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

					context.SubmitChanges();
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
					context.Dossier.DeleteOnSubmit(dossierForDelete);
					context.SubmitChanges();
				}
			}
		}

		#endregion Dossiers

		#region Medias

		public override List<Media> GetAllMedia()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Media.Select(m => new Media
													 {
														 ID_Media = m.ID_Media,
														 Nom = m.Nom,
														 RelativeURL = m.RelativeURL,
														 FK_ID_Dossier = m.FK_ID_Dossier
													 }).ToList();
			}
		}

		public override Media GetMediaByKey(Guid idMedia)
		{
			using (SQLCompactContext context = GetContext())
			{
				var media = context.Media.SingleOrDefault(m => m.ID_Media == idMedia);

				return media != null ? new Media { ID_Media = media.ID_Media, Nom = media.Nom, RelativeURL = media.RelativeURL, FK_ID_Dossier = media.FK_ID_Dossier } : null;
			}
		}


		public override Guid AjouterMedia(string nom, string relativeURL, Guid idDossier)
		{
			Guid id = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				Context.Media newMedia = new Context.Media
				{
					ID_Media = id,
					Nom = nom,
					RelativeURL = relativeURL,
					FK_ID_Dossier = idDossier
				};

				context.Media.InsertOnSubmit(newMedia);
				context.SubmitChanges();
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

					context.SubmitChanges();
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
					context.Media.DeleteOnSubmit(mediaForDelete);
					context.SubmitChanges();
				}
			}
		}

		#endregion Medias


		#region Tags

		public override List<Tag> GetAllTag()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Tag.Select(t => new Tag { ID_Tag = t.ID_Tag, Libelle = t.Libelle, FK_ID_Chapitre = t.FK_ID_Chapitre, FK_ID_Media = t.FK_ID_Media }).ToList();
			}
		}

		public override Tag GetTagByKey(Guid idTag)
		{
			using (SQLCompactContext context = GetContext())
			{
				var tag = context.Tag.SingleOrDefault(tom => tom.ID_Tag == idTag);

				return tag != null ? new Tag { ID_Tag = tag.ID_Tag, Libelle = tag.Libelle, FK_ID_Chapitre = tag.FK_ID_Chapitre, FK_ID_Media = tag.FK_ID_Media } : null;
			}
		}

		public override List<Tag> GetTagOfMedia(Guid idMedia)
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Tag.Where(tom => tom.FK_ID_Media == idMedia).Select(tom => new Tag
																							  {
																								  ID_Tag = tom.ID_Tag,
																								  Libelle = tom.Libelle,
																								  FK_ID_Chapitre = tom.FK_ID_Chapitre,
																								  FK_ID_Media = tom.FK_ID_Media
																							  }).ToList();
			}
		}
		public override List<Tag> GetTagOfChapitre(Guid idChapitre)
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Tag.Where(toc => toc.FK_ID_Chapitre == idChapitre).Select(toc => new Tag
																									{
																										ID_Tag = toc.ID_Tag,
																										Libelle = toc.Libelle,
																										FK_ID_Chapitre = toc.FK_ID_Chapitre,
																										FK_ID_Media = toc.FK_ID_Media
																									}).ToList();
			}
		}

		public override Guid AjouterTagForChapitre(Guid idChapitre, string libelleTexte)
		{
			Guid idTag = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				if (!context.Tag.Any(toc => toc.FK_ID_Chapitre == idChapitre && toc.Libelle == libelleTexte))
				{
					Context.Tag newTag = new Context.Tag
											 {
												 ID_Tag = idTag,
												 Libelle = libelleTexte,
												 FK_ID_Chapitre = idChapitre,
												 FK_ID_Media = null
											 };

					context.Tag.InsertOnSubmit(newTag);
					context.SubmitChanges();

					return idTag;
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
					Context.Tag newTag = new Context.Tag
					{
						ID_Tag = idTag,
						Libelle = libelleTexte,
						FK_ID_Chapitre = null,
						FK_ID_Media = idMedia
					};

					context.Tag.InsertOnSubmit(newTag);
					context.SubmitChanges();

					return idTag;
				}
			}

			return Guid.Empty;
		}
		public override void SupprimerTag(Guid idTag)
		{
			using (SQLCompactContext context = GetContext())
			{
				Context.Tag tagForDelete = context.Tag.SingleOrDefault(t => t.ID_Tag == idTag);

				if (tagForDelete != null)
				{
					context.Tag.DeleteOnSubmit(tagForDelete);
					context.SubmitChanges();
				}
			}
		}

		#endregion TagsOfMedia

		#region Signets

		public override List<Signet> GetAllSignet()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Signet.Select(s => new Signet
													  {
														  ID_Signet = s.ID_Signet,
														  Libelle = s.Libelle,
														  Duree = s.Duree,
														  FK_ID_Media = s.FK_ID_Media
													  }).ToList();
			}
		}

		public override Signet GetSignetByKey(Guid idSignet)
		{
			using (SQLCompactContext context = GetContext())
			{
				var signet = context.Signet.SingleOrDefault(s => s.ID_Signet == idSignet);

				return signet != null ? new Signet { ID_Signet = signet.ID_Signet, Libelle = signet.Libelle, Duree = signet.Duree, FK_ID_Media = signet.FK_ID_Media } : null;
			}
		}
		public override List<Signet> GetSignetOfMedia(Guid idMedia)
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Signet.Where(s => s.FK_ID_Media == idMedia).Select(s => new Signet
																						   {
																							   ID_Signet = s.ID_Signet,
																							   Libelle = s.Libelle,
																							   Duree = s.Duree,
																							   FK_ID_Media = s.FK_ID_Media
																						   }).ToList();
			}
		}

		public override Guid AjouterSignet(Guid idMedia, string libelleTexte, string duree)
		{
			Guid idSignet = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				if (!context.Signet.Any(s => s.FK_ID_Media == idMedia && s.Libelle == libelleTexte && s.Duree == duree))
				{
					Context.Signet newSignet = new Context.Signet
					{
						ID_Signet = idSignet,
						Libelle = libelleTexte,
						Duree = duree,
						FK_ID_Media = idMedia
					};

					context.Signet.InsertOnSubmit(newSignet);
					context.SubmitChanges();

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
					context.Signet.DeleteOnSubmit(signetForDelete);
					context.SubmitChanges();
				}
			}
		}

		#endregion Signets

		#region Chapitres

		public override List<Chapitre> GetAllChapitre()
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Chapitre.Select(c => new Chapitre
														{
															ID_Chapitre = c.ID_Chapitre,
															Debut = c.Debut,
															Fin = c.Fin,
															Description = c.Description
														}).ToList();
			}
		}

		public override Chapitre GetChapitreByKey(Guid idChapitre)
		{
			using (SQLCompactContext context = GetContext())
			{
				var chapitre = context.Chapitre.SingleOrDefault(s => s.ID_Chapitre == idChapitre);

				return chapitre != null ? new Chapitre { ID_Chapitre = chapitre.ID_Chapitre, Debut = chapitre.Debut, Fin = chapitre.Fin, Description = chapitre.Description } : null;
			}
		}
		public override List<Chapitre> GetChapitreOfMedia(Guid idMedia)
		{
			using (SQLCompactContext context = GetContext())
			{
				return context.Chapitre.Where(c => c.FK_ID_Media == idMedia).Select(c => new Chapitre
																							 {
																								 ID_Chapitre = c.ID_Chapitre,
																								 Debut = c.Debut,
																								 Fin = c.Fin,
																								 Description = c.Description
																							 }).ToList();
			}
		}

		public override Guid AjouterChapitre(Guid idMedia, string description, string debut, string fin)
		{
			Guid idChapitre = Guid.NewGuid();

			using (SQLCompactContext context = GetContext())
			{
				if (!context.Chapitre.Any(c => c.Debut == debut && c.Fin == fin && c.Description == description))
				{
					Context.Chapitre newChapitre = new Context.Chapitre
					{
						ID_Chapitre = idChapitre,
						Debut = debut,
						Fin = fin,
						Description = description,
						FK_ID_Media = idMedia
					};

					context.Chapitre.InsertOnSubmit(newChapitre);
					context.SubmitChanges();

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
					context.Chapitre.DeleteOnSubmit(chapitreForDelete);
					context.SubmitChanges();
				}
			}
		}

		#endregion Chapitres
	}
}