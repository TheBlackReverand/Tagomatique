using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using Tagomatique.Models;
using Tagomatique.Models.Abstract;
using Tagomatique.Supplies;
using Tagomatique.Tools;
using System.IO;

namespace Tagomatique.View
{
	public class SearchWindowContext : ViewModelBase
	{
		private int nombrePhoto;
		private int nombreMusique;
		private int nombreVideo;

		public int nombreMediaInvalide;

		public SearchWindowContext()
		{
			InitialiserCompteur();

			#region Liste Medias

			Medias = new ObservableCollection<MediaViewModel>();

			MediasCollectionView = CollectionViewSource.GetDefaultView(Medias);
			MediasCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("RelativeURL"));

			if (MediasCollectionView == null)
				throw new NullReferenceException("'MediasCollectionView' is null in Constructor");

			MediasCollectionView.CurrentChanged += OnMediasCollectionViewCurrentChanged;

			#endregion Liste Medias
		}

		#region Accesseurs "Nombre de ..."

		public int NombreMedia
		{
			get { return nombreMusique + nombreVideo + nombrePhoto; }
		}
		public string NombreMediaText
		{
			get { return String.Format(Messages.NombreMedia, NombreMedia); }
		}

		public int NombrePhoto
		{
			get { return nombrePhoto; }
			set
			{
				nombrePhoto = value;

				OnPropertyChanged("NombrePhoto");
				OnPropertyChanged("NombrePhotoText");
				OnPropertyChanged("NombreMedia");
				OnPropertyChanged("NombreMediaText");
			}
		}
		public string NombrePhotoText
		{
			get { return String.Format(Messages.NombrePhoto, NombrePhoto); }
		}

		public int NombreMusique
		{
			get { return nombreMusique; }
			set
			{
				nombreMusique = value;

				OnPropertyChanged("NombreMusique");
				OnPropertyChanged("NombreMusiqueText");
				OnPropertyChanged("NombreMedia");
				OnPropertyChanged("NombreMediaText");
			}
		}
		public string NombreMusiqueText
		{
			get { return String.Format(Messages.NombreMusique, NombreMusique); }
		}

		public int NombreVideo
		{
			get { return nombreVideo; }
			set
			{
				nombreVideo = value;

				OnPropertyChanged("NombreVideo");
				OnPropertyChanged("NombreVideoText");
				OnPropertyChanged("NombreMedia");
				OnPropertyChanged("NombreMediaText");
			}
		}
		public string NombreVideoText
		{
			get { return String.Format(Messages.NombreVideo, NombreVideo); }
		}

		public int NombreMediaValide { get { return NombreMusique + NombreVideo + NombrePhoto; } }
		public string NombreMediaValideText
		{
			get { return String.Format(Messages.NombreMediaValide, NombreMediaValide); }
		}

		public int NombreMediaInvalide
		{
			get { return nombreMediaInvalide; }
			set
			{
				nombreMediaInvalide = value;

				OnPropertyChanged("NombreMediaInvalide");
				OnPropertyChanged("NombreMediaInvalideText");
			}
		}
		public string NombreMediaInvalideText
		{
			get { return String.Format(Messages.NombreMediaInvalide, NombreMediaInvalide); }
		}

		#endregion Accesseurs "Nombre de ..."

		private void InitialiserCompteur()
		{
			nombrePhoto = 0;
			nombreVideo = 0;
			nombreMusique = 0;

			nombreMediaInvalide = 0;
		}

		#region Commands

		#region Command Ajouter Dossier

		private ICommand ajouterDossierCommand;
		public ICommand AjouterDossierCommand
		{
			get
			{
				if (ajouterDossierCommand == null)
				{
					ajouterDossierCommand = new RelayCommand(() => AjouterDossier(false));
				}

				return ajouterDossierCommand;
			}
		}

		private ICommand ajouterDossierRelatifCommand;
		public ICommand AjouterDossierRelatifCommand
		{
			get
			{
				if (ajouterDossierRelatifCommand == null)
				{
					ajouterDossierRelatifCommand = new RelayCommand(() => AjouterDossier(true));
				}

				return ajouterDossierRelatifCommand;
			}
		}

		private void AjouterDossier(bool relatif)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();

