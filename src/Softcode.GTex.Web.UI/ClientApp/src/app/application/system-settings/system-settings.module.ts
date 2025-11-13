import { NgModule } from '@angular/core';
import { SystemSettingsRoutingModule, RoutedComponents } from './system-settings-routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { DxTabPanelModule, DxCheckBoxModule, DxTemplateModule, DxToolbarModule, DxDataGridModule, DxSelectBoxModule, DxButtonModule, DxTextBoxModule, DxValidatorModule, DxValidationGroupModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxColorBoxModule, DxMultiViewModule, DxRadioGroupModule, DxTreeListModule, DxFileUploaderModule, DxTagBoxModule, DxDropDownBoxModule, DxTreeViewModule } from 'devextreme-angular';

//import application modules
import { ApplicationSharedModule } from './../application-shared/application-shared.module';
import { SystemServiceModule } from './../system-service/system-service.module'

//import currernt module components
import { ConfigurationComponent } from './components/configurations/configuration-menu.component';

import { DocumentExtensionFormComponent } from './components/configurations/system-configuration/document-extension/document-extension-form.component';

import { EmailServerFormComponent } from './components/configurations/system-configuration/email-server/email-server-form.component';
import { SecurityConfigurationComponent } from './components/configurations/system-configuration/security-configuration.component';
import { SecurityProfilesFormComponent } from './components/configurations/system-configuration/security-profile/security-profiles-form.component';
import { SecurityPolicyComponent } from './components/configurations/system-configuration/security-profile/security-policy.component';
import { PasswordSettingComponent } from './components/configurations/system-configuration/security-profile/password-setting.component';
import { SessionAccessControlComponent } from './components/configurations/system-configuration/security-profile/session-access-control.component';
import { TypeAndCategoryComponent } from './components/type-categories/type-and-category-menu.component';
import { CustomCategoriesComponent } from './components/type-categories/custom-categories.component';
import { CustomCategoryFormComponent } from "./components/type-categories/custom-category-form.component";
import { UserFormComponent } from "./components/users/user-form.component";
import { RoleFormComponent } from './components/roles/role-form.component';
import { ApplicantionMenuComponent } from './components/applicantion-menu/applicantion-menu.component';


//import current module services
import { EmailServerService } from './services/email-server.service'
import { SecurityProfileService } from './services/security-profile.service'
import { SystemConfigurationService } from './services/system-configuration.service'
import { BusinessProfileService } from './services/business-profile.service'
import { TypeAndCategoryService } from './services/type-and-category.service'
import { RoleService } from './services/role.service'
import { UserService } from './services/user.service'
import { ApplicationMenuService } from '../application-shared/services/application-menu.service';


@NgModule({
  imports: [SystemSettingsRoutingModule,
    FormsModule
    , CommonModule
    , TranslateModule
    , ReactiveFormsModule
    , DxTabPanelModule
    , DxCheckBoxModule
    , DxTemplateModule
    , DxDataGridModule
    , SystemServiceModule
    , DxToolbarModule
    , DxSelectBoxModule
    , DxTextAreaModule
    , DxDateBoxModule
    , DxFormModule
    , DxNumberBoxModule
    , DxTextBoxModule
    , DxValidatorModule
    , ApplicationSharedModule
    , DxValidationGroupModule
    , DxColorBoxModule
    , DxMultiViewModule
    , DxRadioGroupModule
    , DxTreeListModule
    , DxTreeViewModule
    , DxFileUploaderModule
    , DxTagBoxModule
    , DxDropDownBoxModule
  ],
  exports: [
    ConfigurationComponent
    , DocumentExtensionFormComponent
    , EmailServerFormComponent
    , SecurityConfigurationComponent
    , SecurityPolicyComponent

    , PasswordSettingComponent
    , SessionAccessControlComponent
    , SecurityProfilesFormComponent
    , TypeAndCategoryComponent
    , CustomCategoriesComponent
    , CustomCategoryFormComponent
    , UserFormComponent
    , RoleFormComponent
    , ApplicantionMenuComponent
  ],
  declarations: [
    ...RoutedComponents
  ],
  providers: [
    EmailServerService
    , SecurityProfileService
    , SystemConfigurationService
    , BusinessProfileService
    , TypeAndCategoryService
    , UserService
    , RoleService
    , ApplicationMenuService
  ]
})
export class SystemSettingsModule {
}
