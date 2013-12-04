// -----------------------------------------------------------------------
// <copyright file="Enums.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;

public enum EVisibility
{ 
    Public = 0,
    Unlisted =1,
    Private = 2
}
public enum ExpireData
{ 
    [Description("N")]
    Never, 
    [Description("10M")]
    tenMin,
[Description("1H")] 
    Hour, 
[Description("1D")]
    Day, 
[Description("1W")]
    oneWeek, 
[Description("2W")]
    twoWeek, 
[Description("1M")]
    Month 
}