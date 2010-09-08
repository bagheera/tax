using System;

namespace InstaTax.Core.DomainObjects{
    public class DuplicateUserException : Exception{
        public string Message { get; set; }

        public DuplicateUserException(string message){
            Message = message;
        }
    }
}