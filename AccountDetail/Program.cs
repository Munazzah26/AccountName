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
                    var applicationSetting = ConfigurationManager.GetSection("appSettings") as NameValueConfigurationCollection;

                    Console.WriteLine("Login successfull...");
                    Console.Read();
                    Console.WriteLine("Enity name Account/Contact/Exit");
                    Console.Read();
                    String entityName = Console.ReadLine();
                    Console.WriteLine("Entity you entered is " + entityName);


                    if (entityName.Equals("Account")) //Account information
                    {
                        String fetchXml = @"<fetch mapping='logical' distinct='true'>
                                                         <entity name='account'> 
                                                         {0}
                                                            </entity> 
                                                       </fetch>";


                        String attributeFormat = @"<attribute name = {0}>";

                        String configAttribute = ConfigurationManager.AppSettings["acountKey"];

                        String[] configAttSplit = configAttribute.Split(',');

                        for (var i = 0; i <= configAttSplit.Length; i++)
                        {


                            String attributefrmt = String.Format(configAttribute, attributeFormat[i]);

                           String fetchString = String.Format(fetchXml, configAttSplit[i]);
                            

                            IOrganizationService crmService = client.OrganizationServiceProxy;
                            EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetchString));


                            if (result != null) //Check whether the value of result 
                            {
                                foreach (var entity in result.Entities)
                                {


                                    if (entity.Attributes.Contains(configAttSplit[i]))
                                    {
                                        if (!string.IsNullOrEmpty(entity.GetAttributeValue<string>(configAttSplit[i])) && !string.IsNullOrEmpty(entity.GetAttributeValue<string>(configAttSplit[i+1])))
                                        {

                                            System.Console.Write(entity.Attributes[configAttSplit[i]]);
                                            System.Console.WriteLine(entity.Attributes[configAttSplit[i+1]]);

                                        }

                                    }
                                    else
                                    {
                                        return;
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("No records found for the account entity.");
                            }
                        }

                    }
                    else // contact information
                    {
                        String fetchXml = @"<fetch mapping='logical' distinct='true'>
                                                         <entity name='contact'> 
                                                         {0}
                                                            </entity> 
                                                       </fetch>";


                        String attributeFormat = @"<attribute name = {0}>";

                        String configAttribute = ConfigurationManager.AppSettings["contactKey"];

                        String[] configAttSplit = configAttribute.Split(',');

                        for (var i = 0; i < configAttSplit.Length; i++)
                        {


                            String attributefrmt = String.Format(configAttribute, attributeFormat[i]);

                            String fetchString = String.Format(fetchXml, configAttSplit[i]);

                            IOrganizationService crmService = client.OrganizationServiceProxy;
                            EntityCollection result = crmService.RetrieveMultiple(new FetchExpression(fetchString));

                            if (result != null)//Check whether the value of result 
                            {
                                foreach (var entity in result.Entities)
                                {
                                    if (entity.Attributes.Contains("name"))
                                    {
                                        System.Console.WriteLine(entity.Attributes["name"]);
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("No records found for the account entity.");
                            }
                        }
                    }
                }

                #region commented
                /*if (entityName.Equals("Account"))
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
                        foreach (var entity in result.Entities)
                        {
                            if (entity.Attributes.Contains("name"))
                            {
                                System.Console.WriteLine(entity.Attributes["name"]);
                            }

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
                        foreach (var entity in result.Entities)
                        {
                            if (entity.Attributes.Contains("fullname"))
                            {
                                System.Console.WriteLine(entity.Attributes["fullname"]);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found for the contact entity.");
                    }
                }*/
                #endregion
            }

            catch
            {
                throw;
            }
            Console.Read();
        }
    }
}