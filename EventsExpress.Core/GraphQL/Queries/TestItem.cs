using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsExpress.Core.GraphQL.Queries
{
    public class TestItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
