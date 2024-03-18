using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDemo.Helpers
{
    public interface IActionFactory
    { 
       Action GetAction();
        void CancelTask();
    }
}
