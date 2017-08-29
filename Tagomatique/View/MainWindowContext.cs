using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Tagomatique.Models;
using Tagomatique.Models.Abstract;
using Tagomatique.Tools;

namespace Tagomatique.View
{
	public class MainWindowContext : ViewModelBase
	{
		public MainWindow CurrentWindow { get; set; }

		public MainWindowContext() { }

		public MainWindowContext(MainWindow currentWindow)
		{
			CurrentWindow = currentWindow;

			#region Liste GroupedTag

			GroupedTag = new ObservableCollection<GroupedTagViewModel>();

			// Chargement de la liste des GroupedTagViewModel depuis la liste des TagViewModel
			var result = from tag in TagViewModel.GetAll()
						 group tag.Libelle by tag.Libelle
							 into grp
							 select new GroupedTagViewModel
							 {
								 Libelle = grp.Key,
								 Count = grp.Count(),
								 IsSelected = false,
								 Command = SelectGroupedTagCommand
							 };

			result.ToList().ForEach(t => GroupedTag.Add(t));

			GroupedTagDisponibles = new ListCollectionView(GroupedTag);
			GroupedTagDisponibles.Filter = o => !((GroupedTagViewModel)o).IsSelected;

			GroupedTagSelectionner = new ListCollectionView(GroupedTag);
			GroupedTagSelectionner.Filter = o => ((GroupedTagViewModel)o).IsSelected;

			#endregion Liste GroupedTag

			#region Liste Media correspondant

			MediaCorrespondant = new ObservableCollection<MediaViewModel>();

			// Lors de l'Initialisation aucun GroupedTag n'est selectionne donc aucun Media n'est selectionner

			MediaCorrespondantCollectionView = CollectionViewSource.GetDefaultView(MediaCorrespondant);

			if (MediaCorrespondantCollectionView == null)
				throw new NullReferenceException("'MediaCorrespondantCollectionView' is null in Constructor");

			MediaCorrespondantCollectionView.CurrentChanged += OnMediaCorrespondantCollectionViewCurrentChanged;

			#endregion Liste Media correspondant

			if (GroupedTagDisponibles.Count > 0)
				CalculerRapportInterTagDisponible();
		}

		#region Listes

		#region Liste GroupedTag Disponible

		public ObservableCollection<GroupedTagViewModel> GroupedTag { get; set; }

		public ListCollectionView GroupedTagDisponibles { get; private set; }
		public ListCollectionView GroupedTagSelectionner { get; private set; }

		#endregion Liste GroupedTag Selectionner

		#region Liste Media correspondant

		public ObservableCollection<MediaViewModel> MediaCorrespondant { get; private set; }

		private readonly ICollectionView MediaCorrespondantCollectionView;
		public MediaViewModel CurrentMediaCorrespondant
		{
			get { return MediaCorrespondantCollectionView.CurrentItem as MediaViewModel; }
		}

		private void OnMediaCorrespondantCollectionViewCurrentChanged(object sender, EventArgs e)
		{
			OnPropertyChanged("CurrentMediaCorrespondant");
		}

		#endregion Liste Media correspondant

		#endregion Listes

		#region Commands

		#region Command Selectionner GroupedTag

		private ICommand selectGroupedTagCommand;
		public ICommand SelectGroupedTagCommand
		{
			get
			{
				if (selectGroupedTagCommand == null)
				{
					selectGroupedTagCommand = new RelayCommand<GroupedTagViewModel>(SelectGroupedTag);
				}

				return selectGroupedTagCommand;
			}
		}

		private void SelectGroupedTag(GroupedTagViewModel clickedGroupedTag)
		{
			if (clickedGroupedTag != null)
			{
				clickedGroupedTag.IsSelected = !clickedGroupedTag.IsSelected;

				GroupedTagSelectionner.Refresh();
				GroupedTagDisponibles.Refresh();

				ActualiserListeMediaCorrespondant();
			}
		}

		#endregion Command Selectionner GroupedTag

		#region Command Afficher SearchWindow

		private ICommand showSearchWindowCommand;
		public ICommand ShowSearchWindowCommand
		{
			get
			{
				if (showSearchWindowCommand == null)
				{
					showSearchWindowCommand = new RelayCommand(ShowSearchWindow);
				}

				return showSearchWindowCommand;
			}
		}

		private void ShowSearchWindow()
		{
			SearchWindow searchWindow = new SearchWindow();

			searchWindow.Owner = CurrentWindow;
			searchWindow.ShowDialog();
		}

		#endregion Command Afficher SearchWindow

		#endregion Commands

		private void ActualiserListeMediaCorrespondant()
		{
			List<MediaViewModel> lstMediaCorrespondant = MediaViewModel.GetByTagLibelle(GroupedTagSelectionner.OfType<GroupedTagViewModel>().Select(groupedTag => groupedTag.Libelle));

			for (int i = MediaCorrespondant.Count; i > 0; i--)
			{
				MediaViewModel mediaViewModel = MediaCorrespondant[i - 1];

				if (!lstMediaCorrespondant.Contains(mediaViewModel))
					MediaCorrespondant.Remove(mediaViewModel);
			}

			foreach (MediaViewModel mediaViewModel in lstMediaCorrespondant)
			{
				if (!MediaCorrespondant.Contains(mediaViewModel))
					MediaCorrespondant.Add(mediaViewModel);
			}
		}

		private void CalculerRapportInterTagDisponible()
		{
			// On recherche les Tags les moins utilise, idem avec les plus utilise
			int min = GroupedTag.Min(t => t.Count);
			int max = GroupedTag.Max(t => t.Count);

			int nombreTag = GroupedTag.Sum(t => t.Count);

			foreach (GroupedTagViewModel tag in GroupedTag)
			{
				// Taille normale (+0%)
				if (tag.Count == min)
				{
					tag.PercentOfTotal = 0;
				}
				// Double de la taille normale (+200%)
				else if (tag.Count == max)
				{
					tag.PercentOfTotal = 2;
				}
				// Taille variable (+ 2*N %)
				else
				{
					tag.PercentOfTotal = 2 * ((decimal)tag.Count / nombreTag);
				}
			}
		}
	}
}