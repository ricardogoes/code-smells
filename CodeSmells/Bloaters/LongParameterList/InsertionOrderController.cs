using System;
using System.Linq;

namespace CodeSmells.Bloaters.LongParameterList
{
    class InsertionOrderController
    {
        public object MapInsertionOrderToView(object order, Guid currentUserId)
        {
            User saleRepUser, creatorUser;
            int apId, mId;
            string apName, apPhone, apEmail, mName, mPhone, mEmail;
            bool apAlone, mAlone;
            this.LoadContacts(order, currentUserId, out saleRepUser, 
                out creatorUser, out apId, out apName, out apPhone, 
                out apEmail, out apAlone, out mId, out mName, 
                out mPhone, out mEmail, out mAlone);

            // do some work; return a View Model
            return null;
        }

        private void LoadContacts(object order, 
            Guid currentUserId, 
            out User saleRepUser, 
            out User creatorUser, 
            out int apId, 
            out string apName, 
            out string apPhone, 
            out string apEmail, 
            out bool apAlone, 
            out int mId, 
            out string mName, 
            out string mPhone, 
            out string mEmail, 
            out bool mAlone)
        {
            saleRepUser = new User();
            creatorUser = new User();
            apName = string.Empty;
            apPhone = string.Empty;
            apEmail = string.Empty;
            mName = string.Empty;
            mPhone = string.Empty;
            mEmail = string.Empty;
            apAlone = false;
            mAlone = false;
            apId = 0;
            mId = 0;
            bool contactsFound = false;

            // do a bunch of work to set these values
        }
    }
}