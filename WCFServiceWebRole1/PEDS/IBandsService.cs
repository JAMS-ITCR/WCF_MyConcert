using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1.PEDS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBandsService" in both code and config file together.
    [ServiceContract]
    public interface IBandsService
    {
        [OperationContract]
        [WebInvoke(
          Method = "GET",
          UriTemplate = "/genders",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json,
          BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getGenders();

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/band?nbanda={nbanda}&idgenero={idgenero}&description={description}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String createBand(string nbanda, string idgenero, string description);
        
    }
}
