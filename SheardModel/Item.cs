namespace SheardModel
{
    public class Item
    {
        public int Id { get;  set; }
        public string Name { get; protected set; }
        public string DateTime { get; protected set; }

        public Item(int id,string name)
        {
            Id = id;
            DateTime = System.DateTime.Now.ToString("yy-MM-dd H:mm:ss:ff");
            Name = name;
        }

    }
}