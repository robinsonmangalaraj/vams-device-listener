using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AccessControlSDK;
using Newtonsoft.Json;

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
        public sdkResponseObj PushApiDetails(string access_token, sdkCallEventList sdk)
        {

            sdkResponseObj sdkResp = new sdkResponseObj();

            try
            {

                if (string.Compare(access_token, Properties.Settings.Default.WebApiToken, true) != 0)
                {
                    sdkResp.code = "VAL";
                    sdkResp.showInfoMessage = true;
                    sdkResp.infoMessage = "Invalid Access Token!";
                    return sdkResp;
                }

                if (sdk == null || sdk.apiEventDetList == null || sdk.apiEventDetList.Count == 0)
                {
                    sdkResp.code = "VAL";
                    sdkResp.showInfoMessage = true;
                    sdkResp.infoMessage = "Event call data not passed!";
                    return sdkResp;
                }

                if (Properties.Settings.Default.WriteInfoLog)
                {
                    Logs.WriteLog(JsonConvert.SerializeObject(sdk));
                }
                
                Utilities.accCtrl.apiCallEventList = new sdkCallEventList();
                Utilities.accCtrl.apiCallEventList.apiEventDetList = sdk.apiEventDetList;
                Utilities.accCtrl.apiTmInterval = Properties.Settings.Default.TmIntervalInMin;
                Utilities.accCtrl.sdkApiWriteLog = Properties.Settings.Default.WriteInfoLog.ToString();

                sdkResp = Utilities.accCtrl.StartEventListener();
                               

            }
            catch (Exception exp)
            {
                sdkResp.code = "EXP";
                sdkResp.hasError = true;
                sdkResp.errorMessage = exp.ToString();
            }
             
            return sdkResp;

        }

        #endregion //POST
        #endregion //Methods

    }

}
