using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IDA_C_sh_HomeWork_19_Hidden_data_transmission
{
    internal class DataTransfer
    {
        /////////// CTOR ///////////
        
        public DataTransfer()
        {
            DataSent += DataSent_handler;
            DataReceived += DataReceived_handler;
        }

        /////////// PROPS ///////////

        public string Original_data {  get; set; }
        public string Data_in_choosen_format { get; set; }
        public string ChoosenFormat { private set; get; }
        public string LoadedData { get; set; }
        public string LoadedDataConvertedBack { get; set; }



        /////////// EVENTS ///////////

        public event Action<string> DataSent = delegate { };
        public event Action<string> DataReceived = delegate { };
        public event Action<string> ActionWithData = delegate { };

        public delegate string DataToRequiredFormat(string data);
        public DataToRequiredFormat dataToRequiredFormat_delegate; // = delegate { return string.Empty; };

        /////////// METHODS ///////////

        internal string DataToJSON(string data)
        {
            ChoosenFormat = "JSON";
            return JsonSerializer.Serialize(data); 
        }
        internal  string DataToXML(string data) 
        {
            ChoosenFormat = "XML";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
            MemoryStream xmldatastream = new MemoryStream();
            xmlSerializer.Serialize(xmldatastream, data);
            return new StreamReader(xmldatastream).ReadToEnd();
        }
        internal string DataToText(string data) 
        {
            ChoosenFormat = "Text";
            return data; 
        }

        public string DataConvert_delegateInvoke(string data) { return dataToRequiredFormat_delegate(data); }
        public void ActionWithData_eventInvoke(string destination) { ActionWithData(destination); }

        internal void WriteToFile(string fileName) 
        {
            string correct_filename = fileName.Replace('.', '_') + ".dat";
            using (StreamWriter streamWriter = new StreamWriter(correct_filename))
            {
                streamWriter.Write(Data_in_choosen_format);
            }
            DataSent(correct_filename); 
        }
        internal void SentViaNet(string net_destination) 
        { 
            Console.WriteLine("Data is sending to " + net_destination);
            DataSent(net_destination); 
        }
        internal void LoadFromFile(string fileName) 
        {
            string correct_filename = fileName.Replace('.', '_') + ".dat";
            using (StreamReader streamReader = new StreamReader(correct_filename))
            {
                LoadedData = streamReader.ReadToEnd();
            }

            switch (ChoosenFormat)
            {
                case "Text": LoadedDataConvertedBack = LoadedData; break;
                case "JSON": LoadedDataConvertedBack = JsonSerializer.Deserialize<string>(LoadedData); break;
                case "XML":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                        //LoadedDataConvertedBack = xmlSerializer.Deserialize(new MemoryStream());
                        break;
                    }
            }
            DataReceived(correct_filename);

        }
        internal void LoadFromNet(string net_destination)
        {
            LoadedData = Data_in_choosen_format;
            //Console.WriteLine("\nFormatted data was loaded from net");
            switch (ChoosenFormat)
            {
                case "Text": LoadedDataConvertedBack = LoadedData; break;
                case "JSON": LoadedDataConvertedBack = JsonSerializer.Deserialize<string>(LoadedData); break;
                case "XML":
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                        //LoadedDataConvertedBack = xmlSerializer.Deserialize(new MemoryStream());
                        break;
                    }
            }
            DataReceived(net_destination);
        }

        internal void DataSent_handler(string msg) 
        {
            // После выполнения отправки очистим список вызовов события, чтобы использовать снова извне класса
            ActionWithData = delegate { };
            Console.WriteLine($"Data successfully sent to {msg}"); 
        }
        internal void DataReceived_handler(string msg)
        {
            // После выполнения получения очистим список вызовов события, чтобы использовать снова извне класса
            ActionWithData = delegate { };
            Console.WriteLine($"Data successfully recieved from {msg}");
        }


        /*        Реализуйте методы для передачи данных в каждом из трех форматов(текстовый файл, JSON и XML) внутри класса "DataTransfer". 


                В каждом методе: 

                Создайте делегат, который будет отвечать за запись и чтение данных в нужном формате.
                Используйте делегат, чтобы записать или прочитать данные из файла соответствующего формата.
                Генерируйте событие "DataSent" или "DataReceived" после успешной передачи или получения данных.
                Создайте методы в главном классе программы для демонстрации работы вашей реализации скрытой передачи данных:*/

    }
}
