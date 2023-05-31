using System;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using Attributes = SourceCode.SmartObjects.Services.ServiceSDK.Attributes;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.CodeDom;

namespace Nintex.K2.CustomServiceBrokers.RPA
{

    [Attributes.ServiceObject("KryonTask", "Integrate with Kryon RPA Tasks", "Call Kryon RPA GraphQL public-api")]
    public class KryonTask:KryonObject
    {
        [Attributes.Property("Name", SoType.Text, "Name", "Name of the RPA Task")]
        public string Name { get; set; }
        [Attributes.Property("QueueId", SoType.Text, "QueueId", "QueueId of the RPA Queue")]
        public string QueueId { get; set; }
        [Attributes.Property("TenantId", SoType.Text, "TenantId", "TenantId of Kryon RPA")]
        public string TenantId { get; set; }
        [Attributes.Property("WizardCustomName", SoType.Text, "WizardCustomName", "Custom Name given to the RPA wizard")]
        public string WizardCustomName { get; set; }
        [Attributes.Property("Priority", SoType.Number, "Priority", "Priority of the RPA Task")]
        public int Priority { get; set; }
        [Attributes.Property("Variables", SoType.Memo, "Variables", "Variables sent to RPA Task")]
        public string Variables { get; set; }
        [Attributes.Property("Id", SoType.Text, "Id", "Task Identifier")]
        public string Id{ get; set; }
        [Attributes.Property("StepSerialNo", SoType.Text, "StepSerialNo", "Workflow Step Serial Number")]
        public string StepSerialNo { get; set; }

        //default constructor. No implementation is required, but you must have a default public constructor
        public KryonTask()
        {            

        }
        [Attributes.Method("AddTask", MethodType.Create, "Add Kryon Task", "Add Task into a Kryon RPA Queue", new string[] { "QueueId", "Name", "TenantId", "WizardCustomName", "Priority" }, new string[] { "QueueId", "Name", "TenantId", "WizardCustomName", "Priority", "Variables","StepSerialNo" }, new string[] { "Id", "Name" })]
        public TaskResponse AddTask()
        {
            var client = new KryonGraphQLClient(ServerName, ServerScheme, ServerPort, Realm, UserName, Password);
            NewTaskRequest newTask = new NewTaskRequest { QueueId = QueueId, Name = Name, TenantId = TenantId, WizardCustomName = WizardCustomName, Priority = Priority };
            List<Variable> vars = new List<Variable>();
            if (!string.IsNullOrEmpty(Variables))
            {
                vars=JsonConvert.DeserializeObject<List<Variable>>(Variables);
            }
            if (!string.IsNullOrEmpty(StepSerialNo))
            {
                vars.Add(new Variable { Name = "SerialNo", Value = StepSerialNo });
            }
            newTask.Variables = vars.ToArray();
            var response =client.AddTask(newTask).GetAwaiter().GetResult();
            return response;
        }
    }
}
