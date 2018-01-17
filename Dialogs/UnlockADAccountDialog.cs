using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormBot.Dialogs
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using RestSharp;
    using Microsoft.Bot.Connector;
    using Newtonsoft.Json;
    using System.Web.Script.Serialization;
    using System.Collections.Generic;

    [Serializable]
    public class UnlockADAccountDialog : IDialog<object>
    {
        string sname;
        public async Task StartAsync(IDialogContext context)
        {

            PromptDialog.Text(context, ResumeAfterSamNameClarification, "Please give me your sam account name");
        }

        private async Task ResumeAfterSamNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            sname = await result;
            var client = new RestClient("http://96a7bf35.ngrok.io/aeengine/rest/authenticate");
            var request = new RestRequest(Method.POST);
            request.AddHeader("postman-token", "ea502694-bf8a-9c2e-e27b-8082381ce137");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"username\"\r\n\r\naishwarya\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\nPune@123\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string jsonresult;
            jsonresult = response.Content;
            var myDetails = JsonConvert.DeserializeObject<MyDetail>(jsonresult);
            string token = myDetails.sessionToken;
            var request1 = new RestRequest("http://96a7bf35.ngrok.io/aeengine/rest/execute", Method.POST);
            request1.AddHeader("X-session-token", token);

            JavaScriptSerializer serialiser = new JavaScriptSerializer();
            List<AutomationParameter> ListAutomationField = new List<AutomationParameter>();

            AutomationParameter parameter1 = new AutomationParameter();
            parameter1.name = "Sam_Account_Name";
            parameter1.value = sname;
            parameter1.type = "String";
            parameter1.order = 1;
            parameter1.secret = false;
            parameter1.optional = false;
            parameter1.displayName = "Sam_Account_Name";
            parameter1.extension = null;
            parameter1.poolCredential = false;

            ListAutomationField.Add(parameter1);

            Guid temp = Guid.NewGuid();

            RootAutomation AutoRoot = new RootAutomation();
            AutoRoot.orgCode = "ACTIVEDIREC";
            AutoRoot.workflowName = "UnlockAD";
            AutoRoot.userId = "Aishwarya Chaudhary";
            AutoRoot.@params = ListAutomationField;
            AutoRoot.sourceId = temp.ToString();
            AutoRoot.source = "AutomationEdge HelpDesk";
            AutoRoot.responseMailSubject = null;
            string json = serialiser.Serialize(AutoRoot);


            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", json, ParameterType.RequestBody);
            request1.RequestFormat = DataFormat.Json;
            IRestResponse response1 = client.Execute(request1);

            await context.PostAsync($"I will unlock account for {sname} as soon as possible... Visit me again whenever you need my help. Have a great day:)");
            context.Done(true);
        }

    }
    public class MyDetail
    {
        public string sessionToken
        {
            get;
            set;
        }
    }
}