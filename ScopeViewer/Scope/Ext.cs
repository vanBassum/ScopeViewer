﻿using System;

namespace ScopeViewer.Scope
{
    public static class Ext
    {

        public static int LowestDiv(this int num)
        {
            int i = 0;
            for (i = 2; i < num; i++)
                if (num % i == 0)
                    break;
            return i;
        }

        public static string ToHumanReadable(this double number, int digits = 3)
        {
            string smallPrefix = "mµnpf";
            string largePrefix = "kMGT";

            int thousands = (int)Math.Log(Math.Abs(number), 1000);

            if (Math.Log(Math.Abs(number), 1000) < 0)
                thousands--;

            if (number == 0)
                thousands = 0;

            double scaledNumber = number * Math.Pow(1000, -thousands);

            int places = Math.Max(0, digits - (int)Math.Log10(scaledNumber));
            string s = scaledNumber.ToString("F" + places.ToString());



            if (thousands > 0)
                if (thousands < largePrefix.Length)
                    s += largePrefix[thousands - 1];

            if (thousands < 0)
                if (Math.Abs(thousands) < largePrefix.Length)
                    s += smallPrefix[Math.Abs(thousands) - 1];
            return s;
        }
    }

}
