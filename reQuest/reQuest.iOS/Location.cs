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

    public class Location : ILocation
    {
        public event EventHandler<ILocationData> collitionDetected;
        public event EventHandler<ILocationData> distanceChanged;



		CBPeripheralManager peripheralManager;
		CLLocationManager locationManager;

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

        public void StartTrackDistance(string beaconUUID, string beaconID)
        {
			var uuid = new NSUuid(beaconUUID);

			CLBeaconRegion region = new CLBeaconRegion(uuid, beaconID);
			locationManager.StartRangingBeacons(region);
            locationManager.StartUpdatingLocation();

		}

        public void StopTrackDistance()
        {
            throw new NotImplementedException();
        }

		void HandleDidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
		{
			foreach (CLBeacon beacon in e.Beacons)
			{
				var locationData = new LocationData()
				{
					BeaconUUID = beacon.ProximityUuid.ToString(),
					BeaconID = beacon.Proximity.ToString(),
					Distance = beacon.Accuracy
				};
				distanceChanged(this, locationData);
			}
		}
        private void HandleLocationsUpdated(object sender, CLLocationsUpdatedEventArgs e)
        {
            var locationData = new LocationData()
            {
                Latitude = e.Locations[0].Coordinate.Latitude,
                Longitude = e.Locations[0].Coordinate.Longitude
            };
            distanceChanged(this, locationData);
        }

    }
}
