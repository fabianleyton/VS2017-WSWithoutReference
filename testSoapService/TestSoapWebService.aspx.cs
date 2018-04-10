using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace testSoapService
{
    public partial class TestSoapWebService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            string envelope = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:urn=\"urn:sap-com:document:sap:rfc:functions\">";
            envelope += "<soap:Header/>";
            envelope += "<soap:Body>";
            envelope += "<urn:ZMF_HCMWS_ING_RET>";
            envelope += "<BUKRS>5101</BUKRS>";
            envelope += "<PERNR>83</PERNR>";
            envelope += "<YEAR>2014</YEAR>";
            envelope += "</urn:ZMF_HCMWS_ING_RET>";
            envelope += "</soap:Body>";
            envelope += "</soap:Envelope>";
            
            string respuesta = ClassWS.WSSoapRequest.Peticion(
                "http://yourserviceurl.com",
                "urn:sap-com:document:sap:rfc:functions:ZMF_HCMWS_ING_RET:ZMF_HCMWS_ING_RETRequest",
                envelope,
                "application/soap+xml;charset=\"utf-8\"",
                "POST",
                "usr1",
                "pass1"
                );
            lblResult.Text = respuesta;
        }
    }
}