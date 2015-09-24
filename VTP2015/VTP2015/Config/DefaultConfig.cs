
using System;

namespace VTP2015.Config
{
    public class DefaultConfig : IDefaultConfig
    {
        public string InfoMailFrequency { get; set; }
        public string WarningMailFrequency { get; set; }
        public string StartVrijstellingDayMonth { get; set; }
        public string EindeVrijstellingDayMonth { get; set; }
    }
}