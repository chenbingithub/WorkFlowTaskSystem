using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowTaskSystem.Core.Session
{
   public interface IAbpSessionExtension: IAbpSession
    {
        new  string UserId { get;}
        string Email { get; }
    }
}
