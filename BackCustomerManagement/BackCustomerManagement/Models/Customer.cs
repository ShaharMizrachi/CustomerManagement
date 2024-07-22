﻿namespace BackCustomerManagement.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Date { get; set; }
        public string Phone { get; set; }
        public string? VersionHolding { get; set; }
    }
}
