﻿using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PizzaShop_Course.DataProvider
{
    public class UserDBConnection
    {
        private readonly MySqlConnection _connection;
        public UserDBConnection()
        {
            _connection = SqlDBConnection.GetDBConnection();
        }
        public bool CreateUser(UserModel user)
        {
            var connection = _connection;
            connection.Open();
            string query = "INSERT INTO users (change_roots, first_name, last_name, photo, login, pass, email, phone_number) " +
                       "VALUES (@changeRoots, @firstName, @lastName, @photo, @login, @pass, @email, @phone)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@changeRoots", user.ChangeRoots);
            command.Parameters.AddWithValue("@firstName", user.FirstName);
            command.Parameters.AddWithValue("@lastName", user.LastName);
            command.Parameters.AddWithValue("@photo", user.Photo);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@pass", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@phone", user.Number);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    EventAggregator.Instance.NotificationEvent.Publish("Login already exists");
                }
                else
                {
                    Debug.WriteLine("Sql exception: ", ex);
                }
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public UserModel AuthenticateUser(string login, string password)
        {
            var connection = SqlDBConnection.GetDBConnection();
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @login AND pass = @pass", connection);
            command.Parameters.AddWithValue("@login", login);
            command.Parameters.AddWithValue("@pass", password);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                UserModel user = new UserModel
                {
                    Id = (int)reader["id"],
                    FirstName = (string)reader["first_name"],
                    LastName = (string)reader["last_name"],
                    ChangeRoots = (bool)reader["change_roots"],
                    Login = (string)reader["login"],
                    Password = (string)reader["pass"],
                    Email = (reader["email"] == DBNull.Value) ? string.Empty : (string?)reader["email"],
                    Photo = (reader["photo"] == DBNull.Value) ? null : (byte[]?)reader["photo"],
                    Number = Convert.IsDBNull(reader["phone_number"]) ? string.Empty : (string)reader["phone_number"]
                };
                connection.Close();
                return user;
            }
            return null;
        }
        public void DeleteUser(int id)
        {
            try
            {
                var connection = SqlDBConnection.GetDBConnection();
                connection.Open();
                string query = "DELETE FROM users WHERE id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                connection.Close();
                EventAggregator.Instance.NotificationEvent.Publish("User was deleted");
            }
            catch (Exception ex)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Something went wrong");
            }
        }
        public void UpdateUser(UserModel user)
        {
            const string PasswordRegexPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
            Regex passwordRegex = new Regex(PasswordRegexPattern);

            if (!passwordRegex.IsMatch(user.Password))
            {
                EventAggregator.Instance.NotificationEvent.Publish("Password not suitable");
                return;
            }
            try
            {
                var connection = SqlDBConnection.GetDBConnection();
                connection.Open();
                var query = "UPDATE users SET change_roots = @change_roots, first_name = @first_name, last_name = @last_name, photo = @photo, pass = @password, email = @email, phone_number = @phone, id = @id WHERE login = @login";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@change_roots", user.ChangeRoots);
                command.Parameters.AddWithValue("@first_name", user.FirstName);
                command.Parameters.AddWithValue("@last_name", user.LastName);
                command.Parameters.AddWithValue("@photo", user.Photo);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@phone", user.Number);
                command.Parameters.AddWithValue("@id", user.Id);
                command.ExecuteNonQuery();
                connection.Close();
                EventAggregator.Instance.NotificationEvent.Publish("User was updated");
            }
            catch (Exception ex)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Something went wrong");
            }
        }
        public void UpdateByUser(UserModel user)
        {
            const string PasswordRegexPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
            Regex passwordRegex = new Regex(PasswordRegexPattern);

            if (!passwordRegex.IsMatch(user.Password))
            {
                EventAggregator.Instance.NotificationEvent.Publish("Password not suitable");
                return;
            }
            try
            {
                var connection = SqlDBConnection.GetDBConnection();
                connection.Open();
                var query = "UPDATE users SET first_name = @first_name, last_name = @last_name, photo = @photo, pass = @password, email = @email, phone_number = @phone WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@first_name", user.FirstName);
                command.Parameters.AddWithValue("@last_name", user.LastName);
                command.Parameters.AddWithValue("@photo", user.Photo);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@phone", user.Number);
                command.Parameters.AddWithValue("@id", user.Id);
                command.ExecuteNonQuery();
                connection.Close();
                EventAggregator.Instance.NotificationEvent.Publish("User was updated");
            }
            catch (Exception ex)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Something went wrong");
            }
        }
    }
}
