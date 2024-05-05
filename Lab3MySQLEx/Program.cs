using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;


namespace Lab3MySQLEx
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();

            Console.WriteLine("1. Переглянути дані");
            Console.WriteLine("2. Змінити дані");
            Console.WriteLine("3. Додати новий запис");
            Console.WriteLine("Виберіть опцію:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowData(connection);
                    break;
                case "2":
                    UpdateData(connection);
                    break;
                case "3":
                    InsertData(connection);
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }

            connection.Close();
        }

        private static void ShowData(MySqlConnection connection)
        {
            ShowPlateje(connection);
            ShowSpozyvach(connection);
            ShowTaryfy(connection);
        }

        private static void ShowPlateje(MySqlConnection connection)
        {
            string queryPlateje = "SELECT * FROM plateje";
            MySqlCommand cmdPlateje = new MySqlCommand(queryPlateje, connection);
            MySqlDataReader readerPlateje = cmdPlateje.ExecuteReader();

            while (readerPlateje.Read())
            {
                string platejeId = readerPlateje["plateje_id"].ToString();
                string platejeData = readerPlateje["plateje_data"].ToString();
                string vnesSumma = readerPlateje["vnes_summa"].ToString();
                string nrRahunku = readerPlateje["nr_rahunku"].ToString();
                string popreredniPokazanya = readerPlateje["popreredni_pokazanya"].ToString();
                string aktualnyPokazanya = readerPlateje["aktualny_pokazanya"].ToString();
                string spozyvachId = readerPlateje["spozyvach_id"].ToString();
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine($"Plateje ID: {platejeId}, Plateje Data: {platejeData}, Vnes Summa: {vnesSumma}, Nr Rahunku: {nrRahunku}, Popreredni Pokazanya: {popreredniPokazanya}, Aktualny Pokazanya: {aktualnyPokazanya}, Spozyvach ID: {spozyvachId}");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }

            readerPlateje.Close();
        }

        private static void ShowSpozyvach(MySqlConnection connection)
        {
            string querySpozyvach = "SELECT * FROM spozyvach";
            MySqlCommand cmdSpozyvach = new MySqlCommand(querySpozyvach, connection);
            MySqlDataReader readerSpozyvach = cmdSpozyvach.ExecuteReader();

            while (readerSpozyvach.Read())
            {
                string spozyvachId = readerSpozyvach["spozyvach_id"].ToString();
                string name = readerSpozyvach["name"].ToString();
                string pokazLichPoperedni = readerSpozyvach["pokaz_lich_poperedni"].ToString();
                string zaborgZaPoperedni = readerSpozyvach["zaborg_za_poperedni"].ToString();
                string taryfyId = readerSpozyvach["taryfy_id"].ToString();
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine($"Spozyvach ID: {spozyvachId}, Name: {name}, Pokaz Lich Poperedni: {pokazLichPoperedni}, Zaborg Za Poperedni: {zaborgZaPoperedni}, Taryfy ID: {taryfyId}");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }

            readerSpozyvach.Close();
        }

        private static void ShowTaryfy(MySqlConnection connection)
        {
            string queryTaryfy = "SELECT * FROM taryfy";
            MySqlCommand cmdTaryfy = new MySqlCommand(queryTaryfy, connection);
            MySqlDataReader readerTaryfy = cmdTaryfy.ExecuteReader();

            while (readerTaryfy.Read())
            {
                string taryfyId = readerTaryfy["taryfy_id"].ToString();
                string nameKategorii = readerTaryfy["name_kategorii"].ToString();
                string rozmirOplaty = readerTaryfy["rozmir_oplaty"].ToString();
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine($"Taryfy ID: {taryfyId}, Name Kategorii: {nameKategorii}, Rozmir Oplaty: {rozmirOplaty}");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }

            readerTaryfy.Close();
        }
    

        private static void InsertData(MySqlConnection connection)
        {
            Console.WriteLine("Введіть назву таблиці, в яку потрібно додати новий запис (plateje, spozyvach):");
            string table = Console.ReadLine();

            switch (table)
            {
                case "plateje":
                    InsertPlateje(connection);
                    break;
                case "spozyvach":
                    InsertSpozyvach(connection);
                    break;
                default:
                    Console.WriteLine("Невірна назва таблиці.");
                    break;
            }
        }
        private static void InsertPlateje(MySqlConnection connection)
        {
            Console.WriteLine("Введіть дані для нового запису в таблицю plateje:");

            Console.WriteLine("Введіть дату платежу (у форматі рррр-мм-дд):");
            string platejeData = Console.ReadLine();

            Console.WriteLine("Введіть суму внеску:");
            decimal vnesSumma = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Введіть номер рахунку:");
            string nrRahunku = Console.ReadLine();

            Console.WriteLine("Введіть попередні показання:");
            float popreredniPokazanya = float.Parse(Console.ReadLine());

            Console.WriteLine("Введіть актуальні показання:");
            float aktualnyPokazanya = float.Parse(Console.ReadLine());

            Console.WriteLine("Введіть ID споживача:");
            int spozyvachId = int.Parse(Console.ReadLine());

            string insertQuery = "INSERT INTO plateje (plateje_data, vnes_summa, nr_rahunku, popreredni_pokazanya, aktualny_pokazanya, spozyvach_id) " +
                                 "VALUES (@plateje_data, @vnes_summa, @nr_rahunku, @popreredni_pokazanya, @aktualny_pokazanya, @spozyvach_id)";
            MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);
            insertCmd.Parameters.AddWithValue("@plateje_data", platejeData);
            insertCmd.Parameters.AddWithValue("@vnes_summa", vnesSumma);
            insertCmd.Parameters.AddWithValue("@nr_rahunku", nrRahunku);
            insertCmd.Parameters.AddWithValue("@popreredni_pokazanya", popreredniPokazanya);
            insertCmd.Parameters.AddWithValue("@aktualny_pokazanya", aktualnyPokazanya);
            insertCmd.Parameters.AddWithValue("@spozyvach_id", spozyvachId);

            float rowsAffected = insertCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Дані успішно додано до таблиці plateje!");
            }
            else
            {
                Console.WriteLine("Помилка при додаванні даних до таблиці plateje.");
            }
        }

        private static void InsertSpozyvach(MySqlConnection connection)
        {
            Console.WriteLine("Введіть дані для нового запису в таблицю spozyvach:");

            Console.WriteLine("Введіть ім'я споживача:");
            string name = Console.ReadLine();

            Console.WriteLine("Введіть показник лічильника на початок періоду:");
            float pokazLichPoperedni = float.Parse(Console.ReadLine());

            Console.WriteLine("Введіть заборгованість за попередній період:");
            double zaborgZaPoperedni = double.Parse(Console.ReadLine());

            Console.WriteLine("Введіть ID тарифу:");
            int taryfyId = int.Parse(Console.ReadLine());

            string insertQuery = "INSERT INTO spozyvach (name, pokaz_lich_poperedni, zaborg_za_poperedni, taryfy_id) " +
                                 "VALUES (@name, @pokaz_lich_poperedni, @zaborg_za_poperedni, @taryfy_id)";
            MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection);
            insertCmd.Parameters.AddWithValue("@name", name);
            insertCmd.Parameters.AddWithValue("@pokaz_lich_poperedni", pokazLichPoperedni);
            insertCmd.Parameters.AddWithValue("@zaborg_za_poperedni", zaborgZaPoperedni);
            insertCmd.Parameters.AddWithValue("@taryfy_id", taryfyId);

            int rowsAffected = insertCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Дані успішно додано до таблиці spozyvach!");
            }
            else
            {
                Console.WriteLine("Помилка при додаванні даних до таблиці spozyvach.");
            }
        }

     
        
        private static void UpdateData(MySqlConnection connection)
        {
            Console.WriteLine("Введіть ID запису, який потрібно змінити (plateje, spozyvach, taryfy):");
            string table = Console.ReadLine();

            switch (table)
            {
                case "plateje":
                    UpdatePlateje(connection);
                    break;
                case "spozyvach":
                    UpdateSpozyvach(connection);
                    break;
                case "taryfy":
                    UpdateTaryfy(connection);
                    break;
                default:
                    Console.WriteLine("Невірна назва таблиці.");
                    break;
            }
        }

        private static void UpdatePlateje(MySqlConnection connection)
        {
            Console.WriteLine("Введіть ID запису, який потрібно змінити:");
            int recordId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть нове значення для суми внеску:");
            decimal newVnesSumma = decimal.Parse(Console.ReadLine());

            string updateQuery = "UPDATE plateje SET vnes_summa = @vnes_summa WHERE plateje_id = @plateje_id";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
            updateCmd.Parameters.AddWithValue("@vnes_summa", newVnesSumma);
            updateCmd.Parameters.AddWithValue("@plateje_id", recordId);
            float rowsAffected = updateCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Дані успішно змінено!");
            }
            else
            {
                Console.WriteLine("Помилка при зміні даних.");
            }

        }
        private static void UpdateSpozyvach(MySqlConnection connection)
        {
            Console.WriteLine("Введіть ID запису, який потрібно змінити:");
            int recordId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть нову заборгованість:");
            double newZaborgZaPoperedni = double.Parse(Console.ReadLine());

            string updateQuery = "UPDATE spozyvach SET zaborg_za_poperedni = @zaborg_za_poperedni WHERE spozyvach_id = @spozyvach_id";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
            updateCmd.Parameters.AddWithValue("@zaborg_za_poperedni", newZaborgZaPoperedni);
            updateCmd.Parameters.AddWithValue("@spozyvach_id", recordId);
            int rowsAffected = updateCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Дані успішно змінено!");
            }
            else
            {
                Console.WriteLine("Помилка при зміні даних.");
            }
        }

        private static void UpdateTaryfy(MySqlConnection connection)
        {
            Console.WriteLine("Введіть ID запису, який потрібно змінити:");
            int recordId = int.Parse(Console.ReadLine());

            Console.WriteLine("Введіть новий розмір оплати:");
            double newRozmirOplaty = double.Parse(Console.ReadLine());

            string updateQuery = "UPDATE taryfy SET rozmir_oplaty = @rozmir_oplaty WHERE taryfy_id = @taryfy_id";
            MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection);
            updateCmd.Parameters.AddWithValue("@rozmir_oplaty", newRozmirOplaty);
            updateCmd.Parameters.AddWithValue("@taryfy_id", recordId);
            int rowsAffected = updateCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Дані успішно змінено!");
            }
            else
            {
                Console.WriteLine("Помилка при зміні даних.");
            }
        }
    }
}
