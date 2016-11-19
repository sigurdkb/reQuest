using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public class GameViewModel : INotifyPropertyChanged
	{
		private double distanceToTarget;
		private Player owner;

		public Player Owner
		{
			get { return owner; }
			set
			{
				if (owner != value)
				{
					owner = value;
					OnPropertyChanged(nameof(Owner));
				}
			}
		}

		public double DistanceToTarget
		{
			get { return distanceToTarget; }
			set
			{
				if (distanceToTarget != value)
				{
					distanceToTarget = value;
					OnPropertyChanged(nameof(DistanceToTarget));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			var changed = PropertyChanged;
			if (changed != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
