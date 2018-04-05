using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace cspClient.utils
{
    class Utils
    {
        public static String getGUID()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }


        public static String getMachineUUID() {

            ManagementClass clsMgtClass = new ManagementClass("Win32_ComputerSystemProduct");
            ManagementObjectCollection colMgtObjCol = clsMgtClass.GetInstances();
            foreach (ManagementObject objMgtObj in colMgtObjCol)
            {
                return objMgtObj.Properties["UUID"].Value.ToString();
            }
            return getGUID();
        }

    }
}
