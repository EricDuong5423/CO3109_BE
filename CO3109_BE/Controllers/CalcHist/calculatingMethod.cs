using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CO3109_BE.Controllers.CalcHist
{
    public static class constClass
    {
        public const decimal nk = 1;
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
        public decimal GeneralEfficiency(decimal nol, decimal nbr, decimal nx)
        {
            return constClass.nk * (decimal)Math.Pow((double)nol, 4) * (decimal)Math.Pow((double)nbr, 3) * nx;
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
        public decimal PreliminaryGearRatio(decimal uh, decimal ux)
        {
            return uh * ux;
        }
        // Method for transmission
        public decimal GenTransRatio(decimal ndc, decimal nlv)
        {
            return ndc / nlv;
        }
        public decimal ChainDriveCoef(decimal u, decimal u1, decimal u2)
        {
            return u / (u1 * u2);
        }
        //Method for power
        public decimal Pbt(decimal Plv, decimal nol)
        {
            return Plv / nol;
        }
        public decimal P3(decimal Pbt, decimal nol, decimal nx)
        {
            return Pbt / (nol * nx);
        }
        public decimal P2(decimal P3, decimal nol, decimal nbr)
        {
            return P3 / (nol * nbr);
        }
        public decimal P1(decimal P2, decimal nol, decimal nbr)
        {
            return P2 / (nol * nbr);
        }
        public decimal Pm(decimal P1)
        {
            return P1 / (constClass.nk);
        }
        //Method for rotation velocity
        public decimal n2(decimal n1, decimal u1)
        {
            return n1 / u1;
        }
        public decimal n3 (decimal n2, decimal u2)
        {
            return n2 / u2;
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
