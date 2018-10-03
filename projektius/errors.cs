using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vprokkis.Models;
using vprokkis.Processors;
using vprokkis.Controllers;

namespace vprokkis.Models
{
    [Serializable()]
    public class HealthCheckValidation : System.Exception
    {
        public HealthCheckValidation() : base() { }
        public HealthCheckValidation(string message) : base(message) { }
        public HealthCheckValidation(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected HealthCheckValidation(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
    [Serializable()]
    public class IdNotFound : System.Exception
    {
        public IdNotFound() : base() { }
        public IdNotFound(string message) : base(message) { }
        public IdNotFound(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected IdNotFound(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}