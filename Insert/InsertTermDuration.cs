using System.Globalization;
namespace Insert;
using MySql.Data.MySqlClient;
public class InsertTermDuration
{
    public static void insertTermDuration(string connectionString, string path)
    {

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        int programId = 1;
                        int termId = int.Parse(values[0].Replace("терм", "").Trim());
                        
                        string[] dateRange = values[1].Split('-');
                        DateTime startDate = DateTime.ParseExact(dateRange[0].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        DateTime finishDate = DateTime.ParseExact(dateRange[1].Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        
                        int duration = int.Parse(values[2]);

                        InsertProgramTermDuration(connection, programId, termId, startDate, finishDate, duration);
                    }
                }

                Console.WriteLine("Data inserted successfully into the programs_terms_duration table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    static void InsertProgramTermDuration(MySqlConnection connection, int programId, int termId, DateTime startDate, DateTime finishDate, int duration)
    {
        string query = "INSERT INTO programs_terms_duration (program_id, term_id, start_date, finish_date, duration) VALUES (@programId, @termId, @startDate, @finishDate, @duration)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@programId", programId);
        cmd.Parameters.AddWithValue("@termId", termId);
        cmd.Parameters.AddWithValue("@startDate", startDate);
        cmd.Parameters.AddWithValue("@finishDate", finishDate);
        cmd.Parameters.AddWithValue("@duration", duration);

        cmd.ExecuteNonQuery();
    }
}