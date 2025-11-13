import { NgModule } from '@angular/core';
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TitlebarComponent } from './components/titlebar/titlebar.component'
import { SidebarComponent } from './components/sidebar/sidebar.component'
import { HeaderComponent } from './components/header/header.component'
import { ImageUploadComponent } from './components/common/image-upload.component';
import { DxDataGridModule, DxSelectBoxModule, DxCheckBoxModule, DxButtonModule, DxTreeListModule, DxToolbarModule, DxTextBoxModule, DxValidatorModule, DxFileUploaderModule, DxDropDownBoxModule, DxValidationGroupModule, DxAutocompleteModule, DxTreeViewModule } from 'devextreme-angular';
import { RouterModule } from '@angular/router';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ListService } from './services/list.service';

import { EntitySelectBoxService } from './services/entity-select-box.service';
import { ApplicationMenuService } from './services/application-menu.service';
import { ListComponent } from './components/list/list.component';
import { DetailGroupComponent } from './components/detail/detail-group.component';

import { EntitySelectBoxComponent } from './components/common/app-entity-select-box.component'
import { TemplateDetailFormComponent } from './template/detail-page/template-detail-form.component';
import { FormsModule } from '@angular/forms';
import { TreeViewDropDownBoxComponent } from './components/common/app-tree-view-drop-down-box.component';
import { UtilityService } from './services/utility.service';


@NgModule({
    imports: [
        CommonModule
        , FormsModule
        , DxToolbarModule
        , DxDataGridModule
        , DxCheckBoxModule
        , RouterModule
        , TranslateModule
        , NgbModule
        , DxFileUploaderModule
        , DxDropDownBoxModule
        , DxValidatorModule
        , DxValidationGroupModule
        , DxSelectBoxModule
        , DxTextBoxModule
        , DxAutocompleteModule
        , DxTreeListModule
        , DxTreeViewModule
    ],
    declarations: [
        TitlebarComponent
        , SidebarComponent
        , HeaderComponent
        , ListComponent
        , DetailGroupComponent
        , TreeViewDropDownBoxComponent
        , EntitySelectBoxComponent
        , ImageUploadComponent
        , TemplateDetailFormComponent
    ],
    exports: [
        TitlebarComponent
        , SidebarComponent
        , HeaderComponent
        , ListComponent
        , DetailGroupComponent
        , TreeViewDropDownBoxComponent
        , EntitySelectBoxComponent
        , ImageUploadComponent
        , TemplateDetailFormComponent
    ],
    providers: [
        ListService,        
        EntitySelectBoxService,
        UtilityService,
        ApplicationMenuService
    ]
})
export class ApplicationSharedModule
{

}
