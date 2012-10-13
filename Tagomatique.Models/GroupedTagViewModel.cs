using System;
using System.Windows;
using System.Windows.Input;

namespace Tagomatique.Models
{
    public class GroupedTagViewModel : ICommandSource
    {
        public string Libelle { get; set; }
        public int Count { get; set; }

        public decimal PercentOfTotal { get; set; }
        public int TexteSize { get { return (int)Math.Round(12 + 12 * PercentOfTotal, 0); } }

        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public IInputElement CommandTarget { get { throw new NotImplementedException(); } }
    }
}