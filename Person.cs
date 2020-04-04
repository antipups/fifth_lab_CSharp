using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;


namespace fifth_lab
{
    class Person
    {
        static List<Month> months = new List<Month>();

        public static void start()
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

        public bool write_user_data(String titleMonth, String preAmountDays, String pathToImage)
        {
            // проверка на ввод нормального месяца (не пустая строка)
            if (string.IsNullOrEmpty(titleMonth)) return false;
            int amountDays;
            try { amountDays = Convert.ToInt32(preAmountDays);}
            catch (Exception e) { return false; }
            if (!File.Exists(pathToImage)) return false;
            // добавляем по отдельности каждый день в массив дней и пускаем его в класс с месяцами
            int[] amountDaysInMonth = new int[amountDays];
            for (int j = 0; j < amountDays; j++) amountDaysInMonth[j] = j + 1;
            months.Add(new Month(titleMonth, amountDaysInMonth, pathToImage));
            return true;
        }

        public void write_template()
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
                months.Add(new Month(titlesMonths[i], amountDaysInMonth, @"C:\Users\kurku\Pictures\For5Lab\2.jpg"));
            }
        }
        
        public void write_to_xml()
        {
            XmlDocument xDoc = new XmlDocument();
            
            // загружаем файл который будем записывать, если какая-то ошибка, то создаем новый
            try { xDoc.Load(@"C:\Users\kurku\Documents\C#Projects\fifth_lab_C#\fifth_lab\testFile.xml"); }
            catch (Exception e)
            { File.WriteAllText(@"C:\Users\kurku\Documents\C#Projects\fifth_lab_C#\fifth_lab\testFile.xml",
                    "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<root>\n\n</root>"); }
            
            XmlElement root = xDoc.DocumentElement;
            // чистим весь файл пере записью
            root?.RemoveAll();
            foreach (Month i in months)
            {
                XmlElement month = xDoc.CreateElement("Месяц");
                XmlElement titleMonth = xDoc.CreateElement(name: "Название");
                titleMonth.AppendChild(xDoc.CreateTextNode(i.TitleMonth));
                month.AppendChild(titleMonth);
                XmlElement picture = xDoc.CreateElement("Картинка");
                
                // конвертируем изображения в цифры, чтоб сериализовать норм
                byte[] kek;
                using (var stream = new MemoryStream())
                {
                    i.image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    kek = stream.ToArray();
                }
                String str = "";
                foreach (var VARIABLE in kek)
                {
                    str += VARIABLE.ToString();
                }

                picture.AppendChild(xDoc.CreateTextNode(str));
                month.AppendChild(picture);

                foreach (var one_day in i.EveryDayMonth)
                {
                    XmlElement day = xDoc.CreateElement("День");
                    XmlAttribute attr = xDoc.CreateAttribute("day");
                    attr.AppendChild(xDoc.CreateTextNode(one_day.ToString()));
                    XmlAttribute secondAttr = xDoc.CreateAttribute("codeDay");
                    secondAttr.AppendChild(xDoc.CreateTextNode(i.SortList[one_day - 1].codeDay.ToString()));
                    XmlAttribute thirdAttr = xDoc.CreateAttribute("averageTemperature");
                    thirdAttr.AppendChild(xDoc.CreateTextNode(i.SortList[one_day - 1].averageTemperature.ToString(CultureInfo.InvariantCulture)));
                    day.Attributes.Append(attr);
                    day.Attributes.Append(secondAttr);
                    day.Attributes.Append(thirdAttr);
                    month.AppendChild(day);
                }
                root.AppendChild(month);
            }
            
            xDoc.AppendChild(root);
            xDoc.Save(@"C:\Users\kurku\Documents\C#Projects\fifth_lab_C#\fifth_lab\testFile.xml");
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

        public Bitmap image;

        public Month(string titleMonth, int[] everyDayMonth, String pathToImage)
        {
            this.TitleMonth = titleMonth;
            this.EveryDayMonth = everyDayMonth;
            Random rnd = new Random();
            // заполняем класс для условия случайными числами (чтоб пользователь не задолбался)
            for (int i = 0; i < everyDayMonth.Length; i++)
            {
                try { SortList.Add(i, new InfoAboutEveryDayInMonth(i, rnd.Next(-30, 50))); }
                catch (Exception e) { continue; }
            }
            image = new Bitmap(pathToImage);
        }
    }
    
}