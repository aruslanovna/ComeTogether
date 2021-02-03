
using ComeTogether.Domain.MessageModel;
using System;
using System.Collections.Generic;
using System.Text;


namespace ComeTogether.Service.Interfaces
{
   
   
        public interface IEmailSender
        {
            void SendEmail(Message message);
        }
    

}
