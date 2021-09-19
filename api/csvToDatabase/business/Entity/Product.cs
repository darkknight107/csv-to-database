using System.Data;

namespace business.Entity
{
    public class Product
    {
        public DataColumn ProductId { get; set; }
        public DataColumn ProductType { get; set; }
        public DataColumn ProductCode { get; set; }
        public DataColumn Season { get; set; }
        public DataColumn Price { get; set; }
        public DataColumn Name { get; set; }
        public DataColumn Description { get; set; }
        public DataColumn LaunchDate { get; set; }
    }
}