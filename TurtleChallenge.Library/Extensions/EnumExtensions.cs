namespace TurtleChallenge.Library.Extensions
{
    using System;

    public static class EnumExtensions
    {
        public static T CycleNext<T>(this T sourceEnum) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"{nameof(T)} is not an Enum");

            var enumValues = (T[])Enum.GetValues(sourceEnum.GetType());
            var currentValue = Array.IndexOf(enumValues, sourceEnum) + 1;
            return enumValues.Length==currentValue ? enumValues[0] : enumValues[currentValue];            
        }
    }
}
