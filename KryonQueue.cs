using System;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using Attributes = SourceCode.SmartObjects.Services.ServiceSDK.Attributes;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace Nintex.K2.CustomServiceBrokers.RPA
{

    [Attributes.ServiceObject("KryonQueue", "Integrate with Kryon RPA Queue", "Call Kryon RPA GraphQL public-api")]
    public class KryonQueue : KryonObject
    {
        [Attributes.Property("Id", SoType.Text, "Id", "Queue Id")]
        public string Id { get; set; }
        [Attributes.Property("Name", SoType.Text, "Name", "Queue Name")]
        public string Name { get; set; }
        [Attributes.Property("TenantId", SoType.Text, "TenantId", "TenantId of RPA Server")]
        public string TenantId { get; set; }
        [Attributes.Property("CreatedAt", SoType.DateTime, "CreatedAt", "Queue State")]
        public DateTime CreatedAt { get; set; }
        [Attributes.Property("CreatedBy", SoType.Text, "CreatedBy", "Created User")]
        public DateTime CreatedBy { get; set; }
        [Attributes.Property("isDeleted", SoType.YesNo, "isDeleted", "Whether the queue is deleted or not")]
        public bool isDeleted { get; set; }
        [Attributes.Property("DeletedAt", SoType.DateTime, "DeletedAt", "Deleted At")]
        public DateTime DeletedAt { get; set; }      
        //default constructor. No implementation is required, but you must have a default public constructor
        public KryonQueue()
        {


        }
        [Attributes.Method("GetQueues", MethodType.List, "Get Kryon RPA existing queues", "Get RPA queues", new string[] { }, new string[] { "TenantId" }, new string[] { "Id", "Name", "TenantId"})]
        public Queue[] GetQueues()
        {
            var client = new KryonGraphQLClient(ServerName, ServerScheme, ServerPort, Realm, UserName, Password);
            Queue[] result = client.GetQueues(TenantId).GetAwaiter().GetResult();
            return result;
           
        }

    }
}
