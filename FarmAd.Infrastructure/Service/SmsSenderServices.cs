using FarmAd.Application.Exceptions;
using FarmAd.Application.Abstractions.Services.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ferma.Service.Services.Implementations.User
{
    public class SmsSenderServices : ISmsSenderServices
    {
        private readonly IConfiguration _configuration;

        public SmsSenderServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SmsSend(string number, string code)
        {
            //Mesaj məzmunu
            string newText = "Kod: " + code + " Etibarliliq muddeti - 10 deqiqe. https://minimall.az/";
            // XML məzmunu 
            string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
                                <SMS-InsRequest>
                                    <CLIENT user=""username"" pwd=""apikey"" from=""senderName""/>
                                    <INSERTMSG text=""metn"">
                                        <TO>nomre</TO>
                                    </INSERTMSG>
                                </SMS-InsRequest>";
            xmlContent = xmlContent.Replace("metn", newText);
            xmlContent = xmlContent.Replace("nomre", number);
            xmlContent = xmlContent.Replace("username", _configuration.GetSection("SmsService:Username").Value);
            xmlContent = xmlContent.Replace("apikey", _configuration.GetSection("SmsService:ApiKey").Value);
            xmlContent = xmlContent.Replace("senderName", _configuration.GetSection("SmsService:Sendername").Value);
            // HttpClient 
            using (HttpClient client = new HttpClient())
            {
                // İstek məzmununu ayarla
                StringContent content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");
                // İstek göndər
                HttpResponseMessage response = await client.PostAsync("https://gw.soft-line.az/sendsms", content);

                // Response yoxlanması
                if (response.IsSuccessStatusCode)
                    return true;
                else
                    throw new SmsSenderException("Xəta baş verdi!");
            }

        }
    }
}
