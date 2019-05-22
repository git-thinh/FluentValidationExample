using System;
using FluentValidation.Attributes;

namespace FluentValidationExample.Models
{

    [Validator(typeof(PersonCreateRequestModelValidator))]
    public class PersonCreateRequestModel
    {
        public Guid PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}