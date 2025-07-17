using System;
using System.Linq;
using System.Reflection;

namespace Domain.Shared.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum genericEnum)
    {
        Type enumType = genericEnum.GetType();
        MemberInfo[] memberInformation = enumType.GetMember(genericEnum.ToString());
        
        var attributes = memberInformation[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
        if (attributes.Length != 0)
        {
            return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
        }
        
        return genericEnum.ToString();
    }
}