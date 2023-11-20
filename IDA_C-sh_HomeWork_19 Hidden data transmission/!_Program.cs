// HomeWork template 1.4 // date: 17.10.2023

using IDA_C_sh_HomeWork_19_Hidden_data_transmission;
using Service;
using System;
using System.Linq.Expressions;
using System.Text;

/// QUESTIONS ///
/// 1. 

// { "HomeWork 19 : [Hidden data transmission] --------------------------------

namespace IDA_C_sh_HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MainMenu.MainMenu mainMenu = new MainMenu.MainMenu();

            do
            {
                Console.Clear();
                mainMenu.Show_menu();
                if (mainMenu.User_Choice_Handle() == 0) break;
                Console.ReadKey();
            } while (true);
            // Console.ReadKey();
        }

        public static void Task_1(string work_name)
        /* Задание: "Скрытая передача данных"

        Вам нужно разработать программу на C#, которая позволяет скрыто передавать данные, 
        используя три разных формата сохранения: обычный текстовый файл, JSON и XML. 
        Программа должна включать следующие аспекты: основы С#, ООП, Generics, работу с файлами, делегаты и события.

        Шаги для выполнения задания:

        Создайте класс "DataTransfer", который будет отвечать за скрытую передачу данных. 
        Этот класс должен содержать основную логику для передачи данных в различных форматах.

        Внутри класса "DataTransfer" создайте динамические события:

        "DataSent" - событие, возникающее при успешной передаче данных.
        "DataReceived" - событие, возникающее при успешном получении данных.
        Реализуйте методы для передачи данных в каждом из трех форматов (текстовый файл, JSON и XML) внутри класса "DataTransfer". 
        
        В каждом методе: 

        Создайте делегат, который будет отвечать за запись и чтение данных в нужном формате.
        Используйте делегат, чтобы записать или прочитать данные из файла соответствующего формата.
        Генерируйте событие "DataSent" или "DataReceived" после успешной передачи или получения данных.
        Создайте методы в главном классе программы для демонстрации работы вашей реализации скрытой передачи данных:

        Создайте экземпляр класса "DataTransfer".
        Зарегистрируйте обработчики событий "DataSent" и "DataReceived".
        Вызовите методы передачи данных в каждом из трех форматов.
        В обработчиках событий выведите соответствующие сообщения об успешной передаче или получении данных. */
        { Console.WriteLine("\n***\t{0}\n\n", work_name);


            DataTransfer dataTransfer_1 = new DataTransfer();

            dataTransfer_1.Original_data = " *** TOP SECRET ***\nSi vis pacem, para bellum\n";
            Console.WriteLine("\nOriginal data:\n" +  dataTransfer_1.Original_data);

            Console.WriteLine("\n--- Choose data format to use:\n1) JSON\n2) XML\n3) Text");
            switch (ServiceFunction.Get_Int(1, 3, "noup. try again"))
            {
                case 1: dataTransfer_1.dataToRequiredFormat_delegate = dataTransfer_1.DataToJSON; break;
                case 2: dataTransfer_1.dataToRequiredFormat_delegate = dataTransfer_1.DataToXML; break;
                case 3: dataTransfer_1.dataToRequiredFormat_delegate = dataTransfer_1.DataToText; break;
            }

            dataTransfer_1.Data_in_choosen_format = dataTransfer_1.DataConvert_delegateInvoke(dataTransfer_1.Original_data);
            Console.WriteLine("\nFormatted data:\n" + dataTransfer_1.Data_in_choosen_format);

            Console.WriteLine("\n--- Choose what to do with data:\n1) Sent via net\n2) Save to file");
            switch (ServiceFunction.Get_Int(1, 2, "noup. try again"))
            {
                case 1: dataTransfer_1.ActionWithData += dataTransfer_1.SentViaNet; break;
                case 2: dataTransfer_1.ActionWithData += dataTransfer_1.WriteToFile; break;
            }

            dataTransfer_1.ActionWithData_eventInvoke("www.fbi.com");

            Console.WriteLine("\n\nChoose how to load data:\n1) Load from net\n2) Load from file");
            switch (ServiceFunction.Get_Int(1, 2, "noup. try again"))
            {
                case 1: dataTransfer_1.ActionWithData += dataTransfer_1.LoadFromNet; break;
                case 2: dataTransfer_1.ActionWithData += dataTransfer_1.LoadFromFile; break;
            }

            dataTransfer_1.ActionWithData_eventInvoke("www.fbi.com");

            Console.WriteLine("\n\nRestored original data:\n" + dataTransfer_1.LoadedDataConvertedBack);

        }


    }// class Program
}// namespace