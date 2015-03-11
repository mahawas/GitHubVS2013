using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;



namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        private string _ProcessName = "DemoProcess:14:20067";
        private int _ProcesssInstanceID = 123921;
        private int _TaskID = 123950;


        [TestMethod]
        public async Task Getdeployments()
        {
            await (new Activiti.Model()).Getdeployments("kermit", "kermit");
        }
            
        [TestMethod]
        public async Task NewProceeInstance()
        {
            await (new Activiti.Model()).NewProceeInstance(_ProcessName, 
                55, 
                "task-02",
                "kermit", 
                "kermit");
        }

        [TestMethod]
        public async Task GETProceeInstances()
        {
            await (new Activiti.Model()).GETProceeInstances(_ProcessName, "kermit", "kermit");
        }

        [TestMethod]
        public async Task GETProceeInstance()
        {
            await (new Activiti.Model()).GETProceeInstance(_ProcesssInstanceID, "kermit", "kermit");
        }

        [TestMethod]
        public async Task GETTasks()
        {
            await (new Activiti.Model()).GETTasks(_ProcesssInstanceID, 
                null, 
                "kermit",
                null, 
                null, 
                "kermit", 
                "kermit",
                0);
        }


        [TestMethod]
        public async Task GETGroupTasks()
        {
            await (new Activiti.Model()).GETTasks(null,
                null,
                null,
                null,
                "sales",
                "kermit",
                "kermit",
                0);
        }

        [TestMethod]
        public async Task SetTaskClaimed()
        {
            await (new Activiti.Model()).SetTaskClaimed(_TaskID, "kermit", "kermit", "kermit");

            await (new Activiti.Model()).AddTaskLocalVariables(_TaskID,
               @"[ {""name"" : ""readed"", ""value"" : ""true""  }]",
               "kermit",
               "kermit");
        }

        [TestMethod]
        public async Task SetTaskDelegated()
        {
            await (new Activiti.Model()).SetTaskDelegated(_TaskID, "gonzo", "kermit", "kermit");
        }


        [TestMethod]
        public async Task SetTaskComplete()
        {
            await (new Activiti.Model()).SetTaskAsComplete(_TaskID,
                @"[{""name"":""Decision"", ""value"":""Reject""} ]",
                "kermit",
                "kermit");
        }

        [TestMethod]
        public async Task AddTaskComment()
        {
            await (new Activiti.Model()).AddTaskComment(_TaskID,
                @"testing",
                "kermit",
                "kermit");
        }

        [TestMethod]
        //not done yet
        public async Task SetTaskFormVariables()
        {
            await (new Activiti.Model()).SetTaskFormVariables(_TaskID,
                @"{""id"":""1-Calmied"", ""value"":""true""} {""id"":""Decision"", ""value"":""Reject""}",
                "kermit",
                "kermit");
        }

        [TestMethod]
        public async Task AddTaskLocalVariables()
        {
            await (new Activiti.Model()).AddTaskLocalVariables(_TaskID,
                @"[ {""name"" : ""claimed"", ""value"" : ""true""  }]",
                "kermit",
                "kermit");
        }

        [TestMethod]
        public async Task UpdateTaskLocalVariables()
        {
            string strVaribleName = "claimed";
            await (new Activiti.Model()).UpdateTaskLocalVariables(_TaskID,
                strVaribleName,
                string.Format(@"{{ ""name"" : ""{0}"", ""value"" : ""fa""  }}", strVaribleName),
                "kermit",
                "kermit");
        }

        
        [TestMethod]
        public async Task GetTaskFormVaribales()
        {
            await (new Activiti.Model()).GetTaskFormVaribales(_TaskID, "kermit", "kermit");
        }

        [TestMethod]
        public async Task GetTaskVaribales()
        {
            await (new Activiti.Model()).GetTaskVaribales(_TaskID, "kermit", "kermit");
        }

        [TestMethod]
        public async Task GetTaskVaribale()
        {
            await (new Activiti.Model()).GetTaskVaribale(_TaskID, "Clamied", "kermit", "kermit");
        }

        [TestMethod]
        public async Task GetProcessExecutionID()
        {
            await (new Activiti.Model()).GetProcessExecutionID(_ProcesssInstanceID, "kermit", "kermit");
        }

        [TestMethod]
        public async Task SendSignal()
        {
            bool bRest = await (new Activiti.Model()).SendSignal(_TaskID,
                _ProcesssInstanceID, 
                "Rollback signal", 
                "kermit",
                "kermit");
        }
    }
}
