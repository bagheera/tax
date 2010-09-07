using System;

namespace InstaTax.Core{
    public class User{

        public double RentPaid { get; set; }
        public bool? FromMetro { get; set; }

        public User(double rentPaid, bool? fromMetro){
            RentPaid = rentPaid;
            FromMetro = fromMetro;
        }
    }
}