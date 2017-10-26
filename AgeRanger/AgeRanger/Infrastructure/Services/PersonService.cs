using AgeRanger.Models;
using AgeRanger.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace AgeRanger.Infrastructure.Services
{
    public static class PersonService
    {

        private static int Count(SQLiteConnection conn,string search)
        {
            var query = @"SELECT count(*) FROM  Person WHERE (FirstName like @search OR LastName like @search);";

            return conn.Query<int>(query, new {
                search = $"%{search}%"
            }).FirstOrDefault();
        }

        private static IEnumerable<Profile> Get(SQLiteConnection conn, string search)
        {
            var query = @"SELECT Person.Id, Person.FirstName, Person.LastName, Person.Age, AgeGroup.Description 
                        FROM  Person LEFT JOIN AgeGroup 
                        ON (Person.Age >= AgeGroup.MinAge AND Person.Age < AgeGroup.MaxAge)
                        OR (Person.Age < AgeGroup.MaxAge AND (AgeGroup.MinAge is null OR AgeGroup.MinAge = ''))
                        OR (Person.Age >= AgeGroup.MinAge AND (AgeGroup.MaxAge is null OR AgeGroup.MaxAge = ''))
                        WHERE (FirstName like @search OR LastName like @search);";

            return conn.Query<Profile>(query, new
            {
                search = $"%{search}%"
            });
        }

        public static ProfileListViewModel Search(SQLiteConnection conn, string search)
        {
            return new ProfileListViewModel()
            {
                Count = Count(conn, search),
                Profiles = Get(conn, search)
            };
        }

        public static void Create(SQLiteConnection conn, ProfileCreateViewModel model)
        {
            var query = @"INSERT INTO Person (FirstName, LastName, Age)
                        VALUES (@fname, @lname, @age);";

            conn.Execute(query, new
            {
                fname = model.FirstName,
                lname = model.LastName,
                age = model.Age
            });
        }

        public static void DeleteProfile(SQLiteConnection conn, int id)
        {
            var query = @"DELETE FROM Person WHERE Id = @id;";

            conn.Execute(query, new
            {
                id = id
            });
        }

        public static Profile GetProfile(SQLiteConnection conn, int id)
        {
            var query = @"SELECT Id, FirstName, LastName, Age
                        FROM  Person
                        WHERE Id = @id;";

            return conn.Query<Profile>(query, new {
                id = id
            }).FirstOrDefault();
        }

        public static void ModifyProfile(SQLiteConnection conn, ProfileModifyViewModel model)
        {
            var query = @"UPDATE Person SET 
                        FirstName = @FirstName, 
                        LastName = @LastName, 
                        Age = @Age
                        WHERE Id = @Id;";

            conn.Execute(query, model);
        }

    }
}