			fbd.Description = Messages.SelectionnerDossier;
			fbd.RootFolder = Environment.SpecialFolder.MyComputer;
			fbd.SelectedPath = relatif ? Environment.CurrentDirectory : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			fbd.ShowNewFolderButton = false;

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				foreach (string file in Directory.GetFiles(fbd.SelectedPath))
				{
					string fileUpper = file.ToUpper();

					if (Parametres.ValidExtensionMusique.Contains(Path.GetExtension(fileUpper)))
					{
						NombreMusique++;
					}
					else if (Parametres.ValidExtensionPhoto.Contains(Path.GetExtension(fileUpper)))
					{
						NombrePhoto++;
					}
					else if (Parametres.ValidExtensionVideo.Contains(Path.GetExtension(fileUpper)))
					{
						NombreVideo++;
					}
					else
					{
						NombreMediaInvalide++;
					}

					string directory = relatif ? PathTools.GenerateRelativePath(Path.GetDirectoryName(file)) : Path.GetDirectoryName(file);

					MediaViewModel media = new MediaViewModel();
					media.Nom = Path.GetFileName(file);
					media.RelativeURL = directory;

					Medias.Add(media);
				}
			}
		}


		private ICommand ajouterFichierCommand;
		public ICommand AjouterFichierCommand
		{
			get
			{
				if (ajouterFichierCommand == null)
				{
					ajouterFichierCommand = new RelayCommand(() => AjouterFichier(false));
				}

				return ajouterFichierCommand;
			}
		}

		private ICommand ajouterFichierRelatifCommand;
		public ICommand AjouterFichierRelatifCommand
		{
			get
			{
				if (ajouterFichierRelatifCommand == null)
				{
					ajouterFichierRelatifCommand = new RelayCommand(() => AjouterFichier(true));
				}

				return ajouterFichierRelatifCommand;
			}
		}

		private void AjouterFichier(bool relatif)
		{
			OpenFileDialog ofd = new OpenFileDialog();

			ofd.AddExtension = true;
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;

			string filtres = string.Empty;
			#region Generation des Filtres

			filtres += "Tous le(s) fichier(s) supporté(s)|*.jpg;*.jpeg;*.avi;*.mpg;*.mpeg;*.mp3";
			filtres += "|";

			// { ".JPG", ".JPEG" };
			filtres += "Fichier(s) photo supporté(s) (*.jpg, *.jpeg)|*.jpg;*.jpeg";
			filtres += "|";

			// { ".AVI", ".MPG", ".MPEG" };
			filtres += "Fichier(s) vidéo supporté(s) (*.avi, *.mpg, *.mpeg)|*.avi;*.mpg;*.mpeg";
			filtres += "|";

			// { ".MP3" };
			filtres += "Fichier(s) audio supporté(s) (*.mp3)|*.mp3";
			filtres += "|";

			filtres += "Tous les fichiers (*.*)|*.*";

			#endregion Generation des Filtres

			ofd.Filter = filtres;
			ofd.InitialDirectory = relatif ? Environment.CurrentDirectory : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			ofd.Multiselect = true;
			ofd.Title = Messages.SelectionnerFichier;

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				foreach (string file in ofd.FileNames)
				{
					string fileUpper = file.ToUpper();

					string directory = relatif ? PathTools.GenerateRelativePath(Path.GetDirectoryName(file)) : Path.GetDirectoryName(file);
					if (Parametres.ValidExtensionMusique.Contains(Path.GetExtension(fileUpper)))
					{
						NombreMusique++;
					}
					else if (Parametres.ValidExtensionPhoto.Contains(Path.GetExtension(fileUpper)))
					{
						NombrePhoto++;
					}
					else if (Parametres.ValidExtensionVideo.Contains(Path.GetExtension(fileUpper)))
					{
						NombreVideo++;
					}
					else
					{
						NombreMediaInvalide++;
						directory = "INVALIDE";
					}

					MediaViewModel media = new MediaViewModel();
					media.Nom = Path.GetFileName(file);
					media.RelativeURL = directory;

					Medias.Add(media);
				}
			}
		}

		#endregion Command Ajouter Dossier

		#endregion Commands

		#region Listes

		#region Liste Medias

		public ObservableCollection<MediaViewModel> Medias { get; private set; }

		private readonly ICollectionView MediasCollectionView;
		public MediaViewModel CurrentMedias
		{
			get { return MediasCollectionView.CurrentItem as MediaViewModel; }
		}

		private void OnMediasCollectionViewCurrentChanged(object sender, EventArgs e)
		{
			OnPropertyChanged("CurrentMediaViewModel");
		}

		#endregion Liste Medias

		#endregion Listes
	}
}