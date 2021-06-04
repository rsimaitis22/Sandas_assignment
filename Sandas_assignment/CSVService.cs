using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandas_assignment
{
    public class CSVService
    {
        List<WorkerData> data = new List<WorkerData>();

        public List<WorkerData> GetWorkerDataFromFile(string filename)
        {
            string path = $"{ Directory.GetCurrentDirectory()}\\{filename}.csv";

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        TryAddWorkerDataFromString(line,data);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void TryAddWorkerDataFromString(string line,List<WorkerData> data)
        {
            WorkerData temp = FormatCSVToWorkerData(line);

            if (data.Exists(x => x.Name == temp.Name))
            {
                var existingData = data.FirstOrDefault(x => x.Name == temp.Name);

                existingData.Salary += temp.Salary;
                existingData.BonusPayment += temp.BonusPayment;
                existingData.AdditionalPayment += temp.AdditionalPayment;
                existingData.HolidayPayment += temp.HolidayPayment;

            }
            else if (temp.Name != null)
                data.Add(temp);
        }

        private WorkerData FormatCSVToWorkerData(string v)
        {
            var tempStr = v.Split(',');
            try
            {
                var temp = new WorkerData()
                {
                    Name = tempStr[0],
                    PaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), tempStr[1])
                };

                AssignPaymentAmountToType(temp, Convert.ToDecimal(tempStr[2]));

                return temp;
            }
            catch (Exception)
            {
                Console.WriteLine("Error while creating worker data");
                return new WorkerData();
            }
        }

        private void AssignPaymentAmountToType(WorkerData temp, decimal payment)
        {
            if (temp.PaymentType == PaymentType.Alga)
                temp.Salary += payment;
            else if (temp.PaymentType == PaymentType.Atostoginiai)
                temp.HolidayPayment += payment;
            else if (temp.PaymentType == PaymentType.Premija)
                temp.BonusPayment += payment;
            else if (temp.PaymentType == PaymentType.Priedas)
                temp.AdditionalPayment += payment;
        }

        public void WriteWorkerDataToFile(string filename, List<WorkerData> workers)
        {
            string path = $"{Directory.GetCurrentDirectory()}\\{filename}.csv";

            using (StreamWriter sw = new StreamWriter(path))
            {
                string header = "Darbuotojas; Suma; Mokesciai";
                sw.WriteLine(header);
                foreach (var item in workers)
                {
                    string temp = FormatWorkerDataToCsv(item);
                    sw.WriteLine(temp);
                }
            }
            Console.WriteLine("File created");
        }
        public void WriteSplitWorkerDataToFile(string filename, List<WorkerData> workers)
        {
            string path = $"{Directory.GetCurrentDirectory()}\\{filename}.csv";

            using (StreamWriter sw = new StreamWriter(path))
            {
                string header = "Darbuotojas; Tipas; Suma";
                sw.WriteLine(header);
                foreach (var item in workers.OrderBy(x => x.Name))
                {
                    List<string> temp = FormatSplitWorkerDataToStringList(item);
                    foreach (var line in temp)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            Console.WriteLine("File created");
        }

        private List<string> FormatSplitWorkerDataToStringList(WorkerData item)
        {
            List<string> tempList = new List<string>();
            if (item.AdditionalPayment > 0)
                tempList.Add(FormatSplitWorkerDataToCsv(item.Name, PaymentType.Priedas, item.AdditionalPayment));
            if (item.BonusPayment > 0)
                tempList.Add(FormatSplitWorkerDataToCsv(item.Name, PaymentType.Premija, item.BonusPayment));
            if (item.HolidayPayment > 0)
                tempList.Add(FormatSplitWorkerDataToCsv(item.Name, PaymentType.Atostoginiai, item.HolidayPayment));
            if (item.Salary > 0)
                tempList.Add(FormatSplitWorkerDataToCsv(item.Name, PaymentType.Alga, item.Salary));
            return tempList;
        }

        private string FormatSplitWorkerDataToCsv(string name, PaymentType paymentType, decimal amount)
        {
            return $"{name},{paymentType},{amount}";
        }

        private string FormatWorkerDataToCsv(WorkerData item)
        {
            decimal tempAmount = CalculateAllIncome(item);
            CalculateTax(item);
            return $"{item.Name},{tempAmount},{item.TaxAmount}";
        }

        private decimal CalculateAllIncome(WorkerData item)
        {
            return item.HolidayPayment + item.Salary + item.AdditionalPayment + item.BonusPayment;
        }

        private void CalculateTax(WorkerData item)
        {
            item.TaxAmount = (item.HolidayPayment + item.Salary + item.AdditionalPayment + item.BonusPayment) / 100 * 40;
        }
    }
}
