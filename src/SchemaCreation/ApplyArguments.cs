using CommandDotNet;

namespace SchemaCreation
{
    public class ApplyArguments : IArgumentModel 
    {
        [Option(shortName: 't', longName: "AADTenantId", Description = "Azure Active Directory Tenant Id")]
        public string AADTenantId { get; set; } = "";

        [Option(shortName: 'c', longName: "ClientId", Description = "Your SPN Client Id")]
        public string ClientId { get; set; } = "";

        [Option(shortName: 's', longName: "ClientSecretKey", Description = "Your SPN Client Secret Key")]
        public string ClientSecretKey { get; set; } = "";

        [Option(shortName: 'e', longName: "EventHub", Description = "EventHub name")]
        public string EventHub { get; set; } = "";

        [Option(shortName: 'g', longName: "SchemaGroup", Description = "Schema group")]
        public string SchemaGroup { get; set; } = "";

        [Option(shortName: 'f', longName: "File", Description = "The avro file to apply")]
        public string File { get; set; } = "";
    }
}
