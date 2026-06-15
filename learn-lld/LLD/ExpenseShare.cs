using System;
using System.Collections.Generic;
using System.Linq;

public enum ExpenseType
{
    Equal,
    Exact,
    Percent
}

public class User
{
    public int UserId { get; set; }

    public string Name { get; set; }

    // +ve => should receive
    // -ve => should pay
    public decimal Balance { get; set; }
}

public class ExpenseShare
{
    public User User { get; set; }

    // Used for Exact and Percent
    public decimal Value { get; set; }
}

public class Expense
{
    public decimal Amount { get; set; }

    public User PaidBy { get; set; }

    public ExpenseType Type { get; set; }

    public List<ExpenseShare> Shares { get; set; }
        = new();
}

public class ExpenseManager
{
    private readonly Dictionary<int, User> users
        = new();

    public void AddUser(User user)
    {
        users[user.UserId] = user;
    }

    public void AddExpense(Expense expense)
    {
        switch (expense.Type)
        {
            case ExpenseType.Equal:
                HandleEqual(expense);
                break;

            case ExpenseType.Exact:
                HandleExact(expense);
                break;

            case ExpenseType.Percent:
                HandlePercent(expense);
                break;
        }
    }

    private void HandleEqual(Expense expense)
    {
        int count = expense.Shares.Count;

        decimal share =
            Math.Round(expense.Amount / count, 2);

        foreach (var s in expense.Shares)
        {
            s.User.Balance -= share;
        }

        expense.PaidBy.Balance += expense.Amount;
    }

    private void HandleExact(Expense expense)
    {
        decimal total =
            expense.Shares.Sum(x => x.Value);

        if (total != expense.Amount)
        {
            throw new Exception(
                "Invalid Exact Split");
        }

        foreach (var s in expense.Shares)
        {
            s.User.Balance -= s.Value;
        }

        expense.PaidBy.Balance += expense.Amount;
    }

    private void HandlePercent(Expense expense)
    {
        decimal totalPercent =
            expense.Shares.Sum(x => x.Value);

        if (totalPercent != 100)
        {
            throw new Exception(
                "Percent must equal 100");
        }

        foreach (var s in expense.Shares)
        {
            decimal amount =
                expense.Amount * s.Value / 100;

            s.User.Balance -= amount;
        }

        expense.PaidBy.Balance += expense.Amount;
    }

    public void ShowBalances()
    {
        Console.WriteLine();
        Console.WriteLine("Current Balances");

        foreach (var user in users.Values)
        {
            Console.WriteLine(
                $"{user.Name} => {user.Balance}");
        }

        Console.WriteLine();
    }

   

    public static void Test()
    {
        var manager = new ExpenseManager();

        var shabbeer = new User
        {
            UserId = 1,
            Name = "Shabbeer"
        };

        var john = new User
        {
            UserId = 2,
            Name = "John"
        };

        var david = new User
        {
            UserId = 3,
            Name = "David"
        };

        manager.AddUser(shabbeer);
        manager.AddUser(john);
        manager.AddUser(david);

        //------------------------------------
        // EQUAL SPLIT
        //------------------------------------

        Console.WriteLine(
            "Expense 1 : Equal Split");

        manager.AddExpense(
            new Expense
            {
                Amount = 90,
                PaidBy = shabbeer,
                Type = ExpenseType.Equal,

                Shares = new List<ExpenseShare>
                {
                    new ExpenseShare
                    {
                        User = shabbeer
                    },

                    new ExpenseShare
                    {
                        User = john
                    },

                    new ExpenseShare
                    {
                        User = david
                    }
                }
            });

        manager.ShowBalances();

        //------------------------------------
        // EXACT SPLIT
        //------------------------------------

        Console.WriteLine(
            "Expense 2 : Exact Split");

        manager.AddExpense(
            new Expense
            {
                Amount = 100,
                PaidBy = john,
                Type = ExpenseType.Exact,

                Shares = new List<ExpenseShare>
                {
                    new ExpenseShare
                    {
                        User = shabbeer,
                        Value = 20
                    },

                    new ExpenseShare
                    {
                        User = john,
                        Value = 30
                    },

                    new ExpenseShare
                    {
                        User = david,
                        Value = 50
                    }
                }
            });

        manager.ShowBalances();

        //------------------------------------
        // PERCENT SPLIT
        //------------------------------------

        Console.WriteLine(
            "Expense 3 : Percent Split");

        manager.AddExpense(
            new Expense
            {
                Amount = 200,
                PaidBy = david,
                Type = ExpenseType.Percent,

                Shares = new List<ExpenseShare>
                {
                    new ExpenseShare
                    {
                        User = shabbeer,
                        Value = 40
                    },

                    new ExpenseShare
                    {
                        User = john,
                        Value = 30
                    },

                    new ExpenseShare
                    {
                        User = david,
                        Value = 30
                    }
                }
            });

        manager.ShowBalances();
    }


}
