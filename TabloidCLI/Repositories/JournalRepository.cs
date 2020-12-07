using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id,
                                               Title,
                                               Content,
                                               CreateDateTime
                                          FROM Journal";

                    List<Journal> journal = new List<Journal>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal journalEntry = new Journal()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };
                        journal.Add(journalEntry);
                    }

                    reader.Close();

                    return journal;
                }
            }
        }

        public Journal Get(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT j.Title, j.Content, j.CreateDateTime
                                        FROM Journal j
                                        WHERE j.Id = @id;";

                    cmd.Parameters.AddWithValue("@id", id);

                    Journal journal = null;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (journal == null)
                        {
                            journal = new Journal()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Title = reader.GetString(reader.GetOrdinal("Title")),
                                Content = reader.GetString(reader.GetOrdinal("Content")),
                                CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            };
                        }
                    }

                    reader.Close();

                    return journal;
                }
            }
        }

        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal(Title, Content, CreateDateTime)
                                        VALUES (@title, @content, @CreateDateTime )";
                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@CreateDateTime", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal
                                           SET Title = @title,
                                               Content = @content,
                                               CreateDateTime = @createDateTime
                                         WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", journal.Title);
                    cmd.Parameters.AddWithValue("@content", journal.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", journal.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Journal WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
