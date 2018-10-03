namespace vprokkis.Repositories
{
    public class MongoDbRepository : IRepository
    {
        List<Dog> dogList = new List<Dog>();
        List<Trophy> trophyList = new List<Trophy>();
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017"); 

        
        var db = dbClient.GetDatabase("dogshow");
        var dogs = db.GetCollection<Dog>("dogs");

        public async Task<List<Dog>> GetDog(string id)
        {
            Dog result = new Dog();
            result = dogs.AsQueryable<Dog>().Where(p => p.id == id);
            if (result.id == id) {
                return result.ToList();
            }
            else {
                IdNotFound argEx = new IdNotFound("No dog with given ID was found");
                throw argEx;
                return null;
            }
        }

        public async Task<List<Dog>> GetAllBySex(int sex)
        {
            return dogs.AsQueryable<Dog>().Where(p => p.Sex == sex).ToList();
        }

        public async Task<List<Dog>> GetDogByName(string name)
        {
            return dogs.AsQueryable<Dog>().Where(p => p.Name == name).ToList();
        }
        public async Task<List<Dog>> GetTopDog()
        {
            var result = dogs.AsQueryable<Dog>();
            var resultlist = result.ToList();
            int tmp = 0;
            Dog topDog = new Dog();
            foreach(var dog in resultlist){
                if (dog.TrophyHistory.Count > tmp){
                    tmp = dog.TrophyHistory.Count;
                    topDog = dog;
                }
            }
            return topDog.ToList();
        }

        public async Task<List<Dog>> GetAllDogs()
        {
            var result = dogs.AsQueryable<Dog>();
            var resultlist = result.ToList();
            
            return resultlist;
        }

        public async Task<Dog> CreateDog(Dog dog)
        {
            Dog d = new Dog{
                id = dog.id,
                Name = dog.Name,
                Age = 0,
                Sex = dog.Sex,
                ValidHealthCheck = false,
                TrophyHistory = new List<Trophy>()
            };
            dogs.InsertOne(p.ToBsonDocument());
            dogList.Add(dog);
            return d;
        }

        public async Task<Dog> UpdateDog(string id, UpdatedDog dog)
        {
            var dogId = new ObjectId(id);
            var result = dogs.AsQueryable<Dog>().SingleOrDefault(p => p.id == playerId);
            result.Age = dog.Age;
            result.Sex = dog.Sex;
            result.ValidHealthCheck = dog.ValidHealthCheck;
            result.TrophyHistory = dog.TrophyHistory;
            return result;
        }
        public async Task<Dog> DeleteDog(string id)
        {
            var filter = Builders<Dog>.Filter.Eq("id", id);
            var result = dogs.DeleteOne(filter);
            return null;
        }
        
        public async Task<Trophy> GetTrophy(ObjectId id, ObjectId dogId)
        {
            var filter = Builders<Dog>.Filter.Eq("id", dogId);
            var result = dogs.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.TrophyHistory.Count; i++)
            {
                if (result.TrophyHistory[i].id == id)
                    return result.TrophyHistory[i];
            }
            return null;
        }

        public async Task<List<Trophy>> GetAllTrophies(ObjectId dogId)
        {
            var filter = Builders<Dog>.Filter.Empty;
            var result = dogs.Find(filter).SingleOrDefault();
            return result.TrophyHistory;
        }

        public async Task<Trophy> CreateTrophy(Trophy trophy, ObjectId dogId)
        {
            var filter = Builders<Dog>.Filter.Eq("id", dogId);
            var result = dogs.Find(filter).SingleOrDefault();

            if (result.ValidHealthCheck == false)
            {
                HealthCheckValidation argEx = new HealthCheckValidation("Health Check Validation: Dog must have passed a valid health check to win");
                throw argEx;
                return null;
            }

            result.TrophyHistory.Add(trophy);
            trophyList.Add(Trophy);
            return item;
        }

        public async Task<Trophy> UpdateTrophy(ObjectId id, UpdatedTrophy trophy, ObjectId dogId)
        {
            var filter = Builders<Dog>.Filter.Eq("id", dogId);
            var result = dogs.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.TrophyHistory.Count; i++)
            {
                if (result.TrophyHistory[i].id == id)
                {
                    result.TrophyHistory[i].dogId = trophy.dogId;
                    return result.TrophyHistory[i];
                }
            }
            return null;
        }
        public async Task<bool> DeleteTrophy(ObjectId id, ObjectId dogId)
        {
            var filter = Builders<Dog>.Filter.Eq("id", dogId);
            var result = dogs.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.TrophyHistory.Count; i++)
            {
                if (result.TrophyHistory[i].id == id)
                {
                    result.TrophyHistory.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}