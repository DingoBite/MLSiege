using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.General.StaticUtils.Enums
{
    public static class EnumInfo<TEnum, TUnderlying> where TEnum : Enum
    {
        public static readonly Type EnumType = typeof(TEnum);
        public static readonly IEnumerable<TEnum> Values = Enum.GetValues(EnumType).Cast<TEnum>();
        public static readonly IEnumerable<string> Names = Enum.GetNames(EnumType);
        public static readonly IEnumerable<TUnderlying> UnderlyingValues = Enum.GetValues(EnumType).Cast<TUnderlying>();
        public static readonly IReadOnlyDictionary<TEnum, TUnderlying> EnumToValue;
        public static readonly IReadOnlyDictionary<TUnderlying, TEnum> ValueToEnum;
        public static readonly IReadOnlyDictionary<string, TUnderlying> NameToValue;

        static EnumInfo()
        {
            var enumToValueDictionary = new Dictionary<TEnum, TUnderlying>();
            var valueToEnumDictionary = new Dictionary<TUnderlying, TEnum>();
            var nameToValueDictionary = new Dictionary<string, TUnderlying>();

            var valuesArray = Values.ToArray();
            var namesArray = Names.ToArray();

            var i = 0;
            foreach (var underlyingValue in UnderlyingValues)
            {
                enumToValueDictionary.Add(valuesArray[i], underlyingValue);
                valueToEnumDictionary.Add(underlyingValue, valuesArray[i]);
                nameToValueDictionary.Add(namesArray[i], underlyingValue);
                i++;
            }

            EnumToValue = enumToValueDictionary;
            ValueToEnum = valueToEnumDictionary;
            NameToValue = nameToValueDictionary;
        }
    }
}