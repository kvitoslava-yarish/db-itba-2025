using MySql.Data.MySqlClient;

namespace Insert;

public class InsertCopmetencesCourses
{
    public static void insert_comp_courses(string connectionString, string filePath)
    {
        
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                List<string> competenceIds = new List<string>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read the header line to get competence IDs
                    string headerLine = reader.ReadLine();
                    string[] competenceIdArray = headerLine.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    foreach (string competenceId in competenceIdArray)
                    {
                        competenceIds.Add(competenceId.Trim());
                    }

                    // Read the remaining lines
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] values = line.Split(',');

                        string courseId = values[0].Trim();
                        for (int i = 1; i < values.Length; i++)
                        {
                            if (values[i] == "1")
                            {
                                string competenceId = competenceIds[i - 1]; // Adjusting for zero-based indexing
                                string query = "INSERT INTO courses_competences (course_id, competence_id) VALUES (@courseId, @competenceId)";
                                Console.WriteLine(courseId, competenceId);
                                using (MySqlCommand command = new MySqlCommand(query, connection))
                                {
                                    command.Parameters.AddWithValue("@courseId", courseId);
                                    command.Parameters.AddWithValue("@competenceId", competenceId);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("Data inserted successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}