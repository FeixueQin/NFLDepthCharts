using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum PositionAbbre
{
    [EnumMember(Value = "LWR")]
    LWR = 1,
    
    [EnumMember(Value = "RWR")]
    RWR = 2,
    
    [EnumMember(Value = "SF")]
    SF = 3,
    
    [EnumMember(Value = "LT")]
    LT = 4,
    
    [EnumMember(Value = "LG")]
    LG = 5,
    
    [EnumMember(Value = "C")]
    C = 6,
    
    [EnumMember(Value = "RG")]
    RG = 7,
    
    [EnumMember(Value = "RT")]
    RT = 8,
    
    [EnumMember(Value = "TE")]
    TE = 9,
    
    [EnumMember(Value = "QB")]
    QB = 10,
    
    [EnumMember(Value = "RB")]
    RB = 11,
}