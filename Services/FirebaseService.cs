using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using KeepMyPass.Data;

namespace KeepMyPass.Services
{
    public class FirebaseService
    {
        #region Firebase configurations
        private IFirebaseClient _client;
        private IFirebaseConfig _ifc = new FirebaseConfig()
        {
            AuthSecret = "yLtODNbm8emFnf3ZMkWYbRO6QKtyeHcVivT4Ec0Q",
            BasePath = "https://keepmypass-74e43-default-rtdb.europe-west1.firebasedatabase.app"
        };
        #endregion

        #region C'TOR
        public FirebaseService()
        {
            try
            {
                _client = new FirebaseClient(_ifc);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region Database actions
        public HttpStatusCode AddUser(User user)
        {
            string id = user.Email.Replace('.', ',');
            SetResponse response = _client.Set("/Users/" + id, user);
            return response.StatusCode;
        }
        #endregion
    }
}
