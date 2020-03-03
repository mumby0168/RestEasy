using System;

namespace RestEasy.Core.Exceptions
{
    public class RestEasyHandlerNotRegisteredException : Exception
    {
        public RestEasyHandlerNotRegisteredException(Type t, Exception inner) : 
            base($"The handler of type {t} has not been registered as a handler therefore could not be resolved..", inner)
        {
        }

        public RestEasyHandlerNotRegisteredException(Type type) : base(
            $"The handler of type {type} has not been registered as a handler therefore could not be resolved..")
        {
            
        }
    }
}