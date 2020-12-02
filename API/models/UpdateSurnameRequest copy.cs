using System;
using System.Collections.Generic;

namespace API.models
{
    public class UpdateSurnameRequest
    {
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string NewSurname { get; set; }

    }
}
