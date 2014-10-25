using System;

namespace DAL.DomainModel
{
    public class Product: IEntity
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public decimal Cost { get; set; }
    }
}