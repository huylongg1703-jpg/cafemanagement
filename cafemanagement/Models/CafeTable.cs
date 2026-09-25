namespace CafeManagement.API.Models
{
    public class CafeTable
    {
        public int TableId { get; set; }

        public string TableName { get; set; } = string.Empty;

        public int AreaId { get; set; }

        public int Capacity { get; set; }

        public string Status { get; set; } = "Available";

        public bool IsDeleted { get; set; }
    }
}