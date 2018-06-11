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
                    Console.WriteLine("Enity name Account/Contact/Exit");
                    Console.Read();
                    String entityName = Console.ReadLine();
                    Console.WriteLine("Entity you entered is " + entityName);
                      if (entityName == "Account")
                        {

                            string fetchXMLAccount = @"
                       <fetch mapping='logical'>
                         <entity name='account'> 
                            <attribute name='accountid'/> 
                            <attribute name='name'/> 
                            
                         </entity> 
                       </fetch> ";

                            IOrganizationService crmService = _client.OrganizationServiceProxy;
                            EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetchXMLAccount));

                            if (result != null)
                            {
                                foreach (var c in result.Entities)
                                {
                                    System.Console.WriteLine(c.Attributes["name"]);
                                }
                            }
                            else
                            {
                                throw new System.NullReferenceException();
                                

                            }
                            Console.Read();

                        }


                        else
                        {

                            Console.WriteLine("Contact information");

                            string fetchXmlContact = @"
                      <fetch mapping='logical'>
                        <entity name='contact'> 
                           <attribute name='fullname'/> 
                           <attribute name='contactid'/> 

                        </entity> 
                      </fetch> ";

                            IOrganizationService crmService = _client.OrganizationServiceProxy;
                            EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetchXmlContact));

                            if (result != null)
                            {
                                foreach (var c in result.Entities)
                                {
                                    System.Console.WriteLine(c.Attributes["fullname"]);
                                }
                            }
                            else
                            {

                                throw new System.NullReferenceException();
                            }
                        }
                    }
                    

                    Console.Read();
                
                 
                
            }

           
            catch (FaultException<OrganizationServiceFault> ex)
            {

                throw;
            }
        }
    }
}