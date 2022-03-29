using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.HRMS;

namespace Test.HRMS
{
    class Program
    {
        static void Main(string[] args)
        {
            
            EmployeeServices emp = new EmployeeServices();
            var any = emp.getEmployee();

        }
    }
}
