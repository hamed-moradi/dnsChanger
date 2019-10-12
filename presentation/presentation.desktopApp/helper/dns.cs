using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace presentation.desktopApp.helper {
    public class DNS {
        //private static NetworkInterface GetActiveEthernetOrWifiNetworkInterface() {
        //    return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(
        //        a => a.OperationalStatus == OperationalStatus.Up &&
        //        (a.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || a.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
        //        a.GetIPProperties().GatewayAddresses.Any(g => g.Address.AddressFamily.ToString() == "InterNetwork"));
        //}
        //public static void Set(string[] ips) {
        //    var CurrentInterface = GetActiveEthernetOrWifiNetworkInterface();
        //    if(CurrentInterface == null) return;
        //    var objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    var objMOC = objMC.GetInstances();
        //    foreach(ManagementObject objMO in objMOC) {
        //        if((bool)objMO["IPEnabled"]) {
        //            if(objMO["Description"].ToString().Equals(CurrentInterface.Description)) {
        //                var objdns = objMO.GetMethodParameters("SetDNSServerSearchOrder");
        //                if(objdns != null) {
        //                    objdns["DNSServerSearchOrder"] = ips;
        //                    objMO.InvokeMethod("SetDNSServerSearchOrder", objdns, null);
        //                }
        //            }
        //        }
        //    }
        //}

        public static void Set(string netInterfaceDescription, string[] ips) {
            var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var classInstances = managementClass.GetInstances();
            foreach(ManagementObject manageObj in classInstances) {
                if((bool)manageObj["IPEnabled"]) {
                    if(manageObj["Description"].ToString().ToLower().Equals(netInterfaceDescription.ToLower())) {
                        var dnsServerSearchOrder = manageObj.GetMethodParameters("SetDNSServerSearchOrder");
                        if(dnsServerSearchOrder != null) {
                            dnsServerSearchOrder["DNSServerSearchOrder"] = ips;
                            manageObj.InvokeMethod("SetDNSServerSearchOrder", dnsServerSearchOrder, null);
                        }
                    }
                }
            }
        }
    }
}
