namespace BasicDapperData.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetDataAsync<T,P>(string query, P parameters, string connId = "con");
        Task SaveDataAsync<P>(string query, P parameters, string connId = "con");
    }
}