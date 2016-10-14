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

namespace reQuest.iOS
{
    public class Location : ILocation
    {
        public event EventHandler<ILocationData> collitionDetected;
        public event EventHandler<ILocationData> distanceChanged;

        CBPeripheralManager peripheralMgr;
        //BTPeripheralDelegate peripheralDelegate;
        CLLocationManager locationMgr;
        CLProximity previousProximity;
        CLBeaconRegion beaconRegion;

        public void StartBeacon(string beaconUUID, string beaconID)
        {
            

        }

        public void StopBeacon()
        {
            throw new NotImplementedException();
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
