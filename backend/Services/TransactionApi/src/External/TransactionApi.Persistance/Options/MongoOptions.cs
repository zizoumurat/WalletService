﻿namespace TransactionApi.Persistance.Options;

public class MongoOptions
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
