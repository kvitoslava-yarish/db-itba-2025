using System;
using System.IO;
using System.Collections.Generic;
using Insert;
using MySql.Data.MySqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=localhost;port=3306;user=root;password=my-secret-pw;database=itba;";
        string path = "C:\\Users\\kvita\\RiderProjects\\db-itba-2025\\courses_terms.csv";
        InsertCoursesTerms.insertCoursesTerms(connectionString, path);

    }
}
