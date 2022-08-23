using System;

namespace Test
{
    public class AdviceDTO
    {
      public int total_results {get;set;}
      public string query {get;set;}
      public Slip[] slips {get;set;}
    }

    public class Slip
    {
        public int id {get;set;}
        public string advice {get;set;}
        public DateTime date {get;set;}
    }
}
