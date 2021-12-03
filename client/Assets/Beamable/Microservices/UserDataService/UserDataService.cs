using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using UnityEngine;

namespace Beamable.Server
{
   [Microservice("UserDataService")]
   public class UserDataService : Microservice
   {
      [ClientCallable]
      public bool SaveMessage(string message, int x, int y)
      {
         bool isSuccess = false;

         try
         {
            var db = Storage.GetDatabase<UserDataStorage>();
            var collection = db.GetCollection<UserMessage>("messages");
            collection.InsertOne( new UserMessage()
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
      public List<string> GetMessage(int x, int y)
      {
         var db = Storage.GetDatabase<UserDataStorage>();
         var collection = db.GetCollection<UserMessage>("messages");
         var messages = collection
            .Find(document => document.X == x && document.Y == y)
            .ToList();

         return messages.Select(m => m.Message).ToList();
      }
   }
}