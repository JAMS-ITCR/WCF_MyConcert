
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
    }
   
}
