using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    internal class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }

        public Customer(int id, string firstName, string lastName, string middleName) 
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.Name = lastName +" "+ firstName +" "+ middleName;
        }

        public Customer(Customer customer) 
        { 
            this.ID = customer.ID;
            this.FirstName = customer.FirstName;
            this.LastName = customer.LastName;
            this.MiddleName = customer.MiddleName;
            this.Name = customer.Name;
        }
    }
}
