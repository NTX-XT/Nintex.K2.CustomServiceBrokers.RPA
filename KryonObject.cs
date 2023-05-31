using System;
using SourceCode.SmartObjects.Services.ServiceSDK.Objects;
using Attributes = SourceCode.SmartObjects.Services.ServiceSDK.Attributes;
using SourceCode.SmartObjects.Services.ServiceSDK.Types;

namespace Nintex.K2.CustomServiceBrokers.RPA
{

    public abstract class KryonObject
    {
        private ServiceConfiguration _serviceConfig;
        public ServiceConfiguration ServiceConfiguration
        {
            get { return _serviceConfig; }
            set { _serviceConfig = value; }
        }
        public bool AuthMode
        {
            get
            {
                return _serviceConfig.ServiceAuthentication.Impersonate;
            }
        }
        public string ServerName
        {
            get
            {
                return _serviceConfig["ServerName"].ToString();
            }
        }
        public string Realm
        {
            get
            {
                return _serviceConfig["Realm"].ToString();
            }
        }
        public string ServerScheme
        {
            get
            {
                return _serviceConfig["ServerScheme"].ToString();
            }
        }
        public string ServerPort
        {
            get
            {
                return _serviceConfig["ServerPort"].ToString();
            }
        }
        public string UserName
        {
            get
            {
                return _serviceConfig.ServiceAuthentication.UserName;
            }
        }
        public string Password
        {
            get
            {
                return _serviceConfig.ServiceAuthentication.Password;
            }
        }    
    }
}
