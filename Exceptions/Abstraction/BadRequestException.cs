﻿using System;
namespace Domain.Exceptions
{
    public abstract class BadRequestException(string message) : Exception (message)
    {
    }
}
