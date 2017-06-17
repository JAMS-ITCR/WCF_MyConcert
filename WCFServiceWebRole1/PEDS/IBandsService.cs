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
         Method = "GET",
         UriTemplate = "/genders/fan?idusuario={idusuario}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getGendersByFan(string idusuario);

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/genders?idgenero={idgenero}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String  getGeneroById(string idgenero);

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/band?nbanda={nbanda}&idgenero={idgenero}&description={description}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String createBand(string nbanda, string idgenero, string description);


        [OperationContract]
        [WebInvoke(
         Method = "PUT",
         UriTemplate = "/band?idbanda ={idbanda}&descripcion={descripcion}&estado={estado}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String updateBand(string idbanda, string descripcion, string estado);

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/bands",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getBands();

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/bands/{bandid}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getBand(String bandid);

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/bandmember?nmiembro={nmiembro}&idbanda={idbanda}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String addMemberToBanda(string nmiembro, string idbanda);

        [OperationContract]
        [WebInvoke(
         Method = "DELETE",
         UriTemplate = "/bandmember?idmiembro={idmiembro}&idbanda={idbanda}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String deleteBandMember(string idbanda, string idmiembro);

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/bandmembers?idbanda={idbanda}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getMembersByBand(string idbanda);

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/imageband?idbanda={idbanda}&urlimagen={urlimagen}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String addImageToBand(string idbanda, string urlimagen);


        [OperationContract]
        [WebInvoke(
         Method = "DELETE",
         UriTemplate = "/imageband?idbanda={idbanda}&idimagen={idimagen}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String deleteImageFrom(string idbanda, string idimagen);

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/songband?ncancion={ncancion}&preview={preview}&image={image}&idbanda={idbanda}&idalbum={idalbum}&estado={estado}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String addSongToBand(string ncancion, string preview, string image, string idbanda, string idalbum, string estado);

        [OperationContract]
        [WebInvoke(
        Method = "DELETE",
        UriTemplate = "/songband?idbanda={idbanda}&idcancion={idcancion}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        String deleteSongToBand(string idbanda, string idcancion);

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/rating?idbanda={idbanda}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getRatingBanda(string idbanda);

        [OperationContract]
        [WebInvoke(
         Method = "GET",
         UriTemplate = "/accumulated?idbanda={idbanda}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getAccumulated(string idbanda);

        [OperationContract]
        [WebInvoke(
         Method = "POST",
         UriTemplate = "/comment?idusuario={idusuario}&idbanda={idbanda}&rating={rating}&content={content}",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Wrapped)]
        String addComment(string idusuario, string idbanda, string rating, string content);

        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/comments?idbanda={idbanda}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        String  getComments(string idbanda);


        [OperationContract]
        [WebInvoke(
        Method = "GET",
        UriTemplate = "/bandsbillboards?idcartelera={idcartelera}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        String getBandasXCategoriaXCartelera(string idcartelera);

        [OperationContract]
        [WebInvoke(
        Method = "POST",
        UriTemplate = "/vote?data={data}",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped)]
        String makeVote(string data);

       

    }

}
