
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

        String login(string username, string password);
        [OperationContract]
        [WebInvoke(
          Method = "GET",
          UriTemplate = "/logout/{username}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]

        String logout(string username);

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

        [OperationContract]
        [WebInvoke(
          Method = "GET",
          UriTemplate = "/user?idusuario={idusuario}&idrol={idrol}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getInfoUsuario(string idusuario, string idrol);

        [OperationContract]
        [WebInvoke(
          Method = "GET",
          UriTemplate = "/fan?idusuario={idusuario}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getDetalleFanatico(string idusuario);

        [OperationContract]
        [WebInvoke(
          Method = "POST",
          UriTemplate = "/fan/gender?idusuario={idusuario}&idgenero={idgenero}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]
        String asignarGeneroFanatico(string idusuario, string idgenero);

        [OperationContract]
        [WebInvoke(
          Method = "DELETE",
          UriTemplate = "/fan/gender?idusuario={idusuario}&idgenero={idgenero}",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]
        String eliminarGeneroFanatico(string idusuario, string idgenero);

    }
   
}
