﻿namespace CoffeeShop.Domain.ExternalServices.AWS.Email
{
    public class AwsEmailSettings
    {
        public string Region { get; set; } = null!;
        public string AccessKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string FromAddress { get; set; } = null!;
        public string ConfigurationSet { get; set; } = null!;
    }
}
