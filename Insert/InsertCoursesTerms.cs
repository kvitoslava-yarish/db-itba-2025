using MySql.Data.MySqlClient;

namespace Insert;

public class InsertCoursesTerms
{
    public static void insertCoursesTerms(string connectionString, string filePath)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');
                        int programId = 1; // Set program_id to 1 as required

                        int termId = int.Parse(values[1]);
                        string courseId = values[0].Trim();

                        InsertProgramTermCourse(connection, programId, termId, courseId);
                    }
                }

                Console.WriteLine("Data inserted successfully into the programs_terms_courses table.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    static void InsertProgramTermCourse(MySqlConnection connection, int programId, int termId, string courseId)
    {
        string query = "INSERT INTO programs_terms_courses (program_id, term_id, course_id) VALUES (@programId, @termId, @courseId)";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@programId", programId);
        cmd.Parameters.AddWithValue("@termId", termId);
        cmd.Parameters.AddWithValue("@courseId", courseId);

        cmd.ExecuteNonQuery();
    }
}