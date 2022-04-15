using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using UnityEngine;

namespace Beamable.Server
{
   [Microservice("UserDataService")]
   public class UserDataService : Microservice
   {
      [ClientCallable]
      public async Task<bool> SaveMessage(string message, int x, int y)
      {
         bool isSuccess = false;

         try
         {
            var db = await Storage.GetDatabase<UserDataStorage>();
            var collection = db.GetCollection<UserMessage>("messages");
            await collection.InsertOneAsync( new UserMessage()
            {
               Message = message,
               X = x,
               Y = y
            });
            
            isSuccess = true;
         }
         catch (Exception e)
         {
            Debug.LogError(e.Message);
         }

         return isSuccess;
      }
      
      [ClientCallable]
      public async Task<List<string>> GetMessage(int x, int y)
      {
          var db = await Storage.GetDatabase<UserDataStorage>();
          var collection = db.GetCollection<UserMessage>("messages");
          var messages = collection
             .Find(document => document.X == x && document.Y == y)
             .ToList();

         return messages.Select(m => m.Message).ToList();
      }
   }
}