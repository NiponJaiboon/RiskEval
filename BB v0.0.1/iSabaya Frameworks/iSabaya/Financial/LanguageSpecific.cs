using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public static class LanguageSpecific
    {
        private static String[] thaiNumberWords = new String[] { "", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
        private static String[] thaiUnitNumberWords = new String[] { "", "เอ็ด", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
        private static String[] thaiTenthNumberWords = new String[] { "", "", "ยี่", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
        private static String[] thaiLessThanMillionNumberPositionWords = new String[] { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน" };
        private static String[] thaiMillionUpNumberPositionWords = new String[] { "ล้าน", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน" };

        public static String ToThaiWords(this Money money)
        {
            decimal amount = money.Amount;

            StringBuilder moneyInWordBuilder = new StringBuilder();
            long fractionAmount = (long)(Math.Floor(amount * 100m) % 100m);
            long wholeAmount = (long)Math.Floor(amount);

            //stangs
            if (fractionAmount > 0)
            {
                moneyInWordBuilder.Append("สตางค์");
                ToThaiWords(fractionAmount, ref moneyInWordBuilder);
            }

            //bahts
            if (wholeAmount > 0)
            {
                if (fractionAmount > 0)
                    moneyInWordBuilder.Insert(0, "บาท");
                else
                    moneyInWordBuilder.Insert(0, "บาทถ้วน");
                ToThaiWords(wholeAmount, ref moneyInWordBuilder);
            }

            if (moneyInWordBuilder.Length == 0)
                return "ศูนย์บาทศูนย์สตางค์";
            else
                return moneyInWordBuilder.ToString();
        }

        private static void ToThaiWords(long amount, ref StringBuilder bathInThai)
        {
            int position = 0;
            long digit;
            String[] thaiNumberPositionWords = thaiLessThanMillionNumberPositionWords;

            while (amount > 0)
            {
                digit = amount % 10;
                amount = amount / 10;

                if (digit > 0)
                {
                    bathInThai.Insert(0, thaiNumberPositionWords[position]);
                    if (position == 0)
                    {
                        if (amount > 0 && amount % 10 > 0)
                            bathInThai.Insert(0, thaiUnitNumberWords[digit]);
                        else
                            bathInThai.Insert(0, thaiNumberWords[digit]);
                    }
                    else if (position == 1)
                        bathInThai.Insert(0, thaiTenthNumberWords[digit]);
                    else
                        bathInThai.Insert(0, thaiNumberWords[digit]);
                }

                if (position == 5)
                {
                    thaiNumberPositionWords = thaiMillionUpNumberPositionWords;
                    position = 0;
                }
                else
                    ++position;
            }
        }

        private static String[] englishLessThanTwentyWords = new String[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "forteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen", };

        private static String[] englishUnitWords = new String[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "" };

        private static String[] englishTenthWords = new String[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eight", "nine", "" };

        private static String[] englishPositionWords = new String[] { "", "", "", "hundred", "thousand", "", "", "million", "", "", "billion" };

        public static String ToEnglishWords(this Money money)
        {
            decimal amount = money.Amount;

            StringBuilder moneyInThaiBuilder = new StringBuilder();
            long stangAmount = (long)(Math.Floor(amount * 100m) % 100m);
            long bahtAmount = (long)Math.Floor(amount);

            //stangs
            if (stangAmount > 0)
            {
                moneyInThaiBuilder.Append("สตางค์");
                ToThaiWords(stangAmount, ref moneyInThaiBuilder);
            }

            //bahts
            if (bahtAmount > 0)
            {
                if (stangAmount > 0)
                    moneyInThaiBuilder.Insert(0, "บาท");
                else
                    moneyInThaiBuilder.Insert(0, "บาทถ้วน");
                ToThaiWords(bahtAmount, ref moneyInThaiBuilder);
            }

            if (moneyInThaiBuilder.Length == 0)
                return "ศูนย์บาทศูนย์สตางค์";
            else
                return moneyInThaiBuilder.ToString();
        }

        private static void ToEnglishWords(long amount, ref StringBuilder bathInThai)
        {
            int position = 0;
            long digit;
            String[] thaiNumberPositionWords = thaiLessThanMillionNumberPositionWords;

            while (amount > 0)
            {
                digit = amount % 10;
                amount = amount / 10;

                if (digit > 0)
                {
                    bathInThai.Insert(0, thaiNumberPositionWords[position]);
                    if (position == 0)
                    {
                        if (amount > 0 && amount % 10 > 0)
                            bathInThai.Insert(0, thaiUnitNumberWords[digit]);
                        else
                            bathInThai.Insert(0, thaiNumberWords[digit]);
                    }
                    else if (position == 1)
                        bathInThai.Insert(0, thaiTenthNumberWords[digit]);
                    else
                        bathInThai.Insert(0, thaiNumberWords[digit]);
                }

                if (position == 5)
                {
                    thaiNumberPositionWords = thaiMillionUpNumberPositionWords;
                    position = 0;
                }
                else
                    ++position;
            }
        }
    }
}
