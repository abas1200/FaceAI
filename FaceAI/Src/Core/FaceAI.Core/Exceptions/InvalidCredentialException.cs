using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Exceptions
{
  
    public sealed class InvalidCredentialException : Exception
    { 
        public InvalidCredentialException(string message) :
            base(String.Format("Invalid Credential!  {0}", message)) 
        {
        } 
    }
}
