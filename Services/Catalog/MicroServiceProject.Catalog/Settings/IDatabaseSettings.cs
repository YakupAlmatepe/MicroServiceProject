namespace MicroServiceProject.Catalog.Settings
{
    public interface IDatabaseSettings
    {
        public string CategoryCollectionName { get; set; }//appsetting jsondaki keyler
        public string ProductCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}