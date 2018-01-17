using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Microsoft.Bot.Builder.Dialogs;
using Newtonsoft.Json;
using RestSharp;

namespace FormBot.Dialogs
{
    [Serializable]
    public class AddADAccountDialog : IDialog<object>
    {
        string oname = "";
        string sname = "";
        string uname = "";
        string dname = "";
        string pass = "";
        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Text(context, ResumeAfterSamNameClarification, "Please give me your sam account name");
        }
        private async Task ResumeAfterOrgNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            oname = await result;
            PromptDialog.Text(context, ResumeAfterSamNameClarification, "May I know sam name for your account");
            //await context.PostAsync($"I see you want to order {food}.");
        }

        private async Task ResumeAfterSamNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            sname = await result;
            PromptDialog.Text(context, ResumeAfterDispNameClarification, "What name you would like on display?");
            //await context.PostAsync($"You entered {sname}.");
        }

        private async Task ResumeAfterDispNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            dname = await result;
            PromptDialog.Text(context, ResumeAfterUserNameClarification, "Enter username of your choice");
            //await context.PostAsync($"You entered {sname}.");
        }

        private async Task ResumeAfterUserNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            uname = await result;
            PromptDialog.Text(context, ResumeAfterPasswordClarification, "And what password would you like to set?");
            //await context.PostAsync($"You entered {sname}.");
        }

        private async Task ResumeAfterPasswordClarification(IDialogContext context, IAwaitable<string> result)
        {
            pass = await result;

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
            parameter1.name = "OrganizationUnit_Name";
            parameter1.value = oname;
            parameter1.type = "String";
            parameter1.order = 1;
            parameter1.secret = false;
            parameter1.optional = false;
            parameter1.displayName = "OrganizationUnit_Name";
            parameter1.extension = null;
            parameter1.poolCredential = false;

            ListAutomationField.Add(parameter1);

            AutomationParameter parameter2 = new AutomationParameter();
            parameter2.name = "SamAccount_Name";
            parameter2.value = sname;
            parameter2.type = "String";
            parameter2.order = 2;
            parameter2.secret = false;
            parameter2.optional = false;
            parameter2.displayName = "SamAccount_Name";
            parameter2.extension = null;
            parameter2.poolCredential = false;

            ListAutomationField.Add(parameter2);

            AutomationParameter parameter3 = new AutomationParameter();
            parameter3.name = "User_Name";
            parameter3.value = uname;
            parameter3.type = "String";
            parameter3.order = 3;
            parameter3.secret = false;
            parameter3.optional = false;
            parameter3.displayName = "User_Name";
            parameter3.extension = null;
            parameter3.poolCredential = false;

            ListAutomationField.Add(parameter3);

            AutomationParameter parameter4 = new AutomationParameter();
            parameter4.name = "Display_Name";
            parameter4.value = dname;
            parameter4.type = "String";
            parameter4.order = 4;
            parameter4.secret = false;
            parameter4.optional = false;
            parameter4.displayName = "Display_Name";
            parameter4.extension = null;
            parameter4.poolCredential = false;

            ListAutomationField.Add(parameter4);

            AutomationParameter parameter5 = new AutomationParameter();
            parameter5.name = "Password";
            parameter5.value = pass;
            parameter5.type = "String";
            parameter5.order = 5;
            parameter5.secret = false;
            parameter5.optional = false;
            parameter5.displayName = "Password";
            parameter5.extension = null;
            parameter5.poolCredential = false;

            ListAutomationField.Add(parameter5);

            Guid temp = Guid.NewGuid();

            RootAutomation AutoRoot = new RootAutomation();
            AutoRoot.orgCode = "ACTIVEDIREC";
            AutoRoot.workflowName = "AD";
            AutoRoot.userId = "Aishwarya Chaudhary";
            AutoRoot.@params = ListAutomationField;
            AutoRoot.sourceId = temp.ToString();
            AutoRoot.source = "AutomationEdge HelpDesk";
            AutoRoot.responseMailSubject = null;
            string json = serialiser.Serialize(AutoRoot);



            //string body = "{\"orgCode\":\"FUSION\",\"workflowName\":\"Software Installation\",\"userId\":\"Admin Fusion\",\"sourceId\":\"SID_5b-912-21f4-88-880eb-8a0b-91\",\"source\":\"AutomationEdge HelpDesk\",\"responseMailSubject\":\"null\",\"params\":[{\"name\":\"software\",\"value\":\"JDK\",\"type\":\"String\",\"order\":1,\"secret\":false,\"optional\":false,\"defaultValue\":null,\"displayName\":\"Incident Number\",\"extension\":null,\"poolCredential\":false},{\"name\":\"slackChannel\",\"value\":\"fdgvdfg\",\"type\":\"String\",\"order\":2,\"secret\":false,\"optional\":false,\"defaultValue\":null,\"displayName\":\"Slack Channel\",\"extension\":null,\"poolCredential\":false}]}";
            //var json = JsonConvert.SerializeObject(body);
            request1.AddHeader("content-type", "application/json");
            request1.AddParameter("application/json", json, ParameterType.RequestBody);
            request1.RequestFormat = DataFormat.Json;
            IRestResponse response1 = client.Execute(request1);
            await context.PostAsync($"I will create AD account for {sname} soon... Visit me again whenever you need my help... Have a great day :)");
            context.Done(true);
        }
    }
}