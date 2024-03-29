﻿namespace TaskDesk.Shared.Exceptions;

[Serializable]
public class ValidationException : Exception
{
    public ValidationException() : base() { }

    public ValidationException(string message) : base(message) { }

    public ValidationException(string message, Exception innerException) : base(message, innerException) { }

    protected ValidationException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}