using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;

namespace TestWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<System.Web.Helpers.DynamicJsonObject> Get()
        {
            string rest = await(new Activiti.Model()).GETTasks(null,
                null,
                null,
                null,
                null,
                "kermit",
                "kermit",
                0);

            /*var json = Codeplex.Data.DynamicJson.Parse(rest);
            var v = ((dynamic)((Codeplex.Data.DynamicJson)(json))).data[0];
            var d = ((dynamic)((Codeplex.Data.DynamicJson)(v))).variables;
            d.s SelectNodes("./[@Name = 'Subject']")*/
            System.Web.Helpers.DynamicJsonObject Data = System.Web.Helpers.Json.Decode(rest);
            System.Web.Helpers.DynamicJsonArray Tasks = (Data as dynamic).data;
            for(int nC = 0 ; nC < Tasks.Length ; nC++)
            {
                dynamic task = Tasks[nC];
                IEnumerable<dynamic> Variables = task.Variables;
                //var d = Variables.Single(x => x.name == "Subject");
                Tasks[nC] = (
                            from Subj in Variables where Subj.name == "Subject"
                            from Summary in Variables where Summary.name == "Summary"
                            from Clamied in Variables where Clamied.name == "Clamied"
                            from FileID in Variables where FileID.name == "FileID"
                            
                            select new {                                 
                                ID = task.id,
                                FileID = FileID.value,
                                Assignee = task.assignee,
                                CreateTime = task.createTime,
                                DueDate = task.dueDate,
                                Priority = task.priority,
                                TaskDefinitionKey = task.taskDefinitionKey,
                                ExecutionId = task.executionId,
                                ProcessDefinitionId = task.processDefinitionId,
                                Subject = Subj.value,
                                Summary = Summary.value,
                                Clamied = Clamied.value
                                
                            }
                          ).Single();

                /*task.Variables = null;
                task.Subject = dd.Subject;
                task.Summary = dd.Summary;*/
                //object xi = Variables.Where(x => x["name"] == "Subject");

            }

            //var result = Json<object>(rest);
            //return result;
            return Data;
        }

        // GET api/values/5
        [HttpGet]
        public async Task<object> Get(int id)
        {
            var rest = await(new Activiti.Model()).GETTasks(null,
                null,
                null,
                null,
                null,
                "kermit",
                "kermit",
                0);
            var result = Json<object>(rest);
            return result;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}