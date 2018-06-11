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
        public static void Main(string[] args)
        {
            try
            {
                using (var client = new CrmServiceClient(ConfigurationManager.ConnectionStrings["CRMConnectionString"].ConnectionString))
                {
                    Console.WriteLine("Login successfull...");
                    Console.Read();
                    Console.WriteLine("Enity name Account/Contact/Exit");
                    Console.Read();
                    String entityName = Console.ReadLine();
                    Console.WriteLine("Entity you entered is " + entityName);
                      if (entityName.Eqauls("Account"))
                        {
                            string fetchXMLAccount = @"<fetch mapping='logical' distinct='true'>
                                                         <entity name='account'> 
                                                            <attribute name='accountid'/> 
                                                            <attribute name='name'/> 
                                                         </entity> 
                                                       </fetch>";
                            IOrganizationService crmService = client.OrganizationServiceProxy;
                            EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetchXMLAccount));
                            if (result != null)
                            {
                                foreach(var c in result.Entities)
                                {
                                    System.Console.WriteLine(c.Attributes["name"]);
                                }
                            }
                            else
                            {
                               Console.WriteLine("No records found for the account entity.");
                            }
                        }
                        else
                        {
                         Console.WriteLine("Contact information");
                         string fetchXmlContact = @"<fetch mapping='logical'>
                                                        <entity name='contact' distinct='true'> 
                                                           <attribute name='fullname'/> 
                                                           <attribute name='contactid'/> 
                                                        </entity> 
                                                     </fetch> ";

                            IOrganizationService crmService = client.OrganizationServiceProxy;
                            EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetchXmlContact));
                            if (result != null)
                            {
                                foreach(var c in result.Entities)
                                {
                                    System.Console.WriteLine(c.Attributes["fullname"]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("No records found for the contact entity.");
                              
                            }
                        }
                    }
            }
            catch
            {
                throw;
            }
                Console.Read();
        }
    }
}
