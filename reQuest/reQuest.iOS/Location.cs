using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reQuest.Interfaces;
using CoreBluetooth;
using CoreLocation;
using CoreFoundation;
using MultipeerConnectivity;
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

		NSUuid uuid;
		NSNumber major;
		NSNumber minor;
		NSNumber power;

		CBPeripheralManager peripheralManager;
		NSNumberFormatter numberFormatter;

		public Location()
		{
			var peripheralDelegate = new PeripheralManagerDelegate();
			peripheralManager = new CBPeripheralManager(peripheralDelegate, DispatchQueue.DefaultGlobalQueue);
			numberFormatter = new NSNumberFormatter()
			{
				NumberStyle = NSNumberFormatterStyle.Decimal
			};
			//uuid = Defaults.DefaultProximityUuid;
			//power = Defaults.DefaultPower;
		}

		public void StartBeacon(string beaconUUID, string beaconID)
        {
			uuid = new NSUuid(beaconUUID);
			power = new NSNumber(-59.0d);

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
            throw new NotImplementedException();
        }

        public void StopTrackDistance()
        {
            throw new NotImplementedException();
        }
    }
}
