using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandas_assignment
{
    public class WorkerData
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public decimal HolidayPayment { get; set; }
        public decimal BonusPayment { get; set; }
        public decimal AdditionalPayment { get; set; }
        public decimal TaxAmount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
    

    public enum PaymentType
    {
        Alga,
        Priedas,
        Atostoginiai,
        Premija
    }
}
