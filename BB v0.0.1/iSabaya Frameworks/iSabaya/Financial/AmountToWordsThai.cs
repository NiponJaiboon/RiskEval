using System;
using System.Collections.Generic;
using System.Text;
/**
 *  __
 * /  \___
 * \ THAI \  Nattapong Manchupaiboon
 * < LAND_/  iSbaya Co., Ltd.
 *  \ /\|    THAILAND
 *  //
 *  \\_
 *   \_\     
 */
namespace iSabaya
{
    public class AmountToWordsThai
    {
        private static String[] unit = {
        "",
        "สิบ",
        "ร้อย",
        "พัน",
        "หมื่น",
        "แสน",
        "ล้าน"
    };

    private static String[] number = {
        "",
        "หนึ่ง",
        "สอง",
        "สาม",
        "สี่",
        "ห้า",
        "หก",
        "เจ็ด",
        "แปด",
        "เก้า"
    };

    private static  String one = "เอ็ด";
    private static String two = "ยี่";
    private static  String bath = "บาท";
    private static  String only = "ถ้วน";
    private static  String subUnit = "สตางค์";

    private static String getThaiString(String unit, int luk){
        int i = int.Parse(unit);
        switch(i){
            case 1 : return number[1];
            case 2 : return (luk == 1) ? two : number[2];
            case 3 : return number[3];
            case 4 : return number[4];
            case 5 : return number[5];
            case 6 : return number[6];
            case 7 : return number[7];
            case 8 : return number[8];
            case 9 : return number[9];
            default : return "";
        }
    }

