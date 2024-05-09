using MySql.Data.MySqlClient;

namespace Insert;

public class InsertOutcomesCourses
{
    public static void insert_outcomes_courses(string connectionString, string filePath)
    {
        
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string headerLine = reader.ReadLine();
                    string[] outcomeIds = headerLine.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        string courseId = values[0];

                        for (int i = 1; i < values.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(values[i]))
                            {
                                int outcomeId = int.Parse(outcomeIds[i - 1]);
                                InsertCourseOutcome(connection, courseId, outcomeId);
                            }
                        }
                    }
                }

                Console.WriteLine("Data inserted successfully into the courses_outcomes table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    static void InsertCourseOutcome(MySqlConnection connection, string courseId, int outcomeId)
    {
        string query = "INSERT INTO courses_outcomes (course_id, outcome_id) VALUES (@courseId, @outcomeId)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@courseId", courseId);
        cmd.Parameters.AddWithValue("@outcomeId", outcomeId);

        cmd.ExecuteNonQuery();
    }
}