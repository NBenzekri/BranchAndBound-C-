using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BranchAndBound
{
    class ReplenishmentAPI
    {
        public static DateTime[] holydays = new DateTime[] {
            Convert.ToDateTime("01-01-2018"),
            Convert.ToDateTime("06-01-2018"),
            Convert.ToDateTime("07-01-2018"),
            Convert.ToDateTime("11-01-2018"),
            Convert.ToDateTime("13-01-2018"),
            Convert.ToDateTime("14-01-2018")
        };
        static DateTime epuiDate = DateTime.Now;
        static void Main(String[] args)
        {
            System.Console.WriteLine("Relenishement Date");
            DateTime repDate = RelenishementDate(Convert.ToDateTime("17-01-2018"), Convert.ToDateTime("01-01-2018"), 1, holydays);
            System.Console.WriteLine("Date d'épuisement est: " + epuiDate.ToString("f").First().ToString().ToUpper() + epuiDate.ToString("f").Substring(1));
            System.Console.WriteLine("Date effectif d'alimentation est: " + repDate.ToString("f").First().ToString().ToUpper() + repDate.ToString("f").Substring(1));
            System.Console.ReadLine();
        }



        //calculer la date d'alimentation
        public static DateTime RelenishementDate(DateTime epuisementDate, DateTime Startdate,
            int InterventionCV, DateTime[] holydays)
        {
            epuiDate = epuisementDate;
            DateTime InterventionDate = epuisementDate.AddDays(-InterventionCV);
            DateTime replDate = epuisementDate;

            // number of days before exhaustion Date
            // int i = (epuisementDate - Startdate).Days;
            DateTime eday = epuisementDate.AddDays(-InterventionCV);
            do
            {
                if (holydays.Contains(eday))
                    eday = eday.AddDays(-1);
                else return eday;
            } while (eday != Startdate);

            //bool x = holydays.Contains(Convert.ToDateTime("02-01-2018"));
            //System.Console.WriteLine("Date 02-01-2018 exists /No: " + x);
            return eday;
        }

        // calculer la date d'epuisement
        // Startdate est le jour de la derniere operation d'alimentation
        public static DateTime epuisementDate(double[] demandForecast, DateTime Startdate, double LastReplenishmentlRest)
        {
            double seuilMin = LastReplenishmentlRest * 0.1;
            double seuilMax = LastReplenishmentlRest * 0.25;
            DateTime dateEpuis = Startdate;
            double Amount = LastReplenishmentlRest;
            int i = 0;
            while (Amount >= seuilMax)
            {
                Amount -= demandForecast[i];
                dateEpuis = dateEpuis.AddDays(1);
                i++;
            }
            return dateEpuis;
        }

        //la date de lancement d'ordre
        public static DateTime OrderDate(DateTime replenishmentDate, int delaisIntevention)
        {
            return replenishmentDate.AddDays(-delaisIntevention);
        }




    }
}
