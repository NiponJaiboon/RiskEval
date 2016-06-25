using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSabaya
{
    public enum RoundingTarget
    {
        RoundAmount,  //the amount to calculate 
        RoundFee //Round the result of the calculation
    }

    public enum RoundingMethod
    {
        //None,
        Mathmatics, //Round the middle up
        Up,  //that will be use to calculate 
        Down, //Round the result of the calculation
    }

    public abstract class Rounding<T>
    {
    
        #region persistent

        /// <summary>
        /// True = round amount that will be applied
        /// </summary>
        public virtual RoundingTarget Target { get; set; }

        /// <summary>
        /// Up, down, math
        /// </summary>
        public virtual RoundingMethod Method { get; set; }

        /// <summary>
        /// .01 = two digit after decimal point 
        /// .1 = one digit after decimal point 
        /// 1 = round to integer
        /// 10 = round to the nearest tenth
        /// 100 = round to the nearest hundred
        /// 1000 = round to the nearest thousand
        /// </summary>
        public virtual decimal Precision{ get; set; }

        #endregion

        public abstract T Round(T amount);
    }
}
