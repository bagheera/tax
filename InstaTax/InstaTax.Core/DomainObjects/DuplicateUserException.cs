using System;

namespace InstaTax.Core.DomainObjects{
    public class DuplicateUserException : Exception{
        public DuplicateUserException(string message) : base(message){
        }
    }
}