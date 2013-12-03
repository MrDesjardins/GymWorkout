using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfTestClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var service = new GymWorkoutService.WorkoutServiceClient();
            service.ClientCredentials.UserName.UserName = "123123";
            service.ClientCredentials.UserName.Password = "123123";
            var allWorkout = service.GetAllWorkout();
            foreach (var flattenWorkout in allWorkout)
            {
                Console.WriteLine("{0} {1}", flattenWorkout.UniqueIdentifier, flattenWorkout.Name);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
