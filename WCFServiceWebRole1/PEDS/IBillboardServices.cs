using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using WCFServiceWebRole1.AUX_OBJ;
using System.ServiceModel.Web;

namespace WCFServiceWebRole1.PEDS
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBillboardService" in both code and config file together.
    [ServiceContract]
    public interface IBillboardServices
    {
        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/billboard?" +
                       "name={name}&" +
                       "country={country}&" +
                       "place={place}&" +
                       "dateVotation={dateVotation}&" +
                       "startdate={startdate}&" +
                       "enddate={enddate}&" +
                       "state={state}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String createBillboard(
                string name,
                string country,
                string place,
                string dateVotation,
                string startdate,
                string enddate,
                string state
            );
        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/category?" +
                       "idcategoria={idcategoria}&" +
                       "idcartelera={idcartelera}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String addCategoryToBillboard(
                string idcategoria,
                string idcartelera
            );

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/band?" +
                       "idcategoria={idcategoria}&" +
                       "idcartelera={idcartelera}&" +
                       "idbanda={idbanda}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String addBandCategoryBillboard(
                string idcategoria,
                string idcartelera,
                string idbanda
            );

        [OperationContract]
        [WebInvoke(
       Method = "GET",
       UriTemplate = "/billboards",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getBillBoard();

        [OperationContract]
        [WebInvoke(
      Method = "GET",
      UriTemplate = "/festivals",
      RequestFormat = WebMessageFormat.Json,
      ResponseFormat = WebMessageFormat.Json,
      BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getFestivals();
    }
}
