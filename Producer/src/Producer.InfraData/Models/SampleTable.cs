using System;

namespace Producer.InfraData.Models
{
    public class SampleTable
    {
        public Guid Id { get; set; }

        public string CustomerIdentity { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }
    }
}
