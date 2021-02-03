using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace VotingApp
{
    public class VotingHub : Hub
    {

        public static Dictionary<string, int> poll = new Dictionary<string, int>(){
             {"Apply changes",25 },
             {"Refuse",25},
             {"Abstain",5},
             {"Another",25},
                    
        };


        public void Send(string name)
        {
            poll[name]++;
            string data = JsonConvert.SerializeObject(poll.Select(x => new { name = x.Key, count = x.Value }).ToList());


            Clients.All.showLiveResult(data);
        }
    }

}