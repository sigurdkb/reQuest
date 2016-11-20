using System;
namespace reQuest
{
	public interface IGPSData
	{
		double Latitude { get; set; }
		double Longitude { get; set; }
	}
	public interface IBTData
	{
		double Distance { get; set; }
		string BeaconUUID { get; set; }
		string BeaconID { get; set; }
		string Proximity { get; set; }

	}

	public interface ILocation
	{
		void StartBeacon(string beaconUUID, string beaconID);
		void StopBeacon();
		void StartBeaconRanging(string beaconUUID, string beaconID);
		void StopBeaconRanging();
		void StartLocationTracking();
		void StopLocationTracking();

		double DistanceBetweenPostitions(double latA, double lonA, double latB, double lonB);

		event EventHandler<IGPSData> positionChanged;
		event EventHandler<IBTData> collitionDetected;

	}

}

