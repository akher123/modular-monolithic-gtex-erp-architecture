using System;

namespace Softcode.GTex.Configuration
{
    public static class ApplicationBusinessCategoryType
    {
        public const int EmailServerAuthenticationType = 101;
        public const int EmailServerProtocol = 102;
        public const int EmailServerSenderOption = 103;
        public const int RelationshipType = 104;
        public const int EmailQueueStatus = 105;
        public const int EmailTemplateType = 106;
        public const int EmailType = 107;
        public const int ApplicationContactType = 108;
        public const int PasswordCombinationType = 109;
        public const int PreferredPhoneType = 110;
    }

    public static class EmailServerAuthenticationType
    {
        public const int Usecommonserverauthentication = 10101;
    }

    public static class EmailServerProtocol
    {
        public const int POP_IMAPI_HTTP = 10201;
        public const int MicrosoftExchange = 10203;
    }

    public static class EmailServerSenderOption
    {
        public const int Sendemailusinguserdetail = 10301;
        public const int Sendemailusingserverdetail = 10302;
    }

    public static class RelationshipType
    {
        public const int Customers = 10401;
        public const int Vendors = 10402;
    }

    public static class EmailQueueStatus
    {
        public const int Pending = 10501;
        public const int InProgress = 10502;
        public const int Sent = 10503;
        public const int Failed = 10504;
        public const int Finished = 10505;
    }

    public static class EmailTemplateType
    {
        public const int DefaultTemplate = 10601;
        public const int AccountPasswordChangeTemplate = 10602;
    }

    public static class EmailType
    {
        public const int SingleEmail = 10701;
        public const int MassEmail = 10702;
    }

    public static class ApplicationContactType
    {
        public const int Contact = 10801;
        public const int Employee = 10802;
        public const int User = 10803;
        public const int ServiceUser = 10804;
    }

    public static class PasswordCombinationType
    {
        public const int Any = 10901;
        public const int AtLeast2Types = 10902;
        public const int AtLeast3Types = 10903;
        public const int All4Types = 10904;
    }

    public static class PreferredPhoneType
    {
        public const int BusinessPhone = 11001;
        public const int HomePhone = 11002;
        public const int MobilePhone = 11003;
    }

}
