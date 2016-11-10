using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reQuest.Interfaces;
using CoreBluetooth;
using CoreLocation;
using CoreFoundation;
using Foundation;
using UIKit;
using reQuest.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(Location))]
namespace reQuest.iOS
{
	class PeripheralManagerDelegate : CBPeripheralManagerDelegate
	{
		public override void StateUpdated(CBPeripheralManager peripheral)
		{
		}
	}

	public class GPSData : EventArgs, IGPSData
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
	public class BTData : EventArgs, IBTData
	{
		public double Distance { get; set; }
		public string BeaconUUID { get; set; }
		public string BeaconID { get; set; }

	}

	public class Location : ILocation
    {
        public event EventHandler<IBTData> collitionDetected;
        public event EventHandler<IGPSData> positionChanged;



		CBPeripheralManager peripheralManager;
		CLLocationManager locationManager;
		//GPSData gpsData;
		//BTData btData;

		public Location()
		{
			var peripheralDelegate = new PeripheralManagerDelegate();
			peripheralManager = new CBPeripheralManager(peripheralDelegate, DispatchQueue.DefaultGlobalQueue);

			locationManager = new CLLocationManager();
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				locationManager.RequestWhenInUseAuthorization();
			}

			locationManager.DidRangeBeacons += HandleDidRangeBeacons;
            locationManager.LocationsUpdated += HandleLocationsUpdated;
			//gpsData = new GPSData();
			//btData = new BTData();

		}

        public void StartBeacon(string beaconUUID, string beaconID)
        {
			var uuid = new NSUuid(beaconUUID);
			var power = new NSNumber(-59.0d);

			if (peripheralManager.State < CBPeripheralManagerState.PoweredOn)
			{
				new UIAlertView("Bluetooth must be enabled", "To configure your device as a beacon", null, "OK", null).Show();
				return;
			}

			CLBeaconRegion region = new CLBeaconRegion(uuid, beaconID);
			if (region != null)
			{
				peripheralManager.StartAdvertising(region.GetPeripheralData(power));
			}
		}

        public void StopBeacon()
        {
			peripheralManager.StopAdvertising();
        }

        public void StartBeaconRanging(string beaconUUID, string beaconID)
        {
			var uuid = new NSUuid(beaconUUID);

			CLBeaconRegion region = new CLBeaconRegion(uuid, beaconID);
			locationManager.StartRangingBeacons(region);
			locationManager.DesiredAccuracy = 10;
			locationManager.StartUpdatingLocation();

		}

        public void StopBeaconRangin()
        {
            throw new NotImplementedException();
        }

		void HandleDidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
		{
			foreach (CLBeacon beacon in e.Beacons)
			{
				var btData = new BTData();
				btData.BeaconUUID = beacon.ProximityUuid.ToString();
				btData.BeaconID = beacon.Proximity.ToString();
				btData.Distance = beacon.Accuracy;
				collitionDetected(this, btData);
			}
		}
        private void HandleLocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
			var gpsData = new GPSData();
			gpsData.Latitude = e.Locations[0].Coordinate.Latitude;
			gpsData.Longitude = e.Locations[0].Coordinate.Longitude;
			positionChanged(this, gpsData);
        }

		public void StopBeaconRanging()
		{
			throw new NotImplementedException();
		}

		public void StartLocationTracking()
		{
			throw new NotImplementedException();
		}

		public void StopLocationTracking()
		{
			throw new NotImplementedException();
		}
	}
}
