using System;
using System.Collections.Generic;
using Tagomatique.Data.Objects;
using Tagomatique.Resources;
using Tagomatique.Resources.Enums;

namespace Tagomatique.Data
{
	public abstract class AbstractDatabase
	{
		#region Singleton

		private static object _lock = new object();

		private static AbstractDatabase dataBase;
		public static AbstractDatabase DataBase
		{
			get
			{
				lock (_lock)
				{
					if (dataBase == null)
					{
						switch (Parametres.DataMode)
						{
							case DataModeType.SQL:
								dataBase = new SqlDatabase();
								break;

							case DataModeType.XML:
								throw new NotSupportedException(Erreurs.DataModeNonSupporter);

							default:
								throw new NotSupportedException(Erreurs.DataModeNonSupporter);
						}
					}

					return dataBase;
				}
			}
		}

		#endregion


		#region Infos

		public abstract Infos GetInfos();
		public abstract string GetVersion();

		#endregion Infos


		#region Dossiers

		public abstract List<Dossier> GetAllDossier();

		public abstract Guid AjouterDossier(string nom, string chemin);
		public abstract void ModifierDossier(Guid idDossier, string nom, string chemin);
		public abstract void SupprimerDossier(Guid idDossier);

		#endregion Dosssiers

		#region Medias

		public abstract List<Media> GetAllMedia();

		public abstract Guid AjouterMedia(string nom, string relativeURL, Guid idDossier);
		public abstract void ModifierMedia(Guid idMedia, string nom, string relativeURL, Guid idDossier);
		public abstract void SupprimerMedia(Guid idMedia);

		#endregion Medias


		#region Chapitres

		public abstract List<Chapitre> GetAllChapitre();

		public abstract Guid AjouterChapitre(Guid idMedia, string description, string debut, string fin);
		public abstract void SupprimerChapitre(Guid idChapitre);

		#endregion Chapitres


		#region Tags

		public abstract List<Tag> GetAllTag();

		public abstract Guid AjouterTagForMedia(Guid idMedia, string libelleTexte);
		public abstract Guid AjouterTagForChapitre(Guid idChapitre, string libelleTexte);
		public abstract void SupprimerTag(Guid idTag);

		#endregion Tags

		#region Signets

		public abstract List<Signet> GetAllSignet();

		public abstract Guid AjouterSignet(Guid idMedia, string libelleTexte, string duree);
		public abstract void SupprimerSignet(Guid idSignet);

		#endregion Signets
	}
}