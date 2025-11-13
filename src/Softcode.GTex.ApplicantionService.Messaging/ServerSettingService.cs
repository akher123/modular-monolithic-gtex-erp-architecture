using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicantionService.Messaging.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicantionService.Messaging
{
    public class ServerSettingService : BaseService<ServerSettingService>, IServerSettingService
    {
        private readonly IRepository<EmailServer> serverRepository;

        public ServerSettingService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<EmailServer> serverRepository) :
            base(applicationServiceBuilder)
        {
            this.serverRepository = serverRepository;
        }

        public async Task<LoadResult> GetServerSettingListAsync(DataSourceLoadOptionsBase options)
        {
            options.Select = new[] { "id", "displayName", "isDefault", "protocol.name", "BusinessProfile.companyName", "CreatedByContact.DisplayName" };
            return await serverRepository.GetDevExpressListAsync(options);
        }

        public async Task<EmailServerModel> GetServerSettingAsync(int id)
        {
            if (id < 1)
            {
                return new EmailServerModel { BusinessProfileId = LoggedInUser.DefaultBusinessProfileId };
            }

            EmailServer sysServer = await serverRepository.FindOneAsync(x => x.Id == id);

            if (sysServer == null)
            {
                throw new SoftcodeNotFoundException("Email Server not found");
            }

            EmailServerModel val = this.Mapper.Map<EmailServerModel>(sysServer);

            val.Password = null;

            return val;
        }


        public async Task<int> SaveServerSettingAsync(int id, EmailServerModel model)
        {
            if (model == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid email server data");
            }

            EmailServer dbSP = new EmailServer();

            if (id > 0)
            {
                dbSP = await serverRepository.FindOneAsync(x => x.Id == id);

                if (dbSP == null)
                {
                    throw new SoftcodeNotFoundException("Security Profile not found for edit");
                }
            }


            this.Mapper.Map(model, dbSP);

            dbSP.Password = Encryption.Encrypt(dbSP.Password);

            if (model.IsDefault)
            {
                //set default = false for previous default record 
                serverRepository.Attach(serverRepository.Where(x => x.Id != model.Id && x.IsDefault).ToList()
                                                .Select(x => { x.IsDefault = false; return x; }).FirstOrDefault());
            }


            if (id > 0)
            {
                await serverRepository.UpdateAsync(dbSP);
            }
            else
            {
                await serverRepository.CreateAsync(dbSP);
            }
            return dbSP.Id;
        }

        public async Task<bool> DeleteEmailServerAsync(int id)
        {
            EmailServer server = await serverRepository.FindOneAsync(x => x.Id == id);
            if (server == null)
            {
                throw new SoftcodeNotFoundException("Email server not found");
            }
            if (server.IsDefault)
            {
                throw new SoftcodeInvalidDataException("Default email server cannot be deleted.");
            }

            return await serverRepository.DeleteAsync(server) > 0;

        }

        public bool SendTestEmail(EmailServerModel model)
        {
            EmailServer server = this.Mapper.Map<EmailServer>(model);

            using (ItmSmtpClient smtp = MailService.GetTestMailServerSettings(model, model.SenderId, model.SenderName))
            using (MailMessage message = MailService.GetTestMailMessage(smtp, model))
            {
                try
                {
                    if (!(message.From.Address != null && message.To.Count > 0 && smtp.Host != null && smtp.Port != 0))
                    {
                        throw new SoftcodeInvalidDataException("Invalid message object. Please check you have provided valid To, From, Host & Port");
                    }

                    smtp.Send(message);
                    return true;

                }
                catch (Exception ex)
                {    
                    throw new SoftcodeInvalidDataException("Failed to send test email",ex);
                }
            }
        }
 


    }
}
