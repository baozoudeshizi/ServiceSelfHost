using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HY.Common
{
    public interface IUser
    {
        string ID { get; }

        string Name { get; }
        
        //string Token { get; set; }
    }
}
