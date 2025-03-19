using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CO3109_BE.Controllers.CalcHist
{
    public static class constClass
    {
        public const decimal nk = 1;
        public const decimal nol = 0.993m;
        public const decimal nbr = 0.97m;
        public const decimal nx = 0.91m;
        public const decimal uh = 8;
        public const decimal ux = 2;
        public const decimal u1 = 3.08m;
        public const decimal u2 = 2.6m;
    }
    public class calculatingMethod
    {
        public chapter2Method chapter2;
        public calculatingMethod()
        {
            chapter2 = new chapter2Method();
        }
    }
    public class chapter2Method
    {
        // Method for power
        public decimal ShaftCapacity(decimal F, decimal v)
        {
            return (F * v) / 1000;
        }
        public decimal GeneralEfficiency()
        {
            return constClass.nk * (decimal)Math.Pow((double)constClass.nol, 4) * (decimal)Math.Pow((double)constClass.nbr, 3) * constClass.nx;
        }
        public decimal EquivalentCapacity(decimal T1, decimal T2, decimal t1, decimal t2, decimal Plv)
        {
            return Plv * (decimal)Math.Sqrt((double)((Math.Pow((double)T1, 2) * (double)t1 + Math.Pow((double)T2, 2) * (double)t2) / ((double)t1 + (double)t2)));
        }
        public decimal MinimalCapacity(decimal Peq, decimal n)
        {
            return Peq / n;
        }

        //Method for rotation speed
        public decimal BasicGearRatio(decimal nlv, decimal usb)
        {
            return nlv * usb;
        }
        public decimal NumberOfRotation(decimal v, decimal D)
        {
            return (60000 * v) / ((decimal)Math.PI * D);
        }
        public decimal PreliminaryGearRatio()
        {
            return constClass.uh * constClass.ux;
        }
        // Method for transmission
        public decimal GenTransRatio(decimal ndc, decimal nlv)
        {
            return ndc / nlv;
        }
        public decimal ChainDriveCoef(decimal u)
        {
            return u / (constClass.u1 * constClass.u2);
        }
        //Method for power
        public decimal Pbt(decimal Plv)
        {
            return Plv / constClass.nol;
        }
        public decimal P3(decimal Pbt)
        {
            return Pbt / (constClass.nol * constClass.nx);
        }
        public decimal P2(decimal P3)
        {
            return P3 / (constClass.nol * constClass.nbr);
        }
        public decimal P1(decimal P2)
        {
            return P2 / (constClass.nol * constClass.nbr);
        }
        public decimal Pm(decimal P1)
        {
            return P1 / (constClass.nk);
        }
        //Method for rotation velocity
        public decimal n2(decimal n1)
        {
            return n1 / constClass.u1;
        }
        public decimal n3 (decimal n2)
        {
            return n2 / constClass.u2;
        }
        public decimal nbt (decimal n3, decimal ux)
        {
            return n3 / ux;
        }
        //Method for torque
        public decimal Tm(decimal Pm, decimal ndc)
        {
            return (9.55m * (decimal)Math.Pow(10, 6) * Pm) / ndc;
        }
        public decimal T2(decimal P2, decimal n2)
        {
            return (9.55m * (decimal)Math.Pow(10, 6) * P2) / n2;
        }
        public decimal T3(decimal P3, decimal n3)
        {
            return (9.55m * (decimal)Math.Pow(10, 6) * P3) / n3;
        }
        public decimal Tbt(decimal Pbt, decimal nbt)
        {
            return (9.55m * (decimal)Math.Pow(10, 6) * Pbt) / nbt;
        }
    }
}
