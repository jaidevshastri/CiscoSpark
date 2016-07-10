using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace spark
{
    public class sparkRequestProgram
    {
        static void Main(string[] args)
        {
            sparkREST sparkRequest = new sparkREST();
            sparkRequest.createRoom(sparkRequest, "DevNet2016!");
            sparkRequest.createMembership(sparkRequest, sparkRequest.roomID, "codercollective@gmail.com");
            sparkRequest.createRoom(sparkRequest, sparkRequest.roomID);
        }
    }
}
