﻿using System;
namespace ysh.core
{
    public static class LengthUnitConverter
    {
        /// <summary>
        /// 将内部英制单位转换为公制长度单位
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="type">The type of unit to convert to.</param>
        /// <param name="decimals">The decimal spaces precision.</param>
        /// <returns></returns>
        public static double ConvertToMetric(double value, LengthUnitType type, int decimals)
        {
            switch (type)
            {
                case LengthUnitType.milimeter:
                    return Math.Round(value * 304.8, decimals);
                case LengthUnitType.centimeter:
                    return Math.Round(value * 30.48, decimals);
                case LengthUnitType.decimeter:
                    return Math.Round(value * 3.048, decimals);
                case LengthUnitType.meter:
                    return Math.Round(value * 0.3048, decimals);
                case LengthUnitType.kilometer:
                    return Math.Round(value * 0.0003048, decimals);
                default:
                    return value;
            }
        }


    }
}
