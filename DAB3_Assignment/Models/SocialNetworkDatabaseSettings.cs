﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAB3_Assignment.Models
{
    public class SocialNetworkDatabaseSettings : ISocialNetworkDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string UpdatesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISocialNetworkDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string UpdatesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
