using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;


namespace fifth_lab
{
    class Person
    {
        static List<Month> months = new List<Month>();

        static void start(string[] args)
        {
            Bitmap image1 = new Bitmap(@"C:\Users\kurku\Pictures\screens\2020-04-04_133703.png", true);

            // menu();
        }

        // static void menu()
        // {
            // while (true)
            // {
                // Console.WriteLine("\n\n1 - Использовать шаблонные данные;\n" +
                                  // "2 - Ввести данные;\n" +
                                  // "3 - Сериализовать данные (поместить в XML);\n" +
                                  // "4 - Десериализовать данные (считать в консоль);\n" +
                                  // "ESC - выход;\n" +
                                  // "Выш выбор:\n");
                // switch ((int)Console.ReadKey().KeyChar)
                // {
                    // case '1':
                        // write_template();
                        // break;
                    // case '2':
                        // write_user_data();
                        // break;
                    // case '3':
                        // write_to_xml();
                        // break;
                    // case '4':
                        // read_from_xml();
                        // break;
                    // case 27:
                        // return;
                // }   
            // }
        // }

        public static void write_user_data()
        {
            while (true)
            {
                Console.WriteLine("\n\nВведите месяц: ");
                String titleMonth;
                // проверка на ввод нормального месяца (не пустая строка)
                try
                {
                    titleMonth = Console.ReadLine();
                    if (string.IsNullOrEmpty(titleMonth)) continue;
                }
                catch (Exception e)    // если юзер ввел пустую строку
                { continue; }
                Console.WriteLine("Введите количество дней в месяце:");
                int amountDays = 0;
                while (amountDays <= 0)
                {
                    try { amountDays = Convert.ToInt32(Console.ReadLine());}
                    catch (Exception e) { continue; }
                }
                
                // добавляем по отдельности каждый день в массив дней и пускаем его в класс с месяцами
                int[] amountDaysInMonth = new int[amountDays];
                for (int j = 0; j < amountDays; j++) amountDaysInMonth[j] = j + 1;
                months.Add(new Month(titleMonth, amountDaysInMonth));
                
                Console.WriteLine("Хотите ввести ещё месяца?(1 - Да)");
                if (Console.ReadKey().KeyChar != 49) return;
            }
        }

        public static void write_template()
        {
            String[] titlesMonths = {
                "Январь", "Февраль", "Март", "Апрель", 
                "Май", "Июнь", "Июль", "Август", 
                "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };
            int[] arrayDays = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
            // добавляем в массив все месяца с их днями
            for (int i = 0; i < 12; i++)
            {
                int[] amountDaysInMonth = new int[arrayDays[i]];
                for (int j = 0; j < arrayDays[i]; j++) amountDaysInMonth[j] = j + 1;
                months.Add(new Month(titlesMonths[i], amountDaysInMonth));
            }
        }
        
        public static void write_to_xml()
        {
            XmlDocument xDoc = new XmlDocument();
            
            // загружаем файл который будем записывать, если какая-то ошибка, то создаем новый
            try { xDoc.Load("C:\\Users\\kurku\\Documents\\C#Projects\\test\\test\\test\\Person.xml"); }
            catch (Exception e)
            { File.WriteAllText("C:\\Users\\kurku\\Documents\\C#Projects\\test\\test\\test\\Person.xml",
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<root>\n\n</root>"); }
            
            XmlElement root = xDoc.DocumentElement;
            // чистим весь файл пере записью
            root.RemoveAll();
            foreach (Month i in months)
            {
                XmlElement month = xDoc.CreateElement("Месяц");
                XmlElement titleMonth = xDoc.CreateElement(name: "Название");
                titleMonth.AppendChild(xDoc.CreateTextNode(i.TitleMonth));
                month.AppendChild(titleMonth);
                foreach (var one_day in i.EveryDayMonth)
                {
                    
                    XmlElement day = xDoc.CreateElement("День");
                    XmlAttribute attr = xDoc.CreateAttribute("day");
                    attr.AppendChild(xDoc.CreateTextNode(one_day.ToString()));
                    day.Attributes.Append(attr);
                    month.AppendChild(day);
                }
                root.AppendChild(month);
            }
            
            xDoc.AppendChild(root);
            xDoc.Save("C:\\Users\\kurku\\Documents\\C#Projects\\test\\test\\test\\Person.xml");
        }

        public static void read_from_xml()
        {
            
        }
    }


    [Serializable]
    public class Month
    {
        public string TitleMonth { get; set; }
        public int[] EveryDayMonth;

        public class InfoAboutEveryDayInMonth
        {
            public int codeDay;
            public double averageTemperature;

            public InfoAboutEveryDayInMonth(int codeDay, double averageTemperature)
            {
                this.codeDay = codeDay;
                this.averageTemperature = averageTemperature;
            }
        }

        public SortedList<int, InfoAboutEveryDayInMonth> SortList = new SortedList<int, InfoAboutEveryDayInMonth>();
        
        

        public Month(string titleMonth, int[] everyDayMonth)
        {
            this.TitleMonth = titleMonth;
            this.EveryDayMonth = everyDayMonth;
            Random rnd = new Random();
            // заполняем класс для условия случайными числами (чтоб пользователь не задолбался)
            for (int i = 0; i < everyDayMonth.Length; i++)
            {
                int codeDay = rnd.Next(0, 100);
                try { SortList.Add(codeDay, new InfoAboutEveryDayInMonth(codeDay, rnd.Next(-30, 50))); }
                catch (Exception e) { continue; }
            }
            
        }
    }
    
}