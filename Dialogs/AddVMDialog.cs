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
    public class AddVMDialog : IDialog<object>
    {
        string vcenter_IP = "";
        string Port = "";
        string Admin_Username = "";
        string Admin_Password = "";
        string VMHost_IP = "";
        string Datastore = "";
        string Template_Name = "";
        string VM_Name = "";
        public async Task StartAsync(IDialogContext context)
        {
            PromptDialog.Text(context, ResumeAftervcenterIPClarification, "Give me your vCenter IP please ");
        }
        private async Task ResumeAftervcenterIPClarification(IDialogContext context, IAwaitable<string> result)
        {
            vcenter_IP = await result;
            PromptDialog.Text(context, ResumeAfterPortClarification, "May I know port number for the same ");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterPortClarification(IDialogContext context, IAwaitable<string> result)
        {
            Port = await result;
            PromptDialog.Text(context, ResumeAfterAdminUsernameClarification, "Please enter your admin username below ");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterAdminUsernameClarification(IDialogContext context, IAwaitable<string> result)
        {
            Admin_Username = await result;
            PromptDialog.Text(context, ResumeAfterAdminPasswordClarification, "Can I have password for the same ");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterAdminPasswordClarification(IDialogContext context, IAwaitable<string> result)
        {
            Admin_Password = await result;
            PromptDialog.Text(context, ResumeAfterVMHostIPClarification, "Give me VM Host IP please ");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterVMHostIPClarification(IDialogContext context, IAwaitable<string> result)
        {
            VMHost_IP = await result;
            PromptDialog.Text(context, ResumeAfterDatastoreClarification, "Datastore name please");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterDatastoreClarification(IDialogContext context, IAwaitable<string> result)
        {
            Datastore = await result;
            PromptDialog.Text(context, ResumeAfterTemplateNameClarification, "Give template a name of your choice ");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterTemplateNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            Template_Name = await result;
            PromptDialog.Text(context, ResumeAfterVMNameClarification, "And give this VM a name of your choice ");
            //await context.PostAsync($"I see you want to order {food}.");
        }
        private async Task ResumeAfterVMNameClarification(IDialogContext context, IAwaitable<string> result)
        {
            VM_Name = await result;

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
            parameter1.name = "vcenter_IP";
            parameter1.value = vcenter_IP;
            parameter1.type = "String";
            parameter1.order = 1;
            parameter1.secret = false;
            parameter1.optional = false;
            parameter1.displayName = "vcenter_IP";
            parameter1.extension = null;
            parameter1.poolCredential = false;

            ListAutomationField.Add(parameter1);

            AutomationParameter parameter2 = new AutomationParameter();
            parameter2.name = "Port";
            parameter2.value = Port;
            parameter2.type = "String";
            parameter2.order = 2;
            parameter2.secret = false;
            parameter2.optional = false;
            parameter2.displayName = "Port";
            parameter2.extension = null;
            parameter2.poolCredential = false;

            ListAutomationField.Add(parameter2);

            AutomationParameter parameter3 = new AutomationParameter();
            parameter3.name = "Admin_Username";
            parameter3.value = Admin_Username;
            parameter3.type = "String";
            parameter3.order = 3;
            parameter3.secret = false;
            parameter3.optional = false;
            parameter3.displayName = "Admin_Username";
            parameter3.extension = null;
            parameter3.poolCredential = false;

            ListAutomationField.Add(parameter3);

            AutomationParameter parameter4 = new AutomationParameter();
            parameter4.name = "Admin_Password";
            parameter4.value = Admin_Password;
            parameter4.type = "String";
            parameter4.order = 4;
            parameter4.secret = false;
            parameter4.optional = false;
            parameter4.displayName = "Admin_Password";
            parameter4.extension = null;
            parameter4.poolCredential = false;

            ListAutomationField.Add(parameter4);

            AutomationParameter parameter5 = new AutomationParameter();
            parameter5.name = "VMHost_IP";
            parameter5.value = VMHost_IP;
            parameter5.type = "String";
            parameter5.order = 5;
            parameter5.secret = false;
            parameter5.optional = false;
            parameter5.displayName = "VMHost_IP";
            parameter5.extension = null;
            parameter5.poolCredential = false;

            ListAutomationField.Add(parameter5);

            AutomationParameter parameter6 = new AutomationParameter();
            parameter6.name = "Datastore";
            parameter6.value = Datastore;
            parameter6.type = "String";
            parameter6.order = 6;
            parameter6.secret = false;
            parameter6.optional = false;
            parameter6.displayName = "Datastore";
            parameter6.extension = null;
            parameter6.poolCredential = false;

            ListAutomationField.Add(parameter6);

            AutomationParameter parameter7 = new AutomationParameter();
            parameter7.name = "Template_Name";
            parameter7.value = Template_Name;
            parameter7.type = "String";
            parameter7.order = 7;
            parameter7.secret = false;
            parameter7.optional = false;
            parameter7.displayName = "Template_Name";
            parameter7.extension = null;
            parameter7.poolCredential = false;

            ListAutomationField.Add(parameter7);

            AutomationParameter parameter8 = new AutomationParameter();
            parameter8.name = "VM_Name";
            parameter8.value = VM_Name;
            parameter8.type = "String";
            parameter8.order = 8;
            parameter8.secret = false;
            parameter8.optional = false;
            parameter8.displayName = "VM_Name";
            parameter8.extension = null;
            parameter8.poolCredential = false;

            ListAutomationField.Add(parameter8);

            Guid temp = Guid.NewGuid();

            RootAutomation AutoRoot = new RootAutomation();
            AutoRoot.orgCode = "ACTIVEDIREC";
            AutoRoot.workflowName = "createVM";
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


            await context.PostAsync($"I will add VM named {VM_Name} soon... Visit me again whenever you need my help... Have a great day :)");
        }
    }
}