    /**
     * limit 999,999,999.99 baht
     * @param amount as total of money
     * @return String in thai
     */
    public static String getAmountWords(decimal amount){
        StringBuilder stringBuffer = new StringBuilder();
        String loylan = "";
        String ziplan = "";
        String lan = "";
        String san = "";
        String mean = "";
        String pan = "";
        String loy = "";
        String zip = "";
        String noy = "";

        String digitZip = "";
        String digitNoy = "";

        String tempAmount = amount.ToString("000000000.00");
        //int i = amount.ToString("000000000.00").Length;

        //switch(i){
        //    case 9:  tempAmount = amount.ToString(); break;
        //    case 8:  tempAmount = "0" + amount; break;
        //    case 7:  tempAmount = "00" + amount; break;
        //    case 6:  tempAmount = "000" + amount; break;
        //    case 5:  tempAmount = "0000" + amount; break;
        //    case 4:  tempAmount = "00000" + amount; break;
        //    case 3:  tempAmount = "000000" + amount; break;
        //    case 2:  tempAmount = "0000000" + amount; break;
        //    case 1:  tempAmount = "00000000" + amount; break;

        //}
        String digit;
        int i1 = amount.ToString().LastIndexOf(".");
        digit = amount.ToString().Substring(i1+1,2);


        loylan = tempAmount.ToString().Substring(0, 1);
        ziplan = tempAmount.ToString().Substring(1, 1);
        lan = tempAmount.ToString().Substring(2, 1);
        san = tempAmount.ToString().Substring(3, 1);
        mean = tempAmount.ToString().Substring(4, 1);
        pan = tempAmount.ToString().Substring(5, 1);
        loy = tempAmount.ToString().Substring(6, 1);
        zip = tempAmount.ToString().Substring(7, 1);
        noy = tempAmount.ToString().Substring(8, 1);

        digitZip = digit.ToString().Substring(0, 1);
        digitNoy = digit.ToString().Substring(1, 1);


        if(int.Parse(loylan) >= 0){
            if(int.Parse(loylan) != 0)
                stringBuffer.Append(getThaiString(loylan, 0) + unit[2]);
        }
        if(int.Parse(ziplan) >= 0){
            if(int.Parse(ziplan) == 1  && int.Parse(lan) == 0){
                stringBuffer.Append(unit[1] + unit[6]);
            }else if(int.Parse(ziplan) > 1 && int.Parse(lan) == 0){
                stringBuffer.Append(getThaiString(ziplan, 1) + unit[1] + unit[6]);
            }else{
                stringBuffer.Append(ziplan.Equals("1") ? unit[1] : getThaiString(ziplan, 1));
            }
        }
        if(int.Parse(lan) >= 0){
            if(int.Parse(lan) != 0){
                if(lan.Equals("1") && int.Parse(ziplan) > 0)
                    stringBuffer.Append(one + unit[6]);
                else
                    stringBuffer.Append(getThaiString(lan, 0) + unit[6]);
            }
        }
        if(int.Parse(san) >= 0){
            if(int.Parse(san) != 0)
                stringBuffer.Append(getThaiString(san, 0) + unit[5]);
        }
        if(int.Parse(mean) >= 0){
            if(int.Parse(mean) != 0)
                stringBuffer.Append(getThaiString(mean, 0) + unit[4]);
        }
        if(int.Parse(pan) >= 0){
            if(int.Parse(pan) != 0)
                stringBuffer.Append(getThaiString(pan, 0) + unit[3]);
        }
        if(int.Parse(loy) >= 0){
            if(int.Parse(loy) != 0)
                stringBuffer.Append(getThaiString(loy, 0) + unit[2]);
        }
        if(int.Parse(zip) >= 0){
            if(int.Parse(zip) != 0)
                stringBuffer.Append(getThaiString(zip, 1).Equals(number[1]) ? unit[1] : getThaiString(zip, 1) + unit[1]);
        }
        if(int.Parse(noy) >= 0){
                if(noy.Equals("1") && int.Parse(tempAmount.ToString().Substring(0,8)) <= 0){
                    stringBuffer.Append(getThaiString("1", 0));
                }else if(noy.Equals("1") && int.Parse(tempAmount.ToString().Substring(0,8)) > 0){
                    stringBuffer.Append(one);
                }else{
                    stringBuffer.Append(getThaiString(noy, 0));
                }

                if(int.Parse(digit) <= 0)
                    stringBuffer.Append(bath + only);
                else{
                    if(int.Parse(tempAmount.ToString().Substring(0,9)) > 0)
                        stringBuffer.Append(bath);

                    if(int.Parse(digitZip) > 0){
                        stringBuffer.Append(getThaiString(digitZip, 1).Equals(number[1]) ? unit[1] : getThaiString(digitZip, 1) + unit[1]);
                    }
                    if(int.Parse(digitNoy) > 0){
                        //
                        if(digitNoy.Equals("1") && int.Parse(digit.ToString().Substring(0,1)) <= 0){
                            stringBuffer.Append(getThaiString("1", 0));
                        }else if(digitNoy.Equals("1") && int.Parse(digit.ToString().Substring(0,1)) > 0){
                            stringBuffer.Append(one);
                        }else{
                            stringBuffer.Append(getThaiString(digitNoy, 0));
                        }
                        //
                    }
                    stringBuffer.Append(subUnit);
                }
            }else{
                if(int.Parse(tempAmount.ToString().Substring(0,9)) > 0)
                    stringBuffer.Append(bath);
                if(int.Parse(digitZip) > 0){
                    stringBuffer.Append(getThaiString(digitZip, 1).Equals(number[1]) ? unit[1] : getThaiString(digitZip, 1) + unit[1]);
                }
                if(int.Parse(digitNoy) > 0){
                    if(digitNoy.Equals("1") && int.Parse(digit.ToString().Substring(0,1)) <= 0){
                        stringBuffer.Append(getThaiString("1", 0));
                    }else if(digitNoy.Equals("1") && int.Parse(digit.ToString().Substring(0,1)) > 0){
                        stringBuffer.Append(one);
                    }else{
                        stringBuffer.Append(getThaiString(digitNoy, 0));
                    }
                }
                stringBuffer.Append(subUnit);
            }

        return stringBuffer.ToString();
    }


    }
}
