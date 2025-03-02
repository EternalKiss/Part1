using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace Part1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddWorker = "1";
            const string CommandRemoveWorker = "2";
            const string CommandShowInfo = "3";
            const string CommandExit = "4";

            Dictionary<string, List<string>> workerCase = new Dictionary<string, List<string>>();

            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.WriteLine($"Добавить сотрудника - {CommandAddWorker}");
                Console.WriteLine($"Удалить сотрудника - {CommandRemoveWorker}");
                Console.WriteLine($"Показать список всех сотрудников - {CommandShowInfo}");
                Console.WriteLine($"Закрыть программу - {CommandExit}");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddWorker:
                        AddWorker(workerCase);
                        break;

                    case CommandRemoveWorker:
                        DeleteWorker(workerCase);
                        break;

                    case CommandShowInfo:
                        ShowInfo(workerCase);
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine($"Такой команды нет!");
                        break;
                }
            }
        }

        static void AddWorker(Dictionary<string, List<string>> addWorker)
        {
            Console.Write($"Напишите полное ФИО нового сотрудника: ");
            string workerName = Console.ReadLine();

            Console.Write($"Напишите должность сотрудника: ");
            string workerPost = Console.ReadLine();

            if (addWorker.ContainsKey(workerPost) == false)
            {
                addWorker[workerPost] = new List<string>();
            }

            addWorker[workerPost].Add(workerName);
        }

        static string GetInputString(string messageToUser)
        {
            Console.WriteLine(messageToUser);
            string userInput = Console.ReadLine();

            return userInput;
        }

        static void DeleteWorker(Dictionary<string, List<string>> workerCase)
        {
            bool isUserInputCorrect = false;
            string keyToRemoveFrom = "";
            string userInput;
            int keysContained = 0;
            string userInputName = "";
            string errorMessageToUser = "";

            while (isUserInputCorrect == false)
            {
                userInput = GetInputString("\nСотрудника на какой должности надо удалить?(укажите должность)");
                errorMessageToUser = "У сотрудника нет такой должности!";

                foreach (var entry in workerCase)
                {
                    if (workerCase.ContainsKey(userInput))
                    {
                        keyToRemoveFrom = userInput;
                        isUserInputCorrect = true;
                        keysContained++;

                        userInputName = GetInputString("\nКакого сотрудника надо удалить? (укажите ФИО):");

                        if (entry.Value.Contains(userInputName) && keysContained == 1)
                        {
                            keyToRemoveFrom = entry.Key;
                            isUserInputCorrect = true;
                        }

                        else
                        {
                            errorMessageToUser = "Нет такого сотрудника!";
                            isUserInputCorrect = false;
                        }
                    }

                    if (isUserInputCorrect == false)
                    {
                        Console.WriteLine(errorMessageToUser);
                    }
                }

                workerCase[keyToRemoveFrom].Remove(userInputName);

                if (workerCase[keyToRemoveFrom].Count == 0)
                {
                    workerCase.Remove(keyToRemoveFrom);
                }

                Console.WriteLine("\nДосье удалено!\n");
            }
        }

            static void ShowInfo(Dictionary<string, List<string>> workersInformation)
            {
                int fileNumber = 1;
                Console.WriteLine($"Список сотрудников: ");

                if (workersInformation.Count > 0)
                {
                    foreach (var workerInformation in workersInformation)
                    {
                        Console.Write(fileNumber + ". ");
                        Console.Write($"{workerInformation.Key} - ");

                        foreach (var employeeName in workerInformation.Value)
                        {
                            Console.Write($"{employeeName} ");
                        }

                        fileNumber++;

                        Console.WriteLine("\n");
                    }
                }
            }
        }
    }

