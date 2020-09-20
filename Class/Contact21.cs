using System;
using System.Linq;

namespace cogenNugetContact.Class.General
{
    public class Contact21
    {
        public Models.General.Contact21 Send()
        {
            try
            {
                Models.General.Contact21 contact = new Models.General.Contact21();

                using (Data.DCCogenNugetContent dc = new Data.DCCogenNugetContent())
                {
                    var resultCustomers = (from table in dc.Customers
                                           where
                                             table.iCodeCustomer == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeCustomer"]) &&
                                             table.iAktifMi == 1
                                           select table).FirstOrDefault();

                    var resultContacts = (from table in dc.Contacts
                                          where
                                            table.iCodeWebSite == Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["iCodeWebSite"]) &&
                                            table.iCenter == 1 &&
                                            table.iActive == 1
                                          select table).FirstOrDefault();

                    if (resultCustomers != null && resultContacts != null)
                    {
                        contact.cCompanyName = resultCustomers.cCustomerFooter;
                        contact.cPhone = resultContacts.cTelephone;
                        contact.cGsm = resultContacts.cGSM;
                        contact.cFax = resultContacts.cFaks;
                        contact.cAdress = resultContacts.cAddress;
                        contact.cEmailAdress = resultContacts.cEMail;
                        contact.cFacebookURL = resultContacts.cFacebook;
                        contact.cTwitterURL = resultContacts.cTwitter;
                        contact.cGooglePlusURL = resultContacts.cGooglePlus;
                        contact.cYoutubeURL = resultContacts.cYoutube;
                        contact.cInstagramURL = resultContacts.cInstagram;
                        contact.cLinkedinURL = resultContacts.cLinkedin;
                        contact.cFoursquareURL = resultContacts.cFoursquare;
                        contact.cPinterestURL = resultContacts.cPinterest;
                        contact.cLongitude = resultContacts.cLongitude;
                        contact.cLatitude = resultContacts.cLatitude;
                        contact.cGoogleMapsLink = resultContacts.cGoogleMapsText;
                        contact.cWhatsApp = resultContacts.cWhatsApp;
                    }
                }

                return contact;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
}