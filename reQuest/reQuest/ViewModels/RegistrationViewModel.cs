using System;
using System.ComponentModel;

namespace reQuest
{
	public class RegistrationViewModel : INotifyPropertyChanged
	{
		string externalID;
		string externalToken;

		public string ExternalID 
		{
			get
			{
				return externalID;
			}
			set
			{
				if (externalID != value)
				{
					externalID = value;
					OnPropertyChanged(nameof(ExternalID));
				}
			}
		}

		public string ExternalToken
		{
			get
			{
				return externalToken;
			}
			set
			{
				if (externalToken != value)
				{
					externalToken = value;
					OnPropertyChanged(nameof(ExternalToken));
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public RegistrationViewModel()
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
