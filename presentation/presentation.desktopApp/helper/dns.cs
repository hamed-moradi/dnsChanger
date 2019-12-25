﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace presentation.desktopApp.helper {
    public class DNS {
        public enum NetworkAdapterConfigurationReturnValue: int {
            SuccessfulCompletionNoRebootRequired = 0,
            SuccessfulCompletionRebootRequired = 1,
            MethodNotSupportedOnThisPlatform = 64,
            UnknownFailure = 65,
            InvalidSubnetMask = 66,
            AnErrorOccurredWhileProcessingAnInstanceThatWasReturned = 67,
            InvalidInputParameter = 68,
            MoreThan5GatewaysSpecified = 69,
            InvalidIPAddress = 70,
            InvalidGatewayIPAddress = 71,
            AnErrorOccurredWhileAccessingTheRegistryForTheRequestedInformation = 72,
            InvalidDomainName = 73,
            InvalidHostName = 74,
            NoPrimarySecondaryWINSServerDefined = 75,
            InvalidFile = 76,
            InvalidSystemPath = 77,
            FileCopyFailed = 78,
            InvalidSecurityParameter = 79,
            UnableToConfigureTCPIPService = 80,
            UnableToConfigureDHCPService = 81,
            UnableToRenewDHCPLease = 82,
            UnableToReleaseDHCPLease = 83,
            IPNotEnabledOnAdapter = 84,
            IPXNotEnabledOnAdapter = 85,
            FrameNetworkNumberBoundsError = 86,
            InvalidFrameType = 87,
            InvalidNetworkNumber = 88,
            DuplicateNetworkNumber = 89,
            ParameterOutOfBounds = 90,
            AccessDenied = 91,
            OutOfMemory = 92,
            AlreadyExists = 93,
            PathfileOrObjectNotFound = 94,
            UnableToNotifyService = 95,
            UnableToNotifyDNSService = 96,
            InterfaceNotConfigurable = 97,
            NotSllDHCPLeasesCouldBeReleasedRenewed = 98,
            DHCPNotEnabledOnAdapter = 100,
            Other = 101
        }
        public static NetworkAdapterConfigurationReturnValue Set(string netInterfaceDescription, string[] ips) {
            var managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var classInstances = managementClass.GetInstances();
            foreach(ManagementObject manageObj in classInstances) {
                if((bool)manageObj["IPEnabled"]) {
                    if(manageObj["Description"].ToString().ToLower().Equals(netInterfaceDescription.ToLower())) {
                        var dnsServerSearchOrder = manageObj.GetMethodParameters("SetDNSServerSearchOrder");
                        if(dnsServerSearchOrder != null) {
                            dnsServerSearchOrder["DNSServerSearchOrder"] = ips;
                            var result = manageObj.InvokeMethod("SetDNSServerSearchOrder", dnsServerSearchOrder, null);
                            return (NetworkAdapterConfigurationReturnValue)int.Parse(result["returnvalue"].ToString());
                        }
                    }
                }
            }
            return NetworkAdapterConfigurationReturnValue.Other;
        }


    }
}
