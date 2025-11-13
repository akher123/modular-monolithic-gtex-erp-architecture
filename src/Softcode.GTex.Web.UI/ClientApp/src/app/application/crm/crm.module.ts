import { NgModule } from '@angular/core';
import { DxTextAreaModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxDataGridModule, DxSelectBoxModule, DxCheckBoxModule, DxButtonModule, DxToolbarModule, DxTextBoxModule, DxValidatorModule, DxValidationGroupModule, DxTabPanelModule, DxTagBoxModule, DxRadioGroupModule, DxDropDownBoxModule  } from 'devextreme-angular';
import { CrmRoutingModule, RoutedComponents } from './crm-routing.module'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { ApplicationSharedModule } from './../application-shared/application-shared.module';
import { ContactDetailComponent } from './components/contact/contact-detail.component'

import { CompanyService } from './services/company.service'
import { ContactService } from './services/contact.service';
import { CompanyDetailComponent } from './components/company/company-detail.component';
import { SystemServiceModule } from './../system-service/system-service.module';

@NgModule({
    imports: [CrmRoutingModule
        , DxDataGridModule
        , DxSelectBoxModule
        , DxCheckBoxModule
        , DxButtonModule
        , FormsModule
        , CommonModule
        , TranslateModule
        , ReactiveFormsModule
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
        , DxTabPanelModule
        , DxTagBoxModule
        , DxRadioGroupModule
        , DxDropDownBoxModule
        , SystemServiceModule
    ],
    exports: [
        ContactDetailComponent,  CompanyDetailComponent
    ],
    declarations: [
        ...RoutedComponents
    ],
    providers: [
        CompanyService,
        ContactService
    ]
})
export class CrmModule
{
}
