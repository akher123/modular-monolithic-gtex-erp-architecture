import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigurationComponent } from './components/configurations/configuration-menu.component';
import { SystemConfigurationComponent } from './components/configurations/system-configuration/system-configuration.component';
import { SecurityConfigurationComponent } from './components/configurations/system-configuration/security-configuration.component';
import { DocumentExtensionFormComponent } from './components/configurations/system-configuration/document-extension/document-extension-form.component';

import { EmailServerFormComponent } from './components/configurations/system-configuration/email-server/email-server-form.component';

import { SecurityProfilesFormComponent } from "./components/configurations/system-configuration/security-profile/security-profiles-form.component";
import { SecurityPolicyComponent } from "./components/configurations/system-configuration/security-profile/security-policy.component";
import { PasswordSettingComponent } from "./components/configurations/system-configuration/security-profile/password-setting.component";
import { SessionAccessControlComponent } from "./components/configurations/system-configuration/security-profile/session-access-control.component";
import { SystemSettingsComponent } from "./system-settings.component";

import { BusinessProfileDetailsComponent } from "./components/business-profiles/business-profile-details.component";
import { BusinessProfileGeneralInformationComponent } from "./components/business-profiles/business-profile-general-information.component";
import { TypeAndCategoryComponent } from "./components/type-categories/type-and-category-menu.component";
import { CustomCategoriesComponent } from "./components/type-categories/custom-categories.component";
import { CustomCategoryFormComponent } from "./components/type-categories/custom-category-form.component";
import { UserFormComponent } from "./components/users/user-form.component";
import { ListComponent } from './../application-shared/components/list/list.component'
import { RoleFormComponent } from './components/roles/role-form.component';
import { ChangePasswordComponent } from './components/users/change-password.component';
import { CanDeactivateGuard } from '../../shared/can-deactivate/can-deactivate.guard';
import { ApplicantionMenuComponent } from './components/applicantion-menu/applicantion-menu.component';


const routes: Routes = [{
  path: '', component: SystemSettingsComponent,
  children: [
    { path: 'configurations', component: ConfigurationComponent, data: { title: 'System Settings' } },
    { path: 'system-configuration', component: SystemConfigurationComponent, data: { title: 'System Configuration' } },
    { path: 'business-profiles', component: ListComponent, data: { title: 'Business Profile' } },
    { path: 'business-profile', component: BusinessProfileDetailsComponent, data: { title: 'Business Profile' } },
    { path: 'business-profile/:businessProfileId', component: BusinessProfileDetailsComponent, data: { title: 'Business Profile' } },
    { path: 'types-and-categories', component: TypeAndCategoryComponent, data: { title: 'Type & Category' } },
    { path: 'type-and-category/:routingKey', component: CustomCategoriesComponent, data: { title: 'Type & Category' } },
    { path: 'type-and-category/:routingKey/new', component: CustomCategoryFormComponent, data: { title: 'Type & Category' } },
    { path: 'type-and-category/:routingKey/:id', component: CustomCategoryFormComponent, data: { title: 'Type & Category' } },
    { path: 'user', component: UserFormComponent, data: { title: 'User Detail' } },
    { path: 'user/:userid', component: UserFormComponent, data: { title: 'User Detail' } },
    { path: 'role', component: RoleFormComponent, data: { title: 'Role Detail' } },
    { path: 'role/:roleId', component: RoleFormComponent, data: { title: 'Role Detail' } },
    { path: 'system-configuration/security-profile', component: SecurityProfilesFormComponent, data: { title: 'Security Profile' } },
    { path: 'system-configuration/security-profile/:id', component: SecurityProfilesFormComponent, data: { title: 'Security Profile' } },
    { path: 'security-policy', component: SecurityPolicyComponent, data: { title: 'Security Policy' } },
    { path: 'password-setting', component: PasswordSettingComponent, data: { title: 'Password Settings' } },
    { path: 'session-access-control', component: SessionAccessControlComponent, data: { title: 'Session & Access Control' } },
    { path: 'system-configuration/email-server', component: EmailServerFormComponent, data: { title: 'Email Server' } },
    { path: 'system-configuration/email-server/:id', component: EmailServerFormComponent, data: { title: 'Email Server' } },
    { path: 'change-password', component: ChangePasswordComponent, data: { title: 'Email Server' } },

    { path: 'roles', component: ListComponent, data: { title: 'Roles' } },
    { path: 'users', component: ListComponent, data: { title: 'Users' } },
    { path: 'application-menus', component: ApplicantionMenuComponent, data: { title: 'Application Menu' } },
  ],
}];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SystemSettingsRoutingModule {
}

export const RoutedComponents = [
  BusinessProfileDetailsComponent
  , BusinessProfileGeneralInformationComponent
  , SystemSettingsComponent
  , ConfigurationComponent
  , SecurityConfigurationComponent
  , DocumentExtensionFormComponent
  , EmailServerFormComponent
  , SecurityProfilesFormComponent
  , SecurityPolicyComponent
  , PasswordSettingComponent
  , SessionAccessControlComponent
  , SystemConfigurationComponent
  , TypeAndCategoryComponent
  , CustomCategoriesComponent
  , CustomCategoryFormComponent
  , UserFormComponent
  , RoleFormComponent
  , ChangePasswordComponent
  ,ApplicantionMenuComponent
];

