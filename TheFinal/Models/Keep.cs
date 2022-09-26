namespace TheFinal.Models
{
    public class Keep: RepoItem<int>
    {
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Views { get; set; }
        public int Kept { get; set; }
        public Profile Creator {get; set;}
        
    }

    public class VaultedKeep : Keep 
    {
        public int VaultKeepId { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
    }
}