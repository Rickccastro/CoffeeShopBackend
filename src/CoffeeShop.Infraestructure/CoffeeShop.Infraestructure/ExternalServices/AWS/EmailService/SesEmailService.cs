using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using CoffeeShop.Application.ExternalServices.Contracts.AWS;
using CoffeeShop.Application.ExternalServices.DTO.AWS;
using Microsoft.Extensions.Options;

namespace CoffeeShop.Infraestructure.ExternalServices.AWS.EmailService
{
    public class SesEmailService : ISesEmailService
    {
        private readonly AmazonSimpleEmailServiceClient _client;
        private readonly string _fromAddress;
        private readonly string _configurationSet;
        public SesEmailService(IOptions<AwsEmailSettings> options)
        {
            var settings = options.Value;
            var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
            _client = new AmazonSimpleEmailServiceClient(credentials, RegionEndpoint.GetBySystemName(settings.Region));

            _fromAddress = settings.FromAddress!;
            _configurationSet = settings.ConfigurationSet!;
        }

        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
                var sendRequest = new SendEmailRequest
                {
                    Source = _fromAddress,
                    Destination = new Destination { ToAddresses = new List<string> { toAddress } },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body { Text = new Content(body) },                    
                    },
                    ConfigurationSetName = _configurationSet,
                };

                await _client.SendEmailAsync(sendRequest); 
        }
    }
}

