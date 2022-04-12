using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AccessControlSDK;

namespace VamsDeviceListener
{

    public class WebApiController : ApiController
    {

        #region Methods

        #region POST

        /// <summary>
        /// Upload the Visitors to zk Device
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="zkVisObj"></param>
        /// <returns></returns>
        [HttpPost]
        public List<sdkResponseObj> PushApiDetails(string access_token, sdkCallEventList sdk)
        {

            List<sdkResponseObj> sdkRespList = new List<sdkResponseObj>();

            return sdkRespList;

        }

        #endregion //POST
        #endregion //Methods

    }

}
