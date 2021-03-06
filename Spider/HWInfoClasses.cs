﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spider
{
    [Serializable]
    public class ComputerInfo
    {
        public string Name { get; set; }
        public CpuInfo CPU { get; set; }
        public MainBoardInfo MainBoard { get; set; }
        public List<RAMInfo> Memory { get; set; }
        public List<DiskInfo> Storage { get; set; }
        public List<PartitionInfo> Partitions { get; set; }
        public OperationSystem OS { get; set; }
        public List<MonitorInfo> Monitors { get; set; }
        public DateTime DateCollectedInfo { get; set; }

        public ComputerInfo() { }


    }

    [Serializable]
    public class RAMInfo
    {
        public string Tag { get; set; }
        public string PartNumber { get; set; }
        public int Capacity { get; set; }

        public RAMInfo()
        {

        }        
    }

    [Serializable]
    public class CpuInfo : IIsSame
    {
        public string Name { get; set; }
        public string ProcessorId { get; set; }
        public CpuInfo()
        {

        }

        public bool IsSame(object obj)
        {
            if (obj is CpuInfo)
            {
                CpuInfo cpu = obj as CpuInfo;
                if (this.Name == cpu.Name)
                {
                    return true;
                }
                else
                    return false;

            }
            MessageBox.Show("this obj not a CPU");

            return false;
        }

        public override string ToString()
        {
            return $"{this.Name}\n" +
                $"{this.ProcessorId}\n";
        }

    }

    [Serializable]
    public class MainBoardInfo : IIsSame
    {
        public string Product { get; set; }
        public string SerialNumber { get; set; }
        public string Manufacturer { get; set; }
        public string SMBIOSBIOSVersion { get; set; }
        public string[] MAC { get; set; }
        public MainBoardInfo()
        {

        }

        public bool IsSame(object obj)
        {
            if (obj is MainBoardInfo)
            {
                MainBoardInfo mb = obj as MainBoardInfo;
                if (this.Product == mb.Product &&
                    this.Manufacturer == mb.Manufacturer)
                {
                    return true;
                }
                else
                    return false;

            }
            MessageBox.Show("this obj not a MainBoard");

            return false;
        }
        public bool IsSameMAC(MainBoardInfo mainBoardInfo) {
            foreach (string mac in this.MAC) {
                if (mainBoardInfo.MAC.Contains(mac))
                    return true;
            }
            return false;
        
        }

        public override string ToString()
        {
            return $"{this.Product}\n" +
                $"{this.Manufacturer}\n" +
                $"{this.SerialNumber}\n" +
                $"{this.SMBIOSBIOSVersion}";
        }
    }

    public class DiskInfo : IComparable, IIsSame
    {
        public string Model { get; set; }
        public int Index { get; set; }
        public int Size { get; set; }
        public bool IsSSD { get; set; }
      

        public DiskInfo() { }

        public int CompareTo(object obj)
        {
            return this.Index.CompareTo(((DiskInfo)obj).Index);
        }

        public bool IsSame(object obj)
        {
            if (obj is DiskInfo)
            {
                DiskInfo diskInfo = obj as DiskInfo;
                if (this.Model == diskInfo.Model)
                {
                    return true;
                }
                else
                    return false;

            }
            MessageBox.Show("this obj not a Disc");

            return false;
        }

        public override string ToString()
        {
            return $"{this.Model}\n" +
                $"{this.Size}\n" +
                $"{this.IsSSD}";
        }
    }


    [Serializable]
    public class PartitionInfo
    {
        public string Name { get; set; }
        public bool IsSystem { get; set; }
        public int FullSize { get; set; }
        public int AvailableFreeSpace { get; set; }
        public int IndexDisc { get; set; }


        public PartitionInfo()
        {

        }

    }

    [Serializable]
    public class OperationSystem : IIsSame
    {
        public bool IsX64 { get; set; }
        public string Version { get; set; }
        public string Build { get; set; }
        public string ProductName { get; set; }
        public DateTime InstallDate { get; set; }

        public OperationSystem()
        {

        }

        public bool IsSame(object obj)
        {
            if (obj is OperationSystem)
            {
                OperationSystem os = obj as OperationSystem;
                if (this.ProductName == os.ProductName &&
                    this.Version == os.Version &&
                    this.Build == os.Build &&
                    this.IsX64 == os.IsX64)
                {
                    return true;
                }
                else
                    return false;

            }
            MessageBox.Show("this obj not a OperationSystem");

            return false;
        }

        public override string ToString() {
            return $"{this.ProductName}\n" +
                $"{this.Version}\n" +
                $"{this.Build}\n" +
                $"{this.IsX64}";
        }
    }
    [Serializable]
    public class MonitorInfo : IIsSame
    {

        public string ID { get; set; }
        public Manufacturers Manufacturer { get; set; }
        public string Model { get; set; }
        public string PanelSize { get; set; }
        public string TrueResolution { get; set; }

        public string SerailNumber { get; set; }



        public MonitorInfo() { }

        public bool IsSame(object obj)
        {
            if (obj is MonitorInfo)
            {
                MonitorInfo monitor = obj as MonitorInfo;
                if (this.Manufacturer == monitor.Manufacturer &&
                    this.Model == monitor.Model) 
                {
                    return true;
                }
                else
                    return false;

            }
            MessageBox.Show("this obj not a MonitorInfo");
            return false;
        }

    }
    public enum Manufacturers
    {
        AOC = 1,
        Acer = 2,
        Asrock = 3,
        Asus = 4,
        BenQ = 5,
        Dell = 6,
        Gigabyte = 7,
        HP = 8,
        Iiyama = 9,
        Lenovo = 10,
        LG = 11,
        MSI = 12,
        Pegatron = 13,
        Philips = 14,
        Samsung = 15,
        ViewSonic = 16,
        Unknown = 17,
        NEC = 18
    }
}
