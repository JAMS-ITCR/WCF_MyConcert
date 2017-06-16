using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICountries" in both code and config file together.
    [ServiceContract]
    public interface ICountriesService
    {
        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/countries",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]

        String getCountries();

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/countries?user={username}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]

        int test2(string username);
    }
}
