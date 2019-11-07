using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Models
{
    public class Dal
    {
        public DataTest getData()
        {
            DataTest toReturn = new DataTest();
            Random rand = new Random();

            toReturn.my_list = new List<string>();
            if (rand.Next(0, 100) % 2 == 0)
            {
                toReturn.my_list.Add("https://i.imgur.com/p5tuCE6.png");
            }
            if (rand.Next(0, 100) % 2 == 0)
            {
                toReturn.my_list.Add("https://i.imgur.com/tDsj0GH.jpg");
            }
            if (rand.Next(0, 100) % 2 == 0)
            {
                toReturn.my_list.Add("https://i.imgur.com/nTyPWlq.jpg");
            }
            if (rand.Next(0, 100) % 2 == 0)
            {
                toReturn.my_list.Add("https://i.imgur.com/jUcnjnI.jpg");
            }
            if (rand.Next(0, 100) % 2 == 0)
            {
                toReturn.my_list.Add("https://i.imgur.com/MCh4ZLp.jpg");
            }

            return toReturn;
        }
    }
}
