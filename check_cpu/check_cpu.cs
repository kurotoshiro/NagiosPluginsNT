/* check_cpu.cs - CPU utilization check plugin
 * 
 * NagiosPluginsNT - Nagios NRPE plugins for Windows NT
 * Copyright (c) 2009, Michael T. Conigliaro
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License version 2 as 
 * published by the Free Software Foundation.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 */

using System;
using System.Collections.Generic;
using System.Text;
using NagiosPluginsNT;
using Mono.GetOptions;
using System.Management;

namespace check_cpu
{
    class check_cpu
    {
        static void Main(string[] args)
        {
            WmiPluginOptions Options = new WmiPluginOptions();            
            Options.label = "CPU";
            Options.units = "%";
            Options.ProcessArgs(args);      
            
            WmiPlugin plugin = new WmiPlugin(Options);            
            plugin.Connect();
            plugin.Query("Select DeviceID, LoadPercentage From Win32_Processor");                                 
            foreach (ManagementObject mgtObject in plugin.ResultCollection)
            {
                plugin.AppendValue(mgtObject["DeviceID"].ToString(), Convert.ToDecimal(mgtObject["LoadPercentage"].ToString()));
            }            
            plugin.Finish();
        }
    }
}
