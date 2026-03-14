using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using GymSystemBLL.Services.Interfaces;

namespace GymSystemBLL.Services.Classes
{
    public class EmailService : IEmailService
    {
        public bool SendEmail(string username, string Email, string password)
        {
            try
            {
                var server = Environment.GetEnvironmentVariable("SMTP_HOST");
                var Port = Environment.GetEnvironmentVariable("SMTP_PORT");
                var smtpEmail = Environment.GetEnvironmentVariable("SMTP_USER");
                var smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASS");
                var From = Environment.GetEnvironmentVariable("SMTP_FROM");

                using var Client = new SmtpClient(server, int.Parse(Port!))
                {
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                    EnableSsl = true
                };

                var htmlBody = @"<!DOCTYPE html>
                        <html>
                        <head>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                background-color: #f4f4f4;
                                color: #333333;
                                padding: 20px;
                            }

                            .container {
                                max-width: 600px;
                                margin: auto;
                                background-color: #ffffff;
                                border-radius: 10px;
                                overflow: hidden;
                                box-shadow: 0 4px 12px rgba(0,0,0,0.1);
                            }

                            .header {
                                background-color: #070093;
                                color: white;
                                text-align: center;
                                padding: 20px;
                                font-size: 24px;
                                font-weight: bold;
                            }

                            .content {
                                padding: 20px;
                                font-size: 16px;
                                line-height: 1.5;
                            }

                            .btn {
                                display: inline-block;
                                background-color: #070093;
                                color: white !important;
                                padding: 10px 20px;
                                text-decoration: none;
                                border-radius: 50px;
                                margin-top: 20px;
                            }

                            .footer {
                                padding: 15px;
                                font-size: 12px;
                                text-align: center;
                                color: #999999;
                            }

                            .img-right {
                                width: 200px;
                                border-radius: 10px;
                            }
                        </style>
                        </head>

                        <body>

                        <div class='container'>

                            <div class='header'>Power Fitness</div>

                            <div class='content'>

                                <table width='100%' cellspacing='0' cellpadding='0'>
                                    <tr>

                                        <td style='vertical-align: top; padding-right:15px;'>

                                            <p>Hello <strong>@username</strong>,</p>

                                            <p>
                                            Welcome to <strong>Power Fitness</strong>!  
                                            Your account has been successfully created.
                                            </p>

                                            <p>
                                            Your Login Password <strong>@password</strong>.
                                            </p>

                                        </td>

                                        <td style='vertical-align: top; text-align:right;'>
                                            <img src='./images/about p2.jpg' class='img-right'/>
                                        </td>

                                    </tr>
                                </table>

                            </div>

                            <div class='footer'>
                                &copy; 2026 Power Fitness. All rights reserved.
                            </div>

                        </div>

                        </body>
                        </html>";
                htmlBody = htmlBody.Replace("@username", username);
                htmlBody = htmlBody.Replace("@password", password);

                var mailMessage = new MailMessage()
                {
                    From = new MailAddress(From!),
                    Subject = $"Gym Login Password",
                    Body = htmlBody,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(Email);
                Client.Send(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"COULDNT SEND THIS EMAIL, SOMTHING WRONF HAPPEN: {ex}");
                return false;
            }
        }
    }
}