namespace BankSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            SavingsAccount mySavings = new SavingsAccount
            {
                AccountId = 101,
                AccountName = "Ahmed"
            };

            mySavings.Deposit(1000);
            mySavings.ApplyInterest();  
            mySavings.PrintBalance();

            Console.WriteLine("-------------------");

            CurrentAccount myCurrent = new CurrentAccount
            {
                AccountId = 102,
                AccountName = "Sara"
            };

            myCurrent.Deposit(100);
            myCurrent.Withdraw(400);  
            myCurrent.PrintBalance();
        }
    }


    public class Account
    {
        public string AccountName { get; set; }
        public int AccountId { get; set; }

        protected decimal _balance;

        public virtual decimal Balance => _balance;

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }

            _balance += amount;
            Console.WriteLine($"Deposited {amount}. Current Balance: {_balance}");
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return;
            }

            if (_balance >= amount)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew {amount}. Remaining Balance: {_balance}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public void PrintBalance()
        {
            Console.WriteLine($"Account {AccountName}: Current Balance = {_balance}");
        }
    }


    public class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; } = 0.05m;

        public void ApplyInterest()
        {
            decimal interest = _balance * InterestRate;
            _balance += interest;
            Console.WriteLine($"Interest added: {interest}");
        }
    }

    public class CurrentAccount : Account
    {
        public decimal OverdraftLimit { get; set; } = 500;

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount.");
                return;
            }

            if (_balance - amount >= -OverdraftLimit)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew {amount} (Current Account). New Balance: {_balance}");
            }
            else
            {
                Console.WriteLine("Overdraft limit exceeded!");
            }
        }
    }
}