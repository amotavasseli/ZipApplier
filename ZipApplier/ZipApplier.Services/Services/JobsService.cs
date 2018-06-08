﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipApplier.Services.Domains;

namespace ZipApplier.Services.Services
{
    public class JobsService : IJobsService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ZipApplierConnection"].ConnectionString;

        public List<Job> GetAllJobs()
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlWrapper().Wrapper("Jobs_getall", con);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Job> jobs = null;

                    while (reader.Read())
                    {
                        Job job = new Job();
                        job.Id = reader.GetInt32(0);
                        job.JobId = reader.GetString(1);
                        job.Title = reader.GetString(2);
                        job.Url = reader.GetString(3);
                        job.Company = reader.GetString(4);
                        job.Description = reader.GetString(5);
                        job.Location = reader.GetString(6);
                        job.DateApplied = reader["date_applied"] is DBNull ? (DateTime?)null : (DateTime?)reader["date_applied"];
                        job.Archived = reader.GetBoolean(8);
                        job.QuickApply = reader.GetBoolean(9);
                        job.DateCreated = reader.GetDateTime(10);
                        job.DateModified = reader.GetDateTime(11);
                        if (jobs == null)
                            jobs = new List<Job>();
                        jobs.Add(job);
                    }
                    return jobs;
                }
            }
        }
        public Job GetById(int id)
        {
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlWrapper().Wrapper("jobs_getbyid", con);
                cmd.Parameters.AddWithValue("@id", id);
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    Job job = new Job();
                    reader.Read();
                    job.Id = reader.GetInt32(0);
                    job.JobId = reader.GetString(1);
                    job.Title = reader.GetString(2);
                    job.Url = reader.GetString(3);
                    job.Company = reader.GetString(4);
                    job.Description = reader.GetString(5);
                    job.Location = reader.GetString(6);
                    job.DateApplied = reader["date_applied"] is DBNull ? (DateTime?)null : (DateTime?)reader["date_applied"];
                    job.Archived = reader.GetBoolean(8);
                    job.QuickApply = reader.GetBoolean(9);
                    job.DateCreated = reader.GetDateTime(10);
                    job.DateModified = reader.GetDateTime(11);
                    return job;
                }
            }
        }
    }
}
