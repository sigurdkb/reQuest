using System;
using System.ComponentModel;

namespace reQuest
{
	public class LocationViewModel : INotifyPropertyChanged
	{
		double distance;

		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public double Distance
		{
			get
			{
				return distance;
			}
			set
			{
				if (distance != value)
				{
					distance = value;
					OnPropertyChanged("Distance");
				}
			}
		}
		public string BeaconUUID { get; set; }
		public string BeaconID { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		public LocationViewModel()
		{
		}

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
