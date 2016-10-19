using System;
namespace reQuest
{
	public interface ILocationData
	{
		double Latitude { get; set;}
		double Longitude { get; set; }
		double Distance { get; set; }
		string BeaconUUID { get; set; }
		string BeaconID { get; set; }

	}

	public interface ILocation
	{
		void StartBeacon(string beaconUUID, string beaconID);
		void StopBeacon();
		void StartTrackDistance(string beaconUUID, string beaconID);
		void StopTrackDistance();

		event EventHandler<ILocationData> distanceChanged;
		event EventHandler<ILocationData> collitionDetected;

	}

}

