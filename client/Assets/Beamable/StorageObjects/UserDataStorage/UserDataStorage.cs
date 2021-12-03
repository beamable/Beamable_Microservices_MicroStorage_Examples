using Beamable.Server;
using MongoDB.Bson;

namespace Beamable.Server
{
    [StorageObject("UserDataStorage")]
    public class UserDataStorage : MongoStorageObject
    {

    }

    public class UserMessage
    {
        public ObjectId Id;
        public string Message;
        public int X;
        public int Y;
    }
}
