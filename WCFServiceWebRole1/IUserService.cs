
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WCFServiceWebRole1
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebInvoke(
           Method = "GET",
           UriTemplate = "/login/{username}/{password}",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped)]

        int login(string username, string password);
        [OperationContract]
        [WebInvoke(
          Method = "GET",
          UriTemplate = "/logout/{username}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]

        int logout(string username);

        [OperationContract]
        [WebInvoke(
          Method = "POST",
          UriTemplate = "/user?" +
                        "nameUser={nameUser}&" +
                        "surname1User={surname1User}&" +
                        "surname2User={surname2User}&"+
                        "mailUser={mailUser}&" +
                        "nicknameUser={nicknameUser}&" +
                        "passUser={passUser}&" +
                        "roleUser={roleUser}&" +
                        "birthdateUser={birthdateUser}&" +
                        "countryUser={countryUser}&" +
                        "addressUser={addressUser}&" +
                        "collegeUser={collegeUser}&" +
                        "phoneUser={phoneUser}&" +
                        "photoUser={photoUser}&" +
                        "descriptionUser={descriptionUser}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]
        String createUser(string nameUser,
                       string surname1User,
                       string surname2User,
                       string mailUser,
                       string nicknameUser,
                       string passUser,
                       string roleUser,
                       string birthdateUser,
                       string countryUser,
                       string addressUser,
                       string collegeUser,
                       string phoneUser,
                       string photoUser,
                       string descriptionUser

                       );
    }
   
}
