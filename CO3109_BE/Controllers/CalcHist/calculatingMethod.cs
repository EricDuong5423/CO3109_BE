using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections;
using System.Collections.Generic;

namespace CO3109_BE.Controllers.CalcHist
{
    public static class constClass
    {
        public const decimal nk = 1;
        public const decimal k0 = 1;
        public const decimal ka = 1;
        public const decimal kdc = 1.1m;
        public const decimal kd = 1.2m;
        public const decimal kc = 1.25m;
        public const decimal kbt = 1.3m;
        public const decimal Z01 = 25m;
        public const decimal kf = 1;
        public const decimal kx = 1.05m;
        public const decimal E = 210000;
    }
    public class calculatingMethod
    {
        public chapter2Method chapter2;
        public chapter3Method chapter3;
        public calculatingMethod()
        {
            chapter2 = new chapter2Method();
            chapter3 = new chapter3Method();
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
    public class chapter3Method
    {
        public Dictionary<decimal, decimal> KRDictionary = new Dictionary<decimal, decimal>()
        {
            { 15, 0.59m}, { 20, 0.48m}, { 30, 0.36m}, { 40, 0.29m}, { 50, 0.24m}, { 60, 0.22m}
        };
        public Dictionary<decimal, decimal> ADictionary = new Dictionary<decimal, decimal>()
        {
            {8, 11 }, { 9.525m, 28}, { 12.7m, 39.6m}, { 15.875m, 51.5m}, { 19.05m, 106}, { 25.4m, 180}, { 31.75m, 262}, { 38.1m, 395}, { 44.45m, 473}, { 50.8m, 645}
        };
        public Dictionary<(decimal, decimal), decimal> FindChainRange = new Dictionary<(decimal, decimal), decimal>()
        {
            // Dữ liệu bước xích 12.7 mm
            { (50, 0.19m), 12.7m }, { (200, 0.68m), 12.7m }, { (400, 1.23m), 12.7m }, { (600, 1.68m), 12.7m },
            { (800, 2.06m), 12.7m }, { (1000, 2.42m), 12.7m }, { (1200, 2.72m), 12.7m }, { (1600, 3.20m), 12.7m },

            { (50, 0.35m), 12.7m }, { (200, 1.27m), 12.7m }, { (400, 2.29m), 12.7m }, { (600, 3.13m), 12.7m },
            { (800, 3.86m), 12.7m }, { (1000, 4.52m), 12.7m }, { (1200, 5.06m), 12.7m }, { (1600, 5.95m), 12.7m },

            { (50, 0.45m), 12.7m }, { (200, 1.61m), 12.7m }, { (400, 2.91m), 12.7m }, { (600, 3.98m), 12.7m },
            { (800, 4.90m), 12.7m }, { (1000, 5.74m), 12.7m }, { (1200, 6.43m), 12.7m }, { (1600, 7.55m), 12.7m },

            // Dữ liệu bước xích 15.875 mm
            { (50, 0.57m), 15.875m }, { (200, 2.06m), 15.875m }, { (400, 3.72m), 15.875m }, { (600, 5.08m), 15.875m },
            { (800, 6.26m), 15.875m }, { (1000, 7.34m), 15.875m }, { (1200, 8.22m), 15.875m }, { (1600, 9.65m), 15.875m },

            { (50, 0.75m), 15.875m }, { (200, 2.70m), 15.875m }, { (400, 4.88m), 15.875m }, { (600, 6.67m), 15.875m },
            { (800, 8.22m), 15.875m }, { (1000, 9.63m), 15.875m }, { (1200, 10.8m), 15.875m }, { (1600, 12.7m), 15.875m },

            // Dữ liệu bước xích 19.05 mm
            { (50, 1.41m), 19.05m }, { (200, 4.80m), 19.05m }, { (400, 8.38m), 19.05m }, { (600, 11.4m), 19.05m },
            { (800, 13.5m), 19.05m }, { (1000, 15.3m), 19.05m }, { (1200, 16.9m), 19.05m }, { (1600, 19.3m), 19.05m },

            // Dữ liệu bước xích 25.4 mm
            { (50, 3.20m), 25.4m }, { (200, 11.0m), 25.4m }, { (400, 19.0m), 25.4m }, { (600, 25.7m), 25.4m },
            { (800, 30.7m), 25.4m }, { (1000, 34.7m), 25.4m }, { (1200, 38.3m), 25.4m }, { (1600, 43.8m), 25.4m },

            // Dữ liệu bước xích 31.75 mm
            { (50, 5.83m), 31.75m }, { (200, 19.3m), 31.75m }, { (400, 32.0m), 31.75m }, { (600, 42.0m), 31.75m },
            { (800, 49.3m), 31.75m }, { (1000, 54.9m), 31.75m }, { (1200, 60.0m), 31.75m },

            // Dữ liệu bước xích 38.1 mm
            { (50, 10.5m), 38.1m }, { (200, 34.8m), 38.1m }, { (400, 57.7m), 38.1m }, { (600, 75.7m), 38.1m },
            { (800, 88.9m), 38.1m }, { (1000, 99.2m), 38.1m }, { (1200, 108.0m), 38.1m },

            // Dữ liệu bước xích 44.45 mm
            { (50, 14.7m), 44.45m }, { (200, 43.7m), 44.45m }, { (400, 70.6m), 44.45m }, { (600, 88.3m), 44.45m },
            { (800, 101.0m), 44.45m },

            // Dữ liệu bước xích 50.8 mm
            { (50, 22.9m), 50.8m }, { (200, 68.1m), 50.8m }, { (400, 110.0m), 50.8m }, { (600, 138.0m), 50.8m },
            { (800, 157.0m), 50.8m }
        };
        public Dictionary<decimal, decimal> CheckImpactChain = new Dictionary<decimal, decimal>()
        {
            { 12.7m, 60}, { 15.875m, 50}, { 19.05m, 35}, {25.4m, 30 }, { 31.75m, 25}, { 38.1m, 20}, { 44.45m, 15}, { 50.8m, 15}
        };
        public decimal ChooseChainRange(decimal n01, decimal Pt)
        {
            decimal result = FindChainRange.Where(ele => n01 == ele.Key.Item1)
                                           .Where(ele => ele.Key.Item2 >= Pt)
                                           .OrderBy(ele => ele.Key.Item2)
                                           .FirstOrDefault()
                                           .Value;
            return result;
        }
        public Dictionary<(decimal, decimal), decimal> SafetyFactorDictionary = new Dictionary<(decimal, decimal), decimal>()
        {
            // Bước xích 12.7 và 15.875 mm
            { (12.7m, 50m), 7m }, { (12.7m, 200m), 7.8m }, { (12.7m, 400m), 8.5m }, { (12.7m, 600m), 9.3m },
            { (12.7m, 800m), 10.2m }, { (12.7m, 1000m), 11m }, { (12.7m, 1200m), 11.7m }, { (12.7m, 1600m), 13.2m },
            { (12.7m, 2000m), 14.8m }, { (12.7m, 2400m), 16.3m }, { (12.7m, 2800m), 18m },

            { (15.875m, 50m), 7m }, { (15.875m, 200m), 7.8m }, { (15.875m, 400m), 8.5m }, { (15.875m, 600m), 9.3m },
            { (15.875m, 800m), 10.2m }, { (15.875m, 1000m), 11m }, { (15.875m, 1200m), 11.7m }, { (15.875m, 1600m), 13.2m },
            { (15.875m, 2000m), 14.8m }, { (15.875m, 2400m), 16.3m }, { (15.875m, 2800m), 18m },

            // Bước xích 19.05 và 25.4 mm
            { (19.05m, 50m), 7m }, { (19.05m, 200m), 8.2m }, { (19.05m, 400m), 9.3m }, { (19.05m, 600m), 10.3m },
            { (19.05m, 800m), 11.7m }, { (19.05m, 1000m), 12.9m }, { (19.05m, 1200m), 14m }, { (19.05m, 1600m), 16.3m },

            { (25.4m, 50m), 7m }, { (25.4m, 200m), 8.2m }, { (25.4m, 400m), 9.3m }, { (25.4m, 600m), 10.3m },
            { (25.4m, 800m), 11.7m }, { (25.4m, 1000m), 12.9m }, { (25.4m, 1200m), 14m }, { (25.4m, 1600m), 16.3m },

            // Bước xích 31.75 và 38.1 mm
            { (31.75m, 50m), 7m }, { (31.75m, 200m), 8.5m }, { (31.75m, 400m), 10.2m }, { (31.75m, 600m), 13.2m },
            { (31.75m, 800m), 14.8m }, { (31.75m, 1000m), 16.3m }, { (31.75m, 1200m), 19.5m },

            { (38.1m, 50m), 7m }, { (38.1m, 200m), 8.5m }, { (38.1m, 400m), 10.2m }, { (38.1m, 600m), 13.2m },
            { (38.1m, 800m), 14.8m }, { (38.1m, 1000m), 16.3m }, { (38.1m, 1200m), 19.5m },

            // Bước xích 44.45 và 50.8 mm
            { (44.45m, 50m), 7m }, { (44.45m, 200m), 9.3m }, { (44.45m, 400m), 11.7m }, { (44.45m, 600m), 14m },
            { (44.45m, 800m), 16.3m },

            { (50.8m, 50m), 7m }, { (50.8m, 200m), 9.3m }, { (50.8m, 400m), 11.7m }, { (50.8m, 600m), 14m },
            { (50.8m, 800m), 16.3m }
        };
        public bool CheckSafetyCoefficent(decimal p, decimal n01, decimal s_check)
        {
            SafetyFactorDictionary.TryGetValue((p, n01), out decimal s);
            return s_check > s;
        }
        public bool CheckChainImpact(decimal p, decimal i_check)
        {
            CheckImpactChain.TryGetValue((p), out decimal i);
            return i_check <= i;
        }
        // Xác định thông số của xích và bộ truyền xích
        public decimal SmallDiscTeeth(decimal ux)
        {
            return 29 - 2 * ux;
        }
        public decimal BigDiscTeeth(decimal Z1, decimal ux)
        {
            return ux * Z1;
        }
        public decimal ToothCoefficent(decimal Z1)
        {
            return constClass.Z01 / Z1;
        }
        public decimal RotationCoefficent(decimal n3, decimal n01)
        {
            return n01 / n3;
        }
        public decimal Coefficent()
        {
            return constClass.k0 * constClass.ka * constClass.kdc * constClass.kd * constClass.kc * constClass.kbt;
        }
        public decimal CalculatePower(decimal P3, decimal k, decimal kn, decimal kz)
        {
            return P3 * k * kn * kz;
        }
        public decimal AxialDistance(decimal p)
        {
            return 40 * p;
        }
        public decimal NumberOfLinks(decimal Z1, decimal Z2, decimal a, decimal p)
        {
            decimal pi = (decimal)Math.PI;
            return (2 * a / p) + ((Z1 + Z2) / 2) + (((Z1 - Z2) * (Z1 - Z2) * p) / (4 * pi * pi * a));
        }
        public decimal ReCalcAxialDistance(decimal p, decimal x, decimal Z1, decimal Z2)
        {
            decimal pi = (decimal)Math.PI;
            decimal term1 = x - (Z2 + Z1) / 2;
            decimal term2 = (term1 * term1) - (2 * (Z2 - Z1) * (Z2 - Z1)) / (pi * pi);
            decimal sqrtTerm = (decimal)Math.Sqrt((double)term2);
            return 0.25m * p * (term1 + sqrtTerm);
        }
        public decimal DeltaA(decimal a)
        {
            decimal[] numbers = {0.002m, 0.003m, 0.004m };
            Random rand = new Random();
            int index = rand.Next(0, numbers.Length);
            return numbers[index] * a;
        }
        public decimal ReCalcA(decimal Da, decimal a_sao)
        {
            return a_sao - Da;
        }
        public decimal ChainImpacts(decimal Z1, decimal n1, decimal x)
        {
            return Z1 * (n1 / (15 * x));
        }
        //Kiểm nghiệm xích
        public decimal Velocity(decimal Z1, decimal p , decimal n3)
        {
            return (Z1 * p * n3) / 60000;
        }
        public decimal CircularForce(decimal P3, decimal v)
        {
            return (P3 * 1000) / v;
        }
        public decimal CentrifugalForce(decimal q, decimal v)
        {
            return q * v * v;
        }
        public decimal GravityForce(decimal q, decimal a)
        {
            return (constClass.kf * 9.81m * q * a) / 1000;
        }
        public decimal SafetyCoefficient(decimal Q, decimal Ft, decimal Fo, decimal Fv)
        {
            return (Q * 1000) / (constClass.kd * Ft + Fo + Fv);
        }
        //Xác định thông số đĩa xích và lực tác dụng lên trục
        public decimal d1(decimal p, decimal Z1)
        {
            decimal pi = (decimal)Math.PI;
            return p / (decimal)Math.Sin((double)(pi / Z1));
        }
        public decimal d2(decimal p, decimal Z2)
        {
            decimal pi = (decimal)Math.PI;
            return p / (decimal)Math.Sin((double)(pi / Z2));
        }
        public decimal da1(decimal p, decimal Z1)
        {
            decimal pi = (decimal)Math.PI;
            decimal cot = 1 / ((decimal)Math.Tan((double)(pi / Z1)));
            return p * ((1 / 2) + cot);
        }
        public decimal da2(decimal p, decimal Z2)
        {
            decimal pi = (decimal)Math.PI;
            decimal cot = 1 / ((decimal)Math.Tan((double)(pi / Z2)));
            return p * ((1 / 2) + cot);
        }
        public decimal r(decimal dl)
        {
            return 0.5025m * dl + 0.05m;
        }
        public decimal df1(decimal d1, decimal r)
        {
            return d1 - 2 * r;
        }
        public decimal df2(decimal d2, decimal r)
        {
            return d2 - 2 * r;
        }
        public decimal kr1(decimal Z1)
        {
            if (KRDictionary.TryGetValue(Z1, out decimal exactValue))
            {
                return exactValue;
            }

            // Tìm 2 giá trị Z gần nhất
            var lower = KRDictionary.Keys.Where(z => z < Z1).Max();
            var upper = KRDictionary.Keys.Where(z => z > Z1).Min();

            // Lấy giá trị kr tương ứng
            decimal krLower = KRDictionary[lower];
            decimal krUpper = KRDictionary[upper];

            // Nội suy tuyến tính
            decimal interpolatedValue = krLower + (krUpper - krLower) * ((Z1 - lower) / (upper - lower));

            return interpolatedValue;
        }
        public decimal kr2(decimal Z2)
        {
            if (KRDictionary.TryGetValue(Z2, out decimal exactValue))
            {
                return exactValue;
            }

            // Tìm 2 giá trị Z gần nhất
            var lower = KRDictionary.Keys.Where(z => z < Z2).Max();
            var upper = KRDictionary.Keys.Where(z => z > Z2).Min();

            // Lấy giá trị kr tương ứng
            decimal krLower = KRDictionary[lower];
            decimal krUpper = KRDictionary[upper];

            // Nội suy tuyến tính
            decimal interpolatedValue = krLower + (krUpper - krLower) * ((Z2 - lower) / (upper - lower));

            return interpolatedValue;
        }
        public decimal Fvd(decimal n3, decimal p)
        {
            return 13 * (decimal)Math.Pow(10, -7) * n3 * p * p * p;
        }
        public decimal A(decimal p)
        {
            ADictionary.TryGetValue((p), out decimal result);
            return result;
        }
        public decimal Fr(decimal Ft)
        {
            return constClass.kx * Ft;
        }
        public decimal sH1(decimal kr1, decimal Ft, decimal Fvd, decimal A)
        {
            decimal ratio = (kr1 * (Ft * constClass.kd + Fvd) * constClass.E) / (A * constClass.kd);
            return 0.47m * (decimal)Math.Sqrt((double)ratio);
        }
        public decimal sH2(decimal kr2, decimal Ft, decimal Fvd, decimal A)
        {
            decimal ratio = (kr2 * (Ft * constClass.kd + Fvd) * constClass.E) / (A * constClass.kd);
            return 0.47m * (decimal)Math.Sqrt((double)ratio);
        }
    }
}
