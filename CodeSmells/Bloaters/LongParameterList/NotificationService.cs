using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSmells.Bloaters.LongParameterList
{
    public class NotificationService
    {
        public void EmailCustomerInformationRequest(
            string emailTemplatePath,
            string fromName,
            string company,
            string fromEmailAddress,
            List<string> interests,
            string productUrl,
            string userMessage,
            string supportEmailAddress,
            string accountManagerAddress)
        {
            // Create and send an email
        }
    }
}