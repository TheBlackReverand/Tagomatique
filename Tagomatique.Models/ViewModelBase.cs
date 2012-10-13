using System;
using System.ComponentModel;
using Tagomatique.Data;
using Tagomatique.Resources;
using Tagomatique.Resources.Enums;

namespace Tagomatique.Models
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static AbstractDatabase dataBase;
        public static AbstractDatabase DataBase
        {
            get
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
}