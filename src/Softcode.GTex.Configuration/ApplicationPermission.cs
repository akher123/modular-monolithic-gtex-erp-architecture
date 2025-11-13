using System;

namespace Softcode.GTex.Configuration
{
    public static class ApplicationPermission
    {
        public struct BusinessProfile
        {
            /// <summary>
            /// Show Business Profile
            /// </summary>
            public const int ShowBusinessProfile = 1010101;
            /// <summary>
            /// View Business Profile Details
            /// </summary>
            public const int ViewBusinessProfileDetails = 1010102;
            /// <summary>
            /// Create Business Profile
            /// </summary>
            public const int CreateBusinessProfile = 1010103;
            /// <summary>
            /// Update Business Profile
            /// </summary>
            public const int UpdateBusinessProfile = 1010105;
            /// <summary>
            /// Delete Business Profile
            /// </summary>
            public const int DeleteBusinessProfile = 1010106;
            /// <summary>
            /// Delete Audit Trail Log
            /// </summary>
            public const int DeleteAuditTrailLog = 1010108;
        }

        public struct Configuration
        {
            /// <summary>
            /// Delete User Session Log
            /// </summary>
            public const int ShowConfigurationMenu = 1010109;
            /// <summary>
            /// Delete User Authentication Log
            /// </summary>
            public const int ShowTypesAndCategoriesMenu = 1010110;
            /// <summary>
            /// Delete Application Error Log
            /// </summary>
            public const int DeleteApplicationErrorLog = 1010111;
            /// <summary>
            /// View Configuration
            /// </summary>
            public const int ViewConfiguration = 1010201;
            /// <summary>
            /// Update Configuration
            /// </summary>
            public const int UpdateConfiguration = 1010202;
        }

        public struct TypesAndCategories
        {
            /// <summary>
            /// Types & Categories menu
            /// </summary>
            public const int TypesAndCategoriesMenu = 1010301;
            /// <summary>
            /// Manage Rating Type
            /// </summary>
            public const int ManageRatingType = 1010302;
            /// <summary>
            /// Manage Document Type
            /// </summary>
            public const int ManageDocumentType = 1010303;
            /// <summary>
            /// Manage Communication Method
            /// </summary>
            public const int ManageCommunicationMethod = 1010304;
            /// <summary>
            /// Manage Address Type

            /// </summary>
            public const int ManageAddressType
                = 1010305;
            /// <summary>
            /// Manage Security Question
            /// </summary>
            public const int ManageSecurityQuestion = 1010306;
            /// <summary>
            /// Manage Communication Status

            /// </summary>
            public const int ManageCommunicationStatus
                = 1010307;
            /// <summary>
            /// Manage Bank Account Type
            /// </summary>
            public const int ManageBankAccountType = 1010308;
            /// <summary>
            /// Manage Position Type
            /// </summary>
            public const int ManagePositionType = 1010309;
            /// <summary>
            /// Manage Company Ratings Type
            /// </summary>
            public const int ManageCompanyRatingsType = 1010310;
            /// <summary>
            /// Manage Contact Method Type
            /// </summary>
            public const int ManageContactMethodType = 1010311;
            /// <summary>
            /// Manage Industry Type
            /// </summary>
            public const int ManageIndustryType = 1010312;
            /// <summary>
            /// Manage Membership Type
            /// </summary>
            public const int ManageMembershipType = 1010313;
            /// <summary>
            /// Manage Organisation Type
            /// </summary>
            public const int ManageOrganisationType = 1010314;
            /// <summary>
            /// Manage Title Type
            /// </summary>
            public const int ManageTitleType = 1010315;
            /// <summary>
            /// Manage IM Type
            /// </summary>
            public const int ManageIMType = 1010316;
            /// <summary>
            /// Manage Preferred Contact Method
            /// </summary>
            public const int ManagePreferredContactMethod = 1010317;
            /// <summary>
            /// Manage Skill Type
            /// </summary>
            public const int ManageSkillType = 1010318;
            /// <summary>
            /// Manage Contact Type
            /// </summary>
            public const int ManageContactType = 1010319;
            /// <summary>
            /// Manage Gender Type
            /// </summary>
            public const int ManageGenderType = 1010320;
            /// <summary>
            /// Manage Employment Type
            /// </summary>
            public const int ManageEmploymentType = 1010321;
        }

        public struct User
        {
            /// <summary>
            /// Show User List
            /// </summary>
            public const int ShowUserList = 1010401;
            /// <summary>
            /// View User Details
            /// </summary>
            public const int ViewUserDetails = 1010402;
            /// <summary>
            /// Create User
            /// </summary>
            public const int CreateUser = 1010403;
            /// <summary>
            /// Update User
            /// </summary>
            public const int UpdateUser = 1010404;
            /// <summary>
            /// Delete User
            /// </summary>
            public const int DeleteUser = 1010405;
        }

