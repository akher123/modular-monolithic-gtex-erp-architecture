import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { DxToolbarModule, DxDataGridModule, DxValidationGroupModule, DxMultiViewModule, DxRadioGroupModule, DxButtonModule } from 'devextreme-angular';
import { ApplicationSharedModule } from '../application/application-shared/application-shared.module';
import { ApplicationComponent } from '../application/application.component';
import { DashboardComponent } from '../itm-admin/dashboard/dashboard.component';

import { ItmAdminRouterModule, RoutedComponents } from './itm-admin-routing.module';
import { dynamicPageService } from './services/dynamic-page.service';

@NgModule({
    imports: [ItmAdminRouterModule
        ,CommonModule
        , ReactiveFormsModule
        , FormsModule
        , DxToolbarModule
        , ApplicationSharedModule
        , DxDataGridModule
        , DxValidationGroupModule
        , DxMultiViewModule
        , DxRadioGroupModule
        , DxButtonModule
    ],
    declarations: [
        ...RoutedComponents
    ],
    exports: [

    ],
    providers: [
        dynamicPageService    
    ]
})
export class ItmAdminModule {
}



