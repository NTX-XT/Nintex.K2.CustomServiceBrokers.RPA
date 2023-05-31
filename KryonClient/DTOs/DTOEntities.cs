using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.K2.CustomServiceBrokers.RPA
{
    public class NewTaskRequest
    {
        public string QueueId{ get; set; }
        public string Name{ get; set; }
        public string WizardCustomName { get; set; }
        public string TenantId { get; set; }
        public int Priority { get; set; }
        public Variable[] Variables { get; set; }
    }
    public class TaskResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class Queue
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TenantId { get; set; }
        public string CreatedAt { get; set; }
        public string isDeleted { get; set; }
        public string DeletedAt {get;set;}
        public string DeletedBy { get; set; }
    }    
    public class TokenResponse
    {
        public string Access_Token;
    }
    public class Variable
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
