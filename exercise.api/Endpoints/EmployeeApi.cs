using exercise.api.Models;
using exercise.api.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace exercise.api.Endpoints
{
    public static class EmployeeApi
    {

        public static void ConfigureEmployeeApi(this WebApplication app)
        {
            app.MapGet("/employees", GetEmployees);
            app.MapGet("/employee/{id}", GetEmployee);
            app.MapPost("/employees", AddEmployee);
            app.MapPut("/employees", UpdateEmployee);
            app.MapDelete("/employees", DeleteEmployee);
        }

       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetEmployees(IRepository repository) //mogelijk 2e met hoofdletter de repository
        {
            try
            {
                return await Task.Run(() => {
                    return Results.Ok(repository.GetEmployees());
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> AddEmployee(Employee employee, IRepository service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (service.AddEmployee(employee)) return Results.Created($"https://localhost:7242/{employee.id}", employee);
                    return Results.NotFound(); //hier wellicht wat vraagtekens ivm de 201 en localhost
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetEmployee(int id, IRepository service)
        {
            try
            {
                var employee = service.GetEmployee(id); // Gebruik async methode

                if (employee != null)
                {
                    return Results.Ok(employee); // Return een 200 OK-response met het gevonden Employee-object
                }
                else
                {
                    return Results.NotFound(); // Return een 404 Not Found-response als er geen employee wordt gevonden
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateEmployee(Employee employee, IRepository service)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (service.UpdateEmployee(employee)) return Results.Ok();
                    return Results.NotFound();
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteEmployee(int id, IRepository service)
        {
            try
            {
                if (service.DeleteEmployee(id)) return Results.Ok();
                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }








    }
}
