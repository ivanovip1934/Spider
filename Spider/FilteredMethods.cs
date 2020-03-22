using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Spider
{
    class FilteredMethods
    {
        public string ProcessorName { get; set; }
        public string PatternProcessorModel { get; set; }
        public string MainBoardModel { get; set; }
        public string PatternMainBoardModel { get; set; }
        public int TotalSizeRAM { get; set; }
        public Dictionary<string, int> RangeSizeRAM { get; set; }
        public int SizeOneModuleRAM { get; set; }
        public bool ExistsSSD { get; set; }
        public Dictionary<string, (int Min, int Max)> DicSizeSSD { get; private set; }
        public string SizeSSD { get; set; }
        public Dictionary<string, string> DicVersionOS { get; private set; }
        public string VersionOS { get; set; }
        public string BuildOS { get; set; }
        public bool IsX64OS { get; set; }
        public Dictionary<string, DateTime> RangeInstallDateOS { get; set; }



        public FilteredMethods()
        {
            this.RangeSizeRAM = new Dictionary<string, int>() {
                { "MinSizeRAM", 0 } ,
                { "ManSizeRAM", 0 },
            };
            this.DicSizeSSD = new Dictionary<string, (int, int)>() {
                {"120-128",(111,119) },
                {"240-256",(223,238) },
                {"480-512",(447,476) },
                {"960-1000",(894,931) }
            };

            this.DicVersionOS = new Dictionary<string, string>()
            {
                { "Windows 7", "6.1"},
                { "Windows 10", "10"}
            };

            this.RangeInstallDateOS = new Dictionary<string, DateTime>() {
                { "StartInstallDateOS", DateTime.Now},
                { "EndInstallDateOS", DateTime.Now}
            };

        }
        private delegate bool IsSame(ComputerInfo computerInfo, object parameterforSearch);

        private bool GeneralFilter(ComputerInfo compinfo, ref bool flag, object parameterforSearch, IsSame issame)
        {
            if (flag == false)
                return flag;
            if (compinfo == null)
            {
                flag = false;
                return flag;
            }
            if (parameterforSearch is String)
            {
                if ((string)parameterforSearch == String.Empty)
                {
                    flag = true;
                    return flag;
                }
                else if (issame(compinfo, parameterforSearch))
                {
                    flag = true;
                    return flag;
                }
            }
            else
            {
                if (issame(compinfo, parameterforSearch))
                {
                    flag = true;
                    return flag;
                }
            }
            flag = false;
            return flag;
        }

        private bool SearchbyCpuName(ComputerInfo computerInfo, object cpuName)
        {
            if (cpuName is string)
                return computerInfo.CPU.Name == (string)cpuName;
            return false;
        }
        private bool SearchbyPatterCPU(ComputerInfo computerInfo, object patternCpuName)
        {
            if (patternCpuName is string)
            {
                Regex rgx = new Regex((string)patternCpuName, RegexOptions.IgnoreCase);
                if (rgx.IsMatch(computerInfo.CPU.Name))
                    return true;
            }
            return false;
        }
        private bool SearchbyMainBoadName(ComputerInfo computerInfo, object mainBoardName)
        {
            if (mainBoardName is string)
                return computerInfo.MainBoard.Product == (string)mainBoardName;
            return false;
        }
        private bool SearchbyPatterMainBoard(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is string)
            {
                Regex rgx = new Regex((string)patternMainBoardName, RegexOptions.IgnoreCase);
                if (rgx.IsMatch(computerInfo.MainBoard.Product))
                    return true;
            }
            return false;
        }

        private bool SearchByTotalSizeRAM(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is int)
                //this.labelTab2ShowRAMTotal.Text = $"Total: {pc.Memory.Sum(x => x.Capacity)} GB.";
                return computerInfo.Memory.Sum(x => x.Capacity) == (int)patternMainBoardName;
            return false;
        }

        private bool SearchByRangeSizeRAM(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is Dictionary<string, int>)
                if (computerInfo.Memory.Sum(x => x.Capacity) >= ((Dictionary<string, int>)patternMainBoardName)["MinSizeRAM"] &&
                    computerInfo.Memory.Sum(x => x.Capacity) <= ((Dictionary<string, int>)patternMainBoardName)["MaxSizeRAM"])
                    return true;
            return false;
        }

        private bool SearchByExistsSSD(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is bool)
                return computerInfo.Storage.Any(x => x.IsSSD) == (bool)patternMainBoardName;
            return false;
        }
        private bool SearchBySizeSSD(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is string)
            {
                return computerInfo.Storage.Where(x => x.IsSSD)
                    .Any(x => x.Size >= (DicSizeSSD[(string)patternMainBoardName]).Min 
                    && x.Size <= (DicSizeSSD[(string)patternMainBoardName]).Max);
            }
            return false;
        }

        private bool SearchBySizeOneModuleRAM(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is int)
                return computerInfo.Memory.Any(x => x.Capacity == (int)patternMainBoardName);
            return false;
        }

        private bool SearchByVersionOS(ComputerInfo computerInfo, object VersionOS)
        {
            if (VersionOS is string)
                return computerInfo.OS.Version == this.DicVersionOS[(string)VersionOS];
            return false;
        }

        private bool SearchByBuildOS(ComputerInfo computerInfo, object BuildOS)
        {
            if (BuildOS is string)
                return computerInfo.OS.Build == (string)BuildOS;
            return false;
        }

        private bool SearchByBitOS(ComputerInfo computerInfo, object BuildOS)
        {
            if (BuildOS is bool)
                return computerInfo.OS.IsX64 == (bool)BuildOS;
            return false;
        }

        private bool SearchByInstallDateOS(ComputerInfo computerInfo, object rangeInstallDate)
        {
            if (rangeInstallDate is Dictionary<string, DateTime>) {
                Dictionary<string, DateTime> dicInstallDate = rangeInstallDate as Dictionary<string, DateTime>;                
                if (computerInfo.OS.InstallDate.CompareTo(dicInstallDate["StartInstallDateOS"])>=0 
                    && computerInfo.OS.InstallDate.CompareTo(dicInstallDate["EndInstallDateOS"])<=0)
                    return true;
                return false;
            }
            return false;
        }

        public bool FilterByNameCPU(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.ProcessorName, SearchbyCpuName);
        }
        public bool FilterByPatternCPU(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.PatternProcessorModel, SearchbyPatterCPU);
        }
        public bool FilterByModelMainBoard(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.MainBoardModel, SearchbyMainBoadName);
        }

        public bool FilterByPatternMainBoard(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.PatternMainBoardModel, SearchbyPatterMainBoard);
        }

        public bool FilterByTotalSizeRAM(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.TotalSizeRAM, SearchByTotalSizeRAM);
        }
        public bool FilterByRangeSizeRAM(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.RangeSizeRAM, SearchByRangeSizeRAM);
        }

        public bool FilterBySizeOneModuleRAM(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.SizeOneModuleRAM, SearchBySizeOneModuleRAM);
        }

        public bool FilterByExistsSSD(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.ExistsSSD, SearchByExistsSSD);
        }

        public bool FilterBySizeSSD(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.SizeSSD, SearchBySizeSSD);
        }

        public bool FilterByVersionOS(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.VersionOS, SearchByVersionOS);
        }

        public bool FilterByBuildOS(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.BuildOS, SearchByBuildOS);
        }

        public bool FilterByBitOS(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.IsX64OS, SearchByBitOS);
        }

        public bool FilterByInstallDateOS(ComputerInfo compinfo, ref bool flag)
        {
            return GeneralFilter(compinfo, ref flag, this.RangeInstallDateOS, SearchByInstallDateOS);
        }
    }
}
