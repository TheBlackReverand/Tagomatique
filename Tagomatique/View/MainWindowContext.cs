using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Tagomatique.Models;
using Tagomatique.Tools;
using System.IO;

namespace Tagomatique.View
{
    public class MainWindowContext : ViewModelBase
    {
        public MainWindow CurrentWindow { get; set; }

        public MainWindowContext(MainWindow currentWindow)
        {
            File.AppendAllText(@"C:\log.tago.txt", "MainWindowContext.MainWindowContext()");


            CurrentWindow = currentWindow;

            #region Liste GroupedTagDisponible

            GroupedTagDisponibles = new ObservableCollection<GroupedTagViewModel>();

            // Chargement de la liste des GroupedTagViewModel depuis la liste des TagViewModel
            var result = from tag in TagViewModel.GetAll()
                         group tag.LibelleTexte by tag.LibelleTexte
                             into grp
                             select new GroupedTagViewModel
                             {
                                 Libelle = grp.Key,
                                 Count = grp.Count(),
                                 Command = SelectGroupedTagCommand
                             };

            result.ToList().ForEach(t => GroupedTagDisponibles.Add(t));

            GroupedTagDisponiblesCollectionView = CollectionViewSource.GetDefaultView(GroupedTagDisponibles);

            if (GroupedTagDisponiblesCollectionView == null)
                throw new NullReferenceException("'GroupedTagDisponiblesCollectionView' is null in Constructor");

            GroupedTagDisponiblesCollectionView.CurrentChanged += OnGroupedTagDisponibleCollectionViewCurrentChanged;

            #endregion Liste GroupedTagDisponible

            #region Liste GroupedTagSelectionner

            GroupedTagSelectionner = new ObservableCollection<GroupedTagViewModel>();

            // Lors de l'Initialisation aucun GroupedTag n'est selectionner

            GroupedTagSelectionnerCollectionView = CollectionViewSource.GetDefaultView(GroupedTagSelectionner);

            if (GroupedTagSelectionnerCollectionView == null)
                throw new NullReferenceException("'GroupedTagSelectionnerCollectionView' is null in Constructor");

            GroupedTagSelectionnerCollectionView.CurrentChanged += OnGroupedTagSelectionnerCollectionViewCurrentChanged;

            #endregion Liste TagSelectionner

            if (GroupedTagDisponibles.Count > 0)
                CalculerRapportInterTagDisponible();
        }

        #region Listes

        #region Liste GroupedTag Disponible

        public ObservableCollection<GroupedTagViewModel> GroupedTagDisponibles { get; private set; }

        private readonly ICollectionView GroupedTagDisponiblesCollectionView;
        public GroupedTagViewModel CurrentGroupedTagDisponible
        {
            get { return GroupedTagDisponiblesCollectionView.CurrentItem as GroupedTagViewModel; }
        }

        private void OnGroupedTagDisponibleCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("CurrentGroupedTagDisponible");
        }

        #endregion Liste GroupedTag Disponible

        #region Liste GroupedTag Selectionner

        public ObservableCollection<GroupedTagViewModel> GroupedTagSelectionner { get; private set; }

        private readonly ICollectionView GroupedTagSelectionnerCollectionView;
        public GroupedTagViewModel CurrentGroupedTagSelectionner
        {
            get { return GroupedTagSelectionnerCollectionView.CurrentItem as GroupedTagViewModel; }
        }

        private void OnGroupedTagSelectionnerCollectionViewCurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("CurrentGroupedTagSelectionner");
        }

        #endregion Liste GroupedTag Selectionner

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
                    selectGroupedTagCommand = new RelayCommand<GroupedTagViewModel>(SelectGroupedTag, CanSelectGroupedTag);
                }

                return selectGroupedTagCommand;
            }
        }

        private bool CanSelectGroupedTag(GroupedTagViewModel clickedGroupedTag)
        {
            return true;
        }
        private void SelectGroupedTag(GroupedTagViewModel clickedGroupedTag)
        {
            if (clickedGroupedTag != null && GroupedTagDisponibles.Contains(clickedGroupedTag)
                && !GroupedTagSelectionner.Contains(clickedGroupedTag))
            {
                GroupedTagDisponibles.Remove(clickedGroupedTag);
                GroupedTagSelectionner.Add(clickedGroupedTag);

                // Le GroupedTag a ete selectionner, sa prochaine action sera 'DeSelectionner'
                clickedGroupedTag.Command = UnSelectGroupedTagCommand;
            }
        }

        #endregion Command Selectionner GroupedTag

        #region Command DeSelectionner GroupedTag

        private ICommand unSelectGroupedTagCommand;
        public ICommand UnSelectGroupedTagCommand
        {
            get
            {
                if (unSelectGroupedTagCommand == null)
                {
                    unSelectGroupedTagCommand = new RelayCommand<GroupedTagViewModel>(UnSelectGroupedTag, CanUnSelectGroupedTag);
                }

                return unSelectGroupedTagCommand;
            }
        }

        private bool CanUnSelectGroupedTag(GroupedTagViewModel clickedGroupedTag)
        {
            return true;
        }
        private void UnSelectGroupedTag(GroupedTagViewModel clickedGroupedTag)
        {
            if (clickedGroupedTag != null && !GroupedTagDisponibles.Contains(clickedGroupedTag)
                && GroupedTagSelectionner.Contains(clickedGroupedTag))
            {
                GroupedTagDisponibles.Add(clickedGroupedTag);
                GroupedTagSelectionner.Remove(clickedGroupedTag);

                // Le GroupedTag a ete deselectionner, sa prochaine action sera 'Selectionner'
                clickedGroupedTag.Command = SelectGroupedTagCommand;
            }
        }

        #endregion Command DeSelectionner GroupedTag

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

        private void CalculerRapportInterTagDisponible()
        {
            // On recherche les Tags les moins utilise, idem avec les plus utilise
            int min = GroupedTagDisponibles.Min(t => t.Count);
            int max = GroupedTagDisponibles.Max(t => t.Count);

            int nombreTag = GroupedTagDisponibles.Sum(t => t.Count);

            foreach (GroupedTagViewModel tag in GroupedTagDisponibles)
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