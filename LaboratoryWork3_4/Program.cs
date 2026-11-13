using System;

namespace LaboratoryWork3_4
{
    public delegate int[] RowSumsDelegate(int[,] matrix);

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== ЗАВДАННЯ 1: Лямбда-вираз та анонімний метод (Варіант 8) ===");

            int[,] matrix = {
                { 5, 10, 15 },
                { 2,  4,  6 },
                { 1,  3,  5 }
            };

            RowSumsDelegate anonymousMethod = delegate (int[,] m)
            {
                int rows = m.GetLength(0);
                int cols = m.GetLength(1);
                int[] sums = new int[rows];
                for (int i = 0; i < rows; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < cols; j++) sum += m[i, j];
                    sums[i] = sum;
                }
                return sums;
            };
            Console.WriteLine("Результат через анонімний метод: " + string.Join(", ", anonymousMethod(matrix)));

            RowSumsDelegate lambdaExpr = (int[,] m) =>
            {
                int rows = m.GetLength(0);
                int cols = m.GetLength(1);
                int[] sums = new int[rows];
                for (int i = 0; i < rows; i++)
                {
                    int rowSum = 0;
                    for (int j = 0; j < cols; j++) rowSum += m[i, j];
                    sums[i] = rowSum;
                }
                return sums;
            };
            Console.WriteLine("Результат через лямбда-вираз:   " + string.Join(", ", lambdaExpr(matrix)));

            Console.WriteLine("\n=======================================================");
            Console.WriteLine("=== ЗАВДАННЯ 2 та 3: Компонент BankAccount та подія ===");
            Console.WriteLine("=======================================================");

            BankAccount account = new BankAccount("UA99322000000123456789", 1000m, 500m);

            account.OverdraftOccurred += BankAccount_OverdraftOccurred;

            try
            {
                Console.WriteLine($"\nРахунок № {account.AccountNumber} відкрито.");
                Console.WriteLine($"Поточний баланс: {account.Balance} грн, Допустимий овердрафт: {account.CreditLimit} грн");

                Console.WriteLine("\n[Дія]: Спроба зняти 1600 грн (що перевищує доступні 1500 грн)...");
                account.Withdraw(1600m);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Виняток оброблено в програмі]: {ex.Message}");
            }
        }

        private static void BankAccount_OverdraftOccurred(object sender, OverdraftEventArgs e)
        {
            Console.WriteLine("\n-------------------------------------------------------");
            Console.WriteLine("⚠️ УВАГА: Зафіксовано подію OVERDRAFT (Перевищення кредиту)!");
            Console.WriteLine($"   Номер рахунку: {e.AccountNumber}");
            Console.WriteLine($"   Баланс на момент запиту: {e.CurrentBalance} грн");
            Console.WriteLine($"   Кредитний ліміт: {e.CreditLimit} грн");
            Console.WriteLine($"   Запитана сума: {e.RequestedArgumentAmount} грн");
            Console.WriteLine($"   Сума перевищення (Overdraft): {e.OverdraftAmount} грн");
            Console.WriteLine("-------------------------------------------------------");
        }
    }
}