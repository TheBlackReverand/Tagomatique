using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Tagomatique.Resources.Enums;

namespace Tagomatique.Resources
{
    public static class Parametres
    {
        public static DataModeType DataMode
        {
            get
            {
                string dataMode = ConfigurationManager.AppSettings["DataMode"];

                if (String.IsNullOrWhiteSpace(dataMode))
                {
                    return DataModeType.INCONNU;
                }

                DataModeType mode;

                return Enum.TryParse(dataMode, true, out mode) ? mode : DataModeType.INCONNU;
            }
        }

        // En cas d'ajout mettre à jour les filtres de la OpenFileDialog dans SearchWindowContext.cs
        public static List<string> ValidExtensionPhoto = new List<string> { ".JPG", ".JPEG" };
        public static List<string> ValidExtensionVideo = new List<string> { ".AVI", ".MPG", ".MPEG" };
        public static List<string> ValidExtensionMusique = new List<string> { ".MP3" };

        public static List<string> ValidExtension
        {
            get
            {
                return ValidExtensionMusique.Concat(ValidExtensionVideo).Concat(ValidExtensionPhoto).ToList();
            }
        }
    }
}