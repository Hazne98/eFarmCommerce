using System;
using System.Collections.Generic;
using System.Text;

namespace eFarmCommerce.Model.Exceptions;

public class ClientException : Exception
{
    public int StatusCode { get; }

    public ClientException(string message, int statusCode = 400)
        : base(message)
    {
        StatusCode = statusCode;
    }
}