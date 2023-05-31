using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//import the SourceCode SDK namespaces
using SourceCode.SmartObjects.Services.ServiceSDK;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using Attributes = SourceCode.SmartObjects.Services.ServiceSDK.Attributes;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;
using System.Transactions;
using System.Net.PeerToPeer;
using System.Diagnostics;

namespace Nintex.K2.CustomServiceBrokers.RPA
{
    public class KryonRPAServiceBroker : ServiceAssemblyBase
    {
    
        public override string GetConfigSection()
        {
            try
            {
                this.Service.ServiceConfiguration.Add("ServerScheme", true, "https");
                this.Service.ServiceConfiguration.Add("ServerName", true, string.Empty);
                this.Service.ServiceConfiguration.Add("ServerPort", true, "80");
                this.Service.ServiceConfiguration.Add("Realm", true, "kryon");
                this.Service.ServiceConfiguration.Add("ClientSecert", true, string.Empty);
            }
            catch (Exception ex)
            {
                // Record the exception message and indicate that this was an error.
                ServicePackage.ServiceMessages.Add(ex.Message, MessageSeverity.Error);
            }
            return base.GetConfigSection();
        }


        public override string DescribeSchema()
        {
            try
            {
                //For this broker, we will add a single Service Object based on the definition of the TimeZoneCustom class
                //the TimeZoneCustomClass is decorated with attributed which allows the broker to create a Service Object by
                //interrogating the attributes of the class and properties/methods in the class
                if (string.IsNullOrEmpty(this.Service.ServiceConfiguration.ServiceAuthentication.UserName) || string.IsNullOrEmpty(this.Service.ServiceConfiguration.ServiceAuthentication.Password))
                    throw new Exception("Please Make sure that Service Authentication is Static and both password & username are set");
                //add the custom timezone class as a service object 
                this.Service.ServiceObjects.Create(new ServiceObject(typeof(KryonTask)));
                this.Service.ServiceObjects.Create(new ServiceObject(typeof(KryonQueue)));
                //set up the default values for the service instance
                this.Service.Name = "Kryon RPA Service Broker";
                this.Service.MetaData.DisplayName = "Kryon RPA Service Borker";
                this.Service.MetaData.Description = "This custom Service that allows K2 to integrate with Kryon RPA GraphQL";
                // Indicate that the operation was successful.
                ServicePackage.IsSuccessful = true;                
            }
            catch (Exception ex)
            {
                // Record the exception message and indicate that this was an error.
                ServicePackage.ServiceMessages.Add(ex.Message, MessageSeverity.Error);
                // Indicate that the operation was unsuccessful.
                ServicePackage.IsSuccessful = false;
            }
            return base.DescribeSchema();
        }

        public override void Extend()
        {
            try
            {
                throw new NotImplementedException("Service Object \"Extend()\" is not implemented.");
            }
            catch (Exception ex)
            {
                // Record the exception message and indicate that this was an error.
                ServicePackage.ServiceMessages.Add(ex.Message, MessageSeverity.Error);
                // Indicate that the operation was unsuccessful.
                ServicePackage.IsSuccessful = false;
            }
        }
        public override void Execute()
        {
           base.Execute();
           
        }
        public override void Prepare(PreparingEnlistment preparingEnlistment)
        {
            base.Prepare(preparingEnlistment);
        }


    }
}
