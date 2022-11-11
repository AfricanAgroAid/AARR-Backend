using System.ComponentModel;

namespace Domain.Enums;

public enum CropType
{
    [Description("Dry Season")]
    DrySeason = 1,
    [Description("Rainy Season")]
    RainSeason
}
