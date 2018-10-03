namespace vprokkis.Repositories
{
    public interface IRepository
    {
        Task<List<Dog>> GetDog(string id);
        Task<List<Dog>> GetAllBySex(int sex);
        Task<List<Dog>> GetDogByName(string name);
        Task<List<Dog>> GetTopDog();
        Task<List<Dog>> GetAllDogs();
        Task<Dog> CreateDog(Dog dog);
        Task<Dog> UpdateDog(string id, UpdatedDog dog);
        Task<Dog> DeleteDog(string id);

        Task<Trophy> GetTrophy(ObjectId id, ObjectId dogId);
        Task<List<Trophy>> GetAllTrophies(ObjectId dogId);
        Task<Trophy> CreateTrophy(Trophy trophy, ObjectId dogId);
        Task<Trophy> UpdateTrophy(ObjectId id, UpdatedTrophy trophy, ObjectId dogId);
        Task<bool> DeleteTrophy(ObjectId id, ObjectId dogId);  
    }
}