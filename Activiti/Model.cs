using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net;
using System.Net.Http;
using System.Web;
using System.Net.Http.Headers;


using System.Collections.Specialized;



namespace Activiti
{
    public class Model
    {
        private Uri _URL = new Uri("http://192.168.3.120:8080/activiti-rest/service/");
        
        
        public async Task Getdeployments(string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                
                HttpResponseMessage response = await client.GetAsync("repository/deployments/");
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return ;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    return ;
                }
                return ;
            }
        }


        public async Task NewProceeInstance(string ProcessID, 
            int FileID, 
            string Subject, 
            string UserName, 
            string Password)        
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string strRequest = string.Format(@"{{ ""processDefinitionId"":""{0}"" , ""variables"": [{{""name"":""File ID"", ""value"":""{1}""}},{{""name"":""Subject"", ""value"":""{2}""}} ]}}", 
                    ProcessID,
                    FileID,
                    Subject);

                var oContent = new StringContent(strRequest,System.Text.Encoding.UTF8, "application/json");                
                HttpResponseMessage response = await client.PostAsync("runtime/process-instances", oContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    return;
                }
                return;
            }
        }


        public async Task GETProceeInstances(string ProcessDefID, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/process-instances?processDefinitionId={0}&includeProcessVariables=true", ProcessDefID));
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    return;
                }
                return;
            }
        }

        public async Task GETProceeInstance(int ProcessInstanceID, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/process-instances?id={0}&includeProcessVariables=true", ProcessInstanceID));
                if (response.IsSuccessStatusCode)
                {                    		
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonArray data = System.Web.Helpers.Json.Decode(responseBodyAsText).data;
                    System.Web.Helpers.DynamicJsonArray variables = data[0].variables;
                    return;
                }
                return;
            }
        }

        public async Task<string> GETTasks(int? ProcessInstanceID,
            string Assignee/*Already clamined by that user*/,
            string CandidateUser, 
            string Owner,
            string GroupName, 
            string UserName,             
            string Password,
            int PageNumber)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string Filter = string.Empty;
                if (ProcessInstanceID.HasValue)
                    Filter += "&processInstanceId=" + ProcessInstanceID;
                
                if (Assignee != null)
                    Filter += "&assignee=" + Assignee;

                if (Owner != null)
                    Filter += "&owner=" + Owner;

                if (GroupName != null)
                    Filter += "&candidateGroup=" + GroupName;

                if (CandidateUser != null)
                    Filter += "candidateUser=" + CandidateUser;



                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/tasks?includeTaskLocalVariables=true&includeProcessVariables=true&p={0}{1}", PageNumber, Filter));
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return null;
                    }
                    //dynamic data = System.Web.Helpers.Json.Decode(responseBodyAsText).data;
                    return responseBodyAsText;
                }
                return null;
            }
        }
        
        public async Task GetTaskFormVaribales(int TaskID, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                HttpResponseMessage response = await client.GetAsync(string.Format("form/form-data?taskId={0}", TaskID));
                if (response.IsSuccessStatusCode)
                {                    		
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonArray data = System.Web.Helpers.Json.Decode(responseBodyAsText).formProperties;
                    return;
                }
                return;
            }
        }
            
        public async Task GetTaskVaribales(int TaskID, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/tasks/{0}/variables", TaskID));
                if (response.IsSuccessStatusCode)
                {                    		
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonArray data = System.Web.Helpers.Json.Decode(responseBodyAsText);
                    return;
                }
                return;
            }
        }

        public async Task GetTaskVaribale(int TaskID, string VariableName, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/tasks/{0}/variables/{1}", TaskID, VariableName));
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonObject data = System.Web.Helpers.Json.Decode(responseBodyAsText);
                    return;
                }
                return;
            }
        }

        public async Task SetTaskFormVariables(int TaskID,
            string Variables,
            string UserName,
            string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string strRequest = string.Format(@"{{ ""taskId"" : ""{0}"", ""properties"": [{1}] }}",
                    TaskID,
                    Variables);

                var oContent = new StringContent(strRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("form/form-data", oContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    return;
                }
                return;
            }
        }

        public async Task AddTaskLocalVariables(int TaskID,
            string Variables,
            string UserName,
            string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                var oContent = new StringContent(Variables, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("runtime/tasks/"+TaskID+"/variables", oContent);
                switch(response.StatusCode)
                {
                    case HttpStatusCode.Conflict:
                        System.Diagnostics.Debug.WriteLine("Conflict");
                            break;
                    case HttpStatusCode.Created:
                        System.Diagnostics.Debug.WriteLine("Added");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Fail");
                        break;
                }                
                return;
            }
        }

        public async Task UpdateTaskLocalVariables(int TaskID,
            string VariableName,
            string VariableValue,
            string UserName,
            string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                var oContent = new StringContent(VariableValue, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(string.Format("runtime/tasks/{0}/variables/{1}",
                    TaskID, 
                    VariableName),
                    oContent);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        System.Diagnostics.Debug.WriteLine("NotFound");
                        break;
                    case HttpStatusCode.OK:
                        System.Diagnostics.Debug.WriteLine("Updated");
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Fail");
                        break;
                }
                return;
            }
        }

        public async Task SetTaskAsComplete(int TaskID,             
            string Variables,            
            string UserName, 
            string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string strRequest = string.Format(@"{{ ""action"" : ""{0}"" {1} }}", 
                    "complete",
                    Variables == null? null:(@", ""variables"": " + Variables));

                var oContent = new StringContent(strRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("runtime/tasks/"+TaskID, oContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    return;
                }
                return;
            }
        }


        public async Task SetTaskDelegated(int TaskID,
            string ToUserName,
            string UserName,
            string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string strRequest = string.Format(@"{{ ""action"" : ""{0}"" , ""assignee"": ""{1}""}}",
                    "delegate",
                    ToUserName);

                var oContent = new StringContent(strRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("runtime/tasks/" + TaskID, oContent);
                if (response.IsSuccessStatusCode)
                {
                    /* var responseBodyAsText = await response.Content.ReadAsStringAsync();

                     if (String.IsNullOrEmpty(responseBodyAsText))
                     {
                         System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                         return;
                     }
                     System.Diagnostics.Debug.WriteLine(responseBodyAsText);*/
                    return;
                }
                return;
            }
        }


        public async Task SetTaskClaimed(int TaskID,
            string ToUserName,
            string UserName,
            string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string strRequest = string.Format(@"{{ ""action"" : ""{0}"" , ""assignee"": ""{1}""}}",
                    "claim",
                    ToUserName);

                var oContent = new StringContent(strRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("runtime/tasks/" + TaskID, oContent);
                if (response.IsSuccessStatusCode)
                {
                   /* var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);*/
                    return;
                }
                return;
            }
        }

        private void SetAuthonticate(HttpClient Client, string UserName, string Password)
        {
            Client.BaseAddress = _URL;
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", UserName, Password))));
        }

        public async Task AddTaskComment(int TaskID, string Comment, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                string strRequest = string.Format(@"{{ ""message"" : ""{0}"" , ""saveProcessInstanceId"" : true }}",
                    Comment);

                var oContent = new StringContent(strRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("runtime/tasks/" + TaskID + "/comments", oContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    return;
                }
                return;
            }
            
        }

        public async Task<bool> SendSignal(int TaskID, int ProcesssInstanceID, string SignalName, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);
                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/tasks/{0}/variables/Clamied", TaskID));
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return false;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonObject data = System.Web.Helpers.Json.Decode(responseBodyAsText);
                    if (Boolean.Parse(((dynamic)data).value))
                        return false;
                }


                response = await client.GetAsync(string.Format("runtime/executions?parentId={0}", ProcesssInstanceID));
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return false;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonObject data = System.Web.Helpers.Json.Decode(responseBodyAsText);
                    if (((dynamic)data).total == 1)
                    {
                        System.Web.Helpers.DynamicJsonArray variables = ((dynamic)data).data;
                        string ExecutionID = variables[0].id;
                        string strRequest = string.Format(@"{{ ""action"":""signalEventReceived"", ""signalName"":""{0}""}}", SignalName);

                        var oContent = new StringContent(strRequest, System.Text.Encoding.UTF8, "application/json");
                        response = await client.PutAsync("runtime/executions/" + ExecutionID, oContent);
                        if (response.IsSuccessStatusCode)
                        {                           
                            return true;
                        }
                        return false;
                    }
                    return false;
                }
                return false;
            }
        }

        public async Task GetProcessExecutionID(int ProcesssInstanceID, string UserName, string Password)
        {
            using (var client = new HttpClient())
            {
                SetAuthonticate(client, UserName, Password);

                HttpResponseMessage response = await client.GetAsync(string.Format("runtime/executions?parentId={0}", ProcesssInstanceID));
                if (response.IsSuccessStatusCode)
                {
                    var responseBodyAsText = await response.Content.ReadAsStringAsync();

                    if (String.IsNullOrEmpty(responseBodyAsText))
                    {
                        System.Diagnostics.Debug.WriteLine("Unsuccessful Login.Bonita bundle may not have been started, or the URL is invalid.");
                        return;
                    }
                    System.Diagnostics.Debug.WriteLine(responseBodyAsText);
                    System.Web.Helpers.DynamicJsonObject data = System.Web.Helpers.Json.Decode(responseBodyAsText);
                    if (((dynamic)data).total == 1)
                    {
                        System.Web.Helpers.DynamicJsonArray variables = ((dynamic)data).data;
                        string ID = variables[0].id;
                    }
                    return;
                }
                return;
            }
        }
    }
}
