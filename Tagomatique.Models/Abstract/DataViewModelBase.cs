﻿using Tagomatique.Supplies.Enums;

namespace Tagomatique.Models.Abstract
{
	public abstract class DataViewModelBase : ViewModelBase
	{
		protected DataModelState State { get; set; }

		protected DataViewModelBase()
		{
			State = DataModelState.New;
		}

		protected override void OnPropertyChanged(string propertyName)
		{
			if (State == DataModelState.Unchanged)
				State = DataModelState.Modified;

			base.OnPropertyChanged(propertyName);
		}

		protected abstract void insert();
		protected abstract void update();
		protected abstract void delete();

		public virtual void markedAsToRemove() { State = DataModelState.Deleted; }

		public virtual void save()
		{
			switch (State)
			{
				case DataModelState.Unchanged:
					// DO NOTHING
					break;

				case DataModelState.New:
					insert();
					break;

				case DataModelState.Modified:
					update();
					break;

				case DataModelState.Deleted:
					delete();
					break;
			}
		}
	}
}