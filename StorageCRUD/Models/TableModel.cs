namespace StorageCRUD.Models
{
    public class TableModel : Microsoft.Azure.Cosmos.Table.TableEntity
    {
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}
