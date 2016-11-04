using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms.GoogleMaps;

namespace reQuest
{
	public class GameViewModel : INotifyPropertyChanged
	{
		double distanceToTarget;

		public string Player { get; set; }
		public string Target { get; set; }
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
