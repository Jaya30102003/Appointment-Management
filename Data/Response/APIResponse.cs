using System.Collections.Generic;
using Appointments.Model;
using FluentValidation.Results;

namespace Appointments.Api.Data.Responses;

public class ApiResponse<T>
{
    private bool v1;
    private string v2;
    private Appointment result;

    public bool Success { get; set; }
    public string Message { get; set; }

    public T? Data { get; set; }

    public List<ValidationFailure>? Errors { get; set; }

    public ApiResponse(bool success, string message, T data, List<ValidationFailure>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors;
    }

    public ApiResponse(bool v1, string v2, Appointment result)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.result = result;
    }
}