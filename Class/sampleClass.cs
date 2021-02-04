using System;
using System.Linq;

namespace Contact.Class.General
{
    public class Contact
    {
        public Models.General.Contact Send()
        {
            try
            {
                Models.General.Contact contact = new Models.General.Contact();

                using (Data.DCContent dc = new Data.DCContent())
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
                        contact.cCompanyName = resultCustomers.cCompanyName;
                        contact.cPhone = resultContacts.cPhone;
                        contact.cGsm = resultContacts.cGsm;
                        contact.cFax = resultContacts.cFax;
                        contact.cAdress = resultContacts.cAdress;
                        contact.cEmail = resultContacts.cEMail;
                        contact.cFacebookUrl = resultContacts.cFacebook;
                        contact.cTwitterUrl = resultContacts.cTwitter;
                        contact.cGooglePlusUrl = resultContacts.cGooglePlus;
                        contact.cYoutubeUrl = resultContacts.cYoutube;
                        contact.cInstagramUrl = resultContacts.cInstagram;
                        contact.cLinkedinUrl = resultContacts.cLinkedin;
                        contact.cFoursquareUrl = resultContacts.cFoursquare;
                        contact.cPinterestUrl = resultContacts.cPinterest;
                        contact.cLongitude = resultContacts.cLongitude;
                        contact.cLatitude = resultContacts.cLatitude;
                        contact.cGoogleMapsLink = resultContacts.cGoogleMaps;
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



