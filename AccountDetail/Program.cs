using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace AccountDetail
{
    class Program
    {
        private static CrmServiceClient _client;

        public static void Main(string[] args)
        {
            try
            {
                using (_client = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {

                    Console.WriteLine("Login successfull...");
                    Console.Read();
                    string fetch2 = @"
                       <fetch mapping='logical'>
                         <entity name='account'> 
                            <attribute name='accountid'/> 
                            <attribute name='name'/> 
                            <link-entity name='systemuser' to='owninguser'> 
                               <filter type='and'> 
                                  <condition attribute='lastname' operator='not-null' /> 
                               </filter> 
                            </link-entity> 
                         </entity> 
                       </fetch> ";

                    IOrganizationService crmService = _client.OrganizationServiceProxy;
                    EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetch2));


                    foreach (var c in result.Entities)
                    {
                        System.Console.WriteLine(c.Attributes["name"]);
                     }
                    Console.Read();
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                string message = ex.Message;
                throw;
            }
        }
    }
}