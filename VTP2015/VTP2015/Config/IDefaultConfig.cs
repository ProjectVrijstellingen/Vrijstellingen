using System;

namespace VTP2015.Config
{
    interface IDefaultConfig
    {
        string InfoMailFrequency { get; set; }
        string WarningMailFrequency { get; set; }
        string StartVrijstellingDayMonth { get; set; }
        string EindeVrijstellingDayMonth { get; set; }
    }
}
