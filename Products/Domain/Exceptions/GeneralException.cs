using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Products.Domain.Exceptions
{
    public class GeneralException:ApplicationException
    {
      
         public HttpStatusCode StatusCode { get; private set; }
        public GeneralException(){}

        public GeneralException(HttpStatusCode statusCode, string message) 
            : base(message)
        {
            StatusCode = statusCode;
        }

        public GeneralException(HttpStatusCode statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }

    public class BadRequestException : GeneralException
    {
        public BadRequestException(string message = "Bad request.")
            : base(HttpStatusCode.BadRequest, message) { }
    }

    public class NotFoundException : GeneralException
    {
        public NotFoundException(string message = "Resource not found.")
            : base(HttpStatusCode.NotFound, message) { }
    }

    public class UnauthorizedException : GeneralException
    {
        public UnauthorizedException(string message = "Unauthorized access.")
            : base(HttpStatusCode.Unauthorized, message) { }
    }

    // Nova exceção 1: Forbidden (403)
    public class ForbiddenException : GeneralException
    {
        public ForbiddenException(string message = "Forbidden.")
            : base(HttpStatusCode.Forbidden, message) { }
    }

    // Nova exceção 2: Conflict (409)
    public class ConflictException : GeneralException
    {
        public ConflictException(string message = "Conflict occurred.")
            : base(HttpStatusCode.Conflict, message) { }
    }

    // Nova exceção 3: Internal Server Error (500)
    public class InternalServerErrorException : GeneralException
    {
        public InternalServerErrorException(string message = "An unexpected error occurred.")
            : base(HttpStatusCode.InternalServerError, message) { }
    }
    
}