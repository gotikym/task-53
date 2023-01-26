using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    static void Main(string[] args)
    {
        Prison prison = new Prison();
        prison.Work();
    }
}

class Prison
{
    private List<Prisoner> _prisoners = new List<Prisoner>();

    public Prison()
    {
        AddCriminals();
    }

    private List<string> _typesCrime;

    public void Work()
    {
        const string CommandShowPrisoners = "show";
        const string CommandReleasePrisoners = "release";
        const string CommandExit = "exit";
        bool isExit = false;

        Console.WriteLine("Добро пожаловать в лучшую программу этой тюрьмы, т.к. в ней мы освобождаем людей по амнистии");
        Console.ReadKey();

        while (isExit == false)
        {
            Console.Clear();
            Console.WriteLine("Посмотреть список заключенных - " + CommandShowPrisoners + ", освободить заключенных - " + CommandReleasePrisoners +
                ", выйти из программы - " + CommandExit);
            string userChose = Console.ReadLine();

            switch (userChose)
            {
                case CommandShowPrisoners:
                    ShowPrisoners();
                    break;

                case CommandReleasePrisoners:
                    ReleasePrisoners();
                    break;

                case CommandExit:
                    isExit = true;
                    break;
            }
        }
    }

    private void ReleasePrisoners()
    {
        Console.WriteLine("Выбирайте из списка преступлений, на какое выпала амнистия и сегодня же они будут свободны");
        ShowTypeCrime();
        FilterPrisoners(_prisoners[GetIndex()].TypeCrime);
    }

    private void FilterPrisoners(string typeCrime)
    {
        var filteredPrisoners = _prisoners.Where(prisoner => prisoner.TypeCrime != typeCrime);
        _prisoners = filteredPrisoners.ToList();
    }

    private void ShowTypeCrime()
    {
        int numberTypeCrime = 1;

        var typesCrime = from Prisoner prisoner in _prisoners select prisoner.TypeCrime;
        _typesCrime = typesCrime.Distinct().ToList();

        foreach (string type in _typesCrime)
        {
            Console.WriteLine(numberTypeCrime + " " + type);
            numberTypeCrime++;
        }

        Console.ReadKey();
    }

    private void ShowPrisoners()
    {
        foreach (var criminal in _prisoners)
        {
            Console.WriteLine(criminal.FullName + " " + criminal.TypeCrime);
        }

        Console.ReadKey();
    }

    private void AddCriminals()
    {
        _prisoners.Add(new Prisoner("Майк Вазовски", "Вандализм"));
        _prisoners.Add(new Prisoner("Политек Политовский", "Антиправительственное"));
        _prisoners.Add(new Prisoner("Шрек Фамильный", "ГрабёжЪ"));
        _prisoners.Add(new Prisoner("Осёл Говорящий", "Антиправительственное"));
        _prisoners.Add(new Prisoner("Ирод Проклятый", "Ел шавуху перед голодными"));
        _prisoners.Add(new Prisoner("Друг Ирода", "Ел шавуху перед голодными"));
        _prisoners.Add(new Prisoner("Ваныльный Алекс", "Антиправительственное"));
        _prisoners.Add(new Prisoner("Проверяющий Диваны", "Переработал"));
    }

    private int GetIndex()
    {
        bool isParse = false;
        int numberForReturn = 0;

        while (isParse == false)
        {
            string userNumber = Console.ReadLine();
            int.TryParse(userNumber, out numberForReturn);

            if (numberForReturn <= 0 || numberForReturn >= _typesCrime.Count)
            {
                Console.WriteLine("вводи нормально, а то без еды останешься");
            }
            else
            {
                isParse = true;
            }
        }

        return numberForReturn - 1;
    }
}

class Prisoner
{
    public Prisoner(string fullName, string typeCrime)
    {
        FullName = fullName;
        TypeCrime = typeCrime;
    }

    public string FullName { get; private set; }
    public string TypeCrime { get; private set; }
}