        public struct Role
        {
            /// <summary>
            /// Show Roles List
            /// </summary>
            public const int ShowRolesList = 1010501;
            /// <summary>
            /// View Roles Details
            /// </summary>
            public const int ViewRolesDetails = 1010502;
            /// <summary>
            /// Create Role
            /// </summary>
            public const int CreateRole = 1010503;
            /// <summary>
            /// Update Role
            /// </summary>
            public const int UpdateRole = 1010504;
            /// <summary>
            /// Delete Role
            /// </summary>
            public const int DeleteRole = 1010505;
        }

        public struct Region
        {
            /// <summary>
            /// Region menu
            /// </summary>
            public const int RegionMenu = 1010601;
        }

        public struct SecurityProfile
        {
            /// <summary>
            /// Show Security Profile List
            /// </summary>
            public const int SecurityProfileList = 1010701;
            /// <summary>
            /// View Security Profile
            /// </summary>
            public const int ViewSecurityProfile = 1010702;
            /// <summary>
            /// Create Security Profile
            /// </summary>
            public const int CreateSecurityProfile = 1010703;
            /// <summary>
            /// Update Security Profile
            /// </summary>
            public const int UpdateSecurityProfile = 1010704;
            /// <summary>
            /// Delete Security Profile
            /// </summary>
            public const int DeleteSecurityProfile = 1010705;
        }

        public struct Document
        {
            /// <summary>
            /// Show Documents List
            /// </summary>
            public const int ShowDocumentList = 1040101;
            /// <summary>
            /// Upload Document
            /// </summary>
            public const int UploadDocument = 1040102;
            /// <summary>
            /// Download Document
            /// </summary>
            public const int DownloadDocument = 1040103;
            /// <summary>
            /// View Document Details
            /// </summary>
            public const int ViewDocumentDetails = 1040104;
            /// <summary>
            /// Create Document
            /// </summary>
            public const int CreateDocument = 1040105;
            /// <summary>
            /// Update Document
            /// </summary>
            public const int UpdateDocument = 1040106;
            /// <summary>
            /// Delete Document
            /// </summary>
            public const int DeleteDocument = 1040107;
            /// <summary>
            /// Get Documents By Entity
            /// </summary>
            public const int GetDocumentsByEntity = 1040108;
            /// <summary>
            /// Save Documents By Entity
            /// </summary>
            public const int SaveDocumentsByEntity = 1040109;
        }

        public struct NotesAndComms
        {
            /// <summary>
            /// Note & Comm Detail Menu
            /// </summary>
            public const int ViewNoteAndCommDetail = 1050101;
            /// <summary>
            /// Note & Comm List Menu
            /// </summary>
            public const int ShowNoteAndCommList = 1050102;
            /// <summary>
            /// Create Note & Comm List Menu
            /// </summary>
            public const int CreateNoteAndCommList = 1050103;
            /// <summary>
            /// Update Note & Comm List Menu
            /// </summary>
            public const int UpdateNoteAndCommList = 1050104;
            /// <summary>
            /// Delete Note & Comm List Menu
            /// </summary>
            public const int DeleteNoteAndCommList = 1050105;
        }

        public struct Employee
        {
            /// <summary>
            /// Show Employee List
            /// </summary>
            public const int ShowEmployeeList = 1060101;
            /// <summary>
            /// View Employee Detail
            /// </summary>
            public const int ViewEmployeeDetails = 1060102;
            /// <summary>
            /// Create Employee
            /// </summary>
            public const int CreateEmployee = 1060103;
            /// <summary>
            /// Update Employee
            /// </summary>
            public const int UpdateEmployee = 1060104;
            /// <summary>
            /// Delete Employee
            /// </summary>
            public const int DeleteEmployee = 1060105;
        }

        public struct Company
        {
            /// <summary>
            /// Show Company List
            /// </summary>
            public const int ShowCompanyList = 1070101;
            /// <summary>
            /// View Company Details
            /// </summary>
            public const int ViewCompanyDetails = 1070102;
            /// <summary>
            /// Create Company
            /// </summary>
            public const int CreateCompany = 1070103;
            /// <summary>
            /// Update Company
            /// </summary>
            public const int UpdateCompany = 1070104;
            /// <summary>
            /// Delete Company
            /// </summary>
            public const int DeleteCompany = 1070105;
        }

        public struct Contact
        {
            /// <summary>
            /// Show Contact List
            /// </summary>
            public const int ShowContactList = 1070201;
            /// <summary>
            /// View Contact Details
            /// </summary>
            public const int ViewContactDetails = 1070202;
            /// <summary>
            /// Create Contact
            /// </summary>
            public const int CreateContact = 1070203;
            /// <summary>
            /// Update Contact
            /// </summary>
            public const int UpdateContact = 1070204;
            /// <summary>
            /// Delete Contact
            /// </summary>
            public const int DeleteContact = 1070205;
        }

        public struct Customer
        {
            /// <summary>
            /// Customer List Menu
            /// </summary>
            public const int CustomerListMenu = 1070301;
            /// <summary>
            /// Customer Detail Menu
            /// </summary>
            public const int CustomerDetailMenu = 1070302;
        }

        public struct Reports
        {
        }

    }
}
