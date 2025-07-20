using Amazon;
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
        private readonly AwsEmailSettings _settings;

        public SesEmailService(IOptions<AwsEmailSettings> options)
        {
            _settings = options.Value;

            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(
                _settings.AccessKey,
                _settings.SecretKey
            );

            _client = new AmazonSimpleEmailServiceClient(awsCredentials, RegionEndpoint.GetBySystemName(_settings.Region));
        }

        public async Task SendEmailAsync(string toAddress, string subject, string body)
        {
                var sendRequest = new SendEmailRequest
                {
                    Source = _settings.FromAddress,
                    Destination = new Destination { ToAddresses = new List<string> { toAddress } },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body { Text = new Content(body) },                    
                    },
                    ConfigurationSetName = _settings.ConfigurationSet,
                };

                await _client.SendEmailAsync(sendRequest); 
        }
    }
}

