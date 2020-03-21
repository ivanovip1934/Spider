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
        public Dictionary<string,int> RangeSizeRAM { get; set; }
        public int SizeOneModuleRAM { get; set; }
       


        public FilteredMethods()
        {
            RangeSizeRAM = new Dictionary<string, int>() {
                { "MinSizeRAM", 0 } ,
                { "ManSizeRAM", 0 },
            };
            
        }
        private delegate bool IsSame(ComputerInfo computerInfo, object parameterforSearch);

        private bool GeneralFilter(ComputerInfo compinfo, ref bool flag, object parameterforSearch,IsSame issame) {
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
            else if (parameterforSearch is bool || parameterforSearch is int || parameterforSearch is Dictionary<string, int>)
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

        private bool SearchbyCpuName(ComputerInfo computerInfo, object cpuName) {
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
            if (patternMainBoardName is Dictionary<string,int>)
                if (computerInfo.Memory.Sum(x => x.Capacity) >= ((Dictionary<string, int>)patternMainBoardName)["MinSizeRAM"] &&
                    computerInfo.Memory.Sum(x => x.Capacity)<= ((Dictionary<string, int>)patternMainBoardName)["MaxSizeRAM"])
                return true;
            return false;
        }

        private bool SearchBySizeOneModuleRAM(ComputerInfo computerInfo, object patternMainBoardName)
        {
            if (patternMainBoardName is int)                
                return computerInfo.Memory.Any(x => x.Capacity == (int)patternMainBoardName);
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

        public bool FilterByPatternMainBoard(ComputerInfo compinfo,ref bool flag)
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
    }
}
