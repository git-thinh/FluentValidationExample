using System;

namespace FluentValidationExample.Models
{
    public class PersonModel
    {
        public Guid PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}