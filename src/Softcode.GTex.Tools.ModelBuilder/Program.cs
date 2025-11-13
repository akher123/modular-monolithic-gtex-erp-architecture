using System;

namespace Softcode.GTex.Tools.ModelBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Scaffold-DbContext "data source=180.92.239.110;initial catalog=GTEX;user id=sa;password=sa@#`$1;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Force
            //Scaffold-DbContext "data source=180.92.239.110;initial catalog=GTEX;user id=sa;password=sa@#`$1;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Schemas core,security -Force
        }
    }
}
