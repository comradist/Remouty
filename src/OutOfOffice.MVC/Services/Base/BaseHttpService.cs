﻿using OutOfOffice.MVC.Contracts;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OutOfOffice.MVC.Services.Base;

public class BaseHttpService
{
    protected IClient _client;

    public BaseHttpService(IClient client)
    {
        _client = client;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<Guid>() { Message = "Validation errors have occured.", ValidationErrors = ex.Response, Success = false };
        }
        else if (ex.StatusCode == 404)
        {
            return new Response<Guid>() { Message = "The requested item could not be found.", Success = false };
        }
        else
        {
            return new Response<Guid>() { Message = "Something went wrong, please try again.", Success = false };
        }
    }

    protected void AddBearerToken()
    {
        // if (_localStorage.Exists("token"))
        //     _client.HttpClient.DefaultRequestHeaders.Authorization =
        //         new AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
    }
}

