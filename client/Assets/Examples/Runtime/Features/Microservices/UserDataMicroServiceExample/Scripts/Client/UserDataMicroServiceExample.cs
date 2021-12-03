using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Beamable.Server.Clients;

namespace Beamable.Examples.Features.Microservices.UserDataMicroServiceExample
{
    /// <summary>
    /// Demonstrates <see cref="Microservices"/>.
    /// </summary>
    public class UserDataMicroServiceExample : MonoBehaviour
    {
        //  Properties  -----------------------------------
        
        //  Fields  ---------------------------------------
        private UserDataServiceClient _userDataServiceClient = null;
        
        //  Unity Methods  --------------------------------

        protected void Start()
        {
            Debug.Log("Start() Instructions...\n" + 
            "* Complete docker setup per https://docs.beamable.com/docs/microservices-feature\n" +
            "* Start the server per https://docs.beamable.com/docs/microservices-feature\n" +
            "* Play This Scene\n" + 
            "* View the Unity Console output\n" + 
            "* Enjoy!\n\n\n");
            
            SetupBeamable();
        }
        
        //  Methods  --------------------------------------
        private async void SetupBeamable()
        {
            var beamableAPI = await Beamable.API.Instance;

            Debug.Log($"beamableAPI.User.id = {beamableAPI.User.id}");
            
            _userDataServiceClient = new UserDataServiceClient();
            
            // #1 - Call Microservice
            bool isSuccess = await _userDataServiceClient.SaveMessage("Hello World!", 0, 0);
                
            // #2 - Result = true
            Debug.Log ($"SaveMessage() isSuccess = {isSuccess}");
            
            // #3 - Call Microservice
            List<string> messages = await _userDataServiceClient.GetMessage(0, 0);
                
            // #4 - Result = true
            Debug.Log ($"GetMessage() messages.Count = {messages.Count}, messages[0] = {messages[0]}");
        }
    }
}